--ver megas de base de datos
sp_helpdb BD_ConstrucMech

CHECKPOINT
BACKUP LOG BD_ConstrucMech TO DISK = 'C:\Prueba_Log.trn'
DBCC SHRINKFILE (N'BD_ConstrucMech_log' , 0, TRUNCATEONLY)
GO










