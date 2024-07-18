using System.Data.SqlClient;

namespace HappyPlate.Application.Abstractions;

public interface ISqlConnectionFactory
{
    SqlConnection CreateConnection();
}