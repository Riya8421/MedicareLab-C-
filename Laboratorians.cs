using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MedicareLab
{
    public partial class Laboratorians : Form
    {
        public Laboratorians()
        {
            InitializeComponent();
            ShowLaboratorian();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\shanb\OneDrive\Documents\MedicareLabDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowLaboratorian()
        {
            Con.Open();
            string Query = "Select * from Laboratorian";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            LabDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(LNameTb.Text == "" || LAddressTb.Text==""||LPhoneTb.Text==""||LGenderCb.SelectedIndex==-1||LQualificationCb.SelectedIndex==-1)
            {
                MessageBox.Show("Missing Information");
            }else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Laboratorian(LName,LGender,LAddress,LQualification,LPhone,LDOB,LPass) values(@LN,@LG,@LA,@LQ,@LP,@LD,@LPa)", Con);
                    cmd.Parameters.AddWithValue("@LN", LNameTb.Text);
                    cmd.Parameters.AddWithValue("@LG", LGenderCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LA", LAddressTb.Text);
                    cmd.Parameters.AddWithValue("@LQ", LQualificationCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("LP", LPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@LD", LDOB.Value.Date);
                    cmd.Parameters.AddWithValue("LPa", PassTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Laboratorian Saved!!");
                    Con.Close();
                    ShowLaboratorian();
                    Reset(); 
                }
                catch(Exception Ex)
                {
                    MessageBox.Show("Ex.Message");
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (LNameTb.Text == "" || LAddressTb.Text == "" || LPhoneTb.Text == "" || LGenderCb.SelectedIndex == -1 || LQualificationCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update Laboratorian Set LName=@LN,LGender=@LG,LAddress=@LA,LQualification=@LQ,LPhone=@LP,LDOB=@LD,LPass=@LPa where LabId=@LKey", Con);
                    cmd.Parameters.AddWithValue("@LN", LNameTb.Text);
                    cmd.Parameters.AddWithValue("@LG", LGenderCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LA", LAddressTb.Text);
                    cmd.Parameters.AddWithValue("@LQ", LQualificationCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("LP", LPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@LD", LDOB.Value.Date);
                    cmd.Parameters.AddWithValue("LPa", PassTb.Text);
                    cmd.Parameters.AddWithValue("@LKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Laboratorian Updated!!");
                    Con.Close();
                    ShowLaboratorian();
                    Reset(); 
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Ex.Message");
                }
            }
            }
        private void Reset()
        {
            LNameTb.Text = "";
            LAddressTb.Text = "";
            LPhoneTb.Text = "";
            LGenderCb.SelectedIndex = -1;
            LQualificationCb.SelectedIndex = -1;
            Key = 0;

        }
        int Key = 0;
        private void LabDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LNameTb.Text = LabDGV.SelectedRows[0].Cells[1].Value.ToString();
            LGenderCb.SelectedItem = LabDGV.SelectedRows[0].Cells[2].Value.ToString();
            LAddressTb.Text = LabDGV.SelectedRows[0].Cells[3].Value.ToString();
            LQualificationCb.SelectedItem = LabDGV.SelectedRows[0].Cells[4].Value.ToString();
            LPhoneTb.Text = LabDGV.SelectedRows[0].Cells[5].Value.ToString();
            LDOB.Text = LabDGV.SelectedRows[0].Cells[6].Value.ToString();
            PassTb.Text = LabDGV.SelectedRows[0].Cells[7].Value.ToString();
            if (LNameTb.Text=="")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(LabDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select A Laboratorian...");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Laboratorian where LabId=@LKey", Con);
                  
                    cmd.Parameters.AddWithValue("@LKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Laboratorian Deleted!!");
                    Con.Close();
                    ShowLaboratorian();
                    Reset(); 
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Ex.Message");
                }
        }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
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
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void LabDGV_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
