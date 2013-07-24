--Inserta un insumo en la tabla material
create procedure PA_InsertTMaterial
	@prod varchar(100),@codU1 int,@pre1 decimal(8,2),@est int,@codA int,@codT int,@hist varchar(500),
	@Identity int output --parametro de salida
as
	insert into TMaterial(material,codUni,preBase,estado,codAreaM,codTipM,hist) 
		values(@prod,@codU1,@pre1,@est,@codA,@codT,@hist)	
	SET @Identity = @@Identity
	
	RETURN  @Identity
GO
--inserta un ítem en la tabla detalleSol, Solicitud de requerimiento
create procedure PA_InsertDetalleSol
	@pri varchar(20),@can decimal(8,2),@des varchar(100),@uni varchar(20),@codP int,@obs1 varchar(100),@codP1 int,
	@codE int,@obs2 varchar(200),@codM int,@codA int,@idS int,
	@Identity int output --parametro de salida
as
	insert into TDetalleSol(prioridad,cant,descrip,unidad,codPers,obs1,codPersA,codEstS,obs2,codMat,codAreaM,idSol) 
		values(@pri,@can,@des,@uni,@codP,@obs1,@codP1,@codE,@obs2,@codM,@codA,@idS)	
	SET @Identity = @@Identity
	
	RETURN  @Identity
GO
-- inserta un registro en la tabla Grupo, para cotizaciones
create procedure PA_InsertTGrupo
	@nro int,@des varchar(40),@est int,
	@Identity int output --parametro de salida
as
	insert into TGrupoCot(nroGru,descrip,estGru) values(@nro,@des,@est)	
	SET @Identity = @@Identity
	
	RETURN  @Identity
GO
--inserta un registro de cotización
create procedure PA_InsertTCotizacion
	@nro int,@codI int,@fec date,@tie varchar(20),@ate varchar(40),@pla varchar(40),@codPa int,@lug varchar(100),@inc varchar(100),
	@sol int,@cod varchar(10),@codP int,@obs varchar(200),@idS int,@codG int,@codMon int,
	@Identity int output --parametro de salida
as
	insert into TCotizacion(nroCot,codIde,fecCot,tiempoVig,atencion,plazo,codPag,lugarEnt,incluir,codPersS,codigo,codPers,obs,idSol,codGruC,codMon) 
		values(@nro,@codI,@fec,@tie,@ate,@pla,@codPa,@lug,@inc,@sol,@cod,@codP,@obs,@idS,@codG,@codMon) 
	
	SET @Identity = @@Identity
	RETURN  @Identity
GO
-- inserta un ítem en la tabla DetalleCot. Cotización
create procedure PA_InsertDetalleCot
	@can decimal(8,2),@uni varchar(20),@des varchar(100),@pre decimal(8,2),@sub decimal(8,2),@codC int,@codM int,@est int,
	@Identity int output --parametro de salida
as
	insert into TDetalleCot(cant,unidad,descrip,precio,subTotal,codCot,codMat,estado) 
		values(@can,@uni,@des,@pre,@sub,@codC,@codM,@est)	
	SET @Identity = @@Identity
	
	RETURN  @Identity
GO

-- inserta un ítem en la tabla detalleOrden. Orden de compra

create procedure PA_InsertDetalleOrden
	@can decimal(8,2),@uni varchar(20),@des varchar(100),@pre decimal(8,2),@sub decimal(8,2),@codM int,@nro int,
	@Identity int output --parametro de salida
as
	insert into TDetalleOrden(cant,unidad,descrip,precio,subTotal,codMat,nroOrden) 
		values(@can,@uni,@des,@pre,@sub,@codM,@nro)	
	SET @Identity = @@Identity
	
	RETURN  @Identity
GO

--inserta un registro en la tabala OrdenDesembolso. 
--DROP procedure PA_InsertOrdenDesembolso
create procedure PA_InsertOrdenDesembolso
	@ser varchar(5),@nroD int,@fecD date,@codMon int,@mon decimal(10,2),@mon1 decimal(8,2),@mon2 decimal(10,2),@est int,@cod varchar(10),@codIde int,
	@ban varchar(60),@nroC varchar(50),@nroDE varchar(30),@dato varchar(100),@fact int,@bol int,@guia int,@vou int,
	@vouD int,@reci int,@otro int,@des varchar(60),@nroCF varchar(30),@fec varchar(10),@hist varchar(200),@codSerO int,
	@Identity int output
as
	insert into TOrdenDesembolso(serie,nroDes,fecDes,codMon,monto,montoDet,montoDif,estado,codigo,codIde,banco,nroCta,nroDet,datoReq,factCheck,bolCheck,guiaCheck,vouCheck,vouDCheck,reciCheck,otroCheck,descOtro,nroConfor,fecEnt,hist,codSerO)
		values (@ser,@nroD,@fecD,@codMon,@mon,@mon1,@mon2,@est,@cod,@codIde,@ban,@nroC,@nroDE,@dato,@fact,@bol,@guia,@vou,@vouD,@reci,@otro,@des,@nroCF,@fec,@hist,@codSerO)

	 SET @Identity=@@IDENTITY 
	 Return @Identity
GO

create procedure PA_InsertTPagoDesembolso
	@fec date,@codT int,@nro varchar(20),@pago varchar(100),@codC int,@codM int,@monto decimal(10,2),@montoD decimal(10,2),@idOP int,@idC int,
	@Identity int output --parametro de salida
as
	insert into TPagoDesembolso(fecPago,codTipP,nroP,pagoDet,codCla,codMon,montoPago,montoD,idOP,idCue) 
		values(@fec,@codT,@nro,@pago,@codC,@codM,@monto,@montoD,@idOP,@idC)	
	SET @Identity = @@Identity
	
	RETURN  @Identity
GO

create procedure PA_InsertOrdenCompra
	@nroO int,@fecO date, @codIde int, @codPers int,@codPag int, @igv decimal(6,2),@calIGV int,@codMon int,
	@atiendeCom varchar(50),@cel varchar(50),@plazoEnt varchar(40),@transfe varchar(100),@nroProf varchar(40),@obsFac varchar(200), @estado int,
	@codCot int,@idSol int,@codPersO int, @codigo varchar(10),@lugar varchar(100), @hist varchar(200),@codET int,@nota varchar(200),
	@Identity int output
as
	insert into TOrdenCompra(nroO,fecOrden,codIde,codPers,codPag,igv,calIGV,codMon,atiendeCom,celAti,plazoEnt,transfe,nroProf,obsFac,estado,codCot,idSol,codPersO,codigo,lugarEnt,hist,codET,nota)
	 values (@nroO,@fecO,@codIde,@codPers,@codPag,@igv,@calIGV,@codMon,@atiendeCom,@cel,@plazoEnt,@transfe,@nroProf,@obsFac,@estado,@codCot,@idSol,@codPersO,@codigo,@lugar,@hist,@codET,@nota)
	 SET @Identity=@@IDENTITY 
	 Return @Identity
go
--------EJECUTADOS CR-----------------
-----------23/07/2013-----------------------
-- PAra consultar los seguimientos
create proc PA_SeguimientoDesembolso
as
Select idOP,serie,nroDes,nro,fecDes,estado_desembolso,hist,monto,montoDet,montoDif,obra,proveedor,
banco,nroCta,nroDet,datoReq,factCheck,bolCheck,guiaCheck,vouCheck,vouDCheck,reciCheck,otroCheck,
descOtro,nroConfor,fecEnt,moneda,simbolo,solicitante,ruc,fono,email,codObra,codIde 
from VOrdenDesembolsoSeguimiento
go

--Para recuperar las ordenes de compra
create proc PA_RecuperarOrdenCompra
@idDesembolso int
as
select nroOrden from TDesOrden where idOp=@idDesembolso
go

--Para consultar los Pagos de Desembolsos
create proc PA_SeguimientoPagos
as
Select codDesembolso,fecPago,montoPago,tipoP,moneda,simbolo,nroCue,banco,pagoDet,montoD,nroP,clasif 
from VPagoDesembolsoSeguimiento
go

--Para consultar los Comprobantes registrados

create proc PA_SeguimientoComprobantes
as
select idOP,fecEnt,nroConfor  from TOrdenDesembolso
go

-- Para Consultar los estados de Aprobacion
create proc PA_SeguimientoAprobaciones
as
select idOp,nombre,apellido,Area,Estado,ObserDesem,fecFir 
from VAprobacionesSeguimiento
go

--PAra consultar los lugares de trabajo
create proc PA_LugarTrabajo
as
Select codigo,nombre from tLugarTrabajo
go
--Para consultar los proveedores
create proc PA_Proveedores
as
Select codIde,razon from TIdentidad where idTipId=2
go



--------EJECUTAR AHORITA-----------------
-----------23/07/2013-----------------------

create procedure PA_InsertTMatUbi
	@codMat int,@codUbi int,@stock decimal(10,2),
	@Identity int output --parametro de salida
as
	insert into TMatUbi(codMat,codUbi,stock) values(@codMat,@codUbi,@stock)	
	SET @Identity = @@Identity
	
	RETURN  @Identity
GO

create procedure PA_InsertTSaldo 
	@sal decimal(10,2),@codS varchar(10),
	@Identity int output --parametro de salida
as
	insert into TSaldo(saldo,codLug) values(@sal,@codS)	
	SET @Identity = @@Identity
	
	RETURN  @Identity
GO

create procedure PA_InsertTEntradaSalida	---ENTRADAS y Salida
	@fecha date,@codMat int,@idMU int,@codUbi int,@can1 decimal(8,2),@valor1 decimal(8,2),@can2 decimal(8,2),@valor2 decimal(8,2),
	@codGuia int,@nroGuia varchar(30),@codDoc int,@nroDoc varchar(30),@otroDoc varchar(30),@codTrans int,@codUsu int,@codPers int,@obs varchar(200),@codSal int,
	@Identity int output --parametro de salida
as
	insert into TEntradaSalida(fecha,codMat,idMU,codUbi,cantEnt,preUniEnt,cantSal,preUniSal,codGuia,nroGuia,codDoc,nroDoc,otroDoc,codTrans,codUsu,codPers,obs,codSal) 
				values(@fecha,@codMat,@idMU,@codUbi,@can1,@valor1,@can2,@valor2,@codGuia,@nroGuia,@codDoc,@nroDoc,@otroDoc,@codTrans,@codUsu,@codPers,@obs,@codSal)
	SET @Identity = @@Identity
	
	RETURN  @Identity
GO


























































