CREATE PROCEDURE dbo.st_ProcCadastrar @Nome             Varchar(80)
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
									 ,@Imagem           Varchar(MAX)
									 ,@ID_Identificador Integer      = 0  OUTPUT
                                     ,@Return_Code      Integer      = 0  OUTPUT
                                     ,@ErrMsg           VarChar(255) = '' OUTPUT
AS
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;  
  BEGIN TRY
    
    DECLARE @NO_Tran Integer = @@TRANCOUNT;
    
	DECLARE @ID_Pessoa Integer   = 0
	       ,@ID_Endereco Integer = 0
		   ,@ID_Contato  Integer = 0

    SELECT @Return_Code = 0
          ,@ErrMsg      = '';

    IF(@NO_Tran = 0) BEGIN TRAN;


	IF(EXISTS (SELECT 1
	           FROM Pessoa WITH (NOLOCK)
			   WHERE Documento = @Documento))
	BEGIN
		SET @ErrMsg = 'Já existe cadastro com esse documento.';
		RAISERROR(@ErrMsg, 18, 1);
	END
    
	 INSERT INTO Pessoa(Nome
	                   ,Documento
					   ,Email
					   ,Senha)
	  VALUES(@Nome
	        ,@Documento
			,@Email
			,@Senha)
   
    SELECT @ID_Pessoa = SCOPE_IDENTITY();

	IF(@ID_Pessoa = 0)
	BEGIN
		SET @ErrMsg = 'Erro ao inserir dados Pessoa';
		RAISERROR(@ErrMsg, 18, 1);
	END
    
	INSERT INTO Endereco(Logradouro
	                    ,Numero
						,Bairro
						,Cep
						,Cidade
						,Estado)
	VALUES(@Logradouro
	      ,@Numero
		  ,@Bairro
		  ,@Cep
		  ,@Cidade
		  ,@Estado)

    SELECT @ID_Endereco = SCOPE_IDENTITY();

	IF(@ID_Endereco = 0)
	BEGIN
		SET @ErrMsg = 'Erro ao inserir dados Endereco';
		RAISERROR(@ErrMsg, 18, 1);
	END

	INSERT INTO Contato(DDD
	                   ,Telefone)
    VALUES(@DDD
	      ,@Telefone)

	SELECT @ID_Contato = SCOPE_IDENTITY();

	IF(@ID_Contato = 0)
	BEGIN
		SET @ErrMsg = 'Erro ao inserir dados Contato';
		RAISERROR(@ErrMsg, 18, 1);
	END

	IF(@Tipo = 'CL')
	BEGIN
		INSERT INTO Cliente(ID_Pessoa
		                   ,ID_Endereco
						   ,ID_Contato
						   ,Imagem)
		VALUES(@ID_Pessoa
		      ,@ID_Endereco
			  ,@ID_Contato
			  ,@Imagem);

	    SELECT @ID_Identificador = SCOPE_IDENTITY();
	END

	IF(@Tipo = 'CO')
	BEGIN
		INSERT INTO Comerciante(ID_Pessoa
		                       ,ID_Endereco
							   ,ID_Contato
							   ,Nome_Fantasia
							   ,Imagem)
		VALUES(@ID_Pessoa
		      ,@ID_Endereco
			  ,@ID_Contato
			  ,@Nome_Fantasia
			  ,@Imagem)

		SELECT @ID_Identificador = SCOPE_IDENTITY();
	END

	IF(@Tipo = 'EN')
	BEGIN
		INSERT INTO Entregador(ID_Pessoa
		                      ,ID_Endereco
							  ,ID_Contato
							  ,Imagem)
		VALUES(@ID_Pessoa
		      ,@ID_Endereco
			  ,@ID_Contato
			  ,@Imagem)
	   
	    SELECT @ID_Identificador = SCOPE_IDENTITY();
	END

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
    