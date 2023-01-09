using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicareLab
{
    public partial class Tests : Form
    {
        public Tests()
        {
            InitializeComponent();
            ShowTest();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\shanb\OneDrive\Documents\MedicareLabDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowTest()
        {
            Con.Open();
            string Query = "Select * from Test";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TestDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (TNameTb.Text == "" || TCostTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Test(TestName,TestCost) values(@TN,@TC)", Con);
                    cmd.Parameters.AddWithValue("@TN", TNameTb.Text);
                    cmd.Parameters.AddWithValue("@TC", TCostTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Test Saved!!");
                    Con.Close();
                    ShowTest();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Ex.Message");
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select A Test ...");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Test where TestCode=@TKey", Con);

                    cmd.Parameters.AddWithValue("@TKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Test Deleted!!");
                    Con.Close();
                    ShowTest();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Ex.Message");
                }
            }
        }
        int Key = 0;
        private void TestDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TNameTb.Text = TestDGV.SelectedRows[0].Cells[1].Value.ToString();
            TCostTb.Text = TestDGV.SelectedRows[0].Cells[2].Value.ToString();
            if (TNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(TestDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        private void Reset()
        {
            TNameTb.Text = "";
            TCostTb.Text = "";
            Key = 0;
        }
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (TNameTb.Text == "" || TCostTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update Test Set TestName=@TN,TestCost=@TC where TestCode=@TKey", Con);
                    cmd.Parameters.AddWithValue("@TN", TNameTb.Text);
                    cmd.Parameters.AddWithValue("@TC", TCostTb.Text);
                    cmd.Parameters.AddWithValue("@TKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Test Update!!");
                    Con.Close();
                    ShowTest();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Ex.Message");
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Results Obj = new Results();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Laboratorians Obj = new Laboratorians();
            Obj.Show();
            this.Hide();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Results Obj = new Results();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Patients Obj = new Patients();
            Obj.Show();
            this.Hide();
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
