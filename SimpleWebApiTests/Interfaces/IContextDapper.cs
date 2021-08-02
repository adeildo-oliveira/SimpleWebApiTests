using System.Data;

namespace SimpleWebApiTests.Interfaces
{
    public interface IContextDapper
    {
        IDbConnection Connection();
    }
}
