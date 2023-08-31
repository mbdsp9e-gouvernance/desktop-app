using System.Text.Json;

namespace EGouvernance
{
    partial class ListTender : Form
    {
        // List view to display the list of tenders
        private ListView listViewTenders;
        private Panel panelListView;
        private List<Tender> tenderList = new List<Tender>();

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
            this.panelListView = new Panel();
            this.listViewTenders = new ListView();

            this.SuspendLayout();

            // Set form size and title
            this.ClientSize = new System.Drawing.Size(1000, 450);
            this.Text = "E-gouvernance - Liste appel d'offre";

            // Panel for holding the list view with scroll
            this.panelListView.Location = new System.Drawing.Point(10, 10);
            this.panelListView.Size = new System.Drawing.Size(975, 400);
            this.panelListView.AutoScroll = true;
            this.Controls.Add(this.panelListView);

            // List view for displaying the list of tenders
            this.listViewTenders.Location = new System.Drawing.Point(0, 0);
            this.listViewTenders.Size = new System.Drawing.Size(975, 400);
            this.listViewTenders.View = View.Details;
            this.listViewTenders.FullRowSelect = true;
            this.listViewTenders.GridLines = true;
            this.listViewTenders.Columns.Add("Référence", 120);
            this.listViewTenders.Columns.Add("Titre", 200);
            this.listViewTenders.Columns.Add("Description", 300);
            this.listViewTenders.Columns.Add("Date d'émission", 120);
            this.listViewTenders.Columns.Add("Date limite", 120);
            this.listViewTenders.Columns.Add("Status", 150);
            this.listViewTenders.SelectedIndexChanged += new EventHandler(listViewTenders_SelectedIndexChanged);
            this.panelListView.Controls.Add(this.listViewTenders);

            // Add data to the ListView
            FetchDataAllTender();

            this.listViewTenders.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(listViewTenders_ItemSelectionChanged);

            this.ResumeLayout(false);
        }
        private async void FetchDataAllTender()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string response = await client.GetStringAsync(AppConfig.WebServiceUrl + "/tender/filter");

                    // Utilisez System.Text.Json pour désérialiser la réponse JSON
                    List<Tender> tenderItems = JsonSerializer.Deserialize<List<Tender>>(response);

                    foreach (var item in tenderItems)
                    {
                        string status = "";
                        if (item.tenderStatus == 0) status = "Ouvert";
                        else if (item.tenderStatus == 1) status = "Evaluation en cours";
                        else if (item.tenderStatus == 2) status = "Clôturé";
                        string[] tender = { item.reference, item.title, item.description, item.dateEmission, item.dateLimit, status };
                        listViewTenders.Items.Add(new ListViewItem(tender));
                        tenderList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite lors de la récupération des données : " + ex.Message);
            }
        }

        private void listViewTenders_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listViewTenders.SelectedItems.Count > 0)
            {
                // Get the selected item and show its details in a dialog box or another form if needed
                ListViewItem selectedItem = listViewTenders.SelectedItems[0];
                string reference = selectedItem.SubItems[0].Text;
                string titre = selectedItem.SubItems[1].Text;
                string description = selectedItem.SubItems[2].Text;
                string dateEmission = selectedItem.SubItems[3].Text;
                string dateLimite = selectedItem.SubItems[4].Text;
                string tenderStatus = selectedItem.SubItems[5].Text;
            }
        }

        private void listViewTenders_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                // Récupérer les détails de l'élément sélectionné
                ListViewItem selectedItem = e.Item;
                string reference = selectedItem.SubItems[0].Text;
                string status = selectedItem.SubItems[5].Text;

                Tender tender = new Tender();
                // Charger la liste des critères associée à cet appel d'offre (vous devez implémenter cette partie)
                List<Critere> critereList = new List<Critere>();

                foreach (var t in tenderList)
                {
                    if (reference == t.reference)
                    {
                        tender = t;
                    }
                }

                // Créer une boîte de dialogue personnalisée pour afficher les détails de l'appel d'offre sélectionné
                TenderDetailsDialog detailsDialog = new TenderDetailsDialog(tender, status);

                // Afficher la boîte de dialogue
                DialogResult result = detailsDialog.ShowDialog();

            }
        }

        #endregion
    }
}
