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
using System.Reflection;
namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private string[] val;
        private List<int> id;
        private string[] tip;
        private List<int> uniqs;
        public List<int> frg;
        public List<int> chastfrg;
        private Standart_table table;
        public Form1(Standart_table table1,string[] tipe)
        {
            table = table1;
            tip = tipe;
            int x = 0;
            if (table.names.Length > 7)
            {
                x = (table.names.Length - 7) * 50;
                int h = 600;
                int w = 1005;
                int h1 = 400;
                int w1 = 960;
                this.Size = new System.Drawing.Size(w+x, h + x);
                dataGridView1.Size = new System.Drawing.Size(w1+x, h1 + x);
            }
            Button btn = new Button();
            btn.Location = new System.Drawing.Point(100, 420 + x);
            btn.Size = new System.Drawing.Size(150, 50);
            btn.Text = "Добавить";
            btn.Name = "btn";
            btn.BackColor = Color.White;
            btn.Click += new EventHandler(btn_Click);
            this.Controls.Add(btn);
            Button btn1 = new Button();
            btn1.Location = new System.Drawing.Point(300, 420 + x);
            btn1.Size = new System.Drawing.Size(150, 50);
            btn1.Text = "Изменить";
            btn1.Name = "btn";
            btn1.BackColor = Color.White;
            btn1.Click += new EventHandler(btn_Click1);
            this.Controls.Add(btn1);
            Button btn2 = new Button();
            btn2.Location = new System.Drawing.Point(500, 420 + x);
            btn2.Size = new System.Drawing.Size(150, 50);
            btn2.Text = "Удалить";
            btn2.Name = "btn";
            btn2.BackColor = Color.White;
            btn2.Click += new EventHandler(btn_Click2);
            this.Controls.Add(btn2);
            if (table.tip == "hystory") { btn2.Hide();btn1.Hide();btn.Hide();}
            if (table.tip == "view") { btn.Hide(); btn2.Hide(); }
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int size=0;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToOrderColumns = false;
            DataGridViewTextBoxColumn[] dgvc;
            dgvc = new DataGridViewTextBoxColumn[table.names.Length];
            for (int i = 0; i < table.names.Length; i++)
            {
                dgvc[i] = new DataGridViewTextBoxColumn();
                dgvc[i].HeaderText = table.names[i];
                dgvc[i].Name = "col" + i;
                dgvc[i].Width = table.shirina[i];
                size += table.shirina[i];
                Console.WriteLine(table.shirina[i]);
                dgvc[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns.Add(dgvc[i]);
            }
            //dataGridView1.Size = new Size(size+50, 300);
            using (SqlConnection connection = new SqlConnection(table.sqlcon))
            {
                connection.Open();
                uniqs = new List<int>();
                SqlCommand command = new SqlCommand();
                command.CommandText = table.sqlq;
                command.Connection = connection;
                try { 
                SqlDataReader reader = command.ExecuteReader();
                int count = reader.FieldCount;
                string[] s; s = new string[reader.FieldCount - 1];
                id = new List<int>();              
                int j = 1;
                while (reader.Read())
                {
                    id.Add(Convert.ToInt32(reader[0]));
                    for (int i = 1; i < count; i++)
                    {
                        s[i - 1] = String.Format("{0}", reader[i]);
                    }
                    dataGridView1.Rows.Add(s);
                    j++;
                }
                }
                catch {MessageBox.Show("У вас нет доступа на эту таблицу");this.Close(); }
                List<string> value = new List<string>();
                frg = new List<int>();
                string curval;
                for (int i = 0; i < table.foreigns.Count; i++)
                {
                curval = "";
                SqlCommand fcom = new SqlCommand(table.foreigns[i].fsqlq, connection);
                    Console.WriteLine(table.foreigns[i].fsqlq);
                SqlDataReader fk = fcom.ExecuteReader();
                while (fk.Read()) 
                {
                    curval = fk[0].ToString()+"-";
                    for(int k = 1; k < fk.FieldCount; k++)
                    {
                       curval+=fk[k].ToString();
                       if (i != fk.FieldCount - 1) { curval += ";"; }
                    }              
                value.Add(curval);
                }                
                table.combos.Add(new combo(i, value));
                frg.Add(table.foreigns[i].id);
                value = new List<string>();                            
                }
            }
            foreach(combo x in table.combos)
            {
                Console.WriteLine(x.id);
                foreach(string str in x.values) 
                {
                    Console.WriteLine(str);                
                }
            }
        }
        private void btn_Click(object sender, EventArgs e)
        {
            string commandq;
            val = null;
            bool itk = false;
            if (table.table == "dogovor") { itk = true; }
            form3 fm2 = new form3(table.names, table.combos, tip,val,frg,itk);
            fm2.Size = new System.Drawing.Size(275, 75 * table.names.Length + 40);
            fm2.Text = "Добавление";
            fm2.ShowDialog();
            if (fm2.DialogResult == DialogResult.OK)
            {
                commandq = "EXECUTE insert_" + table.table + " " + fm2.result;
                if (itk) { commandq = commandq.Substring(0, commandq.Length - 1); }
                using (SqlConnection connection = new SqlConnection(table.sqlcon))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = commandq;
                    Console.WriteLine(commandq);
                    command.Connection = connection;
                    try { command.ExecuteNonQuery();
                        Form1 f = new Form1(table,tip); 
                        f.Text = this.Text;
                        f.Show();
                       
                        this.Close(); }
                    catch (Exception ex)
                    { 
                        if (ex.Message.Contains("UNIQUE"))
                        { 
                            MessageBox.Show("Кадастровый номер не может повторяться"); 
                        }
                        else if(ex.Message.Contains("Запрещено разрешение")) { MessageBox.Show("У вас нет прав на эту операцию"); }
                        else { MessageBox.Show("Ошибка"); }
                    }
                }
            }
        }
        private void btn_Click1(object sender, EventArgs e)
        {
            string commandq="";
            val = new string[table.names.Length];
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int idr = dataGridView1.SelectedCells[0].RowIndex;
                for (int i = 0; i < table.names.Length; i++) 
                {
                    val[i] = dataGridView1.Rows[idr].Cells[i].Value.ToString();                                      
                }
                bool itk = false;
                if (table.table == "dogovor") { itk = true; }
                form3 fm2 = new form3(table.names, table.combos, tip, val,frg,itk);
                fm2.Text = "Изменить";
                fm2.Size = new System.Drawing.Size(275, 75 *table.names.Length +40);
                fm2.ShowDialog();
                if (fm2.DialogResult == DialogResult.OK)
                {
                    commandq = "EXECUTE update_" + table.table + " "+id[idr]+"," + fm2.result;
                    if (itk) { commandq = commandq.Substring(0, commandq.Length - 1);}
                    Console.WriteLine(commandq);
                    using (SqlConnection connection = new SqlConnection(table.sqlcon))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand();
                        command.CommandText = commandq;
                        command.Connection = connection;
                        try
                        {
                            command.ExecuteNonQuery();
                            Form1 f = new Form1(table, tip);
                            f.Text = this.Text;
                            f.Show();
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("UNIQUE"))
                            {
                                MessageBox.Show("Кадастровый номер не может повторяться");
                            }
                            else if (ex.Message.Contains("Запрещено разрешение")) { MessageBox.Show("У вас нет прав на эту операцию"); }
                            else { MessageBox.Show("Ошибка"); }
                        }
                    }
                }
            }
        }
        private void btn_Click2(object sender, EventArgs e) 
        {
            if (dataGridView1.SelectedCells.Count > 0) 
            {
                int idr = id[dataGridView1.SelectedCells[0].RowIndex];
                string comandq = "EXECUTE delete_"+table.table+" "+idr;
                using (SqlConnection connection = new SqlConnection(table.sqlcon))
                {
                    Console.WriteLine(comandq);
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = comandq;
                    command.Connection = connection;
                    try
                    {
                        command.ExecuteNonQuery();
                        Form1 f = new Form1(table, tip);
                        f.Text = this.Text;
                        f.Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Запрещено разрешение")) { MessageBox.Show("У вас нет прав на эту операцию"); }
                        else { MessageBox.Show("Ошибка"); }
                    }
                }
            }
            else { MessageBox.Show("Вы не выбрали строку");}
        }
    }
}

