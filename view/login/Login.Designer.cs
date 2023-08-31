using System;
using System.Windows.Forms;

namespace EGouvernance
{
    partial class Login : Form
    {
        // Add private fields for username and password (for demonstration purposes)
        private const string ValidUsername = "admin";
        private const string ValidPassword = "admin";



        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code


        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(800, 450); // Updated size to 800x450
            this.Text = "E-gouvernance - Connexion";

            // Add controls for username, password, and login button
            Label lblUsername = new Label();
            lblUsername.Text = "Nom d'utilisateur:";
            lblUsername.Location = new System.Drawing.Point(150, 100);
            lblUsername.AutoSize = true;
            this.Controls.Add(lblUsername);

            TextBox txtUsername = new TextBox();
            txtUsername.Location = new System.Drawing.Point(275, 100);
            txtUsername.Size = new System.Drawing.Size(300, 70); // Updated size to 200x30
            this.Controls.Add(txtUsername);

            Label lblPassword = new Label();
            lblPassword.Text = "Mot de passe:";
            lblPassword.Location = new System.Drawing.Point(150, 150); // Updated Y position
            lblPassword.AutoSize = true;
            this.Controls.Add(lblPassword);

            TextBox txtPassword = new TextBox();
            txtPassword.Location = new System.Drawing.Point(275, 150); // Updated Y position
            txtPassword.Size = new System.Drawing.Size(300, 70); // Updated size to 200x30
            txtPassword.PasswordChar = '*';
            this.Controls.Add(txtPassword);

            Button btnLogin = new Button();
            btnLogin.Text = "Login";
            btnLogin.Location = new System.Drawing.Point(275, 200); // Updated Y position
            btnLogin.Size = new System.Drawing.Size(300, 40); // Updated size to 200x30
            btnLogin.Click += (sender, e) => OnLoginClicked(txtUsername.Text, txtPassword.Text);
            this.Controls.Add(btnLogin);

            this.ResumeLayout(false);
        }


        private void OnLoginClicked(string username, string password)
        {
            //Temporary
            Home homeForm = new Home();
            homeForm.Show();


            // Create HttpClient instance
            // using (var httpClient = new HttpClient())
            // {
            //     try
            //     {
            //         // Prepare the data to send in the request body
            //         var formData = new Dictionary<string, string>
            //             {
            //                 { "username", username },
            //                 { "password", password }
            //             };

            //         // Create the form content to send in the request body
            //         var formContent = new FormUrlEncodedContent(formData);
            //         HttpResponseMessage response = await httpClient.PostAsync(WebServiceUrl, formContent);

            //         // Check if the request was successful
            //         if (response.IsSuccessStatusCode)
            //         {
            //             string responseData = await response.Content.ReadAsStringAsync();
            //             // Show the home form
            //             Home homeForm = new Home();
            //             homeForm.Show()
            //             //MessageBox.Show("Login Successful!\nWeb Service Response: " + responseData);

            //         }
            //         else
            //         {
            //             MessageBox.Show(" Nom d'utilisateur ou Mot de passe invalide.");
            //         }
            //     }
            //     catch (Exception ex)
            //     {
            //         MessageBox.Show("An error occurred while calling the web service: " + ex.Message);
            //     }
            // }


        }

        #endregion
    }
}
