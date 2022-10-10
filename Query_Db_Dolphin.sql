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
	idUtente int,
	foreign key (idUtente) references Utenti(id)
	on update cascade
	on delete cascade
);

select * from posts


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


create table MiPiacePosts
(
	id int primary key identity(1,1),
	idPost INT FOREIGN KEY REFERENCES Posts(id)
	ON DELETE NO ACTION
	ON UPDATE NO ACTION,
	idUtente INT FOREIGN KEY REFERENCES Utenti(id)
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
);



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

select * from Posts
where id=6;

select * from utentiView

insert into Amicizie
(idUtente,idUtente2)
VALUES
(2,1);

ALTER TABLE Utenti
ADD copertina varchar(2000);
GO

create view UtentiView as
SELECT Utenti.id, Utenti.nome, Utenti.cognome, Utenti.fotoprofilo 
FROM Utenti
go

select * from Posts

SELECT Utenti2.id, Utenti2.nome, Utenti2.cognome, Utenti2.fotoProfilo, Posts.contenutoPost
FROM Utenti 
INNER JOIN Amicizie ON Utenti.id = Amicizie.idUtente 
INNER JOIN Utenti AS Utenti2 ON Amicizie.idUtente2 = Utenti2.id 
LEFT JOIN Posts on Posts.idUtente = Utenti.id 
WHERE utenti.id=1 OR Amicizie.idUtente = 1;
go

create view Amici as
select Utenti1.id as idUtente1 ,Utenti2.id as idUtente2
from Utenti as Utenti1
inner join Amicizie on Utenti1.id=Amicizie.id
inner join Utenti as Utenti2 on Utenti2.id=Amicizie.idUtente2

select distinct * from amici
where idAmicoLato1 = 1 and idUtente != idAmicoLato1

go
select distinct Utenti.nome, Utenti.cognome, utenti.fotoProfilo, Utenti.id,Amici.idUtente1,Amici.idUtente2,Posts.contenutoPost,Posts.idUtente
from Posts
full join Amici on amici.idUtente1=Posts.idUtente
inner join Utenti on Utenti.id=Posts.idUtente
Full JOIN Amici as Amici2 on Amici2.idUtente2 = Posts.idUtente
where Amici.idUtente1=3 OR (Amici.idUtente2=3 and Posts.idUtente!=3)



select distinct Utenti.nome, Utenti.cognome, utenti.fotoProfilo,Posts.contenutoPost,Posts.data_ora
from Posts
full join Amici on amici.idUtente1=Posts.idUtente
inner join Utenti on Utenti.id=Posts.idUtente
Full JOIN Amici as Amici2 on Amici2.idUtente2 = Posts.idUtente
where Amici.idUtente1=3 OR (Amici.idUtente2=3 and Posts.idUtente!=3)

go
create view Amici
as
select utenti.nome as nomeUtente ,utenti.cognome as cognomeUtente,utenti.id as idUtente,utenti.fotoProfilo as fotoUtente, Amicizie.idUtente as idAmicoLato1,Amicizie.idUtente2 as idAmicoLato2
from Amicizie inner join Utenti on Amicizie.idUtente=Utenti.id or Amicizie.idUtente2 = Utenti.id

go
select distinct Amici.nomeUtente, amici.cognomeUtente, Posts.contenutopost, Posts.data_ora, amici.fotoutente, posts.id as idPost
from Posts
left join MiPiacePosts on posts.id = MiPiacePosts.idPost
inner join Amici on Posts.idUtente=Amici.idAmicoLato1 or Posts.idUtente = Amici.idAmicoLato2
where (Amici.idAmicoLato1=3 or Amici.idAmicoLato2=3) and Posts.idUtente != 3 and (Amici.idUtente != 3);

select * from amici

UPDATE Posts 
SET miPiace=miPiace+1
WHERE id=[metti l'id]

select * 
from Posts
left join MiPiacePosts on Posts.id = MiPiacePosts.idPost
left join utenti on utenti.id = Posts.idUtente


select idPost, MiPiacePosts.idUtente, Utenti.nome, Utenti.cognome
from MiPiacePosts
inner join Utenti on utenti.id = MiPiacePosts.idUtente
where idPost = 8

ALTER TABLE MiPiacePost
 TO MiPiacePosts;

insert into MiPiacePosts
(idPost,idUtente)
VALUES
(3,1),
(8,2),
(8,4),
(16,1),
(9,4),
(15,3),
(1002,4)

select * from MiPiacePosts
where idPost = 8
