
using System.Text;
using System.Text.Json;

namespace EGouvernance
{
    partial class CreateTender : Form
    {
        // Input fields for creating a tender
        private Label lblReference;
        private Label lblTitre;
        private Label lblDescription;
        private Label lblDateLimite;
        private TextBox txtReference;
        private TextBox txtTitre;
        private TextBox txtDescription;
        private DateTimePicker dateLimite;
        private Button btnAjouter;

        private ListView listViewCritere;
        private List<Critere> critereList = new List<Critere>();
        private Button btnSupprimer;


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
            this.lblReference = new Label();
            this.lblTitre = new Label();
            this.lblDescription = new Label();
            this.lblDateLimite = new Label();
            this.txtReference = new TextBox();
            this.txtTitre = new TextBox();
            this.txtDescription = new TextBox();
            this.dateLimite = new DateTimePicker();


            this.SuspendLayout();

            // Set form size and title
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "E-gouvernance - Création d'appel d'offre";

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
            this.dateLimite.Location = new System.Drawing.Point(120, 310);
            this.dateLimite.Size = new System.Drawing.Size(300, 20);
            this.Controls.Add(this.dateLimite);

            //List critere
            listViewCritere = new ListView();
            listViewCritere.Location = new System.Drawing.Point(550, 100);
            listViewCritere.Size = new System.Drawing.Size(200, 150);
            listViewCritere.View = View.Details;
            listViewCritere.GridLines = true;
            listViewCritere.Columns.Add("Intitulé", 75);
            listViewCritere.Columns.Add("Description", 175);
            this.Controls.Add(listViewCritere);

            // Bouton pour supprimer un critère
            btnSupprimer = new Button();
            btnSupprimer.Location = new System.Drawing.Point(650, 300);
            btnSupprimer.Size = new Size(100, 30);
            btnSupprimer.Text = "Supprimer";
            btnSupprimer.Click += new EventHandler(btnSupprimer_Click);
            this.Controls.Add(btnSupprimer);

            // Bouton pour ajouter critere
            Button btnAjouterCritere = new Button();
            btnAjouterCritere.Location = new System.Drawing.Point(550, 300);
            btnAjouterCritere.Size = new System.Drawing.Size(100, 30);
            btnAjouterCritere.Text = "Ajouter critère";
            btnAjouterCritere.Click += new EventHandler(btnAjouterCritere_Click);
            this.Controls.Add(btnAjouterCritere);


            // Bouton "Ajouter"
            btnAjouter = new Button();
            btnAjouter.Location = new System.Drawing.Point(200, 360); // Position du bouton "Ajouter"
            btnAjouter.Size = new System.Drawing.Size(120, 30);
            btnAjouter.Text = "Ajouter";
            btnAjouter.Click += new EventHandler(btnAjouter_Click);
            this.Controls.Add(btnAjouter);

            this.ResumeLayout(false);
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

        private void UpdateCritereListView()
        {
            listViewCritere.Items.Clear();
            foreach (var criterion in critereList)
            {
                string[] row = { criterion.entitle, criterion.description };
                listViewCritere.Items.Add(new ListViewItem(row));
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (listViewCritere.SelectedItems.Count > 0)
            {
                // Supprimer le critère sélectionné de la liste
                int selectedIndex = listViewCritere.SelectedIndices[0];
                critereList.RemoveAt(selectedIndex);

                // Mettre à jour la liste après la suppression
                UpdateCritereListView();
            }
            else
            {
                MessageBox.Show("Sélectionnez un critère à supprimer.", "Aucune sélection");
            }
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            // Récupérer les valeurs saisies dans les champs
            string reference = txtReference.Text;
            string titre = txtTitre.Text;
            string description = txtDescription.Text;
            string dateLimiteValue = dateLimite.Value.ToString("yyyy-MM-dd");

            // code save 
            createTender(reference, titre, description, dateLimiteValue, critereList);
        }

        private async void createTender(String reference, String title, String description, String dateLimit, List<Critere> criteres)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Créez un objet Tender avec les données de la nouvelle offre
                    var newTender = new Tender
                    {
                        reference = reference,
                        title = title,
                        description = description,
                        critere = criteres,
                        dateLimit = dateLimit
                    };

                    // Convertissez l'objet en JSON
                    var jsonNewTender = JsonSerializer.Serialize(newTender);

                    // Créez un contenu JSON à partir des données de la nouvelle offre
                    var content = new StringContent(jsonNewTender, Encoding.UTF8, "application/json");

                    // Effectuez la requête HTTP POST avec les données JSON
                    HttpResponseMessage response = await client.PostAsync($"{AppConfig.WebServiceUrl}/tender/create", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("L'appel d'offre a été ajouté avec succès !");
                    }
                    else
                    {
                        MessageBox.Show("La création de l'appel d'offre a échoué. Code de statut : " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite lors de la création de l'appel d'offre : " + ex.Message);
            }
        }



        #endregion
    }
}
