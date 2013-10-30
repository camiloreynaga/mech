
--muestra la relacion de orden de desembolso con ls aprobaciones de las diferentes 
--áreas 

--********************************************************
--creacion de vista intermedias
--********************************************************

-- obteniendo las Id de las ordenes de desembolso pendientes
create view vDesembolsoFirmaSolicitante
as
select codPers,idOP,tipoA from TPersDesem where tipoa =1 

-- obteniendo las Id de las ordenes de desembolso aprobadas por gerencia
create view vDesembolsosFirmaGerencia
as
select codPers,idOP,tipoA,estDesem from TPersDesem where tipoA =2 

-- obteniendo las Id de las ordenes de desembolso pagadas por Tesoreria
create view vDesembolsosFirmaTesoreria
as
select codPers,idOP,tipoA,estDesem from TPersDesem where tipoa =3 

--order by idOP asc  

-- obteniendo las Id de las ordenes de desembolso entregadas a contabilidad
create view vDesembolsosFirmaContabilidad
as
select codPers,idOP,tipoA,estDesem  from TPersDesem where tipoa =4
 

-- vista para ver las firmas de cada área
create view vDesembolsosFirma
as
select VFS.idOP, VFS.tipoA firmaSolicitante,VFG.estDesem estado_Gere,VFG.tipoA firmaGerencia ,VFT.estDesem estado_Teso, VFT.tipoA firmaTesoreria,VFC.estDesem estado_Conta, VFC.tipoA firmaContabilidad    
from vDesembolsoFirmaSolicitante VFS
left join  vDesembolsosFirmaGerencia VFG on  VFS.idOP=VFG.idOP
left join vDesembolsosFirmaTesoreria VFT on VFS.idOP=VFT.idOP 
left join vDesembolsosFirmaContabilidad VFC on VFS.idOP=VFC.idOP    

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
--Condicional para pagos pendientes 
--where firmaTesoreria is null and firmaGerencia =2
-- condicional para facturas pendientes
--where firmaContabilidad is null --verifica si contabilidad 
--and VOF.estado_Gere=1

select fecDes,idOP,serie,nroDes,simbolo,monto,montoPagado,diferencia,montoDet,pagoDetraccion,diferenciaDetra,proveedor,datoReq,obra,solicitante,codIde from vseguimientoDesembolsoPagos where firmaTesoreria is null and firmaGerencia =2 and estado_gere=1

select fecDes,idOP,serie,nroDes,monto,montoPagado,diferencia,montoDet,pagoDetraccion,diferenciaDetra,simbolo,obra,datoReq,solicitante from vseguimientoDesembolsoPagos where 


select * from Vordendesembolsoseguimiento


select * from vDesembolsosFirma
where firmaTesoreria is null and firmaGerencia =2

select * from Vordendesembolsoseguimiento


select * from VOrdenDesembolso 

select vod.IdOP,isnull(SUM(tpa.montoPago),0.00) as  pago 
from VOrdenDesembolsoSeguimiento VOD
LEFT join TPagoDesembolso TPA on tpa.idOP =vod.idOP 
group by vod.IdOP





--consulta para mostrar los aprobadores, solicitante, gerencia, tesoreria, etc
 
select idOp,nombre,apellido,Area,Estado,ObserDesem,fecFir from VAprobacionesSeguimiento

