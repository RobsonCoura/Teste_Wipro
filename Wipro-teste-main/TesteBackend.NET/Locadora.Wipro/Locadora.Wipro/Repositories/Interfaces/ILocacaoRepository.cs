using Wipro.Locadora.Models;
using System.Collections.Generic;

namespace Wipro.Locadora.Repositories.Interfaces
{
    interface ILocacaoRepository
    {
        string Post(Locacao locacao);
        string PutRealizarEntrega(int idLocacao);
        List<Locacao> GetLocacoes();
        Locacao GetLocacaoPorId(int idLocacao);
    }
}
