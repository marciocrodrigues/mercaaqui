CREATE FUNCTION dbo.fn_ListarClientes()
RETURNS TABLE
AS
RETURN (
  SELECT ID_Cliente
        ,Nome
		,Logradouro
		,Numero
		,Cep
		,Cidade
		,Estado
		,DDD
		,Telefone
		,Imagem
  FROM Cliente WITH (NOLOCK)
       INNER JOIN Pessoa ON Pessoa.ID_Pessoa = Cliente.ID_Pessoa
	   INNER JOIN Endereco ON Endereco.ID_Endereco = Cliente.ID_Endereco
	   INNER JOIN Contato ON Contato.ID_Contato = Cliente.ID_Contato
)