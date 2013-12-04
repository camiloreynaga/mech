select * from TDocVenta

create  view vDocumentosVentas
as 
select TDV.codDocV,TDV.serie, TDV.nroDoc,TDV.fecDoc,TDV.fecCan,TDV.idSesM,TDV.codSerS,TDV.codIde,TDV.estado codEstado,
'estado'=case when TDV.estado=0 then 'ABIERTO' when TDV.estado=1 then 'COBRADO' else 'ANULADO' end,  

TDV.igv,TDV.calIGV,TDV.codMon,TDv.codigo,TIDE.razon,TIDE.ruc,TIDE.dir,TLU.nombre,TMO.simbolo,TDV.camD,TDV.obs          
from TDocVenta TDV
inner join TIdentidad TIDE on TIDE.codIde=TDV.codIde 
inner join TLugarTrabajo TLU on TLU.codigo = TDV.codigo
inner join TMoneda TMO on TMO.codMon = TDV.codMon

select * from TDocVenta

select codDocV,serie,nroDoc,fecDoc,ruc,razon,dir,codIde,codigo,nombre,estado,simbolo,camD,codSerS from vDocumentosVentas 


select * from TDetalleVenta

select codDV,cant,unidad,detalle,linea,simbolo, preUni from TDetalleVenta TD inner join vDocumentosVentas TDV on TDV.codDocV =  TD.codDocV where codDocV =


select codDV,cant,unidad,detalle,linea,preUni  from TDetalleVenta where codDocV =

 
select * from TSerieSede 





select * from TIdentidad



select codIde,razon from TIdentidad where idTipId =1 order by razon asc

select * from TSerieSede

select * from TTipoDocEmp 

select codSerS, serie from TSerieSede where codTipDE=70 

select 