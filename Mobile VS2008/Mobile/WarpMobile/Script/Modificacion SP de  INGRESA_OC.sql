
ALTER PROCEDURE [dbo].[INGRESA_OC]
@CLIENTE_ID		VARCHAR(15),
@OC				VARCHAR(100),
@Remito			varchar(30),
@DOCUMENTO_ID	BIGINT OUTPUT

AS
BEGIN
	SET XACT_ABORT ON
	SET NOCOUNT ON

	DECLARE @DOC_ID				NUMERIC(20,0)
	DECLARE @DOC_TRANS_ID		NUMERIC(20,0)
	DECLARE @DOC_EXT			VARCHAR(100)
	DECLARE @SUCURSAL_ORIGEN	VARCHAR(20)
	DECLARE @CAT_LOG_ID			VARCHAR(50)
	DECLARE @DESCRIPCION		VARCHAR(30)
	DECLARE @UNIDAD_ID			VARCHAR(15)
	DECLARE @NRO_PARTIDA		NUMERIC(38)
	DECLARE @LOTE_AT			VARCHAR(50)
	DECLARE @Preing				VARCHAR(45)
	DECLARE @CatLogId			Varchar(50)
	DECLARE @LineBO				Float
	DECLARE @qtyBO				Float
	DECLARE @ToleranciaMax		Float
	DECLARE @QtyIngresada		Float
	DECLARE @tmax				Float
	DECLARE @MAXP				VARCHAR(50)
	DECLARE @NROLINEA			INTEGER
	DECLARE @cantidad			numeric(20,5)
	DECLARE @fecha				datetime	
	DECLARE @PRODUCTO_ID		VARCHAR(30)
	DECLARE @PALLET_AUTOMATICO	VARCHAR(1)
	declare @lote				VARCHAR(1)
	DECLARE @NRO_PALLET			VARCHAR(100)
		
	-----------------------------------------------------------------------------------------------------------------
	--obtengo los valores de las secuencias.
	-----------------------------------------------------------------------------------------------------------------	
	--obtengo la secuencia para el numero de partida.
	--	exec get_value_for_sequence  'NRO_PARTIDA', @nro_partida Output
	
		SELECT 	TOP 1
				@DOC_EXT=SD.DOC_EXT,@SUCURSAL_ORIGEN=AGENTE_ID
		FROM 	SYS_INT_DOCUMENTO SD INNER JOIN SYS_INT_DET_DOCUMENTO SDD  ON(SD.CLIENTE_ID=SDD.CLIENTE_ID AND SD.DOC_EXT=SDD.DOC_EXT)
		WHERE 	ORDEN_DE_COMPRA=@OC
				--AND PRODUCTO_ID=@PRODUCTO_ID
				AND SD.CLIENTE_ID=@CLIENTE_ID
				and SDD.fecha_estado_gt is null
				and SDD.estado_gt is null
					
	-----------------------------------------------------------------------------------------------------------------
	--Comienzo con la carga de las tablas.
	-----------------------------------------------------------------------------------------------------------------
	Begin transaction	
	--Creo Documento
	Insert into Documento (	Cliente_id	, Tipo_comprobante_id	, tipo_operacion_id	, det_tipo_operacion_id	, sucursal_origen		, fecha_cpte	, fecha_pedida_ent	, Status	, anulado	, nro_remito	,orden_de_compra, nro_despacho_importacion	,GRUPO_PICKING		, fecha_alta_gtw)
					Values(	@Cliente_Id	, 'DO'					, 'ING'				, 'MAN'					,@SUCURSAL_ORIGEN		, GETDATE()		, GETDATE()			,'D05'		,'0'		, @Remito		,@oc			,@DOC_EXT					,null			, getdate())		
	--Obtengo el Documento Id recien creado.	
	Set @Doc_ID= Scope_identity()
	
	declare Ingreso_Cursor CURSOR FOR
	select producto_id, cantidad, fecha from ingreso_oc	WHERE (CLIENTE_ID = @CLIENTE_ID) AND (ORDEN_COMPRA = @oc) AND (PROCESADO = 0)

	set @Nrolinea=0
	open Ingreso_Cursor
	fetch next from Ingreso_Cursor INTO @producto_id, @cantidad, @fecha
	
	WHILE @@FETCH_STATUS = 0
	BEGIN	
		exec get_value_for_sequence  'NRO_PARTIDA', @nro_partida Output
		SET @PALLET_AUTOMATICO=NULL
		set @lote=null
		set @Nrolinea= @Nrolinea + 1
		
		SELECT 	TOP 1
				@DOC_EXT=SD.DOC_EXT,@SUCURSAL_ORIGEN=AGENTE_ID
		FROM 	SYS_INT_DOCUMENTO SD INNER JOIN SYS_INT_DET_DOCUMENTO SDD  ON(SD.CLIENTE_ID=SDD.CLIENTE_ID AND SD.DOC_EXT=SDD.DOC_EXT)
		WHERE 	ORDEN_DE_COMPRA=@OC
				AND PRODUCTO_ID=@PRODUCTO_ID
				AND SD.CLIENTE_ID=@CLIENTE_ID
				and SDD.fecha_estado_gt is null
				and SDD.estado_gt is null

		if @doc_ext is null
		begin
			raiserror('El producto %s no se encuentra en la orden de compra %s',16,1,@producto_id, @oc)
			return
		end
		SELECT @ToleranciaMax=isnull(TOLERANCIA_MAX,0) from producto where cliente_id=@cliente_id and producto_id=@producto_id

		-----------------------------------------------------------------------------------------------------------------
		--tengo que controlar el maximo en cuanto a tolerancias.
		-----------------------------------------------------------------------------------------------------------------
		Select 	@qtyBO=sum(cantidad_solicitada)
		from	sys_int_det_documento
		where	doc_ext=@doc_ext
				and fecha_estado_gt is null
				and estado_gt is null

		set @tmax= @qtyBO + ((@qtyBO * @ToleranciaMax)/100)
		
		if @cantidad > @tmax
		begin
			Set @maxp=ROUND(@tmax,0)
			raiserror('1- La cantidad recepcionada supera a la tolerancia maxima permitida.  Maximo permitido: %s ',16,1, @maxp)
			return
		end
		-----------------------------------------------------------------------------------------------------------------
		--Obtengo las categorias logicas antes de la transaccion para acortar el lockeo.
		-----------------------------------------------------------------------------------------------------------------
		SELECT 	@CAT_LOG_ID=PC.CAT_LOG_ID
		FROM 	RL_PRODUCTO_CATLOG PC 
		WHERE 	PC.CLIENTE_ID=@CLIENTE_ID
				AND PC.PRODUCTO_ID=@PRODUCTO_ID
				AND PC.TIPO_COMPROBANTE_ID='DO'

		If @CAT_LOG_ID Is null begin
			--entra porque no tiene categorias particulares y busca la default.
			select 	@CAT_LOG_ID=p.ing_cat_log_id,
					@PALLET_AUTOMATICO=PALLET_AUTOMATICO,
					@lote=lote_automatico
			From 	producto p 
			where  	p.cliente_id=@CLIENTE_ID
					and p.producto_id=@PRODUCTO_ID
		end 
		IF @PALLET_AUTOMATICO = '1'
			BEGIN
				--obtengo la secuencia para el numero de partida.
				exec get_value_for_sequence  'NROPALLET_SEQ', @nro_pallet Output
			END
			
		if @lote='1'
			begin		
				--obtengo la secuencia para el numero de Lote.
				exec get_value_for_sequence 'NROLOTE_SEQ', @Lote_At Output			
			end
		select @descripcion=descripcion, @unidad_id=unidad_id from producto where cliente_id=@cliente_id and producto_id=@producto_id

		-- INSERTANDO EL DETALLE
		insert into det_documento (documento_id, nro_linea	, cliente_id	, producto_id	, cantidad	, cat_log_id	, cat_log_id_final	, tie_in	, fecha_vencimiento	, nro_partida	, unidad_id		, descripcion	, busc_individual	, item_ok	, cant_solicitada , prop1	, prop2			, nro_bulto	,nro_lote)
								values(@doc_id, @Nrolinea	, @cliente_id	, @producto_id	, @cantidad	, null			, @cat_log_id		, '0'		, null			, @nro_partida	, @unidad_id	, @descripcion	, '1'				, '1'		,@qtyBO			, @nro_pallet	,@DOC_EXT	, null		, @lote_at)

		--Documento a Ingreso.
		select 	@Preing=nave_id
		from	nave
		where	pre_ingreso='1'
		
		SELECT 	@catlogid=cat_log_id
		FROM 	categoria_stock cs
				INNER JOIN categoria_logica cl
				ON cl.categ_stock_id = cs.categ_stock_id
		WHERE 	cs.categ_stock_id = 'TRAN_ING'
				And cliente_id =@cliente_id

		UPDATE det_documento
		Set cat_log_id =@catlogid
		WHERE documento_id = @Doc_ID

		Update documento set status='D20' where documento_id=@doc_id
		
		Insert Into RL_DET_DOC_TRANS_POSICION (
					DOC_TRANS_ID,				NRO_LINEA_TRANS,
					POSICION_ANTERIOR,			POSICION_ACTUAL,
					CANTIDAD,					TIPO_MOVIMIENTO_ID,
					ULTIMA_ESTACION,			ULTIMA_SECUENCIA,
					NAVE_ANTERIOR,				NAVE_ACTUAL,
					DOCUMENTO_ID,				NRO_LINEA,
					DISPONIBLE,					DOC_TRANS_ID_EGR,
					NRO_LINEA_TRANS_EGR,		DOC_TRANS_ID_TR,
					NRO_LINEA_TRANS_TR,			CLIENTE_ID,
					CAT_LOG_ID,					CAT_LOG_ID_FINAL,
					EST_MERC_ID)
		Values (NULL, NULL, NULL, NULL, @cantidad, NULL, NULL, NULL, NULL, @PREING, @doc_id, @Nrolinea, null, null, null, null, null, @cliente_id, @catlogid,@CAT_LOG_ID,null)

		------------------------------------------------------------------------------------------------------------------------------------
		--Generacion del Back Order.
		-----------------------------------------------------------------------------------------------------------------
		select @lineBO=max(isnull(nro_linea,1))+1 from sys_int_det_documento WHERE 	 DOC_EXT=@doc_ext

		Select 	@qtyBO=sum(cantidad_solicitada)
		from	sys_int_det_documento
		where	doc_ext=@doc_ext
				and fecha_estado_gt is null
				and estado_gt is null

		
		UPDATE SYS_INT_DOCUMENTO SET ESTADO_GT='P'	,FECHA_ESTADO_GT=getdate() WHERE DOC_EXT=@doc_ext
		
		UPDATE SYS_INT_DET_DOCUMENTO SET ESTADO_GT='P', DOC_BACK_ORDER=@doc_ext,FECHA_ESTADO_GT=getdate(), DOCUMENTO_ID=@Doc_ID
		WHERE 	DOC_EXT=@doc_ext	and documento_id is null

		set @qtyBO=@qtyBO - @cantidad

		IF @qtyBO>0 --Si esta variable es mayor a 0, genero el backorder.
		begin
		insert into sys_int_det_documento 
			select TOP 1 
					DOC_EXT, @lineBO ,CLIENTE_ID, PRODUCTO_ID, @qtyBO ,Cantidad , EST_MERC_ID, CAT_LOG_ID, NRO_BULTO, DESCRIPCION, NRO_LOTE, NRO_PALLET, FECHA_VENCIMIENTO, NRO_DESPACHO, NRO_PARTIDA, UNIDAD_ID, UNIDAD_CONTENEDORA_ID, PESO, UNIDAD_PESO, VOLUMEN, UNIDAD_VOLUMEN, PROP1, PROP2, PROP3, LARGO, ALTO, ANCHO, NULL, NULL, NULL,  NULL,NULL,NULL,NULL,NULL 
			from 	sys_int_det_documento 
			WHERE 	DOC_EXT=@Doc_Ext
		end
		------------------------------------------------------------------------------------------------------------------------------------
		--Guardo en la tabla de auditoria
		-----------------------------------------------------------------------------------------------------------------
		exec dbo.AUDITORIA_HIST_INSERT_ING @doc_id
		--insert into IMPRESION_RODC VALUES(@Doc_id, 1, @Tipo_eti,'0')
		--COMMIT TRANSACTION
		Set @DOCUMENTO_ID=@doc_id

		update ingreso_oc
		set procesado = 1
		WHERE     (CLIENTE_ID = @CLIENTE_ID) AND (PRODUCTO_ID = @producto_id) AND (ORDEN_COMPRA = @oc)
		
		fetch next from Ingreso_Cursor INTO @producto_id, @cantidad, @fecha
	END	
	COMMIT TRANSACTION
	CLOSE Ingreso_Cursor
	DEALLOCATE Ingreso_Cursor
	
	INSERT INTO IMPRESION_RODC VALUES(@Doc_ID,0,'D',0)
	-----------------------------------------------------------------------------------------------------------------
	--ASIGNO TRATAMIENTO...
	-----------------------------------------------------------------------------------------------------------------
	exec asigna_tratamiento#asigna_tratamiento_ing @doc_id	
	exec dbo.AUDITORIA_HIST_INSERT_ING @doc_id
	
END

