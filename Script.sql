USE [master]
GO

CREATE DATABASE [DBVuelos2]
GO

USE [DBVuelos2]
GO

CREATE TABLE [dbo].[Aerolinea](
	[AerolineaID] [int] IDENTITY(1,1) NOT NULL,
	[NombreComercial] [varchar](100) NULL,
	[FechaCreacion] [datetime] NULL,
	[Director] [varchar](100) NULL,
 CONSTRAINT [PK_Aerolinea] PRIMARY KEY CLUSTERED 
(
	[AerolineaID] ASC
))
GO

CREATE TABLE [dbo].[Ciudad](
	[CiudadID] [char](3) NOT NULL,
	[Nombre] [varchar](100) NULL,
 CONSTRAINT [PK_Ciudad] PRIMARY KEY CLUSTERED 
(
	[CiudadID] ASC
))
GO

CREATE TABLE [dbo].[Tripulacion](
	[AerolineaID] [int] NOT NULL,
	[TripulanteID] [int] NOT NULL,
 CONSTRAINT [PK_Tripulacion] PRIMARY KEY CLUSTERED 
(
	[AerolineaID] ASC,
	[TripulanteID] ASC
))
GO

CREATE TABLE [dbo].[Tripulante](
	[TripulanteID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Email] [varchar](50) NULL,
	[VueloID] [int] NULL,
 CONSTRAINT [PK_Tripulante] PRIMARY KEY CLUSTERED 
(
	[TripulanteID] ASC
))
GO

CREATE TABLE [dbo].[Vuelo](
	[VueloID] [int] IDENTITY(1,1) NOT NULL,
	[NumeroVuelo] [varchar](10) NULL,
	[FechaVuelo] [datetime] NULL,
	[CiudadOrigen] [char](3) NULL,
	[CiudadDestino] [char](3) NULL,
	[AerolineaID] [int] NULL,
 CONSTRAINT [PK_Vuelo] PRIMARY KEY CLUSTERED 
(
	[VueloID] ASC
))
GO

ALTER TABLE [dbo].[Tripulacion]  WITH CHECK ADD  CONSTRAINT [FK_Tripulacion_Aerolinea] FOREIGN KEY([AerolineaID])
REFERENCES [dbo].[Aerolinea] ([AerolineaID])
GO
ALTER TABLE [dbo].[Tripulacion] CHECK CONSTRAINT [FK_Tripulacion_Aerolinea]
GO
ALTER TABLE [dbo].[Tripulacion]  WITH CHECK ADD  CONSTRAINT [FK_Tripulacion_Tripulante] FOREIGN KEY([TripulanteID])
REFERENCES [dbo].[Tripulante] ([TripulanteID])
GO
ALTER TABLE [dbo].[Tripulacion] CHECK CONSTRAINT [FK_Tripulacion_Tripulante]
GO
ALTER TABLE [dbo].[Tripulante]  WITH CHECK ADD  CONSTRAINT [FK_Tripulante_Vuelo] FOREIGN KEY([VueloID])
REFERENCES [dbo].[Vuelo] ([VueloID])
GO
ALTER TABLE [dbo].[Tripulante] CHECK CONSTRAINT [FK_Tripulante_Vuelo]
GO
ALTER TABLE [dbo].[Vuelo]  WITH CHECK ADD  CONSTRAINT [FK_Vuelo_Aerolinea] FOREIGN KEY([AerolineaID])
REFERENCES [dbo].[Aerolinea] ([AerolineaID])
GO
ALTER TABLE [dbo].[Vuelo] CHECK CONSTRAINT [FK_Vuelo_Aerolinea]
GO

INSERT INTO [dbo].[Ciudad] ([CiudadID],[Nombre]) VALUES('AQP','Arequipa')
INSERT INTO [dbo].[Ciudad] ([CiudadID],[Nombre]) VALUES('CIX','Chiclayo')
INSERT INTO [dbo].[Ciudad] ([CiudadID],[Nombre]) VALUES('CUZ','Cusco')
INSERT INTO [dbo].[Ciudad] ([CiudadID],[Nombre]) VALUES('LIM','Lima')
INSERT INTO [dbo].[Ciudad] ([CiudadID],[Nombre]) VALUES('PIU','Piura')
INSERT INTO [dbo].[Ciudad] ([CiudadID],[Nombre]) VALUES('TCQ','Tacna')
INSERT INTO [dbo].[Ciudad] ([CiudadID],[Nombre]) VALUES('TPP','Tarapoto')
INSERT INTO [dbo].[Ciudad] ([CiudadID],[Nombre]) VALUES('TRU','Trujillo')
GO

INSERT INTO [dbo].[Aerolinea]([NombreComercial],[FechaCreacion],[Director]) VALUES('LAN Peru','20020502','Jose Perez')
INSERT INTO [dbo].[Aerolinea]([NombreComercial],[FechaCreacion],[Director]) VALUES('Avianca','20050607','Pepe Rioz')
INSERT INTO [dbo].[Aerolinea]([NombreComercial],[FechaCreacion],[Director]) VALUES('Iberia','19970403','Charles Vargas')
GO

INSERT INTO [dbo].[Vuelo]([NumeroVuelo],[FechaVuelo],[CiudadOrigen],[CiudadDestino],[AerolineaID]) VALUES('L102','20161004','CIX','CUZ',1)
INSERT INTO [dbo].[Vuelo]([NumeroVuelo],[FechaVuelo],[CiudadOrigen],[CiudadDestino],[AerolineaID]) VALUES('L344','20160113','PIU','CIX',3)
INSERT INTO [dbo].[Vuelo]([NumeroVuelo],[FechaVuelo],[CiudadOrigen],[CiudadDestino],[AerolineaID]) VALUES('A456','20160120','CUZ','TCQ',2)
INSERT INTO [dbo].[Vuelo]([NumeroVuelo],[FechaVuelo],[CiudadOrigen],[CiudadDestino],[AerolineaID]) VALUES('L847','20160112','CIX','PIU',1)
INSERT INTO [dbo].[Vuelo]([NumeroVuelo],[FechaVuelo],[CiudadOrigen],[CiudadDestino],[AerolineaID]) VALUES('I765','20160223','AQP','PIU',3)
GO

INSERT INTO [dbo].[Tripulante]([Nombre],[Email],[VueloID]) VALUES('Eric Gutierrez','eric@correo.com',2)
INSERT INTO [dbo].[Tripulante]([Nombre],[Email],[VueloID]) VALUES('Jose Perez','jose@cuenta.com',1)
INSERT INTO [dbo].[Tripulante]([Nombre],[Email],[VueloID]) VALUES('Luis Diaz','luis@correo.com',1)
INSERT INTO [dbo].[Tripulante]([Nombre],[Email],[VueloID]) VALUES('Luis Vargas','lvargas@mail.com',3)
INSERT INTO [dbo].[Tripulante]([Nombre],[Email],[VueloID]) VALUES('Carlos Calors','carlos@cuenta.com',4)
INSERT INTO [dbo].[Tripulante]([Nombre],[Email],[VueloID]) VALUES('Jose Jose','jjose@mail.com',1)
INSERT INTO [dbo].[Tripulante]([Nombre],[Email],[VueloID]) VALUES('Lius Perez','lperez@correo.com',3)
INSERT INTO [dbo].[Tripulante]([Nombre],[Email],[VueloID]) VALUES('Maria Maria','mmaria@cuenta.com',2)
INSERT INTO [dbo].[Tripulante]([Nombre],[Email],[VueloID]) VALUES('Juan Jose','jjose@correo.com',3)
INSERT INTO [dbo].[Tripulante]([Nombre],[Email],[VueloID]) VALUES('Oliver Jose','ojose@mail.com',3)
INSERT INTO [dbo].[Tripulante]([Nombre],[Email],[VueloID]) VALUES('Jorge Luis','jluis@cuenta.com',2)
GO

INSERT INTO [dbo].[Tripulacion]([AerolineaID],[TripulanteID]) VALUES(1,2)
INSERT INTO [dbo].[Tripulacion]([AerolineaID],[TripulanteID]) VALUES(1,4)
INSERT INTO [dbo].[Tripulacion]([AerolineaID],[TripulanteID]) VALUES(2,2)
INSERT INTO [dbo].[Tripulacion]([AerolineaID],[TripulanteID]) VALUES(2,3)
INSERT INTO [dbo].[Tripulacion]([AerolineaID],[TripulanteID]) VALUES(2,9)
INSERT INTO [dbo].[Tripulacion]([AerolineaID],[TripulanteID]) VALUES(3,5)
INSERT INTO [dbo].[Tripulacion]([AerolineaID],[TripulanteID]) VALUES(3,8)
INSERT INTO [dbo].[Tripulacion]([AerolineaID],[TripulanteID]) VALUES(3,11)
GO
