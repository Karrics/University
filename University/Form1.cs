using System.Windows.Forms;
using UniversityApp;

namespace University
{
    public partial class MainForm : Form
    {
        List<string> cities = new List<string>();
        List<string[]> universities = new List<string[]>();

        public MainForm()
        {
            InitializeComponent();
            LoadData();
            DisplayUniversities();
        }

        private void âûéòèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadData()
        {
            string[] cityLines = File.ReadAllLines("City.txt");
            foreach (string line in cityLines)
            {
                int dotIndex = line.IndexOf('.');
                if (dotIndex != -1)
                {
                    string cityName = line.Substring(dotIndex + 1).Trim();
                    cities.Add(cityName);
                }
            }

            string[] universityLines = File.ReadAllLines("Universities.txt");
            foreach (string line in universityLines)
            {
                universities.Add(line.Split(','));
            }
        }

        private void DisplayUniversities()
        {
            dataGridView1.Rows.Clear();
            foreach (var uni in universities)
            {
                dataGridView1.Rows.Add(uni);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCity = comboBox1.SelectedItem!.ToString()!;
            int cityIndex = cities.IndexOf(selectedCity);

            dataGridView1.Rows.Clear();
            foreach (var uni in universities)
            {
                if (int.Parse(uni[1]) - 1 == cityIndex)
                {
                    dataGridView1.Rows.Add(uni);
                }
            }

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                string selectedUniversity = dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString()!;

                string imagePath = Path.Combine("Images", selectedUniversity!.Replace(".", "") + ".jpg");

                if (File.Exists(imagePath))
                {
                    pictureBox1.Image = Image.FromFile(imagePath);
                }

                textBox1.Text = selectedUniversity;
                textBox2.Text = cities[int.Parse(universities[selectedRowIndex][1]) - 1];
            }
        }

        private void äîáàâèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUniversityForm addUniForm = new AddUniversityForm(cities);
            if (addUniForm.ShowDialog() == DialogResult.OK)
            {
                universities.Add(addUniForm.GetUniversityData());
                DisplayUniversities();
            }
        }

        private void èçìåíèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

                AddUniversityForm editUniForm = new AddUniversityForm(cities);
                editUniForm.LoadUniversityDataForEdit(universities[selectedRowIndex], selectedRowIndex);

                if (editUniForm.ShowDialog() == DialogResult.OK)
                {
                    universities[selectedRowIndex] = editUniForm.GetUniversityData();
                    DisplayUniversities();
                }
            }

        }

        private void óäàëèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                universities.RemoveAt(selectedRowIndex);
                DisplayUniversities();
            }
        }
    }
}

