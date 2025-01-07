using Dashboard_Times.Models;
using OfficeOpenXml;

namespace Dashboard_Times.GerenciaArquivos.Exportacoes
{
    public class GerenciadorExportacoes
    {
        public static byte[] ExportarExcelTime(List<Time> times)
        {
            // Cria um pacote de Excel usando EPPlus
            using (var pacote = new ExcelPackage())
            {
                // Cria uma nova planilha
                var planilha = pacote.Workbook.Worksheets.Add("Times");

                // Define o cabeçalho da tabela
                planilha.Cells[1, 1].Value = "Code";
                planilha.Cells[1, 2].Value = "Photo";
                planilha.Cells[1, 3].Value = "Name";
                planilha.Cells[1, 4].Value = "Abbreviation";

                // Preenche os dados da tabela a partir da lista de 'times'
                for (int i = 0; i < times.Count; i++)
                {
                    var time = times[i];
                    planilha.Cells[i + 2, 1].Value = time.IdTime;
                    planilha.Cells[i + 2, 2].Value = time.Img;
                    planilha.Cells[i + 2, 3].Value = time.Nome;
                    planilha.Cells[i + 2, 4].Value = time.Abreviacao;
                }

                // Autoajusta as colunas para o conteúdo
                planilha.Cells.AutoFitColumns();

                // Retorna o arquivo Excel como um array de bytes
                return pacote.GetAsByteArray();
            }
        }

        public static byte[] ExportarExcelJogador(List<Jogador> jogadores)
        {
            // Cria um pacote de Excel usando EPPlus
            using (var pacote = new ExcelPackage())
            {
                // Cria uma nova planilha
                var planilha = pacote.Workbook.Worksheets.Add("Jogadores");

                // Define o cabeçalho da tabela
                planilha.Cells[1, 1].Value = "Code";
                planilha.Cells[1, 2].Value = "Full name";
                planilha.Cells[1, 3].Value = "Shirt Name";
                planilha.Cells[1, 4].Value = "Age";
                planilha.Cells[1, 5].Value = "Position";
                planilha.Cells[1, 6].Value = "Shirt Number";
                planilha.Cells[1, 7].Value = "Current Team";

                // Preenche os dados da tabela a partir da lista de 'times'
                for (int i = 0; i < jogadores.Count; i++)
                {
                    var jogador = jogadores[i];
                    planilha.Cells[i + 2, 1].Value = jogador.IdJogador;
                    planilha.Cells[i + 2, 2].Value = jogador.NomeCompleto;
                    planilha.Cells[i + 2, 3].Value = jogador.NomeCamisa;
                    planilha.Cells[i + 2, 4].Value = jogador.Idade;
                    planilha.Cells[i + 2, 5].Value = jogador.RefIdPosicao.Nome;
                    planilha.Cells[i + 2, 6].Value = jogador.NumeroCamisa;
                    planilha.Cells[i + 2, 7].Value = jogador.RefIdTime.Abreviacao;
                }

                // Autoajusta as colunas para o conteúdo
                planilha.Cells.AutoFitColumns();

                // Retorna o arquivo Excel como um array de bytes
                return pacote.GetAsByteArray();
            }
        }
    }
}
