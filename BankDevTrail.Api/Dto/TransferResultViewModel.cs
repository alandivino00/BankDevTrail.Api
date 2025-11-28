namespace BankDevTrail.Api.Dto
{
    public class TransferResultViewModel
    {
        public string NumeroOrigem { get; set; } = string.Empty;
        public string TitularOrigem { get; set; } = string.Empty;
        public decimal SaldoOrigem { get; set; }

        public string NumeroDestino { get; set; } = string.Empty;
        public string TitularDestino { get; set; } = string.Empty;
        public decimal SaldoDestino { get; set; }
    }
}
