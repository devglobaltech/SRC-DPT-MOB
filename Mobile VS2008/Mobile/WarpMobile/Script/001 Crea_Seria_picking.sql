use produ
go

IF OBJECT_ID('Dbo.SeriePicking','U') IS NOT NULL
	DROP TABLE Dbo.SeriePicking
	GO

Create Table Dbo.SeriePicking(
SeriePicking_Id numeric(20,0) Identity not null,
PICKING_ID numeric(20,0) NOT NULL,
Nro_Serie varchar(100),
Fecha datetime,
constraint [PK_SeriePicking] PRIMARY KEY CLUSTERED  (SeriePicking_Id asc))
GO

ALTER TABLE dbo.SeriePicking  WITH CHECK ADD CONSTRAINT FK_SeriePicking_PICKING FOREIGN KEY(PICKING_ID)
REFERENCES dbo.PICKING (PICKING_ID)
GO
ALTER TABLE dbo.SeriePicking CHECK CONSTRAINT FK_SeriePicking_PICKING
GO