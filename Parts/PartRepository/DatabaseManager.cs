using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartDB;

namespace PartRepository
{
    public class DatabaseManager
    {
        private static readonly InventoryEntities entities;

        // Initialize and open the database connection
        static DatabaseManager()
        {
            entities = new InventoryEntities();
            entities.Database.Connection.Open();
        }

        // Provide an accessor to the database
        public static InventoryEntities Instance
        {
            get
            {
                return entities;
            }
        }
    }
}
