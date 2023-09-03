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
    public partial class select_uslt : Form
    { private SqlCommand fg;
        private ComboBox[] cb;
        private TextBox[] tb;
        private ussl table;
        public select_uslt(ussl table1)
        {   
            table = table1;
            tb = new TextBox[table.fun.Length];
            int x = 0;
            if (table.values[0, 0] == "1") { Label l = new Label(); l.Size = new Size(450, 26);
                l.Location = new Point(10, 10);
                l.Text = table.types[0];
                l.TextAlign = ContentAlignment.MiddleCenter;
                this.Controls.Add(l);
                x = 480;
            }
                tb[0] = new TextBox();
                tb[0].Size = new Size(150,26);
                tb[0].Location = new Point(10+x, 10);
                this.Controls.Add(tb[0]);
            Button but = new Button();
            but.Size = new Size(100,30);
            but.Location = new Point(200+x, 10);
            but.Text = "Искать";
            but.Click += new EventHandler(but_Click);
            this.Controls.Add(but);
            InitializeComponent();
        }
        private void but_Click(object sender, EventArgs e)
        {
            Console.WriteLine("dd");
            table.sqlq = table.sqlq.Insert(table.fun[0], tb[0].Text);
            using (SqlConnection connection = new SqlConnection(table.connect))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(table.sqlq,connection);
                SqlDataReader reader = command.ExecuteReader();
                int count = reader.FieldCount;
                string[] s; s = new string[reader.FieldCount ];
                while (reader.Read())
                {
                    for (int i = 0; i < count; i++)
                    { 
                        s[i] = String.Format("{0}", reader[i]);
                    }
                    dataGridView1.Rows.Add(s);
                }

            }
        }
        private void select_uslt_Load(object sender, EventArgs e)
        {
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
                //dgvc[i].Width = table.shirina[i];
               // size += table.shirina[i];
                //Console.WriteLine(table.shirina[i]);
                dgvc[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns.Add(dgvc[i]);
            }
        }
    }
}
