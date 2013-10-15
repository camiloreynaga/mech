
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
	--join TDesOrden TDECO on tdeco.idOP = tod.idOP  
	--join TOrdenCompra TOC on toc.nroOrden =tdeco.nroOrden   
GO
