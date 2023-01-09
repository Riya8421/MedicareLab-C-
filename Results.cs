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
    public partial class Results : Form
    {
        public Results()
        {
            InitializeComponent();
            ShowResult();
            GetPatient();
            GetLab();
            GetTest();
            DateLbl.Text = DateTime.Today.Date.Day.ToString()+"-"+ DateTime.Today.Date.Month.ToString() + "-"+ DateTime.Today.Date.Year.ToString() + "-";
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\shanb\OneDrive\Documents\MedicareLabDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void GetPatName()
        {
            Con.Open();
            string Query = "Select * from Patient where PId="+PIdCb.SelectedValue.ToString()+"";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                PNameTb.Text = dr["PName"].ToString();
            }
            Con.Close();
        }
        int Cost;
        private void GetTestName()
        {
            Con.Open();
            string Query = "Select * from Test where TestCode=" + TestIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TestNameTb.Text = dr["TestName"].ToString();
                Cost = Convert.ToInt32(dr["TestCost"].ToString());
            }
            Con.Close();
        }
        private void GetLabName()
        {
            Con.Open();
            string Query = "Select * from Laboratorian where LabId=" + LIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                LNameTb.Text = dr["LName"].ToString();
            }
            Con.Close();
        }
        private void ShowResult()
        {
            Con.Open();
            string Query = "Select * from Result";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            RDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void GetTest()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select TestCode from Test", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TestCode", typeof(int));
            dt.Load(Rdr);
            TestIdCb.ValueMember = "TestCode";
            TestIdCb.DataSource = dt;
            Con.Close();
        }
        private void GetPatient()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select PId from Patient", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PId", typeof(int));
            dt.Load(Rdr);
            PIdCb.ValueMember = "PId";
            PIdCb.DataSource = dt;
            Con.Close();
        }
        private void GetLab()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select LabId from Laboratorian", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("LabId", typeof(int));
            dt.Load(Rdr);
            LIdCb.ValueMember = "LabId";
            LIdCb.DataSource = dt;
            Con.Close();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Results_Load(object sender, EventArgs e)
        {

        }
        string TR="";
        int GrdCost = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            if (LIdCb.SelectedIndex == -1 || RIdCb.SelectedIndex == -1)
            {
                MessageBox.Show("Select The Test and Result");
            }
            else
            {
                TR = TR + "***" + TestNameTb.Text + ":" + RIdCb.SelectedItem.ToString();
                TestTb.Text = TR;
                GrdCost = GrdCost + Cost;
                CostTb.Text = "" + GrdCost;
                //LIdCb.SelectedIndex = -1;
                //LNameTb.Text = "";
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (TestNameTb.Text == "" || PNameTb.Text == "" || TestTb.Text == "" ||  LIdCb.SelectedIndex == -1|| CostTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Result(PId,PName,LId,LName,TestId,TestDone,TestDate,TestCost) values(@PI,@PN,@LI,@LN,@TI,@TD,@TDa,@TC)", Con);
                    cmd.Parameters.AddWithValue("@PI", PIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@PN", PNameTb.Text);
                    cmd.Parameters.AddWithValue("@LI", LIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@LN", LNameTb.Text);
                    cmd.Parameters.AddWithValue("@TI", TestIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("TD", TestTb.Text);
                    cmd.Parameters.AddWithValue("@TDa", TDate.Value.Date);
                    cmd.Parameters.AddWithValue("TC", CostTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Result Saved!!");
                    Con.Close();
                    ShowResult();
                   // Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Ex.Message");
                }
            }
        }

        private void PIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetPatName();
        }

        private void TestIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetTestName();
        }

        private void LIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetLabName();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Laboratorians Obj = new Laboratorians();
            Obj.Show();
            this.Hide();
        }

        private void RDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 500, 600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Medicare Lab", new Font("Averia", 12, FontStyle.Bold), Brushes.Red, new Point(160, 25));
            e.Graphics.DrawString("Test Report", new Font("Averia", 10, FontStyle.Bold), Brushes.Blue, new Point(165, 45));

            string RNum = RDGV.SelectedRows[0].Cells[0].Value.ToString();
            string PId = RDGV.SelectedRows[0].Cells[1].Value.ToString();
            string PName = RDGV.SelectedRows[0].Cells[2].Value.ToString();
            string TestDone = RDGV.SelectedRows[0].Cells[5].Value.ToString();
            string TestDate = RDGV.SelectedRows[0].Cells[6].Value.ToString();
            string TestId = RDGV.SelectedRows[0].Cells[7].Value.ToString();
            string TestCost = RDGV.SelectedRows[0].Cells[8].Value.ToString();
            string LId = RDGV.SelectedRows[0].Cells[3].Value.ToString();
            string LName = RDGV.SelectedRows[0].Cells[4].Value.ToString();

            e.Graphics.DrawString("Report Number: " +RNum, new Font("Time New Romans", 10, FontStyle.Bold), Brushes.Black, new Point(50, 100));
            e.Graphics.DrawString("Patient Id: " + PId, new Font("Time New Romans", 10, FontStyle.Bold), Brushes.Black, new Point(50, 150));
            e.Graphics.DrawString("Patient Name: " + PName, new Font("Time New Romans", 10, FontStyle.Bold), Brushes.Black, new Point(250, 150));
            e.Graphics.DrawString("Laboratian Code: " + LId, new Font("Time New Romans", 10, FontStyle.Bold), Brushes.Black, new Point(50, 180));
            e.Graphics.DrawString("Laboratian Name: " + LName, new Font("Time New Romans", 8, FontStyle.Bold), Brushes.Black, new Point(50, 210));
            e.Graphics.DrawString("Test Done: " + TestDone, new Font("Time New Romans", 8, FontStyle.Bold), Brushes.Black, new Point(50, 240));
            e.Graphics.DrawString("Cost     :Tk" + TestCost, new Font("Time New Romans", 8, FontStyle.Bold), Brushes.Black, new Point(50, 270));
            e.Graphics.DrawString("Test Code: " + TestId, new Font("Time New Romans", 8, FontStyle.Bold), Brushes.Black, new Point(50, 300));
            e.Graphics.DrawString("Report Date: " + TestDate, new Font("Time New Romans", 8, FontStyle.Bold), Brushes.Black, new Point(50, 330));

            e.Graphics.DrawString("Powerded By Medicare Lab", new Font("Time New Romans", 12, FontStyle.Bold), Brushes.Blue, new Point(150, 420));
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
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Results Obj = new Results();
            Obj.Show();
            this.Hide();
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Tests Obj = new Tests();
            Obj.Show();
            this.Hide();
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Medicare Lab", new Font("Averia", 12, FontStyle.Bold), Brushes.Red, new Point(160, 25));
            e.Graphics.DrawString("Test Report", new Font("Averia", 10, FontStyle.Bold), Brushes.Blue, new Point(165, 45));

            string RNum = RDGV.SelectedRows[0].Cells[0].Value.ToString();
            string PId = RDGV.SelectedRows[0].Cells[1].Value.ToString();
            string PName = RDGV.SelectedRows[0].Cells[2].Value.ToString();
            string TestDone = RDGV.SelectedRows[0].Cells[5].Value.ToString();
            string TestDate = RDGV.SelectedRows[0].Cells[6].Value.ToString();
            string TestId = RDGV.SelectedRows[0].Cells[7].Value.ToString();
            string TestCost = RDGV.SelectedRows[0].Cells[8].Value.ToString();
            string LId = RDGV.SelectedRows[0].Cells[3].Value.ToString();
            string LName = RDGV.SelectedRows[0].Cells[4].Value.ToString();

            e.Graphics.DrawString("Report Number: " + RNum, new Font("Time New Romans", 10, FontStyle.Bold), Brushes.Black, new Point(50, 100));
            e.Graphics.DrawString("Patient Id: " + PId, new Font("Time New Romans", 10, FontStyle.Bold), Brushes.Black, new Point(50, 150));
            e.Graphics.DrawString("Patient Name: " + PName, new Font("Time New Romans", 10, FontStyle.Bold), Brushes.Black, new Point(250, 150));
            e.Graphics.DrawString("Laboratian Code: " + LId, new Font("Time New Romans", 10, FontStyle.Bold), Brushes.Black, new Point(50, 180));
            e.Graphics.DrawString("Laboratian Name: " + LName, new Font("Time New Romans", 8, FontStyle.Bold), Brushes.Black, new Point(50, 210));
            e.Graphics.DrawString("Test Done: " + TestDone, new Font("Time New Romans", 8, FontStyle.Bold), Brushes.Black, new Point(50, 240));
            e.Graphics.DrawString("Cost     :Tk" + TestCost, new Font("Time New Romans", 8, FontStyle.Bold), Brushes.Black, new Point(50, 270));
            e.Graphics.DrawString("Test Code: " + TestId, new Font("Time New Romans", 8, FontStyle.Bold), Brushes.Black, new Point(50, 300));
            e.Graphics.DrawString("Report Date: " + TestDate, new Font("Time New Romans", 8, FontStyle.Bold), Brushes.Black, new Point(50, 330));

            e.Graphics.DrawString("Powerded By Medicare Lab", new Font("Time New Romans", 12, FontStyle.Bold), Brushes.Blue, new Point(150, 420));
        }
    }
    }
