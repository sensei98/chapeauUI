using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Table
    {
        public int TableID { get; set; }
        public int Capacity { get; set; }
        public TableState State { get; set; }

        public override string ToString()
        {
            return $"table number {TableID}";
        }

    }
    public enum TableState { Free = 1, OrderTaken, OrderServed, Reserved, unspecified }
}
