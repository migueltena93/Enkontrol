USE [Broxel]
GO

IF OBJECT_ID('SPInsProspecto') IS NOT NULL
	DROP PROCEDURE [dbo].[SPInsProspecto]
GO

CREATE PROCEDURE [dbo].[SPInsProspecto] 
(	
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

		SET @vcMessage = 'ERROR- NO SE PUDO INSERTAR EL NUEVO PROSPECTO EN LA BD.'

		INSERT INTO opeProspectos
			VALUES(	@pcNombre,
					@pcApellidoPaterno,
					@pcApellidoMaterno,
					@pdFechaNacimiento,
					@pcTelefono,
					@pcEmail,
					@piIdGenero,
					@piEstadoCivil)
			
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