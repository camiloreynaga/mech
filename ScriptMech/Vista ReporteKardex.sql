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

select codmat,material,unidad,unidad,preBase,tipoM,stock,codUbi from VMaterialObra 

--vista para vkardex
select nroNota,tipo,fecha,material,cantEnt,preUniEnt,cantSal,preUniSal,saldo,unidad,nroGuia,nroDoc,veri,almObra,nomObraDes,obs,nomRecibe,provee,ruc,usuario,codMat,idMU,codUbi,codigo,codGuia,codDoc,codTrans,codPers,codSal,vanET,codUbiDes,ubicacion,nombre,codUsu from VKardex1 where codMat=@codMat and codUbi=@codUbi

