namespace ImageProcess
{
    partial class ImageProcess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStartProcess = new System.Windows.Forms.Button();
            this.lblImageDirectory = new System.Windows.Forms.Label();
            this.lblBaseDirectory = new System.Windows.Forms.Label();
            this.prBarFileProcessed = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tbxBaseDirectory = new System.Windows.Forms.TextBox();
            this.tbxCurrentDirectory = new System.Windows.Forms.TextBox();
            this.pBxResultImage = new System.Windows.Forms.PictureBox();
            this.trvPercentageDivision = new System.Windows.Forms.TreeView();
            this.lblSelectedImage = new System.Windows.Forms.Label();
            this.btnExistingData = new System.Windows.Forms.Button();
            this.btnMatch = new System.Windows.Forms.Button();
            this.cbxMatchWithResized = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pBxResultImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartProcess
            // 
            this.btnStartProcess.Location = new System.Drawing.Point(144, 97);
            this.btnStartProcess.Name = "btnStartProcess";
            this.btnStartProcess.Size = new System.Drawing.Size(75, 23);
            this.btnStartProcess.TabIndex = 0;
            this.btnStartProcess.Text = "Resize";
            this.btnStartProcess.UseVisualStyleBackColor = true;
            this.btnStartProcess.Click += new System.EventHandler(this.btnResizeCurrentImages_Click);
            // 
            // lblImageDirectory
            // 
            this.lblImageDirectory.AutoSize = true;
            this.lblImageDirectory.Location = new System.Drawing.Point(13, 38);
            this.lblImageDirectory.Name = "lblImageDirectory";
            this.lblImageDirectory.Size = new System.Drawing.Size(86, 13);
            this.lblImageDirectory.TabIndex = 2;
            this.lblImageDirectory.Text = "Current Directory";
            // 
            // lblBaseDirectory
            // 
            this.lblBaseDirectory.AutoSize = true;
            this.lblBaseDirectory.Location = new System.Drawing.Point(13, 12);
            this.lblBaseDirectory.Name = "lblBaseDirectory";
            this.lblBaseDirectory.Size = new System.Drawing.Size(76, 13);
            this.lblBaseDirectory.TabIndex = 4;
            this.lblBaseDirectory.Text = "Base Directory";
            // 
            // prBarFileProcessed
            // 
            this.prBarFileProcessed.Location = new System.Drawing.Point(112, 58);
            this.prBarFileProcessed.Name = "prBarFileProcessed";
            this.prBarFileProcessed.Size = new System.Drawing.Size(388, 16);
            this.prBarFileProcessed.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(13, 73);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 20);
            this.lblStatus.TabIndex = 6;
            // 
            // tbxBaseDirectory
            // 
            this.tbxBaseDirectory.Location = new System.Drawing.Point(112, 6);
            this.tbxBaseDirectory.Name = "tbxBaseDirectory";
            this.tbxBaseDirectory.Size = new System.Drawing.Size(388, 20);
            this.tbxBaseDirectory.TabIndex = 3;
            this.tbxBaseDirectory.Text = global::ImageProcess.Properties.Settings.Default.BaseDir;
            // 
            // tbxCurrentDirectory
            // 
            this.tbxCurrentDirectory.Location = new System.Drawing.Point(112, 32);
            this.tbxCurrentDirectory.Name = "tbxCurrentDirectory";
            this.tbxCurrentDirectory.Size = new System.Drawing.Size(388, 20);
            this.tbxCurrentDirectory.TabIndex = 1;
            this.tbxCurrentDirectory.Text = global::ImageProcess.Properties.Settings.Default.CurrentDir;
            // 
            // pBxResultImage
            // 
            this.pBxResultImage.Location = new System.Drawing.Point(271, 133);
            this.pBxResultImage.Name = "pBxResultImage";
            this.pBxResultImage.Size = new System.Drawing.Size(510, 438);
            this.pBxResultImage.TabIndex = 7;
            this.pBxResultImage.TabStop = false;
            // 
            // trvPercentageDivision
            // 
            this.trvPercentageDivision.Location = new System.Drawing.Point(16, 133);
            this.trvPercentageDivision.Name = "trvPercentageDivision";
            this.trvPercentageDivision.Size = new System.Drawing.Size(221, 615);
            this.trvPercentageDivision.TabIndex = 9;
            this.trvPercentageDivision.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.clickOnTreeNode);
            // 
            // lblSelectedImage
            // 
            this.lblSelectedImage.AutoSize = true;
            this.lblSelectedImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedImage.Location = new System.Drawing.Point(552, 58);
            this.lblSelectedImage.Name = "lblSelectedImage";
            this.lblSelectedImage.Size = new System.Drawing.Size(87, 16);
            this.lblSelectedImage.TabIndex = 10;
            this.lblSelectedImage.Text = "Selected File";
            // 
            // btnExistingData
            // 
            this.btnExistingData.Location = new System.Drawing.Point(162, 133);
            this.btnExistingData.Name = "btnExistingData";
            this.btnExistingData.Size = new System.Drawing.Size(75, 23);
            this.btnExistingData.TabIndex = 11;
            this.btnExistingData.Text = "Import";
            this.btnExistingData.UseVisualStyleBackColor = true;
            this.btnExistingData.Click += new System.EventHandler(this.btnExistingData_Click);
            // 
            // btnMatch
            // 
            this.btnMatch.Location = new System.Drawing.Point(319, 97);
            this.btnMatch.Name = "btnMatch";
            this.btnMatch.Size = new System.Drawing.Size(75, 23);
            this.btnMatch.TabIndex = 12;
            this.btnMatch.Text = "Match";
            this.btnMatch.UseVisualStyleBackColor = true;
            this.btnMatch.Click += new System.EventHandler(this.btnMatch_Click);
            // 
            // cbxMatchWithResized
            // 
            this.cbxMatchWithResized.AutoSize = true;
            this.cbxMatchWithResized.Checked = true;
            this.cbxMatchWithResized.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxMatchWithResized.Location = new System.Drawing.Point(319, 80);
            this.cbxMatchWithResized.Name = "cbxMatchWithResized";
            this.cbxMatchWithResized.Size = new System.Drawing.Size(159, 17);
            this.cbxMatchWithResized.TabIndex = 13;
            this.cbxMatchWithResized.Text = "Match With Resized Images";
            this.cbxMatchWithResized.UseVisualStyleBackColor = true;
            // 
            // ImageProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1853, 790);
            this.Controls.Add(this.cbxMatchWithResized);
            this.Controls.Add(this.btnMatch);
            this.Controls.Add(this.btnExistingData);
            this.Controls.Add(this.lblSelectedImage);
            this.Controls.Add(this.trvPercentageDivision);
            this.Controls.Add(this.pBxResultImage);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.prBarFileProcessed);
            this.Controls.Add(this.lblBaseDirectory);
            this.Controls.Add(this.tbxBaseDirectory);
            this.Controls.Add(this.lblImageDirectory);
            this.Controls.Add(this.tbxCurrentDirectory);
            this.Controls.Add(this.btnStartProcess);
            this.Name = "ImageProcess";
            this.Text = "Image Resize";
            ((System.ComponentModel.ISupportInitialize)(this.pBxResultImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartProcess;
        private System.Windows.Forms.TextBox tbxCurrentDirectory;
        private System.Windows.Forms.Label lblImageDirectory;
        private System.Windows.Forms.Label lblBaseDirectory;
        private System.Windows.Forms.TextBox tbxBaseDirectory;
        private System.Windows.Forms.ProgressBar prBarFileProcessed;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.PictureBox pBxResultImage;
        private System.Windows.Forms.TreeView trvPercentageDivision;
        private System.Windows.Forms.Label lblSelectedImage;
        private System.Windows.Forms.Button btnExistingData;
        private System.Windows.Forms.Button btnMatch;
        private System.Windows.Forms.CheckBox cbxMatchWithResized;
    }
}

