USE Broxel 
GO

DECLARE @vcMessage			VARCHAR(255) = ''

IF object_id('dbo.catGeneros') IS NOT NULL
BEGIN
	DROP TABLE dbo.catGeneros
	IF @@ERROR <> 0  GOTO ERRORES
END

IF object_id('dbo.catEstadoCivil') IS NOT NULL
BEGIN
	DROP TABLE dbo.catEstadoCivil
	IF @@ERROR <> 0  GOTO ERRORES
END

IF object_id('dbo.opeProspectos') IS NOT NULL
BEGIN
	DROP TABLE dbo.opeProspectos
	IF @@ERROR <> 0  GOTO ERRORES
END

BEGIN TRAN T1

	/*
			DESCRIPTION:	TABLA CATALOGO PARA AGREGAR TIPOS DE GENERO
	*/

	CREATE TABLE dbo.catGeneros
		(
			Id					INT				NOT NULL		IDENTITY(1,1),
			cDescripcion		VARCHAR(50)		NOT	NULL
		)

		-- PK CONSTRAINTS
		ALTER TABLE	dbo.catGeneros
		ADD CONSTRAINT			PK_IdcatGeneros				PRIMARY KEY	CLUSTERED			( Id ) 
		IF @@ERROR <> 0  GOTO ERRORES

		INSERT INTO 			dbo.catGeneros 				VALUES('Masculino'),('Femenino')

	/*
			DESCRIPTION:	TABLA CATALOGO PARA AGREGAR TIPOS DE ESTADOS CIVILES
	*/

	CREATE TABLE dbo.catEstadoCivil
		(
			Id					INT				NOT NULL		IDENTITY(1,1),
			cDescripcion		VARCHAR(50)		NOT	NULL
		)

		-- PK CONSTRAINTS
		ALTER TABLE	dbo.catEstadoCivil
		ADD CONSTRAINT			PK_IdCatEstadoCivil				PRIMARY KEY	CLUSTERED			( Id ) 
		IF @@ERROR <> 0  GOTO ERRORES

		INSERT INTO 			dbo.catEstadoCivil 				VALUES('Soltero(a)'),('Casado(a)'),('Divorciado(a)'),('Viudo(a)'),('Unión Libre')

	/*
			DESCRIPTION:	TABLA PARA REGISTRAR PROSPECTOS
	*/

	CREATE TABLE dbo.opeProspectos
		(
			Id					INT				NOT NULL		IDENTITY(1,1),
			cNombre				VARCHAR(50)		NOT	NULL,
			cApellidoPaterno	VARCHAR(50)		NOT NULL,
			cApellidoMaterno	VARCHAR(50)		NOT NULL,
			dtFechaNacimiento	DATETIME		NOT NULL,
			cTelefonoMovil		VARCHAR(50)		NOT	NULL,
			cEmail				VARCHAR(50)		NOT NULL,
			fkIdGenero			INT				NOT NULL,
			fkIdEstadoCivil		INT				NOT NULL
		)

		-- PK CONSTRAINTS
		ALTER TABLE	dbo.opeProspectos
		ADD CONSTRAINT			PK_IdopeProspectos				PRIMARY KEY	CLUSTERED			( Id ) 
		IF @@ERROR <> 0  GOTO ERRORES

		ALTER TABLE	dbo.opeProspectos
		ADD CONSTRAINT				FK_opeProspectos_01	FOREIGN KEY		(fkIdGenero)			REFERENCES catGeneros		(Id),
			CONSTRAINT				FK_opeProspectos_02	FOREIGN KEY		(fkIdEstadoCivil)		REFERENCES catEstadoCivil	(Id)
		IF @@ERROR <> 0  GOTO ERRORES

COMMIT TRAN T1 
RETURN

ERRORES:
	ROLLBACK TRAN T1
	SELECT @vcMessage =ERROR_MESSAGE()
	RAISERROR(@vcMessage,10,1)
	RETURN