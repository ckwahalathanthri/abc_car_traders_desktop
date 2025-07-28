using ABCCarTraders.Business;
using ABCCarTraders.Models;
using System;
using System.Windows.Forms;

namespace ABCCarTraders.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new System.Drawing.Size(400, 300);
            this.Text = "ABC Car Traders - Login";
            this.StartPosition = FormStartPosition.CenterScreen;

            // Create controls
            Label lblTitle = new Label();
            lblTitle.Text = "ABC Car Traders";
            lblTitle.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(120, 30);
            lblTitle.Size = new System.Drawing.Size(200, 30);

            Label lblUsername = new Label();
            lblUsername.Text = "Username:";
            lblUsername.Location = new System.Drawing.Point(50, 80);
            lblUsername.Size = new System.Drawing.Size(80, 20);

            TextBox txtUsername = new TextBox();
            txtUsername.Name = "txtUsername";
            txtUsername.Location = new System.Drawing.Point(150, 78);
            txtUsername.Size = new System.Drawing.Size(180, 20);

            Label lblPassword = new Label();
            lblPassword.Text = "Password:";
            lblPassword.Location = new System.Drawing.Point(50, 120);
            lblPassword.Size = new System.Drawing.Size(80, 20);

            TextBox txtPassword = new TextBox();
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Location = new System.Drawing.Point(150, 118);
            txtPassword.Size = new System.Drawing.Size(180, 20);

            Button btnLogin = new Button();
            btnLogin.Text = "Login";
            btnLogin.Location = new System.Drawing.Point(150, 160);
            btnLogin.Size = new System.Drawing.Size(80, 30);
            btnLogin.Click += (sender, e) => PerformLogin(txtUsername.Text, txtPassword.Text);

            Button btnRegister = new Button();
            btnRegister.Text = "Register";
            btnRegister.Location = new System.Drawing.Point(250, 160);
            btnRegister.Size = new System.Drawing.Size(80, 30);
            btnRegister.Click += (sender, e) => OpenRegisterForm();

            // Add controls to form
            this.Controls.AddRange(new Control[] { lblTitle, lblUsername, txtUsername, lblPassword, txtPassword, btnLogin, btnRegister });
        }

        private void PerformLogin(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (AuthenticationService.Login(username, password))
            {
                this.Hide();
                if (AuthenticationService.IsAdmin)
                {
                    new AdminDashboard().Show();
                }
                else
                {
                    new CustomerDashboard().Show();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenRegisterForm()
        {
            new RegisterForm().ShowDialog();
        }
    }
}
