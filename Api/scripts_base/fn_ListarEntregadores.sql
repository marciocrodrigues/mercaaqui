CREATE FUNCTION dbo.fn_ListarEntregadores()
RETURNS TABLE
AS
RETURN (
  SELECT ID_Entregador
        ,Nome
		,Logradouro
		,Numero
		,Cep
		,Cidade
		,Estado
		,DDD
		,Telefone
		,Imagem
  FROM Entregador WITH (NOLOCK)
       INNER JOIN Pessoa ON Pessoa.ID_Pessoa = Entregador.ID_Pessoa
	   INNER JOIN Endereco ON Endereco.ID_Endereco = Entregador.ID_Endereco
	   INNER JOIN Contato ON Contato.ID_Contato = Entregador.ID_Contato
)
    