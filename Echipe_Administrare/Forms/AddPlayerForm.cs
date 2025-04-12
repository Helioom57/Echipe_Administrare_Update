using Echipe_Administrare.Models;
using Echipe_Administrare.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
 // Adjust if Constants is in another namespace

namespace Echipe_Administrare.Forms
{
    public class AddPlayerForm : Form
    {
        private readonly AdministrareEchipe_Memorie _adminEchipe;
        private ComboBox _comboTeams;
        private TextBox _txtName;
        private DateTimePicker _datePicker;
        private ComboBox _comboPosition;
        private TextBox _txtSalary;

        private Label _lblNameError;
        private Label _lblSalaryError;
        private Label _lblDateError;

        public AddPlayerForm(AdministrareEchipe_Memorie adminEchipe)
        {
            _adminEchipe = adminEchipe;
            this.Text = "Adaugă Jucător";
            this.Size = new Size(350, 320);
            this.StartPosition = FormStartPosition.CenterParent;

            // Controale
            _comboTeams = new ComboBox { Location = new Point(20, 40), Width = 200 };
            _txtName = new TextBox { Location = new Point(20, 80), Width = 200 };
            _datePicker = new DateTimePicker { Location = new Point(20, 120), Width = 200, Value = DateTime.Now.AddYears(-20) };
            _comboPosition = new ComboBox { Location = new Point(20, 160), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            _txtSalary = new TextBox { Location = new Point(20, 200), Width = 200 };

            //Labels
            _lblNameError = new Label { ForeColor = Color.Red, Visible = false, Location = new Point(230, 80), Size = new Size(100, 40) };
            _lblDateError = new Label { ForeColor = Color.Red, Visible = false, Location = new Point(230, 120), Size = new Size(100, 40) };
            _lblSalaryError = new Label { ForeColor = Color.Red, Visible = false, Location = new Point(230, 200), Size = new Size(100, 40) };

            //Combo Boxes
            _comboTeams.Items.AddRange(_adminEchipe.GetEchipe().Select(t => t.NumeEchipa).ToArray());
            if (_comboTeams.Items.Count > 0) _comboTeams.SelectedIndex = 0;

            _comboPosition.Items.AddRange(Enum.GetNames(typeof(PozitieJucator)));
            _comboPosition.SelectedIndex = 0;

            //Adaugare buton
            var btnAdd = new Button { Text = "Adaugă", Location = new Point(20, 240), Size = new Size(100, 30) };

            // Adauagare control pentru form
            this.Controls.AddRange(new Control[]
            {
                new Label { Text = "Echipă:", Location = new Point(20, 20) },
                _comboTeams,
                new Label { Text = "Nume jucător:", Location = new Point(20, 60) },
                _txtName,
                _lblNameError,
                new Label { Text = "Data nașterii:", Location = new Point(20, 100) },
                _datePicker,
                _lblDateError,
                new Label { Text = "Poziție:", Location = new Point(20, 140) },
                _comboPosition,
                new Label { Text = "Salariu (RON):", Location = new Point(20, 180) },
                _txtSalary,
                _lblSalaryError,
                btnAdd
            });

            //Validare
            btnAdd.Click += (s, e) =>
            {
                _lblNameError.Visible = false;
                _lblSalaryError.Visible = false;
                _lblDateError.Visible = false;

                //Validare nume
                if (string.IsNullOrWhiteSpace(_txtName.Text) ||
                    _txtName.Text.Length < Constants.MinNameLength ||
                    _txtName.Text.Length > Constants.MaxNameLength)
                {
                    _lblNameError.Text = $"Numele trebuie între {Constants.MinNameLength} și {Constants.MaxNameLength} caractere!";
                    _lblNameError.Visible = true;
                    return;
                }

                //Validare zi de nastere
                if (_datePicker.Value < Constants.MinBirthDate || _datePicker.Value > Constants.MaxBirthDate)
                {
                    _lblDateError.Text = $"Data între {Constants.MinBirthDate:dd.MM.yyyy} și {Constants.MaxBirthDate:dd.MM.yyyy}";
                    _lblDateError.Visible = true;
                    return;
                }

                //Validare salariu
                if (!decimal.TryParse(_txtSalary.Text, out decimal salary) ||
                    salary < Constants.MinSalary || salary > Constants.MaxSalary)
                {
                    _lblSalaryError.Text = $"Salariul între {Constants.MinSalary} și {Constants.MaxSalary}!";
                    _lblSalaryError.Visible = true;
                    return;
                }

                //Adaugare jucator
                var team = _adminEchipe.GasesteEchipa(_comboTeams.SelectedItem.ToString());
                team.AdaugaJucator(new Jucator(
                    _txtName.Text.Trim(),
                    _datePicker.Value,
                    (PozitieJucator)Enum.Parse(typeof(PozitieJucator), _comboPosition.SelectedItem.ToString()),
                    (double)salary
                ));

                this.DialogResult = DialogResult.OK;
                this.Close();
            };
        }
    }
}
