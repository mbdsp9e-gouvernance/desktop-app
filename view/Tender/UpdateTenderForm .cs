using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace EGouvernance
{
    public partial class UpdateTenderForm : Form
    {

        // Propriétés publiques pour les données de l'appel d'offre
        public string tenderId { get; set; }
        public string reference { get; set; }
        public string titre { get; set; }
        public string description { get; set; }
        public string dateLimite { get; set; }

        public List<Critere> critereListUpdate { get; set; }

        private Label lblReference;
        private Label lblTitre;
        private Label lblDescription;
        private Label lblDateLimite;
        private TextBox txtReference;
        private TextBox txtTitre;
        private TextBox txtDescription;
        private DateTimePicker dtpDateLimite;
        private Button btnSave;
        private Button btnCancel;

        private ListView listViewCritere;
        private Button btnSupprimerCritere;
        private Button btnAjouterCritere;

        private List<Critere> critereList = new List<Critere>();

        public UpdateTenderForm(Tender tender)
        {
            // Initialiser les propriétés et les contrôles avec les données existantes
            InitializeComponent();
            tenderId = tender.id;
            reference = tender.reference;
            titre = tender.title;
            description = tender.description;
            dateLimite = tender.dateLimit;


            // Remplir les contrôles avec les données de l'appel d'offre existantes
            txtReference.Text = reference;
            txtTitre.Text = titre;
            txtDescription.Text = description;


            DateTime dateLimitParse;
            if (DateTime.TryParse(tender.dateLimit, out dateLimitParse))
            {
                dtpDateLimite.Value = dateLimitParse;
            }

            critereList = tender.critere;
            UpdateCritereListView();
        }

        private void InitializeComponent()
        {
            this.lblReference = new Label();
            this.lblTitre = new Label();
            this.lblDescription = new Label();
            this.lblDateLimite = new Label();
            this.txtReference = new TextBox();
            this.txtTitre = new TextBox();
            this.txtDescription = new TextBox();
            this.dtpDateLimite = new DateTimePicker();


            this.SuspendLayout();

            // Set form size and title
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "E-gouvernance - Modification d'appel d'offre";


            // Label for reference
            this.lblReference.Location = new System.Drawing.Point(10, 10);
            this.lblReference.Size = new System.Drawing.Size(100, 20);
            this.lblReference.Text = "Référence:";
            this.Controls.Add(this.lblReference);

            // Input text for reference
            this.txtReference.Location = new System.Drawing.Point(120, 10);
            this.txtReference.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(this.txtReference);

            // Label for titre
            this.lblTitre.Location = new System.Drawing.Point(10, 40);
            this.lblTitre.Size = new System.Drawing.Size(100, 20);
            this.lblTitre.Text = "Titre:";
            this.Controls.Add(this.lblTitre);

            // Input text for titre
            this.txtTitre.Location = new System.Drawing.Point(120, 40);
            this.txtTitre.Size = new System.Drawing.Size(400, 20);
            this.Controls.Add(this.txtTitre);

            // Label for description
            this.lblDescription.Location = new System.Drawing.Point(10, 70);
            this.lblDescription.Size = new System.Drawing.Size(100, 20);
            this.lblDescription.Text = "Description:";
            this.Controls.Add(this.lblDescription);

            // Input text area for description
            this.txtDescription.Location = new System.Drawing.Point(120, 70);
            this.txtDescription.Multiline = true;
            this.txtDescription.Size = new System.Drawing.Size(400, 200);
            this.Controls.Add(this.txtDescription);

            // Label for date limite
            this.lblDateLimite.Location = new System.Drawing.Point(10, 310);
            this.lblDateLimite.Size = new System.Drawing.Size(100, 20);
            this.lblDateLimite.Text = "Date Limite:";
            this.Controls.Add(this.lblDateLimite);

            // Input date for date limite
            this.dtpDateLimite.Location = new System.Drawing.Point(120, 310);
            this.dtpDateLimite.Size = new System.Drawing.Size(300, 20);
            this.Controls.Add(this.dtpDateLimite);

            // Créer les contrôles pour gérer la liste des critères
            listViewCritere = new ListView();
            listViewCritere.Location = new System.Drawing.Point(550, 100);
            listViewCritere.Size = new System.Drawing.Size(200, 150);
            listViewCritere.View = View.Details;
            listViewCritere.GridLines = true;
            listViewCritere.Columns.Add("Intitulé", 75);
            listViewCritere.Columns.Add("Description", 175);
            this.Controls.Add(listViewCritere);

            // Bouton pour supprimer un critère
            btnSupprimerCritere = new Button();
            btnSupprimerCritere.Location = new System.Drawing.Point(650, 260);
            btnSupprimerCritere.Size = new Size(100, 30);
            btnSupprimerCritere.Text = "Supprimer critère";
            btnSupprimerCritere.Click += new EventHandler(btnSupprimerCritere_Click);
            this.Controls.Add(btnSupprimerCritere);

            // Bouton pour ajouter un critère
            btnAjouterCritere = new Button();
            btnAjouterCritere.Location = new System.Drawing.Point(550, 260);
            btnAjouterCritere.Size = new Size(100, 30);
            btnAjouterCritere.Text = "Ajouter critère";
            btnAjouterCritere.Click += new EventHandler(btnAjouterCritere_Click);
            this.Controls.Add(btnAjouterCritere);



            // Bouton "Enregistrer"
            btnSave = new Button();
            btnSave.Location = new System.Drawing.Point(150, 360);  // Ajuster la position si nécessaire
            btnSave.Text = "Enregistrer";
            btnSave.Size = new System.Drawing.Size(100, 30);
            btnSave.Click += new EventHandler(btnSave_Click);
            this.Controls.Add(btnSave);

            // Bouton "Annuler"
            btnCancel = new Button();
            btnCancel.Location = new System.Drawing.Point(260, 360);  // Ajuster la position si nécessaire
            btnCancel.Text = "Annuler";
            btnCancel.Size = new System.Drawing.Size(100, 30);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            this.Controls.Add(btnCancel);
        }


        private void UpdateCritereListView()
        {
            listViewCritere.Items.Clear();
            foreach (var critere in critereList)
            {
                string[] row = { critere.entitle, critere.description };
                listViewCritere.Items.Add(new ListViewItem(row));
            }
        }

        private void btnSupprimerCritere_Click(object sender, EventArgs e)
        {
            if (listViewCritere.SelectedItems.Count > 0)
            {
                int selectedIndex = listViewCritere.SelectedIndices[0];
                critereList.RemoveAt(selectedIndex);
                UpdateCritereListView();
            }
            else
            {
                MessageBox.Show("Sélectionnez un critère à supprimer.", "Aucune sélection");
            }
        }

        private void btnAjouterCritere_Click(object sender, EventArgs e)
        {
            // Ouvrir une fenêtre pour ajouter un critère
            AddCritereDialog addCritereDialog = new AddCritereDialog();
            if (addCritereDialog.ShowDialog() == DialogResult.OK)
            {
                critereList.Add(addCritereDialog.GetAddedCriterion());
                UpdateCritereListView();
            }
        }

        private async void UpdateTender()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var updatedTender = new Tender
                    {
                        reference = txtReference.Text,
                        title = txtTitre.Text,
                        description = txtDescription.Text,
                        dateLimit = dtpDateLimite.Value.ToString("yyyy-MM-dd"),
                        critere = critereList
                    };

                    var jsonUpdatedTender = JsonSerializer.Serialize(updatedTender);
                    var content = new StringContent(jsonUpdatedTender, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"{AppConfig.WebServiceUrl}/tender/update?id=" + tenderId, content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("L'appel d'offre a été mis à jour avec succès !");
                    }
                    else
                    {
                        MessageBox.Show("La mise à jour de l'appel d'offre a échoué. Code de statut : " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite lors de la mise à jour de l'appel d'offre : " + ex.Message);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            reference = txtReference.Text;
            titre = txtTitre.Text;
            description = txtDescription.Text;
            dateLimite = dtpDateLimite.Value.ToString("yyyy-MM-dd");
            critereListUpdate = critereList;

            UpdateTender();

            // Fermer la fenêtre après enregistrement
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Fermer la fenêtre sans enregistrement
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
