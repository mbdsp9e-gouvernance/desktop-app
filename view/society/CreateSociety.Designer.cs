// using System;
// using System.Windows.Forms;

// namespace EGouvernance
// {
//     partial class CreateSociety : Form
//     {
//         // Input fields for creating a tender
//         private Label lblName;
//         private Label lblNif;
      
//         private TextBox txtName;
//         private TextBox txtNif;
       
//         private Button btnAjouter;


//         /// <summary>
//         ///  Required designer variable.
//         /// </summary>
//         private System.ComponentModel.IContainer components = null;

//         /// <summary>
//         ///  Clean up any resources being used.
//         /// </summary>
//         /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//         protected override void Dispose(bool disposing)
//         {
//             if (disposing && (components != null))
//             {
//                 components.Dispose();
//             }
//             base.Dispose(disposing);
//         }

//         #region Windows Form Designer generated code

//         private void InitializeComponent()
//         {
//             this.lblName = new Label();
//             this.lblNif = new Label();
    
//             this.txtName = new TextBox();
//             this.txtNif = new TextBox();
        


//             this.SuspendLayout();

//             // Set form size and title
//             this.ClientSize = new System.Drawing.Size(800, 450);
//             this.Text = "E-gouvernance - Création societe";

//             // Label for reference
//             this.lblName.Location = new System.Drawing.Point(10, 10);
//             this.lblName.Size = new System.Drawing.Size(100, 20);
//             this.lblName.Text = "Référence:";
//             this.Controls.Add(this.lblName);

//             // Input text for reference
//             this.txtName.Location = new System.Drawing.Point(120, 10);
//             this.txtName.Size = new System.Drawing.Size(200, 20);
//             this.Controls.Add(this.txtName);

//             // Label for titre
//             this.lblNif.Location = new System.Drawing.Point(10, 40);
//             this.lblNif.Size = new System.Drawing.Size(100, 20);
//             this.lblNif.Text = "Titre:";
//             this.Controls.Add(this.lblNif);

//             // Input text for titre
//             this.txtNif.Location = new System.Drawing.Point(120, 40);
//             this.txtNif.Size = new System.Drawing.Size(400, 20);
//             this.Controls.Add(this.txtNif);

//             // Label for description
//             this.lblDescription.Location = new System.Drawing.Point(10, 70);
//             this.lblDescription.Size = new System.Drawing.Size(100, 20);
//             this.lblDescription.Text = "Description:";
//             this.Controls.Add(this.lblDescription);

//             // Input text area for description
//             this.txtDescription.Location = new System.Drawing.Point(120, 70);
//             this.txtDescription.Multiline = true;
//             this.txtDescription.Size = new System.Drawing.Size(400, 200);
//             this.Controls.Add(this.txtDescription);

//             // Label for date d'emission
//             this.lblDateEmission.Location = new System.Drawing.Point(10, 280);
//             this.lblDateEmission.Size = new System.Drawing.Size(100, 20);
//             this.lblDateEmission.Text = "Emission:";
//             this.Controls.Add(this.lblDateEmission);

//             // Input date for date d'emission
//             this.dateEmission.Location = new System.Drawing.Point(120, 280);
//             this.dateEmission.Size = new System.Drawing.Size(300, 20);
//             this.Controls.Add(this.dateEmission);

//             // Label for date limite
//             this.lblDateLimite.Location = new System.Drawing.Point(10, 310);
//             this.lblDateLimite.Size = new System.Drawing.Size(100, 20);
//             this.lblDateLimite.Text = "Limite:";
//             this.Controls.Add(this.lblDateLimite);

//             // Input date for date limite
//             this.dateLimite.Location = new System.Drawing.Point(120, 310);
//             this.dateLimite.Size = new System.Drawing.Size(300, 20);
//             this.Controls.Add(this.dateLimite);

//             // Bouton pour ajouter une pièce jointe
//             btnAjouterPieceJointe = new Button();
//             btnAjouterPieceJointe.Location = new System.Drawing.Point(600, 100);
//             btnAjouterPieceJointe.Size = new System.Drawing.Size(150, 75);
//             btnAjouterPieceJointe.Text = "Ajouter pièce jointe";
//             btnAjouterPieceJointe.Click += new EventHandler(btnAjouterPieceJointe_Click);
//             this.Controls.Add(btnAjouterPieceJointe);

//             // Label pour afficher le nom du fichier joint
//             this.lblPieceJointe.Location = new System.Drawing.Point(140, 255);
//             this.lblPieceJointe.AutoSize = true;
//             this.Controls.Add(this.lblPieceJointe);

//             // Bouton "Ajouter"
//             btnAjouter = new Button();
//             btnAjouter.Location = new System.Drawing.Point(200, 360); // Position du bouton "Ajouter"
//             btnAjouter.Size = new System.Drawing.Size(120, 30);
//             btnAjouter.Text = "Ajouter";
//             btnAjouter.Click += new EventHandler(btnAjouter_Click);
//             this.Controls.Add(btnAjouter);

//             this.ResumeLayout(false);
//         }
//         private void btnAjouterPieceJointe_Click(object sender, EventArgs e)
//         {
//             // Ouvrir une boîte de dialogue pour sélectionner un fichier
//             using (OpenFileDialog openFileDialog = new OpenFileDialog())
//             {
//                 // Filtre pour les types de fichiers autorisés (vous pouvez le personnaliser selon vos besoins)
//                 openFileDialog.Filter = "Fichiers PDF (*.pdf)|*.pdf|Tous les fichiers (*.*)|*.*";
//                 openFileDialog.FilterIndex = 1;
//                 openFileDialog.RestoreDirectory = true;

//                 // Afficher la boîte de dialogue et attendre que l'utilisateur sélectionne un fichier
//                 if (openFileDialog.ShowDialog() == DialogResult.OK)
//                 {
//                     // Récupérer le nom du fichier sélectionné
//                     string fileName = openFileDialog.FileName;

//                     // Afficher le nom du fichier dans le label
//                     lblPieceJointe.Text = "Pièce jointe : " + fileName;
//                 }
//             }
//         }
//         private void btnAjouter_Click(object sender, EventArgs e)
//         {
//             // Récupérer les valeurs saisies dans les champs
//             string reference = txtName.Text;
//             string titre = txtNif.Text;
//             string description = txtDescription.Text;
//             string dateEmissionValue = dateEmission.Value.ToString("yyyy-MM-dd");
//             string dateLimiteValue = dateLimite.Value.ToString("yyyy-MM-dd");

//             // code save 

//             // Exemple d'affichage d'un message de succès
//             MessageBox.Show("L'appel d'offre a été ajouté avec succès !");

//             // Réinitialisation des champs après l'ajout
//             // txtName.Clear();
//             // txtNif.Clear();
//             // txtDescription.Clear();
//             // dateEmission.Value = DateTime.Today;
//             // dateLimite.Value = DateTime.Today;

//             // Réinitialiser le label pour le nom du fichier joint
//             lblPieceJointe.Text = "Pièce jointe : Aucun fichier sélectionné";
//         }

//         #endregion
//     }
// }
