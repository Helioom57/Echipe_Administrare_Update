using Echipe_Administrare.Models;     
using System.Windows.Forms;
using System.Drawing;

namespace Echipe_Administrare.Forms
{
    public class AddTeamForm : Form
    {
        public string TeamName { get; private set; }

        public AddTeamForm()
        {
            this.Text = "Adaugă Echipă";
            this.Size = new Size(300, 150);

            var txtName = new TextBox { Location = new Point(20, 40), Width = 200 };
            var btnAdd = new Button { Text = "Adaugă", DialogResult = DialogResult.OK, Location = new Point(20, 80) };

            this.Controls.Add(new Label { Text = "Nume echipă:", Location = new Point(20, 20) });
            this.Controls.Add(txtName);
            this.Controls.Add(btnAdd);

            btnAdd.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Numele echipei nu poate fi gol!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                TeamName = txtName.Text;
                this.DialogResult = DialogResult.OK;
            };
        }
    }
}