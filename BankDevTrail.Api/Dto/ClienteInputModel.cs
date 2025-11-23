using System;
using System.ComponentModel.DataAnnotations;

namespace BankDevTrail.Api.Dto
{
    public class ClienteInputModel
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(14, MinimumLength = 11)]
        public string Cpf { get; set; } = string.Empty;

        public DateTime? DataNascimento { get; set; }
    }
}
