using BlogTurma.data;
using BlogTurma.models;
using MySqlConnector;

namespace BlogTurma.services;

public class PublicacaoService
{
    private readonly Conexao _conexao;
    private readonly SessaoUsuario _sessao;

    public PublicacaoService(Conexao conexao, SessaoUsuario sessao)
    {
        _conexao = conexao;
        _sessao = sessao;
    }

    private void ValidarDados(string titulo, string texto)
    {
        if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(texto))
        {
            throw new ArgumentException("Título e texto não podem estar vazios.");
        }
    }

    private int ObterUsuarioAutenticado()
    {
        if (!_sessao.EstaAutenticado || !_sessao.UsuarioId.HasValue)
        {
            throw new InvalidOperationException("É necessário estar autenticado para realizar esta ação.");
        }
        return _sessao.UsuarioId.Value;
    }

    public void Criar(string titulo, string texto)
    {
        ValidarDados(titulo, texto);
        int usuarioId = ObterUsuarioAutenticado();

        using MySqlConnection conexao = _conexao.CriarConexao();
        conexao.Open();

        const string sql = @"
            INSERT INTO publicacoes (Titulo, Texto, DataCriacao, UsuarioId) 
            VALUES (@titulo, @texto, NOW(), @usuarioId);";

        using var comando = new MySqlCommand(sql, conexao);
        comando.Parameters.AddWithValue("@titulo", titulo.Trim());
        comando.Parameters.AddWithValue("@texto", texto.Trim());
        comando.Parameters.AddWithValue("@usuarioId", usuarioId);

        comando.ExecuteNonQuery();
    }

    public List<Publicacao> ListarRecentes()
    {
        var lista = new List<Publicacao>();

        using MySqlConnection conexao = _conexao.CriarConexao();
        conexao.Open();

        const string sql = @"
            SELECT Id, Titulo, Texto, DataCriacao, UsuarioId 
            FROM publicacoes 
            ORDER BY DataCriacao DESC;";

        using var comando = new MySqlCommand(sql, conexao);
        using MySqlDataReader leitor = comando.ExecuteReader();

        while (leitor.Read())
        {
            lista.Add(new Publicacao
            {
                Id = leitor.GetInt32("Id"),
                Titulo = leitor.GetString("Titulo"),
                Texto = leitor.GetString("Texto"),
                DataCriacao = leitor.GetDateTime("DataCriacao"),
                UsuarioId = leitor.GetInt32("UsuarioId")
            });
        }

        return lista;
    }

    public Publicacao? ObterPorId(int id)
    {
        using MySqlConnection conexao = _conexao.CriarConexao();
        conexao.Open();

        const string sql = @"
            SELECT Id, Titulo, Texto, DataCriacao, UsuarioId 
            FROM publicacoes 
            WHERE Id = @id;";

        using var comando = new MySqlCommand(sql, conexao);
        comando.Parameters.AddWithValue("@id", id);

        using MySqlDataReader leitor = comando.ExecuteReader();

        if (leitor.Read())
        {
            return new Publicacao
            {
                Id = leitor.GetInt32("Id"),
                Titulo = leitor.GetString("Titulo"),
                Texto = leitor.GetString("Texto"),
                DataCriacao = leitor.GetDateTime("DataCriacao"),
                UsuarioId = leitor.GetInt32("UsuarioId")
            };
        }

        return null;
    }

    public string ObterNomeAutor(int publicacaoId)
    {
        using MySqlConnection conexao = _conexao.CriarConexao();
        conexao.Open();

        const string sql = @"
            SELECT u.Nome 
            FROM publicacoes p
            INNER JOIN usuarios u ON p.UsuarioId = u.Id
            WHERE p.Id = @id;";

        using var comando = new MySqlCommand(sql, conexao);
        comando.Parameters.AddWithValue("@id", publicacaoId);

        object? resultado = comando.ExecuteScalar();
        return resultado?.ToString() ?? "Autor Desconhecido";
    }
}