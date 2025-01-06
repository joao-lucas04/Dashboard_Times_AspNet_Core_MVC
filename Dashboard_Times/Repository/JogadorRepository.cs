using Dashboard_Times.Models;
using Dashboard_Times.Repository.Contract;
using MySql.Data.MySqlClient;

namespace Dashboard_Times.Repository
{
    public class JogadorRepository : IJogadorRepository
    {
        private readonly string _conexaoMySQL;

        public JogadorRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void AtualizarJogador(Jogador jogador)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                var query = "UPDATE tbJogador SET " +
                    "NomeCompleto = @NomeCompleto, " +
                    "NomeCamisa = @NomeCamisa, " +
                    "Idade = @Idade, " +
                    "NumeroCamisa = @NumeroCamisa, " +
                    "IdTime = @IdTime, " +
                    "IdPosicao = @IdPosicao " +
                    "WHERE IdJogador = @IdJogador";

                MySqlCommand cmd = new MySqlCommand(query, conexao);

                cmd.Parameters.AddWithValue("@IdJogador", jogador.IdJogador);
                cmd.Parameters.AddWithValue("@NomeCompleto", jogador.NomeCompleto);
                cmd.Parameters.AddWithValue("@NomeCamisa", jogador.NomeCamisa);
                cmd.Parameters.AddWithValue("@Idade", jogador.Idade);
                cmd.Parameters.AddWithValue("@NumeroCamisa", jogador.NumeroCamisa);
                cmd.Parameters.AddWithValue("@IdTime", jogador.IdTime ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IdPosicao", jogador.RefIdPosicao.IdPosicao);

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Jogador> BuscarJogador(string termo)
        {
            throw new NotImplementedException();
        }

        public void CadastrarJogador(Jogador jogador)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                var query = "INSERT INTO tbJogador(NomeCompleto, NomeCamisa, Idade, NumeroCamisa, IdTime, IdPosicao) values" +
                    "(@NomeCompleto, @NomeCamisa, @Idade, @NumeroCamisa, @IdTime, @IdPosicao)";

                MySqlCommand cmd = new MySqlCommand(query, conexao);

                cmd.Parameters.Add("@NomeCompleto", MySqlDbType.VarChar).Value = jogador.NomeCompleto;
                cmd.Parameters.Add("@NomeCamisa", MySqlDbType.VarChar).Value = jogador.NomeCamisa;
                cmd.Parameters.Add("@Idade", MySqlDbType.Int16).Value = jogador.Idade;
                cmd.Parameters.Add("@NumeroCamisa", MySqlDbType.Int16).Value = jogador.NumeroCamisa;
                cmd.Parameters.Add("@IdTime", MySqlDbType.Int64).Value = jogador.RefIdTime.IdTime;
                cmd.Parameters.Add("@IdPosicao", MySqlDbType.Int64).Value = jogador.RefIdPosicao.IdPosicao;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void DeletarJogador(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM tbJogador WHERE IdJogador = @IdJogador", conexao);
                cmd.Parameters.AddWithValue("@IdJogador", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Jogador ObterJogador(int Id)
        {
            Jogador jogador = null;
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                var query = "CALL ObterJogador(@IdJogador)";

                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@IdJogador", Id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            jogador = new Jogador
                            {
                                IdJogador = Convert.ToInt32(reader["IdJogador"]),
                                NomeCompleto = reader["NomeCompleto"].ToString(),
                                NomeCamisa = reader["NomeCamisa"].ToString(),
                                Idade = Convert.ToInt16(reader["Idade"]),
                                NumeroCamisa = Convert.ToInt16(reader["NumeroCamisa"]),

                                RefIdTime = new Time
                                {
                                    IdTime = Convert.ToInt32(reader["IdTime"]),
                                    Abreviacao = reader["Abreviacao"].ToString()
                                },

                                RefIdPosicao = new Posicao
                                {
                                    IdPosicao = Convert.ToInt32(reader["IdPosicao"]),
                                    Nome = reader["Nome"].ToString(),
                                }
                            };
                        }
                    }
                }
            }
            return jogador;
        }

        public IEnumerable<Jogador> ObterTodosJogadores()
        {
            List<Jogador> jogadores = new List<Jogador>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("CALL ObterTodosJogadores()", conexao);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        jogadores.Add(new Jogador
                        {
                            IdJogador = Convert.ToInt32(reader["IdJogador"]), 
                            NomeCompleto = reader["NomeCompleto"].ToString(),
                            Idade = Convert.ToInt16(reader["Idade"]),
                            NomeCamisa = reader["NomeCamisa"].ToString(),
                            NumeroCamisa = Convert.ToInt16(reader["NumeroCamisa"]),

                            // Preenchendo a propriedade RefIdTime com um objeto Time
                            RefIdTime = reader["Abreviacao"] != DBNull.Value
                            ? new Time { Abreviacao = reader["Abreviacao"].ToString() }
                            : null,

                            // Preenchendo a propriedade RefIdPosicao com uma string
                            RefIdPosicao = new Posicao { Nome = reader["Nome"].ToString() }
                        });
                    }
                }
            }
            return jogadores;
        }
    }
}
