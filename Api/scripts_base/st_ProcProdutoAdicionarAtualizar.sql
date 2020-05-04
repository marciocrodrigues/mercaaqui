CREATE PROCEDURE dbo.st_ProcProdutoAdicionarAtualizar @ID_Produto       Integer = 0
                                                     ,@ID_Comerciante   Integer
                                                     ,@Descricao        Varchar(255)
										             ,@Quantidade       Integer
										             ,@Preco            Numeric(18, 2)
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
  
    SELECT @Return_Code = 0
          ,@ErrMsg      = '';

	IF(@ID_Produto <> 0)
	BEGIN
		IF(NOT EXISTS(SELECT 1
		              FROM Produto WITH (NOLOCK)
					  WHERE ID_Produto     = @ID_Produto
					    AND ID_Comerciante = @ID_Comerciante))
		BEGIN
			SET @ErrMsg = 'Produto não cadastrado para alteração';
			RAISERROR(@ErrMsg, 18, 1);
		END

		UPDATE Produto SET Descricao  = @Descricao
		                  ,Quantidade = @Quantidade
						  ,Preco      = @Preco
						  ,Imagem     = @Imagem
		WHERE ID_Produto = @ID_Produto;

		SELECT @ID_Identificador = @ID_Produto;
	END
	ELSE
	BEGIN
		INSERT INTO Produto(Descricao
		                   ,Quantidade
						   ,Preco
						   ,ID_Comerciante
						   ,Imagem)
		VALUES (@Descricao
		       ,@Quantidade
			   ,@Preco
			   ,@ID_Comerciante
			   ,@Imagem)

		SELECT @ID_Identificador = SCOPE_IDENTITY();
	END
	 
    IF (@ErrMsg <> '')
    BEGIN
      SET @ErrMsg = 'st_ProcProdutoAddUpd. Parametros: ' + CHAR(13) + @ErrMsg;
      RAISERROR (@ErrMsg, 18, 1);
    END

    IF(@NO_Tran = 0) BEGIN TRAN;

    
    
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
    