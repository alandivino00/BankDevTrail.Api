namespace BankDevTrail.Api.Models
{
    public class Cliente
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;

        public string Cpf { get; set; }

        public DateTime? DataNascimento { get; set; }

        public List<Conta> Contas { get; } = new();
    }
}
