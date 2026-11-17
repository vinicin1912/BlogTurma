using BlogTurma.data;
using BlogTurma.models;
using MySqlConnector;

namespace BlogTurma.services;

public class UsuarioService
{
    private readonly Conexao _conexao;
    private readonly SenhaService _senhaService;

    public UsuarioService(Conexao conexao)
    {
        _conexao = conexao;
        _senhaService = new SenhaService();
    }

    public void Cadastrar(string nome, string email, string senha)
    {
        nome = nome?.Trim() ?? string.Empty;
        email = email?.Trim().ToLower() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
        {
            throw new ArgumentException("Nome, e-mail e senha são obrigatórios.");
        }

        using MySqlConnection conexao = _conexao.CriarConexao();
        conexao.Open();

        // Verificar e-mail duplicado
        const string sqlVerifica = "SELECT COUNT(*) FROM usuarios WHERE Email = @email;";
        using (var cmdVerifica = new MySqlCommand(sqlVerifica, conexao))
        {
            cmdVerifica.Parameters.AddWithValue("@email", email);
            long qtd = Convert.ToInt64(cmdVerifica.ExecuteScalar());
            if (qtd > 0)
            {
                throw new InvalidOperationException("Este e-mail já está cadastrado.");
            }
        }

        // Gerar Hash da Senha e Inserir
        string senhaHash = _senhaService.GerarHash(senha);

        const string sqlInsert = @"
            INSERT INTO usuarios (Nome, Email, SenhaHash)
            VALUES (@nome, @email, @senhaHash);";

        using (var cmdInsert = new MySqlCommand(sqlInsert, conexao))
        {
            cmdInsert.Parameters.AddWithValue("@nome", nome);
            cmdInsert.Parameters.AddWithValue("@email", email);
            cmdInsert.Parameters.AddWithValue("@senhaHash", senhaHash);
            cmdInsert.ExecuteNonQuery();
        }
    }

    public Usuario? Autenticar(string email, string senha)
    {
        email = email?.Trim().ToLower() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
        {
            return null;
        }

        using MySqlConnection conexao = _conexao.CriarConexao();
        conexao.Open();

        const string sql = "SELECT Id, Nome, Email, SenhaHash FROM usuarios WHERE Email = @email;";
        using var comando = new MySqlCommand(sql, conexao);
        comando.Parameters.AddWithValue("@email", email);

        using MySqlDataReader leitor = comando.ExecuteReader();

        if (leitor.Read())
        {
            string hashBanco = leitor.GetString("SenhaHash");

            if (_senhaService.Verificar(senha, hashBanco))
            {
                return new Usuario
                {
                    Id = leitor.GetInt32("Id"),
                    Nome = leitor.GetString("Nome"),
                    Email = leitor.GetString("Email"),
                    SenhaHash = hashBanco
                };
            }
        }

        return null;
    }
}