
--********************************************************
--Autor: CR
--Descripcion:vista para facturas entregadas
-- Gerencia,Tesorería,Contabilidad
--FechaCreación/Actualización: 19/11/13 CR 
--*********************************************************

--Consolidado de ordenes de desembolos por factura. 
create view vFacturaOrdenDesembolso
as
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
where TOD.factCheck=1 and VFC.estDesem =1 

select idOp,serie,nroDes,fecDes,simbolo,monto,montoDet, nombre,razon,concepto,nroConfor,fecEnt,aprobador,codObra,codProv from vFacturaOrdenDesembolso 

select * from vFacturaOrdenDesembolso

and TOD.fecDes Between '01/10/2013' and '31/10/2013'

---
-- comprobantes pendientes de entrega

--********************************************************
--Autor: CR
--Descripcion:vista para facturas pendientes de entrega
-- Gerencia,Tesorería,Contabilidad
--FechaCreación/Actualización: 19/11/13 CR 
--*********************************************************

create view vFacturasPendientes
as
select TOD.idOP,TOD.serie,TOD.nroDes,TOD.fecDes,
	--TM.simbolo,
	TOD.codMon,TOD.monto,TOD.montoDet,TOD.estado estadoDesemboslo,
	TOD.codigo codObra,
	--TLU.nombre,
	TOD.codIde codProv,
	--TID.razon ,
	TOD.datoReq concepto, TOD.factCheck,TOD.bolCheck,TOD.reciCheck,tod.vouCheck,
	TOD.nroConfor,TOD.fecEnt,
    VFC.estDesem,VFC.codPers,(TPE.nombre +' '+ TPE.apellido) as Aprobador
	
from TOrdenDesembolso TOD  
	inner join vDesembolsosFirmaGerencia VFG on VFG.idOP = TOD.idOP 
	left join vDesembolsosFirmaContabilidad VFC on VFC.idOP = TOD.idOP
	inner join vDesembolsoFirmaSolicitante VFS on VFS.idOP =TOD.idOP  
	inner join TPersonal TPE on TPE.codPers = VFS.codPers
where TOD.factCheck=1 and VFC.estDesem is null 

and fecDes Between '01/10/2013' and '31/10/2013'

