using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Wipro.Locadora.Models
{
    public partial class Locacao
    {
        public int IdLocacao { get; set; }

        [Required(ErrorMessage = "Informe o id do filme")]        
        public int IdFilme { get; set; }

        [Required(ErrorMessage = "Informe o id do cliente")]        
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "Informe a data de entrega")]
        [DataType(DataType.Date, ErrorMessage = "Informe uma data válida")]
        public DateTime DtEntrega { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Filme IdFilmeNavigation { get; set; }
    }
}
