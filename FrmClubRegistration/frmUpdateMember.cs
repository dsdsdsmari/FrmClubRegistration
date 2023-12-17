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

namespace FrmClubRegistration
{
    public partial class frmUpdateMember : Form
    {
        private long studentID;
        private int Age, Count;
        private string FirstName, MiddleName, LastName, Gender, Program;

        SqlConnection sqlConnection;
        string sqlConnect; 

        private void cbStudentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM tblClubMembers WHERE StudentID = @id";
            sqlConnection.ConnectionString = sqlConnect;

            using (sqlConnection)
            {
                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();
                
                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@id", cbStudentID.Text);

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            studentID = dr.GetInt64(1);
                            FirstName = dr.GetString(2);
                            MiddleName = dr.GetString(3);
                            LastName = dr.GetString(4);
                            Age = dr.GetInt32(5);
                            Gender = dr.GetString(6);
                            Program = dr.GetString(7);

                            fillData();
                            dr.Close();
                        }
                    }
                sqlConnection.Close();
            }
        }

        public frmUpdateMember()
        {
            InitializeComponent();
            
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string updateQuery = "UPDATE tblClubMembers SET FirstName = @firstName, MiddleName = @middleName, LastName = @lastName, Age = @age, Gender = @gender, Program = @program WHERE StudentID = @id";
            sqlConnection.ConnectionString = sqlConnect;

            using (sqlConnection)
            {
                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand(updateQuery, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@id", studentID);
                    cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@middleName", txtMiddleName.Text);
                    cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@age", Convert.ToInt32(txtAge.Text));
                    cmd.Parameters.AddWithValue("@gender", cbGender.Text);
                    cmd.Parameters.AddWithValue("@program", cbProgram.Text);

                    cmd.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
            MessageBox.Show("Student Information Updated!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
                

        private void frmUpdateMember_Load(object sender, EventArgs e)
        {
            sqlConnect = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\FrmClubRegistration\FrmClubRegistration\ClubDB.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(sqlConnect);

            string query = "SELECT * FROM tblClubMembers";

            sqlConnection.ConnectionString = sqlConnect;
            

            using (sqlConnection)
            {
                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@id", cbStudentID.Text);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            cbStudentID.Items.Add(dr.GetInt64(1));
                        }
                        dr.Close();
                    }
                }
            }

        void fillData()
        {
            cbStudentID.Text = studentID.ToString();
            txtFirstName.Text = FirstName;
            txtLastName.Text = LastName;
            txtMiddleName.Text = MiddleName;
            cbProgram.Text = Program;
            txtAge.Text = Age.ToString();
            cbGender.Text = Gender;
        }
    }
}
