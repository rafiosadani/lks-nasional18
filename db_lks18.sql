use db_lks18

create table menu (menuid int IDENTITY(1,1) PRIMARY KEY, name varchar(50) NOT NULL, price int NOT NULL, photo varchar(100))

create table  msmember (memberid nchar(8) PRIMARY KEY,  name varchar(50) NOT NULL, email varchar(50), handphone varchar(13), join_date date)

create table msemployee (employeeid nchar(6) PRIMARY KEY, name varchar(100) NOT NULL, email varchar(50) NOT NULL, password varchar(50) NOT NULL, handphone varchar(13) NOT NULL, position varchar(50) NOT NULL)

create table headerorder (orderid nchar(10) PRIMARY KEY NOT NULL, employeeid nchar(6) NOT NULL, memberid nchar(8) NOT NULL, date date, payment varchar(50) NOT NULL, bank varchar(50))

alter table headerorder add foreign key(memberid) references msmember(memberid)

alter table headerorder add foreign key(employeeid) references msemployee(employeeid)

create table detailorder (detailid int IDENTITY(1,1) PRIMARY KEY, orderid nchar(10) NOT NULL, menuid int NOT NULL, qty int, price int, status varchar(10))

alter table detailorder add foreign key(orderid) references headerorder(orderid)

alter table detailorder add foreign key(menuid) references menu(menuid)
