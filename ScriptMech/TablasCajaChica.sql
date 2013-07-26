create table TCajaChica
(
codCajaChica int identity primary key,
fechaCreacion Datetime,
moneda int, --Moneda a utilizar FK
saldo decimal(10,2),
codigo varchar(10), --codigo de la Obra FK
codPers int, --responsable codigo de la persona responsable
estadoCaja int ,
foreign key (codPers) references TPersonal (codPers),
foreign key (codigo) references TLugarTrabajo(codigo) 
)
go

create table TSolicitudCaja
(
codSolicitud int identity primary key,
nroSolicitud int,
fechaSolicitud datetime,
codigo varchar(10), -- codigo de la Obra FK
--estadoSolicitud int default 0, -- 0 = abierto  1 = cerrado  
perSolicitante int,
codCajaChica int, --codCajaChica. a la que se solicita dinero 
foreign key (codigo) references TLugarTrabajo(codigo),
foreign key (codCajaChica) references TCajaChica (codCajaChica)
)
go

create table DetSolitudCaja
(
codDetSol int identity primary key,
codSolicitud int, --FK
cantidad decimal (8,2),
insumo varchar(100),
undMedida varchar(20),
obsSolicitante varchar(200),
codAprobador varchar(10),
estadoDet int, --1 pendiente, 2 aprobado, 3 observado, 4 rechazado
obsAprobador varchar(200),
codAreaM int,

foreign key (codSolicitud) references TSolicitudCaja(codSolicitud),
foreign key (codAreaM) references TAreaMat(codAreaM)
)
go

create table TMovimientoCaja
(
codMovCaja int identity primary key,
fecha datetime,
tipoMov int, --0 Salida 1 Ingreso 
monto decimal (8,2),
OrigenDest varchar(50),
medioPago int,
descripcionPago varchar(20),-- numero Cheque, Operacion, Efectivo
codCajaChica int,
foreign key (codCajaChica) references tCajaChica(codCajaChica)

)
go

create table tRendicionCaja
(
codRendicion int identity primary key,
proveedor int, --proveedor
tipoComprobante int, -- Tipo de comprobante a registrar
nroComprobante varchar(20),
monto decimal (8,2),
observacion varchar(200),
estadoRend int , -- 0=pendiente Aprobado 1
codMovCaja int , --Fk movimiento 
foreign key (codMovCaja) references TMovimientoCaja(codMovCaja)
)
go

create table tDetaRendimiento
(
codDetRendicion int identity primary key,
codRendicion int ,
cantidad decimal (8,2),
insumo varchar(100),
undMedida varchar(20),
PrecioUnd decimal (8,2),
total decimal (8,2),
impuesto decimal (8,2),
obs varchar(200),

foreign key (codRendicion) references TRendicionCaja (codRendicion)

)
go