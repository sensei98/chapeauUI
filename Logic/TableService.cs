using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace Logic
{
    public class TableService
    {
        private TableDAO tableDAO = new TableDAO();
        public List<Table> GetAllTables()
        {
            try
            {
                return tableDAO.GetAllTables();
            }
            catch (Exception)
            {
                return new List<Table>();
            }

        }
        public int ChangeTableStatus(Table table, TableState state)
        {
            try 
            {
                 return tableDAO.ChangeTableStatus(table, state);
            }
            catch (Exception) 
            {
                return -1;
            }
        }
    }
}
