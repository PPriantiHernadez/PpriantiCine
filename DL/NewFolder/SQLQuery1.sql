CREATE DATABASE PPriantiCine
GO

USE PPriantiCine
GO

CREATE TABLE Zona
(
IdZona INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50)
)
GO

INSERT INTO Zona VALUES ('Norte'),('Sur'),('Este'),('Oeste')

Select * from Zona

CREATE TABLE Cine
(
IdCine INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50),
Direccion VARCHAR(MAX),
IdZona INT,
Ventas INT,
FOREIGN KEY (IdZona) REFERENCES Zona(IdZona)
)


CREATE TABLE Usuario
(
IdUsuario INT PRIMARY KEY IDENTITY(1,1),
UserName VARCHAR (50) NOT NULL UNIQUE,
Nombre VARCHAR(50) NOT NULL,
ApellidoPaterno VARCHAR(50) NOT NULL,
ApellidoMaterno VARCHAR(50) NULL,
Correo VARCHAR(50) NOT NULL UNIQUE,
Contraseña VARBINARY(20) NOT NULL
)
GO

ALTER TABLE Usuario ALTER COLUMN Contraseña VARBINARY(20)  
GO 

DROP TABLE Usuario

ALTER TABLE cine
ADD Latitud FLOAT NULL;

ALTER TABLE cine
ADD Longitud FLOAT NULL;

Select * From cine

ALTER TABLE cine ALTER COLUMN Latitud FLOAT   
GO 

ALTER TABLE cine ALTER COLUMN Longitud FLOAT   
GO 

INSERT INTO Usuario VALUES('Pprianti','Priscila','Prianti','Hernandez','ppriantihernandez@gmail.com','Password1')
--------------------------------------------------- ADD
CREATE PROCEDURE [dbo].[CineAdd]
@Nombre varchar(50)
           ,@Direccion varchar(max)
           ,@IdZona int
           ,@Ventas int
		   ,@Latitud float
           ,@Longitud float
AS
INSERT INTO [dbo].[Cine]
           ([Nombre]
           ,[Direccion]
           ,[IdZona]
           ,[Ventas]
		   ,[Latitud]
           ,[Longitud])
     VALUES
           (@Nombre
           ,@Direccion
           ,@IdZona
           ,@Ventas
		   ,@Latitud,
		   @Longitud)
GO

--------------------------------------------------- UPDATE
CREATE PROCEDURE [dbo].[CineUpdate]
@IdCine int,
		   @Nombre varchar(50),
           @Direccion varchar(max),
           @IdZona int,
           @Ventas int,
		   @Latitud float,
           @Longitud float
AS
UPDATE [dbo].[Cine]
   SET [Nombre] = @Nombre
      ,[Direccion] = @Direccion
      ,[IdZona] = @IdZona
      ,[Ventas] = @Ventas
	  ,[Latitud] = @Latitud
	  ,[Longitud] = @Longitud
 WHERE IdCine = @IdCine
GO
--------------------------------------------------- DELETE
CREATE PROCEDURE CineDelete 12
@IdCine int
AS
DELETE FROM [dbo].[Cine]
      WHERE IdCine = @IdCine
GO
--------------------------------------------------- GETALL
CREATE ALTER PROCEDURE [dbo].[CineGetAll]
AS
SELECT Cine.[IdCine]
      ,Cine.[Nombre]
      ,Cine.[Direccion]
      ,Zona.[IdZona]
	  ,Zona.Nombre AS 'ZonaNombre'
      ,Cine.[Ventas]
	  ,Cine.[Latitud]
      ,Cine.[Longitud]
  FROM [dbo].[Cine]
INNER JOIN Zona ON Zona.IdZona = Cine.IdZona
GO
--------------------------------------------------- GETBYID
CREATE PROCEDURE [dbo].[CineGetById]
@IdCine int
AS
SELECT Cine.[IdCine]
      ,Cine.[Nombre]
      ,Cine.[Direccion]
      ,Zona.[IdZona]
	  ,Zona.Nombre AS 'ZonaNombre'
      ,Cine.[Ventas]
	  ,Cine.[Latitud]
      ,Cine.[Longitud]
  FROM [dbo].[Cine]
INNER JOIN Zona ON Zona.IdZona = Cine.IdZona
WHERE IdCine = @IdCine
GO
--------------------------------------------GetAllZona
CREATE PROCEDURE ZonaGetAll
AS
SELECT [IdZona]
      ,[Nombre]
  FROM [dbo].[Zona]
GO
--------------------------------------------AddUsuario
ALTER PROCEDURE UsuarioAdd 'Pprianti','Priscila','Prianti','Hernandez','ppriantihernandez@gmail.com',12345
@UserName varchar(50)
           ,@Nombre varchar(50)
           ,@ApellidoPaterno varchar(50)
           ,@ApellidoMaterno varchar(50)
           ,@Correo varchar(50)
           ,@Contraseña varbinary(20)
AS
INSERT INTO [dbo].[Usuario]
           ([UserName]
           ,[Nombre]
           ,[ApellidoPaterno]
           ,[ApellidoMaterno]
           ,[Correo]
           ,[Contraseña])
     VALUES
           (@UserName
           ,@Nombre
           ,@ApellidoPaterno
           ,@ApellidoMaterno
           ,@Correo
           ,@Contraseña)
GO
---------------------------------------------------UsuarioGetById
CREATE PROCEDURE UsuarioGetById
@IdUsuario int
AS
SELECT [IdUsuario]
      ,[UserName]
      ,[Nombre]
      ,[ApellidoPaterno]
      ,[ApellidoMaterno]
      ,[Correo]
      ,[Contraseña]
  FROM [dbo].[Usuario]
WHERE IdUsuario = @IdUsuario
GO

SELECT * FROM Usuario
----------------------------------------------------Login
ALTER PROCEDURE [dbo].[UsuarioGetByIdUserName] 
@UserName varchar(50)
AS
SELECT [IdUsuario]
      ,[UserName]
      ,[Nombre]
      ,[ApellidoPaterno]
      ,[ApellidoMaterno]
      ,[Correo]
      ,[Contraseña]
  FROM [dbo].[Usuario]
WHERE Usuario.UserName = @UserName

-------------------------------------------------

CREATE PROCEDURE [dbo].[GetByEmail] 
@Correo varchar(50)
AS
SELECT [IdUsuario]
      ,[UserName]
      ,[Nombre]
      ,[ApellidoPaterno]
      ,[ApellidoMaterno]
      ,[Correo]
      ,[Contraseña]
  FROM [dbo].[Usuario]
WHERE Usuario.Correo = @Correo

---https://codepen.io/Rh2o/pen/yLgxJoG