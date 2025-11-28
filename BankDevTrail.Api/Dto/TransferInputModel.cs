namespace BankDevTrail.Api.Dto
{
    public class TransferInputModel
    {
        public string NumeroDestino { get; set; } = string.Empty;
        public decimal Valor { get; set; }
    }
}
