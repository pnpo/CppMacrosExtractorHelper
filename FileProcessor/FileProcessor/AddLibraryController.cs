using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using FileProcessor.Processor;
using System.IO;

namespace FileProcessor
{
    public partial class AddLibraryController : UserControl
    {
        private string libPath = string.Empty;

        public AddLibraryController()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openLibrary = new OpenFileDialog();
            if(openLibrary.ShowDialog() == DialogResult.OK)
            {
                libPath = openLibrary.FileName;
                button1.Text = libPath;
            }

            if(Path.GetExtension(libPath) != ".h")
            {
                libPath = string.Empty;
                MessageBox.Show("The extension provided is not valid. Provide a .h file.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(libPath != string.Empty && libraryName.Text != string.Empty)
            {
                string fileContent = System.IO.File.ReadAllText(libPath);
                CppPreProcessorHandler cppPreprocessor = new CppPreProcessorHandler(fileContent, libraryName.Text);
                MessageBox.Show("The JSON file was saved to your Desktop");
            }
            else
            {
                MessageBox.Show("Please provide a .h file and the name of the library.");
            }
            
        }
    }
}
