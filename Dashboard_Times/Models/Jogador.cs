using System.ComponentModel.DataAnnotations;

namespace Dashboard_Times.Models
{
    public class Jogador
    {
        [Key]
        public int IdJogador { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [StringLength(250, ErrorMessage = "O nome completo deve ter no máximo 250 caracteres.")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O nome da camisa é obrigatório.")]
        [StringLength(12, ErrorMessage = "O nome da camisa deve ter no máximo 12 caracteres.")]
        public string NomeCamisa { get; set; }

        [Required(ErrorMessage = "A idade é obrigatória.")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "O número da camisa é obrigatório.")]
        public int NumeroCamisa { get; set; }

        [Required(ErrorMessage = "Selecione o Time Atual")]
        public Time RefIdTime { get; set; }

        [Required(ErrorMessage = "Selecione uma Posição")]
        public Posicao RefIdPosicao { get; set; }
        public int? IdTime { get; set; }
    }
}
