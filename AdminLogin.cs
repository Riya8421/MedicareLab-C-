using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicareLab
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void Log_Click(object sender, EventArgs e)
        {
            if (UPassTb.Text == "")
            {
                MessageBox.Show("Enter A Password.");
            }else if (UPassTb.Text == "Password")
            {
                Login obj = new Login();
                obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Admin Password...");
            }
        }
    }
}
