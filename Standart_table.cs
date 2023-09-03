using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public class combo
    {
        public int id;
        public List<string> values;
        public combo(int i, List<string> v)
        {
            id = i;
            values = v;
        }
    }
    public class constr
    {
        public int id;
        public string tip;
        public constr(int i, string c)
        {
            id = i;
            tip = c;
        }
    }
    public class foreign
    {
        public int id;
        public string fsqlq;
        public foreign(int i, string c)
        {
            id = i;
            fsqlq = c;
        }
    }
    public class Standart_table
    {   
        public int unique=-1;
        public string tip;
        public string[] names;
        public List<foreign> foreigns;
        public List<constr> constrains;
        public List<combo> combos;
        public string sqlcon;
        public string sqlq;
        public int[] shirina;
        public string table;
    }
    public class ussl
    {
        public string[] names;
        public string[] types;
        public int[] fun;
        public string[,] values;
        public string connect;
        public string sqlq;
    }
}

