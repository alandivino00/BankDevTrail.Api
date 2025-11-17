namespace BankDevTrail.Api.Models
{
    public class Transacao
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Tipo Tipo { get; set; }

        public decimal Valor { get; set; }

        public DateTime? DataHora { get; set; }

        public Guid? ContaOrigemId { get; set; }

        public Guid? ContaDestinoId { get; set; }

    }
}
