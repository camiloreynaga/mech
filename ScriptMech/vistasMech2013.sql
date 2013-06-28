create view VLugarTrabajoLogin  --en varias interfaces  
as
	select TL.codigo,TL.nombre,TU.codUbi,TU.ubicacion,TU.estado,TL.color,TL.lugar 
	from TLugarTrabajo TL join TUbicacion TU on TL.codigo=TU.codigo
	where TL.estado=1  --1=Ejecucion
GO

create view VObra
as
	select TL.codigo,TL.nombre,TU.codUbi,TU.ubicacion,TU.estado,TU.color,TL.lugar 
	from TLugarTrabajo TL join TUbicacion TU on TL.codigo=TU.codigo
GO

create view VLugarTrabajo
as
	SELECT TL.codigo, TL.nombre
			FROM TLugarTrabajo TL JOIN TIdentidad TI ON TL.codIde = TI.codIde 
			JOIN TUbicacion TU ON TL.codigo = TU.codigo
go

create view VLugarUbicacion  
as
	select TNC.codigo,TNC.fecAper,TNC.nombre,TNC.lugar,TNC.tiempoMeta,TNC.tiempoContr,TNC.presupMeta,TNC.presupContr,TI.codIde,TI.razon,
	TI.ruc,TI.dir,TI.idTipId,case when TNC.estado=1 then 'Ejecucion' when TNC.estado=2 then 'Paralizado' else 'CULMINADO' end as estado,TNC.estado codEstado,TNC.color 
	 
	from TLugarTrabajo TNC join TIdentidad TI on TNC.codIde=TI.codIde 
GO

--vista VTipoIdentidad
create view VTipoIdentidad
as
	SELECT TI.codIde,TT.tipoId,TI.razon,TI.ruc,TI.dir,TI.fono,TI.fax,TI.celRpm,TI.email,TI.estado,TI.cuentaBan, 
			CASE WHEN TI.estado = 1 THEN 'Activo' ELSE 'Inactivo' END as estado1,TI.repres,TI.dni,TT.idTipId
			FROM TTipoIdent TT join TIdentidad TI ON TT.idTipId = TI.idTipId
GO 
--vista VPersonal
create view VPersonal
as
SELECT     TPe.codPers, TPe.usuario, TPe.codTipU, TPe.nombre, TPe.apellido, TPe.dni, 
                      TPe.dir, TPe.fono, TPe.email, TPe.estado, TCa.cargo, TTu.tipo, 
                      TPe.codCar, CASE WHEN TPe.estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS estado1, 
                      CASE WHEN TPe.usuario = '' THEN '' ELSE CASE WHEN TTu.tipoCargo = 0 THEN '*******' ELSE TPe.pass END END
                       AS password, CASE WHEN TTu.tipoCargo=0 THEN 'TODAS LAS OBRAS' else Tlu.nombre END as ubicacion, Tlu.codigo as codLugar  
FROM         TPersonal TPe INNER JOIN
                      TCargo TCa ON TPe.codCar = TCa.codCar LEFT JOIN
                      TTipoUsu TTu ON TPe.codTipU = TTu.codTipU LEFT JOIN 
                      TPersLugar TPL on TPL.codPers = Tpe.codPers LEFt JOIN
                      TLugarTrabajo TLu on TPL.codigo=Tlu.codigo 

GO
--
create view VMaterial
as
	select codMat,material,TU1.unidad as uniBase,preBase,estado,TT.codTipM,TT.tipoM,TM.codUni,
	'est'=case when estado=1 then 'Activo' else 'INACTIVO' end,TA.areaM,TA.codAreaM,TM.hist
	from TMaterial TM join TUnidad TU1 on TM.codUni=TU1.codUni
	join TTipoMat TT on TM.codTipM=TT.codTipM
	join TAreaMat TA on TM.codAreaM=TA.codAreaM
GO

create view VBusquedaMat
as
	select TT.codTipM,TT.tipoM,TM.codMat,TM.material,TM.estado
	from TTipoMat TT join TMaterial TM  on TT.codTipM=TM.codTipM
GO

create view VSolAper  
as	
	select TS.idSol,TS.nroS,TS.fecSol,'est'=case when TS.estado=0 then 'ABIERTO' else 'CERRADO' end,  --1=cerradp
	'nro'=case when TS.nroS<100 then '000'+ltrim(str(TS.nroS)) when TS.nroS>=100 and TS.nroS<1000 then '00'+ltrim(str(TS.nroS)) when TS.nroS>=1000 and TS.nroS<10000 then '0'+ltrim(str(TS.nroS)) else ltrim(str(TS.nroS)) end,
	TS.estado,TS.obs,TP.codPers,TP.nombre+' '+TP.apellido as nombres,TL.codigo,TL.nombre+' '+TL.lugar as lugar
	from TSolicitud TS join TLugarTrabajo TL on TS.codigo=TL.codigo
	join TPersonal TP on TS.codPers=TP.codPers
	--where TS.estado=0 --0=abierto 1=cerrado
GO

create view VDetSol  --Instanciado en varias interfaces NO TOCAR JUAN EMILIO NATURAL DE ULLPUTU
as
	select TD.codDetS,TD.prioridad,TD.cant,TD.descrip,TD.unidad,TD.obs1,TD.idSol,TP1.codPers,TP1.nombre+' '+TP1.apellido as nombres,
	TD.codPersA,isnull(TP2.nombre+' '+TP2.apellido,'') as nombres1,TD.obs2,TM.codMat,TM.material,TA.codAreaM,TA.areaM,TT.codTipM,TT.tipoM,TE.codEstS,TE.estSol
	from TDetalleSol TD join TPersonal TP1 on TD.codPers=TP1.codPers
	left join TPersonal TP2 on TD.codPersA=TP2.codPers
	join TEstSol TE on TD.codEstS=TE.codEstS
	join TMaterial TM on TD.codMat=TM.codMat
	join TAreaMat TA on TD.codAreaM=TA.codAreaM
	join TTipoMat TT on TM.codTipM=TT.codTipM	
GO

create view VMaterialSele
as
	select codMat,material,TU1.unidad as uniBase,preBase,estado,TT.codTipM,TT.tipoM,TM.codUni,TM.hist
	from TMaterial TM join TUnidad TU1 on TM.codUni=TU1.codUni
	join TTipoMat TT on TM.codTipM=TT.codTipM
	--join TAreaMat TA on TM.codAreaM=TA.codAreaM
	where TM.estado=1 --1 solo activados
GO

create View VSolTodo  --Instanciado en interfaces de solicitud
as	
	select TS.idSol,TS.nroS,TS.fecSol,'est'=case when TS.estado=0 then 'ABIERTO' else 'CERRADO' end,  --1=cerrado
	'nro'=case when TS.nroS<100 then '000'+ltrim(str(TS.nroS)) when TS.nroS>=100 and TS.nroS<1000 then '00'+ltrim(str(TS.nroS)) when TS.nroS>=1000 and TS.nroS<10000 then '0'+ltrim(str(TS.nroS)) else ltrim(str(TS.nroS)) end,
	TS.estado,TS.obs,TP.codPers,TP.nombre+' '+TP.apellido as nombres,TL.codigo,TL.nombre+' '+TL.lugar as lugar
	from TSolicitud TS join TLugarTrabajo TL on TS.codigo=TL.codigo
	join TPersonal TP on TS.codPers=TP.codPers
GO

create view VSolDetSol  --impresion
as
	select TS.idSol,TS.nroS,TS.fecSol,'est'=case when TS.estado=0 then 'ABIERTO' else 'CERRADO' end,  --1=cerrado
	'nro'=case when TS.nroS<100 then '000'+ltrim(str(TS.nroS)) when TS.nroS>=100 and TS.nroS<1000 then '00'+ltrim(str(TS.nroS)) when TS.nroS>=1000 and TS.nroS<10000 then '0'+ltrim(str(TS.nroS)) else ltrim(str(TS.nroS)) end,
	TD.codDetS,TD.prioridad,TD.cant,TD.descrip,TD.unidad,TD.obs1 as obsSol,TP3.codPers as codJefe,TP3.nombre+' '+TP3.apellido as jefe,
	TM.codMat,TM.material,TA.codAreaM,TA.areaM,TT.codTipM,TT.tipoM,TS.estado,TS.obs,TD.obs2 as obsApro,TE.codEstS,TE.estSol,
	TP1.codPers,TP1.nombre+' '+TP1.apellido as nombres,TD.codPersA,isnull(TP2.nombre+' '+TP2.apellido,'') as nombres1
	from TSolicitud TS join TPersonal TP1 on TS.codPers=TP1.codPers
	join TDetalleSol TD on TS.idSol=TD.idSol 
	join TPersonal TP3 on TD.codPers=TP3.codPers
	left join TPersonal TP2 on TD.codPersA=TP2.codPers
	join TAreaMat TA on TD.codAreaM=TA.codAreaM
	join TEstSol TE on TD.codEstS=TE.codEstS 
	join TMaterial TM on TD.codMat=TM.codMat
	join TTipoMat TT on TM.codTipM=TT.codTipM
GO

create view VCotAper  
as	
	select TC.codCot,TC.nroCot,TC.codIde,TC.fecCot,TC.tiempoVig,TC.atencion,TC.plazo,TC.codPag,TC.lugarEnt,TC.incluir,TC.codPersS,
	TC.codigo,TC.codPers,'0' as estado,TC.obs,TC.idSol,TG.codGruC,TG.nroGru,TG.descrip,TG.estGru,TG.descrip as grupo,TC.codMon,  
	'nro'=case when TC.nroCot<100 then '000'+ltrim(str(TC.nroCot)) when TC.nroCot>=100 and TC.nroCot<1000 then '00'+ltrim(str(TC.nroCot)) when TC.nroCot>=1000 and TC.nroCot<10000 then '0'+ltrim(str(TC.nroCot)) else ltrim(str(TC.nroCot)) end
	from TCotizacion TC join TGrupoCot TG on TC.codGruC=TG.codGruC	
	where TG.estGru=0 --0=abierto
GO

create view VDetCot  --Instanciado en varias interfaces
as
	select TD.codDetC,TD.cant,TD.descrip,TD.unidad,TD.precio,TD.subTotal,TD.codCot,TM.codMat,TM.material,TT.codTipM,TT.tipoM,
	'est'=case when TD.estado=0 then 'PENDIENTE' when TD.estado=1 then 'APROBADO' else 'RECHAZADO' end,TD.estado
	from TDetalleCot TD join TMaterial TM on TD.codMat=TM.codMat
	join TTipoMat TT on TM.codTipM=TT.codTipM	
GO

create view VOrdenComAper  
as	
	select nroOrden,nroO,codIde,fecOrden,codPers,codPag,igv,calIGV,codMon,atiendeCom,plazoEnt,celAti,lugarEnt,transfe,obsFac,codPersO,nroProf,idSol,
	codigo,estado,codCot,hist,'nro'=case when nroO<100 then '000'+ltrim(str(nroO)) when nroO>=100 and nroO<1000 then '00'+ltrim(str(nroO)) when nroO>=1000 and nroO<10000 then '0'+ltrim(str(nroO)) else ltrim(str(nroO)) end
	from TOrdenCompra where estado=0 --0=abierto
GO

create view VOrdenNro  
as
	select nroOrden,nroO,codCot,case when nroO<100 then '000'+ltrim(str(nroO)) when nroO>=100 and nroO<1000 then '00'+ltrim(str(nroO)) else ltrim(str(nroO)) end + '-' + ltrim(str(year(fecOrden))) as 'nro'
	from TOrdenCompra 
GO

CREATE FUNCTION FN_ConcaNroOrden(@cod int) RETURNS varchar(500)
BEGIN
	Declare @strConcatenado varchar(500)
	Select @strConcatenado = ''
	select @strConcatenado=@strConcatenado+nro+'  ' from VOrdenNro WHERE codCot=@cod
	--Select @strConcatenado
	RETURN @strConcatenado
END
GO

create view VDetOrden -- no modificar instanciado en varias interfaces
as
	select TD.codDetO,TD.cant,TD.descrip,TD.unidad,TD.precio,TD.subTotal,TD.nroOrden,TM.codMat,TM.material,TM.codTipM
	from TDetalleOrden TD join TMaterial TM on TD.codMat=TM.codMat
GO

create View VDocCompraAper
as 
	select codDocc,serie, nroDoc, fecDoc, fecCan, codTipDC,estado, codIde,codPag,igv,calIGV,codMon,camD,obs,hist from TDocCompra
Go

create view VDetDocCompra
as
	Select Td.codDC,TD.cant,TD.unidad,TD.preUni,TD.codDocC ,TM.codMat,TM.material,tm.codTipM From TDetalleCompra TD join TMaterial TM on td.codMat=tm.codMat 
Go

create view VCotOrden  
as	
	select TC.codCot,TC.nroCot,TC.codIde,TC.fecCot,TC.tiempoVig,TC.atencion,TC.plazo,TF.codPag,TF.forma,TC.lugarEnt,TC.incluir,TC.codPersS,TP1.nombre+' '+TP1.apellido as nom,--mech.FN_ConcaNroOrden(TC.codCot) as nroOrdenConca,
	TC.codigo,TC.codPers,'0' as estado,TC.obs,TC.idSol,TG.codGruC,TG.nroGru,TG.descrip,TG.estGru,TG.descrip as grupo,TP2.nombre+' '+TP2.apellido as nom1,TP2.fono,TP2.email,TM.codMon,TM.moneda,TM.simbolo,  
	'nro'=case when TC.nroCot<100 then '000'+ltrim(str(TC.nroCot)) when TC.nroCot>=100 and TC.nroCot<1000 then '00'+ltrim(str(TC.nroCot)) when TC.nroCot>=1000 and TC.nroCot<10000 then '0'+ltrim(str(TC.nroCot)) else ltrim(str(TC.nroCot)) end,
	case when TS.nroS<100 then '000'+ltrim(str(TS.nroS)) when TS.nroS>=100 and TS.nroS<1000 then '00'+ltrim(str(TS.nroS)) when TS.nroS>=1000 and TS.nroS<10000 then '0'+ltrim(str(TC.nroCot)) else ltrim(str(TC.nroCot)) end +' - ' +ltrim(str(year(fecSol))) as NroSol
	from TCotizacion TC join TGrupoCot TG on TC.codGruC=TG.codGruC	
	join TFormaPago TF on TC.codPag=TF.codPag
	join TPersonal TP1 on TC.codPersS=TP1.codPers
	join TPersonal TP2 on TC.codPers=TP2.codPers
	join TMoneda TM on TC.codMon=TM.codMon
	left join TSolicitud TS on TC.idSol=TS.idSol
GO

create view VOrdenDetOrden  --impresion
as
	select TC.nroOrden,TC.nroO,TC.fecOrden,TI.codIde,TI.razon,TI.ruc,TI.repres,TI.celRpm,TI.fono+' '+TI.fax as fonos,TI.email,TI.dir,TC.estado,TC.nroProf,TC.idSol,
	'nro'=case when TC.nroO<100 then '000'+ltrim(str(TC.nroO)) when TC.nroO>=100 and TC.nroO<1000 then '00'+ltrim(str(TC.nroO)) when TC.nroO>=1000 and TC.nroO<10000 then '0'+ltrim(str(TC.nroO)) else ltrim(str(TC.nroO)) end,
	TC.transfe,TC.atiendeCom,TC.celAti,TC.plazoEnt,TF.codPag,TF.forma,TC.lugarEnt,TC.igv,TC.calIGV,TC.codPersO,TP2.nombre+' '+TP2.apellido as nomAte,
	TL.codigo,TL.lugar,TL.nombre,TP1.codPers,TP1.nombre+' '+TP1.apellido as nomRem,TP1.fono,TP1.email as emaRem,TC.obsFac,TC.codCot,
	TD.codDetO,TD.cant,TD.descrip as material,TD.unidad,TD.precio,TD.subTotal,TM.codMat,TM.material as mate,TMO.codMon,TMO.moneda,TMO.simbolo
	from TOrdenCompra TC join TPersonal TP1 on TC.codPers=TP1.codPers
	join TPersonal TP2 on TC.codPersO=TP2.codPers
	join TIdentidad TI on TC.codIde=TI.codIde
	join TDetalleOrden TD on TC.nroOrden=TD.nroOrden 
	join TLugarTrabajo TL on TC.codigo=TL.codigo
	join TFormaPago TF on TC.codPag=TF.codPag
	join TMoneda TMO on TC.codMon=TMO.codMon
	join TMaterial TM on TD.codMat=TM.codMat
GO

create View VOrdenInforme  
as	
	select TOR.nroOrden,TOR.nroO,TOR.fecOrden,TOR.igv,TOR.calIGV,TOR.atiendeCom,TOR.plazoEnt,TOR.celAti,TOR.lugarEnt,TOR.transfe,TOR.obsFac,TOR.nroProf,
	TOR.estado,TOR.hist,'nro'=case when nroO<100 then '000'+ltrim(str(nroO)) when nroO>=100 and nroO<1000 then '00'+ltrim(str(nroO)) else ltrim(str(nroO)) end,
	est=case when TOR.estado=0 then 'ABIERTO' when TOR.estado=1 then 'TERMINADO' when TOR.estado=2 then 'CERRADO' else 'ANULADO' end,
	TI.codIde,TI.razon,TI.ruc,TI.dir,TI.fono+'  '+TI.fax as fono1,TI.celRpm,TI.email as emailProv,TI.repres,TI.cuentaBan,TP1.codPers,TP1.nombre+' '+TP1.apellido as nomRem,TP1.fono,TP1.email,
	TOR.codPersO,TP2.nombre+' '+TP2.apellido as nomAte,TF.codPag,TF.forma,TMO.codMon,TMO.moneda,TMO.simbolo,TL.codigo,TL.nombre,
	TOR.codCot,TC.nroCot,case when TC.nroCot<100 then '000'+ltrim(str(TC.nroCot)) when TC.nroCot>=100 and TC.nroCot<1000 then '00'+ltrim(str(TC.nroCot)) else '0'+ltrim(str(TC.nroCot)) end + '-' + ltrim(str(year(fecCot))) as 'nroCotCad',
	TOR.idSol,case when TS.nroS<100 then '000'+ltrim(str(TS.nroS)) when TS.nroS>=100 and TS.nroS<1000 then '00'+ltrim(str(TS.nroS)) else '0'+ltrim(str(TS.nroS)) end + '-' + ltrim(str(year(fecSol))) as 'nroSolCad'
	from TOrdenCompra TOR join TIdentidad TI on TOR.codIde=TI.codIde 
	join TPersonal TP1 on TOR.codPers=TP1.codPers
	join TPersonal TP2 on TOR.codPersO=TP2.codPers
	join TFormaPago TF on TOR.codPag=TF.codPag
	join TMoneda TMO on TOR.codMon=TMO.codMon
	left join TCotizacion TC on TOR.codCot=TC.codCot
	left join TSolicitud TS on TOR.idSol=TS.idSol
	join TLugarTrabajo TL on TOR.codigo=TL.codigo
GO

create view VCotDetCot  --impresion
as
	select TC.codCot,TC.nroCot,TC.fecCot,TI.codIde,TI.razon,TI.ruc,TI.repres,TI.celRpm,TI.fono+' '+TI.fax as fonos,TI.email,TI.dir,TD.estado,
	'nro'=case when TC.nroCot<100 then '000'+ltrim(str(TC.nroCot)) when TC.nroCot>=100 and TC.nroCot<1000 then '00'+ltrim(str(TC.nroCot)) when TC.nroCot>=1000 and TC.nroCot<10000 then '0'+ltrim(str(TC.nroCot)) else ltrim(str(TC.nroCot)) end,
	TC.tiempoVig,TC.atencion,TC.plazo,TF.codPag,TF.forma,TC.lugarEnt,TC.incluir,TC.codPersS,TP2.nombre+' '+TP2.apellido as nomSol,
	TL.codigo,TL.lugar,TL.nombre,TP1.codPers,TP1.nombre+' '+TP1.apellido as nomRem,TP1.fono,TP1.email as emaRem,TC.obs,TC.idSol,TG.codGruC,TG.descrip,TG.estGru,TG.nroGru,
	TD.codDetC,TD.cant,TD.descrip as material,TD.unidad,TD.precio,TD.subTotal,TM.codMat,TM.material as mate
	from TCotizacion TC join TPersonal TP1 on TC.codPers=TP1.codPers
	join TPersonal TP2 on TC.codPersS=TP2.codPers
	join TIdentidad TI on TC.codIde=TI.codIde
	join TDetalleCot TD on TC.codCot=TD.codCot 
	join TLugarTrabajo TL on TC.codigo=TL.codigo
	join TFormaPago TF on TC.codPag=TF.codPag
	join TGrupoCot TG on TC.codGruC=TG.codGruC 
	join TMaterial TM on TD.codMat=TM.codMat
GO

create view VOrdenCompraCad  --instanciado en una funcion y una vista  
as
	select TOC.nroOrden,TDO.nroDO,TDO.idOP,case when nroO<100 then '000'+ltrim(str(nroO)) when nroO>=100 and nroO<1000 then '00'+ltrim(str(nroO)) else '0'+ltrim(str(nroO)) end + '-MECH-' + ltrim(str(year(fecOrden))) as 'nro'
	from TOrdenCompra TOC join TDesOrden TDO on TOC.nroOrden=TDO.nroOrden 
GO

CREATE FUNCTION FN_ConcaNroOrden1(@cod int) RETURNS varchar(500)
BEGIN
	Declare @strConcatenado varchar(500)
	Select @strConcatenado = ''
	select @strConcatenado=@strConcatenado+nro+'  ' from VOrdenCompraCad WHERE idOP=@cod
	--Select @strConcatenado
	RETURN @strConcatenado
END
GO
--select mech.FN_ConcaNroOrden1(1)
--select FN_ConcaNroOrden1(1)
create view VPersDesem
as
	select TP.codPers,TP.codCar,TP.codTipU,TP.nombre,TP.dni,TP.apellido,TP.nombre+' '+TP.apellido as nom,TD.codPersDes,TD.idOP,TD.estDesem,TD.tipoA,TD.obserDesem
	from TPersonal TP join TPersDesem TD on TP.codPers=TD.codPers
GO

create view VCotiNro  
as
	select codCot,nroCot,idSol,case when nroCot<100 then '000'+ltrim(str(nroCot)) when nroCot>=100 and nroCot<1000 then '00'+ltrim(str(nroCot)) else ltrim(str(nroCot)) end + '-' + ltrim(str(year(fecCot))) as 'nro'
	from TCotizacion 
GO

CREATE FUNCTION FN_ConcaNroCoti(@cod int) RETURNS varchar(500)
BEGIN
	Declare @strConcatenado varchar(500)
	Select @strConcatenado = ''
	select @strConcatenado=@strConcatenado+nro+'  ' from VCotiNro WHERE idSol=@cod
	RETURN @strConcatenado
END
GO

create View VSolTodoCad
as	
	select TS.idSol,TS.nroS,TS.fecSol,'est'=case when TS.estado=0 then 'ABIERTO' else 'CERRADO' end,mech.FN_ConcaNroCoti(TS.idSol) as nroCotiConca,  
	'nro'=case when TS.nroS<100 then '000'+ltrim(str(TS.nroS)) when TS.nroS>=100 and TS.nroS<1000 then '00'+ltrim(str(TS.nroS)) when TS.nroS>=1000 and TS.nroS<10000 then '0'+ltrim(str(TS.nroS)) else ltrim(str(TS.nroS)) end,
	TS.estado,TS.obs,TP.codPers,TP.nombre+' '+TP.apellido as nombres,TL.codigo,TL.nombre+' '+TL.lugar as lugar
	from TSolicitud TS join TLugarTrabajo TL on TS.codigo=TL.codigo
	join TPersonal TP on TS.codPers=TP.codPers
GO

create view VSolAperAprobado  
as	
	select distinct TS.idSol,TS.nroS,TS.fecSol,'est'=case when TS.estDet=0 then 'ABIERTO' else 'CERRADO' end,TS.estDet,mech.FN_ConcaNroCoti(TS.idSol) as nroCotiConca, 
	'nro'=case when TS.nroS<100 then '000'+ltrim(str(TS.nroS)) when TS.nroS>=100 and TS.nroS<1000 then '00'+ltrim(str(TS.nroS)) when TS.nroS>=1000 and TS.nroS<10000 then '0'+ltrim(str(TS.nroS)) else ltrim(str(TS.nroS)) end,
	TS.obs,TP.codPers,TP.nombre+' '+TP.apellido as nombres,TL.codigo,TL.nombre+' '+TL.lugar as lugar
	from TSolicitud TS join TLugarTrabajo TL on TS.codigo=TL.codigo
	join TPersonal TP on TS.codPers=TP.codPers
	join TDetalleSol TD on TS.idSol=TD.idSol
	where TD.codEstS=2 --2=Solo Aprobados
GO

create view VDetSolAprobado
as
	select TD.codDetS,TD.cant,TD.descrip,TD.unidad,TD.obs1,TD.idSol,TP1.codPers,TP1.nombre+' '+TP1.apellido as nombres,
	TD.codPersR,isnull(TP2.nombre+' '+TP2.apellido,'') as nombres1,TD.obs3,TA.codAreaM,TA.areaM,TD.codEstS,
	'estRec'=case when TD.estRecep=0 then 'PENDIENTE' when TD.estRecep=1 then 'COMPLETO' else 'INCOMPLETO' end,TD.estRecep
	from TDetalleSol TD join TPersonal TP1 on TD.codPers=TP1.codPers
	left join TPersonal TP2 on TD.codPersR=TP2.codPers
	join TAreaMat TA on TD.codAreaM=TA.codAreaM
	where TD.codEstS=2 --2=Solo Aprobados
GO

create view VSoliNro  
as
	select idSol,nroS,case when nroS<100 then '000'+ltrim(str(nroS)) when nroS>=100 and nroS<1000 then '00'+ltrim(str(nroS)) else '0'+ltrim(str(nroS)) end + '-' + ltrim(str(year(fecSol))) as 'nro'
	from TSolicitud 
GO

CREATE FUNCTION FN_ConcaNroSoli(@cod int) RETURNS varchar(500)
BEGIN
	Declare @strConcatenado varchar(500)
	Select @strConcatenado = ''
	select @strConcatenado=@strConcatenado+nro+'  ' from VSoliNro WHERE idSol=@cod
	RETURN @strConcatenado
END
GO

create View VSolTodoCad1
as	
	select TS.idSol,TS.nroS,TS.fecSol,'est'=case when TS.estado=0 then 'ABIERTO' else 'CERRADO' end,mech.FN_ConcaNroSoli(TS.idSol) as nroSoliConca,  
	'nro'=case when TS.nroS<100 then '000'+ltrim(str(TS.nroS)) when TS.nroS>=100 and TS.nroS<1000 then '00'+ltrim(str(TS.nroS)) when TS.nroS>=1000 and TS.nroS<10000 then '0'+ltrim(str(TS.nroS)) else ltrim(str(TS.nroS)) end,
	TS.estado,TS.obs,TP.codPers,TP.nombre+' '+TP.apellido as nombres,TL.codigo,TL.nombre+' '+TL.lugar as lugar
	from TSolicitud TS join TLugarTrabajo TL on TS.codigo=TL.codigo
	join TPersonal TP on TS.codPers=TP.codPers
GO

create view VOrdenDesembolso  
as	
	select idOP,serie,nroDes,fecDes,monto,montoDet,montoDif,estado,'est'=case when estado=0 then 'PENDIENTE' when estado=1 then 'TERMINADO' when estado=2 then 'CERRADO' else 'ANULADO' end, 
	'nro'=case when nroDes<100 then '000'+ltrim(str(nroDes)) when nroDes>=100 and nroDes<1000 then '00'+ltrim(str(nroDes)) else '0'+ltrim(str(nroDes)) end,TM.codMon,TM.moneda,TM.simbolo,
	codigo,codIde,banco,nroCta,nroDet,datoReq,factCheck,nroFact,bolCheck,nroBol,guiaCheck,nroGuia,vouCheck,nroVou,vouDCheck,nroVouD,reciCheck,nroReci,otroCheck,descOtro,nroConfor,fecEnt,hist
	from TOrdenDesembolso TOD join TMoneda TM on TOD.codMon=TM.codMon 
	where estado in (0,1) --0=pendiente 1=terminado
GO




create view VPagoDesembolso
as
	select TT.codTipP,TT.tipoP,TP.codPagD,TP.fecPago,TP.pagoDet,TP.montoPago,TP.idOP,TM.codMon,TM.moneda,TM.simbolo from 
	TTipoPago TT join TPagoDesembolso TP on TT.codTipP=TP.codTipP
	join TMoneda TM on TP.codMon=TM.codMon
GO

create view VOrdenTodoCad  
as	
	select TC.nroOrden,TC.nroO,TC.codIde,TC.fecOrden,TC.transfe,TC.atiendeCom,TC.plazoEnt,TF.codPag,TF.forma,TC.lugarEnt,TC.codPersO,TC.idSol,
	TC.codPers,'0' as estado,TC.obsFac,TC.codCot,TI.razon,TI.ruc,TI.cuentaBan,TM.codMon,TM.moneda,TM.simbolo,TC.igv,TC.calIGV,TL.codigo,TL.lugar,TL.nombre as obra,
	'nro'=case when TC.nroO<100 then '000'+ltrim(str(TC.nroO)) when TC.nroO>=100 and TC.nroO<1000 then '00'+ltrim(str(TC.nroO)) else '0'+ltrim(str(TC.nroO)) end,
	case when TS.nroS<100 then '000'+ltrim(str(TS.nroS)) when TS.nroS>=100 and TS.nroS<1000 then '00'+ltrim(str(TS.nroS)) else '0'+ltrim(str(TS.nroS)) end + ' - ' + ltrim(str(year(TS.fecSol))) as nroSol
	from TOrdenCompra TC join TFormaPago TF on TC.codPag=TF.codPag
	join TMoneda TM on TC.codMon=TM.codMon
	join TIdentidad TI on TC.codIde=TI.codIde
	join TLugarTrabajo TL on TC.codigo=TL.codigo
	left join TSolicitud TS on TC.idSol=TS.idSol
GO

create view VOrdenDesembolsoCad  
as
	select TOC.idOP,TDO.nroDO,TDO.nroOrden,TOC.serie+'-'+ case when nroDes<100 then '000'+ltrim(str(nroDes)) when nroDes>=100 and nroDes<1000 then '00'+ltrim(str(nroDes)) else '0'+ltrim(str(nroDes)) end + ' - ' + ltrim(str(year(fecDes))) as 'nro'
	from TOrdenDesembolso TOC join TDesOrden TDO on TOC.idOP=TDO.idOP 
GO

CREATE FUNCTION FN_ConcaNroOrdenDes(@cod int) RETURNS varchar(500)
BEGIN
	Declare @strConcatenado varchar(500)
	Select @strConcatenado = ''
	select @strConcatenado=@strConcatenado+nro+'  ' from VOrdenDesembolsoCad  WHERE nroOrden=@cod
	--Select @strConcatenado
	RETURN @strConcatenado
END
GO
--select mech.FN_ConcaNroOrdenDes(1)

create view VPersDesemGerencia
as
	select TP.codPers,TP.nombre+' '+TP.apellido as nom,TP.dni,TPD.codPersDes,TPD.estDesem,TPD.tipoA,TPD.obserDesem,TPD.idOP,
	'estApro'=case when TPD.estDesem=1 then 'APROBADO' when TPD.estDesem=2 then 'OBSERVADO' else 'DENEGADO' end 
	from TPersDesem TPD join TPersonal TP on TPD.codPers=TP.codPers where TPD.tipoA=2 --2=firma gerencia
GO

create view VOrdenDesemGerencia  
as	
	select TOD.idOP,serie,nroDes,fecDes,monto,montoDet,montoDif,TOD.estado,'est'=case when TOD.estado=0 then 'PENDIENTE' when TOD.estado=1 then 'TERMINADO' when TOD.estado=2 then 'CERRADO' else 'ANULADO' end, 
	'nro'=case when nroDes<100 then '000'+ltrim(str(nroDes)) when nroDes>=100 and nroDes<1000 then '00'+ltrim(str(nroDes)) else '0'+ltrim(str(nroDes)) end,TM.codMon,TM.moneda,TM.simbolo,
	banco,nroCta,nroDet,datoReq,hist,TL.codigo,TL.nombre,TI.codIde,TI.razon,TI.ruc,TP.codPers,TP.nom,TP.dni,isnull(TP.codPersDes,0) as codPersDes,isnull(TP.estDesem,0) as estDesem,TP.obserDesem,isnull(TP.estApro,'') as estApro
	from TOrdenDesembolso TOD join TMoneda TM on TOD.codMon=TM.codMon 
	join TLugarTrabajo TL on TOD.codigo=TL.codigo
	join TIdentidad TI on TOD.codIde=TI.codIde
	left join VPersDesemGerencia TP on TOD.idOP=TP.idOP --2=gerencia
GO

create view VOrdenCompraDetalle  
as
	select TD.codDetO,TD.cant,TD.unidad,TD.descrip,TD.precio,TD.subTotal,TOC.nroOrden,TOC.nroO,TOC.fecOrden,TOC.igv,TOC.calIGV,TOC.codMon,TDO.nroDO,TDO.idOP,
	'nro'=case when nroO<100 then '000'+ltrim(str(nroO)) when nroO>=100 and nroO<1000 then '00'+ltrim(str(nroO)) else '0'+ltrim(str(nroO)) end,
	TOC.idSol,TS.fecSol,isnull(DATEDIFF(dd,TS.fecSol,getdate()),0) as dias,TM.moneda,TM.simbolo 
	from TOrdenCompra TOC join TDetalleOrden TD on TOC.nroOrden=TD.nroOrden
	join TMoneda TM on TOC.codMon=TM.codMon
	join TDesOrden TDO on TOC.nroOrden=TDO.nroOrden 
	left join TSolicitud TS on TOC.idSol=TS.idSol
GO
---EJECUTAR AHORITA SERVIDOR
create view VPersDesemTesoreria
as
	select TP.codPers,TP.nombre+' '+TP.apellido as nom,TP.dni,TPD.codPersDes,TPD.estDesem,TPD.tipoA,TPD.obserDesem,TPD.idOP,
	'estApro'=case when TPD.estDesem=1 then 'APROBADO' when TPD.estDesem=2 then 'OBSERVADO' else 'DENEGADO' end 
	from TPersDesem TPD join TPersonal TP on TPD.codPers=TP.codPers where TPD.estDesem=1 and tipoA=2 --1=aprobado 2=firma gerencia
GO

create view VOrdenDesemTesoreria  
as	
	select TOD.idOP,serie,nroDes,fecDes,monto,montoDet,montoDif,TOD.estado,'est'=case when TOD.estado=0 then 'PENDIENTE' when TOD.estado=1 then 'TERMINADO' when TOD.estado=2 then 'CERRADO' else 'ANULADO' end, 
	'nro'=case when nroDes<100 then '000'+ltrim(str(nroDes)) when nroDes>=100 and nroDes<1000 then '00'+ltrim(str(nroDes)) else '0'+ltrim(str(nroDes)) end,TM.codMon,TM.moneda,TM.simbolo,
	banco,nroCta,nroDet,datoReq,hist,TL.codigo,TL.nombre,TI.codIde,TI.razon,TI.ruc,TP.codPers,TP.nom,TP.dni,isnull(TP.codPersDes,0) as codPersDes,isnull(TP.estDesem,0) as estDesem,TP.obserDesem,isnull(TP.estApro,'') as estApro
	from TOrdenDesembolso TOD join TMoneda TM on TOD.codMon=TM.codMon 
	join TLugarTrabajo TL on TOD.codigo=TL.codigo
	join TIdentidad TI on TOD.codIde=TI.codIde
	join VPersDesemTesoreria TP on TOD.idOP=TP.idOP --2=gerencia
	where TOD.estado=0 --0=pendiente 1=terminado
GO

create view VPagoDesemTesoreria
as
	select TT.codTipP,TT.tipoP,TP.codPagD,TP.fecPago,TP.pagoDet,TM.codMon,TM.moneda,TM.simbolo,TP.montoPago,TP.idOP,TP.idCue 
	from TTipoPago TT join TPagoDesembolso TP on TT.codTipP=TP.codTipP
	join TMoneda TM on TP.codMon=TM.codMon
GO

create view VBancoCuenta
as
	select TB.codBan,banco,idCue,nroCue,TM.codMon,TM.moneda,TM.simbolo,
	'banmon'=case when banco<>'' then banco+'  '+simbolo else '' end 
	from TBanco TB join TCuentaBan TC on TB.codBan=TC.codBan
	join TMoneda TM on TC.codMon=TM.codMon
	where TC.estado=1  --1=activo
GO 
--------------------------------
-----------------------------------
create view VSolDetSolAproba 
as
	select TS.idSol,TS.nroS,TS.fecSol,'nro'=case when TS.nroS<100 then '000'+ltrim(str(TS.nroS)) when TS.nroS>=100 and TS.nroS<1000 then '00'+ltrim(str(TS.nroS)) else '0'+ltrim(str(TS.nroS)) end,
	TS.estado,TS.obs,TP0.codPers as codPersRe,TP0.nombre+' '+TP0.apellido as nomResid,TL.codigo,TL.nombre+' '+TL.lugar as lugar,
	TD.codDetS,TD.prioridad,TD.cant,TD.descrip,TD.unidad,TD.obs1,TP1.codPers,TP1.nombre+' '+TP1.apellido as nombres,TM.preBase,isnull(DATEDIFF(dd,TS.fecSol,getdate()),0) as dias,
	TD.codPersA,isnull(TP2.nombre+' '+TP2.apellido,'') as nombres1,TD.obs2,TM.codMat,TM.material,TA.codAreaM,TA.areaM,TE.codEstS,TE.estSol
	from TSolicitud TS join TLugarTrabajo TL on TS.codigo=TL.codigo
	join TPersonal TP0 on TS.codPers=TP0.codPers
	join TDetalleSol TD on TS.idSol=TD.idSol
	join TPersonal TP1 on TD.codPers=TP1.codPers
	left join TPersonal TP2 on TD.codPersA=TP2.codPers
	join TEstSol TE on TD.codEstS=TE.codEstS
	join TMaterial TM on TD.codMat=TM.codMat
	join TAreaMat TA on TD.codAreaM=TA.codAreaM
	where TE.codEstS in(1,3) --1Pendiente 3Observado 2Aprobado 4Rechazado
GO


--Nueva Vista para Seguimiento de Desembolso
create view VOrdenDesebolsoSeguimiento
as
	select idOP,serie,nroDes,fecDes,monto,montoDet,montoDif,'est'=case when TOD.estado=0 then 'PENDIENTE' when TOD.estado=1 then 'TERMINADO' when TOD.estado=2 then 'CERRADO' else 'ANULADO' end, 
	'nro'=case when nroDes<100 then '000'+ltrim(str(nroDes)) when nroDes>=100 and nroDes<1000 then '00'+ltrim(str(nroDes)) else '0'+ltrim(str(nroDes)) end,TM.codMon,TM.moneda,TM.simbolo,
	TLU.nombre,codIde,banco,nroCta,nroDet,datoReq,factCheck,bolCheck,guiaCheck,vouCheck,vouDCheck,reciCheck,otroCheck,descOtro,nroConfor,fecEnt,hist
	from TOrdenDesembolso TOD join TMoneda TM on TOD.codMon=TM.codMon join TIdentidad TID on tod.codIde=tod.codIde join TLugarTrabajo TLU on tlu.codigo=TOD.codigo   
	where estado in (0,1) --0=pendiente 1=terminado

GO

select codPagD,fecPago,tipoP,pagoDet,simbolo,montoPago,codTipP,codMon,idOP,idCue from VPagoDesemTesoreria where idOP=@idOP

select idSol,nro+' '+est as nro,fecSol,est,nombres,lugar,obs,estado,codigo,codPers,nro as nroS from VSolAper where codigo=@cod
select codDetS,prioridad,descrip,cant,unidad,estSol,areaM,nro,fecSol,dias,lugar,nombres,obs1,nombres1,obs2,nomResid,obs,idSol,codEstS,codAreaM,codPers,codMat,codPersRe,estado,codigo from VSolDetSolAproba where (codigo=@co1 or codigo>=@co2) and (codAreaM=@codA1 or codAreaM>@codA2)

select * from TPersonal































create view VArticuloConfig		
as
	select ltrim(str(TA.codArt))+'Q' as codigo,TA.codigo as codArtP,TA.artMod,isnull(TDA.stock,0) as stock,TU.unidad,TM.codMar,TM.marca,TT.codTipA,TT.tipo,
	TA.codArt,TA.nro,TU.codUni
	from TMarca TM join TTipoArt TT on TM.codMar=TT.codMar
	join TArticulo TA  on TT.codTipA=TA.codTipA
	join TUnidad TU on TA.codUni=TU.codUni 
	left join (select SUM(cant) as stock,codArt from TDetalleArt where error=0 group by codArt) TDA on TA.codArt=TDA.codArt
	where TA.estado=1 --Activo
GO

create view VArticuloStockUbi
as
	select sum(cant) as stock,TD.codUbi,TD.codArt,TU.ubicacion,TU.color 
	from TDetalleArt TD join TUbicacion TU on TD.codUbi=TU.codUbi 
	where TD.cant>0 group by TD.codUbi,TD.codArt,TU.ubicacion,TU.color
GO

create view VDetalleArt
as
		select ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TDA.cant,TC.codCol,TC.color,TDA.letra,TAR.codTipA,
		TT.talla,TT.idTalla,TDA.precioC,TDA.precioV,TU.ubicacion,TM.codMar,TM.marca,'LT.'+ltrim(str(TB.nrolote))+'.'+convert(varchar(10),TB.fechaLot,103) lote1, 
		TB.nrolote,TB.fechaLot,TB.idB,TU.codUbi,TDA.codArt,TDA.nroDet,TDA.codDetA,TU.color as color1
		from TDetalleArt TDA join TColor TC on TDA.codCol=TC.codCol
		join TUbicacion TU on TDA.codUbi=TU.codUbi
		join TArticulo TAR on TDA.codArt=TAR.codArt
		join TMarca TM on TAR.codMar=TM.codMar
		join TLoteEntrada TB on TDA.idB=TB.idB
		join TTalla TT on TDA.idTalla=TT.idTalla
		where TDA.cant>0
GO

create view VDetArtLoteEntrada		
as
		select ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TAR.codigo as codArtP,TDA.cant,TT.talla,TT.idTalla,ltrim(str(TAR.codArt))+'Q' as cod,
		TC.color,TDA.precioC,TDA.precioV,'almacen'=case when TDA.cant<=0 then 'SALIO DE '+TU.ubicacion else TU.ubicacion end,TU.color as color1,TAR.artMod,TMA.codMar,
		TB.nrolote,TB.idB,TU.codUbi,TC.codCol,TDA.codArt,TDA.nroDet,TU.codigo as idSuc,TDA.error,TDA.codDetA,TMA.marca,'LT.'+ltrim(str(TB.nrolote))+'.'+convert(varchar(10),TB.fechaLot,103) lote1
		from TDetalleArt TDA join TColor TC on TDA.codCol=TC.codCol
		join TUbicacion TU on TDA.codUbi=TU.codUbi
		join TArticulo TAR on TDA.codArt=TAR.codArt
		join TMarca TMA on TAR.codMar=TMA.codMar
		join TLoteEntrada TB on TDA.idB=TB.idB
		join TTalla TT on TDA.idTalla=TT.idTalla
		where TDA.error=0	--0=normal  1=salio error 
GO

create view VArticuloStock			--Instanciado en varias interfaces
as
	select ltrim(str(TA.codArt))+'Q' as codigo,TA.codigo as codArtP,TA.artMod,TDA.stock,TU.unidad,TM.codMar,TM.marca,TT.codTipA,TT.tipo,
	TA.codArt,TA.nro,TU.codUni
	from TMarca TM join TTipoArt TT on TM.codMar=TT.codMar
	join TArticulo TA  on TT.codTipA=TA.codTipA
	join TUnidad TU on TA.codUni=TU.codUni 
	join (select SUM(cant) as stock,codArt from TDetalleArt where error=0 group by codArt) TDA on TA.codArt=TDA.codArt
	where TA.estado=1 --Activo
GO

create view VDetalleArtStock		--VISTA IMPORTANTE NO MODIFICAR
as
		select ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TAR.codigo as codArtP,TDA.cant,TT.talla,ltrim(str(TAR.codArt))+'Q' as codigo1,TT.idTalla,
		TC.color,TDA.codDetA,TMA.marca,TDA.codArt,TU.ubicacion,TDA.nroDet,TU.codUbi,TU.codigo as codSuc,TB.idB,TDA.seleEtiq,TMA.codMar,TDA.precioC,TDA.precioV,
		TU.color as color1,'LT.'+ltrim(str(TB.nrolote))+'.'+convert(varchar(10),TB.fechaLot,103) lote1,TB.nrolote,TB.fechaLot 
		from TDetalleArt TDA join TColor TC on TDA.codCol=TC.codCol
		join TTalla TT on TDA.idTalla=TT.idTalla
		join TUbicacion TU on TDA.codUbi=TU.codUbi
		join TArticulo TAR on TDA.codArt=TAR.codArt
		join TMarca TMA on TAR.codMar=TMA.codMar
		join TLoteEntrada TB on TDA.idB=TB.idB
		where TDA.cant>0
GO

create view VMovimientosArticulo
as
	select distinct TES.nroNota,TDA.codDetA,ltrim(str(TA.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TM.codMar,TM.marca,TAL.color as color1,
	TES.fecha,TES.cantEnt as Entrada,TES.cantSal as Salida,TES.saldo as Saldo,TDA.error,TA.artMod,TT.talla,TC.color,TA.codigo as codArtP,
	TT1.tipo as tipoTrans,TDA.codUbi,TAL.ubicacion,TES.motivo,TA.codArt,TES.codTrans,TUS.nomPers,TUS.codUsu,TES.codRes
	from TArticulo TA join TMarca TM on TA.codMar=TM.codMar
	join TDetalleArt TDA on TA.codArt=TDA.codArt
	join TDetES TDE on TDA.codDetA=TDE.codDetA
	join TUbicacion TAL on TDE.codUbi=TAL.codUbi
	join TEntradaSalida TES on TDE.nroNota=TES.nroNota
	join TTipoTransac TT1 on TES.codTrans=TT1.codTrans
	join TUsuario TUS on TES.codUsu=TUS.codUsu
	join TColor TC on TDA.codCol=TC.codCol
	join TTalla TT on TDA.idTalla=TT.idTalla
GO

create view VLoteSele
as
	select distinct TL.idB,TL.nrolote,fechaLot,TL.estado,'LT.'+ltrim(str(TL.nrolote))+'.'+convert(varchar(10),TL.fechaLot,103) lote1 
	from TLoteEntrada TL join TDetalleArt TDA on TL.idB=TDA.idB
	where TDA.cant>0
GO

create view VMarcaSele
as
		select distinct TMA.codMar,marca
		from TDetalleArt TDA join TArticulo TAR on TDA.codArt=TAR.codArt
		join TMarca TMA on TAR.codMar=TMA.codMar
		where TDA.cant>0
GO

create view VDetalleArtEtiqueta		--VISTA IMPORTANTE NO MODIFICAR
as
		select ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TAR.codigo as codArtP,TDA.cant,TT.talla,TT.idTalla,
		TC.color,TDA.codDetA,TU.ubicacion,'LT.'+ltrim(str(TB.nrolote))+TDA.letra as bloke,TMA.marca,TDA.codArt,TDA.nroDet,TU.codUbi,TU.codigo as codSuc,TB.idB,
		'*'+ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet))+'*' as codAst,TDA.seleEtiq,TMA.codMar,TDA.precioC,TDA.precioV,
		TU.color as color1,'LT.'+ltrim(str(TB.nrolote))+'.'+convert(varchar(10),TB.fechaLot,103) lote1
		from TDetalleArt TDA join TColor TC on TDA.codCol=TC.codCol
		join TUbicacion TU on TDA.codUbi=TU.codUbi
		join TArticulo TAR on TDA.codArt=TAR.codArt
		join TMarca TMA on TAR.codMar=TMA.codMar
		join TLoteEntrada TB on TDA.idB=TB.idB
		join TTalla TT on TDA.idTalla=TT.idTalla
		where TDA.cant>0
GO

create view VDetalleArtGuia	--Vista usada en varias pantallas	
as
	select ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TAR.codigo as codArtP,TDA.cant,TT.talla,ltrim(str(TAR.codArt))+'Q' as codigo1,TAR.artMod,
	TC.color,TDA.codDetA,TMA.marca,TDA.codArt,TU.ubicacion,TDA.nroDet,TU.codUbi,TU.codigo as codSuc,TB.idB,TDA.seleEtiq,TMA.codMar,TDA.precioC,TDA.precioV,
	TU.color as color1,'LT.'+ltrim(str(TB.nrolote))+'.'+convert(varchar(10),TB.fechaLot,103) lote1 
	from TDetalleArt TDA join TColor TC on TDA.codCol=TC.codCol
	join TUbicacion TU on TDA.codUbi=TU.codUbi
	join TArticulo TAR on TDA.codArt=TAR.codArt
	join TMarca TMA on TAR.codMar=TMA.codMar
	join TLoteEntrada TB on TDA.idB=TB.idB
	join TTalla TT on TDA.idTalla=TT.idTalla
	where TDA.cant>0
GO

create view VTraspasoArticulo
as
	select distinct TES.nroNota,TDA.codDetA,ltrim(str(TA.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TES.codDia,
	TES.fecha,TES.cantEnt as Entrada,TES.cantSal as Salida,TES.saldo as Saldo,TA.artMod+' '+TC.color as articulo,TT.talla,TAL.color as color1,
	TDA.codUbi,TAL.ubicacion,TES.motivo+' '+TT1.tipo as motivo,TA.codArt,TES.codTrans,TUS.nomPers,TUS.codUsu,TES.codRes,TA.codigo as codArtP
	--isnull(TG.codGuia,0) as codGuia,isnull(TG.nroGuia,'') as nroGuia
	from TArticulo TA join TMarca TM on TA.codMar=TM.codMar
	join TDetalleArt TDA on TA.codArt=TDA.codArt
	join TDetES TDE on TDA.codDetA=TDE.codDetA
	join TUbicacion TAL on TDE.codUbi=TAL.codUbi
	join TEntradaSalida TES on TDE.nroNota=TES.nroNota
	join TTipoTransac TT1 on TES.codTrans=TT1.codTrans
	join TUsuario TUS on TES.codUsu=TUS.codUsu
	join TColor TC on TDA.codCol=TC.codCol
	join TTalla TT on TDA.idTalla=TT.idTalla
	--left join TGuiaTraspaso TG on TES.codGuia=TG.codGuia
	where TES.codTrans in(6,7) and TES.codGuia=0	--6,7=Entrada Salida
GO

create view VCliente  
as
	select codCli,cliente,ruc,dni,dir,fono,email,'est'=case when estado=1 then 'Activo' else 'INACTIVO' end,estado
	from TCliente 
GO

create view VTSerie
as
	select TUS.codSS,TTD.tipo,TS.serie,TS.iniNroDoc,TS.finNroDoc,TS.descrip,TUS.codigo,TS.codSer,TTD.codTipD 
	from TTipoDoc TTD join  TSerie TS on TTD.codTipD=TS.codTipD
	join TSerieSucursal TUS on TS.codSer=TUS.codSer where TS.estado=1
GO

create view VSerieSucursal
as
	select TT.codTipD,TT.tipo,TS.codSer,TS.serie,TS.iniNroDoc,TS.finNroDoc,TS.descrip,TSS.codSS,TSS.codigo 
	from TTipoDoc TT join TSerie TS on TT.codTipD=TS.codTipD
	join TSerieSucursal TSS on TS.codSer=TSS.codSer
	where TS.estado=1  --Activo
GO

create view VDetalleArtBuscar1
as
		select ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TDA.cant,TT.talla,ltrim(str(TAR.codArt))+'Q' as codigo1,TAR.artMod,
		TC.color,TDA.codDetA,TMA.marca,TDA.codArt,TU.ubicacion,TDA.nroDet,TU.codUbi,TU.codigo as codSuc,TDA.seleEtiq,TMA.codMar,TDA.precioC,TDA.precioV,TU.color as color1,
		ISNULL(TD.serie+'.'+ltrim(str(TD.nroDoc)),'') as doc
		from TDetalleArt TDA join TColor TC on TDA.codCol=TC.codCol
		join TUbicacion TU on TDA.codUbi=TU.codUbi
		join TArticulo TAR on TDA.codArt=TAR.codArt
		join TMarca TMA on TAR.codMar=TMA.codMar
		join TTalla TT on TDA.idTalla=TT.idTalla
		left join TDocCompra TD on TDA.codDC=TD.codDC
		where TDA.cant>0
GO

create view VVentasAnuladas --REPETIDO... esta mas abajo
AS
	select ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TD.codDoc,TD.talonario,TD.nroDoc,TUB.color as color1,TTD.tipo,TUB.codigo as codSuc,TTD.codTipD,TTP.codTipP,TTP.tipoP,TTP.efec,
	'doc'=case when TD.nroDoc<10 then '0000'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=10 and TD.nroDoc<100 then '000'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=100 and TD.nroDoc<1000 then '00'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=1000 and TD.nroDoc<10000 then '0'+ltrim(str(TD.nroDoc))else ltrim(str(TD.nroDoc)) end,
	TUB.ubicacion,TD.fechaDoc,TES.cantSal,TAR.artMod,TC.color,TT.talla,TES.preUniEnt as preCom,TES.preUniSal as valorUni,TD.datoPago,TD.cliente,TD.ruc,TD.dni,TD.dir,TD.codCli,TD.IGV,
	CAST(TES.cantSal*TES.preUniSal as decimal(10,2)) as importe,TES.nroNota,TU.codUsu,TU.nomPers,TDA.codDetA,TDA.cant,TDA.codArt,TDA.codUbi,TMA.codMar,TMA.marca,TES.codDia,TAR.estado
	from TDocumento TD join TSerie TS on TD.codSer=TS.CodSer
	join TTipoDoc TTD on TS.codTipD=TTD.codTipD
	join TTipoPago TTP on TD.codTipP=TTP.codTipP
	join TEntradaSalida TES on CAST(TD.codDoc as varchar(10))=TES.codRes
	join TUsuario TU on TES.codPers=TU.codUsu
	join TDetES TDE on TES.nroNota=TDE.nroNota
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA
	join TArticulo TAR on TDA.codArt=TAR.codArt
	join TMarca TMA on TAR.codMar=TMA.codMar 
	join TColor TC on TDA.codCol=TC.codCol
	join TTalla TT on TDA.idTalla=TT.idTalla
	join TUbicacion TUB on TDA.codUbi=TUB.codUbi
	where TES.codTrans=5 --codTrans=5 anular doc	
GO

create view VDiaSesionVenta
AS
	select distinct TDS.codDia,TDS.fecha,TDS.estado,TDS.codigo 
	from TDiaSesion TDS join TEntradaSalida TES on TDS.codDia=TES.codDia
	where TES.codTrans=4 --codTrans=4 venta doc		
GO

create view VArticuloBusqueda
as
	select ltrim(str(TA.codArt))+'Q' as codigo,TA.codigo as codArtP,TA.artMod,TM.codMar,TM.marca,TA.codArt
	from TMarca TM join TArticulo TA on TM.codMar=TA.codMar
	where TA.estado=1 --Activo
GO

create view VTalla 
as
	select distinct TD.codArt,TT.talla from 
	TDetalleArt TD join TTalla TT on TD.idTalla=TT.idTalla 
	where error=0
GO

create view VDetalleArtLista
as
		select ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TAR.codigo as codArtP,TAR.artMod,TU.color as color1,
		cant,TT.talla,precioC,precioV,TC.color,TMA.marca,TU.ubicacion,TDA.codDetA,TDA.nroDet,TDA.codArt,TC.codCol,TDA.codUbi,TU.codigo as codSuc,TAR.codTipA
		from TDetalleArt TDA join TColor TC on TDA.codCol=TC.codCol
		join TUbicacion TU on TDA.codUbi=TU.codUbi
		join TArticulo TAR on TDA.codArt=TAR.codArt
		join TMarca TMA on TAR.codMar=TMA.codMar 
		join TTalla TT on TDA.idTalla=TT.idTalla
		where TDA.cant>0
GO

--select stock,ubicacion,codArt,codUbi,color from VArticuloStockUbi where codArt=@codArt

create view VMovArt
as
	select distinct TES.nroNota,TDA.codDetA,ltrim(str(TA.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TES.preUniEnt,TES.preUniSal,TMA.codMar,TMA.marca,
	TES.fecha,TES.cantEnt as Entrada,TES.cantSal as Salida,TES.saldo as Saldo,TDA.error,TA.artMod,TT.talla,TC.color,
	TT1.tipo as tipoTrans,TDA.codUbi,TAL.ubicacion,TES.motivo,TA.codArt,TES.codTrans,TUS.nomPers as usuario,TUS.codUsu,TES.codRes,TA.codigo as codArtP,
	TUS1.codUsu as codPers,TUS1.nomPers as pers,TDI.codDia,TDI.fecha as fechaSes,TAL.color as color1
	from TArticulo TA join TMarca TMA on TA.codMar=TMA.codMar
	join TDetalleArt TDA on TA.codArt=TDA.codArt
	join TDetES TDE on TDA.codDetA=TDE.codDetA
	join TUbicacion TAL on TDE.codUbi=TAL.codUbi
	join TEntradaSalida TES on TDE.nroNota=TES.nroNota
	join TTipoTransac TT1 on TES.codTrans=TT1.codTrans
	join TTalla TT on TDA.idTalla=TT.idTalla
	join TUsuario TUS on TES.codUsu=TUS.codUsu
	join TColor TC on TDA.codCol=TC.codCol
	join TDiaSesion TDI on TES.codDia=TDI.codDia
	left join TUsuario TUS1 on TES.codPers=TUS1.codUsu
GO
----------INFORME STOCK LINEAL--------------
create view VArtTipoArtMarca
as
	Select TA.codArt,TA.codigo,TA.artMod,TT.codTipA,TT.tipo,TM.codMar,TM.marca,ltrim(str(TA.codArt))+'Q' as cod 
	from TArticulo TA join TTipoArt TT on TA.codTipA=TT.codTipA
	join TMarca TM on TT.codMar=TM.codMar 
GO

create view VStockSuc
as
	Select TDA.codArt,sum(TDA.cant) as stock,TN.nombre,TN.codigo	
	from TDetalleArt TDA join TUbicacion TAL on TDA.codUbi=TAL.codUbi join TNegocioSuc TN on TAL.codigo=TN.codigo
	where TDA.cant>0 group by TDA.codArt,TN.codigo,TN.nombre --order by TDA.codArt
GO

create view VStockCodigo
as
	select VA.codArt,VA.codigo,VA.cod,VA.artMod,VA.codTipA,VA.tipo,VA.codMar,VA.marca,VS.stock,VS.nombre,VS.codigo as codSuc
	from VArtTipoArtMarca VA join VStockSuc VS on VA.codArt=VS.codArt
GO

create view VTallasLinea
as
	select TD.codArt,sum(TD.cant) as cant,TT.talla,TU.codigo as codSuc
	from TDetalleArt TD join TTalla TT on TD.idTalla=TT.idTalla
	join TUbicacion TU on TD.codUbi=TU.codUbi
	where TD.cant>0 group by TT.talla,TU.codigo,TD.codArt --order by TU.codigo,TD.codArt,TT.talla 
GO
	 
CREATE FUNCTION fn_StockTallaLinea(@codSuc varchar(10)) RETURNS table
AS
RETURN (select V1.codArt,V1.cod,V1.codigo,V1.artMod,V1.stock,V2.talla,V2.cant,V1.codSuc,V1.nombre,V1.codMar,V1.marca,V1.codTipA,V1.tipo
		from ((select codArt,cod,codigo,artMod,codTipA,tipo,codMar,marca,stock,nombre,codSuc from VStockCodigo where codSuc=@codSuc) V1 
		join (select codArt,cant,talla,codSuc from VTallasLinea where codSuc=@codSuc) V2 on V1.codArt=V2.codArt))
GO

create view VStockCodigoTalla
as
	select VA.codArt,VA.codigo,VA.cod,VA.artMod,VA.codTipA,VA.tipo,VA.codMar,VA.marca,VS.stock,VS.nombre,VS.codigo as codSuc,TT.idTalla,TT.talla,'' as Can
	from VArtTipoArtMarca VA join VStockSuc VS on VA.codArt=VS.codArt
	join TTalla TT on VA.codTipA=TT.codTipA
GO
--------------FIN STOCK LINEAL---------------------

create view VVerGuias  --Vista importante no modificar		
as
	select ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TT.talla,TU1.codUbi as codUbiO,TU.codUbi as codUbiD,TUS1.codUsu as codUsuE,TUS2.codUsu as codUsuR,
	TAR.artMod,TC.color,TES.cantEnt,TES.cantSal,TES.codTrans,TES.fecha,TES.nroNota,TES.preUniEnt,TES.preUniSal,TU1.codigo as codSuc,TDA.nroDet,TN.nombre,TN.codigo as CodSucDes,
	TU1.ubicacion as origen,TU.ubicacion AS destino,TUS1.nomPers as entrega,TUS2.nomPers as recibe,TU.color as color1,TG.codGuia,TG.nroGuia,TG.fecMov,TG.motivo,
	'estado'=case when TG.estado=0 then 'Abierto' when TG.estado=1 then 'Cerrado' else 'Anulado' end,TG.estado as estado1,
	TAR.codArt,TDA.codDetA,TAR.codigo as codArtP,TMA.marca,TMA.codMar,'LT.'+ltrim(str(TL.nrolote))+'.'+convert(varchar(10),TL.fechaLot,103) lote1,TES.selEtq,TUS2.codUsu as codPerRec
	from TDetalleArt TDA join TColor TC on TDA.codCol=TC.codCol
	join TDetES TDE on TDA.codDetA=TDE.codDetA
	join TEntradaSalida TES on TDE.nroNota=TES.nroNota
	join TUbicacion TU on TDE.codUbi=TU.codUbi
	join TGuiaTraspaso TG on TES.codGuia=TG.codGuia
	join TUbicacion TU1 on TG.codUbiOri=TU1.codUbi
	join TNegocioSuc TN on TG.codigo=TN.codigo
	join TUsuario TUS1 on TG.codUsuEnt=TUS1.codUsu
	join TUsuario TUS2 on TG.codUsuRec=TUS2.codUsu
	join TArticulo TAR on TES.codArt=TAR.codArt
	join TMarca TMA on TAR.codMar=TMA.codMar
	join TLoteEntrada TL on TDA.idB=TL.idB
	join TTalla TT on TDA.idTalla=TT.idTalla
	where TDA.error=0 and TES.codTrans=13  --13=Entrada Traspaso GUIA 12=Salida Traspaso 
GO

create view VVerGuiasDevol  --Vista importante no modificar		
as
	select ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TT.talla,TU1.codUbi as codUbiO,TU.codUbi as codUbiD,TUS1.codUsu as codUsuE,TUS2.codUsu as codUsuR,
	TAR.artMod,TC.color,TES.cantEnt,TES.cantSal,TES.codTrans,TES.fecha,TES.nroNota,TES.preUniEnt,TES.preUniSal,TU1.codigo as codSuc,TDA.nroDet,TN.nombre,TN.codigo as CodSucDes,
	TU1.ubicacion as origen,TU.ubicacion AS destino,TUS1.nomPers as entrega,TUS2.nomPers as recibe,TU.color as color1,TG.codGuia,TG.nroGuia,TG.fecMov,TG.motivo,
	'estado'=case when TG.estado=0 then 'Abierto' when TG.estado=1 then 'Cerrado' else 'Anulado' end,TG.estado as estado1,
	TAR.codArt,TDA.codDetA,TAR.codigo as codArtP,TMA.marca,TMA.codMar,TES.selEtq,TUS2.codUsu as codPerRec
	from TDetalleArt TDA join TColor TC on TDA.codCol=TC.codCol
	join TDetES TDE on TDA.codDetA=TDE.codDetA
	join TEntradaSalida TES on TDE.nroNota=TES.nroNota
	join TUbicacion TU on TDE.codUbi=TU.codUbi
	join TGuiaDevol TG on TES.codGuia1=TG.codGuia
	join TUbicacion TU1 on TG.codUbiOri=TU1.codUbi
	join TNegocioSuc TN on TG.codigo=TN.codigo
	join TUsuario TUS1 on TG.codUsuEnt=TUS1.codUsu
	join TUsuario TUS2 on TG.codUsuRec=TUS2.codUsu
	join TArticulo TAR on TES.codArt=TAR.codArt
	join TMarca TMA on TAR.codMar=TMA.codMar
	join TTalla TT on TDA.idTalla=TT.idTalla
	where TDA.error=0 and TES.codTrans=11  --11=Entrada Devolucion Almacen  16=Salida Devolucion Almacen 
GO
------------------------------------------------
-------------07/04/2012-------------------------
create view VVentaDoc ---REPETIDO MAS ABAJO ORIGINAL
AS
	select ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TD.codDoc,TD.talonario,TD.nroDoc,TUB.color as color1,TTD.tipo,TUB.codigo as codSuc,TS.codSer,
	'doc'=case when TD.nroDoc<10 then '0000'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=10 and TD.nroDoc<100 then '000'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=100 and TD.nroDoc<1000 then '00'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=1000 and TD.nroDoc<10000 then '0'+ltrim(str(TD.nroDoc))else ltrim(str(TD.nroDoc)) end,
	TUB.ubicacion,TD.fechaDoc,TES.cantSal,TAR.artMod,TC.color,TT.talla,TES.preUniEnt as preCom,TES.preUniSal as valorUni,TTD.codTipD,TD.cliente,TD.ruc,TD.dni,TD.dir,TD.codCli,TD.IGV,
	CAST(TES.cantSal*TES.preUniSal as decimal(10,2)) as importe,TES.nroNota,TU.codUsu,TU.nomPers,TDA.codDetA,TDA.cant,TDA.codArt,TDA.codUbi,TMA.codMar,TMA.marca,TES.codDia,TAR.estado,TES.motivo
	from TDocumento TD join TSerie TS on TD.codSer=TS.CodSer
	join TTipoDoc TTD on TS.codTipD=TTD.codTipD
	join TEntradaSalida TES on CAST(TD.codDoc as varchar(10))=TES.codRes
	join TUsuario TU on TES.codPers=TU.codUsu
	join TDetES TDE on TES.nroNota=TDE.nroNota
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA
	join TArticulo TAR on TDA.codArt=TAR.codArt
	join TMarca TMA on TAR.codMar=TMA.codMar 
	join TColor TC on TDA.codCol=TC.codCol
	join TTalla TT on TDA.idTalla=TT.idTalla
	join TUbicacion TUB on TDA.codUbi=TUB.codUbi
	where TES.codTrans=4 --codTrans=4 venta doc
GO

create view VVentas		-- REPETIDO MAS BAJO
AS
	select ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TD.codDoc,TD.talonario,TD.nroDoc,TUB.color as color1,TTD.tipo,TUB.codigo as codSuc,TTP.codTipP,TTP.tipoP,TTP.efec,TD.datoPago,
	'doc'=case when TD.nroDoc<10 then '0000'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=10 and TD.nroDoc<100 then '000'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=100 and TD.nroDoc<1000 then '00'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=1000 and TD.nroDoc<10000 then '0'+ltrim(str(TD.nroDoc))else ltrim(str(TD.nroDoc)) end,
	TUB.ubicacion,TD.fechaDoc,TES.cantSal,TAR.artMod,TC.color,TT.talla,TES.preUniEnt as preCom,TES.preUniSal as valorUni,TS.codSer,TD.cliente,TD.ruc,TD.dni,TD.dir,TD.codCli,TD.IGV,
	CAST(TES.cantSal*TES.preUniSal as decimal(10,2)) as importe,TES.nroNota,TU.codUsu,TU.nomPers,TDA.codDetA,TDA.cant,TDA.codArt,TDA.codUbi,TMA.codMar,TMA.marca,TES.codDia,TAR.estado,TES.motivo
	from TDocumento TD join TSerie TS on TD.codSer=TS.CodSer
	join TTipoDoc TTD on TS.codTipD=TTD.codTipD
	join TTipoPago TTP on TD.codTipP=TTP.codTipP
	join TEntradaSalida TES on CAST(TD.codDoc as varchar(10))=TES.codRes
	join TUsuario TU on TES.codPers=TU.codUsu
	join TDetES TDE on TES.nroNota=TDE.nroNota
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA
	join TArticulo TAR on TDA.codArt=TAR.codArt
	join TMarca TMA on TAR.codMar=TMA.codMar 
	join TColor TC on TDA.codCol=TC.codCol
	join TTalla TT on TDA.idTalla=TT.idTalla
	join TUbicacion TUB on TDA.codUbi=TUB.codUbi
	where TES.codTrans=4 --codTrans=4 venta doc	
GO
---------SCRIPT PLANILLA---------------------------------------
	---PROCESOS ALMACEN
	--select * from TEntradaSalida where codTrans=1  --Entrada Recibido Proveedor
create view RecibProv
as
	select TA.codArt,TA.codTipA,SUM(cantEnt) as recProv,TA.artMod,ltrim(str(TA.codArt))+'Q' as cod,TA.codigo  
	from TEntradaSalida TES join TArticulo TA on TES.codArt=TA.codArt 
	where codTrans=1 and TA.estado=1 group by TA.codArt,TA.codTipA,TA.artMod,TA.codigo  --Entrada Recibido Proveedor
GO
	--select * from TEntradaSalida where codTrans=2  --Salida por Error
create view SalidaError	
as
	select codArt,SUM(cantSal) as salErr from TEntradaSalida where codTrans=2 group by codArt  --Salida por Error
GO
	--select * from TEntradaSalida where codTrans=10  --Salida Devolucion proveedor
create view DevolProv
as
	select codArt,SUM(cantSal) as salProv from TEntradaSalida where codTrans=10 group by codArt  --Salida Devolucion proveedor
GO
	--select * from TEntradaSalida where codTrans=9  --Salida OTROS
create view SalidaOtros
as
	select codArt,SUM(cantSal) as salNor from TEntradaSalida where codTrans=9 group by codArt  --Salida OTROS
GO
	--select * from TEntradaSalida where codTrans=12  --Salida Traspaso guia
create view SalidaGuia
as
	select codArt,SUM(cantSal) as salGuia from TEntradaSalida where codTrans=12 group by codArt --Salida Traspaso guia
GO
	--select * from TEntradaSalida where codTrans=8 --ANULACION GUIA ENTRADA
create view AnuGuia
as
	select codArt,SUM(cantEnt) as AnuGuia from TEntradaSalida where codTrans=8 group by codArt  --ANULACION GUIA ENTRADA
GO
	--select * from TEntradaSalida where codTrans=11 and codGuia1>0 --DEVOLUCION ALMACEN	
create view DevolAlmacen
as
	select codArt,SUM(cantEnt) as devAlm from TEntradaSalida where codTrans=11 and codGuia1>0 group by codArt  --DEVOLUCION ALMACEN	
GO	

create view VStockPlanilla
as
	Select TDA.codArt,sum(TDA.cant) as stock,TN.nombre,TN.codigo	
	from TDetalleArt TDA join TUbicacion TAL on TDA.codUbi=TAL.codUbi join TNegocioSuc TN on TAL.codigo=TN.codigo
	where TN.codigo='00-01' group by TDA.codArt,TN.codigo,TN.nombre --order by TDA.codArt
GO
	
create view PlanillaAlmacenBruto
as
	select RP.codArt,RP.codTipA,RP.recProv,isnull(SE.salErr,0) as salErr,isnull(DP.salProv,0) as salProv,ISNULL(SO.salNor,0) as salNor,
	ISNULL(SG.salGuia,0) as salGuia,ISNULL(AG.anuGuia,0) as anuGuia,ISNULL(DA.devAlm,0) as devAlm,ISNULL(stock,0) as stock,RP.artMod,RP.cod,RP.codigo  
	from RecibProv RP left join SalidaError SE on RP.codArt=SE.codArt
	left join DevolProv DP on RP.codArt=DP.codArt
	left join SalidaOtros SO on RP.codArt=SO.codArt
	left join SalidaGuia SG on RP.codArt=SG.codArt
	left join AnuGuia AG on RP.codArt=AG.codArt
	left join DevolAlmacen DA on RP.codArt=DA.codArt
	left join VStockPlanilla SP on RP.codArt=SP.codArt  
GO		

create view PlanillaAlmacen
as	
	select codArt,codTipA,recProv,salErr,recProv-salErr as saldo1,salProv,salNor,((recProv-salErr)-(salProv+salNor))+anuGuia as saldo2,salGuia,
	(((recProv-salErr)-(salProv+salNor))+anuGuia)-salGuia as saldo3,devAlm,((((recProv-salErr)-(salProv+salNor))+anuGuia)-salGuia)+devAlm as saldo4,stock,artMod,cod,codigo
	from PlanillaAlmacenBruto
GO


--select * from TEntradaSalida where codTrans=1 and codRes<>'00-01' -- ENTRADA X CAMBIO DE ARTICULO (ANULACION)
--update TEntradaSalida set codTrans=15 where codTrans=1 and codRes<>'00-01' -- ENTRADA X CAMBIO DE ARTICULO (ANULACION)
-----------------------------FIN PROCESOS PLANILLA ALMACEN-------------------------------------------------------------------------------------------

------------------------------------------------
-------------10/04/2012-------------------------
-------------EJECUTAR HOY-----------------------------------
---------SCRIPT PLANILLA PROCESOS TIENDA---------------------------------------
create view EstrucArt
as
	select codArt,codTipA,artMod,ltrim(str(codArt))+'Q' as cod,codigo from TArticulo 
	where estado=1 and codArt in(select distinct codArt from TEntradaSalida where codTrans=13 and codGuia>0) 
GO
	
	select * from TEntradaSalida where codTrans=13 and codGuia>0   --Entrada Recibido Almacen
create view RecibTienda
as
	select codRes,codArt,SUM(cantEnt) as recTie from TEntradaSalida where codTrans=13 and codGuia>0 group by codRes,codArt --Entrada Recibido Almacen
GO
	select * from TEntradaSalida where codTrans=16  --Salida Devolucion Almacen
create view DevolAlm
as
	select codRes,codArt,SUM(cantSal) as salAlm from TEntradaSalida where codTrans=16 group by codRes,codArt  --Salida Devolucion almacen
GO
	select * from TEntradaSalida where codTrans=18  --Entrada Anulacion Devolucion
create view AnulDev
as
	select codRes,codArt,SUM(cantEnt) as anuDev from TEntradaSalida where codTrans=18 group by codRes,codArt  --Entrada Anulacion Devolucion
GO
	select codArt,codigo,cantEnt from TEntradaSalida TES join TDetES TD on TES.nroNota=TD.nroNota join TUbicacion TU on TD.codUbi=TU.codUbi where codTrans=6 ORDER BY codArt  --Entrada Intercambio
create view InterEnt
as
	select codigo,codArt,SUM(cantEnt) as intEnt from TEntradaSalida TES join TDetES TD on TES.nroNota=TD.nroNota join TUbicacion TU on TD.codUbi=TU.codUbi where codTrans=6 group by codigo,codArt  --Entrada Intercambio
GO
	select * from TEntradaSalida where codTrans=7  --Salida Intercambio
create view InterSal
as
	select codRes,codArt,SUM(cantSal) as intSal from TEntradaSalida where codTrans=7 group by codRes,codArt  --Salida Intercambio
GO
----------------------------------------------------------------------------
	select codArt,codigo,cantSal from TEntradaSalida TES join TDetES TD on TES.nroNota=TD.nroNota join TUbicacion TU on TD.codUbi=TU.codUbi where codTrans=4 and ven=0  --VENTA CON DOCUMENTO
	select * from TEntradaSalida where codTrans=4 and ven=0  -- 0=Ven Doc
	update TEntradaSalida set ven=1 where codTrans=4 and motivo not like'VENTA DOCUMENTO%'  --1=Intercambio
create view VenDoc
as
	select codigo,codArt,SUM(cantSal) as ventaSal from TEntradaSalida TES join TDetES TD on TES.nroNota=TD.nroNota join TUbicacion TU on TD.codUbi=TU.codUbi where codTrans=4 and ven=0 group by codigo,codArt  ---VENTA CON DOCUMENTO  1=Venta Intercambio
GO
-----------------------------------------------------------------------------
	select * from TEntradaSalida where codTrans=15 and ven=1 --ENTRADA X CAMBIO DE ART
	update TEntradaSalida set ven=1 where codTrans=15 and motivo NOT like'ENTRADA POR ANULAR VENTA%'
create view CambEnt
as
	select codRes,codArt,SUM(cantEnt) as entVenAnu from TEntradaSalida where codTrans=15 and ven=1 group by codRes,codArt  --ENTRADA X CAMBIO DE ART
GO
--------------------------------------------------------------------------------
	select codArt,codigo,cantSal from TEntradaSalida TES join TDetES TD on TES.nroNota=TD.nroNota join TUbicacion TU on TD.codUbi=TU.codUbi where codTrans=4 and ven=1  --VENTA INTERCAMBIO
	select * from TEntradaSalida where codTrans=4 and ven=1  -- 1=VENTA INTERCAMBIO
create view VenCamb
as
	select codigo,codArt,SUM(cantSal) as salVenCam from TEntradaSalida TES join TDetES TD on TES.nroNota=TD.nroNota join TUbicacion TU on TD.codUbi=TU.codUbi where codTrans=4 and ven=1 group by codigo,codArt  ---VENTA INTERCAMBIO  1=Venta Intercambio
GO
--------------------------------------------------------------------------------------
create view VStockTienda
as
	Select TDA.codArt,sum(TDA.cant) as stock,TN.nombre,TN.codigo	
	from TDetalleArt TDA join TUbicacion TAL on TDA.codUbi=TAL.codUbi join TNegocioSuc TN on TAL.codigo=TN.codigo
	group by TN.codigo,TDA.codArt,TN.nombre
GO

--drop FUNCTION fn_PlanillaTiendaBruto
CREATE FUNCTION fn_PlanillaTiendaBruto(@codSuc varchar(10)) RETURNS table
AS
RETURN (select isnull(V1.codRes,V4.codigo) as codRes,VR.codArt,VR.codTipA,VR.cod,VR.codigo,VR.artMod,isnull(V1.recTie,0) as recTie,isnull(V2.salAlm,0) as salAlm,isnull(V3.anuDev,0) as anuDev,
		isnull(V4.intEnt,0) as intEnt,isnull(V5.intSal,0) as intSal,isnull(V6.ventaSal,0) as ventaSal,isnull(V7.entVenAnu,0) as entVenAnu,isnull(V8.salVenCam,0) as salVenCam,isnull(V9.stock,0) as stock
		from ((select codArt,codTipA,cod,codigo,artMod from EstrucArt) VR
		left join (select codRes,codArt,recTie from RecibTienda where codRes=@codSuc) V1 on VR.codArt=V1.codArt
		left join (select codArt,salAlm from DevolAlm where codRes=@codSuc) V2 on VR.codArt=V2.codArt
		left join (select codArt,anuDev from AnulDev where codRes=@codSuc) V3 on VR.codArt=V3.codArt
		left join (select codigo,codArt,intEnt from InterEnt where codigo=@codSuc) V4 on VR.codArt=V4.codArt
		left join (select codArt,intSal from InterSal where codRes=@codSuc) V5 on VR.codArt=V5.codArt
		left join (select codArt,ventaSal from VenDoc where codigo=@codSuc) V6 on VR.codArt=V6.codArt
		left join (select codArt,entVenAnu from CambEnt where codRes=@codSuc) V7 on VR.codArt=V7.codArt
		left join (select codArt,salVenCam from VenCamb where codigo=@codSuc) V8 on VR.codArt=V8.codArt
		left join (select codArt,stock from VStockTienda where codigo=@codSuc) V9 on VR.codArt=V9.codArt
		)where isnull(V1.RecTie,0)>0 or isnull(V4.intEnt,0)>0) 
GO
--drop FUNCTION fn_PlanillaTienda
CREATE FUNCTION fn_PlanillaTienda(@codSuc1 varchar(10)) RETURNS table
AS
	RETURN (select codArt,codTipA,cod,codigo,artMod,recTie,salAlm,RecTie-(salAlm-anuDev) as saldo1,intEnt,intSal,((RecTie-(salAlm-anuDev))+intEnt)-intSal as saldo2,
	ventaSal,entVenAnu,salVenCam,((((recTie-(salAlm-anuDev))+intEnt)-intSal)-((ventaSal-entVenAnu)+salVenCam)) as saldo3,stock,codRes 
	from (select codRes,codArt,codTipA,cod,codigo,artMod,RecTie,salAlm,anuDev,intEnt,intSal,ventaSal,entVenAnu,salVenCam,stock 
	from fn_PlanillaTiendaBruto(@codSuc1)) planilla) 
GO

select * from fn_PlanillaTiendaBruto('00-02') ORDER BY codArt
select codArt,codTipA,cod,codigo,artMod,recTie,salAlm,saldo1,intEnt,intSal,saldo2,ventaSal,entVenAnu,salVenCam,saldo3,stock,codRes from fn_PlanillaTienda('00-02') where codTipA=20 ORDER BY codArt
---------------------------------------------------------------------------------------------------------------------

------------------------------------------------
-------------30/05/2012-------------------------
-------------EJECUTAR HOY-----------------------------------
---------SCRIPT PLANILLA x COLORES PROCESOS TIENDA---------------------------------------
create view EstrucArt1
as
	select distinct TA.codArt,TA.codTipA,TA.artMod,ltrim(str(TA.codArt))+'Q' as cod,TA.codigo,TC.codCol,TC.color,
	ltrim(str(TA.codArt))+'-'+ltrim(str(TC.codCol)) as codArtCol 
	from TArticulo TA join TDetalleArt TDA on TA.codArt=TDA.codArt
	join TColor TC on TDA.codCol=TC.codCol 
	where estado=1 --order by TA.codArt
GO
	
create view RecibTienda1
as
	select TES.codRes,TES.codArt,SUM(cantEnt) as recTie,TDA.codCol,ltrim(str(TES.codArt))+'-'+ltrim(str(TDA.codCol)) as codArtCol 
	from TEntradaSalida TES join TDetES TDE on TES.nroNota=TDE.nroNota
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA 
	where codTrans=13 and codGuia>0 group by TES.codRes,TES.codArt,TDA.codCol --Entrada Recibido Almacen
GO
	
create view DevolAlm1
as
	select TES.codRes,TES.codArt,SUM(cantSal) as salAlm,TDA.codCol,ltrim(str(TES.codArt))+'-'+ltrim(str(TDA.codCol)) as codArtCol 
	from TEntradaSalida TES join TDetES TDE on TES.nroNota=TDE.nroNota
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA  
	where codTrans=16 group by TES.codRes,TES.codArt,TDA.codCol  --Salida Devolucion almacen
GO
	
create view AnulDev1
as
	select TES.codRes,TES.codArt,SUM(cantEnt) as anuDev,TDA.codCol,ltrim(str(TES.codArt))+'-'+ltrim(str(TDA.codCol)) as codArtCol 
	from TEntradaSalida TES join TDetES TDE on TES.nroNota=TDE.nroNota
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA  
	where codTrans=18 group by TES.codRes,TES.codArt,TDA.codCol  --Entrada Anulacion Devolucion
GO
	
create view InterEnt1
as
	select codigo,TES.codArt,SUM(cantEnt) as intEnt,TDA.codCol,ltrim(str(TES.codArt))+'-'+ltrim(str(TDA.codCol)) as codArtCol 
	from TEntradaSalida TES join TDetES TD on TES.nroNota=TD.nroNota 
	join TUbicacion TU on TD.codUbi=TU.codUbi 
	join TDetalleArt TDA on TD.codDetA=TDA.codDetA
	where codTrans=6 group by codigo,TES.codArt,TDA.codCol  --Entrada Intercambio
GO
	
create view InterSal1
as
	select TES.codRes,TES.codArt,SUM(cantSal) as intSal,TDA.codCol,ltrim(str(TES.codArt))+'-'+ltrim(str(TDA.codCol)) as codArtCol 
	from TEntradaSalida TES join TDetES TDE on TES.nroNota=TDE.nroNota
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA  
	where codTrans=7 group by TES.codRes,TES.codArt,TDA.codCol  --Salida Intercambio
GO
----------------------------------------------------------------------------
	
create view VenDoc1
as
	select codigo,TES.codArt,SUM(cantSal) as ventaSal,TDA.codCol,ltrim(str(TES.codArt))+'-'+ltrim(str(TDA.codCol)) as codArtCol 
	from TEntradaSalida TES join TDetES TD on TES.nroNota=TD.nroNota 
	join TUbicacion TU on TD.codUbi=TU.codUbi 
	join TDetalleArt TDA on TD.codDetA=TDA.codDetA
	where codTrans=4 and ven=0 group by codigo,TES.codArt,TDA.codCol  ---VENTA CON DOCUMENTO  1=Venta Intercambio
GO
-----------------------------------------------------------------------------

create view CambEnt1
as
	select codRes,TES.codArt,SUM(cantEnt) as entVenAnu,TDA.codCol,ltrim(str(TES.codArt))+'-'+ltrim(str(TDA.codCol)) as codArtCol 
	from TEntradaSalida TES join TDetES TD on TES.nroNota=TD.nroNota 
	join TUbicacion TU on TD.codUbi=TU.codUbi 
	join TDetalleArt TDA on TD.codDetA=TDA.codDetA 
	where codTrans=15 and ven=1 group by codRes,TES.codArt,TDA.codCol  --ENTRADA X CAMBIO DE ART
GO
--------------------------------------------------------------------------------
	
create view VenCamb1
as
	select codigo,TES.codArt,SUM(cantSal) as salVenCam,TDA.codCol,ltrim(str(TES.codArt))+'-'+ltrim(str(TDA.codCol)) as codArtCol 
	from TEntradaSalida TES join TDetES TD on TES.nroNota=TD.nroNota 
	join TUbicacion TU on TD.codUbi=TU.codUbi 
	join TDetalleArt TDA on TD.codDetA=TDA.codDetA 
	where codTrans=4 and ven=1 group by codigo,TES.codArt,TDA.codCol  ---VENTA INTERCAMBIO  1=Venta Intercambio
GO
--------------------------------------------------------------------------------------

create view VStockTienda1
as
	Select TDA.codArt,sum(TDA.cant) as stock,TN.nombre,TN.codigo,TDA.codCol,ltrim(str(TDA.codArt))+'-'+ltrim(str(TDA.codCol)) as codArtCol	
	from TDetalleArt TDA join TUbicacion TAL on TDA.codUbi=TAL.codUbi 
	join TNegocioSuc TN on TAL.codigo=TN.codigo
	group by TN.codigo,TDA.codArt,TDA.codCol,TN.nombre-- order by TDA.codArt
GO

--drop FUNCTION fn_PlanillaTiendaBruto1
CREATE FUNCTION fn_PlanillaTiendaBruto1(@codSuc varchar(10)) RETURNS table
AS
RETURN (select isnull(V1.codRes,V4.codigo) as codRes,VR.codArt,VR.codTipA,VR.cod,VR.codigo,VR.artMod,isnull(V1.recTie,0) as recTie,isnull(V2.salAlm,0) as salAlm,isnull(V3.anuDev,0) as anuDev,VR.codCol,VR.color,VR.codArtCol, 
		isnull(V4.intEnt,0) as intEnt,isnull(V5.intSal,0) as intSal,isnull(V6.ventaSal,0) as ventaSal,isnull(V7.entVenAnu,0) as entVenAnu,isnull(V8.salVenCam,0) as salVenCam,isnull(V9.stock,0) as stock
		from ((select codArt,codTipA,cod,codigo,artMod,codCol,color,codArtCol from EstrucArt1) VR
		left join (select codRes,codArt,recTie,codArtCol from RecibTienda1 where codRes=@codSuc) V1 on VR.codArtCol=V1.codArtCol
		left join (select codArt,salAlm,codArtCol from DevolAlm1 where codRes=@codSuc) V2 on VR.codArtCol=V2.codArtCol
		left join (select codArt,anuDev,codArtCol from AnulDev1 where codRes=@codSuc) V3 on VR.codArtCol=V3.codArtCol
		left join (select codigo,codArt,intEnt,codArtCol from InterEnt1 where codigo=@codSuc) V4 on VR.codArtCol=V4.codArtCol
		left join (select codArt,intSal,codArtCol from InterSal1 where codRes=@codSuc) V5 on VR.codArtCol=V5.codArtCol
		left join (select codArt,ventaSal,codArtCol from VenDoc1 where codigo=@codSuc) V6 on VR.codArtCol=V6.codArtCol
		left join (select codArt,entVenAnu,codArtCol from CambEnt1 where codRes=@codSuc) V7 on VR.codArtCol=V7.codArtCol
		left join (select codArt,salVenCam,codArtCol from VenCamb1 where codigo=@codSuc) V8 on VR.codArtCol=V8.codArtCol
		left join (select codArt,stock,codArtCol from VStockTienda1 where codigo=@codSuc) V9 on VR.codArtCol=V9.codArtCol
		)where isnull(V1.RecTie,0)>0 or isnull(V4.intEnt,0)>0) 
GO

--drop FUNCTION fn_PlanillaTienda1
CREATE FUNCTION fn_PlanillaTienda1(@codSuc1 varchar(10)) RETURNS table
AS
	RETURN (select codArt,codTipA,cod,codigo,artMod,recTie,salAlm,RecTie-(salAlm-anuDev) as saldo1,intEnt,intSal,((RecTie-(salAlm-anuDev))+intEnt)-intSal as saldo2,
	ventaSal,entVenAnu,salVenCam,((((recTie-(salAlm-anuDev))+intEnt)-intSal)-((ventaSal-entVenAnu)+salVenCam)) as saldo3,stock,codRes,codCol,color,codArtCol 
	from (select codRes,codArt,codTipA,cod,codigo,artMod,RecTie,salAlm,anuDev,intEnt,intSal,ventaSal,entVenAnu,salVenCam,stock,codCol,color,codArtCol 
	from fn_PlanillaTiendaBruto1(@codSuc1)) planilla) 
GO


---------SCRIPT PLANILLA---------------------------------------
	---PROCESOS ALMACEN CON COLOR

create view Recib1
as
	select distinct TES.nroNota,TA.codArt,TA.artMod,TA.codTipA,cantEnt,ltrim(str(TA.codArt))+'Q' as cod,TA.codigo,
	TDA.codCol,TC.color,ltrim(str(TA.codArt))+'-'+ltrim(str(TDA.codCol)) as codArtCol
	from TArticulo TA join TEntradaSalida TES on TA.codArt=TES.codArt 
	join TDetES TDE on TES.nroNota=TDE.nroNota 
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA
	join TColor TC on TDA.codCol=TC.codCol
	where TES.codTrans=1 and TA.estado=1  --Entrada Recibido Proveedor
GO

create view RecibProv1
as
	select codArt,codTipA,SUM(cantEnt) as recProv,artMod,cod,codigo,codCol,color,codArtCol
	from Recib1	group by codArt,codCol,codTipA,artMod,cod,codigo,color,codArtCol --order by codArt  --Entrada Recibido Proveedor
GO

create view SalidaError1	
as
	select TES.codArt,SUM(cantSal) as salErr,TDA.codCol,ltrim(str(TES.codArt))+'-'+ltrim(str(TDA.codCol)) as codArtCol 
	from TEntradaSalida	TES join TDetES TDE on TES.nroNota=TDE.nroNota 
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA 
	where codTrans=2 group by TES.codArt,TDA.codCol  --order by TES.codArt --Salida por Error
GO

create view DevolProv1
as
	select TES.codArt,SUM(cantSal) as salProv,TDA.codCol,ltrim(str(TES.codArt))+'-'+ltrim(str(TDA.codCol)) as codArtCol 
	from TEntradaSalida	TES join TDetES TDE on TES.nroNota=TDE.nroNota 
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA  
	where codTrans=10 group by TES.codArt,TDA.codCol  --Salida Devolucion proveedor
GO

create view SalidaOtros1
as
	select TES.codArt,SUM(cantSal) as salNor,TDA.codCol,ltrim(str(TES.codArt))+'-'+ltrim(str(TDA.codCol)) as codArtCol 
	from TEntradaSalida	TES join TDetES TDE on TES.nroNota=TDE.nroNota 
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA 
	where codTrans=9 group by TES.codArt,TDA.codCol  --Salida OTROS
GO
	
create view SalidaGuia1
as
	select TES.codArt,SUM(cantSal) as salGuia,TDA.codCol,ltrim(str(TES.codArt))+'-'+ltrim(str(TDA.codCol)) as codArtCol 
	from TEntradaSalida	TES join TDetES TDE on TES.nroNota=TDE.nroNota 
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA  
	where codTrans=12 group by TES.codArt,TDA.codCol --order by codArt--Salida Traspaso guia
GO

create view AnuGuia1
as
	select TES.codArt,SUM(cantEnt) as AnuGuia,TDA.codCol,ltrim(str(TES.codArt))+'-'+ltrim(str(TDA.codCol)) as codArtCol  
	from TEntradaSalida	TES join TDetES TDE on TES.nroNota=TDE.nroNota 
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA   
	where codTrans=8 group by TES.codArt,TDA.codCol  --ANULACION GUIA ENTRADA
GO
	
create view DevolAlmacen1
as
	select TES.codArt,SUM(cantEnt) as devAlm,TDA.codCol,ltrim(str(TES.codArt))+'-'+ltrim(str(TDA.codCol)) as codArtCol 
	from TEntradaSalida	TES join TDetES TDE on TES.nroNota=TDE.nroNota 
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA 
	where codTrans=11 and codGuia1>0 group by TES.codArt,TDA.codCol  --DEVOLUCION ALMACEN	
GO	

create view VStockPlanilla1
as
	Select TDA.codArt,sum(TDA.cant) as stock,TN.nombre,TN.codigo,TDA.codCol,ltrim(str(TDA.codArt))+'-'+ltrim(str(TDA.codCol)) as codArtCol	
	from TDetalleArt TDA join TUbicacion TAL on TDA.codUbi=TAL.codUbi join TNegocioSuc TN on TAL.codigo=TN.codigo
	where TN.codigo='00-01' group by TDA.codArt,TDA.codCol,TN.codigo,TN.nombre --order by TDA.codArt
GO
	
create view PlanillaAlmacenBruto1
as
	select RP.codArt,RP.codTipA,RP.recProv,isnull(SE.salErr,0) as salErr,isnull(DP.salProv,0) as salProv,ISNULL(SO.salNor,0) as salNor,
	ISNULL(SG.salGuia,0) as salGuia,ISNULL(AG.anuGuia,0) as anuGuia,ISNULL(DA.devAlm,0) as devAlm,ISNULL(stock,0) as stock,RP.artMod,RP.cod,RP.codigo,
	RP.codCol,RP.color,RP.codArtCol  
	from RecibProv1 RP left join SalidaError1 SE on RP.codArtCol=SE.codArtCol
	left join DevolProv1 DP on RP.codArtCol=DP.codArtCol
	left join SalidaOtros1 SO on RP.codArtCol=SO.codArtCol
	left join SalidaGuia1 SG on RP.codArtCol=SG.codArtCol
	left join AnuGuia1 AG on RP.codArtCol=AG.codArtCol
	left join DevolAlmacen1 DA on RP.codArtCol=DA.codArtCol
	left join VStockPlanilla1 SP on RP.codArtCol=SP.codArtCol --order by RP.codArt  
GO		

create view PlanillaAlmacen1
as	
	select codArt,codTipA,recProv,salErr,recProv-salErr as saldo1,salProv,salNor,((recProv-salErr)-(salProv+salNor))+anuGuia as saldo2,salGuia,codCol,color,codArtCol, 
	(((recProv-salErr)-(salProv+salNor))+anuGuia)-salGuia as saldo3,devAlm,((((recProv-salErr)-(salProv+salNor))+anuGuia)-salGuia)+devAlm as saldo4,stock,artMod,cod,codigo
	from PlanillaAlmacenBruto1
GO
-----------------------------FIN PROCESOS PLANILLA ALMACEN-------------------------------------------------------------------------------------------

----------INFORME STOCK LINEAL--------------
create view VArtTipoArtMarca1
as
	Select TA.codArt,TA.codigo,TA.artMod,TT.codTipA,TT.tipo,TM.codMar,TM.marca,ltrim(str(TA.codArt))+'Q' as cod 
	from TArticulo TA join TTipoArt TT on TA.codTipA=TT.codTipA
	join TMarca TM on TT.codMar=TM.codMar 
GO

create view VStockSuc1
as
	Select TDA.codArt,sum(TDA.cant) as stock,TN.nombre,TN.codigo,TC.codCol,TC.color	
	from TDetalleArt TDA join TUbicacion TAL on TDA.codUbi=TAL.codUbi 
	join TNegocioSuc TN on TAL.codigo=TN.codigo
	join TColor TC on TDA.codCol=TC.codCol
	where TDA.cant>0 group by TN.codigo,TDA.codArt,TC.codCol,TN.nombre,TC.color --order by TDA.codArt
GO

create view VStockCodigo1
as
	select VA.codArt,VA.codigo,VA.cod,VA.artMod,VA.codTipA,VA.tipo,VA.codMar,VA.marca,VS.stock,VS.nombre,VS.codigo as codSuc,VS.codCol,VS.color
	from VArtTipoArtMarca1 VA join VStockSuc1 VS on VA.codArt=VS.codArt --order by VA.codArt
GO

create view VTallasLinea1
as
	select TD.codArt,sum(TD.cant) as cant,TT.talla,TU.codigo as codSuc,TC.codCol,TC.color
	from TDetalleArt TD join TTalla TT on TD.idTalla=TT.idTalla
	join TUbicacion TU on TD.codUbi=TU.codUbi
	join TColor TC on TD.codCol=TC.codCol
	where TD.cant>0 group by TC.codCol,TT.talla,TU.codigo,TD.codArt,TC.color --order by TU.codigo,TD.codArt,TT.talla 
GO

create view VDocumentoVenta --REPETIDO VALE PARTE INFERIOR
AS
	select ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TD.codDoc,TD.talonario,TD.nroDoc,TUB.color as color1,TTD.tipo,TUB.codigo as codSuc,TTD.codTipD,TTP.codTipP,TTP.tipoP,TTP.efec,
	'doc'=case when TD.nroDoc<10 then '0000'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=10 and TD.nroDoc<100 then '000'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=100 and TD.nroDoc<1000 then '00'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=1000 and TD.nroDoc<10000 then '0'+ltrim(str(TD.nroDoc))else ltrim(str(TD.nroDoc)) end,
	TUB.ubicacion,TD.fechaDoc,TES.cantSal,TAR.artMod,TC.color,TT.talla,TES.preUniEnt as preCom,TES.preUniSal as valorUni,TD.datoPago,TD.cliente,TD.ruc,TD.dni,TD.dir,TD.codCli,TD.IGV,
	CAST(TES.cantSal*TES.preUniSal as decimal(10,2)) as importe,TES.nroNota,TU.codUsu,TU.nomPers,TDA.codDetA,TDA.cant,TDA.codArt,TDA.codUbi,TMA.codMar,TMA.marca,TES.codDia,TAR.estado
	from TDocumento TD join TSerie TS on TD.codSer=TS.CodSer
	join TTipoDoc TTD on TS.codTipD=TTD.codTipD
	join TTipoPago TTP on TD.codTipP=TTP.codTipP
	join TEntradaSalida TES on CAST(TD.codDoc as varchar(10))=TES.codRes
	join TUsuario TU on TES.codPers=TU.codUsu
	join TDetES TDE on TES.nroNota=TDE.nroNota
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA
	join TArticulo TAR on TDA.codArt=TAR.codArt
	join TMarca TMA on TAR.codMar=TMA.codMar 
	join TColor TC on TDA.codCol=TC.codCol
	join TTalla TT on TDA.idTalla=TT.idTalla
	join TUbicacion TUB on TDA.codUbi=TUB.codUbi
	where TES.codTrans=4 --codTrans=4 venta doc		02=doc
GO

--------------FIN STOCK LINEAL---------------------

------------------------------------------------
-------------13/06/2012-------------------------
--DROP view VDocumentoVenta 
create view VDocumentoVenta --Vista importante no modificar
AS
	select ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TD.codDoc,TD.talonario,TD.nroDoc,TUB.color as color1,TTD.tipo,TUB.codigo as codSuc,TTD.codTipD,TTP.codTipP,TTP.tipoP,TTP.efec,
	'doc'=case when TD.nroDoc<10 then '0000'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=10 and TD.nroDoc<100 then '000'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=100 and TD.nroDoc<1000 then '00'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=1000 and TD.nroDoc<10000 then '0'+ltrim(str(TD.nroDoc))else ltrim(str(TD.nroDoc)) end,
	TUB.ubicacion,TD.fechaDoc,TES.cantSal,TAR.artMod,TC.color,TT.talla,TES.preUniEnt as preCom,TES.preUniSal as valorUni,TD.datoPago,TD.cliente,TD.ruc,TD.dni,TD.dir,TD.codCli,TD.IGV,
	CAST(TES.cantSal*TES.preUniSal as decimal(10,2)) as importe,TES.nroNota,TU.codUsu,TU.nomPers,TDA.codDetA,TDA.cant,TDA.codArt,TDE.codUbi,TMA.codMar,TMA.marca,TES.codDia,TAR.estado
	from TDocumento TD join TSerie TS on TD.codSer=TS.CodSer
	join TTipoDoc TTD on TS.codTipD=TTD.codTipD
	join TTipoPago TTP on TD.codTipP=TTP.codTipP
	join TEntradaSalida TES on CAST(TD.codDoc as varchar(10))=TES.codRes
	join TUsuario TU on TES.codPers=TU.codUsu
	join TDetES TDE on TES.nroNota=TDE.nroNota
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA
	join TArticulo TAR on TDA.codArt=TAR.codArt
	join TMarca TMA on TAR.codMar=TMA.codMar 
	join TColor TC on TDA.codCol=TC.codCol
	join TTalla TT on TDA.idTalla=TT.idTalla
	join TUbicacion TUB on TDE.codUbi=TUB.codUbi
	where TES.codTrans=4 --codTrans=4 venta doc		02=doc
GO
--drop view VVentasAnuladas
create view VVentasAnuladas 
AS
	select ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TD.codDoc,TD.talonario,TD.nroDoc,TUB.color as color1,TTD.tipo,TUB.codigo as codSuc,TTD.codTipD,TTP.codTipP,TTP.tipoP,TTP.efec,
	'doc'=case when TD.nroDoc<10 then '0000'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=10 and TD.nroDoc<100 then '000'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=100 and TD.nroDoc<1000 then '00'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=1000 and TD.nroDoc<10000 then '0'+ltrim(str(TD.nroDoc))else ltrim(str(TD.nroDoc)) end,
	TUB.ubicacion,TD.fechaDoc,TES.cantSal,TAR.artMod,TC.color,TT.talla,TES.preUniEnt as preCom,TES.preUniSal as valorUni,TD.datoPago,TD.cliente,TD.ruc,TD.dni,TD.dir,TD.codCli,TD.IGV,
	CAST(TES.cantSal*TES.preUniSal as decimal(10,2)) as importe,TES.nroNota,TU.codUsu,TU.nomPers,TDA.codDetA,TDA.cant,TDA.codArt,TDE.codUbi,TMA.codMar,TMA.marca,TES.codDia,TAR.estado
	from TDocumento TD join TSerie TS on TD.codSer=TS.CodSer
	join TTipoDoc TTD on TS.codTipD=TTD.codTipD
	join TTipoPago TTP on TD.codTipP=TTP.codTipP
	join TEntradaSalida TES on CAST(TD.codDoc as varchar(10))=TES.codRes
	join TUsuario TU on TES.codPers=TU.codUsu
	join TDetES TDE on TES.nroNota=TDE.nroNota
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA
	join TArticulo TAR on TDA.codArt=TAR.codArt
	join TMarca TMA on TAR.codMar=TMA.codMar 
	join TColor TC on TDA.codCol=TC.codCol
	join TTalla TT on TDA.idTalla=TT.idTalla
	join TUbicacion TUB on TDE.codUbi=TUB.codUbi
	where TES.codTrans=5 --codTrans=5 anular doc	
GO
--drop view VVentaDoc 
create view VVentaDoc 
AS
	select ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TD.codDoc,TD.talonario,TD.nroDoc,TUB.color as color1,TTD.tipo,TUB.codigo as codSuc,TS.codSer,
	'doc'=case when TD.nroDoc<10 then '0000'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=10 and TD.nroDoc<100 then '000'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=100 and TD.nroDoc<1000 then '00'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=1000 and TD.nroDoc<10000 then '0'+ltrim(str(TD.nroDoc))else ltrim(str(TD.nroDoc)) end,
	TUB.ubicacion,TD.fechaDoc,TES.cantSal,TAR.artMod,TC.color,TT.talla,TES.preUniEnt as preCom,TES.preUniSal as valorUni,TTD.codTipD,TD.cliente,TD.ruc,TD.dni,TD.dir,TD.codCli,TD.IGV,
	CAST(TES.cantSal*TES.preUniSal as decimal(10,2)) as importe,TES.nroNota,TU.codUsu,TU.nomPers,TDA.codDetA,TDA.cant,TDA.codArt,TDE.codUbi,TMA.codMar,TMA.marca,TES.codDia,TAR.estado,TES.motivo
	from TDocumento TD join TSerie TS on TD.codSer=TS.CodSer
	join TTipoDoc TTD on TS.codTipD=TTD.codTipD
	join TEntradaSalida TES on CAST(TD.codDoc as varchar(10))=TES.codRes
	join TUsuario TU on TES.codPers=TU.codUsu
	join TDetES TDE on TES.nroNota=TDE.nroNota
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA
	join TArticulo TAR on TDA.codArt=TAR.codArt
	join TMarca TMA on TAR.codMar=TMA.codMar 
	join TColor TC on TDA.codCol=TC.codCol
	join TTalla TT on TDA.idTalla=TT.idTalla
	join TUbicacion TUB on TDE.codUbi=TUB.codUbi
	where TES.codTrans=4 --codTrans=4 venta doc
GO
--DROP view VVentas	
create view VVentas		
AS
	select ltrim(str(TAR.codArt))+'Q'+ltrim(str(TDA.nroDet)) as codigo,TD.codDoc,TD.talonario,TD.nroDoc,TUB.color as color1,TTD.tipo,TUB.codigo as codSuc,TTP.codTipP,TTP.tipoP,TTP.efec,TD.datoPago,
	'doc'=case when TD.nroDoc<10 then '0000'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=10 and TD.nroDoc<100 then '000'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=100 and TD.nroDoc<1000 then '00'+ltrim(str(TD.nroDoc)) when TD.nroDoc>=1000 and TD.nroDoc<10000 then '0'+ltrim(str(TD.nroDoc))else ltrim(str(TD.nroDoc)) end,
	TUB.ubicacion,TD.fechaDoc,TES.cantSal,TAR.artMod,TC.color,TT.talla,TES.preUniEnt as preCom,TES.preUniSal as valorUni,TS.codSer,TD.cliente,TD.ruc,TD.dni,TD.dir,TD.codCli,TD.IGV,
	CAST(TES.cantSal*TES.preUniSal as decimal(10,2)) as importe,TES.nroNota,TU.codUsu,TU.nomPers,TDA.codDetA,TDA.cant,TDA.codArt,TDE.codUbi,TMA.codMar,TMA.marca,TES.codDia,TAR.estado,TES.motivo
	from TDocumento TD join TSerie TS on TD.codSer=TS.CodSer
	join TTipoDoc TTD on TS.codTipD=TTD.codTipD
	join TTipoPago TTP on TD.codTipP=TTP.codTipP
	join TEntradaSalida TES on CAST(TD.codDoc as varchar(10))=TES.codRes
	join TUsuario TU on TES.codPers=TU.codUsu
	join TDetES TDE on TES.nroNota=TDE.nroNota
	join TDetalleArt TDA on TDE.codDetA=TDA.codDetA
	join TArticulo TAR on TDA.codArt=TAR.codArt
	join TMarca TMA on TAR.codMar=TMA.codMar 
	join TColor TC on TDA.codCol=TC.codCol
	join TTalla TT on TDA.idTalla=TT.idTalla
	join TUbicacion TUB on TDE.codUbi=TUB.codUbi
	where TES.codTrans=4 --codTrans=4 venta doc	
GO



select codigo,codArtP,marca,color,talla,precioV,ubicacion,artMod,codArt,codCol,cant,color1,nroDet,codUbi from VDetalleArtLista where codArt=@codArt or (codArt=@codArt1 and talla=@talla)

select * from TArticulo where codArt=3620
select * from TDetalleArt where codArt=3620

select distinct codigo,nombre,dir,fono,serieMaq from VNegocioSucLogin

select * from TNegocioSuc







































































