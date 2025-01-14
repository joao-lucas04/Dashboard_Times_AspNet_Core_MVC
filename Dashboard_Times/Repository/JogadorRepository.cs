using Dashboard_Times.Models;
using Dashboard_Times.Repository.Contract;
using MySql.Data.MySqlClient;
using System.Data;

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
                cmd.Parameters.AddWithValue("@IdTime", jogador.IdTime);
                cmd.Parameters.AddWithValue("@IdPosicao", jogador.RefIdPosicao.IdPosicao);

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Jogador> BuscarJogador(string termo)
        {
            List<Jogador> jogadores = new List<Jogador>();

            using (MySqlConnection conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                // Primeiro, tenta buscar pelo nome
                MySqlCommand cmdNome = new MySqlCommand("call BuscarJogadorNomeCompleto(@NomeCompleto)", conexao);
                cmdNome.Parameters.AddWithValue("@NomeCompleto", termo);

                using (var reader = cmdNome.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        jogadores.Add(new Jogador
                        {
                            IdJogador = reader.GetInt32("IdJogador"),
                            NomeCompleto = reader.GetString("NomeCompleto"),
                            NomeCamisa = reader.GetString("NomeCamisa"),
                            Idade = reader.GetInt32("Idade"),
                            NumeroCamisa = reader.GetInt32("NumeroCamisa"),

                            //se o time for nulo retorna o Idzero, sem times
                            RefIdTime = reader.IsDBNull(reader.GetOrdinal("IdTime"))
                            ? new Time { IdTime = 0, Nome = null }
                            : new Time
                            {
                                IdTime = reader.GetInt32("IdTime"),
                                Nome = reader.GetString("NomeTime"),
                            },

                            RefIdPosicao = new Posicao
                            {
                                IdPosicao = reader.GetInt32("IdPosicao"),
                                Nome = reader.GetString("Nome"),
                            }
                        });
                    }
                }

                //se não achar por Nome tenta achar por ID
                if (!jogadores.Any())
                {
                    MySqlCommand cmdId = new MySqlCommand("call BuscarJogadorNomeCamisa(@NomeCamisa)", conexao);
                    cmdId.Parameters.AddWithValue("@NomeCamisa", termo);

                    using (var reader = cmdId.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            jogadores.Add(new Jogador
                            {
                                IdJogador = reader.GetInt32("IdJogador"),
                                NomeCompleto = reader.GetString("NomeCompleto"),
                                NomeCamisa = reader.GetString("NomeCamisa"),
                                Idade = reader.GetInt32("Idade"),
                                NumeroCamisa = reader.GetInt32("NumeroCamisa"),

                                RefIdTime = reader.IsDBNull(reader.GetOrdinal("IdTime"))
                                ? new Time { IdTime = 0, Nome = null }
                                : new Time
                                {
                                    IdTime = reader.GetInt32("IdTime"),
                                    Nome = reader.GetString("NomeTime"),
                                },

                                RefIdPosicao = new Posicao
                                {
                                    IdPosicao = reader.GetInt32("IdPosicao"),
                                    Nome = reader.GetString("Nome"),
                                }
                            });
                        }
                    }
                } 
                if (!jogadores.Any())
                {
                    MySqlCommand cmdId = new MySqlCommand("call BuscarJogadorId(@Id)", conexao);
                    cmdId.Parameters.AddWithValue("@Id", termo);

                    using (var reader = cmdId.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            jogadores.Add(new Jogador
                            {
                                IdJogador = reader.GetInt32("IdJogador"),
                                NomeCompleto = reader.GetString("NomeCompleto"),
                                NomeCamisa = reader.GetString("NomeCamisa"),
                                Idade = reader.GetInt32("Idade"),
                                NumeroCamisa = reader.GetInt32("NumeroCamisa"),

                                RefIdTime = reader.IsDBNull(reader.GetOrdinal("IdTime"))
                                ? new Time { IdTime = 0, Nome = null }
                                : new Time
                                {
                                    IdTime = reader.GetInt32("IdTime"),
                                    Nome = reader.GetString("NomeTime"),
                                },

                                RefIdPosicao = new Posicao
                                {
                                    IdPosicao = reader.GetInt32("IdPosicao"),
                                    Nome = reader.GetString("Nome"),
                                }
                            });
                        }
                    }
                }
            }

            return jogadores;
        }

        public void CadastrarJogador(Jogador jogador)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                var query = "CALL CadastrarJogador(@NomeCompleto, @NomeCamisa, @Idade, @NumeroCamisa, @IdTime, @IdPosicao)";

                MySqlCommand cmd = new MySqlCommand(query, conexao);

                cmd.Parameters.Add("@NomeCompleto", MySqlDbType.VarChar).Value = jogador.NomeCompleto;
                cmd.Parameters.Add("@NomeCamisa", MySqlDbType.VarChar).Value = jogador.NomeCamisa;
                cmd.Parameters.Add("@Idade", MySqlDbType.Int16).Value = jogador.Idade;
                cmd.Parameters.Add("@NumeroCamisa", MySqlDbType.Int16).Value = jogador.NumeroCamisa;
                cmd.Parameters.Add("@IdTime", MySqlDbType.Int64).Value = jogador.IdTime;
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
                MySqlCommand cmd = new MySqlCommand("CALL DeletarJogador(@IdJogador)", conexao);
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

                                RefIdTime = reader["NomeTime"] != DBNull.Value
                                ? new Time { Abreviacao = reader["NomeTime"].ToString() }
                                : null,

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
                            RefIdTime = reader["NomeTime"] != DBNull.Value
                            ? new Time { Abreviacao = reader["NomeTime"].ToString() }
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
