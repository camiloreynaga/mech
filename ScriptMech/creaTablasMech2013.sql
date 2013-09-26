-----------------------------------------
------------01/03/2013---------------
----------------------------------------
--drop database BD_ConstrucMech
--create database BD_ConstrucMech

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
	email varchar(100),
	estado int, 	--1=Activo 0=Inactivo
	--foreign key(codTipU) references TTipoUsu,
	foreign key(codCar) references TCargo
)
--- select * from TPersonal
--Modificar campos a la estructura de nuestra base de datos
--ALTER TABLE TPersonal ALTER COLUMN email varchar(100)

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
	cuentaDet varchar(60) default '',
	foreign key(idTipId) references TTipoIdent
)
--select * from TIdentidad
--aumentar campos a la estructura de nuestra base de datos
--ALTER TABLE TIdentidad ADD cuentaDet varchar(60) default ''
--update TIdentidad set cuentaDet=''

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

create table TTipoDocCompra
(	codTipDC int identity(1,1) primary key,	
	tipoDC varchar(20)  --Factura, Boleta...Nota Compra	
)
--select * from TTipoDocCompra	

--FORMA DE PAGO DISPONIBLES PARA COMPRAS
create table TFormaPago
(	codPag int identity(1,1) primary key,	
	forma varchar(60)  --Contado, Credito	
)	
-- select * from TFormaPago
--MODIFICAR TIPO de DATOS campos a la estructura de nuestra base de datos
--ALTER TABLE TFormaPago ALTER COLUMN forma varchar(60)

-- MONEDAS USADAS EN TRANSACCIONES
create table TMoneda
(	codMon int identity(30,5) primary key,	--NO CAMBIAR JUAN
	moneda varchar(20), --Nuevos Soles - Dolares Americanos
	simbolo varchar(10),
)	

--select * from TSolicitud
--DROP table TSolicitud
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
--select * from TDetalleSol
--DROP table TDetalleSol
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
--select * from TGrupoCot
--DROP table TGrupoCot
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
	codET int default 1,  -- TRANSPORTE
	nota varchar(200) default '',
	foreign key(codIde) references TIdentidad,
	foreign key(codPers) references TPersonal,
	foreign key(codPag) references TFormaPago,
	foreign key(codMon) references TMoneda,
	foreign key(codigo) references TLugarTrabajo
)
--select * from TOrdenCompra
--aumentar campos a la estructura de nuestra base de datos
--ALTER TABLE TOrdenCompra ADD nota varchar(200) default ''
--update TOrdenCompra set nota=''

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
	banco varchar(60),
	nroCta varchar(50), --proveedor
	nroDet varchar(30), --nro detraccion
	datoReq varchar(200), -- descripcion de requerimiento si no tiene orden de compra
	factCheck int, -- 0 1
	bolCheck int, -- 0 1
	guiaCheck int, -- 0 1
	vouCheck int, -- 0 1
	vouDCheck int, -- 0 1
	reciCheck int, -- 0 1
	otroCheck int, -- 0 1
	descOtro varchar(60), --descripcion de otro tipo de doc
	nroConfor varchar(30), --Nro de comprobante conformidad
	fecEnt varchar(10), --fecha de entrega
	hist varchar(200),
	codSerO int default 1,  --Codigo de serie de orden de desembolso
	vanCaja int default 0,	--OBSOLETO YA NO SIRVE vandera pa saber si gue procesado en caja chica 1=Procesado
	foreign key(codMon) references TMoneda,
	foreign key(codigo) references TLugarTrabajo,
	foreign key(codIde) references TIdentidad
) 

--- select * from TOrdenDesembolso
--MODIFICAR TIPO de DATOS campos a la estructura de nuestra base de datos
--ALTER TABLE TOrdenDesembolso ALTER COLUMN banco varchar(60)

create table TDesOrden
(	nroDO int identity(1,1) primary key,
	idOP int,
	nroOrden int, 
	foreign key(idOP) references TOrdenDesembolso,
	foreign key(nroOrden) references TOrdenCompra
)
-- select * from TPersDesem
--DROP table TPersDesem
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
	tipoP varchar(100),  --Tranferencia, cheque, efectivo	
	nro varchar(10)
)	

create table TClasifPago  --QUEDA OBSOLETO
(	codCla int identity(1,1) primary key,	
	clasif varchar(20)  --Proveedores, Haberes CTS	
)	

create table TPagoDesembolso
(	codPagD int identity(1,1) primary key,
	fecPago date,
	codTipP int,
	nroP varchar(20),  --nro cheque operacion bancaria
	pagoDet varchar(100), --descripcion de la transferecia, cheque  
	codCla int,
	codMon int,
	montoPago decimal(10,2), --monto total desembolso
	montoD decimal(10,2), --monto detraccion
	idOP int,
	idCue int default 0,  --cuenta banco  0=sin cuenta
	vanCaja int default 0,	--vandera pa saber si gue procesado en caja chica 1=Procesado
	codTipCla int default 1, --Varios=1
	vanEgreso int default 0, --0=Egreso 1=NO egreso
	idSesM int int default 1, -- mes improvisado pa todos los ingresos anteriores
	foreign key(codTipP) references TTipoPago,
	foreign key(codCla) references TClasifPago,
	foreign key(codMon) references TMoneda,
	foreign key(idOP) references TOrdenDesembolso
)
--select * from TPagoDesembolso
--aumentar campos a la estructura de nuestra base de datos
--ALTER TABLE TPagoDesembolso ADD codTipCla int default 1
--ALTER TABLE TPagoDesembolso ADD vanEgreso int default 0
--ALTER TABLE TPagoDesembolso ADD idSesM int default 1
--update TPagoDesembolso set codTipCla=1
--update TPagoDesembolso set vanEgreso=0
--update TPagoDesembolso set idSesM=1

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
	saldoBan decimal(12,2) default 0,
	foreign key(codMon) references TMoneda,
	foreign key(codBan) references TBanco
)
--select * from TCuentaBan
--aumentar campos a la estructura de nuestra base de datos
--ALTER TABLE TCuentaBan ADD saldoBan decimal(12,2) default 0
--update TCuentaBan set saldoBan=0

--SELECT * FROM TSerieOrden
create table TSerieOrden
(	codSerO int identity primary key, 
	serie varchar(10) default '001',
	iniNroDoc int,
	descrip varchar(40),
	estado int 	--1=Activo 0=Inactivo
)
--SELECT * FROM TSeriePers
create table TSeriePers
(	codSP int identity primary key,
	codPers int,	
	codSerO int,
	foreign key(codPers) references TPersonal,
	foreign key(codSerO) references TSerieOrden
)

create table TMatUbi
(	idMU int identity primary key,
	codMat int,
	codUbi int,	
	stock decimal(10,2),
	foreign key(codMat) references TMaterial,
	foreign key(codUbi) references TUbicacion
)

create table TTipoTransac
(	codTrans int identity(1,1) primary key,
	tipo varchar(30),
)

create table TSaldo
(	codSal int identity primary key,
	saldo decimal(10,2),
	codLug varchar(10)  --codigo obra
)

create table TEntradaSalida
(	nroNota int identity primary key,
	fecha date,
	codMat int,
	idMU int,
	codUbi int,  --Alamcen origeb
	cantEnt decimal(8,2) default 0,
	preUniEnt decimal(8,2) default 0,
	cantSal decimal(8,2) default 0,
	preUniSal decimal(8,2) default 0,
	codGuia	int,	
	nroGuia varchar(30),
	codDoc int,
	nroDoc varchar(30),
	otroDoc varchar(100),  --Rescatado para obser. de Incompleto u otro
	codTrans int,
	codUsu int  default 0,
	codPers int,	
	obs varchar(200) default '',
	codSal int,
	codUbiDes int default 0,  --Almacen destino para salidas de Mech
	vanET int default 0,    --0=Pendiente  1=Recibido 2=Incompleto
	codProv int default 0,  --Proveedor de entrada
	foreign key(codMat) references TMaterial,
	foreign key(idMU) references TMatUbi,
	foreign key(codUbi) references TUbicacion,
	foreign key(codTrans) references TTipoTransac,
	foreign key(codSal) references  TSaldo
)
--- select * from  TEntradaSalida 
update TEntradaSalida set codUbiDes=1,codProv=1
--MODIFICAR TIPO de DATOS campos a la estructura de nuestra base de datos
--ALTER TABLE  TEntradaSalida ALTER COLUMN otroDoc varchar(100)

--select * from TEntradaSalida
--aumentar campos a la estructura de nuestra base de datos
--ALTER TABLE TEntradaSalida ADD codUbiDes int default 0
--ALTER TABLE TEntradaSalida ADD vanET int default 0
--ALTER TABLE TEntradaSalida ADD codProv int default 0
--update TEntradaSalida set codUbiDes=0, vanET=0, codProv=0

-- EMPRESA DE TRANSPORTES
-- select * from TEmpTransp
create table TEmpTransp
(	codET int identity primary key, 
	nombre varchar(60),
	ruc varchar(11),
	dir varchar(120),
	fono varchar(60),
	contacto varchar(60)
)
-- DROP table TVehiculo
-- select * from TVehiculo
create table TVehiculo
(	codVeh int identity primary key, 
	marcaNro varchar(40), --marca nro placa
	nroConst varchar(40),
	codET int,
	foreign key(codET) references TEmpTransp
)	
--DROP table TTransportista
-- select * from TTransportista
create table TTransportista
(	codT int identity primary key, 
	nombre varchar(60),
	DNI varchar(8),
	nroLic varchar (30),
	codET int,
	foreign key(codET) references TEmpTransp
)	
--DROP table TMotivoGuia
-- select * from TMotivoGuia
create table TMotivoGuia
(	codMotG int identity primary key, 
	motivo varchar(40)  --venta compra traspaso
)	
--select * from TTipoDocEmp
create table TTipoDocEmp
(	codTipDE int identity(70,5) primary key, --NO CAMBIAR JUAN...
	tipoDE varchar(30),  --Factura, guia de REmision...
	estado int, 	--1=Activo 0=Inactivo
)	
--select * from TSerieSede
create table TSerieSede
(	codSerS int identity primary key, 
	serie varchar(10) default '001',
	iniNroDoc int,
	finNroDoc int,
	descrip varchar(40),
	estado int, 	--1 Activo 0 Inactivo
	codTipDE int,
	foreign key(codTipDE) references TTipoDocEmp
)

--DROP table TGuiaRemision
-- select * from TGuiaRemEmp
create table TGuiaRemEmp
(	codGuiaE int identity primary key,
	talon varchar(5) default '001',
	nroGuia int,
	fecIni date,
	codSerS int,
	codIdeProv int default 0,  --PROVEEDOR EXTERNO
	codIde int,		--DESTINATARIO
	estado int,	--0=abierto,1=cerrado,2=anulado
	codUbiOri int, --Alamcen origen
	codUbiDes int, --almacen destino
	partida varchar(100),
	llegada varchar(100),
	codET int, --Empresa Transporte
	codVeh int,  --vehiculo
	codT int,	-- transportista
	codMotG int,
	nroFact varchar(30),
	obs varchar(200),
	codPers int,  --personal que crea nueva guia
	hist varchar(500),	--quien creo/anulo y en que fecha
	foreign key(codSerS) references TSerieSede,
	foreign key(codIde) references TIdentidad,
	foreign key(codVeh) references TVehiculo,
	foreign key(codT) references TTransportista,
	foreign key(codMotG) references TMotivoGuia,
	foreign key(codPers) references TPersonal
)
--DROP table TDetalleGuia
create table TDetalleGuiaEmp
(	codDGE int identity(1,1) primary key,
	codigo varchar(20), --codigo articulo proveedor
	cant decimal(8,2),
	descrip varchar(100),
	unidad varchar(20),
	peso decimal(7,2),
	codGuiaE int,
	codMat int,
	linea1 varchar(300), --algun tipo de descripcion por lines
	entregado int default 0,	--0=pendiente  1=recibido
	codPers int default 0, --personal que recibe
	recibido int default 0, --0 pendiente 1=recibido 2=incompleto
	obsR varchar(100) default '', --Observacion recibido 
	foreign key(codGuiaE) references TGuiaRemEmp,
	foreign key(codMat) references TMaterial
)

-- DROP table TDocGuia
create table TDocGuia
(	nroDG int identity(1,1) primary key,
	codDocC int,
	codGuia int 
	foreign key(codDocC) references TDocCompra,
	foreign key(codGuia) references TGuiaRemision
)

--DROP table TOrdenGuia
create table TOrdenGuia
(	nroOG int identity(1,1) primary key,
	nroOrden int,
	codGuia int, 
	foreign key(nroOrden) references TOrdenCompra,
	foreign key(codGuia) references TGuiaRemision
)

-- DROP table TCajaChica
create table TCajaChica
(	codCC int identity primary key,
	fechaCre date,
	codigo varchar(10), --codigo de la Obra FK
	codPers int, --responsable codigo de la persona responsable
	codSerO int, -- Serie de Orden Desembolso para caja por obra
	foreign key (codigo) references TLugarTrabajo,
	foreign key (codPers) references TPersonal,
	foreign key (codSerO) references TSerieOrden
)

-- DROP table TCajas
create table TCajas
(	codCaj int identity primary key,
	caja varchar(20),  --Caja A  o Caja B
	codMon int, --Moneda a utilizar FK
	saldo decimal(10,2),
	estCaja int,  --0=Inactivo  1=Activo
	codCC int,
	foreign key(codMon) references TMoneda,
	foreign key (codCC) references TCajaChica
)

--drop table  TSolicitudCaja
create table TSolicitudCaja
(	codSC int identity primary key,
	fechaSol date,
	nroSol int,
	codPers int,  --solicitante
	estSol int,  --0 pendiente 1 aprobado 2=cerrado  3=anulado
	salAnt decimal(10,2), --saldo anterior personal
	montoSol decimal(10,2),  --solicitado
	imprevisto decimal(10,2), -- monto pedido pa gastos no sabidos con anterioridad
	montoRen decimal(10,2),   --rendido
	codObra varchar(10), --codigo de la Obra para donde se gastara
	codSede varchar(10), --codigo de la Obra de caja chica
	foreign key (codPers) references TPersonal
)

-- drop table TDetSolCaja
create table TDetSolCaja
(	codDetSol int identity primary key,
	cant1 decimal (8,2), --cant pedida
	cant2 decimal (8,2), --cant real
	uniMed varchar(20),
	insumo varchar(200), -- editable
	ingreso int, -- 0=pedido normal 1=pedido improvisado
	prec1 decimal(8,2), --precio proyectado
	prec2 decimal(8,2), --precio real
	obsSol varchar(200), -- obs solicitante
	codApro int,  --aprobador luis mech
	estDet int, --0 pendiente, 1 aprobado, 2 observado
	obsApro varchar(200), --obs aprobador
	codMat int,  -- 0=si no hereda insumo estructurado
	codAreaM int,
	codTipM int,
	codSC int,
	estRen int, --0 pendiente  1 OK 2 Observado
	codRen int, --personal revisa rendicion
	obsRen varchar(200), --obs del que revisa
	codDC int, -- cod detalle doc. de compra factura boleta, etc.
	nroOtros varchar(30), --nro de otro doc recibo ticket
	compCheck int, -- 1=factura 2=BV  3=Honorarios 4=otros Recibos ticket 
	foreign key(codAreaM) references TAreaMat,
	foreign key(codTipM) references TTipoMat,
	foreign key (codSC) references TSolicitudCaja
)

--DROP table TTipoMovCaja
create table TTipoMovCaja
(	codTM int identity(1,1) primary key,
	tipoMov varchar(20),  --ingreso  egreso
)

--drop table TDiaCaja
create table TDiaCaja
(	codDia int identity primary key,
	fecha date,
	estado int,	--1=Abierto,  2=cerrado
	horaAbrio varchar(10),
	horaCerro varchar(10),
	codPersA int,	--aperturo
	codPersC int,    --Cerro
	codigo varchar(10),
	foreign key(codigo) references TLugarTrabajo
)

-- drop table TMovimientoCaja
create table TMovimientoCaja
(	nroMC int identity primary key,
	codDia int,
	codTM int,
	codigo varchar(10),
	codCaj int,
	codPagD int,
	codSC int,  --Solicitante
	montoEnt decimal(10,2),
	montoSal decimal(10,2),
	saldoMov decimal(10,2),
	codUsu int,  --cajera
	descrip varchar(200) default '',
	foreign key(codDia) references TDiaCaja,
	foreign key(codTM) references TTipoMovCaja,
	foreign key(codigo) references TLugarTrabajo,
	foreign key (codCaj) references TCajas
	--foreign key(idOP) references TOrdenDesembolso,
	--foreign key (codSC) references TSolicitudCaja
)

-------MODULO INGRESOS EGRESOS(DESEMBOLSOS) MECH------
-----------------EJECUTAR 23/09/2013------------------
------------------------------------------------------
create table TMes
(	idMes int identity primary key,
	mes varchar(20)  --  Setiembre
)

create table TSesionMes
(	idSesM int identity primary key,
	idMes int,
	ano int,	--2013
	estado int, --1=abierto  2=cerrado
	codigo varchar(10),	--codigo de de lugar
	foreign key(idMes) references TMes,
	foreign key(codigo) references TlugarTrabajo
)

-- DROP table TDocCompra
create table TDocCompra
(	codDocC int identity(1,1) primary key,
	serie varchar(5),
	nroDoc int,
	fecDoc date,
	fecCan varchar(10),
	idSesM int,
	codTipDC int,
	estado int,	--0=abierto,1=cerrado,2=anulado
	codIde int,
	codPag int,
	igv decimal(6,2),
	calIGV int,  --0=Boleta otros  1=Tipo IGV 2=TipoIGV
	codMon int,
	camD decimal(5,2),
	obs varchar(200),
	hist varchar(500), --quien creo/modifico/anulo y en que fecha
	codTipCla int,     --0=No es ingreso de dinero a MECH
	foreign key(codTipDC) references TTipoDocCompra,
	foreign key(codIde) references TIdentidad,
	foreign key(codPag) references TFormaPago,
	foreign key(codMon) references TMoneda,
	foreign key(idSesM) references TSesionMes
)

--DROP table TDetalleCompra
create table TDetalleCompra
(	codDC int identity(1,1) primary key,
	cant decimal(8,2),
	unidad varchar(20),
	detalle varchar(100),
	preUni decimal(12,2),
	codDocC int,
	codMat int,  -- 0=si no hereda insumo estructurado
	foreign key(codDocC) references TDocCompra,
	--foreign key(codMat) references TMaterial
)

create table TTipoMovimiento
(	idTM int identity(1,1) primary key,
	tipoMov varchar(20),  --ingreso=Factura  egreso=desembolso
)

create table TClasificacion
(	codCla int identity(1,1) primary key,	
	clasificacion varchar(30),  --Obras, Sueldos, Cajas Chica, Activos
	idTM int,
	foreign key(idTM) references TTipoMovimiento	
)	

create table TTipoClasif
(	codTipCla int identity(1,1) primary key,	
	tipoClasif varchar(30),  --Sueldos, CTS, IGV
	estado int,	--0=Inactivo  1=Activo
	codCla int,
	foreign key(codCla) references TClasificacion
)	

create table TMovimientoMech
(	idMov int identity primary key,
	fecDoc date,
	idSesM int,
	idTM int,
	codigo varchar(10),
	idCue int, --cuenta banco donde esta el saldo
	codPagD int, --Desembolso para egreso
	codDocC int,  --Ingreso Doc. (Factura)
	montoIng decimal(12,2),
	montoEgr decimal(12,2),
	saldoMov decimal(12,2),
	codPers int,  --pers procesa
	descrip varchar(200) default '',
	foreign key(idSesM) references TSesionMes,
	foreign key(idTM) references TTipoMovimiento,
	foreign key(codigo) references TLugarTrabajo,
	foreign key (idCue) references TCuentaBan,
	foreign key(codPers) references TPersonal,
)


