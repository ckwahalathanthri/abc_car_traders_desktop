using ABCCarTraders.DataAccess;
using ABCCarTraders.Models;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ABCCarTraders.Forms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new System.Drawing.Size(450, 500);
            this.Text = "Customer Registration";
            this.StartPosition = FormStartPosition.CenterScreen;

            // Create form controls
            Label lblTitle = new Label();
            lblTitle.Text = "Customer Registration";
            lblTitle.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(120, 20);
            lblTitle.Size = new System.Drawing.Size(250, 30);

            // Username
            Label lblUsername = new Label();
            lblUsername.Text = "Username:";
            lblUsername.Location = new System.Drawing.Point(50, 70);
            lblUsername.Size = new System.Drawing.Size(100, 20);

            TextBox txtUsername = new TextBox();
            txtUsername.Name = "txtUsername";
            txtUsername.Location = new System.Drawing.Point(180, 68);
            txtUsername.Size = new System.Drawing.Size(200, 20);

            // Password
            Label lblPassword = new Label();
            lblPassword.Text = "Password:";
            lblPassword.Location = new System.Drawing.Point(50, 110);
            lblPassword.Size = new System.Drawing.Size(100, 20);

            TextBox txtPassword = new TextBox();
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Location = new System.Drawing.Point(180, 108);
            txtPassword.Size = new System.Drawing.Size(200, 20);

            // Full Name
            Label lblFullName = new Label();
            lblFullName.Text = "Full Name:";
            lblFullName.Location = new System.Drawing.Point(50, 150);
            lblFullName.Size = new System.Drawing.Size(100, 20);

            TextBox txtFullName = new TextBox();
            txtFullName.Name = "txtFullName";
            txtFullName.Location = new System.Drawing.Point(180, 148);
            txtFullName.Size = new System.Drawing.Size(200, 20);

            // Email
            Label lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Location = new System.Drawing.Point(50, 190);
            lblEmail.Size = new System.Drawing.Size(100, 20);

            TextBox txtEmail = new TextBox();
            txtEmail.Name = "txtEmail";
            txtEmail.Location = new System.Drawing.Point(180, 188);
            txtEmail.Size = new System.Drawing.Size(200, 20);

            // Phone
            Label lblPhone = new Label();
            lblPhone.Text = "Phone:";
            lblPhone.Location = new System.Drawing.Point(50, 230);
            lblPhone.Size = new System.Drawing.Size(100, 20);

            TextBox txtPhone = new TextBox();
            txtPhone.Name = "txtPhone";
            txtPhone.Location = new System.Drawing.Point(180, 228);
            txtPhone.Size = new System.Drawing.Size(200, 20);

            // Address
            Label lblAddress = new Label();
            lblAddress.Text = "Address:";
            lblAddress.Location = new System.Drawing.Point(50, 270);
            lblAddress.Size = new System.Drawing.Size(100, 20);

            TextBox txtAddress = new TextBox();
            txtAddress.Name = "txtAddress";
            txtAddress.Multiline = true;
            txtAddress.Location = new System.Drawing.Point(180, 268);
            txtAddress.Size = new System.Drawing.Size(200, 60);

            // Buttons
            Button btnRegister = new Button();
            btnRegister.Text = "Register";
            btnRegister.Location = new System.Drawing.Point(180, 350);
            btnRegister.Size = new System.Drawing.Size(80, 30);
            btnRegister.Click += (sender, e) => RegisterUser(txtUsername.Text, txtPassword.Text, 
                txtFullName.Text, txtEmail.Text, txtPhone.Text, txtAddress.Text);

            Button btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Location = new System.Drawing.Point(280, 350);
            btnCancel.Size = new System.Drawing.Size(80, 30);
            btnCancel.Click += (sender, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblTitle, lblUsername, txtUsername, lblPassword, txtPassword,
                lblFullName, txtFullName, lblEmail, txtEmail, lblPhone, txtPhone, lblAddress, txtAddress,
                btnRegister, btnCancel });
        }

        private void RegisterUser(string username, string password, string fullName, string email, string phone, string address)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please fill all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var user = new User
            {
                Username = username,
                Password = password, // In production, hash this password
                FullName = fullName,
                Email = email,
                Phone = phone,
                Address = address,
                UserType = UserType.Customer
            };

            var userRepository = new UserRepository();
            if (userRepository.CreateUser(user))
            {
                MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Registration failed. Username might already exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
