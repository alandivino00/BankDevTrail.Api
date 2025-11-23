using System.ComponentModel.DataAnnotations.Schema;

namespace BankDevTrail.Api.Models
{
    public class Conta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Numero { get; set; }
        public decimal Saldo { get; set; }
        
        // Foreign key and navigation
        public Guid? ClienteId { get; set; }

        public Status Status { get; set; }

        public Tipo Tipo { get; set; }
        
        [NotMapped]
        public List<Transacao> Transacoes { get; } = new();

    }
}
