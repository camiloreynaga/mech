
--********************************************************
--Autor: CR
--Descripcion: muestra los datos de reporte de contabilidad para 
-- documentos de venta
--FechaCreaci�n/Actualizaci�n: 04/12/13 CR
--*********************************************************
create view vReporteVentasConta
as
select 
	TDV.codDocV,
	TDV.fecDoc, --Fecha de Emisi�n
	TDV.fecCan, --Fecha de Cancelaci�n
	--codigo domumento
	'01' as tipoDoc,
	'FACTURA' as doc,
	TDV.serie, --serie 
	TDV.nroDoc, --N�mero de Documento
	TDV.calIGV,--tipo de calculo de igv 0
	TDV.estado, -- estado de documento 0=Abierto,1=cobrado,2=Anulado
	TDV.codMon,-- FK c�digo de moneda
	TDV.codIde, --Fk de cliente
	TDV.codigo, -- FK de Obra
	TL.nombre, -- nombre del lugar de trabajo
	TI.ruc,--ruc del cliente
	TI.razon,--nombre del cliente
	VPV.PV, --precio de venta
	TDV.codSers, --cod de la serie del documetno
	TMO.simbolo --Simbolo de la moneda usada
from TDocVenta TDV
inner join TLugarTrabajo TL on TL.codigo =TDV.codigo 
inner join TIdentidad TI on TI.codIde =TDV.codIde 
inner join vPrecioVenta VPV on VPV.codDocV=TDV.codDocV 
inner join TMoneda TMO on TMO.codMon  = TDV.codMon 


--********************************************************
--Autor: CR
--Descripcion: Obteniendo el precio de venta por cada factura emitida 
--agrupado por codigo de documento
--FechaCreaci�n/Actualizaci�n: 04/12/13 CR
--*********************************************************

create view  vPrecioVenta
as
select  
	sum((cant*preUni)) as PV, --precio total de venta
	codDocV -- FK codigo de docVenta
from TDetalleVenta group by codDocV  

--consulta para windowsForm
select codDocV,nombre,fecDoc,fecCan,tipoDoc,doc,serie,nroDoc,ruc,razon,PV,codIde,codigo  from vReporteVentasConta 

--****************************
--Reporte para compras
--********************************************************
--Autor: CR
--Descripcion: muestra los datos de reporte de compras para contabilidad 

--FechaCreaci�n/Actualizaci�n: 04/12/13 CR
--*********************************************************
create view vReporteComprasConta
as
select
	 TOD.idOP,
	 '' as fechaEmision, --fecha de emisi�n de la factura 
	 '' as fechaVenc, --fecha de vencimiento
	 Case when TOD.factCheck =1 then 'FACTURA' 
		 when TOD.bolCheck =1 and TOD.factCheck=0 then 'BOLETA'
		 when TOD.otroCheck =1 and TOD.bolCheck =0 and TOD.factCheck =0 then descOtro 
		 else 'OTRO'
	 end  Doc, --Descripci�n de comprobante de pago
	 Case when TOD.factCheck =1 then '01' 
		 when TOD.bolCheck =1 and TOD.factCheck=0 then '03'
		 when TOD.otroCheck =1 and TOD.bolCheck =0 and TOD.factCheck =0 then '' 
		 else ''
	 end  codigoDoc, --codigo contable para el comprobante de pago
	 TOD.nroConfor,  --n�mero de documentos de compra 
	 
	 Case when len(REPLACE(LEFT(TOD.nroConfor,CHARINDEX('-',TOD.nroConfor)),'-',''))=1  
		then '000'+REPLACE(LEFT(TOD.nroConfor,CHARINDEX('-',TOD.nroConfor)),'-','') 
		when len(REPLACE(LEFT(TOD.nroConfor,CHARINDEX('-',TOD.nroConfor)),'-',''))=2  
		then '00'+REPLACE(LEFT(TOD.nroConfor,CHARINDEX('-',TOD.nroConfor)),'-','')
		when len(REPLACE(LEFT(TOD.nroConfor,CHARINDEX('-',TOD.nroConfor)),'-',''))=3  
		then '0'+REPLACE(LEFT(TOD.nroConfor,CHARINDEX('-',TOD.nroConfor)),'-','')
		else ''
	 end serie,
	 SUBSTRING(TOD.nroConfor,CHARINDEX('-',TOD.nroConfor)+1,LEN(TOD.nroConfor)) nroFact,
	 --CASE when   
	 
	 --end
	 TID.ruc,  --ruc del proveedor
	 TID.razon,  --razon social del proveedor
	 '' codGravado,
	 '' gravado,
	 --Datos de monto pagado
	 TOD.monto, --monto compra 
	 TMO.simbolo, --simbolo de moneda
	 TPD.montoPago, --monto pagado por tesoreria
	 '' percep1, --Percepci�n / de pago emitido 
	 '' percep2, --Percepci�n /Monto
	 '' fechaEmiD, --Detracci�n / fecha de emisi�n
	 '' codigoD, --Detracci�n / c�digo
	 '' nroD, --Detracci�n /N�
	 montoD,--Detracci�n, Monto detracci�n
	 '' codCuenta, --CTA, codigo contable
	 '' descrCuenta, --CTA,Descripci�n de la cuenta
	 TMP.nro codTipoPago, -- codigo de medio de pago
	 TMP.tipoP tipoPago, --descripci�n del tipo de pago
	 TPD.nroP, -- nro de operaci�n
	 TPD.fecPago, --Fecha de pago
	 '' codigoCuenta, -- codigo de cuenta bancaria
	 TOD.nroCta, --n�mero de cuenta bancaria origen
	 TOD.codIde -- codigo de proveedor
from TPagoDesembolso TPD
inner join TOrdenDesembolso TOD on TOD.idOP=TPD.idOP  
inner join TIdentidad TID on TID.codIde = TOD.codIde 
inner join TTipoPago TMP on TMP.codTipP= TPD.codTipP
inner join TMoneda TMO on TMO.codMon = TPD.codMon  

--consulta para windowsForm

select idOP,fechaEmision,fechaVenc,Doc,codigoDoc,serie,nroFact,ruc,razon,codGravado,gravado,monto,simbolo,montoPago,percep1,percep2,fechaEmiD,codigoD,nroD,montoD,codCuenta,descrCuenta,codTipoPago,tipoPago,nroP,fecPago,codigoCuenta,nroCta,codIde from vReporteComprasConta

select * from TOrdenDesembolso 

select * from TPagoDesembolso 
select * from TTipoPago