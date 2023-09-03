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
    public partial class form3 : Form
    {
        private ComboBox[] cb;
        private TextBox[] tb;
        private Label[] lb;
        public string result;
        private List<combo> combos;
        private List<int> forg;
        private string[] names;
        private string[] tip;
        private int c;
        private int t;
        private bool iss;
        private bool itke;
        private List<int> uniqe;
        public form3(string[]nam,List<combo> com,string[] tipes,string[]valu,List<int> frg,bool itk)
        {
            this.DialogResult = DialogResult.No;
            this.Size= new System.Drawing.Size(250, 50*(nam.Length+2));
            forg = frg;
            itke=itk;
            combos = com;
            names = nam;
            tip = tipes;
            Console.WriteLine(tip[0]);
             c=0;
             t=0;
             iss = false;
            cb = new ComboBox[com.Count];
            tb = new TextBox[nam.Length];
            lb = new Label[nam.Length];
            for(int i=0; i < nam.Length; i++) 
            {
                if (i == nam.Length - 1) { if (itk) { break; } }
                iss = false;
                lb[i] = new Label();
                lb[i].Text = names[i];
                lb[i].Location = new Point(12, 30 + i * 75);
                lb[i].Font = new Font("Microsoft Sans Serif", 10);
                lb[i].Size = new Size(350, 30);
                lb[i].TextAlign = ContentAlignment.MiddleCenter;
                this.Controls.Add(lb[i]);
                foreach(combo co in com) 
                {   
                    if (co.id == i)
                    {   
                        cb[c] = new ComboBox();
                        cb[c].AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;                      
                        cb[c].Location = new Point(12, 70 + i * 75);
                        cb[c].Size = new Size(350, 30);                  
                        foreach(string val in co.values)
                        {                              
                            cb[c].Items.Add(val);                           
                        }
                        if (valu != null) 
                        { 
                        if (forg.Contains(i))
                            {
                                Console.WriteLine("here");
                                Console.WriteLine(valu[i]);
                                string curs = "";
                                int stops = 0;
                                for(int m = 0; m < co.values.Count;m++)
                                {
                                    curs = "";
                                    curs = co.values[m];
                                    Console.WriteLine(curs);
                                    for(int k = 0; k < curs.Length; k++)
                                    {
                                        if (curs[k] == '-') { Console.WriteLine(stops); stops = k;break; }
                                    }
                                    if (valu[i] == curs.Substring(0, stops)) { cb[c].SelectedItem = curs; }
                                }
                            }
                        else { cb[c].SelectedItem = valu[i]; }
                        }
                        this.Controls.Add(cb[c]);
                        c++;
                        iss = true;
                        break;
                    }
                }
                if (iss) continue;
                tb[t] = new TextBox();
                tb[t].Location = new Point(12, 70 + i * 75);
                tb[t].Size = new Size(350, 30);
                if (valu != null)
                {
                    tb[t].Text = valu[i];
                }
                this.Controls.Add(tb[t]);
                t++;
            }   
            InitializeComponent();
            if (!(valu is null))
            {
                button1.Text = "Изменение";
            }
            button1.Location = new Point(40,  (nam.Length-1) * 75+20);
        }
        private void add_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            c = 0;
            t = 0;
            result = "";
            int test1;
            double test2;
            string cur;
            int stop=0;
            bool can_it=true;
            for(int i = 0; i < names.Length; i++)
            {
                iss = false;
                if (i == names.Length - 1) { if (itke) { break; } }
                foreach (combo co in combos)
                {
                    if (itke) 
                    {
                        if (co.id == i)
                        {
                            if (i == 0)
                            { 
                                if(((cb[c].SelectedIndex==-1)&&(cb[c+1].SelectedIndex == -1))|| ((cb[c].SelectedIndex != -1) && (cb[c + 1].SelectedIndex != -1)))
                                {
                                    can_it = false; break;
                                }
                                if (cb[c].SelectedIndex == -1)
                                {
                                    result = result + "NULL"+',';
                                    cur = cb[c + 1].SelectedItem.ToString();
                                    if (forg.Contains(i))
                                    {
                                        for (int k = 0; k < cur.Length; k++)
                                        {
                                            if (cur[k] == '-') { stop = k; break; }
                                        }
                                        cur = cur.Substring(0, stop);
                                    }
                                    result = result + @cur;
                                    if (i != names.Length - 1) { result += ","; }
                                    c=c+2;
                                    i++;
                                    iss = true;
                                    break;
                                }
                                if (cb[c+1].SelectedIndex == -1)
                                {
                                    
                                    cur = cb[c].SelectedItem.ToString();
                                    if (forg.Contains(i))
                                    {
                                        for (int k = 0; k < cur.Length; k++)
                                        {
                                            if (cur[k] == '-') { stop = k; break; }
                                        }
                                        cur = cur.Substring(0, stop);
                                    }
                                    result = result + @cur;                            
                                    if (i != names.Length - 1) { result += ","; }
                                    result = result + "NULL" + ',';
                                    c = c + 2;
                                    i++;
                                    iss = true;
                                    break;
                                }
                            }
                            else { 
                            if (cb[c].SelectedIndex == -1) { can_it = false; break; }
                            cur = cb[c].SelectedItem.ToString();
                            if (forg.Contains(i))
                            {
                                for (int k = 0; k < cur.Length; k++)
                                {
                                    if (cur[k] == '-') { stop = k; break; }
                                }
                                cur = cur.Substring(0, stop);
                            }
                            result = result + @cur;
                            if (i != names.Length - 1) { result += ","; }
                            c++;
                            iss = true;
                            break;
                            }
                        }
                    }
                    else
                    {


                        if (co.id == i)
                        {
                            if (cb[c].SelectedIndex == -1) { can_it = false; break; }
                            cur = cb[c].SelectedItem.ToString();
                            if (forg.Contains(i))
                            {
                                for (int k = 0; k < cur.Length; k++)
                                {
                                    if (cur[k] == '-') { stop = k; break; }
                                }
                                cur = cur.Substring(0, stop);
                            }
                            if (tip[i] == "string") { cur = "'" + cur + "'"; }
                            result = result + @cur;
                            if (i != names.Length - 1) { result += ","; }

                            c++;
                            iss = true;
                            break;
                        }
                    }
                    
                }
                if (!can_it) { break; }
                if (iss) continue;
                switch (tip[i])
                {
                    case "string":
                        if (i != names.Length - 1) 
                        {
                            cur = "'" + @tb[t].Text + "',";
                        }
                        else 
                        { 
                            cur = "'" + @tb[t].Text + "'"; 
                        }
                        result = result + cur;
                        break;
                    case "int":
                        can_it= int.TryParse(tb[t].Text, out test1);
                        if (can_it) { 
                        if (i != names.Length - 1)
                        {
                            result = result + @tb[t].Text + ",";
                        }
                        else 
                        {
                            result = result + @tb[t].Text;
                        }

                        }
                        break;
                    case "double":
                        cur = @tb[t].Text;
                        cur = cur.Replace(".", ",");
                        can_it = double.TryParse(cur, out test2);
                        if (can_it)
                        {   cur= cur.Replace(",", ".");
                            if (i != names.Length - 1)
                            {   
                                result = result + @cur + ",";
                            }
                            else
                            {
                                result = result + @cur;
                            }
                        }
                        break;

                }
                if (!can_it) { break; }
                t++;
            }
            if (can_it) { this.DialogResult = DialogResult.OK; }
            else { MessageBox.Show("Ошибка");}
        }
    }
}
