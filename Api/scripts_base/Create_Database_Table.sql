create database MegaHack
go
use MegaHack
go
create table Pessoa(
	ID_Pessoa Integer Primary Key Identity(1,1),
	Nome Varchar(100) Not Null,
	Documento Varchar(14) Not Null,
	Email Varchar(30) Not Null,
	Senha Varchar(10) Not Null
)
go
create table Endereco(
	ID_Endereco Integer Primary Key Identity(1,1),
	Logradouro Varchar(100) Not Null,
	Numero Varchar(10) Not Null,
	Bairro Varchar(30) Not Null,
	Cep Varchar(10) Not Null,
	Cidade Varchar(50) Not Null,
	Estado Char(2)
)
go
create table Contato(
	ID_Contato Integer Primary Key Identity(1,1),
	DDD Char(2) Not Null,
	Telefone Varchar(10) Not Null,
)
go
create table Comerciante(
	ID_Comerciante Integer Primary Key Identity(1,1),
	ID_Pessoa Integer,
	ID_Endereco Integer,
	ID_Contato Integer,
	Nome_Fantasia Varchar(100),
	Imagem Varchar(MAX),
	foreign key(ID_Pessoa) references Pessoa(ID_Pessoa),
	foreign key(ID_Endereco) references Endereco(ID_Endereco),
	foreign key(ID_Contato) references Contato(ID_Contato)
)
go
create table Cliente(
	ID_Cliente Integer Primary Key Identity(1,1),
	ID_Pessoa Integer,
	ID_Endereco Integer,
	ID_Contato Integer,
	Imagem Varchar(MAX),
	foreign key(ID_Pessoa) references Pessoa(ID_Pessoa),
	foreign key(ID_Endereco) references Endereco(ID_Endereco),
	foreign key(ID_Contato) references Contato(ID_Contato)
)
go
create table Entregador(
	ID_Entregador Integer Primary Key Identity(1,1),
	ID_Pessoa Integer,
	ID_Endereco Integer,
	ID_Contato Integer,
	Imagem Varchar(MAX)
	foreign key(ID_Pessoa) references Pessoa(ID_Pessoa),
	foreign key(ID_Endereco) references Endereco(ID_Endereco),
	foreign key(ID_Contato) references Contato(ID_Contato)
)
go
create table Produto(
	ID_Produto Integer Primary Key Identity(1,1),
	ID_Comerciante Integer,
	Descricao Varchar(255),
	Quantidade Numeric(10,4),
	Preco Numeric(18,2),
	Imagem Varchar(MAX)
	foreign key(ID_Comerciante) references Comerciante(ID_Comerciante)
)
go
create table Processo(
	ID_Processo Integer Primary Key Identity(1,1),
	No_Comprovante Varchar(14) Not Null,
	Data_Inicio Datetime Not Null,
	Data_Atualizacao Datetime Null,
	Data_Finalizacao Datetime null,
	Status Varchar(20) Not Null,
	ID_Comerciante Integer Not Null,
	ID_Cliente Integer Not Null,
	ID_Produto Integer Not Null,
	ID_Entregador Integer Null
	foreign key(ID_Comerciante) references Comerciante(ID_Comerciante),
	foreign key(ID_Cliente) references Cliente(ID_Cliente),
	foreign key(ID_Produto) references Produto(ID_Produto),
	foreign key(ID_Entregador) references Entregador(ID_Entregador)
)