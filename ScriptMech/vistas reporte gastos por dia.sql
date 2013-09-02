
create view VGastosPorDia
as
SELECT TPD.fecPago, TPD.nroP AS nroOperacion, TPD.pagoDet AS concepto, TTP.tipoP, TMO.simbolo,TMO.codMon, TPD.montoPago, TPD.montoD, TOD.serie, TOD.nroDes, 
TBCO.banco, TBCO.codBan, TCU.nroCue,TCU.idCue, TID.ruc, TID.razon,TOD.codigo, TL.nombre  
FROM mech.TPagoDesembolso AS TPD 
INNER JOIN mech.TCuentaBan AS TCU ON TCU.idCue = TPD.idCue
INNER JOIN mech.TBanco AS TBCO ON TBCO.codBan = TCU.codBan
INNER JOIN mech.TMoneda AS TMO ON TMO.codMon = TPD.codMon
INNER JOIN mech.TOrdenDesembolso AS TOD ON TOD.idOP = TPD.idOP
INNER JOIN mech.TIdentidad AS TID ON TID.codIde = TOD.codIde
INNER JOIN mech.TTipoPago AS TTP ON TTP.codTipP = TPD.codTipP
INNER JOIN mech.TLugarTrabajo TL ON TL.codigo = TOD.codigo  
go

select * from mech.TCuentaBan
