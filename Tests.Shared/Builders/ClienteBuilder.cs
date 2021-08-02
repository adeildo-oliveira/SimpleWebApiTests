using SimpleWebApiTests.Entities;

namespace Tests.Shared.Builders
{
    public class ClienteBuilder
    {
        private long _id = 1598;
        private string _cpfCnpj = "85963214568";
        private string _nome = "Nome do Cliente";
        private string _mae = "Nome da Mãe do Cliente";
        private string _pai = "Nome do Pai do Cliente";

        public ClienteBuilder ComId(long item)
        {
            _id = item;
            return this;
        }

        public ClienteBuilder ComCpfCnpj(string item)
        {
            _cpfCnpj = item;
            return this;
        }
        
        public ClienteBuilder ComNome(string item)
        {
            _nome = item;
            return this;
        }
        
        public ClienteBuilder ComMae(string item)
        {
            _mae = item;
            return this;
        }

        public ClienteBuilder ComPai(string item)
        {
            _pai = item;
            return this;
        }

        public Cliente Instanciar() => new()
        {
            Id = _id,
            CpfCnpj = _cpfCnpj,
            Nome = _nome,
            Mae = _mae,
            Pai = _pai
        };
    }
}
