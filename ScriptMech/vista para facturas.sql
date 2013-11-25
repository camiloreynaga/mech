select fecDes,idOP,serie,nroDes,simbolo,monto,montoPagado,diferencia,montoDet,pagoDetraccion,diferenciaDetra,proveedor,datoReq,obra,solicitante,codIde 
from vseguimientoDesembolsoPagos where firmaTesoreria is null and firmaGerencia =2 and estado_gere=1

create view vSeguimientoDesembolsoPagos
as

select VOD.fecDes, VOD.idOP, VOD.serie, VOD.nroDes,VOD.monto,isnull(VPD.montoPago,0.0)montoPagado,(VOD.monto - isnull(VPD.montoPago,0.0) ) diferencia 
,VOD.montoDet,isnull(VPDE.montoD,0.0) pagoDetraccion,(VOD.montoDet - isnull(VPDE.montoD,0.0)) diferenciaDetra,VOD.simbolo,
VOD.obra,VOD.datoReq,Vod.solicitante,vof.estado_gere,vof.firmaSolicitante ,VOF.firmaGerencia,VOF.firmaTesoreria,VOF.firmaContabilidad ,VOD.proveedor,VOD.codIde,
VOD.codObra 
          
from Vordendesembolsoseguimiento VOD   
inner join vDesembolsosFirma VOF on VOF.idOP = VOD.idOP  
left join vPagosDesembolso VPD on VPD.idOP = VOD.idOP   
left join vPagoDetraccionDesembolso VPDE on VPDE.idOP = VOD.idOP



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
	where TPDE.tipoA=1	
GO
---
--consolidado facturas
 SET DATEFORMAT  dmy
select TOD.idOP,TOD.serie,TOD.nroDes,TOD.fecDes,TM.simbolo,TOD.codMon,TOD.monto,TOD.montoDet,TOD.estado estadoDesemboslo,
	TOD.codigo codObra,TLU.nombre,TOD.codIde codProv,TID.razon ,TOD.datoReq concepto,TOD.factCheck,TOD.bolCheck,TOD.reciCheck,tod.vouCheck,
	TOD.nroConfor,TOD.fecEnt,
    VFC.estDesem,VFC.codPers,(TPE.nombre +' '+ TPE.apellido) as Aprobador

from TOrdenDesembolso TOD  --where TOD.factCheck=1 and TOD.bolCheck =1 
	inner join vDesembolsosFirmaContabilidad VFC on VFC.idOP=TOD.idOp 
	join TMoneda TM on TOD.codMon=TM.codMon 
	join  TIdentidad TID on TID.codIde=TOD.codIde 
	join TLugarTrabajo TLU on tlu.codigo=TOD.codigo  
	join TPersonal TPE on TPE.codPers = VFC.codPers
where TOD.factCheck=1 and VFC.estDesem =1 and TOD.fecDes Between '01/09/2013' and '30/09/2013'

--facturas pendientes
create view 
select TOD.idOP,TOD.serie,TOD.nroDes,TOD.fecDes,
	TM.simbolo,
	TOD.codMon,TOD.monto,TOD.montoDet,TOD.estado estadoDesemboslo,
	TOD.codigo codObra,
	TLU.nombre,
	TOD.codIde codProv,
	TID.razon ,
	TOD.datoReq concepto, TOD.factCheck,TOD.bolCheck,TOD.reciCheck,tod.vouCheck,
	TOD.nroConfor,TOD.fecEnt,
    VFC.estDesem,VFC.codPers,(TPE.nombre +' '+ TPE.apellido) as Aprobador
	
from TOrdenDesembolso TOD  
	inner join vDesembolsosFirmaGerencia VFG on VFG.idOP = TOD.idOP 
	join TMoneda TM on TOD.codMon=TM.codMon 
	join  TIdentidad TID on TID.codIde=TOD.codIde 
	join TLugarTrabajo TLU on tlu.codigo=TOD.codigo
	left join vDesembolsosFirmaContabilidad VFC on VFC.idOP = TOD.idOP
	inner join vDesembolsoFirmaSolicitante VFS on VFS.idOP =TOD.idOP  
	inner join TPersonal TPE on TPE.codPers = VFS.codPers
	
	
where TOD.factCheck=1 and VFC.estDesem is null and fecDes Between '01/09/2013' and '30/09/2013'