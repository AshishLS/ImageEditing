﻿using System;
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
using EmguOpenCVMagic;
using Newtonsoft.Json;

namespace ImageProcess
{
    public partial class ImageProcess : Form
    {
        string comparisonDatafile;

        public struct MatchInfo
        {
            public double Percetange;
            public string ComparisonImagePath;
            public int Index;
            public override string ToString()
            {
                return String.Format("Similarity by = {0}% \n Image Index - {1} \nPath - {2}", Percetange, Index, ComparisonImagePath);
            }
        }
        public ImageProcess()
        {
            InitializeComponent();
        }

        private void btnResizeCurrentImages_Click(object sender, EventArgs e)
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

            // Since user has chosen to resize images, match with resized images.
        }

        private void btnMatch_Click(object sender, EventArgs e)
        {
            // Test matches
            MatchImages();
        }

        public void MatchImages()
        {
            // Now the Results directory in current folder ready. Let's check for similarity.
            // Initialize.
            prBarFileProcessed.Value = 0;
            lblStatus.Text = "";
            this.BackColor = Color.Yellow;

            DirectoryInfo sampleImgsDirInfo = null;
            if(cbxMatchWithResized.Checked)
                sampleImgsDirInfo = Directory.CreateDirectory(tbxCurrentDirectory.Text + "\\Results");
            else
                sampleImgsDirInfo = Directory.CreateDirectory(tbxCurrentDirectory.Text);

            var analysedImgsDirInfo = Directory.CreateDirectory(tbxCurrentDirectory.Text + "\\Analyzed");
            // Also create the comparison data file which we can use later.
            comparisonDatafile = String.Format("{0}\\ComparisonData{1}.txt",analysedImgsDirInfo.FullName, DateTime.Now.ToFileTimeUtc());

            var baseDirFiles = Directory.GetFiles(tbxBaseDirectory.Text);

            prBarFileProcessed.Maximum = baseDirFiles.Count();

            int count = 0;

            // Debug sample random file from dataset.
            List<int> chosenIndices = new List<int>();
            Random randomGen = new Random(baseDirFiles.Count() - 1);
            for (int i = 0; i < 10; i++) // take 100 samples.
            {
                chosenIndices.Add(randomGen.Next(0, baseDirFiles.Count()));
            }

            foreach (var baseLineFile in baseDirFiles)
            {
                // Debug - Take only those images generated by Random gen
                if (chosenIndices.Contains(count))
                {

                    var baseFileInfo = new FileInfo(baseLineFile);
                    var sampleDirImageName = sampleImgsDirInfo.FullName + "\\" + baseFileInfo.Name;

                    if (File.Exists(sampleDirImageName))
                    {
                        // Testing-
                        Bitmap resultImg;
                        double percentage = EmguOpenCVMagic.DrawMatches.MatchImages(sampleDirImageName, baseLineFile, out resultImg);
                        if (resultImg != null)
                        {
                            percentage = Math.Round(percentage, 2);

                            var analysedImagePath = analysedImgsDirInfo.FullName + "\\" + baseFileInfo.Name;
                            resultImg.Save(analysedImagePath);

                            // Let the treenode know where to look
                            // Save the info in Tag as a JSON.
                            var matchInfo = new MatchInfo();
                            matchInfo.Percetange = percentage;
                            matchInfo.ComparisonImagePath = analysedImagePath;
                            matchInfo.Index = count;

                            // Save this info for later reference.
                            SaveInComparisonDataFile(matchInfo);
                            resultImg.Dispose();
                        }

                    }
                    else
                    {
                        Console.WriteLine(String.Format("File {0} is not available in base directory.", sampleDirImageName));
                    }
                }

                // update progress bar
                count++;
                prBarFileProcessed.Value = count;
                lblStatus.Text = String.Format("comparing \n {0}/{1}", count, prBarFileProcessed.Maximum);
                lblStatus.Update();
            }

            count = 0;
            this.BackColor = Color.LightSeaGreen;
            ShowDataInComparisonFileAsTreeView();

        }

        private void SaveInComparisonDataFile(MatchInfo matchInfo)
        {
            StreamWriter filestream = null;
            if (!File.Exists(comparisonDatafile))
            {
                filestream = File.CreateText(comparisonDatafile);
            }
            else
            {
                filestream = File.AppendText(comparisonDatafile);
            }
            filestream.WriteLine(JsonConvert.SerializeObject(matchInfo));
            filestream.Close();
        }

        private void ShowDataInComparisonFileAsTreeView()
        {
            if (!File.Exists(comparisonDatafile))
            {
                MessageBox.Show("Invalid Comparison Data File.");
                return;
            }
            // Clean the existing tree first
            trvPercentageDivision.Nodes.Clear();

            StreamReader readerStream = File.OpenText(comparisonDatafile);

            while (!readerStream.EndOfStream)
            {
                var stringJsonInfo = readerStream.ReadLine();
                var matchInfo = JsonConvert.DeserializeObject<MatchInfo>(stringJsonInfo);
                // Add it in the tree.
                FileInfo fileInfo = new FileInfo(matchInfo.ComparisonImagePath);
                var treeNode = trvPercentageDivision.Nodes.Add(fileInfo.Name);
                var color = getColorBasedOnPercentage(matchInfo.Percetange);
                treeNode.BackColor = color;
                // Add the matchInfo as a Tag on the tree node as well.
                treeNode.Tag = matchInfo;
            }
            readerStream.Close();
        }

        private Color getColorBasedOnPercentage(double percentage)
        {
            Color returnColor = Color.Green;
            //if (percentage >= 80.0)
            //    returnColor = Color.Red;
            //else if (percentage >= 60.0)
            //    returnColor = Color.Orange;
            //else if (percentage >= 40.0)
            //    returnColor = Color.Yellow;
            //else if (percentage >= 20.0)
            //    returnColor = Color.GreenYellow;
            //else if (percentage >= 0.0)
            //    returnColor = Color.ForestGreen;

            // When match points are compared against key points.
            //if (percentage >= 30.0)
            //    returnColor = Color.ForestGreen;
            //else if (percentage >= 25.0)
            //    returnColor = Color.Green;
            //else if (percentage >= 20.0)
            //    returnColor = Color.GreenYellow;
            //else if (percentage >= 15.0)
            //    returnColor = Color.Yellow;
            //else if (percentage >= 5.0)
            //    returnColor = Color.Orange;
            //else if (percentage >= 1.0)
            //    returnColor = Color.Red;
            //else
            //    returnColor = Color.DarkRed;

            // When rectangle diagonal distance compared
            if (percentage >= 98.0)
                returnColor = Color.ForestGreen;
            else if (percentage >= 95.0)
                returnColor = Color.Green;
            else if (percentage >= 90.0)
                returnColor = Color.GreenYellow;
            else if (percentage >= 80.0)
                returnColor = Color.Yellow;
            else if (percentage >= 60.0)
                returnColor = Color.Orange;
            else if (percentage >= 40.0)
                returnColor = Color.PaleVioletRed;
            else if (percentage >= 20.0)
                returnColor = Color.Red;
            else if (percentage < 0.0)
                returnColor = Color.Blue;
            else
                returnColor = Color.DarkRed;

            return returnColor;
        }

        private void clickOnTreeNode(object sender, TreeViewEventArgs e)
        {
            var treeNode = e.Node;
            Bitmap bitMap = new Bitmap(((MatchInfo)treeNode.Tag).ComparisonImagePath);
            pBxResultImage.Size = bitMap.Size;
            pBxResultImage.Image = bitMap;
            pBxResultImage.Update();
            lblSelectedImage.Text = treeNode.Tag.ToString();
        }

        private void btnExistingData_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileOpenDia = new OpenFileDialog();

            fileOpenDia.Title = "Open Comparison Data File.";
            fileOpenDia.Filter = "TXT files|*.txt";
            fileOpenDia.InitialDirectory = @"C:\";
            if (fileOpenDia.ShowDialog() == DialogResult.OK)
            {
                this.comparisonDatafile = fileOpenDia.FileName;
                ShowDataInComparisonFileAsTreeView();
            }
        }
    }
}
