using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Wipro.Locadora.Models;
using Wipro.Locadora.Repositories.Interfaces;
using Wipro.Locadora.Services;

namespace Locadora.Wipro.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        readonly Conexao conexao = new Conexao();

        /// <summary>
        /// Obtém um Filme pelo seu Id
        /// </summary>
        /// <param name="idFilme">Parâmetro do tipo int</param>
        /// <returns>Objeto do tipo Filme</returns>
        public Filme GetFilmePorId(int idFilme)
        {
            try
            {
                var stringBuilder = new StringBuilder();
                var query = stringBuilder.Append("SELECT idFilme, nomeFilme, dtLancamento, disponibilidade FROM tb_Filme WHERE idFilme = @idFilme").ToString();

                using (SqlConnection conexaoDb = new SqlConnection(conexao.StringConexao))
                {
                    conexaoDb.Open();

                    using (SqlCommand comandoDb = new SqlCommand(query, conexaoDb))
                    {
                        comandoDb.Parameters.AddWithValue("@idFilme", idFilme);

                        SqlDataReader leituraDb = comandoDb.ExecuteReader();
                        if (leituraDb.HasRows)
                        {
                            while (leituraDb.Read())
                            {
                                Filme filme = new Filme()
                                {
                                    IdFilme = Convert.ToInt32(leituraDb["idFilme"]),
                                    NomeFilme = leituraDb["nomeFilme"].ToString(),
                                    Disponibilidade = Convert.ToBoolean(leituraDb["disponibilidade"]),
                                    DtLancamento = Convert.ToDateTime(leituraDb["dtLancamento"])
                                };
                                return filme;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception exception) 
            { 
                throw exception; 
            }
        }

        /// <summary>
        /// Obtém uma lista de todos os filmes cadastrados
        /// </summary>
        /// <returns>List de Filmes</returns>
        public List<Filme> GetFilmes()
        {
            try
            {
                var stringBuilder = new StringBuilder();
                var query = stringBuilder.Append(@"SELECT idFilme, nomeFilme, dtLancamento, disponibilidade FROM tb_Filme;").ToString();

                List<Filme> listaFilmes = new List<Filme>();

                using (SqlConnection conexaoDb = new SqlConnection(this.conexao.StringConexao))
                {
                    conexaoDb.Open();

                    using (SqlCommand comandoDb = new SqlCommand(query, conexaoDb))
                    {
                        SqlDataReader leituraDb = comandoDb.ExecuteReader();
                        if (leituraDb.HasRows)
                        {
                            while (leituraDb.Read())
                            {
                                Filme filme = new Filme()
                                {
                                    IdFilme = Convert.ToInt32(leituraDb["idFilme"]),
                                    NomeFilme = leituraDb["nomeFilme"].ToString(),
                                    Disponibilidade = Convert.ToBoolean(leituraDb["disponibilidade"]),
                                    DtLancamento = Convert.ToDateTime(leituraDb["dtLancamento"])
                                };
                                listaFilmes.Add(filme);
                            }
                        }
                    }
                }
                return listaFilmes;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Registra um filme
        /// </summary>
        /// <param name="filme">Objeto do tipo Filme</param>
        public void Post(Filme filme)
        {
            try
            {
                var stringBuilder = new StringBuilder();
                var query = stringBuilder.Append(@"INSERT INTO tb_Filme (nomeFilme, dtLancamento, disponibilidade) VALUES (@nomeFilme, @dtLancamento, @disponibilidade);").ToString();

                using (SqlConnection conexaoDb = new SqlConnection(conexao.StringConexao))
                {
                    conexaoDb.Open();
                    SqlCommand comandoDb = new SqlCommand(query, conexaoDb);
                    comandoDb.Parameters.AddWithValue("@nomeFilme", filme.NomeFilme);
                    comandoDb.Parameters.AddWithValue("@dtLancamento", filme.DtLancamento);
                    comandoDb.Parameters.AddWithValue("@disponibilidade", filme.Disponibilidade);
                    comandoDb.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Atualiza um filme
        /// </summary>
        /// <param name="filme">Objeto do tipo Filme</param>
        public void Put(Filme filme)
        {
            try
            {
                var stringBuilder = new StringBuilder();
                var query = stringBuilder.Append(@"UPDATE tb_Filme SET disponibilidade = @disponibilidade, nomeFilme = @nomeFilme, dtLancamento = @dtLancamento WHERE idFilme = @idFilme;").ToString();

                GetFilmePorId(filme.IdFilme);

                if (filme != null)
                {
                    using (SqlConnection conexaoDb = new SqlConnection(conexao.StringConexao))
                    {
                        conexaoDb.Open();
                        SqlCommand comandoDb = new SqlCommand(query, conexaoDb);
                        comandoDb.Parameters.AddWithValue("@idFilme", filme.IdFilme);
                        comandoDb.Parameters.AddWithValue("@nomeFilme", filme.NomeFilme);
                        comandoDb.Parameters.AddWithValue("@dtLancamento", filme.DtLancamento);
                        comandoDb.Parameters.AddWithValue("@disponibilidade", filme.Disponibilidade);
                        comandoDb.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Atualiza a disponibilidade de um filme
        /// </summary>
        /// <param name="idFilme">Id do tipo Int</param>
        /// <param name="disponibilidade">Valor de tipo Booleano</param>
        public void PutDisponibilidade(int idFilme, bool disponibilidade)
        {
            try
            {
                var stringBuilder = new StringBuilder();
                var query = stringBuilder.Append(@"UPDATE tb_Filme SET disponibilidade = @disponibilidade WHERE idFilme = @idFilme;").ToString();                

                if (GetFilmePorId(idFilme) != null)
                {
                    using (SqlConnection conexaoDb = new SqlConnection(conexao.StringConexao))
                    {
                        conexaoDb.Open();
                        SqlCommand comandoDb = new SqlCommand(query, conexaoDb);
                        comandoDb.Parameters.AddWithValue("@idFilme", idFilme);
                        comandoDb.Parameters.AddWithValue("@disponibilidade", disponibilidade);
                        comandoDb.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}