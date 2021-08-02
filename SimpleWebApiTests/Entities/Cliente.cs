using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SimpleWebApiTests.Entities
{
    public class Cliente
    {
        public long Id { get; set; }
        public string CpfCnpj { get; set; }
        public string Nome { get; set; }
        public string Mae { get; set; }
        public string Pai { get; set; }
    }

    public partial class ClienteAlterado : EqualityComparer<Cliente>
    {
        public override bool Equals(Cliente x, Cliente y)
        {
            if (x is null && y is null)
                return true;

            return (x.Nome.ToLowerInvariant() == y.Nome.ToLowerInvariant() &&
                x.Mae.ToLowerInvariant() == y.Mae.ToLowerInvariant() &&
                x.Pai.ToLowerInvariant() == y.Pai.ToLowerInvariant());
        }

        public override int GetHashCode([DisallowNull] Cliente obj) => $"{obj.Nome}{obj.Pai}{obj.Mae}".GetHashCode();
    }
}
