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
    public partial class select_usl : Form
    { private string con;
        public select_usl(string cone)
        {
            con = cone;
            InitializeComponent();
        }

        private void select_usl_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT id,name FROM firma",connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader[0].ToString()+"-"+ reader[1].ToString());
                    comboBox2.Items.Add(reader[0].ToString() + "-" + reader[1].ToString());
                }
                connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ussl table1 = new ussl();
            table1.names = new string[] { "Адрес", "Кол-во этажей", "Номер помещения", "Площадь", "Цена", "Мин. цена недвижимости", "Средняя цена недвижимости", "Макс. цена недвижимости" };
            table1.types = new string[] {"f","id недвижимости"};
            table1.values = new string[,] { { "Select id,cost from nedvishimost" } };            
            table1.sqlq = "SELECT (SELECT address FROM uchastok u WHERE u.id=n.uchastok_id)AS address,floor,flat,square,cost,pkdk.ned_min,ROUND(pkdk.ned_avg, 2)AS ned_avg, pkdk.ned_max FROM nedvishimost n,(SELECT min(x.cost)AS ned_min, ROUND(avg(x.cost), 2)AS ned_avg, max(x.cost)AS ned_max FROM nedvishimost x)as pkdk WHERE cost > ALL(SELECT cost FROM nedvishimost WHERE id = )";
            table1.connect = con;
            table1.fun = new int[] { table1.sqlq.Length - 2 };
            Console.WriteLine( table1.sqlq);
            select_uslt f = new select_uslt(table1);
            f.Text = "Статистика и информация о недвижимости дороже";
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ussl table1 = new ussl();
            table1.names = new string[] { "Адрес", "Кол-во недвижимости"};
            table1.types = new string[] { "Введите минимальную границу" };
            table1.values = new string[,] { { "1" } };
            table1.sqlq = "SELECT * FROM p35()";
            Console.WriteLine(table1.sqlq);
            table1.connect = con;
            table1.fun = new int[] { table1.sqlq.Length - 1 };
            Console.WriteLine(table1.sqlq);
            select_uslt f = new select_uslt(table1);
            f.Text = "Количество недвижимости на участке";
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string conect = con;
            int i = 84;
            string username = "";
            while (con[i] != ';')
            {
                username += con[i];
                i++;
            }
            Form2 f = new Form2(con);
            f.Text = username;
            f.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ussl table1 = new ussl();
            table1.names = new string[] { "Название", "Директор","Телефон","ИНН" };
            table1.types = new string[] { "Введите минимальную границу" };
            table1.values = new string[,] { { "1" } };
            table1.sqlq = "SELECT * FROM p36()";
            Console.WriteLine(table1.sqlq);
            table1.connect = con;
            table1.fun = new int[] { table1.sqlq.Length - 1 };
            Console.WriteLine(table1.sqlq);
            select_uslt f = new select_uslt(table1);
            f.Text = "Список фирм не покупающих дешевле";
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("EXECUTE try_del '"+textBox1.Text+"'", connection);
                MessageBox.Show(command.CommandText);
                try { command.ExecuteReader(); }
                catch(Exception ex) { MessageBox.Show(ex.ToString()) ; }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT dbo.get_address(" + textBox2.Text+")", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) 
                {
                   MessageBox.Show(reader[0].ToString()); 
                }    
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if((comboBox1.SelectedIndex!=-1)&(comboBox2.SelectedIndex!=-1))
            {
                string str1=comboBox1.SelectedItem.ToString();
                string str2=comboBox2.SelectedItem.ToString();
                str1=str1.Substring(0,str1.IndexOf('-'));
                str2=str2.Substring(0, str2.IndexOf('-'));
                Console.WriteLine(str1 + " " + str2);
                Standart_table table1 = new Standart_table();
                table1.sqlcon = con;
                table1.sqlq = "SELECT d.firma_id,d.nedvishimost_id,d.uchastok_id,d.type,d.cost,d.pribil FROM dogovor d WHERE d.firma_id=" + str1 + " AND d.cost>ALL(SELECT cost FROM dogovor dd WHERE dd.firma_id="+str2+")";
                table1.names = new string[] { "id фирмы", "id Недвижимости", "id Участка", "Тип договора", "Стоимость", "Прибыль" };
                Form5 form = new Form5(table1);
                form.Text = "Более дорогие договора, чем у другой фирмы";
                form.Show();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
