create view VSolicitudCaja
as
SELECT  TSC.codSC,TSC.nroSol,tsc.fechaSol,'estado' =case when TSC.estSol=0 then 'ABIERTO' when TSC.estSol=1 then 'CERRADO' else 'RECHAZADO' end,
'nro' = case when TSC.nroSol <100 then '000'+ LTRIM(STR(TSC.nroSol)) when TSC.nroSol >= 100 and TSC.nroSol <1000 then '00'+LTRIM(STR(TSC.nroSol))
when TSC.nroSol >=1000 and TSC.nroSol <1000 then '0'+LTRIM(STR(TSC.nroSol)) else LTRIM (str(TSC.nroSol)) end, 
TSC.estSol,tsc.montoSol,tsc.montoRen,tsc.codCC,tsc.codPers,tpe.nombre +' '+TPE.apellido as nombres 
FROM TSolicitudCaja TSC
join TPersonal TPE on TPE.codPers = TSC.codPers 


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



select * from DetSolCaja 


select codSC,nro from VsolicitudCaja

select  * from detSolCaja