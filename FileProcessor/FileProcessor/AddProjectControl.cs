using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MacrosAndIncludesInspector;

namespace FileProcessor
{
    public partial class AddProjectControl : UserControl
    {
        string selectedPath;

        public AddProjectControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openProjectDir = new FolderBrowserDialog();
            DialogResult result = openProjectDir.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openProjectDir.SelectedPath))
            {
                button1.Text = openProjectDir.SelectedPath;
                this.selectedPath = openProjectDir.SelectedPath;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedPath))
            {
                MacrosExtractAPI api = new MacrosExtractAPI();
                api.ExtractFromProject(this.selectedPath);
                MessageBox.Show("The JSON file was saved to your Desktop");
            }
        }
    }
}
