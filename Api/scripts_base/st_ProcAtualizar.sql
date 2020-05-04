CREATE PROCEDURE dbo.st_ProcAtualizar @ID_Pessoa        Integer
                                     ,@Nome             Varchar(80)
                                     ,@Nome_Fantasia    Varchar(100)
                                     ,@Documento        Varchar(14)
									 ,@Email            Varchar(30)
									 ,@Senha            Varchar(10)
									 ,@Logradouro       Varchar(100)
									 ,@Numero           Varchar(10)
									 ,@Bairro           Varchar(30)
									 ,@Cep              Varchar(10)
									 ,@Cidade           Varchar(50)
									 ,@Estado           Char(2)
									 ,@DDD              Char(2)
									 ,@Telefone         Varchar(10)
									 ,@Tipo             Char(2)
									 ,@ID_Identificador Integer      = 0  OUTPUT
                                     ,@Return_Code      Integer      = 0  OUTPUT
                                     ,@ErrMsg           VarChar(255) = '' OUTPUT
AS
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;  
  BEGIN TRY
    
    DECLARE @NO_Tran Integer = @@TRANCOUNT;
    
	DECLARE @ID_Comerciante Integer = 0
	       ,@ID_Cliente     Integer = 0
		   ,@ID_Entregador  Integer = 0
	       ,@ID_Endereco    Integer = 0
		   ,@ID_Contato     Integer = 0

    SELECT @Return_Code = 0
          ,@ErrMsg      = '';

    IF(@NO_Tran = 0) BEGIN TRAN;


	IF(NOT EXISTS (SELECT 1
	               FROM Pessoa WITH (NOLOCK)
			       WHERE ID_Pessoa = @ID_Pessoa))
	BEGIN
		SET @ErrMsg = 'Não existe cadastro para alterar.';
		RAISERROR(@ErrMsg, 18, 1);
	END
    
	IF(@Tipo = 'CL')
	BEGIN
		SELECT @ID_Cliente  = ID_Cliente
		      ,@ID_Endereco = ID_Endereco
			  ,@ID_Contato  = ID_Contato
		FROM Cliente WITH (NOLOCK)
	    WHERE ID_Pessoa = @ID_Pessoa

		SELECT @ID_Identificador = @ID_Cliente;
	END

	IF(@Tipo = 'CO')
	BEGIN
		SELECT @ID_Comerciante = ID_Comerciante
		      ,@ID_Endereco    = ID_Endereco
			  ,@ID_Contato     = ID_Contato
		FROM Comerciante WITH (NOLOCK)
		WHERE ID_Pessoa = @ID_Pessoa

		SELECT @ID_Identificador = @ID_Comerciante;

		UPDATE Comerciante SET Nome_Fantasia = @Nome_Fantasia WHERE ID_Comerciante = @ID_Comerciante;

	END

	IF(@Tipo = 'EN')
	BEGIN
		SELECT @ID_Entregador = ID_Entregador
		      ,@ID_Entregador = ID_Entregador
			  ,@ID_Contato    = ID_Contato
		FROM Entregador WITH (NOLOCK)
		WHERE ID_Pessoa = @ID_Pessoa

		SELECT @ID_Identificador = @ID_Entregador;
	END
	
	 UPDATE Pessoa SET Nome      = @Nome
	                  ,Documento = @Documento
					  ,Email     = @Email
					  ,Senha     = @Senha
	 WHERE ID_Pessoa = @ID_Pessoa

	 UPDATE Endereco SET Logradouro = @Logradouro
	                    ,Numero     = @Numero
						,Bairro     = @Bairro
						,Cidade     = @Cidade
						,Estado     = @Estado
						,Cep        = @Cep
	 WHERE ID_Endereco = @ID_Endereco;

	 UPDATE Contato SET DDD      = @DDD
	                   ,Telefone = @Telefone
	 WHERE ID_Contato = @ID_Contato;

    IF(@NO_Tran = 0) AND (@@TRANCOUNT > 0) COMMIT TRAN;
    
    SELECT @Return_Code = 0,
           @ErrMsg      = '';
  END TRY

  BEGIN CATCH
    IF(@NO_TRAN = 0) AND (@@TRANCOUNT > 0) ROLLBACK TRAN;

    SELECT @ErrMsg      = @ErrMsg,
           @Return_Code = CASE
                            WHEN @Return_Code = 0 THEN 1 ELSE @Return_Code
                          END
  END CATCH

  RETURN;
END