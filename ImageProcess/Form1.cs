using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace ImageProcess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartProcess_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(tbxCurrentDirectory.Text) || !Directory.Exists(tbxBaseDirectory.Text))
                return;
            // Initialize.
            prBarFileProcessed.Value = 0;

            Directory.CreateDirectory(tbxCurrentDirectory.Text + "\\Results\\");

            var currentDirFiles = Directory.GetFiles(tbxCurrentDirectory.Text);

            prBarFileProcessed.Maximum = currentDirFiles.Count();

            int count = 0;

            foreach (var currentFile in currentDirFiles)
            {
                var fileInfo = new FileInfo(currentFile);
                var currentImg = Image.FromFile(currentFile);

                // Some file might be missing from base and vise versa. Check if the same name file is available.
                var baseDirImageName = tbxBaseDirectory.Text + "\\" + fileInfo.Name;
                if (File.Exists(baseDirImageName))
                {
                    var baseImg = Image.FromFile(baseDirImageName);

                    // make the current image size equal as base file.
                    var reducedImage = currentImg.GetThumbnailImage(baseImg.Width, baseImg.Height, null, IntPtr.Zero);

                    // save the newly generated files in Results Folder.
                    reducedImage.Save(fileInfo.DirectoryName + "\\Results\\" + fileInfo.Name);
                }
                else
                {
                    Console.WriteLine(String.Format("File {0} is not available in base directory.", baseDirImageName));
                }

                // update progress bar
                count++;
                prBarFileProcessed.Value = count;
            }

            count = 0;
        }
    }
}
