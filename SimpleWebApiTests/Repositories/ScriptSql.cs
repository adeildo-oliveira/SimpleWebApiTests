namespace SimpleWebApiTests.Repositories
{
    internal struct ScriptSql
    {
		internal const string INSERIR_CLIENTE = @"INSERT INTO CLIENTE (CpfCnpj, Nome, Mae, Pai)
													VALUES (@CpfCnpj, @Nome, @Mae, @Pai)";

		internal const string ALTERAR_CLIENTE = @"UPDATE CLIENTE 
												  SET Nome = @Nome, Mae = @Mae, Pai = @Pai
												  WHERE CpfCnpj = @CpfCnpj";

		internal const string CONSULTAR_CLIENTE = @"SELECT CpfCnpj, Nome, Mae, Pai FROM CLIENTE (NOLOCK) WHERE CpfCnpj = @CpfCnpj";
	}
}