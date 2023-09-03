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
    public partial class Form5 : Form
    {
        Standart_table table;
        public Form5(Standart_table tables)
        {
            table = tables;
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
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
                dgvc[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns.Add(dgvc[i]);
            }
            using (SqlConnection connection = new SqlConnection(table.sqlcon))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = table.sqlq;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                int count = reader.FieldCount;
                string[] s; s = new string[reader.FieldCount];
                while (reader.Read())
                {   
                    for (int i = 0; i < count; i++)
                    {
                        s[i] = String.Format("{0}", reader[i]);
                    }
                    dataGridView1.Rows.Add(s);
                }
                connection.Close();
            }
        }
    }
}
