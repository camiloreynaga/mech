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
