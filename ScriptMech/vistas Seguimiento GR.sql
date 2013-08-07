create  view VSeguimientoGR
as
select TGR.codGuiaE,TGR.talon,TGR.nroGuia,TGR.fecIni,TGR.codSerS,tid.razon, TGR.codIde,
TGR.estado codestado, 'Estado'=case when TGR.estado=0 then 'PENDIENTE' when TGR.estado=1 then 'CERRADO' else 'ANULADO' end,
tu.ubicacion Origen,tu2.ubicacion Destino ,  TGR.codUbiOri,TGR.codUbiDes,
TGR.partida,TGR.llegada,TGR.codET, TET.nombre empTrans,TVE.marcaNro, TGR.codVeh, 
TCO.nombre, TGR.codT, TMO.motivo,  TGR.codMotG,TGR.nroFact,TGR.obs,
(TPE.nombre + TPE.apellido) as Personal, TGR.codPers, TID.ruc  
from TGuiaRemEmp TGR
inner join TUbicacion TU on tu.codUbi =TGR.codUbiOri 
inner join TUbicacion TU2 on tu2.codUbi=TGR.codUbiDes 
inner join TIdentidad TID on Tid.codIde = Tgr.codIde 
inner join TEmpTransp TET on TET.codET = TGR.codET 
inner join TVehiculo TVE on TVE.codVeh = TGR.codVeh 
inner join TTransportista TCO on TCO.codT = TGR.codT 
inner join TMotivoGuia TMO on TMO.codMotG =TGR.codMotG 
inner join TPersonal TPE on TPE.codPers = tgr.codPers 
go




CREATE view VSeguimientoGRDetalle
as
select TDEGR.codDGE, TDEGR.codigo, TDEGR.cant, TDEGR.descrip, TDEGR.unidad, TDEGR.peso, TDEGR.codGuiaE,
TDEGR.codMat, TDEGR.linea1 
FROM TDetalleGuiaEmp TDEGR
--inner join TLugarTrabajo TLU on TLU.codigo = TDEGR.codigo 
--inner join TMaterial TMA on Tma.codMat = TDEGR.codMat 
go   
select * from TDetalleGuiaEmp 

select TDEGR.codDGE,  TDEGR.codigo, TDEGR.cant, TDEGR.descrip, TDEGR.unidad, TDEGR.peso, TDEGR.codGuiaE,
TDEGR.codMat, TDEGR.linea1 
FROM TDetalleGuiaEmp TDEGR

select * from TGuiaRemEmp 