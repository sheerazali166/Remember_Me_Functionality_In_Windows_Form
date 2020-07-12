using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Remember_Me_Application
{
    public partial class Form1 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["csdb"].ConnectionString;

        public Form1()
        {
            InitializeComponent();
            LoadCredentials();
        }

        private void buttonBogin_Click(object sender, EventArgs e)
        {
            //if (textBoxUsername.Text == "Saba" && textBoxPassword.Text == "Mehak")
            //{

            //    MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    SaveCredentials();
            //    this.Hide();
            //    Form2 form2 = new Form2();
            //    form2.Show();
            //}
            //else
            //{

            //    MessageBox.Show("Login Failed.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            SqlConnection conn = new SqlConnection(cs);
            string query = "select * from Login where Username = @uName and Password = @pass";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@uName", textBoxUsername.Text);
            cmd.Parameters.AddWithValue("@pass", textBoxPassword.Text);

            conn.Open();
            
            SqlDataReader reader = cmd.ExecuteReader();

            if (!string.IsNullOrEmpty(textBoxUsername.Text) && !string.IsNullOrEmpty(textBoxPassword.Text))
            {

                if (reader.HasRows)
                {

                    MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SaveCredentials();
                    this.Hide();
                    Form2 form2 = new Form2();
                    form2.Show();
                }
                else
                {

                    MessageBox.Show("Invalid username or password", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else {

                MessageBox.Show("username or password is empty", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }

        private void SaveCredentials() {

            if (checkBoxRemeberMe.Checked == true)
            {

                Properties.Settings.Default.Username = textBoxUsername.Text;
                Properties.Settings.Default.Password = textBoxPassword.Text;
                Properties.Settings.Default.Save();
            }
            else {

                Properties.Settings.Default.Username = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
            }
        }

        private void LoadCredentials() {

            if (Properties.Settings.Default.Username != string.Empty) {

                textBoxUsername.Text = Properties.Settings.Default.Username;
                textBoxPassword.Text = Properties.Settings.Default.Password;
                checkBoxRemeberMe.Checked = true;
            }
        }
    }
}
