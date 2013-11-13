select montoPago from TPagoDesembolso where codmon=30

select codPagD,fecPago,codTipP,nroP,pagoDet,codCla,codMon,montoPago,montoD, (montoPago+montoD) as montoTotal,idOP, case when codMon = 30 then (montoPago+montoD) else 0 end 'MontoSoles',case when codMon = 35 then (montoPago+montoD) else 0 end 'MontoDolares' from TPagoDesembolso 

