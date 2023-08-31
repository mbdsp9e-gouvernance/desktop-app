namespace EGouvernance;

using System.Text;
using System.Text.Json;


public class BidderDetailsDialog : Form
{
    // Propriétés publiques pour les détails de l'appel d'offre
    public string name { get; private set; }
    public string nif { get; private set; }
    public string stat { get; private set; }

    private string bidderId;

    private Tender tender;
    private Society society;

    // Contrôles pour les détails de l'appel d'offre
    private Label lblName;
    private Label lblNif;
    private Label lblStat;
    private ListView listViewDocs;
    private Panel panelListView;
    private Button btnDownload;
    private Button btnDecline;
    private Button btnAccept;
    public BidderDetailsDialog(Bidder bidder)
    {
        // Initialiser les propriétés
        name = bidder.society.name;
        nif = bidder.society.nif;
        stat = bidder.society.stat;
        bidderId = bidder.id;
        tender = bidder.tender;
        society = bidder.society;

        // Initialiser les contrôles de la boîte de dialogue
        InitializeComponent(name, nif, stat);

        // Ajuster la taille de la boîte de dialogue en fonction du contenu
        this.ClientSize = new System.Drawing.Size(700, 300);

    }

    private void InitializeComponent(string name, string nif, string stat)
    {
        this.panelListView = new Panel();
        this.listViewDocs = new ListView();

        this.SuspendLayout();


        // Ajouter le bouton "download"
        this.btnDownload = new Button();
        this.btnDownload.Location = new System.Drawing.Point(10, 265);
        this.btnDownload.Size = new System.Drawing.Size(200, 25);
        this.btnDownload.Text = "Télécharger les documents";
        this.btnDownload.Click += new EventHandler(btnDownload_Click);
        this.Controls.Add(this.btnDownload);

        // Ajouter le bouton "Rejeter"
        this.btnDecline = new Button();
        this.btnDecline.Location = new System.Drawing.Point(480, 265);
        this.btnDecline.Size = new System.Drawing.Size(100, 25);
        this.btnDecline.Text = "Rejeter";
        this.btnDecline.Click += new EventHandler(btnDecline_Click);
        this.Controls.Add(this.btnDecline);

        // Ajouter le bouton "valider"
        this.btnAccept = new Button();
        this.btnAccept.Location = new System.Drawing.Point(590, 265);
        this.btnAccept.Size = new System.Drawing.Size(100, 25);
        this.btnAccept.Text = "Valider";
        this.btnAccept.Click += new EventHandler(btnAccepts_Click);
        this.Controls.Add(this.btnAccept);

        InitializeDialogContent(name, nif, stat);

        // Panel for holding the list view with scroll
        this.panelListView.Location = new System.Drawing.Point(10, 70);
        this.panelListView.Size = new System.Drawing.Size(680, 180);
        this.panelListView.AutoScroll = true;
        this.Controls.Add(this.panelListView);

        // List view for displaying the list of tenders
        this.listViewDocs.Location = new System.Drawing.Point(0, 0);
        this.listViewDocs.Size = new System.Drawing.Size(680, 180);
        this.listViewDocs.View = View.Details;
        this.listViewDocs.FullRowSelect = true;
        this.listViewDocs.GridLines = true;
        this.listViewDocs.Columns.Add("Document", 650);
        // this.listViewDocs.SelectedIndexChanged += new EventHandler(listViewBidders_SelectedIndexChanged);
        this.panelListView.Controls.Add(this.listViewDocs);

        this.FetchBidderDetails(society.id);

        this.ResumeLayout(false);

    }


    private async void FetchBidderDetails(String societyId)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(AppConfig.WebServiceUrl + "/soumission/filter?society=" + societyId);

                // Utilisez System.Text.Json pour désérialiser la réponse JSON
                List<Bidder> bidderDetailsItems = JsonSerializer.Deserialize<List<Bidder>>(response);

                foreach (var item in bidderDetailsItems)
                {
                    string[] doc = { "doc(" + item.dateSoumission + ")" + ".pdf" };
                    listViewDocs.Items.Add(new ListViewItem(doc));
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Une erreur s'est produite lors de la récupération des données : " + ex.Message);
        }
    }

    private void InitializeDialogContent(string name, string nif, string stat)
    {
        this.Text = "E-Gouvernance - Détails de l'appel d'offre";

        // Labels pour afficher les détails du soumissionaire
        lblName = new Label();
        lblName.Text = "Société: " + name;
        lblName.Location = new Point(10, 10);
        lblName.AutoSize = true;
        this.Controls.Add(lblName);

        lblNif = new Label();
        lblNif.Text = "NIF: " + nif;
        lblNif.Location = new Point(10, lblName.Bottom + 5); // Position en dessous du label "name"
        lblNif.AutoSize = true;
        this.Controls.Add(lblNif);

        lblStat = new Label();
        lblStat.Text = "STAT: " + stat;
        lblStat.Location = new Point(10, lblNif.Bottom + 5); // Position en dessous du label "nif"
        lblStat.AutoSize = true;
        this.Controls.Add(lblStat);

    }

    private async void bidderValidation(String statusUpdate, String message)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                // Créez un objet JSON pour la mise à jour de statut
                var updateData = new { status = statusUpdate };

                // Convertissez l'objet en JSON
                var jsonUpdateData = JsonSerializer.Serialize(updateData);

                // Créez un contenu JSON à partir des données mises à jour
                var content = new StringContent(jsonUpdateData, Encoding.UTF8, "application/json");

                // Effectuez la requête HTTP POST avec les données JSON
                HttpResponseMessage response = await client.PutAsync($"{AppConfig.WebServiceUrl}/soumission/updateValidation?id={bidderId}", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(message);

                }
                else
                {
                    MessageBox.Show("La mise à jour a échoué. Code de statut : " + response.StatusCode);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Une erreur s'est produite lors de la mise à jour : " + ex.Message);
        }
    }

    private async void updateStatusTender(int status, string message)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                var updatedTender = new Tender
                {
                    reference = tender.reference,
                    title = tender.title,
                    description = tender.description,
                    dateLimit = tender.dateLimit.ToString(),
                    critere = tender.critere,
                    tenderStatus = status
                };

                var jsonUpdatedTender = JsonSerializer.Serialize(updatedTender);
                var content = new StringContent(jsonUpdatedTender, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"{AppConfig.WebServiceUrl}/tender/update?id=" + tender.id, content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(message);
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
    }

    private async void btnDecline_Click(object sender, EventArgs e)
    {
        bidderValidation("0", "Soumission rejeté");

    }

    private void btnAccepts_Click(object sender, EventArgs e)
    {
        bidderValidation("1", "Soumission validé");
        updateStatusTender(1, "Status de l'appel d'offre a été mis à jour en évaluation en cours avec succès !");
    }

    private void btnDownload_Click(object sender, EventArgs e)
    {

    }
}
