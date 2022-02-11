using Wipro.Locadora.Models;
using System.Collections.Generic;

namespace Wipro.Locadora.Repositories.Interfaces
{
    interface IFilmeRepository
    {
        void Post(Filme filme);
        List<Filme> GetFilmes();
        Filme GetFilmePorId(int idFilme);
        void Put(Filme filme);

    }
}
