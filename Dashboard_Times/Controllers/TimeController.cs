using Dashboard_Times.GerenciaArquivos;
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

        public IActionResult Index()
        {
            var times = _timeRepository.ObterTodosTimes();  
            var totalTimes = times.Count();  


            ViewData["TotalTimes"] = totalTimes;

            return View(times);  
        }

        public IActionResult CadastrarTime()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadTime(Time time, IFormFile file)
        {
            var Caminho = GerenciadorArquivo.CadastrarImagemTimes(file);
            time.Img = Caminho;

            _timeRepository.CadastrarTime(time);
            return RedirectToAction(nameof(Index)); 
        }

        public IActionResult EditarTime(int Id)
        {
            var time = _timeRepository.ObterTime(Id);

            if (time == null)
            {
                // Se o produto não for encontrado, redireciona ou exibe uma mensagem de erro
                return NotFound();
            }

            return View(time); // Passa o produto para a view
        }
        public IActionResult EditTime(Time time, IFormFile file)
        {
            _timeRepository.AtualizarTime(time);
            return RedirectToAction(nameof(Index));
        }
    

        public IActionResult DelTime(int Id)
        {
            _timeRepository.DeletarTime(Id);
            return RedirectToAction(nameof(Index));
        }
    }   
}
