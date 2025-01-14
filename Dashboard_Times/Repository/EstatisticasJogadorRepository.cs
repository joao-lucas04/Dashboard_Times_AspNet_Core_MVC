using Dashboard_Times.Models;
using Dashboard_Times.Repository.Contract;
using MySql.Data.MySqlClient;

namespace Dashboard_Times.Repository
{
    public class EstatisticasJogadorRepository : IEstatisticasJogadorRepository
    {
        private readonly string _conexaoMySQL;

        public EstatisticasJogadorRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void AtualizarEstatisticas(EstatisticasJogador EstJogador)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                var query = "UPDATE tbEstatisticasJogador SET " +
                        "ChutesFora = @ChutesFora, " +
                        "ChutesGol = @ChutesGol, " +
                        "Gols = @Gols, " +
                        "Dribles = @Dribles, " +
                        "Assistencias = @Assistencias, " +
                        "Passes = @Passes, " +
                        "Cruzamentos = @Cruzamentos, " +
                        "Impedimentos = @Impedimentos, " +
                        "Desarmes = @Desarmes, " +
                        "DuelosGanhos = @DuelosGanhos, " +
                        "Interceptacoes = @Interceptacoes, " +
                        "BolasDefendidas = @BolasDefendidas, " +
                        "BolasDificeisDefendidas = @BolasDificeisDefendidas, " +
                        "GolsSofridos = @GolsSofridos, " +
                        "FaltasSofridas = @FaltasSofridas, " +
                        "FaltasCometidas = @FaltasCometidas, " +
                        "PenaltisSofridos = @PenaltisSofridos, " +
                        "PenaltisCometidos = @PenaltisCometidos, " +
                        "CartoesAmarelos = @CartoesAmarelos, " +
                        "CartoesVermelhos = @CartoesVermelhos, " +
                        "GolsPenaltis = @GolsPenaltis, " +
                        "GolsPenaltisPerdido = @GolsPenaltisPerdido, " +
                        "DefesasPenaltis = @DefesasPenaltis, " +
                        "GolsPenaltisSofridos = @GolsPenaltisSofridos " +
                        "WHERE IdEst = @IdEst";

                MySqlCommand cmd = new MySqlCommand(query, conexao);

                cmd.Parameters.AddWithValue("@IdEst", EstJogador.IdEst);
                cmd.Parameters.AddWithValue("@ChutesFora", EstJogador.ChutesFora);
                cmd.Parameters.AddWithValue("@ChutesGol", EstJogador.ChutesGol);
                cmd.Parameters.AddWithValue("@Gols", EstJogador.Gols);
                cmd.Parameters.AddWithValue("@Dribles", EstJogador.Dribles);
                cmd.Parameters.AddWithValue("@Assistencias", EstJogador.Assistencias);
                cmd.Parameters.AddWithValue("@Passes", EstJogador.Passes);
                cmd.Parameters.AddWithValue("@Cruzamentos", EstJogador.Cruzamentos);
                cmd.Parameters.AddWithValue("@Impedimentos", EstJogador.Impedimentos);
                cmd.Parameters.AddWithValue("@Desarmes", EstJogador.Desarmes);
                cmd.Parameters.AddWithValue("@DuelosGanhos", EstJogador.DuelosGanhos);
                cmd.Parameters.AddWithValue("@Interceptacoes", EstJogador.Interceptacoes);
                cmd.Parameters.AddWithValue("@BolasDefendidas", EstJogador.BolasDefendidas);
                cmd.Parameters.AddWithValue("@BolasDificeisDefendidas", EstJogador.BolasDificeisDefendidas);
                cmd.Parameters.AddWithValue("@GolsSofridos", EstJogador.GolsSofridos);
                cmd.Parameters.AddWithValue("@FaltasSofridas", EstJogador.FaltasSofridas);
                cmd.Parameters.AddWithValue("@FaltasCometidas", EstJogador.FaltasCometidas);
                cmd.Parameters.AddWithValue("@PenaltisSofridos", EstJogador.PenaltisSofridos);
                cmd.Parameters.AddWithValue("@PenaltisCometidos", EstJogador.PenaltisCometidos);
                cmd.Parameters.AddWithValue("@CartoesAmarelos", EstJogador.CartoesAmarelos);
                cmd.Parameters.AddWithValue("@CartoesVermelhos", EstJogador.CartoesVermelhos);
                cmd.Parameters.AddWithValue("@GolsPenaltis", EstJogador.GolsPenaltis);
                cmd.Parameters.AddWithValue("@GolsPenaltisPerdido", EstJogador.GolsPenaltisPerdido);
                cmd.Parameters.AddWithValue("@DefesasPenaltis", EstJogador.DefesasPenaltis);
                cmd.Parameters.AddWithValue("@GolsPenaltisSofridos", EstJogador.GolsPenaltisSofridos);

                cmd.ExecuteNonQuery();
            }             
        }

        public EstatisticasJogador ObterEstatisticas(int Id)
        {
            EstatisticasJogador EstJogador = null;
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                var query = "CALL ObterEstatisticasJogador(@IdJogador)";

                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@IdJogador", Id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            EstJogador = new EstatisticasJogador
                            {
                                IdEst = Convert.ToInt32(reader["IdEst"]),
                                ChutesFora = Convert.ToInt32(reader["ChutesFora"]),
                                ChutesGol = Convert.ToInt32(reader["ChutesGol"]),
                                Gols = Convert.ToInt32(reader["Gols"]),
                                Dribles = Convert.ToInt32(reader["Dribles"]),
                                Assistencias = Convert.ToInt32(reader["Assistencias"]),
                                Passes = Convert.ToInt32(reader["Passes"]),
                                Cruzamentos = Convert.ToInt32(reader["Cruzamentos"]),
                                Impedimentos = Convert.ToInt32(reader["Impedimentos"]),
                                Desarmes = Convert.ToInt32(reader["Desarmes"]),
                                DuelosGanhos = Convert.ToInt32(reader["DuelosGanhos"]),
                                Interceptacoes = Convert.ToInt32(reader["Interceptacoes"]),
                                BolasDefendidas = Convert.ToInt32(reader["BolasDefendidas"]),
                                BolasDificeisDefendidas = Convert.ToInt32(reader["BolasDificeisDefendidas"]),
                                GolsSofridos = Convert.ToInt32(reader["GolsSofridos"]),
                                FaltasSofridas = Convert.ToInt32(reader["FaltasSofridas"]),
                                FaltasCometidas = Convert.ToInt32(reader["FaltasCometidas"]),
                                PenaltisSofridos = Convert.ToInt32(reader["PenaltisSofridos"]),
                                PenaltisCometidos = Convert.ToInt32(reader["PenaltisCometidos"]),
                                CartoesAmarelos = Convert.ToInt32(reader["CartoesAmarelos"]),
                                CartoesVermelhos = Convert.ToInt32(reader["CartoesVermelhos"]),
                                GolsPenaltis = Convert.ToInt32(reader["GolsPenaltis"]),
                                GolsPenaltisPerdido = Convert.ToInt32(reader["GolsPenaltisPerdido"]),
                                DefesasPenaltis = Convert.ToInt32(reader["DefesasPenaltis"]),
                                GolsPenaltisSofridos = Convert.ToInt32(reader["GolsPenaltisSofridos"]),
                                IdJogador = Convert.ToInt32(reader["IdJogador"]),
                                NomeCompleto = reader["NomeCompleto"].ToString(),
                                Nome = reader["Nome"].ToString()
                            };
                        }
                    }
                }
            }
            return EstJogador;
        }
    }
}


