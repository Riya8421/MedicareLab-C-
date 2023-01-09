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
    public partial class Patients : Form
    {
        public Patients()
        {
            InitializeComponent();
            ShowPatient();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\shanb\OneDrive\Documents\MedicareLabDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowPatient()
        {
            Con.Open();
            string Query = "Select * from Patient";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PatDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            PNameTb.Text = "";
            PPhoneTb.Text = "";
            PAddressTb.Text = "";
            Key = 0;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (PNameTb.Text == "" || PAddressTb.Text == "" || PPhoneTb.Text == "" || PGenderCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Patient(PName,PGender,PAddress,PPhone,PDOB) values(@PN,@PG,@PA,@PP,@PD)", Con);
                    cmd.Parameters.AddWithValue("@PN", PNameTb.Text);
                    cmd.Parameters.AddWithValue("@PG", PGenderCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PA", PAddressTb.Text);
                    cmd.Parameters.AddWithValue("PP", PPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@PD", PDOB.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Saved!!");
                    Con.Close();
                    ShowPatient();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Ex.Message");
                }
            }
            }
        int Key = 0;
        private void PatDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PNameTb.Text = PatDGV.SelectedRows[0].Cells[1].Value.ToString();
            PGenderCb.SelectedItem = PatDGV.SelectedRows[0].Cells[2].Value.ToString();
            PAddressTb.Text = PatDGV.SelectedRows[0].Cells[3].Value.ToString();
            PPhoneTb.Text = PatDGV.SelectedRows[0].Cells[4].Value.ToString();
            PDOB.Text = PatDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (PNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(PatDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select A Patient...");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Patient where PId=@PKey", Con);

                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Deleted!!");
                    Con.Close();
                    ShowPatient();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Ex.Message");
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (PNameTb.Text == "" || PAddressTb.Text == "" || PPhoneTb.Text == "" || PGenderCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update Patient Set PName=@PN,PGender=@PG,PAddress=@PA,PPhone=@PP,PDOB=@PD where PId=@Pkey", Con);
                    cmd.Parameters.AddWithValue("@PN", PNameTb.Text);
                    cmd.Parameters.AddWithValue("@PG", PGenderCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PA", PAddressTb.Text);
                    cmd.Parameters.AddWithValue("PP", PPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@PD", PDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Updated!!");
                    Con.Close();
                    ShowPatient();
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
            Tests Obj = new Tests();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
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
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Patients Obj = new Patients();
            Obj.Show();
            this.Hide();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
