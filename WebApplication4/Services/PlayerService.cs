using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Services
{
    public class PlayerService: IDisposable
    {
        public void Dispose()
        {
        }

        public DataTable GetPlayerTable() {
            var table = new DataTable();
            var playerNumber = new DataColumn("PlayerNumber", typeof(int));
            table.Columns.Add(playerNumber);
            table.PrimaryKey = new DataColumn[] { playerNumber };
            table.Columns.Add(new DataColumn("Name", typeof(string)));
            table.Columns.Add(new DataColumn("Email", typeof(string)));
            table.Columns.Add(new DataColumn("Phone", typeof(string)));
            table.Columns.Add(new DataColumn("Tier", typeof(string)));

            table.Rows.Add(123456789, "John Smith", "john@none.com", "+19095554444", "Classic");

            return table;
        }
    }
}
