create  view VSeguimientoGR
as
select TGR.codGuiaE,TGR.talon,TGR.nroGuia,TGR.fecIni,TGR.codSerS,tid.razon, TGR.codIde,
TGR.estado codestado, 'Estado'=case when TGR.estado=0 then 'ABIERTO' when TGR.estado=1 then 'TERMINADO' when TGR.estado=2 
then 'CERRADO' else 'ANULADO' end,
tu.ubicacion Origen,tu2.ubicacion Destino ,  TGR.codUbiOri,TGR.codUbiDes,
TGR.partida,TGR.llegada,TGR.codET, TET.nombre empTrans,TVE.marcaNro, TGR.codVeh, 
TCO.nombre, TGR.codT, TMO.motivo,  TGR.codMotG,TGR.nroFact,TGR.obs,
(TPE.nombre + TPE.apellido) as Personal, TGR.codPers, TID.ruc ,TSE.serie  
from TGuiaRemEmp TGR
inner join TUbicacion TU on tu.codUbi =TGR.codUbiOri 
inner join TUbicacion TU2 on tu2.codUbi=TGR.codUbiDes 
inner join TIdentidad TID on Tid.codIde = Tgr.codIde 
inner join TEmpTransp TET on TET.codET = TGR.codET 
inner join TVehiculo TVE on TVE.codVeh = TGR.codVeh 
inner join TTransportista TCO on TCO.codT = TGR.codT 
inner join TMotivoGuia TMO on TMO.codMotG =TGR.codMotG 
inner join TPersonal TPE on TPE.codPers = tgr.codPers 
inner join TSerieSede  TSE on TSE.codSerS = TGR.codSerS  
inner join TTipoDocEmp TTD on TTD.codTipDE=TSE.codTipDE 
where TSE.codSerS>1 and TTD.codTipDE =75
go
select * from TGuiaRemEmp
--inner join TLugarTrabajo TLU on TLU.codigo = TDEGR.codigo 
--inner join TMaterial TMA on Tma.codMat = TDEGR.codMat 
create view VSeguimientoGRDetalle
as
select TDEGR.codDGE,  TDEGR.codigo, TDEGR.cant, TDEGR.descrip, TDEGR.unidad, TDEGR.peso, TDEGR.codGuiaE,
TDEGR.codMat, TDEGR.linea1,TDEGR.entregado codEnt, 
'entregado' = case when TDEGR.entregado=0 then 'PENDIENTE' else 'ENTREGADO' end,
TDEGR.codPers  ,isnull((TPE.nombre +' '+ TPE.apellido),'') personal,  TDEGR.recibido codRecib,
'recibido'=case when TDEGR.recibido=0 then 'PENDIENTE' when TDEGR.recibido=1 then 'RECIBIDO' else 'INCOMPLETO' end,
TDEGR.obsR   
FROM TDetalleGuiaEmp TDEGR
left join TPersonal TPE on TPE.codPers=TDEGR.codPers    
--
go   

create view VSeguimientoGRSerie
as
select codSerS,serie 
from TSerieSede TSE
inner join TTipoDocEmp TTD on TTD.codTipDE = TSE.codTipDE 
where TTD.codTipDE =75 and TSE.codSerS >1



select * from ttipodocemp

select * from TSerieSede 

select * from tpersonal
select * from TDetalleGuiaEmp 
select * from TGuiaRemEmp 

update TGuiaRemEmp set estado=1 where nroGuia =830

update TDetalleGuiaEmp set entregado =1 where codGuiaE =3

