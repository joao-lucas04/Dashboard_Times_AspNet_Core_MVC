﻿namespace Dashboard_Times.Models
{
    public class Jogador
    {
        public int IdJogador { get; set; }
        public string NomeCompleto { get; set; }
        public string NomeCamisa { get; set; }
        public int Idade { get; set; }
        public int NumeroCamisa { get; set; }
        public Time RefIdTime { get; set; }
        public Posicao RefIdPosicao { get; set; }
        public int? IdTime { get; set; }
    }
}
