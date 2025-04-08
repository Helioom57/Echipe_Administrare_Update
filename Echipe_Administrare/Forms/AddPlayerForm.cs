using Echipe_Administrare.Models;    
using Echipe_Administrare.Services;   
using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

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

        public AddPlayerForm(AdministrareEchipe_Memorie adminEchipe)
        {
            _adminEchipe = adminEchipe;
            this.Text = "Adaugă Jucător";
            this.Size = new Size(350, 300);

            _comboTeams = new ComboBox { Location = new Point(20, 40), Width = 200 };
            _comboTeams.Items.AddRange(_adminEchipe.GetEchipe().Select(t => t.NumeEchipa).ToArray());
            if (_comboTeams.Items.Count > 0) _comboTeams.SelectedIndex = 0;

            _txtName = new TextBox { Location = new Point(20, 80), Width = 200 };
            _txtName.Text = "Nume";
            _txtName.GotFocus += (s, e) => { if (_txtName.Text == "Nume") _txtName.Text = ""; };
            _txtName.LostFocus += (s, e) => { if (string.IsNullOrWhiteSpace(_txtName.Text)) _txtName.Text = "Nume"; };
            _datePicker = new DateTimePicker { Location = new Point(20, 120), Width = 200 };
            _comboPosition = new ComboBox { Location = new Point(20, 160), Width = 200 };
            _comboPosition.Items.AddRange(Enum.GetNames(typeof(PozitieJucator)));
            _comboPosition.SelectedIndex = 0;
            _txtSalary = new TextBox { Location = new Point(20, 200), Width = 200 };
            _txtSalary.GotFocus += (s, e) => { if (_txtSalary.Text == "Salariu (RON)") _txtSalary.Text = ""; };
            _txtSalary.LostFocus += (s, e) => { if (string.IsNullOrWhiteSpace(_txtSalary.Text)) _txtSalary.Text = "Salariu (RON)"; };
            _txtSalary.Text = "Salariu (RON)";

            var btnAdd = new Button { Text = "Adaugă", DialogResult = DialogResult.OK, Location = new Point(20, 240) };

            this.Controls.AddRange(new Control[]
            {
                new Label { Text = "Echipă:", Location = new Point(20, 20) },
                _comboTeams,
                new Label { Text = "Nume jucător:", Location = new Point(20, 60) },
                _txtName,
                new Label { Text = "Data nașterii:", Location = new Point(20, 100) },
                _datePicker,
                new Label { Text = "Pozitie:", Location = new Point(20, 140) },
                _comboPosition,
                new Label { Text = "Salariu:", Location = new Point(20, 180) },
                _txtSalary,
                btnAdd
            });

            btnAdd.Click += (s, e) =>
            {
                if (double.TryParse(_txtSalary.Text, out double salary))
                {
                    var team = _adminEchipe.GasesteEchipa(_comboTeams.SelectedItem.ToString());
                    team.AdaugaJucator(new Jucator(
                        _txtName.Text,
                        _datePicker.Value,
                        (PozitieJucator)Enum.Parse(typeof(PozitieJucator), _comboPosition.SelectedItem.ToString()),
                        salary
                    ));
                }
            };
        }
    }
}