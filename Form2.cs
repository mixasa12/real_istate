using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace WindowsFormsApp3
{
    public partial class Form2 : Form
    {
        Form1 f;
        private string conect;
        public Form2(string con)
        {
            conect = con;
            int i = 84;
            string username = "";
            while (con[i] != ';')
            {
                username += con[i];
                i++;
            }
            this.Text=username;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Standart_table table1 = new Standart_table();
            table1.names = new string[] { "Адрес", "Площадь (кв.м)", "Кадастровая цена", "Кадастровый номер" };
            table1.shirina = new int[] { 275, 100, 100, 100 };
            table1.sqlcon = conect;
            table1.sqlq = "SELECT* FROM uchastok";
            table1.tip = "standart";
            table1.table = "uchastok";
            table1.foreigns = new List<foreign>();
            table1.combos = new List<combo>();
            table1.unique = 4;
            String[] tipd = new String[] { "string", "double", "double", "int" };
            Console.WriteLine(tipd[0]);
            Form1 f = new Form1(table1, tipd);
            f.Text = "Участки";
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Standart_table table2 = new Standart_table();
            table2.names = new string[] { "id участка", "Количество этажей", "Номер помещения", "Площадь", "Цена", "Тип", "Кадастровый номер" };
            table2.shirina = new int[] { 75, 75, 75, 100, 100, 100, 100 };
            table2.sqlcon = conect;
            table2.sqlq = "SELECT * FROM nedvishimost";
            table2.tip = "standart";
            table2.table = "nedvishimost";
            table2.foreigns = new List<foreign>();
            table2.foreigns.Add(new foreign(0, "select id,address from uchastok"));
            table2.combos = new List<combo>();
            List<string> str = new List<string> { "Жилая", "Полужилая", "Нежилая", "Особые условия" };
            table2.combos.Add(new combo(5, str));
            table2.unique = 7;
            String[] tip = new String[] { "int", "int", "int", "double", "double", "string", "int" };
            Form1 f = new Form1(table2, tip);
            f.Text = "Недвижимость";
            f.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Standart_table table3 = new Standart_table();
            table3.names = new string[] { "id участка", "id недвижимости", "id фирмы", "Тип", "Цена", "Прибыль" };
            table3.shirina = new int[] { 75, 75, 75, 100, 100, 100, };
            table3.sqlcon = conect;
            table3.sqlq = "SELECT * FROM dogovor";
            table3.tip = "standart";
            table3.table = "dogovor";
            table3.foreigns = new List<foreign>();
            table3.foreigns.Add(new foreign(0, "select id,address from uchastok"));
            table3.foreigns.Add(new foreign(1, "select n.id,u.address,n.flat from uchastok u join nedvishimost n ON u.id=n.uchastok_id"));
            table3.foreigns.Add(new foreign(2, "select id,name from firma"));
            table3.combos = new List<combo>();
            List<string> str = new List<string> { "Покупка", "Аренда" };
            table3.combos.Add(new combo(3, str));
            String[] tip = new String[] { "int", "int", "int", "string", "double", "double" };
            Form1 f = new Form1(table3, tip);
            f.Text = "Договора";
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Standart_table table1 = new Standart_table();
            table1.names = new string[] { "Название", "Директор", "Телефон", "ИНН" };
            table1.shirina = new int[] { 150, 150, 100, 100 };
            table1.sqlcon = conect;
            table1.sqlq = "SELECT * FROM firma";
            table1.tip = "standart";
            table1.table = "firma";
            table1.foreigns = new List<foreign>();
            table1.combos = new List<combo>();
            table1.unique = 4;
            String[] tip = new String[] { "string", "string", "string", "string" };
            Form1 f = new Form1(table1, tip);
            //f.Size = new Size(700, 1000);
            //f.Height = 1000;
            f.Text = "Фирмы";
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Form4 f = new Form4(conect);
            int i = 84;
            string username = "";
            while (conect[i] != ';')
            {
                username += conect[i];
                i++;
            }
            f.Text = username;
            f.Show();
            this.Close();
        }
    
        private void button6_Click(object sender, EventArgs e)
        {
            select_usl f = new select_usl(conect);
            int i = 84;
            string username = "";
            while (conect[i] != ';')
            {
                username += conect[i];
                i++;
            }
            f.Text = username;
            f.Show();
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            auto f = new auto();
            f.Show();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Standart_table table2 = new Standart_table();
            table2.names = new string[] { "id договора", "Прибыль", "Тип договора", "Тип операции", "Время создания" };
            table2.shirina = new int[] { 75, 100, 100, 100, 100};
            table2.sqlcon = conect;
            table2.sqlq = "SELECT * FROM HISTORY";
            table2.tip = "hystory";
            table2.table = "HISTORY";
            table2.foreigns = new List<foreign>();
            table2.combos = new List<combo>();
            String[] tip = new String[] {  };
            Form1 f = new Form1(table2, tip);
            f.Text = "История договоров";
            f.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Standart_table table2 = new Standart_table();
            table2.names = new string[] { "Адрес", "Номер помещения","Площадь", "Цена", "Тип договора"};
            table2.shirina = new int[] { 250, 75, 100, 100, 100, 100 };
            table2.sqlcon = conect;
            table2.sqlq = "SELECT * FROM ned_on_uch";
            table2.tip = "view";
            table2.table = "view";
            table2.combos = new List<combo>();
            List<string> str = new List<string> { "Аренда", "Покупка"};
            table2.combos.Add(new combo(4, str));
            table2.foreigns = new List<foreign>();
            String[] tip = new String[] { "string", "int","double", "double", "string"};
            Form1 f = new Form1(table2, tip);
            f.Text = "Информация о недвижимости";
            f.Show();
        }
    }
}
