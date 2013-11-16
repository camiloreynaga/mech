select montoPago from TPagoDesembolso where codmon=30

select codPagD,fecPago,codTipP,nroP,pagoDet,codCla,codMon,montoPago,montoD, (montoPago+montoD) as montoTotal,idOP, case when codMon = 30 then (montoPago+montoD) else 0 end 'MontoSoles',case when codMon = 35 then (montoPago+montoD) else 0 end 'MontoDolares' from TPagoDesembolso 


--Vista de informe gastos por día 
-- Versión para gerencia
create view VGastosPorDia2
as
SELECT TPD.fecPago, TPD.nroP AS nroOperacion, TPD.pagoDet AS concepto, TTP.tipoP, TMO.simbolo,TMO.codMon, TPD.montoPago, TPD.montoD,
(TPD.montoPago+TPD.montoD) as montoTotal,case when TPD.codMon = 30 then (TPD.montoPago+TPD.montoD) else 0 end 'MontoSoles',case when TPD.codMon = 35 then (TPD.montoPago+TPD.montoD) else 0 end 'MontoDolares' 
,TOD.serie, TOD.nroDes,case when CHARINDEX(' ',TBCO.banco,3) >0 then left(TBCO.banco,CHARINDEX(' ',TBCO.banco,3))else TBCO.banco end bco, 
TBCO.banco, TBCO.codBan, TCU.nroCue,
case when TBCO.codBan in (3,4,5) then LEFT(RIGHT( replace(TCU.nroCue,'-',''),6),4) else RIGHT( replace(TCU.nroCue,'-',''),4) end longitud4, 
TCU.idCue, TID.ruc, TID.razon,TOD.codigo, TL.nombre, TPD.codTipCla, TPD.vanEgreso,TCLA.tipoClasif                       
FROM mech.TPagoDesembolso AS TPD 
INNER JOIN mech.TCuentaBan AS TCU ON TCU.idCue = TPD.idCue
INNER JOIN mech.TBanco AS TBCO ON TBCO.codBan = TCU.codBan
INNER JOIN mech.TMoneda AS TMO ON TMO.codMon = TPD.codMon
INNER JOIN mech.TOrdenDesembolso AS TOD ON TOD.idOP = TPD.idOP
INNER JOIN mech.TIdentidad AS TID ON TID.codIde = TOD.codIde
INNER JOIN mech.TTipoPago AS TTP ON TTP.codTipP = TPD.codTipP
INNER JOIN mech.TLugarTrabajo TL ON TL.codigo = TOD.codigo  
INNER JOIN mech.TTipoClasif AS TCLA ON TCLA.codTipCla = TPD.codTipCla
go