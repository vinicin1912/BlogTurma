using MySqlConnector;

namespace BlogTurma.data;

public class Conexao
{
    private string LerVariavel(string chave)
    {
        string? valor = Environment.GetEnvironmentVariable(chave);

        // Se for DB_PASSWORD e estiver vazio/nulo, assume string vazia (padrão XAMPP)
        if (chave == "DB_PASSWORD")
        {
            return valor ?? string.Empty;
        }

        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new InvalidOperationException($"A variável de ambiente '{chave}' não foi configurada.");
        }

        return valor;
    }

    public MySqlConnection CriarConexao()
    {
        string host = LerVariavel("DB_HOST");
        string porta = LerVariavel("DB_PORT");
        string banco = LerVariavel("DB_NAME");
        string usuario = LerVariavel("DB_USER");
        string senha = LerVariavel("DB_PASSWORD");

        var configuracao = new MySqlConnectionStringBuilder
        {
            Server = host,
            Port = uint.Parse(porta),
            Database = banco,
            UserID = usuario,
            Password = senha
        };

        return new MySqlConnection(configuracao.ConnectionString);
    }
}