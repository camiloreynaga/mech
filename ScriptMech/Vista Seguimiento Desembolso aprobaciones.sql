
--muestra la relacion de orden de desembolso con ls aprobaciones de las diferentes 
--áreas 
create view VOrdenDesembolsoSeguimientoAprobaciones
as
select TOD.idOP,tod.codigo as codObra,tod.serie,tod.nroDes,tod.fecDes,tod.monto,tod.montoDet,tod.montoDif,
'estado_desembolso'=case when TOD.estado=0 then 'PENDIENTE' when TOD.estado=1 then 'TERMINADO' when TOD.estado=2 then 'CERRADO' else 'ANULADO' end, 
	'nro'=case when tod.nroDes<100 then '000'+ltrim(str(tod.nroDes)) when tod.nroDes>=100 and tod.nroDes<1000 then '00'+ltrim(str(tod.nroDes)) else '0'+ltrim(str(tod.nroDes)) end,
	TM.codMon,TM.moneda,TM.simbolo,
	TLU.nombre as 'obra',Tid.razon as 'proveedor',tod.banco,tod.nroCta,tod.nroDet,tod.datoReq,tod.factCheck,tod.bolCheck,tod.guiaCheck,
	tod.vouCheck,tod.vouDCheck,tod.reciCheck,tod.otroCheck,tod.descOtro,tod.nroConfor,tod.fecEnt,tod.hist
	,(TPE.nombre +' '+ TPE.apellido) as solicitante,tid.codIde  ,TID.ruc, TID.fono,TID.email
	--,toc.nroOrden as idCompra,toc.nroO as nroCompra     
	from TOrdenDesembolso TOD 
	join TMoneda TM on TOD.codMon=TM.codMon 
	join  TIdentidad TID on TID.codIde=tod.codIde 
	join TLugarTrabajo TLU on tlu.codigo=TOD.codigo  
	inner join TPersDesem TPDE on TPDE.idOP= TOD.idOP 
	join TPersonal TPE on TPE.codPers = TPDE.codPers  
	where tpde.tipoA=2
	
	--join TDesOrden TDECO on tdeco.idOP = tod.idOP  
	--join TOrdenCompra TOC on toc.nroOrden =tdeco.nroOrden   
GO

select TOD.idOP,TOD.serie,TOD.nroDes,codPersDes,codPers,tipoA      
from TOrdenDesembolso TOD 
inner join TPersDesem TPD on TPD.idOP = TOD.idOP where tipoa = 3


-- obteniendo las Id de las ordenes de desembolso cerradas
select codPers,idOP,tipoA from TPersDesem where tipoa =3 order by idOP asc  
-- obteniendo las Id de las ordenes de desembolso aprobadas por gerencia
select codPers,idOP,tipoA from TPersDesem where tipoA =2 order by idOP asc 

--obteniendo la suma de pagos para las ordenes de desembolso
create view vPagosDesembolso
as
select idOP ,sum(montoPago) montoPago from TPagoDesembolso where codMon=30 group by idOP 


--obteniendo la suma de pagos detracciones para las ordenes de desembolso

create view vPagoDetraccionDesembolso
as
select idOp, sum (montoD) montoD from TPagoDesembolso group by idOP 

--select idOP,montoPago  from TPagoDesembolso order by idOP 

--consulta que muestra 
select VOD.idOP, VOD.serie, VOD.nroDes,VOD.monto,isnull(VPD.montoPago,0.0)montoPagado,(VOD.monto - isnull(VPD.montoPago,0.0) ) diferencia 
,VOD.montoDet,isnull(VPDE.montoD,0.0) pagoDetraccion,(VOD.montoDet - isnull(VPDE.montoD,0.0)) diferenciaDetra ,VOD.estado,VOD.est,VOD.simbolo     
from VOrdenDesembolso VOD
left join vPagosDesembolso VPD on VPD.idOP = VOD.idOP   
left join vPagoDetraccionDesembolso VPDE on VPDE.idOP = VOD.idOP





select * from VOrdenDesembolso 

select vod.IdOP,isnull(SUM(tpa.montoPago),0.00) as  pago 
from VOrdenDesembolsoSeguimiento VOD
LEFT join TPagoDesembolso TPA on tpa.idOP =vod.idOP 
group by vod.IdOP



--consulta para mostrar los aprobadores, solicitante, gerencia, tesoreria, etc
 
select idOp,nombre,apellido,Area,Estado,ObserDesem,fecFir from VAprobacionesSeguimiento

