using System;
using System.Collections.Generic;

namespace BankDevTrail.Api.Dto
{
    public class ClienteViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Cpf { get; set; } = string.Empty;

        public DateTime? DataNascimento { get; set; }

        // Lista de contas do cliente (uso do ContaViewModel para manter separação de DTOs)
        public List<ContaViewModel> Contas { get; set; } = new();
    }
}
