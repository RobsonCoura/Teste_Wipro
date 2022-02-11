﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Wipro.Locadora.Models
{
    public partial class Cliente
    {
        [JsonIgnore]
        public virtual ICollection<Locacao> TableLocacao { get; set; }

        public Cliente()
        {
            TableLocacao = new HashSet<Locacao>();
        }

        public int IdCliente { get; set; }
        [Required(ErrorMessage = "Informe o nome do Cliente")]
        public string NomeCliente { get; set; }
        [Required(ErrorMessage = "Informe o CPF do Cliente")]
        [StringLength(14, ErrorMessage = "O campo deve ter 14 caracteres")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Informe a data de Nascimento")]
        [DataType(DataType.Date, ErrorMessage = "Informe uma data válida")]
        public DateTime? DtNascimento { get; set; }
    }
}
