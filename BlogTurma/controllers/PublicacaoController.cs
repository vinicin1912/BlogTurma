using BlogTurma.models;
using BlogTurma.services;
using BlogTurma.views;

namespace BlogTurma.controllers;

public class PublicacaoController
{
    private readonly PublicacaoService _service;
    private readonly PublicacaoView _view;
    private readonly SessaoUsuario _sessao;

    public PublicacaoController(PublicacaoService service, PublicacaoView view, SessaoUsuario sessao)
    {
        _service = service;
        _view = view;
        _sessao = sessao;
    }

    private bool VerificarSessao()
    {
        return _sessao.EstaAutenticado;
    }

    public void Listar()
    {
        try
        {
            var publicacoes = _service.ListarRecentes();
            _view.ExibirPublicacoes(publicacoes);
        }
        catch (Exception ex)
        {
            _view.ExibirMensagem($"Erro ao listar publicações: {ex.Message}");
        }
    }

    public void Criar()
    {
        if (!VerificarSessao())
        {
            _view.ExibirMensagem("Acesso negado. Você precisa estar conectado para criar uma publicação.");
            return;
        }

        var (titulo, texto) = _view.SolicitarDadosPublicacao();

        try
        {
            _service.Criar(titulo, texto);
            _view.ExibirMensagem("Publicação criada com sucesso!");
        }
        catch (Exception ex)
        {
            _view.ExibirMensagem($"Erro ao criar publicação: {ex.Message}");
        }
    }

    public void Visualizar()
    {
        int id = _view.SolicitarId();

        if (id <= 0)
        {
            _view.ExibirMensagem("ID inválido.");
            return;
        }

        try
        {
            Publicacao? publicacao = _service.ObterPorId(id);

            if (publicacao == null)
            {
                _view.ExibirMensagem("Publicação não encontrada.");
                return;
            }

            string nomeAutor = _service.ObterNomeAutor(publicacao.Id);

            // Lista de comentários vazia (será implementada no Dia 4)
            var comentariosVazios = new List<(string ComentarioTexto, string AutorComentario)>();

            _view.ExibirDetalhes(publicacao, nomeAutor, comentariosVazios);
        }
        catch (Exception ex)
        {
            _view.ExibirMensagem($"Erro ao visualizar publicação: {ex.Message}");
        }
    }
}