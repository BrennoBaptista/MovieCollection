using MovieCollection.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollection.Repository
{
    public class PessoaRepository
    {
        string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MovieCollection;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Listar
        public IEnumerable<Pessoa> ListarPessoas()
        {
            var usuarios = new List<Pessoa>();
            using var connection = new SqlConnection(_connectionString);
            var cmdText = "SELECT * FROM Pessoa";
            var select = new SqlCommand(cmdText, connection);

            try
            {
                connection.Open();
                using (var reader = select.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        var usuario = new Pessoa();
                        usuario.Id = (int)reader["Id"];
                        usuario.Nome = reader["Nome"].ToString();
                        usuario.Sobrenome = reader["Sobrenome"].ToString();
                        usuario.DataNascimento = (DateTime)reader["DataNascimento"];
                        usuarios.Add(usuario);
                    }
                }
            }
            finally
            {
                connection.Close();
            }
            return usuarios;
        }

        //Criar
        public void CriarPessoa(Pessoa pessoa)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            string cmdText = "INSERT INTO Pessoa (Nome, Sobrenome, DataNascimento) Values(@Nome, @Sobrenome, @DataNascimento)";
            SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Nome", pessoa.Nome);
            cmd.Parameters.AddWithValue("@Sobrenome", pessoa.Sobrenome);
            cmd.Parameters.AddWithValue("@DataNascimento", pessoa.DataNascimento);
            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }

        //Atualizar
        public void AtualizarPessoa(Pessoa pessoa)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            string cmdText = "UPDATE Pessoa SET Nome=@Nome, Sobrenome=@Sobrenome, DataNascimento=@DataNascimento WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@Id", pessoa.Id);
            cmd.Parameters.AddWithValue("@Nome", pessoa.Nome);
            cmd.Parameters.AddWithValue("@Sobrenome", pessoa.Sobrenome);
            cmd.Parameters.AddWithValue("@DataNascimento", pessoa.DataNascimento);
            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }

        //Detalhar
        public Pessoa DetalharPessoa(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            string sql = "SELECT Id, Nome, Sobrenome, DataNascimento FROM Pessoa WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            Pessoa pessoa = null;
            try
            {
                connection.Open();
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            pessoa = new Pessoa();
                            pessoa.Id = (int)reader["Id"];
                            pessoa.Nome = reader["Nome"].ToString();
                            pessoa.Sobrenome = reader["Sobrenome"].ToString();
                            pessoa.DataNascimento = (DateTime)reader["DataNascimento"];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return pessoa;
        }

        //Excluir
        public void ExcluirPessoa(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            string cmdText = "DELETE Pessoa WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}