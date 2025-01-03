create database dbSports;
use dbSports;

create table tbTime(
IdTime int primary key auto_increment,
Nome varchar(250) not null,
Abreviacao char(3) not null unique,
Img varchar(100) not null 
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
