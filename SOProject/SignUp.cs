using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace SOProject
{
    public partial class SignUp : Form
    {

        Socket server;
        public SignUp(Socket s)
        {
            InitializeComponent();

            this.server = s;
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void email_TextChanged(object sender, EventArgs e)
        {

        }

        private void Username_TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ConfirmPassword_TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void acceptbutton_Click(object sender, EventArgs e)
        {

            //Registers the user with its password

            try
            {
               if (password_TextBox.Text != ConfirmPassword_TextBox.Text)
               {
                    MessageBox.Show("The passwords do not match. Please, check it again."); //Error of not matching passwords
               }

               else
               {
                    byte[] newUser = System.Text.Encoding.ASCII.GetBytes("01" + email.Text + '\0' + Username_TextBox.Text + '\0' + password_TextBox.Text + '\0');
                    server.Send(newUser);

                    byte[] response = new byte[1024];
                    server.Receive(response);
               }

            }

            catch (SocketException ex)
            {
                MessageBox.Show("The connection with the sever failed");
            }

        }

    }
}

