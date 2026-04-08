using System.Data;

namespace Assessment.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
