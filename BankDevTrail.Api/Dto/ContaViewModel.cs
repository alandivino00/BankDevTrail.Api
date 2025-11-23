using System;

namespace BankDevTrail.Api.Dto
{
    public class ContaViewModel
    {
        public string Numero { get; set; } = string.Empty;

        public string Titular { get; set; } = string.Empty;

        public decimal Saldo { get; set; }

        public Guid? ClienteId { get; set; }
    }
}
