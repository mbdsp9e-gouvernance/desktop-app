using System;
using System.Windows.Forms;

namespace EGouvernance
{
    partial class Home : Form
    {

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
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "E-gouvernance - Accueil";

            Button btnCreateTender = new Button();
            btnCreateTender.Text = "Créé appel d'offre";
            btnCreateTender.Location = new System.Drawing.Point(275, 100);
            btnCreateTender.Size = new System.Drawing.Size(300, 40);
            btnCreateTender.Click += (sender, e) => OnCreateTenderClicked();
            this.Controls.Add(btnCreateTender);

            Button btnListTender = new Button();
            btnListTender.Text = "Liste appel d'offre";
            btnListTender.Location = new System.Drawing.Point(275, 200);
            btnListTender.Size = new System.Drawing.Size(300, 40);
            btnListTender.Click += (sender, e) => OnListTenderClicked();
            this.Controls.Add(btnListTender);

            this.ResumeLayout(false);
        }

        private void OnCreateTenderClicked()
        {
            CreateTender createTenderForm = new CreateTender();
            createTenderForm.Show();
        }

        private void OnListTenderClicked()
        {
            ListTender listTenderForm = new ListTender();
            listTenderForm.Show();
        }

        #endregion
    }
}
