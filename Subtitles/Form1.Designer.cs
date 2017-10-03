namespace Subtitles
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtPath = new System.Windows.Forms.TextBox();
            this.chcUTF = new System.Windows.Forms.CheckBox();
            this.chcNames = new System.Windows.Forms.CheckBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.chcSubFold = new System.Windows.Forms.CheckBox();
            this.chcDownloadSub = new System.Windows.Forms.CheckBox();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(12, 12);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(414, 20);
            this.txtPath.TabIndex = 0;
            // 
            // chcUTF
            // 
            this.chcUTF.AutoSize = true;
            this.chcUTF.Location = new System.Drawing.Point(135, 38);
            this.chcUTF.Name = "chcUTF";
            this.chcUTF.Size = new System.Drawing.Size(47, 17);
            this.chcUTF.TabIndex = 1;
            this.chcUTF.Text = "UTF";
            this.chcUTF.UseVisualStyleBackColor = true;
            // 
            // chcNames
            // 
            this.chcNames.AutoSize = true;
            this.chcNames.Location = new System.Drawing.Point(202, 38);
            this.chcNames.Name = "chcNames";
            this.chcNames.Size = new System.Drawing.Size(89, 17);
            this.chcNames.TabIndex = 2;
            this.chcNames.Text = "SRT NAMES";
            this.chcNames.UseVisualStyleBackColor = true;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(351, 61);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 4;
            this.btnGo.Text = "Convert";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.button1_Click);
            // 
            // chcSubFold
            // 
            this.chcSubFold.AutoSize = true;
            this.chcSubFold.Location = new System.Drawing.Point(328, 38);
            this.chcSubFold.Name = "chcSubFold";
            this.chcSubFold.Size = new System.Drawing.Size(98, 17);
            this.chcSubFold.TabIndex = 6;
            this.chcSubFold.Text = "SUBFOLDERS";
            this.chcSubFold.UseVisualStyleBackColor = true;
            // 
            // chcDownloadSub
            // 
            this.chcDownloadSub.AutoSize = true;
            this.chcDownloadSub.Checked = true;
            this.chcDownloadSub.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chcDownloadSub.Location = new System.Drawing.Point(12, 38);
            this.chcDownloadSub.Name = "chcDownloadSub";
            this.chcDownloadSub.Size = new System.Drawing.Size(117, 17);
            this.chcDownloadSub.TabIndex = 7;
            this.chcDownloadSub.Text = "Download Subtitles";
            this.chcDownloadSub.UseVisualStyleBackColor = true;
            // 
            // txtInfo
            // 
            this.txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInfo.Location = new System.Drawing.Point(12, 92);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(414, 224);
            this.txtInfo.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 328);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.chcDownloadSub);
            this.Controls.Add(this.chcSubFold);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.chcNames);
            this.Controls.Add(this.chcUTF);
            this.Controls.Add(this.txtPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.CheckBox chcUTF;
        private System.Windows.Forms.CheckBox chcNames;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.CheckBox chcSubFold;
        private System.Windows.Forms.CheckBox chcDownloadSub;
        private System.Windows.Forms.TextBox txtInfo;
    }
}

