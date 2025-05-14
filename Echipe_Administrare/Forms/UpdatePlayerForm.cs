using Echipe_Administrare.Models;
using Echipe_Administrare.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Echipe_Administrare.Forms
{
    public partial class UpdatePlayerForm : Form
    {
        private readonly AdministrareEchipe_Memorie _adminEchipe;
        private readonly Echipa _originalTeam;
        private readonly Jucator _player;
        private ComboBox _comboTeams;
        private TextBox _txtName;
        private DateTimePicker _datePicker;
        private ComboBox _comboPosition;
        private TextBox _txtSalary;


        public UpdatePlayerForm(AdministrareEchipe_Memorie adminEchipe, Echipa originalTeam, Jucator player)
        {
            _adminEchipe = adminEchipe;
            _originalTeam = originalTeam;
            _player = player;

            InitializeForm();
            LoadPlayerData();
        }

        private void InitializeForm()
        {
            this.Text = "Actualizare Jucător";
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterParent;


            _comboTeams = new ComboBox
            {
                Location = new Point(20, 40),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _comboTeams.Items.AddRange(_adminEchipe.GetEchipe().Select(t => t.NumeEchipa).ToArray());


            _txtName = new TextBox { Location = new Point(20, 80), Width = 200 };


            _datePicker = new DateTimePicker { Location = new Point(20, 120), Width = 200 };


            _comboPosition = new ComboBox
            {
                Location = new Point(20, 160),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _comboPosition.Items.AddRange(Enum.GetNames(typeof(PozitieJucator)));


            _txtSalary = new TextBox { Location = new Point(20, 200), Width = 200 };

            // BButoane
            var btnUpdate = new Button
            {
                Text = "Actualizează",
                Location = new Point(20, 240),
                Size = new Size(100, 30)
            };
            btnUpdate.Click += BtnUpdate_Click;

            var btnCancel = new Button
            {
                Text = "Anulează",
                Location = new Point(130, 240),
                Size = new Size(100, 30),
                DialogResult = DialogResult.Cancel
            };

            // Controale
            this.Controls.AddRange(new Control[]
            {
                new Label { Text = "Echipă:", Location = new Point(20, 20) },
                _comboTeams,
                new Label { Text = "Nume jucător:", Location = new Point(20, 60) },
                _txtName,
                new Label { Text = "Data nașterii:", Location = new Point(20, 100) },
                _datePicker,
                new Label { Text = "Poziție:", Location = new Point(20, 140) },
                _comboPosition,
                new Label { Text = "Salariu (RON):", Location = new Point(20, 180) },
                _txtSalary,
                btnUpdate,
                btnCancel
            });
        }

        private void LoadPlayerData()
        {
            _comboTeams.SelectedItem = _originalTeam.NumeEchipa;
            _txtName.Text = _player.Nume;
            _datePicker.Value = _player.DataNasterii;
            _comboPosition.SelectedItem = _player.Pozitie.ToString();
            _txtSalary.Text = _player.Salariu.ToString();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Validare
            if (string.IsNullOrWhiteSpace(_txtName.Text))
            {
                MessageBox.Show("Introduceți un nume valid!", "Eroare",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(_txtSalary.Text, out double salary) || salary <= 0)
            {
                MessageBox.Show("Introduceți un salariu valid!", "Eroare",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get echipa selectata
            var selectedTeam = _adminEchipe.GasesteEchipa(_comboTeams.SelectedItem.ToString());

            // Actualizeaza proprietati jucator
            _player.Nume = _txtName.Text;
            _player.DataNasterii = _datePicker.Value;
            _player.Pozitie = (PozitieJucator)Enum.Parse(typeof(PozitieJucator), _comboPosition.SelectedItem.ToString());
            _player.Salariu = salary;

            // Daca echipa se schimba, muta jucatorul catre echipa noua
            if (selectedTeam != _originalTeam)
            {
                _originalTeam.EliminaJucator(_player);
                selectedTeam.AdaugaJucator(_player);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}