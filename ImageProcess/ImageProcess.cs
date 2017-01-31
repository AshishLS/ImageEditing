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
    public partial class ImageProcess : Form
    {
        public ImageProcess()
        {
            InitializeComponent();
        }

        private void btnStartProcess_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(tbxCurrentDirectory.Text) || !Directory.Exists(tbxBaseDirectory.Text))
                return;
            // Initialize.
            prBarFileProcessed.Value = 0;
            lblStatus.Text = "";
            this.BackColor = Color.LightGray;

            // Check how it remembers last paths. Added Settings.
            // https://msdn.microsoft.com/en-us/library/25zf0ze8(v=vs.100).aspx
            // http://stackoverflow.com/questions/453161/best-practice-to-save-application-settings-in-a-windows-forms-application

            Properties.Settings.Default.BaseDir = tbxBaseDirectory.Text;
            Properties.Settings.Default.CurrentDir = tbxCurrentDirectory.Text;
            Properties.Settings.Default.Save();


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

                    // dispose.
                    reducedImage.Dispose();
                    baseImg.Dispose();
                }
                else
                {
                    Console.WriteLine(String.Format("File {0} is not available in base directory.", baseDirImageName));
                }

                // update progress bar
                count++;
                prBarFileProcessed.Value = count;
                lblStatus.Text = String.Format("{0}/{1}", count, prBarFileProcessed.Maximum);
                lblStatus.Update();

                // dispose.
                currentImg.Dispose();
            }

            count = 0;
            this.BackColor = Color.LightSeaGreen;
        }
    }
}
