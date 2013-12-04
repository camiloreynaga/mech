select * from tlugartrabajo

select * from TPersObra 


-- consulta para combo obras
select codigo,nombre  from TLugarTrabajo where estado =1

-- consulta para grilla personal

select TP.codPer,tp.dni,(TP.nombre +' '+ TP.apePat +' '+TP.apeMat) nombre,tp.sexo  from tpersona TP



--consulta para grilla personal por Obra

select codPO,codPer,dni,nombre,sexo,codigo  from vPersonalObra 

--Vista para personal por Obra

CREATE VIEW vPersonalObra
as
select TP.codPer,tp.dni,(TP.nombre +' '+ TP.apePat +' '+TP.apeMat) nombre,tp.sexo, 
	TPO.codigo,TPO.codPO 
from tpersona TP	
inner join TPersObra TPO on TP.codPer = TPO.codPer  