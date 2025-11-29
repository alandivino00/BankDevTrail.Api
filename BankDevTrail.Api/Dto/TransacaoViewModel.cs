using BankDevTrail.Api.Models;

namespace BankDevTrail.Api.Dto
{
    public class TransacaoViewModel
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataHora { get; set; }
        public TipoTransacao Tipo { get; set; }
        public Guid? ContaOrigemId { get; set; }
        public Guid? ContaDestinoId { get; set; }
    }
}
