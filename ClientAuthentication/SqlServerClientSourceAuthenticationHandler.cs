using Microsoft.Data.SqlClient;

namespace ClientAuthentication;

public class SqlServerClientSourceAuthenticationHandler : IClientSourceAuthenticationHandler, IDisposable
{
    private readonly string _connectionString;
    private SqlConnection connection;
    private bool disposedValue;
    public SqlServerClientSourceAuthenticationHandler(string connectionString)
    {
        _connectionString = connectionString;
        
        connection = new SqlConnection(_connectionString);
    }

    public bool Validate(string clientSource)
    {
        if (connection.State == System.Data.ConnectionState.Closed)
        {
            connection.Open();
        }

        var query =
            "SELECT TOP 1 1 FROM ClientSources WHERE ClientId = @ClientSource AND GETDATE() >= ValidFrom AND GETDATE() <= ValidTo AND IsEnable = 1";
        using (var command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@ClientSource", clientSource);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return true;
            }
        }
        return false;
    }


    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }

                connection.Dispose();
            }
            disposedValue = true;
        }
    }
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
