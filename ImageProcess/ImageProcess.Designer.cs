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
            this.SuspendLayout();
            // 
            // btnStartProcess
            // 
            this.btnStartProcess.Location = new System.Drawing.Point(227, 104);
            this.btnStartProcess.Name = "btnStartProcess";
            this.btnStartProcess.Size = new System.Drawing.Size(75, 23);
            this.btnStartProcess.TabIndex = 0;
            this.btnStartProcess.Text = "Start Process";
            this.btnStartProcess.UseVisualStyleBackColor = true;
            this.btnStartProcess.Click += new System.EventHandler(this.btnStartProcess_Click);
            // 
            // lblImageDirectory
            // 
            this.lblImageDirectory.AutoSize = true;
            this.lblImageDirectory.Location = new System.Drawing.Point(13, 18);
            this.lblImageDirectory.Name = "lblImageDirectory";
            this.lblImageDirectory.Size = new System.Drawing.Size(86, 13);
            this.lblImageDirectory.TabIndex = 2;
            this.lblImageDirectory.Text = "Current Directory";
            // 
            // lblBaseDirectory
            // 
            this.lblBaseDirectory.AutoSize = true;
            this.lblBaseDirectory.Location = new System.Drawing.Point(13, 44);
            this.lblBaseDirectory.Name = "lblBaseDirectory";
            this.lblBaseDirectory.Size = new System.Drawing.Size(76, 13);
            this.lblBaseDirectory.TabIndex = 4;
            this.lblBaseDirectory.Text = "Base Directory";
            // 
            // prBarFileProcessed
            // 
            this.prBarFileProcessed.Location = new System.Drawing.Point(112, 73);
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
            this.tbxBaseDirectory.Location = new System.Drawing.Point(112, 38);
            this.tbxBaseDirectory.Name = "tbxBaseDirectory";
            this.tbxBaseDirectory.Size = new System.Drawing.Size(388, 20);
            this.tbxBaseDirectory.TabIndex = 3;
            this.tbxBaseDirectory.Text = global::ImageProcess.Properties.Settings.Default.BaseDir;
            // 
            // tbxCurrentDirectory
            // 
            this.tbxCurrentDirectory.Location = new System.Drawing.Point(112, 12);
            this.tbxCurrentDirectory.Name = "tbxCurrentDirectory";
            this.tbxCurrentDirectory.Size = new System.Drawing.Size(388, 20);
            this.tbxCurrentDirectory.TabIndex = 1;
            this.tbxCurrentDirectory.Text = global::ImageProcess.Properties.Settings.Default.CurrentDir;
            // 
            // ImageProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 149);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.prBarFileProcessed);
            this.Controls.Add(this.lblBaseDirectory);
            this.Controls.Add(this.tbxBaseDirectory);
            this.Controls.Add(this.lblImageDirectory);
            this.Controls.Add(this.tbxCurrentDirectory);
            this.Controls.Add(this.btnStartProcess);
            this.Name = "ImageProcess";
            this.Text = "Image Resize";
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
    }
}

