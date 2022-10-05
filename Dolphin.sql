create database Dolphin;
use Dolphin;

create table Utente
(
	id int primary key identity(1,1),
	nome varchar(100),
	cognome varchar(100),
	telefono varchar(20),
	email varchar(100),
	indirizzo varchar(100),
	codice_fiscale varchar(100),
	fotoProfilo varchar(1000)
);

insert into Utente(nome, cognome, telefono, email, indirizzo, codice_fiscale, fotoProfilo) values
('Luigi','Granchio','3914772658','LuigiGranchio00@outlet.com','via santa lucia 15','GRNLGU90L06H501C','https://quintadimensione.it/uploads/products_image/file/11801/28072_FigurArts-LuigiA.jpg'),
('Mario', 'Delfino','3387465892','MarioDelfino99@gmail.com','via la torre 46','DLFLGU69P13A794R','https://www.smartworld.it/wp-content/uploads/2018/02/Super-Mario-Film-1280x720.jpg');

create table Post
(
	id int primary key identity(1,1),
	post varchar(800),
	data_ora datetime,
	miPiace int,
	idUtente int,
	foreign key (idUtente) references Utente(id)
	on update cascade
	on delete cascade
);

create table Commento
(
	id int primary key identity(1,1),
	idPost int,
	idUtente int,
	commento varchar(800),
	data_ora datetime,
	miPiace int,
	foreign key (idUtente) references Utente(id)
	on update no action
	on delete no action,
	foreign key (idPost) references Post(id)
	on update no action
	on delete no action
);
/*
create table Bacheca
(
	id int primary key identity(1,1),
	idPost int,
	idAmicizia int,
	foreign key (idAmicizia) references Amicizia(id)
	on update cascade
	on delete cascade,
	foreign key (idPost) references Post(id)
	on update cascade
	on delete cascade
);
*/
create table Amicizia
(
	id int primary key identity(1,1),
	idUtente int,
	foreign key (idUtente) references Utente(id)
	on update no action
	on delete no action,
	idUtente2 int,
	foreign key (idUtente2) references Utente(id)
	on update no action
	on delete no action
);