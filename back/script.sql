
use master
go
if exists(select * from sys.databases where name = 'tcc_site')
drop database tcc_site
go
create database tcc_site
go
use tcc_site
go

create table Usuario(
	ID int identity primary key,
	nome varchar(100),
	email varchar(MAX),
	senha varchar(MAX),
	confsenha varchar(MAX),
	matricula varchar(10),
	telefone varchar(20),
	endereco varchar(MAX),
	UF varchar(10),
	estado varchar(30),
	DataNascimento date,
	Setor varchar(25),
	cidade varchar(30),
	empresa varchar(50)
);
create table Token(
	ID int identity primary key,
	UserId int references Usuario(ID),
	valor varchar(MAX) not null
);

create table PostAtividade(
	ID int identity primary key,
	UserID int references Usuario(ID),
	Momento datetime not null,
	Contexto varchar(MAX) not null
);
create table Cargos(
	ID int identity primary key,
	UserID int references Usuario(ID),
	Cargo varchar(50)
);