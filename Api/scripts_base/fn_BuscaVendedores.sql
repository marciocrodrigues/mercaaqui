CREATE FUNCTION dbo.fn_BuscaVendedores(@Cidade Varchar(50)
                                      ,@Estado Char(2))
RETURNS TABLE
AS
RETURN (
  SELECT ID_Comerciante
        ,Nome
		,Nome_Fantasia
		,Logradouro
		,Numero
		,Cep
		,Cidade
		,Estado
		,DDD
		,Telefone
		,Imagem
  FROM Comerciante
       INNER JOIN Pessoa ON Pessoa.ID_Pessoa = Comerciante.ID_Pessoa
	   INNER JOIN Endereco ON Endereco.ID_Endereco = Comerciante.ID_Endereco
	   INNER JOIN Contato ON Contato.ID_Contato = Comerciante.ID_Contato
  WHERE UPPER(Cidade) = UPPER(@Cidade)
    AND UPPER(Estado) = UPPER(@Estado)
)
    