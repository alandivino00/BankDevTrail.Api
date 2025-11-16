namespace BankDevTrail.Api.Models
{
    public class Conta
    {
        public string Numero { get; set; }
        public decimal Saldo { get; set; }
        
        // Foreign key and navigation
        public Guid? ClienteId { get; set; }

        public Status Status { get; set; }

        public Tipo Tipo { get; set; }

        public List<Transacao> Transacoes { get; } = new();

    }
}
