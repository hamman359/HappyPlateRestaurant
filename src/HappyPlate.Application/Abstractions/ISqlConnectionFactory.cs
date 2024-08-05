using System.Data.SqlClient;

namespace HappyPlate.Application.Abstractions;

//TODO: Documentation Needed
public interface ISqlConnectionFactory
{
    SqlConnection CreateConnection();
}