using Locadora.Wipro.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Wipro.Locadora.Models;
using Wipro.Locadora.Repositories.Interfaces;
using Wipro.Locadora.Services;

namespace Wipro.Locadora.Repositories
{
    public class LocacaoRepository : ILocacaoRepository
    {
        readonly Conexao conexao = new Conexao();

        /// <summary>
        /// Obtém uma locação pelo seu Id
        /// </summary>
        /// <param name="idLocacao">Valor que define um Id de uma locacao</param>
        /// <returns>Objeto do tipo Locacao</returns>
        public Locacao GetLocacaoPorId(int idLocacao)
        {
            try
            {
                var stringBuilder = new StringBuilder();
                var query = stringBuilder.Append(@"SELECT tl.idLocacao, tf.idFilme, tf.nomeFilme, tc.idCliente, tc.nomeCliente, tl.dtEntrega 
                                        FROM tb_Locacao tl 
                                        INNER JOIN tb_Cliente tc ON tl.idCliente = tc.idCliente 
                                        INNER JOIN tb_Filme tf ON tl.idFilme = tf.idFilme 
                                        WHERE idLocacao = @idLocacao;").ToString();

                using (SqlConnection conexaoDb = new SqlConnection(conexao.StringConexao))
                {
                    conexaoDb.Open();

                    using (SqlCommand comandoDb = new SqlCommand(query, conexaoDb))
                    {
                        comandoDb.Parameters.AddWithValue("@idLocacao", idLocacao);

                        SqlDataReader leituraDb = comandoDb.ExecuteReader();
                        if (leituraDb.HasRows)
                        {
                            while (leituraDb.Read())
                            {
                                Locacao locacao = new Locacao()
                                {
                                    IdLocacao = Convert.ToInt32(leituraDb["idLocacao"]),
                                    IdClienteNavigation = new Cliente()
                                    {
                                        IdCliente = Convert.ToInt32(leituraDb["idCliente"]),
                                        NomeCliente = leituraDb["nomeCliente"].ToString()
                                    },
                                    IdCliente = Convert.ToInt32(leituraDb["idCliente"]),
                                    IdFilmeNavigation = new Filme()
                                    {
                                        IdFilme = Convert.ToInt32(leituraDb["idFilme"]),
                                        NomeFilme = leituraDb["nomeFilme"].ToString()
                                    },
                                    IdFilme = Convert.ToInt32(leituraDb["idFilme"]),
                                    DtEntrega = Convert.ToDateTime(leituraDb["dtEntrega"])
                                };
                                return locacao;
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
        /// Obtém todas as locacoes
        /// </summary>
        /// <returns>List de objetos do tipo Locacao</returns>
        public List<Locacao> GetLocacoes()
        {
            try
            {
                var stringBuilder = new StringBuilder();
                var query = stringBuilder.Append(@"SELECT tl.idLocacao, tf.idFilme, tf.nomeFilme, tf.dtLancamento, tc.idCliente, tc.nomeCliente, tl.dtEntrega 
                                                 FROM tb_Locacao tl 
                                                 INNER JOIN tb_Cliente tc ON tl.idCliente = tc.idCliente 
                                                 INNER JOIN tb_Filme tf ON tl.idFilme = tf.idFilme;").ToString();

                List<Locacao> listaLocacoes = new List<Locacao>();

                using (SqlConnection conexaoDb = new SqlConnection(conexao.StringConexao))
                {
                    conexaoDb.Open();

                    using (SqlCommand comandoDb = new SqlCommand(query, conexaoDb))
                    {
                        SqlDataReader leituraDb = comandoDb.ExecuteReader();
                        if (leituraDb.HasRows)
                        {
                            while (leituraDb.Read())
                            {
                                Locacao locacao = new Locacao()
                                {
                                    IdLocacao = Convert.ToInt32(leituraDb["idLocacao"]),
                                    IdClienteNavigation = new Cliente()
                                    {
                                        IdCliente = Convert.ToInt32(leituraDb["idCliente"]),
                                        NomeCliente = leituraDb["nomeCliente"].ToString()
                                    },
                                    IdFilmeNavigation = new Filme()
                                    {
                                        IdFilme = Convert.ToInt32(leituraDb["idFilme"]),
                                        NomeFilme = leituraDb["nomeFilme"].ToString(),
                                        DtLancamento = Convert.ToDateTime(leituraDb["dtLancamento"])
                                    },
                                    DtEntrega = Convert.ToDateTime(leituraDb["dtEntrega"])
                                };
                                listaLocacoes.Add(locacao);
                            }
                        }
                    }
                }
                return listaLocacoes;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Registra uma nova locacao
        /// </summary>
        /// <param name="locacao">Objeto do tipo Locacao necessário para cadastro</param>
        /// <returns>Menssagem de status do tipo String</returns>
        public string Post(Locacao locacao)
        {
            string fRetorno = String.Empty;
            try
            {
                ClienteRepository clienteRepository = new ClienteRepository();
                FilmeRepository filmeRepository = new FilmeRepository();
                
                if (clienteRepository.GetClientePorId(locacao.IdCliente) != null)
                {
                    Filme filme = filmeRepository.GetFilmePorId(locacao.IdFilme);

                    //Verificando disponibilidade do filme
                    if (filme != null && filme.Disponibilidade)
                    {
                        var stringBuilder = new StringBuilder();
                        var query = stringBuilder.Append(@"INSERT INTO tb_Locacao (idCliente, idFilme, dtEntrega) VALUES (@idCliente, @idFilme, @dtEntrega);").ToString();

                        using (SqlConnection conexaoDb = new SqlConnection(conexao.StringConexao))
                        {
                            conexaoDb.Open();
                            SqlCommand comandoDb = new SqlCommand(query, conexaoDb);
                            comandoDb.Parameters.AddWithValue("@idCliente", locacao.IdCliente);
                            comandoDb.Parameters.AddWithValue("@idFilme", locacao.IdFilme);
                            comandoDb.Parameters.AddWithValue("@dtEntrega", locacao.DtEntrega);
                            comandoDb.ExecuteNonQuery();
                        }
                        // Atualizando a disponibilidade do filme
                        filme.Disponibilidade = false;
                        filmeRepository.Put(filme);

                        fRetorno = "Locação realizada com êxito.";
                    }
                    else fRetorno = "O Filme já está alugado.";
                }
            }
            catch (Exception exception) 
            {
                throw exception;
            }

            return fRetorno;
        }

        /// <summary>
        /// Atualiza uma locacao
        /// </summary>
        /// <param name="idLocacao">Id do filme a ser locado de tipo int</param>
        /// <returns>Menssagem de status do tipo string</returns>
        public string PutRealizarEntrega(int idLocacao)
        {
            string fRetorno = "Filme entregue com sucesso!";
            try
            {
                var stringBuilder = new StringBuilder();
                var query = stringBuilder.Append(@"UPDATE tb_Locacao SET dtEntrega = @dtEntrega WHERE idLocacao = @idLocacao;").ToString();

                // Buscando a locacao através do Id
                Locacao locacao = new Locacao();
                locacao = GetLocacaoPorId(idLocacao);

                if (locacao != null)
                {
                    // Verificando a data de entrega
                    if (locacao.DtEntrega < DateTime.Now) fRetorno = "Filme com atraso na entrega!";

                    using (SqlConnection conexaoDb = new SqlConnection(conexao.StringConexao))
                    {
                        conexaoDb.Open();
                        SqlCommand comandoDb = new SqlCommand(query, conexaoDb);
                        comandoDb.Parameters.AddWithValue("@idLocacao", idLocacao);
                        comandoDb.Parameters.AddWithValue("@dtEntrega", DateTime.Now);
                        comandoDb.ExecuteNonQuery();
                    }

                    // Atualizando a disponibilidade do filme após a entrega
                    FilmeRepository filmeRepository = new FilmeRepository();
                    filmeRepository.PutDisponibilidade(locacao.IdFilmeNavigation.IdFilme, true);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return fRetorno;
        }
    }
}