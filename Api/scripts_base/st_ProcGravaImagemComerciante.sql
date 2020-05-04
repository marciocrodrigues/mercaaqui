CREATE PROCEDURE dbo.st_ProcGravaImagemComerciante @ID_Comerciante Integer
                                                  ,@Img            Varbinary(MAX)
                                                  ,@Return_Code    Integer      = 0 
                                                  ,@ErrMsg         VarChar(255) = ''
AS
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;  
  BEGIN TRY
    
    DECLARE @NO_Tran Integer = @@TRANCOUNT;
  
    SELECT @Return_Code = 0
          ,@ErrMsg      = '';
    
	IF(NOT EXISTS(SELECT 1
	              FROM Comerciante WITH (NOLOCK)
				  WHERE ID_Comerciante = @ID_Comerciante))
	BEGIN
		SET @ErrMsg = 'Não existe comerciante cadastrado para gravar a imagem.';
		RAISERROR(@ErrMsg, 18, 1);
	END

	UPDATE Comerciante SET Img = @Img
	WHERE ID_Comerciante = @ID_Comerciante;

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
    