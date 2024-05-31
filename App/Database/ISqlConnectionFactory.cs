using System.Data;

namespace App.Database
{
    public interface ISqlConnectionFactory
    {
        IDbConnection Create(); 
    }
}
