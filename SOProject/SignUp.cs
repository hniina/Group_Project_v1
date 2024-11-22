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
                    byte[] register = System.Text.Encoding.ASCII.GetBytes("1/" + email.Text + "/" + Username_TextBox.Text + "/" + password_TextBox.Text);
                    server.Send(register);
                }

            }

            catch (SocketException ex)
            {
                MessageBox.Show("The connection with the sever failed");
            }

        }

        public void SignUpFunction(string mensaje)
        {
            switch (Convert.ToInt32(mensaje))
            {
                case 0:
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action<string>(SignUpFunction), mensaje);
                    }
                    else
                    {
                        MessageBox.Show("The email is already registered. Use another one.");
                    }
                    break;

                case 1:
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action<string>(SignUpFunction), mensaje);
                    }
                    else
                    {
                        MessageBox.Show("This username already exists. Please, choose another one.");
                    }
                    break;

                case 2:
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action<string>(SignUpFunction), mensaje);
                    }
                    else
                    {
                        MessageBox.Show("The email must have between 15 and 80 characters.");
                    }
                    break;

                case 3:
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action<string>(SignUpFunction), mensaje);
                    }
                    else
                    {
                        MessageBox.Show("Your username must have between 3 and 80 characters.");
                    }
                    break;


                case 4:
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action<string>(SignUpFunction), mensaje);
                    }
                    else
                    {
                        MessageBox.Show("Your passwrd must have between 8 and 20 characters");
                    }
                    break;


                case 5:
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action<string>(SignUpFunction), mensaje);
                    }
                    else
                    {
                        MessageBox.Show("Register was unsuccessful. Please, try again.");
                    }
                    break;

                case 6:
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action<string>(SignUpFunction), mensaje);
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("New user registered successfully!");
                        this.Hide();
                    }
                    break;
            }
        }

    }
}

