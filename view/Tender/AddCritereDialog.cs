using System;
using System.Windows.Forms;

namespace EGouvernance
{
    public partial class AddCritereDialog : Form
    {
        private TextBox txtEntitle;
        private TextBox txtDescription;

        public AddCritereDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtEntitle = new TextBox();
            this.txtDescription = new TextBox();

            this.SuspendLayout();

            // Set dialog size and title
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.Text = "Ajouter un critère";

            // Label for entitle
            Label lblEntitle = new Label();
            lblEntitle.Location = new System.Drawing.Point(10, 10);
            lblEntitle.Size = new System.Drawing.Size(100, 20);
            lblEntitle.Text = "Intitule:";
            this.Controls.Add(lblEntitle);

            // Input text for entitle
            this.txtEntitle.Location = new System.Drawing.Point(120, 10);
            this.txtEntitle.Size = new System.Drawing.Size(250, 20);
            this.Controls.Add(this.txtEntitle);

            // Label for description
            Label lblDescription = new Label();
            lblDescription.Location = new System.Drawing.Point(10, 40);
            lblDescription.Size = new System.Drawing.Size(100, 20);
            lblDescription.Text = "Description:";
            this.Controls.Add(lblDescription);

            // Input text area for description
            this.txtDescription.Location = new System.Drawing.Point(120, 40);
            this.txtDescription.Multiline = true;
            this.txtDescription.Size = new System.Drawing.Size(250, 100);
            this.Controls.Add(this.txtDescription);

            // OK button
            Button btnOK = new Button();
            btnOK.Location = new System.Drawing.Point(120, 150);
            btnOK.Size = new System.Drawing.Size(75, 25);
            btnOK.Text = "OK";
            btnOK.DialogResult = DialogResult.OK;
            this.Controls.Add(btnOK);

            // Cancel button
            Button btnCancel = new Button();
            btnCancel.Location = new System.Drawing.Point(195, 150);
            btnCancel.Size = new System.Drawing.Size(75, 25);
            btnCancel.Text = "Annuler";
            btnCancel.DialogResult = DialogResult.Cancel;
            this.Controls.Add(btnCancel);

            this.ResumeLayout(false);
        }

        public Critere GetAddedCriterion()
        {
            string entitle = txtEntitle.Text;
            string description = txtDescription.Text;

            return new Critere { entitle = entitle, description = description };
        }
    }
}
