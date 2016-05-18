use produ

IF OBJECT_ID('DBO.MOB_INSERT_SERIE_PICKING','P') IS NOT NULL
	DROP PROCEDURE DBO.MOB_INSERT_SERIE_PICKING
	GO


Create Procedure dbo.MOB_INSERT_SERIE_PICKING
	@Picking_id NUMERIC(20,0) OUTPUT,
	@Nro_Serie VARCHAR(100) OUTPUT
AS
BEGIN

INSERT INTO [dbo].[SeriePicking]
           ([Picking_id]
           ,[Nro_Serie]
           ,[Fecha])
     VALUES
           (@Picking_id
           ,@Nro_Serie
           ,GETDATE())

END