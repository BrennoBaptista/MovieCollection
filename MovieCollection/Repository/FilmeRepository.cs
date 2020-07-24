using MovieCollection.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollection.Repository
{
    public class FilmeRepository
    {
        string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MovieCollection;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Listar filmes
        public IEnumerable<Filme> ListarFilmes()
        {
            var filmes = new List<Filme>();
            using var connection = new SqlConnection(_connectionString);
            var cmdText = "SELECT * FROM Filme";
            var select = new SqlCommand(cmdText, connection);
            try
            {
                connection.Open();
                using var reader = select.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    var filme = new Filme();
                    filme.Id = (int)reader["Id"];
                    filme.Titulo = reader["Titulo"].ToString();
                    filme.TituloOriginal = reader["TituloOriginal"].ToString();
                    filme.Ano = (int)reader["Ano"];
                    filme.Genero = reader["Genero"].ToString();
                    filmes.Add(filme);
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
            return filmes;
        }

        //Criar filmes
        public void CriarFilme(Filme filme)
        {
            using var connection = new SqlConnection(_connectionString);
            var cmdText = "INSERT INTO Filme (Titulo, TituloOriginal, Ano, Genero) VALUES (@Titulo, @TituloOriginal, @Ano, @Genero)";
            var insert = new SqlCommand(cmdText, connection);
            insert.CommandType = CommandType.Text;
            insert.Parameters.AddWithValue("@Titulo", filme.Titulo);
            insert.Parameters.AddWithValue("@TituloOriginal", filme.TituloOriginal);
            insert.Parameters.AddWithValue("@Ano", filme.Ano);
            insert.Parameters.AddWithValue("@Genero", filme.Genero);
            try
            {
                connection.Open();
                insert.ExecuteNonQuery();
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

        //Atualizar filmes
        public void AtualizarFilme(Filme filme)
        {
            using var connection = new SqlConnection(_connectionString);
            var cmdText = "UPDATE Filme SET Titulo=@Titulo, TituloOriginal=@TituloOriginal, Ano=@Ano, Genero=@Genero WHERE Id=@Id";
            var insert = new SqlCommand(cmdText, connection);
            insert.CommandType = CommandType.Text;
            insert.Parameters.AddWithValue("@Id", filme.Id);
            insert.Parameters.AddWithValue("@Titulo", filme.Titulo);
            insert.Parameters.AddWithValue("@TituloOriginal", filme.TituloOriginal);
            insert.Parameters.AddWithValue("@Ano", filme.Ano);
            insert.Parameters.AddWithValue("@Genero", filme.Genero);
            try
            {
                connection.Open();
                insert.ExecuteNonQuery();
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

        //Detalhar filmes
        public Filme DetalharFilme(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            string sql = "SELECT Id, Titulo, TituloOriginal, Ano, Genero FROM Filme WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            Filme filme = null;
            try
            {
                connection.Open();
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            filme = new Filme();
                            filme.Id = (int)reader["Id"];
                            filme.Titulo = reader["Titulo"].ToString();
                            filme.TituloOriginal = reader["TituloOriginal"].ToString();
                            filme.Ano = (int)reader["Ano"];
                            filme.Genero = reader["Genero"].ToString();
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
            return filme;
        }

        public Filme DetalharFilmePorGenero(string genero)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            string sql = "SELECT Id, Titulo, TituloOriginal, Ano, Genero FROM Filme WHERE Genero=@Genero";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Genero", genero);
            Filme filme = null;
            try
            {
                connection.Open();
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            filme = new Filme();
                            filme.Id = (int)reader["Id"];
                            filme.Titulo = reader["Titulo"].ToString();
                            filme.TituloOriginal = reader["TituloOriginal"].ToString();
                            filme.Ano = (int)reader["Ano"];
                            filme.Genero = reader["Genero"].ToString();
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
            return filme;
        }

        //Excluir filmes
        public void ExcluirFilme(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            string cmdText = "DELETE Filme WHERE Id=@Id";
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