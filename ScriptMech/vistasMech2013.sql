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
			CASE WHEN TI.estado = 1 THEN 'Activo' ELSE 'Inactivo' END as estado1,TI.repres,TI.dni,TT.idTipId,TI.cuentaDet
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

create view VPagoDesembolso
as
	select TT.codTipP,TT.tipoP,TP.codPagD,TP.fecPago,TP.pagoDet,TP.montoPago,TP.idOP,TM.codMon,TM.moneda,TM.simbolo from 
	TTipoPago TT join TPagoDesembolso TP on TT.codTipP=TP.codTipP
	join TMoneda TM on TP.codMon=TM.codMon
GO

create view VOrdenTodoCad  
as	
	select TC.nroOrden,TC.nroO,TC.codIde,TC.fecOrden,TC.transfe,TC.atiendeCom,TC.plazoEnt,TF.codPag,TF.forma,TC.lugarEnt,TC.codPersO,TC.idSol,TI.cuentaDet,
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

create view VPersDesemTesoreria
as
	select TP.codPers,TP.nombre+' '+TP.apellido as nom,TP.dni,TPD.codPersDes,TPD.estDesem,TPD.tipoA,TPD.obserDesem,TPD.idOP,
	'estApro'=case when TPD.estDesem=1 then 'APROBADO' when TPD.estDesem=2 then 'OBSERVADO' else 'DENEGADO' end 
	from TPersDesem TPD join TPersonal TP on TPD.codPers=TP.codPers where TPD.estDesem=1 and tipoA=2 --1=aprobado 2=firma gerencia
GO

create view VBancoCuenta
as
	select TB.codBan,banco,idCue,nroCue,TM.codMon,TM.moneda,TM.simbolo,
	'banmon'=case when banco<>'' then banco+'  '+simbolo else '' end 
	from TBanco TB join TCuentaBan TC on TB.codBan=TC.codBan
	join TMoneda TM on TC.codMon=TM.codMon
	where TC.estado=1  --1=activo
GO 

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

create view VPersDesemFirma
as
	select TPD.codPersDes,TPD.idOP,TPD.estDesem,TPD.tipoA,TPD.obserDesem,TPD.fecFir,TP.codPers,TP.codTipU,TP.nombre+' '+TP.apellido as nom,
	TP.nombre+'  '+TP.apellido+' ' as nom1,'est'=+case when TPD.estDesem=1 then '[APROBADO]' when TPD.estDesem=2 then '[OBSERVADO]' else '[DENEGADO]' end 
	from TPersDesem TPD join TPersonal TP on TPD.codPers=TP.codPers
GO

CREATE FUNCTION FN_ConcaTPersDesem(@id int,@tipo int) RETURNS varchar(100)
BEGIN
	RETURN isnull((select nom1 from VPersDesemFirma where idOP=@id and tipoA=@tipo),'')
END
GO

CREATE FUNCTION FN_ConcaEstadoDesem(@id int,@tipo int) RETURNS varchar(100)
BEGIN
	RETURN isnull((select est from VPersDesemFirma where idOP=@id and tipoA=@tipo),'')
END
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

create view VDetCot  --Instanciado en varias interfaces
as
	select TD.codDetC,TD.cant,TD.descrip,TD.unidad,TD.precio,TD.subTotal,TD.codCot,TM.codMat,TM.material,TT.codTipM,TT.tipoM,
	'est'=case when TD.estado=0 then 'NO APROBADO' when TD.estado=1 then 'APROBADO' else 'RECHAZADO' end,TD.estado
	from TDetalleCot TD join TMaterial TM on TD.codMat=TM.codMat
	join TTipoMat TT on TM.codTipM=TT.codTipM	
GO

create view VPersDesemSol
as
	select TP.codPers,TP.codCar,TP.codTipU,TP.nombre,TP.dni,TP.apellido,TP.nombre+' '+TP.apellido as nom,TD.codPersDes,TD.idOP,TD.estDesem,TD.tipoA,TD.obserDesem
	from TPersonal TP join TPersDesem TD on TP.codPers=TD.codPers
	where TD.tipoA=1  --1=Solicitante
GO

create view VPagoDesemTesoreria
as
	select TT.codTipP,TT.tipoP,TT.nro,TP.codPagD,TP.fecPago,TP.nroP,TP.pagoDet,TM.codMon,TM.moneda,TM.simbolo,
	TP.montoPago,TP.montoD,TP.idOP,TP.idCue,TC.codCla,TC.clasif 
	from TTipoPago TT join TPagoDesembolso TP on TT.codTipP=TP.codTipP
	join TMoneda TM on TP.codMon=TM.codMon
	join TClasifPago TC on TP.codCla=TC.codCla
GO

create view VTSerie
as
	select TUS.codSP,TS.serie,TS.iniNroDoc,TS.descrip,TUS.codPers,TS.codSerO 
	from TSerieOrden TS join TSeriePers TUS on TS.codSerO=TUS.codSerO where TS.estado=1
GO

create view VOrdenComAper  
as	
	select nroOrden,nroO,codIde,fecOrden,codPers,codPag,igv,calIGV,codMon,atiendeCom,plazoEnt,celAti,lugarEnt,transfe,obsFac,codPersO,nroProf,idSol,codET,nota,
	codigo,estado,codCot,hist,'nro'=case when nroO<100 then '000'+ltrim(str(nroO)) when nroO>=100 and nroO<1000 then '00'+ltrim(str(nroO)) when nroO>=1000 and nroO<10000 then '0'+ltrim(str(nroO)) else ltrim(str(nroO)) end
	from TOrdenCompra where estado=0 --0=abierto
GO

	create view VOrdenDetOrden  --impresion
	as
		select TC.nroOrden,TC.nroO,TC.fecOrden,TI.codIde,TI.razon,TI.ruc,TI.repres,TI.celRpm,TI.fono+' '+TI.fax as fonos,TI.email,TI.dir,TC.estado,TC.nroProf,TC.idSol,TI.cuentaBan,TI.cuentaDet,
		'nro'=case when TC.nroO<100 then '000'+ltrim(str(TC.nroO)) when TC.nroO>=100 and TC.nroO<1000 then '00'+ltrim(str(TC.nroO)) when TC.nroO>=1000 and TC.nroO<10000 then '0'+ltrim(str(TC.nroO)) else ltrim(str(TC.nroO)) end,
		TC.transfe,TC.atiendeCom,TC.celAti,TC.plazoEnt,TF.codPag,TF.forma,TC.lugarEnt,TC.igv,TC.calIGV,TC.codPersO,TP2.nombre+' '+TP2.apellido as nomAte,TP2.dni,TP2.fono as fonoAte,
		TL.codigo,TL.lugar,TL.nombre,TP1.codPers,TP1.nombre+' '+TP1.apellido as nomRem,TP1.fono,TP1.email as emaRem,TC.obsFac,TC.codCot,TC.nota,
		TD.codDetO,TD.cant,TD.descrip as material,TD.unidad,TD.precio,TD.subTotal,TMO.codMon,TMO.moneda,TMO.simbolo,TE.codET,TE.nombre as trans,TE.ruc as rucT,TE.dir as dirT,TE.fono as fonoT,TE.contacto
		from TOrdenCompra TC join TPersonal TP1 on TC.codPers=TP1.codPers
		join TPersonal TP2 on TC.codPersO=TP2.codPers
		join TIdentidad TI on TC.codIde=TI.codIde
		join TDetalleOrden TD on TC.nroOrden=TD.nroOrden 
		join TLugarTrabajo TL on TC.codigo=TL.codigo
		join TFormaPago TF on TC.codPag=TF.codPag
		join TMoneda TMO on TC.codMon=TMO.codMon
		join TEmpTransp TE on TC.codET=TE.codET
	GO

create view VSerieOrdenPers
as
	select codSP,codPers,TSO.codSerO,serie,iniNroDoc
	from TSerieOrden TSO join TSeriePers TSP on TSO.codSerO=TSP.codSerO where estado=1
GO

create view VOrdenDesembolso  
as	
	select idOP,serie,nroDes,fecDes,monto,montoDet,montoDif,estado,'est'=case when estado=0 then 'PENDIENTE' when estado=1 then 'TERMINADO' when estado=2 then 'CERRADO' else 'ANULADO' end, 
	'nro'=case when nroDes<100 then '000'+ltrim(str(nroDes)) when nroDes>=100 and nroDes<1000 then '00'+ltrim(str(nroDes)) else '0'+ltrim(str(nroDes)) end,TM.codMon,TM.moneda,TM.simbolo,
	codigo,codIde,banco,nroCta,nroDet,datoReq,factCheck,bolCheck,guiaCheck,vouCheck,vouDCheck,reciCheck,otroCheck,descOtro,nroConfor,fecEnt,hist,codSerO
	from TOrdenDesembolso TOD join TMoneda TM on TOD.codMon=TM.codMon 
	where estado in (0,1) --0=pendiente 1=terminado
GO

create view VPersAprobadoSolicitante
as
	select TP.codPers,TP.nombre+' '+TP.apellido as nom,TPD.codPersDes,TPD.estDesem,TPD.tipoA,TPD.idOP
	from TPersDesem TPD join TPersonal TP on TPD.codPers=TP.codPers where TPD.estDesem=1 and tipoA=1 --1=aprobado 1=firma solicitante
GO

create view VPersAprobadoGerencia
as
	select TP.codPers,TP.nombre+' '+TP.apellido as nom,TPD.codPersDes,TPD.estDesem,TPD.tipoA,TPD.idOP
	from TPersDesem TPD join TPersonal TP on TPD.codPers=TP.codPers where TPD.estDesem=1 and tipoA=2 --1=aprobado 2=firma gerencia
GO

create view VPersAprobadoTesoreria
as
	select TP.codPers,TP.nombre+' '+TP.apellido as nom,TPD.codPersDes,TPD.estDesem,TPD.tipoA,TPD.idOP
	from TPersDesem TPD join TPersonal TP on TPD.codPers=TP.codPers where TPD.estDesem=1 and tipoA=3 --1=aprobado 3=firma tesoreria
GO

create view VOrdenDesemConta  
as	
	select TOD.idOP,serie,nroDes,fecDes,monto,montoDet,montoDif,TOD.estado,'est'=case when TOD.estado=0 then 'PENDIENTE' when TOD.estado=1 then 'TERMINADO' else 'ERROR' end,
	'nro'=case when nroDes<100 then '000'+ltrim(str(nroDes)) when nroDes>=100 and nroDes<1000 then '00'+ltrim(str(nroDes)) else '0'+ltrim(str(nroDes)) end,TM.codMon,TM.moneda,TM.simbolo,
	banco,nroCta,nroDet,datoReq,hist,TL.codigo,TL.nombre,TI.codIde,TI.razon,TI.ruc,TPS.codPers as codPersSol,TPS.nom as nomSol,TPG.codPers as codPersGer,TPG.nom as nomGer,isnull(TPT.codPers,0) as codPersTes,isnull(TPT.nom,'') as nomTes
	from TOrdenDesembolso TOD join TMoneda TM on TOD.codMon=TM.codMon 
	join TLugarTrabajo TL on TOD.codigo=TL.codigo
	join TIdentidad TI on TOD.codIde=TI.codIde
	join VPersAprobadoSolicitante TPS on TOD.idOP=TPS.idOP --1=Soicitante
	join VPersAprobadoGerencia TPG on TOD.idOP=TPG.idOP --2=Gerencia
	left join VPersAprobadoTesoreria TPT on TOD.idOP=TPT.idOP --3=Tesoreria
	where TOD.estado in(0,1) --0=pendiente 1=terminado
GO

create view VPersVerificado
as
	select distinct TP.codPers,TP.nombre+' '+TP.apellido as nom,TPD.codPersDes,TPD.estDesem,TPD.tipoA,TPD.idOP,TPD.obserDesem,
	'est'=case when TPD.estDesem=1 then '[ APROBADO ]' when TPD.estDesem=2 then '[ OBSERVADO ]' else '[ DENEGADO ]' end
	from TPersDesem TPD join TPersonal TP on TPD.codPers=TP.codPers 
	join TOrdenDesembolso TOD on TPD.idOP=TOD.idOP
	where tipoA in(1,2) and TOD.estado in(0,1) --0,1=pendiente,terminado    tipoA=1,2=solicitante,firma gerencia
GO

create view VOrdenNoEnlazada  
as	
	select TC.nroOrden,TC.nroO,TC.codIde,TC.fecOrden,TC.transfe,TC.atiendeCom,TC.plazoEnt,TF.codPag,TF.forma,TC.lugarEnt,TC.codPersO,TC.idSol,TI.cuentaDet,
	TC.codPers,'0' as estado,TC.obsFac,TC.codCot,TI.razon,TI.ruc,TI.cuentaBan,TM.codMon,TM.moneda,TM.simbolo,TC.igv,TC.calIGV,TL.codigo,TL.lugar,TL.nombre as obra,
	'nro'=case when TC.nroO<100 then '000'+ltrim(str(TC.nroO)) when TC.nroO>=100 and TC.nroO<1000 then '00'+ltrim(str(TC.nroO)) else '0'+ltrim(str(TC.nroO)) end
	from TOrdenCompra TC join TFormaPago TF on TC.codPag=TF.codPag
	join TMoneda TM on TC.codMon=TM.codMon
	join TIdentidad TI on TC.codIde=TI.codIde
	join TLugarTrabajo TL on TC.codigo=TL.codigo
	where TC.nroOrden not in(select nroOrden from TDesOrden)
GO

create view VOrdenDesemGerencia1  
as	
	select TOD.idOP,serie,nroDes,fecDes,monto,montoDet,montoDif,TOD.estado,'est'=case when TOD.estado=0 then 'PENDIENTE' when TOD.estado=1 then 'TERMINADO' when TOD.estado=2 then 'CERRADO' else 'ANULADO' end, 
	'nro'=case when nroDes<100 then '000'+ltrim(str(nroDes)) when nroDes>=100 and nroDes<1000 then '00'+ltrim(str(nroDes)) else '0'+ltrim(str(nroDes)) end,TM.codMon,TM.moneda,TM.simbolo,
	banco,nroCta,nroDet,datoReq,hist,TL.codigo,TL.nombre,TI.codIde,TI.razon,TI.ruc,TP.codPers,TP.nom,TP.dni,isnull(TP.codPersDes,0) as codPersDes,isnull(TP.estDesem,0) as estDesem,TP.obserDesem,isnull(TP.estApro,'') as estApro
	from TOrdenDesembolso TOD join TMoneda TM on TOD.codMon=TM.codMon 
	join TLugarTrabajo TL on TOD.codigo=TL.codigo
	join TIdentidad TI on TOD.codIde=TI.codIde
	left join VPersDesemGerencia TP on TOD.idOP=TP.idOP --2=gerencia
	where TOD.idOP not in(select idOP from TPagoDesembolso) and TOD.estado=0
GO

create view VOrdenCompraDetalle  
as
	select TD.codDetO,TD.cant,TD.unidad,TD.descrip,TD.precio,TD.subTotal,TOC.nroOrden,TOC.nroO,TOC.fecOrden,TOC.igv,TOC.calIGV,TOC.codMon,TDO.nroDO,TDO.idOP,
	'nro'=case when nroO<100 then '000'+ltrim(str(nroO)) when nroO>=100 and nroO<1000 then '00'+ltrim(str(nroO)) else '0'+ltrim(str(nroO)) end,
	TOC.idSol,TS.fecSol,isnull(DATEDIFF(dd,TOC.fecOrden,getdate()),0) as dias,TM.moneda,TM.simbolo 
	from TOrdenCompra TOC join TDetalleOrden TD on TOC.nroOrden=TD.nroOrden
	join TMoneda TM on TOC.codMon=TM.codMon
	join TDesOrden TDO on TOC.nroOrden=TDO.nroOrden 
	left join TSolicitud TS on TOC.idSol=TS.idSol
GO

create view VOrdenComAper1  
as	
	select TOC.nroOrden,nroO,codIde,fecOrden,codPers,codPag,igv,calIGV,codMon,atiendeCom,plazoEnt,celAti,lugarEnt,transfe,obsFac,codPersO,nroProf,idSol,codET,nota,isnull(idOP,0) as idOP,
	codigo,estado,codCot,hist,'nro'=case when nroO<100 then '000'+ltrim(str(nroO)) when nroO>=100 and nroO<1000 then '00'+ltrim(str(nroO)) when nroO>=1000 and nroO<10000 then '0'+ltrim(str(nroO)) else ltrim(str(nroO)) end
	from TOrdenCompra TOC left join TDesOrden TDO on TOC.nroOrden=TDO.nroOrden 
	where estado=0 --0=abierto
GO

create view VMaterialStock
as
	select TM.codMat,material,TU1.unidad as uniBase,preBase,estado,TT.codTipM,TT.tipoM,isnull(TMS.stock,-1) as stock,TM.codUni,TM.hist
	from TMaterial TM join TUnidad TU1 on TM.codUni=TU1.codUni
	join TTipoMat TT on TM.codTipM=TT.codTipM
	left join (select codMat,SUM(stock) as stock from TMatUbi group by codMat) TMS on TM.codMat=TMS.codMat
	where TM.estado=1
GO

--Nueva Vista para Seguimiento de Desembolso
create view VOrdenDesembolsoSeguimiento
as
select TOD.idOP,tod.codigo as codObra,tod.serie,tod.nroDes,tod.fecDes,tod.monto,tod.montoDet,tod.montoDif,
'estado_desembolso'=case when TOD.estado=0 then 'PENDIENTE' when TOD.estado=1 then 'TERMINADO' when TOD.estado=2 then 'CERRADO' else 'ANULADO' end, 
	'nro'=case when tod.nroDes<100 then '000'+ltrim(str(tod.nroDes)) when tod.nroDes>=100 and tod.nroDes<1000 then '00'+ltrim(str(tod.nroDes)) else '0'+ltrim(str(tod.nroDes)) end,
	TM.codMon,TM.moneda,TM.simbolo,
	TLU.nombre as 'obra',Tid.razon as 'proveedor',tod.banco,tod.nroCta,tod.nroDet,tod.datoReq,tod.factCheck,tod.bolCheck,tod.guiaCheck,
	tod.vouCheck,tod.vouDCheck,tod.reciCheck,tod.otroCheck,tod.descOtro,tod.nroConfor,tod.fecEnt,tod.hist
	,(TPE.nombre +' '+ TPE.apellido) as solicitante,tid.codIde  ,TID.ruc, TID.fono,TID.email
	--,toc.nroOrden as idCompra,toc.nroO as nroCompra     
	from TOrdenDesembolso TOD 
	join TMoneda TM on TOD.codMon=TM.codMon 
	join  TIdentidad TID on TID.codIde=tod.codIde 
	join TLugarTrabajo TLU on tlu.codigo=TOD.codigo  
	inner join TPersDesem TPDE on TPDE.idOP= TOD.idOP 
	join TPersonal TPE on TPE.codPers = TPDE.codPers  
	--join TDesOrden TDECO on tdeco.idOP = tod.idOP  
	--join TOrdenCompra TOC on toc.nroOrden =tdeco.nroOrden   
	where TPDE.tipoA=1	
GO

-- Nueva Vista para Seguimeinto de Desembolso
--Pago de desembolsos
create view VPagoDesembolsoSeguimiento
as
	select  TP.idOP as codDesembolso,TP.fecpago,TP.pagoDet,Tp.montoPago,TT.tipoP,Tm.moneda,TM.simbolo,TCBA.nroCue,TBA.banco,
	TP.nroP,TP.montoD,TCLA.clasif                              
from TPagoDesembolso TP 
join TTipoPago TT  on TT.codTipP=TP.codTipP
join TMoneda TM on TP.codMon=TM.codMon 
join TCuentaBan TCBA on TCBA.idCue=TP.idCue  
join TBanco TBA on TCBA.codBan = TBA.codBan 
join TClasifPago TCLA on tCLA.codCla=TP.codCla  
GO

 --vista para mostrar las aprobaciones de la orden de desembolso
create view VAprobacionesSeguimiento
as
Select tpd.idOP, tpe.nombre,tpe.apellido ,'Area'=case when tpd.tipoA=1 then 'SOLICITANTE' when tpd.tipoA=2 then 'GERENCIA' when tpd.tipoA=3 then 'TESORERIA' when tpd.tipoA=4 then 'CONTABILIDAD' end, 
'Estado'=case  when tpd.estDesem =1 then 'APROBADO' when tpd.estDesem=2 then 'OBSERVADO' when tpd.estDesem =3 then  'RECHAZADO' else 'PENDIENTE' end,
obserDesem,fecFir  
   from TPersDesem TPD 
   join TPersonal TPE on tpd.codPers=tpe.codPers 
   join TOrdenDesembolso TOD on tpd.idOP=tod.idOP 
  -- where tod.idOP= 2
GO

create view VKardex
as
	select TES.nroNota,TES.fecha,TM.codMat,TM.material,TU.unidad,TES.idMU,TUB.codUbi,TUB.ubicacion,TUB.color,TL.codigo,TL.nombre,
	TES.cantEnt,TES.preUniEnt,TES.cantSal,TES.preUniSal,TES.codGuia,TES.nroGuia,TES.codDoc,TES.nroDoc,TES.otroDoc,
	TT.codTrans,TT.tipo,TP.codPers,TP.nombre+' '+TP.apellido as nom,TES.obs,TS.codSal,TS.saldo,TS.codLug
	from TEntradaSalida TES join TTipoTransac TT on TES.codTrans=TT.codTrans
	join TSaldo TS on TES.codSal=TS.codSal
	join TMaterial TM on TES.codMat=TM.codMat
	join TUnidad TU on TM.codUni=TU.codUni
	join TUbicacion TUB on TES.codUbi=TUB.codUbi
	join TLugarTrabajo TL on TUB.codigo=TL.codigo
	join TPersonal TP on TES.codUsu=TP.codPers
GO

create view VPersDesemSolic
as
	select TP.codPers,TP.nombre+' '+TP.apellido as nom,TP.dni,TPD.codPersDes,TPD.estDesem,TPD.tipoA,TPD.obserDesem,TPD.idOP
	from TPersDesem TPD join TPersonal TP on TPD.codPers=TP.codPers where tipoA=1 -- 1=firma solicitante
GO
--DROP view VOrdenDesemTesoreria
create view VOrdenDesemTesoreria  
as	
	select TOD.idOP,serie,nroDes,fecDes,monto,montoDet,montoDif,TOD.estado,'est'=case when TOD.estado=0 then 'PENDIENTE' when TOD.estado=1 then 'TERMINADO' when TOD.estado=2 then 'CERRADO' else 'ANULADO' end,TOD.codSerO, 
	'nro'=case when nroDes<100 then '000'+ltrim(str(nroDes)) when nroDes>=100 and nroDes<1000 then '00'+ltrim(str(nroDes)) else '0'+ltrim(str(nroDes)) end,TM.codMon,TM.moneda,TM.simbolo,TP1.codPers as codPersSol,TP1.nom as nomSol,
	banco,nroCta,nroDet,datoReq,hist,TL.codigo,TL.nombre,TI.codIde,TI.razon,TI.ruc,TP.codPers,TP.nom,TP.dni,isnull(TP.codPersDes,0) as codPersDes,isnull(TP.estDesem,0) as estDesem,TP.obserDesem,isnull(TP.estApro,'') as estApro
	from TOrdenDesembolso TOD join TMoneda TM on TOD.codMon=TM.codMon 
	join TLugarTrabajo TL on TOD.codigo=TL.codigo
	join TIdentidad TI on TOD.codIde=TI.codIde
	join VPersDesemTesoreria TP on TOD.idOP=TP.idOP --2=gerencia
	join VPersDesemSolic TP1 on TOD.idOP=TP1.idOP
	where TOD.estado=0 --0=pendiente 1=terminado
GO

create view VLugarObraAlmacen  
as
	select TL.codigo,TL.nombre,TU.codUbi,TU.ubicacion,TU.estado,TL.color,TL.lugar 
	from TLugarTrabajo TL join TUbicacion TU on TL.codigo=TU.codigo
	where TU.estado=1  --1=Activo
GO
-- DROP VIEW VCajaChica
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

create view VTransporte
as
	select TE.codET,TE.nombre as razon,TE.ruc,TV.codVeh,TV.marcaNro,TV.nroConst,TT.codT,TT.nombre,TT.DNI,TT.nroLic
	from TEmpTransp TE join TVehiculo TV on TE.codET=TV.codET
	join TTransportista TT on TE.codET=TT.codET
GO

create view VStockUbi
as
	select TL.codigo,TL.nombre as obra,TL.estado,TU.codUbi,TU.ubicacion,TU.estado as estUbi,TU.color,TMU.idMU,TMU.codMat,TMU.stock
	from TLugarTrabajo TL join TUbicacion TU on TL.codigo=TU.codigo
	join TMatUbi TMU on TU.codUbi=TMU.codUbi
	where TU.estado=1 -- solo almacen activo
GO
--DROP view VGuiaRemEmpAper
create view VGuiaRemEmpAper  
as	
	select codGuiaE,talon,nroGuia,fecIni,codSerS,codIde,TG.estado,codUbiOri,codUbiDes,partida,llegada,codVeh,codT,codMotG,nroFact,obs,codPers,hist,TU1.codigo as codObraOri,TU2.codigo as codObraDes,codET,TG.codIdeProv,
	talon+' - '+case when nroGuia<100 then '000'+ltrim(str(nroGuia)) when nroGuia>=100 and nroGuia<1000 then '00'+ltrim(str(nroGuia)) when nroGuia>=1000 and nroGuia<10000 then '0'+ltrim(str(nroGuia)) else ltrim(str(nroGuia)) end as nro
	from TGuiaRemEmp TG join TUbicacion TU1 on TG.codUbiOri=TU1.codUbi
	join TUbicacion TU2 on TG.codUbiDes=TU2.codUbi 
	where TG.estado in(0,1) --0=abierto 1=terminado
GO
--DROP view VDetGuiaE
create view VDetGuiaE --utilizado en dos interfaces
as
	select TD.codDGE,TD.codigo,TD.cant,TD.descrip,TD.unidad,TD.peso,TD.codGuiaE,TD.linea1,TM.codMat,TM.material,TM.codTipM,TD.codPers,TP.nombre+' '+TP.apellido as nomRec,
	TD.descrip+'. '+TD.linea1 as detalle,TD.entregado,'entre'=case when TD.entregado=0 then 'Pendiente' else 'Entregado' end,TD.recibido,TD.obsR,
	'recib'=case when TD.recibido=0 then 'Pendiente' when TD.recibido=1 then 'Recibido' else 'Incompleto' end
	from TDetalleGuiaEmp TD join TMaterial TM on TD.codMat=TM.codMat
	left join TPersonal TP on TD.codPers=TP.codPers
GO

create view VGuiaDetGuiaE  --impresion
as
		select TG.codGuiaE,TG.talon+' - '+ltrim(str(TG.nroGuia)) as nroGuia,TG.fecIni,TG.codSerS,TI.codIde,TI.razon,TI.ruc,TG.estado,TG.codUbiOri,TU1.ubicacion as almOri,
		TG.codUbiDes,TU2.ubicacion as almDes,TG.partida,TG.llegada,TE.nombre as empresa,TE.ruc as rucEmp,TV.marcaNro,TV.nroConst,TT.nroLic,TT.nombre as nomTrans,TT.DNI,TM.codMotG,TM.motivo,TG.nroFact,TG.obs,TG.codPers,
		TD.codDGE,TD.codigo,TD.cant,TD.descrip+'. '+TD.linea1 as descrip,TD.unidad,TD.peso,TD.codMat
		from TGuiaRemEmp TG join TIdentidad TI on TG.codIde=TI.codIde
		join TDetalleGuiaEmp TD on TG.codGuiaE=TD.codGuiaE 
		join TUbicacion TU1 on TG.codUbiOri=TU1.codUbi
		join TUbicacion TU2 on TG.codUbiDes=TU2.codUbi
		join TVehiculo TV on TG.codVeh=TV.codVeh
		join TTransportista TT on TG.codT=TT.codT
		join TEmpTransp TE on TV.codET=TE.codET
		join TMotivoGuia TM on TG.codMotG=TM.codMotG
GO
--DROP view VLugarUbiStoc
create view VLugarUbiStoc
as
	select TL.codigo,TL.nombre,TL.estado,TU.codUbi,TU.ubicacion,TU.estado as estUbi
	from TLugarTrabajo TL join TUbicacion TU on TL.codigo=TU.codigo
	where TU.estado=1  --1=ALMACENES ACTIVOS
GO
--DROP view VKardex1
create view VKardex1
as
	select TES.nroNota,TES.fecha,TM.codMat,TM.material,TU.unidad,TES.idMU,TUB1.codUbi,TUB1.ubicacion,TUB1.color,TL1.codigo,TL1.nombre,TES.cantEnt,TES.preUniEnt,TES.codProv,TI.razon as provee,TI.ruc,
	TES.cantSal,TES.preUniSal,TES.codGuia,TES.nroGuia,TES.codDoc,TES.nroDoc,TES.otroDoc,TT.codTrans,TT.tipo,TES.codUsu,TP1.nombre+' '+TP1.apellido as usuario,TES.obs,
	TS.codSal,TS.saldo,TS.codLug,TES.codUbiDes,TUB2.ubicacion as almObra,TUB2.color as colorDes,TL2.codigo as codigoObra,TL2.nombre as nomObraDes,TES.codPers,TP2.nombre+' '+TP2.apellido as nomRecibe,
	TES.vanET,'veri'=case when TES.vanET=0 and TES.codTrans=2 then 'Pendiente' when TES.vanET=1 and TES.codTrans=2 then 'Recibido' when TES.vanET=3 and TES.codTrans=2 then 'Incompleto' else '' end  --3=incompleto 2=Salida
	from TEntradaSalida TES join TTipoTransac TT on TES.codTrans=TT.codTrans
	join TSaldo TS on TES.codSal=TS.codSal
	join TMaterial TM on TES.codMat=TM.codMat
	join TUnidad TU on TM.codUni=TU.codUni
	join TUbicacion TUB1 on TES.codUbi=TUB1.codUbi
	join TLugarTrabajo TL1 on TUB1.codigo=TL1.codigo
	join TPersonal TP1 on TES.codUsu=TP1.codPers
	left join TPersonal TP2 on TES.codPers=TP2.codPers
	left join TUbicacion TUB2 on TES.codUbiDes=TUB2.codUbi
	left join TLugarTrabajo TL2 on TUB2.codigo=TL2.codigo
	left join TIdentidad TI on TES.codProv=TI.codIde
GO

create view VGuiaRemEmpEnt  
as	
	select codGuiaE,talon,nroGuia,fecIni,codSerS,TG.estado,codUbiOri,codUbiDes,TU1.ubicacion+' - '+partida as partida,TU2.ubicacion+' - '+llegada as llegada,
	nroFact,obs,hist,TU1.codigo as codObraOri,TU2.codigo as codObraDes,TI.codIde,TI.razon,TI.ruc,TE.codET,TE.nombre as empTra,TE.ruc as rucTra,
	TV.codVeh,TV.marcaNro,TV.nroConst,TT.codT,TT.nombre as nomTra,TT.DNI,TT.nroLic,TM.codMotG,TM.motivo,TP.codPers,TP.nombre+' '+TP.apellido as nomPers,
	talon+' - '+case when nroGuia<100 then '000'+ltrim(str(nroGuia)) when nroGuia>=100 and nroGuia<1000 then '00'+ltrim(str(nroGuia)) when nroGuia>=1000 and nroGuia<10000 then '0'+ltrim(str(nroGuia)) else ltrim(str(nroGuia)) end as nro
	from TGuiaRemEmp TG join TUbicacion TU1 on TG.codUbiOri=TU1.codUbi
	join TUbicacion TU2 on TG.codUbiDes=TU2.codUbi
	join TIdentidad TI on TG.codIde=TI.codIde 
	join TEmpTransp TE on TG.codET=TE.codET
	join TVehiculo TV on TG.codVeh=TV.codVeh
	join TTransportista TT on TG.codT=TT.codT
	join TMotivoGuia TM on TG.codMotG=TM.codMotG
	join TPersonal TP on TG.codPers=TP.codPers
	where TG.codSerS>1 and TG.estado=0 --0=abierto  1=reservado guia remision provee
GO

---Vistas para seguimiento de Guias de Remision
---Ejecutar en BD Web 10-08-2013
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
GO

create view VGuiaRemEmpAperProv  
as	
	select codGuiaE,talon,nroGuia,fecIni,codSerS,codIde,TG.estado,codUbiOri,codUbiDes,partida,llegada,codVeh,codT,codMotG,nroFact,obs,codPers,hist,
	TU2.codigo as codObraDes,codET,TG.codIdeProv,talon+' - '+ltrim(str(nroGuia)) as nro
	from TGuiaRemEmp TG join TUbicacion TU2 on TG.codUbiDes=TU2.codUbi 
	where TG.estado in(0,1) --0=abierto 1=terminado
GO

create view VGuiaRemProvEnt  
as	
	select codGuiaE,talon,nroGuia,fecIni,codSerS,TG.estado,codUbiOri,codUbiDes,partida,TU2.ubicacion+' - '+llegada as llegada,
	nroFact,obs,hist,TU2.codigo as codObraDes,TI.codIde,TI.razon,TI.ruc,TE.codET,TE.nombre as empTra,TE.ruc as rucTra,talon+' - '+ltrim(str(nroGuia)) as nro,
	TV.codVeh,TV.marcaNro,TV.nroConst,TT.codT,TT.nombre as nomTra,TT.DNI,TT.nroLic,TM.codMotG,TM.motivo,TP.codPers,TP.nombre+' '+TP.apellido as nomPers
	from TGuiaRemEmp TG join TUbicacion TU2 on TG.codUbiDes=TU2.codUbi
	join TIdentidad TI on TG.codIde=TI.codIde 
	join TEmpTransp TE on TG.codET=TE.codET
	join TVehiculo TV on TG.codVeh=TV.codVeh
	join TTransportista TT on TG.codT=TT.codT
	join TMotivoGuia TM on TG.codMotG=TM.codMotG
	join TPersonal TP on TG.codPers=TP.codPers
	where TG.codSerS=1 and TG.estado=0 --0=abierto  1=reservado guia remision provee
GO

create view VSeguimientoGR_Proveedor
as
SELECT TGR.codGuiaE, TGR.talon, TGR.nroGuia, TGR.fecIni, TGR.codSerS, TID.razon, TGR.codIde, TGR.estado AS codestado, 
CASE WHEN TGR.estado = 0 THEN 'ABIERTO' WHEN TGR.estado = 1 THEN 'TERMINADO' WHEN TGR.estado = 2 THEN 'CERRADO' ELSE 'ANULADO' END
AS Estado, TU2.ubicacion AS Destino, TGR.codUbiOri, TGR.codUbiDes, TGR.partida, TGR.llegada, TGR.codET, TET.nombre AS empTrans, 
TVE.marcaNro, TGR.codVeh, TCO.nombre, TGR.codT, TMO.motivo, TGR.codMotG, TGR.nroFact, TGR.obs, TPE.nombre + TPE.apellido AS Personal, 
TGR.codPers, TID.ruc
FROM mech.TGuiaRemEmp AS TGR INNER JOIN
mech.TUbicacion AS TU2 ON TU2.codUbi = TGR.codUbiDes INNER JOIN
mech.TIdentidad AS TID ON TID.codIde = TGR.codIde INNER JOIN
mech.TEmpTransp AS TET ON TET.codET = TGR.codET INNER JOIN
mech.TVehiculo AS TVE ON TVE.codVeh = TGR.codVeh INNER JOIN
mech.TTransportista AS TCO ON TCO.codT = TGR.codT INNER JOIN
mech.TMotivoGuia AS TMO ON TMO.codMotG = TGR.codMotG INNER JOIN
mech.TPersonal AS TPE ON TPE.codPers = TGR.codPers INNER JOIN
mech.TSerieSede AS TSE ON TSE.codSerS = TGR.codSerS
WHERE     (TGR.codSerS = 1)
go

create view VMaterialStockObra
as
	select TM.codMat,material,TU1.unidad as uniBase,preBase,estado,TT.codTipM,TT.tipoM,isnull(TMS.stock,-1) as stock,TM.codUni,TM.hist,TMS.codigo
	from TMaterial TM join TUnidad TU1 on TM.codUni=TU1.codUni
	join TTipoMat TT on TM.codTipM=TT.codTipM
	join (select codMat,SUM(stock) as stock,codigo from TMatUbi TMU join TUbicacion TU on TMU.codUbi=TU.codUbi group by TU.codigo,codMat) TMS on TM.codMat=TMS.codMat
	where TM.estado=1
GO
---------------------------------------
-------EJECUTAR 20/08/2013-------------
---------------------------------------
CREATE FUNCTION fn_MatStockObra(@cod varchar(10)) RETURNS table
AS
	RETURN (select TM.codMat,material,TU1.unidad as uniBase,preBase,estado,TT.codTipM,TT.tipoM,isnull(TMS.stock,-1) as stock,TM.codUni,TM.hist,TMS.codigo
	from TMaterial TM join TUnidad TU1 on TM.codUni=TU1.codUni
	join TTipoMat TT on TM.codTipM=TT.codTipM
	left join (select codMat,SUM(stock) as stock,codigo from TMatUbi TMU join TUbicacion TU on TMU.codUbi=TU.codUbi where codigo=@cod group by TU.codigo,codMat) TMS on TM.codMat=TMS.codMat
	where TM.estado=1)
GO

--DROP view VOrdenDesembolsoImprimir
create view VOrdenDesembolsoImprimir
as
	select TD.idOP,TD.serie+' - '+ltrim(str(TD.nroDes)) as nro,TD.fecDes,TM.codMon,TM.moneda,TM.simbolo,TD.monto,TD.montoDet,TD.montoDif,TD.estado,TL.codigo,TL.nombre as obra,
	TI.codIde,TI.razon,TI.ruc,TI.fono,TI.email,TD.banco,TD.nroCta,TD.nroDet,TD.datoReq,TD.factCheck,'faCheck'=case when TD.factCheck=1 then 'X' else '' end, 
	TD.bolCheck,'boCheck'=case when TD.bolCheck=1 then 'X' else '' end,TD.guiaCheck,'guCheck'=case when TD.guiaCheck=1 then 'X' else '' end,
	TD.vouCheck,'voCheck'=case when TD.vouCheck=1 then 'X' else '' end,TD.vouDCheck,'voDCheck'=case when TD.vouDCheck=1 then 'X' else '' end,
	TD.reciCheck,'reCheck'=case when TD.reciCheck=1 then 'X' else '' end,TD.otroCheck,TD.descOtro,'otCheck'=case when TD.otroCheck=1 then 'X' else '' end,
	TD.nroConfor,TD.fecEnt,TD.hist,mech.FN_ConcaTPersDesem(TD.idOP,1) as soli,mech.FN_ConcaTPersDesem(TD.idOP,2) as gere,mech.FN_ConcaTPersDesem(TD.idOP,3) as teso,mech.FN_ConcaTPersDesem(TD.idOP,4) as conta,
	mech.FN_ConcaEstadoDesem(TD.idOP,1) as soliEst,mech.FN_ConcaEstadoDesem(TD.idOP,2) as gereEst,mech.FN_ConcaEstadoDesem(TD.idOP,3) as tesoEst,mech.FN_ConcaEstadoDesem(TD.idOP,4) as contaEst,
	TT.codTipP,TT.tipoP,TP.codPagD,TP.fecPago,TP.nroP,TP.pagoDet,TMO.codMon as codMon1,TMO.moneda as moneda1,TMO.simbolo as simbolo1,TP.montoPago,TP.montoD,TC.codCla,TC.clasif,TB.codBan,TB.banco as bancoPago
	from TOrdenDesembolso TD join TMoneda TM on TD.codMon=TM.codMon
	join TLugarTrabajo TL on TD.codigo=TL.codigo
	join TIdentidad TI on TD.codIde=TI.codIde
	left join TPagoDesembolso TP on TD.idOP=TP.idOP
	left join TTipoPago TT on TP.codTipP=TT.codTipP
	left join TMoneda TMO on  TP.codMon=TMO.codMon
	left join TClasifPago TC on TP.codCla=TC.codCla
	left join TCuentaBan TCB on TP.idCue=TCB.idCue
	left join TBanco TB on TCB.codBan=TB.codBan
GO


---Vistas para reportes 

--Muestra los materiales ingresado por Obra
create view VMaterialObra
as
select TMA.codMat, tma.material, TUN.unidad ,tma.codUni, tma.preBase, tma.codTipM,TTI.tipoM, 
TMU.stock,TMU.codUbi   
from TMaterial TMA
join TTipoMat TTI on TTI.codTipM = TMA.codTipM 
join TUnidad TUN on TUN.codUni = TMA.codUni    
join TMatUbi TMU On tmu.codMat = TMA.codMat   
go

--vista para gastos por dia
create view VGastosPorDia
as
SELECT TPD.fecPago, TPD.nroP AS nroOperacion, TPD.pagoDet AS concepto, TTP.tipoP, TMO.simbolo,TMO.codMon, TPD.montoPago, TPD.montoD, TOD.serie, TOD.nroDes, 
TBCO.banco, TBCO.codBan, TCU.nroCue,TCU.idCue, TID.ruc, TID.razon
FROM mech.TPagoDesembolso AS TPD 
INNER JOIN mech.TCuentaBan AS TCU ON TCU.idCue = TPD.idCue
INNER JOIN mech.TBanco AS TBCO ON TBCO.codBan = TCU.codBan
INNER JOIN mech.TMoneda AS TMO ON TMO.codMon = TPD.codMon
INNER JOIN mech.TOrdenDesembolso AS TOD ON TOD.idOP = TPD.idOP
INNER JOIN mech.TIdentidad AS TID ON TID.codIde = TOD.codIde
INNER JOIN mech.TTipoPago AS TTP ON TTP.codTipP = TPD.codTipP

go

select * from TPagoDesembolso

select * from TBanco
select * from TCuentaBan
select codDetS,prioridad,descrip,cant,unidad,estSol,areaM,tipoM,nombres,obs1,nombres1,obs2,idSol,codEstS,codAreaM,codPers,codMat from VDetSol where idSol=@idS and (codAreaM=@codA or codAreaM>@nro)

select idOP,fecDes,serie,nro,simbolo,monto,montoDet,montoDif,nombre,estApro,nom,datoReq,ruc,razon,banco,nroCta,nroDet,hist,estDesem,codPersDes,estado,codMon,nomSol,codPersSol,codSerO from VOrdenDesemTesoreria where codSerO>@codSer1 or codSerO=@codSer2

select * from TSolicitud
update TSolicitud set estado=0 where idSol=10
select * from TDetalleSol


select codGuiaE,fecIni,nro,razon,ruc,partida,llegada,motivo,nroFact,nomPers,empTra,rucTra,marcaNro,nroConst,nroLic,nomTra,DNI,obs,hist,talon,nroGuia,codVeh,codT,codMotG,codPers,codUbiOri,codObraDes,codET,codSerS,codIde,codUbiDes from VGuiaRemProvEnt
			0		1	2	3	  4		5		6		7		8		9		10	11		12		  13	  14	  15   16  17	18	 19		20		21	  22	23		24		25			26		27		28		29		30	
																																												
	
select nroNota,tipo,fecha,material,cantEnt,preUniEnt,cantSal,preUniSal,saldo,unidad,nroGuia,nroDoc,veri,almObra,nomObraDes,obs,nomRecibe,provee,ruc,usuario,codMat,idMU,codUbi,codigo,codGuia,codDoc,codTrans,codPers,codSal,vanET,codUbiDes,ubicacion,nombre,codUsu from VKardex1 where codMat=@codMat and codUbi=@codUbi
		0		1	  2		3		  4			5		6		7		  8		9		10	   11	12		13		14		15		16		17	 18		19	  20	21	  22	23		24		25		26		27		28	   29		30		31		32		33	
	

select codGuiaE,fecIni,nro,razon,ruc,partida,llegada,motivo,nroFact,nomPers,empTra,rucTra,marcaNro,nroConst,nroLic,nomTra,DNI,obs,hist,talon,nroGuia,codVeh,codT,codMotG,codPers,codObraOri,codObraDes,codET,codSerS,codIde,codUbiOri,codUbiDes from VGuiaRemEmpEnt where codObraDes='00-06'
			0		1	2	3	  4		5		6		7		8		9		10	11		12		13			14	  15   16  17  18    19		20		21	  22	23		24		25			26		  27	28		29		30			31

select codDGE,codigo,cant,unidad,detalle,peso,entre,entregado,codGuiaE,codMat,recib,nomRec,obsR,recibido,codPers from VDetGuiaE where codGuiaE=@nro
		 0	1	  2		  3		4	   5	   6		7		 8		9	   10	  11	12		   13	14
select codDGE,codigo,cant,unidad,detalle,peso,recib,recibido,codGuiaE,codMat,nomRec,obsR from VDetGuiaE where codGuiaE=@nro



select * from TMatUbi
select * from TSaldo
select * from TUbicacion
select * from TEntradaSalida
select * from TTipoTransac

select * from TUbicacion
update TUbicacion set estado=1
select hist,codGuiaE from TGuiaRemEmp



















































