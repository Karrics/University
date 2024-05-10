using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using University;

namespace UniversityApp
{
    public partial class AddUniversityForm : Form
    {
        private int editIndex = -1;

        private List<string> cities;

        public AddUniversityForm(List<string> cities)
        {
            InitializeComponent();
            this.cities = cities;
            comboBox1.Items.AddRange(cities.ToArray());
        }
        public void LoadUniversityDataForEdit(string[] universityData, int index)
        {
            editIndex = index;

            comboBox1.SelectedItem = cities[int.Parse(universityData[1])];
            textBox1.Text = universityData[0];
        }

        public string[] GetUniversityData()
        {
            string? selectedCity = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "";
            string universityName = textBox1.Text.Trim();

            string[] universityData = new string[2];
            universityData[0] = universityName;

            if (!string.IsNullOrEmpty(selectedCity))
            {
                int cityIndex = cities.IndexOf(selectedCity);
                if (cityIndex >= 0)
                {
                    universityData[1] = cityIndex.ToString();
                }
            }

            return universityData;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
