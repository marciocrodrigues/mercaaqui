CREATE PROCEDURE dbo.st_Login @Email            Varchar(50)
                             ,@Senha            Varchar(10)
							 ,@ID_Identificador	Integer      = 0  OUTPUT
							 ,@Tipo        		Char(2)      = '' OUTPUT
                             ,@Return_Code      Integer      = 0  OUTPUT
                             ,@ErrMsg           VarChar(255) = '' OUTPUT
AS
BEGIN
  SET NOCOUNT ON;
  BEGIN TRY
    
    DECLARE @ID_Pessoa        Integer = 0,
	        @ID_Comerciante   Integer = 0,
			@ID_Entregador    Integer = 0,
			@ID_Cliente       Integer = 0;

    SELECT @Return_Code = 0
          ,@ErrMsg      = '';

    IF(NOT EXISTS(SELECT 1
	              FROM Pessoa WITH (NOLOCK)
				  WHERE UPPER(Email) LIKE UPPER(@Email)
				    AND UPPER(Senha) LIKE UPPER(@Senha)))
	BEGIN
		SET @ErrMsg = 'Email ou Senha invalidos';
		RAISERROR(@ErrMsg, 18, 1);
	END

	SELECT @ID_Pessoa = ID_Pessoa
	FROM Pessoa WITH (NOLOCK)
	WHERE UPPER(Email) LIKE UPPER(@Email)
	  AND UPPER(Senha) LIKE UPPER(@Senha)

	SELECT @ID_Cliente = ID_Cliente
	FROM Cliente WITH (NOLOCK)
	WHERE ID_Pessoa = @ID_Pessoa

	SELECT @ID_Entregador = ID_Entregador
	FROM Entregador WITH (NOLOCK)
	WHERE ID_Pessoa = @ID_Pessoa

	SELECT @ID_Comerciante = ID_Comerciante
	FROM Comerciante WITH (NOLOCK)
	WHERE ID_Pessoa = @ID_Pessoa

	IF(@ID_Cliente <> 0)
	BEGIN
		SET @ID_Identificador = @ID_Cliente;
		SET @Tipo = 'CL';
	END

	IF(@ID_Entregador <> 0)
	BEGIN
		SET @ID_Identificador = @ID_Entregador;
		SET @Tipo = 'EN';
	END

	IF(@ID_Comerciante <> 0)
	BEGIN
		SET @ID_Identificador = @ID_Comerciante;
		SET @Tipo = 'CO';
	END

    SELECT @Return_Code = 0,
           @ErrMsg      = '';
  END TRY

  BEGIN CATCH

    SELECT @ErrMsg      = @ErrMsg,
           @Return_Code = CASE
                            WHEN @Return_Code = 0 THEN 1 ELSE @Return_Code
                          END
  END CATCH

  RETURN;
END
    