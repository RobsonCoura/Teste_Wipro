using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Wipro.Locadora.Models;
using Wipro.Locadora.Repositories.Interfaces;
using Wipro.Locadora.Services;

namespace Locadora.Wipro.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        readonly Conexao conexao = new Conexao();

        /// <summary>
        /// Obtém um Cliente pelo seu Id
        /// </summary>
        /// <param name="idCliente">Id de tipo Int que representa o cliente</param>
        /// <returns>Um objeto do tipo Cliente</returns>
        public Cliente GetClientePorId(int idCliente)
        {
            try
            {
                var stringBuilder = new StringBuilder();
                var query = stringBuilder.Append("SELECT idCliente, nomeCliente, CPF, dtNascimento FROM tb_Cliente WHERE idCliente = @idCliente").ToString();
                                
                using (SqlConnection conexao = new SqlConnection(this.conexao.StringConexao))
                {
                    conexao.Open();

                    using (SqlCommand comando = new SqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@idCliente", idCliente);

                        SqlDataReader leituraDb = comando.ExecuteReader();
                        if (leituraDb.HasRows)
                        {
                            while (leituraDb.Read())
                            {
                                Cliente cliente = new Cliente()
                                {
                                    IdCliente = Convert.ToInt32(leituraDb["idCliente"]),
                                    NomeCliente = leituraDb["nomeCliente"].ToString(),
                                    Cpf = leituraDb["CPF"].ToString(),
                                    DtNascimento = Convert.ToDateTime(leituraDb["dtNascimento"])                                    
                                };
                                return cliente;
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
        /// Obtéum uma lista de todos os clientes
        /// </summary>
        /// <returns>List de objetos do tipo Cliente</returns>
        public List<Cliente> GetClientes()
        {
            try
            {
                var stringBuilder = new StringBuilder();
                var query = stringBuilder.Append(@"SELECT idCliente, nomeCliente, CPF, dtNascimento FROM tb_Cliente;").ToString();

                List<Cliente> listaClientes = new List<Cliente>();

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
                                Cliente cliente = new Cliente()
                                {
                                    IdCliente= Convert.ToInt32(leituraDb["idCliente"]),
                                    NomeCliente = leituraDb["nomeCliente"].ToString(),
                                    Cpf = leituraDb["CPF"].ToString(),
                                    DtNascimento= Convert.ToDateTime(leituraDb["dtNascimento"])                                    
                                };
                                listaClientes.Add(cliente);
                            }
                        }
                    }
                }
                return listaClientes;
            }
            catch (Exception exception) 
            { 
                throw exception; 
            }
        }

        /// <summary>
        /// Registra um cliente
        /// </summary>
        /// <param name="cliente">Objeto do tipo Cliente</param>
        public void Post(Cliente cliente)
        {
            try
            {
                var stringBuilder = new StringBuilder();
                string query = @"INSERT INTO tb_Cliente (nomeCliente, CPF, dtNascimento) VALUES (@nomeCliente, @CPF, @dtNascimento);";
                
                using (SqlConnection conexaoDb = new SqlConnection(conexao.StringConexao))
                {
                    conexaoDb.Open();
                    SqlCommand comando = new SqlCommand(query, conexaoDb);
                    comando.Parameters.AddWithValue("@nomeCliente", cliente.NomeCliente);
                    comando.Parameters.AddWithValue("@CPF", cliente.Cpf);
                    comando.Parameters.AddWithValue("@dtNascimento", cliente.DtNascimento);                    
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception exception) 
            { 
                throw exception; 
            }
        }
    }
}