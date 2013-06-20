-----------------------------------------
------------01/03/2013---------------
----------------------------------------
--drop database BD_ConstrucMech
--create database BD_ConstrucMech
--TIPOS DE USUARIO
create table TTipoUsu
(	codTipU int identity primary key,
	tipo varchar(30),  --Administrador,Almacenero,obra,logistica 
	tipoCargo int -- 0 :Corporativo 1 :Obra
)
--CARGOS DE PERSONAL
create table TCargo
(	codCar int identity primary key,
	cargo varchar(30)  --gerente,administrador 
	
)
--REGISTROS DE PERSONAL
create table TPersonal
(	codPers int identity primary key,
	usuario varchar(20),
	pass varchar(20),
	codTipU int, 	--0=sin acceso al sistema
	nombre varchar(20),
	apellido varchar(30),
	codCar int,
	dni varchar(8),
	dir varchar(60),
	fono varchar(60),
	email varchar(50),
	estado int, 	--1=Activo 0=Inactivo
	--foreign key(codTipU) references TTipoUsu,
	foreign key(codCar) references TCargo
)
--- select * from TPersonal
--aumentar campos a la estructura de nuestra base de datos
--ALTER TABLE TPersonal ALTER COLUMN email varchar(50)

--TIPOS DE IDENTIDAD (CLIENTE/PROVEEDOR)
create table TTipoIdent
(	idTipId int identity primary key,
	tipoId varchar(30)  --1Proveedor otros, 2=Cliente
)
--IDENTIDAD
create table TIdentidad
(	codIde int identity primary key,
	razon varchar(60),
	ruc varchar(11),
	dir varchar(120),
	fono varchar(30),
	fax varchar(30),
	celRpm varchar(50),
	email varchar(30),
	repres varchar(60),
	dni varchar(8),
	estado int, 	--1=Activo 0=Inactivo
	idTipId int,
	cuentaBan varchar(60) default '',
	foreign key(idTipId) references TTipoIdent
)
--select * from TLugarTrabajo
--aumentar campos a la estructura de nuestra base de datos
--ALTER TABLE TIdentidad ADD cuentaBan varchar(60) default ''
--update TIdentidad set cuentaBan=''
--LUGARES DE TRABAJO. OBRAS O SEDES
create table TLugarTrabajo
(	codigo varchar(10) primary key,  
	fecAper date,
	nombre varchar(100),
	lugar varchar(100),
	tiempoMeta varchar(20),
	tiempoContr varchar(20),
	presupMeta decimal(10,2),
	presupContr decimal(10,2),
	codIde int,
	estado int not null default 1,
	color varchar(15), 
	foreign key(codIde) references TIdentidad
)
--TABLA INTERMEDIA RELACIONA PERSONAL CON OBRAS/SEDES (LUGARES DE TRABAJO)
create table TPersLugar
(	codPL int identity primary key,
	codPers int,
	codigo varchar(10),	--codigo lugarTrabajo...
	foreign key(codPers) references TPersonal,
	foreign key(codigo) references TLugarTrabajo
)
-- UBICACIONES. (ALMACENES DE OBRA)
create table TUbicacion
(	codUbi int identity primary key,
	ubicacion varchar(50),
	estado int,  --1=Activo 0=Inactivo
	codigo varchar(10),
	color varchar(15),	--Identificar con un color a un modulo
	foreign key(codigo) references TLugarTrabajo
)
-- AREAS QUE SOLICITAN MATERIAL (ÁREAS DE OBRA)
create table TAreaMat
(	codAreaM int identity(1,1) primary key,
	areaM varchar(40)
)
-- TIPO DE MATERIAL, CLASISFICACIÓN SEGUN MECH
create table TTipoMat
(	codTipM int identity(1,1) primary key,
	tipoM varchar(40)
)
-- UNIDADES DE MEDIDAD PARA MATERIAL(INSUMOS)
create table TUnidad
(	codUni int identity primary key,
	unidad varchar(20)
)
-- REGISTROS DE MATERIALES (INSUMOS)
create table TMaterial
(	codMat int identity(10,1) primary key,
	material varchar(100),
	codUni int,
	preBase decimal(8,2),
	estado int, 	--1=Activo 0=Inactivo
	codAreaM int,
	codTipM int,
	hist varchar(500) default '', --quien creo/modifico/anulo y en que fecha
	foreign key(codUni) references TUnidad,
	foreign key(codAreaM) references TAreaMat,
	foreign key(codTipM) references TTipoMat
)
--select * from TMaterial
--aumentar campos a la estructura de nuestra base de datos
--ALTER TABLE TMaterial ADD hist varchar(500) default ''
--update TMaterial set hist=''

-- TIPOS DE DOCUMENTOS DE COMPRA (FACTURA/BOLETA)
create table TTipoDocCompra
(	codTipDC int identity(1,1) primary key,	
	tipoDC varchar(20)  --Factura, Boleta...Nota Compra	
)	
--FORMA DE PAGO DISPONIBLES PARA COMPRAS
create table TFormaPago
(	codPag int identity(1,1) primary key,	
	forma varchar(20)  --Contado, Credito	
)	
-- MONEDAS USADAS EN TRANSACCIONES
create table TMoneda
(	codMon int identity(30,5) primary key,	--NO CAMBIAR JUAN
	moneda varchar(20), --Nuevos Soles - Dolares Americanos
	simbolo varchar(10),
)	
-- REGISTRO DE DOCUMENTOS DE COMPRA.
create table TDocCompra
(	codDocC int identity(1,1) primary key,
	serie varchar(5),
	nroDoc int,
	fecDoc date,
	fecCan varchar(10),
	codTipDC int,
	estado int,	--0=abierto,1=cerrado,2=anulado
	codIde int,
	codPag int,
	igv decimal(6,2),
	calIGV int,  --0=Boleta otros  1=Tipo IGV 2=TipoIGV
	codMon int,
	camD decimal(5,2),
	obs varchar(100),
	hist varchar(500), --quien creo/modifico/anulo y en que fecha
	foreign key(codTipDC) references TTipoDocCompra,
	foreign key(codIde) references TIdentidad,
	foreign key(codPag) references TFormaPago,
	foreign key(codMon) references TMoneda
)
-- REGISTRO DE DETALLE DEL DOCUMENTO DE COMPRA
create table TDetalleCompra
(	codDC int identity(1,1) primary key,
	cant decimal(8,2),
	unidad varchar(20),
	material varchar(100),
	preUni decimal(8,2),
	codDocC int,
	codMat int,
	foreign key(codDocC) references TDocCompra,
	foreign key(codMat) references TMaterial
)
-- EMPRESA DE TRANSPORTES
create table TEmpTransp
(	codET int identity primary key, 
	nombre varchar(50),
	ruc varchar(11)
)
-- VEHICULO QUE REALIZA TRANSPORTE
create table TVehiculo
(	codVeh int identity primary key, 
	marcaNro varchar(40), --marca nro placa
	nroConst varchar(40),
	codET int,
	foreign key(codET) references TEmpTransp
)	
-- TRANPOSRTISTA
create table TTransportista
(	codT int identity primary key, 
	nombre varchar(60),
	DNI varchar(8),
	nroLic varchar (30),
	codET int,
	foreign key(codET) references TEmpTransp
)	
--
create table TMotivoGuia
(	codMotG int identity primary key, 
	motivo varchar(40)  --venta compra traspaso
)	
--
create table TGuiaRemision
(	codGuia int identity primary key,
	talon varchar(5) default '001',
	nroGuia int,
	fecIni date,
	estado int,	--0=abierto,1=cerrado,2=anulado
	codIde int, --proveedor
	codIde1 int, --cliente
	partida varchar(60),
	llegada varchar(60),
	codVeh int,  --vehiculo
	codT int,	-- transportista
	codMotG int,
	obs varchar(100),
	hist varchar(500),	--quien creo/anulo y en que fecha
	foreign key(codIde) references TIdentidad,
	foreign key(codVeh) references TVehiculo,
	foreign key(codT) references TTransportista,
	foreign key(codMotG) references TMotivoGuia
)
-- TABLA INTERMEDIA PARA DOCUMENTO COMPRA (FACTURA) Y GUIA DE REMISION
create table TDocGuia
(	nroDG int identity(1,1) primary key,
	codDocC int,
	codGuia int 
	foreign key(codDocC) references TDocCompra,
	foreign key(codGuia) references TGuiaRemision
)

create table TDetalleGuia
(	codDG int identity(1,1) primary key,
	codigo varchar(20),
	descrip varchar(100),
	cant decimal(8,2),
	unidad varchar(20),
	peso decimal(7,2),
	--capacDeri int,
	codGuia int,
	codMat int,
	linea1 varchar(100), --algun tipo de descripcion por lines
	foreign key(codGuia) references TGuiaRemision,
	foreign key(codMat) references TMaterial
)
-- SOLICITUD DE REQUERIMIENTO DE OBRA
create table TSolicitud
(	idSol int identity primary key,
	nroS int,  --incrementado de acuerdo a lugar de trabajo
	fecSol date,
	codPers int, --solicitante
	codigo varchar(10), -- codLugar
	estado int, -- 0 abierto 1 cerrado
	obs varchar(200), -- Observacion solicitante
	estDet int default 0,   -- 0=abierto 1=cerrado pa no modificaciones en Detalle Solicitud
	foreign key(codPers) references TPersonal,
	foreign key(codigo) references TLugarTrabajo
)
--select * from TSolicitud
--aumentar campos a la estructura de nuestra base de datos
--ALTER TABLE TSolicitud ADD estDet int default 0
--update TSolicitud set estDet=0

-- ESTADOS DE SOLICITUD
create table TEstSol
(	codEstS int identity primary key, 
	estSol varchar(20)  -- pendiente aprobado observado rechazado
)	

create table TDetalleSol
(	codDetS int identity(1,1) primary key,
	prioridad varchar(20), --urgente
	cant decimal(8,2),
	descrip varchar(100),
	unidad varchar(20),
	codPers int, --solicitante x area
	obs1 varchar(200), 
	codPersA int, --persona qe aprueva
	codEstS int,
	obs2 varchar(200) default '', -- Observacion aprobador
	codMat int,
	codAreaM int,
	idSol int,
	estRecep int default 0,   --0=pendiente 1=recibido 2=incompleto
	codPersR int default 0, --persona qe recepciona insumo almacenero
	obs3 varchar(200) default '', -- Observacion recepcionista materiales
	foreign key(codPers) references TPersonal,
	foreign key(codEstS) references TEstSol,
	foreign key(codMat) references TMaterial,
	foreign key(codAreaM) references TAreaMat,
	foreign key(idSol) references TSolicitud
)
--select * from TDetalleSol
--aumentar campos a la estructura de nuestra base de datos
--ALTER TABLE TDetalleSol ADD estRecep int default 0
--ALTER TABLE TDetalleSol ADD codPersR int default 0
--ALTER TABLE TDetalleSol ADD obs3 varchar(200) default ''
--update TDetalleSol set estRecep=0,codPersR=0,obs3=''

----------NUEVO 23/04/2013--------------------
--GRUPO PARA COTIZACIONES
create table TGrupoCot
(	codGruC int identity primary key, 
	nroGru int, 
	descrip varchar(40),  -- descripcion del grupo de cotizaciones
	estGru int	--0=abierto,1=cerrado
)

create table TCotizacion
(	codCot int identity(1,1) primary key,
	nroCot int,  --007-MECH-2013
	codIde int,
	fecCot date,
	tiempoVig varchar(20),
	atencion varchar (40), --pers qe atiende departe del provee
	plazo varchar(40),  --plazo de entrega
	codPag int,
	lugarEnt varchar(100),
	incluir varchar(100),  --certificado de calidad
	codPersS int, -- solicitante obra
	codigo varchar(10), -- codLugar obra
	codPers int,   --remitente
	obs varchar(200),
	idSol int default 0,  --0 sin solicitud
	codGruC int,
	codMon int default 30, --30=Nuevos soles
	foreign key(codIde) references TIdentidad,
	foreign key(codPag) references TFormaPago,
	foreign key(codigo) references TLugarTrabajo,
	foreign key(codPers) references TPersonal,
	foreign key(codGruC) references TGrupoCot
)
--select * from TCotizacion
--aumentar campos a la estructura de nuestra base de datos
--ALTER TABLE TCotizacion ADD codMon int default 30
--update TCotizacion set codMon=30

--select * from TCotizacion
create table TDetalleCot
(	codDetC int identity(1,1) primary key,
	cant decimal(8,2),
	unidad varchar(20),
	descrip varchar(100),
	precio decimal(8,2),
	subTotal decimal(8,2), 
	codCot int,
	codMat int,
	estado int,	--0=Pendiente,1=aprobado,2=denegado
	foreign key(codMat) references TMaterial,
	foreign key(codCot) references TCotizacion
)

----------NUEVO 01/05/2013--------------------
create table TOrdenCompra
(	nroOrden int identity(1,1) primary key,
	nroO int,	
	fecOrden date,
	codIde int, --proveedor
	codPers int, --responsable compra
	codPag int,
	igv decimal(6,2),
	calIGV int,  --0=Boleta otros  1=Tipo IGV 2=TipoIGV
	codMon int,
	atiendeCom varchar(50), -- nombre pers provee
	celAti varchar(50),
	plazoEnt varchar(40), -- plazo entrega
	transfe varchar(100), -- cuenta proveedor
	nroProf varchar(40),   -- nro Proforma
	obsFac varchar(200),
	estado int,	--0=abierto,1=terminado,2=cerrado,3=anulado
	codCot int default 0,  -- 0 si no tiene cotizacion
	idSol int default 0,  -- 0 si no tiene solicitud
	codPersO int, -- recepcionista materiales
	codigo varchar(10), -- codLugar obra
	lugarEnt varchar(100),
	hist varchar(200),
	foreign key(codIde) references TIdentidad,
	foreign key(codPers) references TPersonal,
	foreign key(codPag) references TFormaPago,
	foreign key(codMon) references TMoneda,
	foreign key(codigo) references TLugarTrabajo
)
--select * from TOrdenCompra
create table TDetalleOrden
(	codDetO int identity(1,1) primary key,
	cant decimal(8,2),
	unidad varchar(20),
	descrip varchar(100),
	precio decimal(8,2),
	subTotal decimal(8,2), 
	codMat int,
	nroOrden int,
	foreign key(codMat) references TMaterial,
	foreign key(nroOrden) references TOrdenCompra
)
--select * from TDetalleOrden
create table TOrdenGuia
(	nroOG int identity(1,1) primary key,
	nroOrden int,
	codGuia int, 
	foreign key(nroOrden) references TOrdenCompra,
	foreign key(codGuia) references TGuiaRemision
)

----------NUEVO 18/05/2013--------------------
-- TABLA INTERMEDIA DE ORNDE DE COMPRA DESEMBOLSO

create table TOrdenDesembolso
(	idOP int identity(1,1) primary key,
	serie varchar(5),
	nroDes int,
	fecDes date,
	codMon int,
	monto decimal(10,2), --monto total desembolso
	montoDet decimal(8,2), --monto detraccion
	montoDif decimal(10,2), --monto diferencia
	estado int,	--0=pendiente,1=terminado,2=cerrado 3=anulado
	codigo varchar(10), -- codLugar obra
	codIde int,
	banco varchar(30),
	nroCta varchar(50), --proveedor
	nroDet varchar(30), --nro detraccion
	datoReq varchar(200), -- descripcion de requerimiento si no tiene orden de compra
	factCheck int, -- 0 1
	nroFact varchar(20),
	bolCheck int, -- 0 1
	nroBol varchar(20),
	guiaCheck int, -- 0 1
	nroGuia varchar(20),
	vouCheck int, -- 0 1
	nroVou varchar(20),
	vouDCheck int, -- 0 1
	nroVouD varchar(20),
	reciCheck int, -- 0 1
	nroReci varchar(20),
	otroCheck int, -- 0 1
	descOtro varchar(60), --descripcion de otro tipo de doc
	nroConfor varchar(30), --Nro de comprobante conformidad
	fecEnt varchar(10), --fecha de entrega
	hist varchar(200),
	foreign key(codMon) references TMoneda,
	foreign key(codigo) references TLugarTrabajo,
	foreign key(codIde) references TIdentidad
)
-- TABLA INTERMEDIA ORDEN DE COMPRA POR DESEMBOLSO
create table TDesOrden
(	nroDO int identity(1,1) primary key,
	idOP int,
	nroOrden int, 
	foreign key(idOP) references TOrdenDesembolso,
	foreign key(nroOrden) references TOrdenCompra
)
-- TABLA INTERMEDIA PERSONA POR DESEMBOLSO
create table TPersDesem
(	codPersDes int identity(1,1) primary key,
	idOP int,
	codPers int, -- personal solicitante, gerente, tesorero
	estDesem int, --- 0=pendiente 1=aprobado 2=observado 3=denegado
	tipoA int, --- 1=solicitante 2=gerencia 3=tesoreria 4=contabilidad
	obserDesem varchar(100), -- observacion 
	fecFir date,             -- fecha firma
	foreign key(idOP) references TOrdenDesembolso,
	foreign key(codPers) references TPersonal
)

create table TTipoPago
(	codTipP int identity(1,1) primary key,	
	tipoP varchar(30)  --Tranferencia, cheque, efectivo	
)	

create table TPagoDesembolso
(	codPagD int identity(1,1) primary key,
	fecPago date,
	codTipP int,
	pagoDet varchar(100), --descripcion de la transferecia, cheque  
	codMon int,
	montoPago decimal(10,2), --monto total desembolso
	idOP int,
	idCue int default 0,  --cuenta banco  0=sin cuenta
	foreign key(codTipP) references TTipoPago,
	foreign key(codMon) references TMoneda,
	foreign key(idOP) references TOrdenDesembolso
)

create table TBanco
(	codBan int identity(1,1) primary key,	
	banco varchar(40)  --continental BCP
)

create table TCuentaBan
(	idCue int identity(1,1) primary key,
	nroCue varchar(60),
	codMon int,
	codBan int,
	estado int,			--1=activo   0=inactivo	
	foreign key(codMon) references TMoneda,
	foreign key(codBan) references TBanco
)


--*****************************************************
--------------------FIN DE SCRIPT----------------------
--*****************************************************
--DOCUMENTACION DE ESTADO EN TABLAS
------------------------------------------------------
-- TIdentidad 
--*************************
--ESTADO: 0=INACTIVO 1=ACTIVO

--TLugarTrabajo
--*************************
--ESTADO: 1=EJECUCION 2=PARALIZADO 3=CULMINADO

--TLUGAR ESTADO: 0=ABIERTO 1=CERRADO

--TCotizacion
--*************************
--TGrupoCot
-- ESTADO: 0=ABIERTO 1=CERRADO

--TDetalleCot
--*************************
--ESTADO 0=PENDIENTE 1=APROBADO 2 RECHAZADO

--TOrdenCompra
--*************************
--ESTADO: 0=ABIERTO 1=TERMINADO 2=CERRADO 3=ANULADO

--TOrdenDesembolso
--*************************
--ESTADO: 0=PENDIENTE 1=TERMINADO 2=CERRADO 3=ANULADO


--TSolicitud
--**************************
--Estado: 0=Abierto 1=Cerrado, controla si la solicitud se le pueden registrar insumos.
--EstDet: 0=Abierto 1=Cerrado, controla si la solicitud para a ser historica o si aún tiene ítems pendientes de compra.



