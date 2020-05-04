CREATE FUNCTION dbo.fn_BuscaProdutosComerciantes(@ID_Comerciante Integer)
RETURNS TABLE
AS
RETURN (
  SELECT ID_Produto
        ,Descricao
		,Quantidade
		,Preco
		,Imagem
  FROM Produto WITH (NOLOCK)
  WHERE ID_Comerciante = @ID_Comerciante
)
    