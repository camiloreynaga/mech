create view VSolicitudCaja
as
SELECT  TSC.codSC,TSC.nroSol,tsc.fechaSol,'estado' =case when TSC.estSol=0 then 'ABIERTO' when TSC.estSol=1 then 'CERRADO' else 'RECHAZADO' end,
'nro' = case when TSC.nroSol <100 then '000'+ LTRIM(STR(TSC.nroSol)) when TSC.nroSol >= 100 and TSC.nroSol <1000 then '00'+LTRIM(STR(TSC.nroSol))
when TSC.nroSol >=1000 and TSC.nroSol <1000 then '0'+LTRIM(STR(TSC.nroSol)) else LTRIM (str(TSC.nroSol)) end, 
TSC.estSol,tsc.montoSol,tsc.montoRen,tsc.codCC,tsc.codPers,tpe.nombre +' '+TPE.apellido as nombres 
FROM TSolicitudCaja TSC
join TPersonal TPE on TPE.codPers = TSC.codPers 

create view VDetaleSolCaja  
as
	select TDC.codDetSol,TDC.cant1,TDC.cant2,TDC.insumo,TDC.prec1,TDC.prec2,TDC.uniMed,TDC.obsSol,TDC.codSC,TDC.codApro,
	isnull(TP.nombre+' '+TP.apellido,'') aprobador,TDC.obsApro,TM.codMat ,TM.material ,TAM.codAreaM,TAM.areaM,TTM.codTipM,
	TTM.tipoM,TDC.estDet codEstado,
	'estado'= case when TDC.estDet=0 then 'PENDIENTE' when TDC.estDet=1 then 'APROBADO' when TDC.estDet =2 then 'OBSERVADO' else 'RECHAZADO' end,
	TDC.estRen , 'rendicion'=case when TDC.estRen =0 then 'PENDIENTE' else 'RENDIDO' end,isnull(TP2.nombre+' '+TP2.apellido,'') rendidor ,tdc.nroDocRen        
	from detSolCaja TDC
	left join TPersonal TP on TP.codPers = TDC.codApro 
	left join TPersonal TP2 on TP2.codPers = TDC.codRen  
	inner join TMaterial TM on TM.codMat=TDC.codMat 
	Inner join TAreaMat TAM on TAM.codAreaM = TDC.codAreaM 
	inner join TTipoMat TTM on TTM.codTipM =TM.codTipM
GO



create procedure PA_InsertDetalleSolCaja
	@pri varchar(20),@can decimal(8,2),@des varchar(100),@uni varchar(20),@codP int,@obs1 varchar(100),@codP1 int,
	@codE int,@obs2 varchar(200),@codM int,@codA int,@idS int,
	@Identity int output --parametro de salida
as
	insert into TDetalleSol(prioridad,cant,descrip,unidad,codPers,obs1,codPersA,codEstS,obs2,codMat,codAreaM,idSol) 
		values(@pri,@can,@des,@uni,@codP,@obs1,@codP1,@codE,@obs2,@codM,@codA,@idS)	
	SET @Identity = @@Identity
	
	RETURN  @Identity
GO

create procedure PA_InsertDetalleSolCaja

@cant1 Decimal (8,1),
@cant2 decimal(8,2),
@insumo  varchar(100),
@prec1 decimal (8,2),
@prec2 decimal(8,2),
@uniMed varchar(20),
@obsSol varchar(200),
@codApro int,
@estDet int,
@obsApro varchar(200),
@codMat int,
@codAreaM int,
@codSC int ,
@estRen int ,
@codRen int ,
@nroDocRen varchar(30),
--Parametro de Salida
@Identity int output

as
INSERT Into DetSolCaja values (@cant1,@cant2,@insumo,@prec1,@prec2,@uniMed,@obsSol
,@codApro,@estDet,@obsApro,@codMat,@codAreaM,@codSC,@estRen,@codRen,@nroDocRen)

SET @Identity=@@IDENTITY 

return @Identity






select codSC,nro from VsolicitudCaja

select * from VDetSol 
select  * from detSolCaja