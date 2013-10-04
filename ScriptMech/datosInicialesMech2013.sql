--Datos Iniciales
--TIPO DE IDENTIDAD
insert into TTipoIdent(tipoId)	values('CLIENTE') --1
insert into TTipoIdent(tipoId)	values('PROVEEDOR') --2
GO
--select * from TTipoIdent
--DATOS DE MECH 
insert TIdentidad(razon,ruc,dir,fono,fax,celRpm,email,repres,dni,estado,idTipId) 
	values('CONSTRUCTORA MECH SRL','20277722187','CALLE BERNARDO TAMBOHUACSO Nº100 4TO NIVEL','0842717446','','','','','',1,1)
GO
--select * from TIdentidad
-- LUGAR DE TRABAJO INICIAL. SEDE PRINCIPAL
insert TLugarTrabajo(codigo,fecAper,nombre,lugar,tiempoMeta,tiempoContr,presupMeta,presupContr,codIde,estado,color) 
	values('00-00','01/03/2013','SEDE PRINCIPAL','CALLE BERNARDO TAMBOHUACSO Nº100 4TO NIVEL','','',0.0,0.0,1,1,'White')
GO
--select * from TLugarTrabajo
-- INSERCIÓN DE ALMACEN PRINCIPAL RELACIONADO CON LA SEDE PRINCIPAL (LUGAR DE TRABAJO)
insert into TUbicacion(ubicacion,estado,codigo,color)
	values('ALMACEN PRINCIPAL',0,'00-00','White') --1
GO
--select * from TUbicacion
--TIPOS DE USUARIOS
insert into TTipoUsu(tipo,tipoCargo ) values('Administrador Sistema',0)--1
insert into TTipoUsu(tipo,tipoCargo ) values('Gerencia General',0)--2
insert into TTipoUsu(tipo,tipoCargo) values('Gerencia Construcciones',0)--3
insert into TTipoUsu(tipo,tipoCargo) values('Logistica',0)--4
insert into TTipoUsu(tipo,tipoCargo) values('Tesorería',0)--5
insert into TTipoUsu(tipo,tipoCargo) values('Residente',1)--6
insert into TTipoUsu(tipo,tipoCargo) values('Jefe Seguridad',1)--7
insert into TTipoUsu(tipo,tipoCargo) values('Jefe Equipo Mecánico',1)--8
insert into TTipoUsu(tipo,tipoCargo) values('Administrador Obra',1)--9
insert into TTipoUsu(tipo,tipoCargo) values('Almacenero',1)--10
insert into TTipoUsu(tipo,tipoCargo) values('Contabilidad',0)--11
insert into TTipoUsu(tipo,tipoCargo) values('Caja Chica',0)--12
--update TTipoUsu set tipo='Residente' where codTipU=3
--select * from TTipoUsu
GO
--CARGOS INICIALES 
insert into TCargo(cargo) values('GERENTE GENERAL')--1
insert into TCargo(cargo) values('GERENTE CONSTRUCCION')--2
insert into TCargo(cargo) values('GERENTE LOGISTICA')--2
insert into TCargo(cargo) values('TESORERIA')--2
insert into TCargo(cargo) values('RESIDENTE')--2
insert into TCargo(cargo) values('JEFE DE SEGURIDAD')--2
insert into TCargo(cargo) values('JEFE DE EQUIPO MECÁNICO')--2
insert into TCargo(cargo) values('ADMINISTRADOR DE OBRA')--2
--select * from TCargo
GO
--USUARIO POR DEFECTO DEL SISTEMA
insert into TPersonal(usuario,pass,codTipU,nombre,apellido,codCar,dni,dir,fono,email,estado) 
	values('sistema','mimech',1,'SISTEMA','SSP SAC',1,'','','','',1)
GO
--select * from TPersonal

insert TUnidad(unidad) values('')  --1
GO
--select * from TUnidad

--ESTADOS PARA SOLICITUD DE REQUERIMIENTO
insert into TEstSol(estSol) values('PENDIENTE')--1
insert into TEstSol(estSol) values('APROBADO')--2
insert into TEstSol(estSol) values('OBSERVADO')--3
insert into TEstSol(estSol) values('RECHAZADO')--4
GO
--select * from TEstSol
-- FORMA DE PAGOS INICIALES
insert TFormaPago(forma) values('CONTADO')  --1
insert TFormaPago(forma) values('CREDITO')  --2
GO
--select * from TFormaPago
--MONEDAS
insert into TMoneda(moneda,simbolo) values('NUEVOS SOLES','S/.') --30  --No cambiar juan
insert into TMoneda(moneda,simbolo) values('DOLARES AMERICANOS','US$') --35 
GO
--select * from TMoneda
-- TIPO DE DOCUMENTO DE COMPRA
insert TTipoDocCompra (tipoDC) values ('FACTURA')
insert TTipoDocCompra (tipoDC) values ('BOLETA')
GO
-- select * from TTipoDocCompra
insert TBanco (banco) values ('')  --   1  --sin banco
insert TBanco (banco) values ('BCP')  --   2
insert TBanco (banco) values ('BBVA')  --  3
GO
--select * from TBanco
insert TCuentaBan (nroCue,codMon,codBan,estado) values ('',30,1,1)  --sin cuenta  1
insert TCuentaBan (nroCue,codMon,codBan,estado) values ('285-0028323-0-77',30,2,1)  --1=activo   2
insert TCuentaBan (nroCue,codMon,codBan,estado) values ('285-0028321-1-67',35,2,1)  --1=activo   
insert TCuentaBan (nroCue,codMon,codBan,estado) values ('0011-0204-0100006048-51',30,3,1)  --1=activo   3
insert TCuentaBan (nroCue,codMon,codBan,estado) values ('0011-0204-0100003771-59',35,3,1)  --1=activo    4
GO
--select * from TCuentaBan

insert TEmpTransp (nombre,ruc,dir,fono,contacto) values ('','','','') --1 para sin transporte
GO
--select * from TEmpTransp

insert TClasifPago (clasif) values ('') --1 sin clasificacion
insert TClasifPago (clasif) values ('PROVEEDORES') --2
insert TClasifPago (clasif) values ('HABERES') --3
insert TClasifPago (clasif) values ('CTS') --4
GO
-- select * from TClasifPago
insert TTipoTransac (tipo) values ('INGRESO') --1
insert TTipoTransac (tipo) values ('SALIDA') --2
insert TTipoTransac (tipo) values ('INGRESO OBRA') --3
insert TTipoTransac (tipo) values ('SALIDA PERS') --4
GO
--select * from TTipoTransac
insert TTipoDocEmp(tipoDE,estado) values ('FACTURA',1)--70
insert TTipoDocEmp(tipoDE,estado) values ('GUIA DE REMISION',1)--75
GO
-- select * from TTipoDocEmp
insert TSerieSede(serie,iniNroDoc,finNroDoc,descrip,estado,codTipDE) values ('001',1,99999,'SERIE GUIA PROVEEDOR',1,75)  --codSerS=1
insert TSerieSede(serie,iniNroDoc,finNroDoc,descrip,estado,codTipDE) values ('001',1,99999,'SERIE FACTURA PROVEEDOR',1,70) --codSerS=2
GO
-- select * from TSerieSede
--UPDATE TTipoTransac SET TIPO='SALIDA' WHERE CODTRANS=2
--UPDATE TTipoTransac SET TIPO='INGRESO' WHERE CODTRANS=1

insert TTipoMovCaja(tipoMov) values ('INGRESO')--1
insert TTipoMovCaja(tipoMov) values ('EGRESO')--2
GO
--select * from TTipoMovCaja

--*****************************************************
--------------------EJECUTAR 04/10/2013----------------------
--*****************************************************
insert TTipoMovimiento(tipoMov) values ('EGRESO')--1
insert TTipoMovimiento(tipoMov) values ('INGRESO')--2
GO
--select * from TTipoMovimiento
