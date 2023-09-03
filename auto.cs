using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class auto : Form
    {
        public auto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin") { if (textBox2.Text == "2803") { string con = $@"Data Source=.\SQLEXPRESS;Initial Catalog=reestr;Integrated Security=False;User Id=Dbadmin;Password=2803;MultipleActiveResultSets=True;TrustServerCertificate=true;"; Form2 f = new Form2(con);f.Text="admin"; f.Show(); this.Hide(); } else { MessageBox.Show("Неверный логин или пароль"); } }
            else if (textBox1.Text == "user") { if (textBox2.Text == "1234") { string con = $@"Data Source=.\SQLEXPRESS;Initial Catalog=reestr;Integrated Security=False;User Id=Dbuser;Password=1234;MultipleActiveResultSets=True;TrustServerCertificate=true;"; Form2 f = new Form2(con);f.Text="user"; f.Show(); this.Hide(); } else { MessageBox.Show("Неверный логин или пароль"); } }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void auto_Load(object sender, EventArgs e)
        {

        }
    }
}
