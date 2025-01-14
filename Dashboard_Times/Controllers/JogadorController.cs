using Dashboard_Times.GerenciaArquivos;
using Dashboard_Times.GerenciaArquivos.Exportacoes;
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
        private IEstatisticasJogadorRepository _EstJogador;

        public JogadorController(ILogger<JogadorController> logger, IEstatisticasJogadorRepository EstJogador, ITimeRepository timeRepository, IJogadorRepository jogadorRepository, IPosicaoRepository posicaoRepository)
        {
            _logger = logger;
            _jogadorRepository = jogadorRepository;
            _posicaoRepository = posicaoRepository;
            _timeRepository = timeRepository;
            _EstJogador = EstJogador;
        }

        public IActionResult Index(string termo)
        {
            //Devolve oque foi escrito na input depois da nova consulta
            ViewBag.txtTermo = termo;

            IEnumerable<Jogador> jogadores;

            //se o termo de pesquisa for nulo
            if (!string.IsNullOrEmpty(termo))
            {
                //primeiro tenta se o nome ou codigo forem registrados
                try
                {
                    jogadores = _jogadorRepository.BuscarJogador(termo);
                }
                //se retornar uma exeption é porque o nome do time não esta cadastrado
                catch (Exception)
                {
                    ViewBag.MsgErro = "No Team Found";
                    jogadores = Enumerable.Empty<Jogador>(); //retorna uma lista vazia pois não há registros encontrados pelo termo
                }
            }
            else
            {
                jogadores = _jogadorRepository.ObterTodosJogadores();
            }

            //contagem dos registros da tabela
            var totalJogador = jogadores.Count();
            ViewData["TotalJogadores"] = totalJogador;

            if (totalJogador == 0)
            {
                ViewBag.MsgErro = "No Team Found";
            }

            return View(jogadores);
        }

        public IActionResult CadastrarJogador()
        {
            var listaTimes = _timeRepository.ObterTodosTimes().ToList();
            listaTimes.Insert(0, new Time { IdTime = 0, Nome = "Passes Livres" });
            ViewBag.ListaTimes = new SelectList(listaTimes, "IdTime", "Nome");

            var listaPosicoes = _posicaoRepository.ObterTodasPosicoes();
            ViewBag.ListaPosicoes = new SelectList(listaPosicoes, "IdPosicao", "Nome");

            return View();
        }

        [HttpPost]
        public IActionResult CadJogador(Jogador jogador)
        {
            var listaTimes = _timeRepository.ObterTodosTimes().ToList();
            listaTimes.Insert(0, new Time { IdTime = 0, Nome = "Passes Livres" });
            ViewBag.ListaTimes = new SelectList(listaTimes, "IdTime", "Nome");

            var listaPosicoes = _posicaoRepository.ObterTodasPosicoes();
            ViewBag.ListaPosicoes = new SelectList(listaPosicoes, "IdPosicao", "Nome");

            jogador.IdTime = jogador.RefIdTime.IdTime;

            if (jogador.IdTime == 0)
            {
                jogador.IdTime = null;
            }

            _jogadorRepository.CadastrarJogador(jogador);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditarJogador(int Id)
        {
            var listaTimes = _timeRepository.ObterTodosTimes().ToList();
            listaTimes.Insert(0, new Time { IdTime = 0, Nome = "Passes Livres" });
            ViewBag.ListaTimes = new SelectList(listaTimes, "IdTime", "Nome");

            var listaPosicoes = _posicaoRepository.ObterTodasPosicoes();
            ViewBag.ListaPosicoes = new SelectList(listaPosicoes, "IdPosicao", "Nome");

            var jogador = _jogadorRepository.ObterJogador(Id);

            // se o time for nulo ele pega o IdTime = 0
            if(jogador.RefIdTime == null)
            {
                jogador.RefIdTime = new Time { IdTime = 0 };
            }

            return View(jogador);
        }

        public IActionResult EditJogador(Jogador jogador)
        {
            if (!ModelState.IsValid)
            {
                var listaTimes = _timeRepository.ObterTodosTimes().ToList();
                listaTimes.Insert(0, new Time { IdTime = 0, Nome = "Passes Livres" });
                ViewBag.ListaTimes = new SelectList(listaTimes, "IdTime", "Nome");

                var listaPosicoes = _posicaoRepository.ObterTodasPosicoes();
                ViewBag.ListaPosicoes = new SelectList(listaPosicoes, "IdPosicao", "Nome");

                jogador.IdTime = jogador.RefIdTime.IdTime;

                if (jogador.IdTime == 0)
                {
                    jogador.IdTime = null;
                }
            }
            
            _jogadorRepository.AtualizarJogador(jogador);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DelJogador(int Id)
        {
            _jogadorRepository.DeletarJogador(Id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ExportarParaExecel()
        {
            // Pega todos os times
            var jogadores = _jogadorRepository.ObterTodosJogadores().ToList();

            // Chama o método de exportação
            var arquivoExcel = GerenciadorExportacoes.ExportarExcelJogador(jogadores);

            // Retorna o arquivo Excel como um download
            return File(arquivoExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "jogadores.xlsx");
        }

        public IActionResult ViewJogador(int Id)
        {
            var jogadorEstatisticas = _EstJogador.ObterEstatisticas(Id);

            ViewBag.TotalChutes = jogadorEstatisticas.ChutesFora + jogadorEstatisticas.ChutesGol + jogadorEstatisticas.Gols;

            return View(jogadorEstatisticas);
        }

        public IActionResult EditarEstatisticas(int Id)
        {
            var jogadorEstatisticas = _EstJogador.ObterEstatisticas(Id);
            return View(jogadorEstatisticas);
        }

        public IActionResult EditEstatisticasJogador(EstatisticasJogador EstJogador)
        {
            _EstJogador.AtualizarEstatisticas(EstJogador);
            return RedirectToAction("ViewJogador", new { Id = EstJogador.IdJogador });
        }
    }
}
