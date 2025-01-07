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

insert into tbTime(Nome, Abreviacao, Img) values
('Corinthians','COR','img/coringao'),
('Jogos Mortais','JDM','img/jdmortais'),
('Danados Futebol Clube','DAN','img/danados');

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

insert into tbJogador(NomeCompleto, NomeCamisa, Idade, NumeroCamisa, IdTime, IdPosicao) values
('Yuri Alberto', 'Yuri', '23', '9', '1', '18'),
('Memphis Depay', 'Memphis', '30', '94', '1', '16'),
('Hugo Souza', 'Hugo', '25', '1', '1', '1'),
('Rodrigo Garro', 'Garro', '26', '10', '1', '12'),
('Carlos Alarcon', 'CRZ', '17', '10', '2', '9'),
('João Lucas', 'JL', '18', '1', '2', '1'),
('Cauã Silva', 'Miles', '18', '11', '2', '8'),
('Ruan Pablo', 'Tanque', '18', '5', '2', '4'),
('Denis Lemos', 'Denis', '18', '18', '2', '17'),
('Leonardo Macêdo', 'Leo', '18', '9', '2', '17');

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
		INNER JOIN
			tbTime as T on J.IdTime = T.IdTime
		INNER JOIN
			tbPosicao as P on J.IdPosicao = P.IdPosicao
		WHERE IdJogador = _IdJogador;
end //

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



call BuscarJogadorNomeCompleto("Z");
call BuscarJogadorId(9);
call BuscarJogadorNomeCamisa("Z");

