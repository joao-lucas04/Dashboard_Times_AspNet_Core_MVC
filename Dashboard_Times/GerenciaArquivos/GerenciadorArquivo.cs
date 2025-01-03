namespace Dashboard_Times.GerenciaArquivos
{
    public class GerenciadorArquivo
    {
        public static string CadastrarImagemTimes(IFormFile file)
        {
            var NomeArquivo = Path.GetFileName(file.FileName);
            var Caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/times", NomeArquivo);

            using (var stream = new FileStream(Caminho, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return Path.Combine("/img/times", NomeArquivo).Replace("\\", "/");
        }
    }
}
