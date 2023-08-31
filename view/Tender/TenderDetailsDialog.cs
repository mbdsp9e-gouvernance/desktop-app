using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EGouvernance
{
    public class TenderDetailsDialog : Form
    {
        public string tenderId { get; private set; }
        public string reference { get; private set; }
        public string titre { get; private set; }
        public string description { get; private set; }
        public string dateEmission { get; private set; }
        public string dateLimite { get; private set; }
        public string status { get; private set; }
        public Tender tenderDetail { get; private set; }
        public List<Critere> critereList { get; private set; }
        private ListView listViewCritere;

        private Label lblReference;
        private Label lblTitre;
        private Label lblDescription;
        private Label lblDateEmission;
        private Label lblDateLimite;
        private Label lblStatus;
        private Button btnModifier;
        private Button btnSoumission;

        public TenderDetailsDialog(Tender tender, string statusTender)
        {
            tenderId = tender.id;
            reference = tender.reference;
            titre = tender.title;
            description = tender.description;
            dateEmission = tender.dateEmission;
            dateLimite = tender.dateLimit;
            status = statusTender;
            critereList = tender.critere;
            tenderDetail = tender;

            InitializeComponent(reference, titre, description, dateEmission, dateLimite, critereList);
            this.ClientSize = new System.Drawing.Size(700, 275);
        }

        private void InitializeComponent(string reference, string titre, string description, string dateEmission, string dateLimite, List<Critere> critereList)
        {
            this.btnModifier = new Button();
            this.btnModifier.Location = new System.Drawing.Point(10, 200);
            this.btnModifier.Size = new System.Drawing.Size(100, 25);
            this.btnModifier.Text = "Modifier";
            this.btnModifier.Click += new EventHandler(btnModifier_Click);
            this.Controls.Add(this.btnModifier);

            this.btnSoumission = new Button();
            this.btnSoumission.Location = new System.Drawing.Point(120, 200);
            this.btnSoumission.Size = new System.Drawing.Size(200, 25);
            this.btnSoumission.Text = "Liste des soumissionnaires";
            this.btnSoumission.Click += new EventHandler(btnSoumissions_Click);
            this.Controls.Add(this.btnSoumission);

            InitializeDialogContent(reference, titre, description, dateEmission, dateLimite, critereList);
        }

        private void InitializeDialogContent(string reference, string titre, string description, string dateEmission, string dateLimite, List<Critere> critereList)
        {
            this.Text = "E-Gouvernance - Détails de l'appel d'offre";

            lblReference = new Label();
            lblReference.Text = "Référence: " + reference;
            lblReference.Location = new Point(10, 10);
            lblReference.AutoSize = true;
            this.Controls.Add(lblReference);

            lblTitre = new Label();
            lblTitre.Text = "Titre: " + titre;
            lblTitre.Location = new Point(10, lblReference.Bottom + 5);
            lblTitre.AutoSize = true;
            this.Controls.Add(lblTitre);

            lblDescription = new Label();
            lblDescription.Text = "Description: " + description;
            lblDescription.Location = new Point(10, lblTitre.Bottom + 5);
            lblDescription.AutoSize = true;
            this.Controls.Add(lblDescription);

            // Initialiser le ListView pour les critères
            listViewCritere = new ListView();
            listViewCritere.Location = new Point(450, 25);
            listViewCritere.Size = new Size(200, 150);
            listViewCritere.View = View.Details;
            listViewCritere.Columns.Add("Critère", 75);
            listViewCritere.Columns.Add("Description", 175);
            foreach (var critere in critereList)
            {
                string[] critereDetails = { critere.entitle, critere.description };
                listViewCritere.Items.Add(new ListViewItem(critereDetails));
            }
            this.Controls.Add(listViewCritere);

            int yOffset = lblDescription.Bottom + 5;
            lblDateEmission = new Label();
            lblDateEmission.Text = "Date d'émission: " + dateEmission;
            lblDateEmission.Location = new Point(10, yOffset);
            lblDateEmission.AutoSize = true;
            this.Controls.Add(lblDateEmission);

            lblDateLimite = new Label();
            lblDateLimite.Text = "Date limite: " + dateLimite;
            lblDateLimite.Location = new Point(10, lblDateEmission.Bottom + 5);
            lblDateLimite.AutoSize = true;
            this.Controls.Add(lblDateLimite);

            lblStatus = new Label();
            lblStatus.Text = "Status: " + status;
            lblStatus.Location = new Point(10, lblDateLimite.Bottom + 5);
            lblStatus.AutoSize = true;
            this.Controls.Add(lblStatus);
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            UpdateTenderForm updateForm = new UpdateTenderForm(tenderDetail);
            DialogResult result = updateForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                reference = updateForm.reference;
                titre = updateForm.titre;
                description = updateForm.description;
                dateLimite = updateForm.dateLimite;

                listViewCritere.Items.Clear();
                foreach (var critere in updateForm.critereListUpdate)
                {
                    string[] row = { critere.entitle, critere.description };
                    listViewCritere.Items.Add(new ListViewItem(row));
                }

                lblReference.Text = "Référence: " + reference;
                lblTitre.Text = "Titre: " + titre;
                lblDescription.Text = "Description: " + description;
                lblDateLimite.Text = "Date limite: " + dateLimite;
            }
        }

        private void btnSoumissions_Click(object sender, EventArgs e)
        {
            ListBidder listBidderForm = new ListBidder(tenderDetail);
            listBidderForm.Show();
        }
    }
}
