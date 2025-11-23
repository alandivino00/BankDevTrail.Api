using System;
using System.ComponentModel.DataAnnotations;
using BankDevTrail.Api.Models;

namespace BankDevTrail.Api.Dto
{
    public class ContaInputModel
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Numero { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Saldo { get; set; }

        [Required]
        public Guid ClienteId { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public Tipo Tipo { get; set; }
    }
}
