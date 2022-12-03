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
	nome varchar(100) not null,
	email varchar(MAX) not null,
	senha varchar(MAX) not null,
	confsenha varchar(MAX) not null,
	matricula varchar(10) not null,
	telefone varchar(20),
	endereco varchar(MAX),
	UF varchar(10),
	estado varchar(30),
	DataNascimento date,
	Setor varchar,
);
create table Token(
	ID int identity primary key,
	UserId int references Usuario(ID),
	valor varchar(MAX) not null
);

create table PostAtividade(
	ID int identity primary key,
	UserID int references Usuario(ID),
	Moment datetime not null,
	Content varchar(MAX) not null

);
	
create table cargos(
		ID int identity primary key,
		CargoID int references Usuario(ID),
		CargoNome varchar(20)

);
