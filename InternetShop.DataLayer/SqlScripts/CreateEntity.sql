use master
if not exists( select * from sys.databases where name = 'InternetShop' )
  create database InternetShop
go

use InternetShop

-- Acount information tables
if exists ( select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'UsersRoles' )
  drop table UsersRoles
if exists ( select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Users' )
  drop table Users
if exists ( select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Roles' )
  drop table Roles

create table Users
(
  UserId nvarchar( 128 ) not null,
  Email nvarchar( 256 ) not null,
  PasswordHash nvarchar( MAX ) not null,
  Name nvarchar( 124 ) not null,
  Surname nvarchar( 124 ) not null,
  Address nvarchar( 256 ) not null,

  constraint PK_Users primary key clustered ( UserId ASC )
)

create table Roles
(
  RoleId int not null,
  Name nvarchar( 100 ) not null,

  constraint PK_Roles primary key clustered ( RoleId ASC )
)

create table UsersRoles
(
  UserId nvarchar( 128 ) not null,
  RoleId int not null,

  constraint PK_UsersRoles primary key clustered ( UserId ASC, RoleId ASC ),
  constraint FK_Users_UsersRoles foreign key ( UserId )
    references Users ( UserId ),
  constraint FK_Roles_UsersRoles foreign key ( RoleId )
    references Roles ( RoleId )
)
go

create unique nonclustered index UserEmailIndex
  on Users ( Email ASC )


-- Products tables
if exists ( select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Products' )
  drop table Products


create table Products
(
  ProductId int identity not null,
  Name nvarchar( 50 ) not null,
  Description varchar( MAX ) null,
  Year int null,
  Price decimal not null,
  Perfomer nvarchar( 50 ) null,
  MusicalDirection nvarchar( 50 ) null,
  Director nvarchar( 50 ) null,
  Genre nvarchar( 50 ) null,

  constraint PK_Products primary key clustered ( ProductId ASC )
)
go
create unique nonclustered index ProductNameIndex
  on dbo.Products( Name ASC )

-- TODO Genres and Musical Directions tables

-- Orders tables
if exists( select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'OrderedProducts' )
  drop table OrderedProducts
if exists( select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Orders' )
  drop table Orders

create table Orders
(
  OrderId int identity not null,
  Date datetime not null,
  UserId nvarchar( 128 ) not null,

  constraint PK_Orders primary key clustered ( OrderId ASC ),
  constraint FK_Users_Orders foreign key ( UserId )
   references Users ( UserId )
)

create table OrderedProducts
(
  OrderId int not null,
  ProductId int not null,
  Amount int not null default( 1 ),

  constraint PK_OrderedProducts primary key clustered ( OrderId ASC, ProductId ASC ),
  constraint FK_Orders_OrderedProducts foreign key ( OrderId )
    references Orders ( OrderId ),
  constraint FK_Products_OrderedProducts foreign key ( ProductId )
    references Products ( ProductId )
)