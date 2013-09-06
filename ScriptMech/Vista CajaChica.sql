create  VIEW VCajaChica
As
SELECT tcc.codCC,tcc.fechaCre ,tcc.codigo --Obra
,tcc.codPers,
TLU.nombre as obra,(TPE.nombre + ' '+ TPE.apellido) as responsable,
tcc.codSerO,tso.serie 
from TCajaChica TCC 
INNER JOIN TLugarTrabajo TLU on TLU.codigo=TCC.codigo
INNER JOIN TPersonal TPE on TPE.codPers=TCC.codPers
INNER JOIN TSerieOrden TSO on tso.codSerO = TCC.codSerO  
GO

select codCaj,caja,codMon,moneda,simbolo,saldo,codEstado,estado,codCC from VCajas

create view VCajas
as
SELECT TCS.codCaj,TCS.caja,tcs.codMon,TCS.saldo,TCS.estCaja codEstado,
case when tcs.estCaja =1 then 'Activo' else 'Inactivo' end as estado,
TCS.codCC,TMO.moneda ,TMO.simbolo   
from TCajas TCS 
INNER JOIN TCajaChica TCC on TCC.codCC = TCS.codCC 
INNER JOIN TMoneda TMO on Tmo.codMon = TCS.codMon
go



select isnull(max(nroSol),0)+1 from TSolicitudCaja where codCC=

select * from TSolicitud 


Select codCC,fechaCre,simbolo,codMon,saldo,codigo,obra,codPers,responsable,estado,codSerO
from VCajaChica

select * from TSerieOrden 

select * from tcajachica