create database Dolphin;
use Dolphin;

create table Utenti
(
	id int primary key identity(1,1),
	nome varchar(100),
	cognome varchar(100),
	telefono varchar(20),
	email varchar(100),
	pass varchar(500),
	indirizzo varchar(100),
	codice_fiscale varchar(100),
	fotoProfilo varchar(1000)
);

insert into Utenti(nome, cognome, telefono, email, pass, indirizzo, codice_fiscale, fotoProfilo) 
values
('Luigi','Granchio','3914772658','LuigiGranchio00@outlet.com',HASHBYTES('SHA2_256', '1234'),'via santa lucia 15','GRNLGU90L06H501C','https://quintadimensione.it/uploads/products_image/file/11801/28072_FigurArts-LuigiA.jpg'),
('Mario', 'Delfino','3387465892','MarioDelfino99@gmail.com',HASHBYTES('SHA2_256', '5678'),'via la torre 46','DLFLGU69P13A794R','https://www.smartworld.it/wp-content/uploads/2018/02/Super-Mario-Film-1280x720.jpg');


create table Posts
(
	id int primary key identity(1,1),
	contenutoPost varchar(800),
	data_ora datetime,
	miPiace int,
	idUtente int,
	foreign key (idUtente) references Utenti(id)
	on update cascade
	on delete cascade
);

create table Commenti
(
	id int primary key identity(1,1),
	idPost int,
	idUtente int,
	contenutoCommento varchar(800),
	data_ora datetime,
	miPiace int,
	foreign key (idUtente) references Utenti(id)
	on update no action
	on delete no action,
	foreign key (idPost) references Posts(id)
	on update no action
	on delete no action
);
/*
create table Bacheche
(
	id int primary key identity(1,1),
	idPost int,
	idAmicizia int,
	foreign key (idAmicizia) references Amicizie(id)
	on update cascade
	on delete cascade,
	foreign key (idPost) references Posts(id)
	on update cascade
	on delete cascade
);
*/
create table Amicizie
(
	id int primary key identity(1,1),
	idUtente int,
	foreign key (idUtente) references Utenti(id)
	on update no action
	on delete no action,
	idUtente2 int,
	foreign key (idUtente2) references Utenti(id)
	on update no action
	on delete no action
);