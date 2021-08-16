create database TutorpalveluDB;

create table Person (
	PersonID int PRIMARY KEY IDENTITY(1,1) not null,
    Etunimi varchar(25) not null,
    Sukunimi varchar(50) not null,
    Osoite varchar(60) null,
    Postitoimipaikka varchar(50) null,
	Postinumero varchar(5) null,
	PuhNro varchar(20) null,
	Email varchar(80) not null,
	Tutor BIT not null,
	
);

create table Palvelu (
	PalveluID int PRIMARY KEY IDENTITY(1,1) not null,
    Nimi varchar(50) not null,
	Tyyppi varchar(50) null,
    Ryhmä varchar(50) null,
    Hinta decimal not null,
	Pvm datetime not null,
	Kesto int not null,
	Sijainti varchar(100) not null,
	Varattu BIT not null,
	TutorID int FOREIGN KEY REFERENCES Person(PersonID)

);

