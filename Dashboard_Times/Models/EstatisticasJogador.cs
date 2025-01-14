using System.ComponentModel.DataAnnotations;

namespace Dashboard_Times.Models
{
    public class EstatisticasJogador
    {
        [Key]
        public int IdEst { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int ChutesFora { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int ChutesGol { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int Gols { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int Dribles { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int Assistencias { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int Passes { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int Cruzamentos { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int Impedimentos { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int Desarmes { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int DuelosGanhos { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int Interceptacoes { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int BolasDefendidas { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int BolasDificeisDefendidas { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int GolsSofridos { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int FaltasSofridas { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int FaltasCometidas { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int PenaltisSofridos { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int PenaltisCometidos { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int CartoesAmarelos { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int CartoesVermelhos { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int GolsPenaltis { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int GolsPenaltisPerdido { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int DefesasPenaltis { get; set; }

        [Required(ErrorMessage = "Todos os Campos são Obrigatório.")]
        public int GolsPenaltisSofridos { get; set; }


        // chave estrangeira
        public int IdJogador { get; set; }
        public string NomeCompleto { get; set; }
        public string Nome { get; set; }
    }
}
