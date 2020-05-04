CREATE PROCEDURE dbo.st_ProcProcessoAdicionarAtualizar @ID_Processo       Integer = 0
                                                      ,@No_Comprovante    Varchar(14)
	                                                  ,@Data_Atualizacao  Datetime = NULL
	                                                  ,@Data_Finalizacao  Datetime = NULL
	                                                  ,@Status            Varchar(20)
	                                                  ,@ID_Comerciante    Integer
	                                                  ,@ID_Cliente        Integer
	                                                  ,@ID_Produto        Integer
	                                                  ,@ID_Entregador     Integer      = NULL
													  ,@Quantidade        Integer
											          ,@ID_Identificador  Integer      = 0  OUTPUT 
                                                      ,@Return_Code       Integer      = 0  OUTPUT
                                                      ,@ErrMsg            VarChar(255) = '' OUTPUT
AS								  
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;  
  BEGIN TRY
    
    DECLARE @NO_Tran Integer = @@TRANCOUNT;
    
	DECLARE @Quantidade_Atual Numeric(9,2);

    SELECT @Return_Code = 0
          ,@ErrMsg      = '';

    IF(@NO_Tran = 0) BEGIN TRAN;

	IF(@ID_Processo <> 0)
	BEGIN
		IF(NOT EXISTS(SELECT 1
		              FROM Processo WITH (NOLOCK)
					  WHERE ID_Processo = @ID_Processo))
		BEGIN
			SET @ErrMsg = 'Não existe processo para atualizar.'
			RAISERROR(@ErrMsg, 18, 1);
		END
		ELSE
		BEGIN
			UPDATE Processo SET Data_Atualizacao = GETDATE()
			                   ,Data_Finalizacao = @Data_Finalizacao
							   ,Status = @Status
			WHERE ID_Processo = @ID_Processo;

			SELECT @ID_Identificador = @ID_Processo;
		END
	END
    ELSE
	BEGIN
		IF(EXISTS(SELECT 1
		          FROM Processo
				  WHERE No_Comprovante = @No_Comprovante))
		BEGIN
			SET @ErrMsg = 'Já existe processo com o número de comprovante informado';
			RAISERROR(@ErrMsg, 18, 1);
		END
		
		INSERT INTO Processo(No_Comprovante
	                        ,Data_Inicio
	                        ,Data_Atualizacao
	                        ,Data_Finalizacao
	                        ,Status
	                        ,ID_Comerciante  
	                        ,ID_Cliente      
	                        ,ID_Produto      
	                        ,ID_Entregador)
		VALUES(@No_Comprovante
		      ,GETDATE()
			  ,@Data_Atualizacao
			  ,@Data_Finalizacao
			  ,@Status
			  ,@ID_Comerciante
			  ,@ID_Cliente
			  ,@ID_Produto
			  ,@ID_Entregador)
	   
	   SELECT @ID_Identificador = SCOPE_IDENTITY();

	   IF(@ID_Identificador > 0)
	   BEGIN
		SELECT @Quantidade_Atual = Quantidade
		FROM Produto WITH (NOLOCK)
		WHERE ID_Produto = @ID_Produto

		UPDATE Produto SET Quantidade = (@Quantidade_Atual - @Quantidade)
		WHERE ID_Produto = @ID_Produto

	   END

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
    