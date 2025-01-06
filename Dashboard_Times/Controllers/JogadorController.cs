using Dashboard_Times.GerenciaArquivos;
using Dashboard_Times.Models;
using Dashboard_Times.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dashboard_Times.Controllers
{
    public class JogadorController : Controller
    {
        private readonly ILogger<JogadorController> _logger;
        private IJogadorRepository _jogadorRepository;
        private IPosicaoRepository _posicaoRepository;
        private ITimeRepository _timeRepository;

        public JogadorController(ILogger<JogadorController> logger, ITimeRepository timeRepository, IJogadorRepository jogadorRepository, IPosicaoRepository posicaoRepository)
        {
            _logger = logger;
            _jogadorRepository = jogadorRepository;
            _posicaoRepository = posicaoRepository;
            _timeRepository = timeRepository;
        }

        public IActionResult Index()
        {
            return View(_jogadorRepository.ObterTodosJogadores());
        }

        public IActionResult CadastrarJogador()
        {
            var listaTimes = _timeRepository.ObterTodosTimes();
            ViewBag.ListaTimes = new SelectList(listaTimes, "IdTime", "Abreviacao");

            var listaPosicoes = _posicaoRepository.ObterTodasPosicoes();
            ViewBag.ListaPosicoes = new SelectList(listaPosicoes, "IdPosicao", "Nome");

            return View();
        }

        [HttpPost]
        public IActionResult CadJogador(Jogador jogador)
        {
            var listaTimes = _timeRepository.ObterTodosTimes();
            ViewBag.ListaTimes = new SelectList(listaTimes, "IdTime", "Abreviacao");

            var listaPosicoes = _posicaoRepository.ObterTodasPosicoes();
            ViewBag.ListaPosicoes = new SelectList(listaPosicoes, "IdPosicao", "Nome");

            jogador.IdTime = jogador.RefIdTime.IdTime;

            _jogadorRepository.CadastrarJogador(jogador);
            return RedirectToAction(nameof(Index));
        }
    }
}
