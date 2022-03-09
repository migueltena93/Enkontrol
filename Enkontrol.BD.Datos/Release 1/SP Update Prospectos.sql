USE [Broxel]
GO

IF OBJECT_ID('SPUpdProspectos') IS NOT NULL
	DROP PROCEDURE [dbo].[SPUpdProspectos]
GO

CREATE PROCEDURE [dbo].[SPUpdProspectos] 
(	
	@piIdProspecto			INT,
	@pcNombre				VARCHAR(20),
	@pcApellidoPaterno		VARCHAR(50),
	@pcApellidoMaterno		VARCHAR(50),
	@pdFechaNacimiento		DATETIME,
	@pcTelefono				VARCHAR(50),
	@pcEmail				VARCHAR(50),
	@piIdGenero				INT,
	@piEstadoCivil			INT
)
AS
DECLARE	@vcMessage	NVARCHAR(255)	=	''

SET NOCOUNT ON

	BEGIN TRAN T1

		SET @vcMessage = 'ERROR- NO SE PUDO ATUALIZAR LA INFORMACIÓN DEL PROSPECTO.'

		UPDATE 	opeProspectos
			SET cNombre=@pcNombre,
				cApellidoPaterno=@pcApellidoPaterno,
				cApellidoMaterno=@pcApellidoMaterno,
				dtFechaNacimiento=@pdFechaNacimiento,
				cTelefonoMovil=@pcTelefono,
				cEmail=@pcEmail,
				fkIdGenero=@piIdGenero,
				fkIdEstadoCivil=@piEstadoCivil
			WHERE Id = @piIdProspecto
			
		IF @@ERROR <> 0  GOTO ERRORES
		
	COMMIT TRAN T1
	
	SELECT 1;
		
SET NOCOUNT OFF
	RETURN 0

ERRORES:
	IF(@@TRANCOUNT>0)
		ROLLBACK TRAN T1
	SET NOCOUNT OFF
	SET @vcMessage = @vcMessage + ISNULL(ERROR_MESSAGE(),'')
	RAISERROR(@vcMessage,10,1)
	RETURN -1
GO