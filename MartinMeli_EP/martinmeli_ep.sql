/*
	Enterprise Programming
	Assignment 1
	Martin Meli
*/

-- destruction

use martinmeli_ep
go

drop table meliNews.article;
go

drop table meliNews.category;
go

drop table meliNews.journalist;
go

drop schema meliNews;
go

use master;
go

drop database martinmeli_ep;
go

-- creation

create database martinmeli_ep;
go

use martinmeli_ep;
go

create schema meliNews;
go

create table meliNews.journalist(
	id int primary key identity(1,1)
	, name nvarchar(64) not null
	, surname nvarchar(64) not null
	, about nvarchar(512) not null
	, email nvarchar(256) not null
	, username nvarchar(32) not null
	, [password] nvarchar(64) not null
);
go

create table meliNews.category(
	id int primary key
	, category nvarchar(16) not null
);
go

create table meliNews.article(
	articleId int primary key identity(1,1)
	, headline nvarchar(256) not null
	, subheading nvarchar(256) not null
	, [description] nvarchar(256) not null
	, imageURL nvarchar(1024) not null
	, authorId int not null foreign key references meliNews.journalist(id)
	, categoryId int not null foreign key references meliNews.category(id)
);
go

-- populate

insert into meliNews.category values
	(1, 'National')
	, (2, 'Overseas')
	, (3, 'Sports')
	, (4, 'Odd')
	, (5, 'Travel')
	, (6, 'Opinion');
go