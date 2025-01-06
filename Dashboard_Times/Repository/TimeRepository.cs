using Dashboard_Times.Models;
using Dashboard_Times.Repository.Contract;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;
using System.Globalization;

namespace Dashboard_Times.Repository
{
    public class TimeRepository : ITimeRepository
    {
        private readonly string _conexaoMySQL;

        public TimeRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void AtualizarTime(Time time)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                var query = "UPDATE tbTime set Nome = @Nome, Abreviacao = @Abreviacao, Img = @Img WHERE IdTime = @IdTime";

                MySqlCommand cmd = new MySqlCommand(query, conexao);

                cmd.Parameters.AddWithValue("@IdTime", time.IdTime);
                cmd.Parameters.AddWithValue("@Nome", time.Nome);
                cmd.Parameters.AddWithValue("@Abreviacao", time.Abreviacao);
                cmd.Parameters.AddWithValue("@Img", time.Img);

                cmd.ExecuteNonQuery();
            }
        }

        //busca é feita pelo nome ou ID do time, através do LIKE das Procedures no MYSQL
        public IEnumerable<Time> BuscarTime(string termo)
        {
            
            List<Time> times = new List<Time>();

            using (MySqlConnection conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                // Primeiro, tenta buscar pelo nome
                MySqlCommand cmdNome = new MySqlCommand("call BuscarTimeNome(@Nome)", conexao);
                cmdNome.Parameters.AddWithValue("@Nome", termo);

                using (var reader = cmdNome.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        times.Add(new Time
                        {
                            IdTime = reader.GetInt32("IdTime"),
                            Nome = reader.GetString("Nome"),
                            Abreviacao = reader.GetString("Abreviacao"),
                            Img = reader.GetString("Img")
                        });
                    }
                }

                //se não achar por Nome tenta achar por ID
                if (!times.Any())
                {
                    MySqlCommand cmdId = new MySqlCommand("call BuscarTimeId(@Id)", conexao);
                    cmdId.Parameters.AddWithValue("@Id", termo);

                    using (var reader = cmdId.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            times.Add(new Time
                            {
                                IdTime = reader.GetInt32("IdTime"),
                                Nome = reader.GetString("Nome"),
                                Abreviacao = reader.GetString("Abreviacao"),
                                Img = reader.GetString("Img")
                            });
                        }
                    }
                }
            }

            return times;
        }

        public void CadastrarTime(Time time)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                var query = "INSERT INTO tbTime (Nome, Abreviacao, Img) VALUES (@Nome, @Abreviacao, @Img)";
                MySqlCommand cmd = new MySqlCommand(query, conexao);

                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = time.Nome;
                cmd.Parameters.Add("@Abreviacao", MySqlDbType.VarChar).Value = time.Abreviacao;
                cmd.Parameters.Add("@Img", MySqlDbType.VarChar).Value = time.Img;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void DeletarTime(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("call DeletarTime(@IdTime)", conexao);
                cmd.Parameters.AddWithValue("@IdTime", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Time ObterTime(int Id)
        {
            Time time = null;
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                
                conexao.Open();
                using (var cmd = new MySqlCommand("Select * from tbTime WHERE IdTime = @IdTime", conexao))
                {
                    cmd.Parameters.AddWithValue("@IdTime", Id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            time = new Time
                            {
                                IdTime = Convert.ToInt32(reader["IdTime"]),
                                Nome = reader["Nome"].ToString(),
                                Abreviacao = reader["Abreviacao"].ToString(),
                                Img = reader["Img"].ToString(),
                            };
                        }
                    }
                }
            }
            return time;
        }

        public IEnumerable<Time> ObterTodosTimes()
        {
            List<Time> times = new List<Time>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbTime", conexao);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        times.Add(new Time
                        {
                            IdTime = Convert.ToInt32(reader["IdTime"]), // Certifique-se de que o Id está sendo lido
                            Nome = reader["Nome"].ToString(),
                            Abreviacao = reader["Abreviacao"].ToString(),
                            Img = reader["Img"].ToString()
                        });
                    }
                }
            }
            return times;
        }
    }
}
