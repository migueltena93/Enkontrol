USE [Broxel]
GO

IF OBJECT_ID('SPSelProspectos') IS NOT NULL
	DROP PROCEDURE [dbo].[SPSelProspectos]
GO

CREATE PROCEDURE [dbo].[SPSelProspectos] 
(	@pcFiltro			VARCHAR(20),
	@piGenero			VARCHAR(2),
	@piEstadoCivil		VARCHAR(2)
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
				g.cDescripcion AS Genero,
				ec.cDescripcion AS EstadoCivil
			FROM opeProspectos op
			JOIN catGeneros g ON g.Id = op.fkIdGenero
			JOIN catEstadoCivil ec ON ec.Id = op.fkIdEstadoCivil
			WHERE (@pcFiltro IS NULL OR op.cNombre LIKE @pcFiltro + '%')
			AND (@piGenero IS NULL OR op.fkIdGenero = CAST(@piGenero AS INT))
			AND (@piEstadoCivil IS NULL OR op.fkIdEstadoCivil = CAST(@piEstadoCivil AS INT))
			
		
SET NOCOUNT OFF
	RETURN 0

ERRORES:
	SET NOCOUNT OFF
	SET @vcMessage = @vcMessage + ISNULL(ERROR_MESSAGE(),'')
	RAISERROR(@vcMessage,10,1)
	RETURN -1
GO