using System.Text.Json;

namespace EGouvernance
{
    partial class ListBidder : Form
    {
        public string tenderTitle { get; private set; }
        public string tenderId { get; private set; }

        private List<Bidder> bidderList = new List<Bidder>();


        // List view to display the list of tenders
        private ListView listViewBidders;
        private Panel panelListView;
        private Label lblTender;


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

        public ListBidder(Tender tender)
        {
            tenderTitle = tender.title;
            tenderId = tender.id;

            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.panelListView = new Panel();
            this.listViewBidders = new ListView();

            this.SuspendLayout();

            // Set form size and title
            this.ClientSize = new System.Drawing.Size(900, 450);
            this.Text = "E-gouvernance - Liste des soumissionnaires";

            // Tender's name
            lblTender = new Label();
            lblTender.Text = "Appel d'offre: " + tenderTitle;
            lblTender.Location = new Point(10, 10);
            lblTender.AutoSize = true;
            this.Controls.Add(lblTender);

            // Panel for holding the list view with scroll
            this.panelListView.Location = new System.Drawing.Point(10, 40);
            this.panelListView.Size = new System.Drawing.Size(875, 400);
            this.panelListView.AutoScroll = true;
            this.Controls.Add(this.panelListView);

            // List view for displaying the list of tenders
            this.listViewBidders.Location = new System.Drawing.Point(0, 0);
            this.listViewBidders.Size = new System.Drawing.Size(875, 400);
            this.listViewBidders.View = View.Details;
            this.listViewBidders.FullRowSelect = true;
            this.listViewBidders.GridLines = true;
            this.listViewBidders.Columns.Add("Société", 300);
            this.listViewBidders.Columns.Add("NIF", 200);
            this.listViewBidders.Columns.Add("STAT", 200);
            this.listViewBidders.Columns.Add("Status", 200);
            this.listViewBidders.SelectedIndexChanged += new EventHandler(listViewBidders_SelectedIndexChanged);
            this.panelListView.Controls.Add(this.listViewBidders);

            // Add data to the ListView
            FetchDataAllBidderForTender(tenderId);

            this.listViewBidders.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(listViewBidders_ItemSelectionChanged);

            this.ResumeLayout(false);
        }

        private async void FetchDataAllBidderForTender(String tenderId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string response = await client.GetStringAsync(AppConfig.WebServiceUrl + "/soumission/filter?tender=" + tenderId);

                    // Utilisez System.Text.Json pour désérialiser la réponse JSON
                    List<Bidder> bidderItems = JsonSerializer.Deserialize<List<Bidder>>(response);

                    foreach (var item in bidderItems)
                    {
                        String bidderStatus = "";
                        if (item.status == 0) bidderStatus = "Rejeté";
                        else if (item.status == 1) bidderStatus = "Validé";
                        string[] bidder = { item.society.name, item.society.nif, item.society.stat, bidderStatus, item.id };
                        listViewBidders.Items.Add(new ListViewItem(bidder));
                        bidderList.Add(item);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite lors de la récupération des données : " + ex.Message);
            }
        }

        private void listViewBidders_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listViewBidders.SelectedItems.Count > 0)
            {
                // Get the selected item and show its details in a dialog box or another form if needed
                ListViewItem selectedItem = listViewBidders.SelectedItems[0];
                string name = selectedItem.SubItems[0].Text;
                string nif = selectedItem.SubItems[1].Text;
                string stat = selectedItem.SubItems[2].Text;
                string status = selectedItem.SubItems[3].Text;
            }
        }

        private void listViewBidders_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                // Récupérer les détails de l'élément sélectionné
                ListViewItem selectedItem = e.Item;
                string id = selectedItem.SubItems[4].Text;
                Bidder bidder = new Bidder();

                foreach (var bid in bidderList)
                {
                    if (id == bid.id)
                    {

                        bidder = bid;
                    }
                }

                // Créer une boîte de dialogue personnalisée pour afficher les détails de l'appel d'offre sélectionné
                BidderDetailsDialog detailsDialog = new BidderDetailsDialog(bidder);

                // Afficher la boîte de dialogue
                DialogResult result = detailsDialog.ShowDialog();

            }
        }

        #endregion
    }
}
