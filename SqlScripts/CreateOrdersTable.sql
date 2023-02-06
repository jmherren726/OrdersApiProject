USE Main;

CREATE TABLE Orders (
	Id UNIQUEIDENTIFIER Primary Key NOT NULL,
	OrderType varchar(50) NOT NULL,
	CustomerName varchar(50) NOT NULL,
	CreatedDate DateTime NOT NULL,
	CreatedByUsername varchar(50) NOT NULL
	
);