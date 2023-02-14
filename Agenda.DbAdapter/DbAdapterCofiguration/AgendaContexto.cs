using System.Data;

namespace Agenda.DbAdapter.DbAdapterCofiguration
{
    public class AgendaContexto
    {
        public IDbConnection Connection { get; set; }
        public AgendaContexto(IDbConnection dbConnection)
        {
            Connection = dbConnection;

        }
    }
}
