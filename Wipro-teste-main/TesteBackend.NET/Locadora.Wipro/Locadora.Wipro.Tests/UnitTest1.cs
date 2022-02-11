using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wipro.Locadora.Models;
using Wipro.Locadora.Repositories;
using Wipro.Locadora.Services;

namespace Wipro.Locadora.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void IniciarTestes()
        {
            TestConexaoDb();
        }
        [TestMethod]
        public void TestConexaoDb()
        {
            // Teste de conex�o com o banco de Dados
            Conexao conexao = new Conexao();
            string query = "USE db_locadora";

            using (SqlConnection con = new SqlConnection(conexao.StringConexao))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.ExecuteNonQuery();
                    Assert.AreEqual("db_locadora", con.Database);
                }
            }
        }
        [TestMethod]
        public void TestPostCliente()
        {
            LocacaoRepository locacaoRepository = new LocacaoRepository();
            Locacao locacao = new Locacao()
            {
                IdFilme = 3,
                IdCliente = 1,
                DtEntrega = DateTime.Now
            };
            string mRetorno = locacaoRepository.Post(locacao);

            Assert.IsTrue(!String.IsNullOrEmpty(mRetorno));
        }

        [TestCleanup]
        public void FinalizarTestes()
        {

        }
    }
}