using ClubRegistration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmClubRegistration
{
    public partial class frmClubRegistration : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;
        private int ID, Age, Count;
        private string FirstName, MiddleName, LastName, Gender, Program;

        private long StudentID;

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshListOfClubMembers(); 
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            frmUpdateMember udMember = new frmUpdateMember();
            udMember.Show();
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {

            StudentID = Convert.ToInt64(txtStudentID.Text);
            Age = Convert.ToInt32(txtAge.Text);
            FirstName = txtFirstName.Text;
            MiddleName = txtMiddleName.Text;
            LastName = txtLastName.Text;
            Program = cbProgram.SelectedItem.ToString();
            Gender = cbGender.SelectedItem.ToString();

            clubRegistrationQuery.RegisterStudent(RegistrationID(), StudentID, FirstName, MiddleName, LastName, Age, Gender, Program);

            RefreshListOfClubMembers();
        }

        private void frmClubRegistration_Load(object sender, EventArgs e)
        {
            clubRegistrationQuery = new ClubRegistrationQuery();
            RefreshListOfClubMembers();
        }

        public frmClubRegistration()
        {
            InitializeComponent();
        }
        void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dgvClubMembers.DataSource = clubRegistrationQuery.bindingSource;
        }
        int RegistrationID()
        {
            Count++;
            return Count;
        }
    }
}