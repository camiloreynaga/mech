CREATE VIEW VCajaChica
As
SELECT tcc.codCC,tcc.fechaCre ,tcc.codMon,tcc.saldo,tcc.codigo --Obra
,tcc.codPers ,tcc.estCaja codEstado,case when tcc.estCaja =1 then 'Activo' else 'Inactivo' end as estado,
TLU.nombre  as obra,(TPE.nombre + ' '+ TPE.apellido) as responsable,TMO.moneda ,TMO.simbolo   

from TCajaChica TCC 
INNER JOIN TLugarTrabajo TLU on TLU.codigo=TCC.codigo
INNER JOIN TPersonal TPE on TPE.codPers=TCC.codPers
INNER JOIN TMoneda TMO on Tmo.codMon = TCC.codMon
GO


Select codCC,fechaCre,simbolo,codMon,saldo,codigo,obra,codPers,responsable,estado 
from VCajaChica

