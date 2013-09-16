create view vStockAlmacen 
as
select TMU.idMU,tmu.codUbi,TMU.stock,TM.codMat,TM.material,TUN.unidad,TTM.tipoM       
from TMatUbi TMU 
inner join TMaterial TM on TM.codMat = TMU.codMat  
inner join TUnidad TUN on TUN.codUni=TM.codUni 
inner join TTipoMat TTM on TM.codTipM = TTM.codTipM   

where codUbi =1

select idMU,codUbi,material,unidad,tipoM,stock from vStockAlmacen where codUbi = 1

select * from TPersonal 