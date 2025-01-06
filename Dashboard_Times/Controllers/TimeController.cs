using Dashboard_Times.GerenciaArquivos;
using Dashboard_Times.GerenciaArquivos.Exportacoes;
using Dashboard_Times.Models;
using Dashboard_Times.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dashboard_Times.Controllers
{
    public class TimeController : Controller
    {
        private readonly ILogger<TimeController> _logger;
        private ITimeRepository _timeRepository;

        public TimeController(ILogger<TimeController> logger, ITimeRepository timeRepository)
        {
            _logger = logger;
            _timeRepository = timeRepository;
        }

        public IActionResult Index(string termo)
        {
            ViewBag.txtTermo = termo;

            IEnumerable<Time> times;

            //se o termo de pesquisa for nulo
            if (!string.IsNullOrEmpty(termo))
            {
                //primeiro tenta se o nome ou codigo forem registrados
                try
                {
                    times = _timeRepository.BuscarTime(termo);
                }
                //se retornar uma exeption é porque o nome do time não esta cadastrado
                catch (Exception)
                {
                    ViewBag.MsgErro = "No Team Found";
                    times = Enumerable.Empty<Time>(); //retorna uma lista vazia pois não há registros encontrados pelo termo
                }
            }
            else
            {
                times = _timeRepository.ObterTodosTimes();
            }
            
            //contagem dos registros da tabela
            var totalTimes = times.Count();
            ViewData["TotalTimes"] = totalTimes;

            if (totalTimes == 0)
            {
                ViewBag.MsgErro = "No Team Found";
            }

            return View(times);  
        }

        public IActionResult CadastrarTime()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadTime(Time time, IFormFile file)
        {
            try
            {
                var Caminho = GerenciadorArquivo.CadastrarImagemTimes(file);
                time.Img = Caminho;

                _timeRepository.CadastrarTime(time);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ViewBag.MsgErro = "This abbreviation has already been registered.";
                return View("CadastrarTime", time);
            }
        }

        public IActionResult EditarTime(int Id)
        {
            var time = _timeRepository.ObterTime(Id);

            return View(time);
        }
        public IActionResult EditTime(Time time, IFormFile file)
        {
            try
            {
                _timeRepository.AtualizarTime(time);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ViewBag.MsgErro = "This abbreviation has already been registered.";
                return View("EditarTime", time);
            }
        }
    

        public IActionResult DelTime(int Id)
        {
            _timeRepository.DeletarTime(Id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ExportarParaExecel()
        {
            // Pega todos os times
            var times = _timeRepository.ObterTodosTimes().ToList();

            // Chama o método de exportação
            var arquivoExcel = GerenciadorExportacoes.ExportarExcel(times);

            // Retorna o arquivo Excel como um download
            return File(arquivoExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "times.xlsx");
        }
    }   
}
