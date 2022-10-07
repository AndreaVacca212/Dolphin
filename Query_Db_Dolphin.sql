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
	fotoProfilo varchar(1000),
	copertina varchar(2000)
);

insert into Utenti(nome, cognome, telefono, email, pass, indirizzo, codice_fiscale, fotoProfilo) 
values
('Luigi','Granchio','3914772658','LuigiGranchio00@outlet.com',HASHBYTES('SHA2_256', '1234'),'via santa lucia 15','GRNLGU90L06H501C','https://quintadimensione.it/uploads/products_image/file/11801/28072_FigurArts-LuigiA.jpg'),
('Mario', 'Delfino','3387465892','MarioDelfino99@gmail.com',HASHBYTES('SHA2_256', '5678'),'via la torre 46','DLFLGU69P13A794R','https://www.smartworld.it/wp-content/uploads/2018/02/Super-Mario-Film-1280x720.jpg');

SELECT * FROM Utenti WHERE email = 'LuigiGranchio00@outlet.com' AND pass = HASHBYTES('SHA2_256', '1234');


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

select * from Utenti
SELECT Utenti.id, Utenti.nome, Utenti.cognome,Posts.contenutoPost,Posts.data_ora, Posts.id As idPost, Posts.miPiace, Amicizie.id 
FROM Utenti 
INNER JOIN Posts ON Posts.idUtente = Utenti.id 
INNER JOIN Amicizie ON Amicizie.idUtente2 = Utenti.id;

SELECT *
FROM Utenti 
INNER JOIN Posts ON Posts.idUtente = Utenti.id
INNER JOIN Commenti ON Commenti.idUtente = Utenti.id
INNER JOIN Amicizie ON Amicizie.idUtente = Utenti.id

SELECT Utenti2.nome, Utenti2.cognome, Utenti2.fotoProfilo
FROM Utenti
INNER JOIN Amicizie ON Utenti.id = Amicizie.idUtente
INNER JOIN Utenti AS Utenti2 ON Amicizie.idUtente2 = Utenti2.id
where utenti.id=1 or Amicizie.idUtente = 1

/*SELECT * FROM
Amicizie INNER JOIN Utenti on Utenti.id = Amicizie.idUtente
INNER JOIN Amicizie AS Amicizie2 on Amicizie2.idUtente2 = Utenti.id
where Utenti.id = 1 or Amicizie2.idUtente = 1;*/

delete from Posts
where id=6;

select * from Amicizie

insert into Amicizie
(idUtente,idUtente2)
VALUES
(2,1);

ALTER TABLE Utenti
ADD copertina varchar(2000);
