create database dbSports;
use dbSports;

create table tbTime(
IdTime int primary key auto_increment,
Nome varchar(250) not null,
Abreviacao char(3) not null unique,
Img varchar(900) not null 
);

create table tbPosicao(
IdPosicao int primary key auto_increment,
Nome varchar(30) not null
);

create table tbJogador(
IdJogador int primary key auto_increment,
NomeCompleto varchar(250) not null,
NomeCamisa varchar(12) not null,
Idade smallint not null,
NumeroCamisa smallint not null,
IdTime int,
foreign key (IdTime) references tbTime(IdTime),
IdPosicao int not null,
foreign key (IdPosicao) references tbPosicao(IdPosicao)
);

create table tbEstatisticasJogador(
IdEst int primary key auto_increment,
ChutesFora int not null default 0,
ChutesGol int not null default 0,
Gols int not null default 0,
Dribles int not null default 0,
Assistencias int not null default 0, 
Passes int not null default 0,
Cruzamentos int not null default 0,
Impedimentos int not null default 0,
Desarmes int not null default 0,
DuelosGanhos int not null default 0,
Interceptacoes int not null default 0,
BolasDefendidas int not null default 0,
BolasDificeisDefendidas int not null default 0,
GolsSofridos int not null default 0,
FaltasSofridas int not null default 0,
FaltasCometidas int not null default 0,
PenaltisSofridos int not null default 0,
PenaltisCometidos int not null default 0,
CartoesAmarelos int not null default 0,
CartoesVermelhos int not null default 0,
GolsPenaltis int not null default 0,
GolsPenaltisPerdido int not null default 0,
DefesasPenaltis int not null default 0,
GolsPenaltisSofridos int not null default 0,
IdJogador int not null,
foreign key (IdJogador) references tbJogador(IdJogador)
);

create table tbCampeonato(
IdCamp int primary key auto_increment,
Nome varchar(200) not null
);

-- tabela na relação N:N para evitar duplicação com a chave primaria composta
create table tbParticipante(
IdCamp int not null,
IdTime int not null,
Situacao varchar(50) not null default 'Ativo', -- Ativo do campeonato ou Eliminado ou Campeão da competição
foreign key (IdCamp) references tbCampeonato(IdCamp),
foreign key (IdTime) references tbTime(IdTime),
primary key (IdCamp, IdTime)
);

create table tbPartida(
IdPartida int primary key auto_increment,
PlacarTimeCasa smallint not null default 0,
PlacarTimeVisitante smallint not null default 0,
IdCamp int not null,
IdTimeCasa int not null,
IdTimeVisitante int not null,
foreign key (IdCamp, IdTimeCasa) references tbParticipantes(IdCamp, IdTime),
foreign key (IdCamp, IdTimeVisitante) references tbParticipantes(IdCamp, IdTime)
);



insert into tbPosicao(Nome) values
('Goleiro'),
('Zagueiro'),
('Líbero'),
('Fixo'),
('Lateral Direito'),
('Lateral Esquerdo'),
('Ala'),
('Ala Direito'),
('Ala Esquerdo'),
('Volante'),
('Meio-Central'),
('Meio-Atacante'),
('Ponta'),
('Ponta Direita'),
('Ponta Esqueda'),
('Atacante'),
('Pivô'),
('Centroavante');



delimiter //
create procedure CadastrarJogador(
_NomeCompleto varchar(250),
_NomeCamisa varchar(12),
_Idade smallint,
_NumeroCamisa smallint,
_IdTime int,
_IdPosicao int
)
begin
	INSERT INTO tbJogador(NomeCompleto, NomeCamisa, Idade, NumeroCamisa, IdTime, IdPosicao) values 
                         (_NomeCompleto, _NomeCamisa, _Idade, _NumeroCamisa, _IdTime, _IdPosicao);
    
    set @IdJogador = LAST_INSERT_ID();
    
    INSERT INTO tbEstatisticasJogador(IdJogador) values
								     (@IdJogador);
end //



delimiter //
create procedure ObterEstatisticasJogador(
_IdJogador int
) 
begin
	select 
		E.IdEst,
		E.ChutesFora,
		E.ChutesGol,
        E.Gols,
        E.Dribles,
        E.Assistencias,
        E.Passes,
        E.Cruzamentos,
        E.Impedimentos,
        E.Desarmes,
        E.DuelosGanhos,
        E.Interceptacoes,
        E.BolasDefendidas,
        E.BolasDificeisDefendidas,
        E.GolsSofridos,
        E.FaltasSofridas,
        E.FaltasCometidas,
        E.PenaltisSofridos,
        E.PenaltisCometidos,
        E.CartoesAmarelos,
        E.CartoesVermelhos,
        E.GolsPenaltis,
        E.GolsPenaltisPerdido,
        E.DefesasPenaltis,
        E.GolsPenaltisSofridos,
        E.IdJogador,
        J.NomeCompleto,
        P.Nome
    from tbEstatisticasJogador as E
	inner Join
		tbJogador as J on E.IdJogador = J.IdJogador
	inner join
		tbPosicao as P on J.IdPosicao = P.IdPosicao
	where E.IdJogador = _IdJogador;
end //



delimiter //
create procedure DeletarTime(
_IdTime int
)
begin
	update tbJogador set IdTime = null where IdTime = _IdTime;
	delete from tbTime where IdTime = _IdTime;
end //

delimiter //
create procedure BuscarTimeId(
_IdTime int
)
begin
	select * from tbTime where
    IdTime like CONCAT('%', _IdTime ,'%');
end //

delimiter //
create procedure BuscarTimeNome(
_Nome varchar(250)
)
begin
	select * from tbTime where
    Nome like CONCAT('%', _Nome ,'%');
end //




delimiter //
create procedure BuscarJogadorId(
_IdJogador int
)
begin
	SELECT 
		J.IdJogador,
        J.NomeCompleto,
        J.NomeCamisa,
        J.Idade,
        J.NumeroCamisa,
        T.IdTime,
        T.Nome as NomeTime,
        P.IdPosicao,
        P.Nome 
	FROM 
		tbJogador as J
		LEFT JOIN
			tbTime as T on J.IdTime = T.IdTime
		LEFT JOIN
			tbPosicao as P on J.IdPosicao = P.IdPosicao
		WHERE IdJogador like CONCAT('%', _IdJogador ,'%');
end //




delimiter //
create procedure BuscarJogadorNomeCompleto(
_NomeCompleto varchar(250)
)
begin
	SELECT 
		J.IdJogador,
        J.NomeCompleto,
        J.NomeCamisa,
        J.Idade,
        J.NumeroCamisa,
        T.IdTime,
        T.Nome as NomeTime,
        P.IdPosicao,
        P.Nome 
	FROM 
		tbJogador as J
		LEFT JOIN
			tbTime as T on J.IdTime = T.IdTime
		LEFT JOIN
			tbPosicao as P on J.IdPosicao = P.IdPosicao
		WHERE NomeCompleto like CONCAT('%', _NomeCompleto ,'%');
end //



delimiter //
create procedure BuscarJogadorNomeCamisa(
_NomeCamisa varchar(12) 
)
begin
	SELECT 
		J.IdJogador,
        J.NomeCompleto,
        J.NomeCamisa,
        J.Idade,
        J.NumeroCamisa,
        T.IdTime,
        T.Nome as NomeTime,
        P.IdPosicao,
        P.Nome 
	FROM 
		tbJogador as J
		LEFT JOIN
			tbTime as T on J.IdTime = T.IdTime
		LEFT JOIN
			tbPosicao as P on J.IdPosicao = P.IdPosicao
		WHERE NomeCamisa like CONCAT('%', _NomeCamisa ,'%');
end //



delimiter //
create procedure ObterJogador(
_IdJogador int
)
begin
	SELECT 
		J.IdJogador,
        J.NomeCompleto,
        J.NomeCamisa,
        J.Idade,
        J.NumeroCamisa,
        T.IdTime,
        T.Nome as NomeTime,
        P.IdPosicao,
        P.Nome 
	FROM 
		tbJogador as J
		LEFT JOIN
			tbTime as T on J.IdTime = T.IdTime
		LEFT JOIN
			tbPosicao as P on J.IdPosicao = P.IdPosicao
		WHERE IdJogador = _IdJogador;
end //

call ObterJogador(16)

delimiter //
create procedure ObterTodosJogadores()
begin
	SELECT 
		J.IdJogador,
        J.NomeCompleto,
        J.NomeCamisa,
        J.Idade,
        J.NumeroCamisa,
        T.IdTime,
        T.Nome as NomeTime,
        P.IdPosicao,
        P.Nome 
	FROM 
		tbJogador as J
		LEFT JOIN
			tbTime as T on J.IdTime = T.IdTime
		LEFT JOIN
			tbPosicao as P on J.IdPosicao = P.IdPosicao
		ORDER BY NomeCompleto ASC;
end //



delimiter //
create procedure DeletarJogador(
_IdJogador int
)
begin
	delete from tbEstatisticasJogador where IdJogador = _IdJogador;
	delete from tbJogador where IdJogador = _IdJogador;
end //


