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
    public partial class Form4 : Form
    {
        string conect;
        public Form4(string con)
        {
            conect=con;     
            InitializeComponent();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Standart_table table1 = new Standart_table();
            table1.sqlcon = conect;
            table1.sqlq = "SELECT * FROM case_func2()";
            table1.names = new string[] { "Тип договора", "Цена", "Адрес", "Квартира", "Название фирмы", "Наценка", "Тип фирмы" };
            Form5 form = new Form5(table1);
            form.Text = "Договорная информация по недвижимости";
            form.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Standart_table table1 = new Standart_table();
            table1.sqlcon = conect;
            table1.sqlq = "SELECT * FROM testfunction3()";
            table1.names = new string[] { "Тип договора", "Цена", "Адрес", "Название фирмы", "Наценка", "Тип фирмы" };
            Form5 form = new Form5(table1);
            form.Text = "Договорная информация по участкам";
            form.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Standart_table table1 = new Standart_table();
            table1.sqlcon = conect;
            table1.sqlq = @"SELECT f.name,(SELECT sum(d.cost) FROM dogovor d WHERE d.firma_id = f.id AND
d.type = 'Аренда')AS "+'"'+"Сумма аренды"+'"'+@",(SELECT count(d.cost) FROM dogovor d WHERE
d.firma_id = f.id AND d.type = 'Аренда')AS "+'"'+"Кол-во аренды"+'"' +@",(SELECT sum(d.cost) FROM dogovor d
    WHERE d.firma_id = f.id AND d.type = 'Покупка')AS "+'"'+"Сумма покупок"+'"'+@",(SELECT count(d.cost) FROM
        dogovor d WHERE d.firma_id = f.id AND d.type = 'Покупка')AS "+'"'+"Кол-во покупок"+'"'+@" ,doga_avg AS
"+'"'+"Общее кол-во"+'"'+@"FROM firma f,(SELECT dd.firma_id AS fid, COUNT(*) AS doga_avg FROM dogovor
dd GROUP BY dd.firma_id )as avg_dog WHERE avg_dog.fid = f.id AND(0 < (SELECT COUNT(*)
  FROM dogovor d WHERE d.firma_id = f.id))";
            table1.names = new string[] { "Название фирмы", "Сумма аренды", "Кол-во аренды", "Сумма покупок", "Кол-во покупок", "Общее кол-во" };
            Form5 form = new Form5(table1);
            form.Text = "Информация о деятельности фирм";
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Standart_table table1 = new Standart_table();
            table1.sqlcon = conect;
            table1.sqlq = "SELECT uchastok_id,floor,flat,square,cost,type,number FROM nedvishimost pp WHERE pp.square = (SELECT max(ppm.square) FROM nedvishimost ppm  WHERE ppm.uchastok_id = pp.uchastok_id)";
            table1.names = new string[] { "id Участка", "Кол-во этажей","Номер помещения" ,"Площадь", "Кадастровая стоимость", "Тип", "Кадастровый номер" };
            Form5 form = new Form5(table1);
            form.Text = "Максимальная по площади недвижимость для участков";
            form.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string sqlcon = conect;
            string sqlq = "EXECUTE trig";
            using (SqlConnection connection = new SqlConnection(sqlcon))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlq;
                command.Connection = connection;
                try {  command.ExecuteNonQuery();}
                catch { MessageBox.Show("У вас недостаточно прав для этой операции"); }
                connection.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Standart_table table1 = new Standart_table();
            table1.sqlcon = conect;
            table1.sqlq = "SELECT firma_id,uchastok_id,nedvishimost_id,type,cost,pribil FROM dogovor pp WHERE pp.cost = (SELECT min(ppm.cost) FROM dogovor ppm WHERE ppm.firma_id = pp.firma_id AND ppm.type = 'Аренда')AND pp.type = 'Аренда'";
            table1.names = new string[] { "id фирмы", "id Недвижимости", "id Участка", "Тип договора", "Стоимость","Прибыль"};
            Form5 form = new Form5(table1);
            form.Text = "Минимальная по стоимости аренда для каждой фирмы";
            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string con = conect;
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

        private void button8_Click(object sender, EventArgs e)
        {
            Standart_table table1 = new Standart_table();
            table1.sqlcon = conect;
            table1.sqlq = "SELECT * FROM p92()";
            table1.names = new string[] { "Тип сделки", "Прибыль" };
            Form5 form = new Form5(table1);
            form.Text = "Самые дорогие договора за историю приложелния";
            form.Show();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
