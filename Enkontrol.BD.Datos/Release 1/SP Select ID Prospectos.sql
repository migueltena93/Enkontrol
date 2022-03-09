USE [Broxel]
GO

IF OBJECT_ID('SPSelIDProspectos') IS NOT NULL
	DROP PROCEDURE [dbo].[SPSelIDProspectos]
GO

CREATE PROCEDURE [dbo].[SPSelIDProspectos] 
(	
	@piID				INT
)
AS
DECLARE	@vcMessage	NVARCHAR(255)	=	''

SET NOCOUNT ON

		SELECT 	op.Id,
				op.cNombre,
				op.cApellidoPaterno,
				op.cApellidoMaterno,
				op.dtFechaNacimiento,
				op.cTelefonoMovil,
				op.cEmail,
				op.fkIdGenero AS Genero,
				op.fkIdEstadoCivil AS EstadoCivil
			FROM opeProspectos op
			WHERE op.Id = @piID
			
		
SET NOCOUNT OFF
	RETURN 0

ERRORES:
	SET NOCOUNT OFF
	SET @vcMessage = @vcMessage + ISNULL(ERROR_MESSAGE(),'')
	RAISERROR(@vcMessage,10,1)
	RETURN -1
GO