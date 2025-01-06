using Dashboard_Times.Models;
using OfficeOpenXml;

namespace Dashboard_Times.GerenciaArquivos.Exportacoes
{
    public class GerenciadorExportacoes
    {
        public static byte[] ExportarExcel(List<Time> times)
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
    }
}
