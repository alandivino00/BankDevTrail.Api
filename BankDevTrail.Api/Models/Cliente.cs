namespace BankDevTrail.Api.Models
{
    public class Cliente
    {
        public Cliente(string nome, string cpf, DateTime? dataNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;

        public string Cpf { get; set; }

        public DateTime? DataNascimento { get; set; }

        public List<Conta> Contas { get; } = new();
    }
}
