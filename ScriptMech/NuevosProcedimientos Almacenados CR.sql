-- PAra consultar los seguimientos
--Pendiente de actualización
create proc PA_SeguimientoDesembolso
@fechaInicio date,
@fechaFin date
as
Select idOP,serie,nroDes,nro,fecDes,estado_desembolso,hist,monto,montoDet,montoDif,obra,proveedor,
banco,nroCta,nroDet,datoReq,factCheck,bolCheck,guiaCheck,vouCheck,vouDCheck,reciCheck,otroCheck,
descOtro,nroConfor,fecEnt,moneda,simbolo,solicitante,ruc,fono,email,codObra,codIde 
from VOrdenDesembolsoSeguimiento where fecDes between @fechaInicio and @fechaFin 
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


--- caja chica

create procedure PA_InsertTCajaChica
@fechaCre date ,
@codigo varchar(20),
@codPers int,
@codSerie int,
@Identity int output 
as
	insert into TCajaChica(fechaCre,codigo,codPers,codSerO) values (@fechaCre,@codigo,@codPers,@codSerie)
	SET @Identity=@@IDENTITY 
	
	return @Identity
go