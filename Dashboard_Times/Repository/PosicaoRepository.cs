using Dashboard_Times.Models;
using Dashboard_Times.Repository.Contract;
using MySql.Data.MySqlClient;
using System.Data;

namespace Dashboard_Times.Repository
{
    public class PosicaoRepository : IPosicaoRepository
    {
        private readonly string _conexaoMySQL;

        public PosicaoRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public IEnumerable<Posicao> ObterTodasPosicoes()
        {
            List<Posicao> posicoes = new List<Posicao>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbPosicao ORDER BY Nome ASC;", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    posicoes.Add(
                        new Posicao
                        {
                            IdPosicao = Convert.ToInt32(dr["IdPosicao"]),
                            Nome = (string)(dr["Nome"]),
                        });
                }
                return posicoes;
            }
        }
    }
}
