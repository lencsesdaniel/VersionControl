using Lencsés_Dániel_M8I1LN_VersionControl.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lencsés_Dániel_M8I1LN_VersionControl
{
    

    public partial class Form1 : Form
    {
        BindingList<Entities.User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            label1.Text = Resource1.FullName;
            button1.Text = Resource1.Add;

            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";

            var u = new User()
            {
                FullName = textBox1.Text,
            };
            users.Add(u);
        }
    }
}
