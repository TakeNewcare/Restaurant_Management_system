using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CM
{
    public partial class fkmLogin : Form
    {
        public fkmLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (MainClass.IsValidUser(txtUser.Text, txtPass.Text) == false)   
            {
                guna2MessageDialog1.Show("아이디 또는 비밀번호가 틀렸습니다.");
                return;
            }
            else           
            {
                this.Hide();
                fkmMain frm = new fkmMain();
                frm.Show();
            }
        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (MainClass.IsValidUser(txtUser.Text,txtPass.Text) == false)
                {
                    guna2MessageDialog1.Show("아이디 또는 비밀번호가 틀렸습니다.");
                    return;
                }
                else
                {
                    this.Hide();
                    fkmMain frm = new fkmMain();
                    frm.Show();
                }
            }
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (MainClass.IsValidUser(txtUser.Text, txtPass.Text) == false)
                {
                    guna2MessageDialog1.Show("아이디 또는 비밀번호가 틀렸습니다.");
                    return;
                }
                else
                {
                    this.Hide();
                    fkmMain frm = new fkmMain();
                    frm.Show();
                }

            }

        }
    }
}
