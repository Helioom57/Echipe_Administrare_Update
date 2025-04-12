using Echipe_Administrare.Models;     
using Echipe_Administrare.Services;   
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Echipe_Administrare.Forms
{
    public partial class MainForm : Form
    {
        private TextBox _txtSearch;
        private Button _btnShowAll;
        private bool _isInSearchMode = false;
        private AdministrareEchipe_Memorie _adminEchipe = new AdministrareEchipe_Memorie();
        private DataGridView _dataGridView;

        public MainForm()
        {
            InitializeComponent();
            SetupUI();
            LoadData();
        }
        private void InitializeComponent()
        {

        }
        private void SetupUI()
        {
            this.Text = "Administrare Echipe de Fotbal";
            this.Size = new Size(900, 600);
            this.BackColor = Color.LightSkyBlue;

            _dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.Navy,
                    ForeColor = Color.White
                }
            };
            _dataGridView.Columns.Add("Echipa", "Echipă");
            _dataGridView.Columns.Add("Jucatori", "Jucători (Nume, Salariu)");

            var btnAddTeam = new Button
            {
                Text = "Adaugă Echipă",
                BackColor = Color.Navy,
                ForeColor = Color.White,
                Size = new Size(150, 40),
                Location = new Point(20, 20)
            };
            btnAddTeam.Click += BtnAddTeam_Click;

            var btnAddPlayer = new Button
            {
                Text = "Adaugă Jucător",
                BackColor = Color.Navy,
                ForeColor = Color.White,
                Size = new Size(150, 40),
                Location = new Point(190, 20)
            };
            btnAddPlayer.Click += BtnAddPlayer_Click;

            var btnTotalSalary = new Button
            {
                Text = "Salariu Total",
                BackColor = Color.Green,
                ForeColor = Color.White,
                Size = new Size(150, 40),
                Location = new Point(360, 20)
            };
            btnTotalSalary.Click += BtnTotalSalary_Click;

            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.LightSteelBlue
            };
            panel.Controls.AddRange(new Control[] { btnAddTeam, btnAddPlayer, btnTotalSalary });

            this.Controls.AddRange(new Control[] { _dataGridView, panel });

            
            _txtSearch = new TextBox
            {
                Text = "Caută jucător...", 
                ForeColor = Color.Gray,
                Location = new Point(550, 20),
                Width = 150
            };

            _txtSearch.GotFocus += (s, e) =>
            {
                if (_txtSearch.Text == "Caută jucător...")
                {
                    _txtSearch.Text = "";
                    _txtSearch.ForeColor = Color.Black;
                }
            };

            _txtSearch.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(_txtSearch.Text))
                {
                    _txtSearch.Text = "Caută jucător...";
                    _txtSearch.ForeColor = Color.Gray;
                }
            };

            
            var btnSearch = new Button
            {
                Text = "Caută",
                Location = new Point(710, 20),
                Size = new Size(80, 23)
            };

            
            _btnShowAll = new Button
            {
                Text = "Arată tot",
                Location = new Point(800, 20),
                Size = new Size(80, 23),
                Visible = false
            };

           
            panel.Controls.AddRange(new Control[] { _txtSearch, btnSearch, _btnShowAll });

            
            btnSearch.Click += (s, e) => ApplySearch(_txtSearch.Text);
            _btnShowAll.Click += (s, e) => ResetSearch();
        }

        private void LoadData()
        {
            _dataGridView.Rows.Clear();
            foreach (var team in _adminEchipe.GetEchipe())
            {
                string playersInfo = string.Join(", ",
                    team.Jucatori.Select(p => $"{p.Nume} ({p.Salariu} RON)"));
                _dataGridView.Rows.Add(team.NumeEchipa, playersInfo);
            }
        }
        private void ApplySearch(string criteriu)
        {
            if (string.IsNullOrWhiteSpace(criteriu)) return;

            var results = _adminEchipe.CautaJucatori(criteriu);
            _dataGridView.Rows.Clear();

            foreach (var (jucator, echipa) in results)
            {
                _dataGridView.Rows.Add(echipa, $"{jucator.Nume} | {jucator.Pozitie} | {jucator.Salariu} RON");
            }

            _isInSearchMode = true;
            _btnShowAll.Visible = true;
            _dataGridView.BackgroundColor = results.Count == 0 ? Color.FromArgb(255, 240, 240) : Color.White;
        }

        private void ResetSearch()
        {
            _txtSearch.Text = "";
            _isInSearchMode = false;
            _btnShowAll.Visible = false;
            LoadData();
        }

        private void BtnAddTeam_Click(object sender, EventArgs e)
        {
            using (var form = new AddTeamForm())
            {
                if (form.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(form.TeamName))
                {
                    _adminEchipe.AdaugaEchipa(new Echipa(form.TeamName));
                    LoadData();
                }
            }
        }

        private void BtnAddPlayer_Click(object sender, EventArgs e)
        {
            if (_adminEchipe.GetEchipe().Count == 0)
            {
                MessageBox.Show("Nu există echipe! Adaugă o echipă mai întâi.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new AddPlayerForm(_adminEchipe))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }

        private void BtnTotalSalary_Click(object sender, EventArgs e)
        {
            if (_dataGridView.SelectedRows.Count > 0)
            {
                string teamName = _dataGridView.SelectedRows[0].Cells["Echipa"].Value.ToString();
                var team = _adminEchipe.GasesteEchipa(teamName);
                MessageBox.Show($"Salariul total pentru {teamName}: {team.SalariulTotalEchipei()} RON", "Salariu Total");
            }
            else
            {
                MessageBox.Show("Selectează o echipă din tabel!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _adminEchipe.SaveData();
            base.OnFormClosing(e);
        }

    }
}