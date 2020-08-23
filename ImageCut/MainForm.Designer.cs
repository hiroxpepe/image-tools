namespace ImageCut {
    partial class MainForm {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.pictureBoxThumbnail = new System.Windows.Forms.PictureBox();
            this.buttonOutputFile = new System.Windows.Forms.Button();
            this.labelOutputDir = new System.Windows.Forms.Label();
            this.textBoxOutputDir = new System.Windows.Forms.TextBox();
            this.labelOutputFile = new System.Windows.Forms.Label();
            this.textBoxOutputFile = new System.Windows.Forms.TextBox();
            this.buttonReferOutputFile = new System.Windows.Forms.Button();
            this.buttonReferOutputDir = new System.Windows.Forms.Button();
            this.openOutputFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.outputFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.mainToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonShowForm = new System.Windows.Forms.Button();
            this.buttonOutputSelectedFile = new System.Windows.Forms.Button();
            this.listBoxItem = new System.Windows.Forms.ListBox();
            this.buttonDeleteItem = new System.Windows.Forms.Button();
            this.buttonAddItem = new System.Windows.Forms.Button();
            this.textBoxItemName = new System.Windows.Forms.TextBox();
            this.buttonLoadItem = new System.Windows.Forms.Button();
            this.radioButtonNormal = new System.Windows.Forms.RadioButton();
            this.radioButtonAuto = new System.Windows.Forms.RadioButton();
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbnail)).BeginInit();
            this.statusStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxThumbnail
            // 
            this.pictureBoxThumbnail.Location = new System.Drawing.Point(14, 7);
            this.pictureBoxThumbnail.Name = "pictureBoxThumbnail";
            this.pictureBoxThumbnail.Size = new System.Drawing.Size(366, 193);
            this.pictureBoxThumbnail.TabIndex = 2;
            this.pictureBoxThumbnail.TabStop = false;
            this.pictureBoxThumbnail.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBoxThumbnail_DragDrop);
            this.pictureBoxThumbnail.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBoxThumbnail_DragEnter);
            this.pictureBoxThumbnail.DragOver += new System.Windows.Forms.DragEventHandler(this.pictureBoxThumbnail_DragOver);
            // 
            // buttonOutputFile
            // 
            this.buttonOutputFile.Location = new System.Drawing.Point(166, 349);
            this.buttonOutputFile.Name = "buttonOutputFile";
            this.buttonOutputFile.Size = new System.Drawing.Size(145, 25);
            this.buttonOutputFile.TabIndex = 6;
            this.buttonOutputFile.Text = "オリジナル画像出力";
            this.buttonOutputFile.UseVisualStyleBackColor = true;
            this.buttonOutputFile.Click += new System.EventHandler(this.buttonOutputFile_Click);
            // 
            // labelOutputDir
            // 
            this.labelOutputDir.AutoSize = true;
            this.labelOutputDir.Location = new System.Drawing.Point(18, 295);
            this.labelOutputDir.Name = "labelOutputDir";
            this.labelOutputDir.Size = new System.Drawing.Size(137, 15);
            this.labelOutputDir.TabIndex = 10;
            this.labelOutputDir.Text = "画像出力先フォルダー";
            // 
            // textBoxOutputDir
            // 
            this.textBoxOutputDir.Location = new System.Drawing.Point(14, 315);
            this.textBoxOutputDir.Name = "textBoxOutputDir";
            this.textBoxOutputDir.Size = new System.Drawing.Size(297, 22);
            this.textBoxOutputDir.TabIndex = 9;
            // 
            // labelOutputFile
            // 
            this.labelOutputFile.AutoSize = true;
            this.labelOutputFile.Location = new System.Drawing.Point(18, 241);
            this.labelOutputFile.Name = "labelOutputFile";
            this.labelOutputFile.Size = new System.Drawing.Size(124, 15);
            this.labelOutputFile.TabIndex = 8;
            this.labelOutputFile.Text = "画像出力ファイル名";
            // 
            // textBoxOutputFile
            // 
            this.textBoxOutputFile.Location = new System.Drawing.Point(14, 261);
            this.textBoxOutputFile.Name = "textBoxOutputFile";
            this.textBoxOutputFile.Size = new System.Drawing.Size(297, 22);
            this.textBoxOutputFile.TabIndex = 7;
            // 
            // buttonReferOutputFile
            // 
            this.buttonReferOutputFile.Location = new System.Drawing.Point(318, 261);
            this.buttonReferOutputFile.Name = "buttonReferOutputFile";
            this.buttonReferOutputFile.Size = new System.Drawing.Size(61, 25);
            this.buttonReferOutputFile.TabIndex = 11;
            this.buttonReferOutputFile.Text = "参照";
            this.buttonReferOutputFile.UseVisualStyleBackColor = true;
            this.buttonReferOutputFile.Click += new System.EventHandler(this.buttonReferOutputFile_Click);
            // 
            // buttonReferOutputDir
            // 
            this.buttonReferOutputDir.Location = new System.Drawing.Point(319, 315);
            this.buttonReferOutputDir.Name = "buttonReferOutputDir";
            this.buttonReferOutputDir.Size = new System.Drawing.Size(61, 25);
            this.buttonReferOutputDir.TabIndex = 12;
            this.buttonReferOutputDir.Text = "参照";
            this.buttonReferOutputDir.UseVisualStyleBackColor = true;
            this.buttonReferOutputDir.Click += new System.EventHandler(this.buttonReferOutputDir_Click);
            // 
            // openOutputFileDialog
            // 
            this.openOutputFileDialog.FileName = "openOutputFileDialog";
            // 
            // statusStripMain
            // 
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripStatusLabel,
            this.toolStripStatusLabelMain});
            this.statusStripMain.Location = new System.Drawing.Point(0, 501);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStripMain.Size = new System.Drawing.Size(393, 22);
            this.statusStripMain.TabIndex = 13;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // mainToolStripStatusLabel
            // 
            this.mainToolStripStatusLabel.Name = "mainToolStripStatusLabel";
            this.mainToolStripStatusLabel.Size = new System.Drawing.Size(0, 16);
            // 
            // toolStripStatusLabelMain
            // 
            this.toolStripStatusLabelMain.Name = "toolStripStatusLabelMain";
            this.toolStripStatusLabelMain.Size = new System.Drawing.Size(0, 16);
            // 
            // buttonShowForm
            // 
            this.buttonShowForm.Location = new System.Drawing.Point(14, 206);
            this.buttonShowForm.Name = "buttonShowForm";
            this.buttonShowForm.Size = new System.Drawing.Size(194, 25);
            this.buttonShowForm.TabIndex = 14;
            this.buttonShowForm.Text = "トリミング設定フォームを開く";
            this.buttonShowForm.UseVisualStyleBackColor = true;
            this.buttonShowForm.Click += new System.EventHandler(this.buttonShowForm_Click);
            // 
            // buttonOutputSelectedFile
            // 
            this.buttonOutputSelectedFile.Location = new System.Drawing.Point(13, 349);
            this.buttonOutputSelectedFile.Name = "buttonOutputSelectedFile";
            this.buttonOutputSelectedFile.Size = new System.Drawing.Size(145, 25);
            this.buttonOutputSelectedFile.TabIndex = 15;
            this.buttonOutputSelectedFile.Text = "トリミング画像出力";
            this.buttonOutputSelectedFile.UseVisualStyleBackColor = true;
            this.buttonOutputSelectedFile.Click += new System.EventHandler(this.buttonOutputSelectedFile_Click);
            // 
            // listBoxItem
            // 
            this.listBoxItem.FormattingEnabled = true;
            this.listBoxItem.ItemHeight = 15;
            this.listBoxItem.Location = new System.Drawing.Point(13, 391);
            this.listBoxItem.Name = "listBoxItem";
            this.listBoxItem.Size = new System.Drawing.Size(298, 64);
            this.listBoxItem.TabIndex = 16;
            // 
            // buttonDeleteItem
            // 
            this.buttonDeleteItem.Location = new System.Drawing.Point(318, 422);
            this.buttonDeleteItem.Name = "buttonDeleteItem";
            this.buttonDeleteItem.Size = new System.Drawing.Size(61, 25);
            this.buttonDeleteItem.TabIndex = 17;
            this.buttonDeleteItem.Text = "削除";
            this.buttonDeleteItem.UseVisualStyleBackColor = true;
            this.buttonDeleteItem.Click += new System.EventHandler(this.buttonDeleteItem_Click);
            // 
            // buttonAddItem
            // 
            this.buttonAddItem.Location = new System.Drawing.Point(318, 465);
            this.buttonAddItem.Name = "buttonAddItem";
            this.buttonAddItem.Size = new System.Drawing.Size(61, 25);
            this.buttonAddItem.TabIndex = 18;
            this.buttonAddItem.Text = "追加";
            this.buttonAddItem.UseVisualStyleBackColor = true;
            this.buttonAddItem.Click += new System.EventHandler(this.buttonAddItem_Click);
            // 
            // textBoxItemName
            // 
            this.textBoxItemName.Location = new System.Drawing.Point(13, 466);
            this.textBoxItemName.Name = "textBoxItemName";
            this.textBoxItemName.Size = new System.Drawing.Size(298, 22);
            this.textBoxItemName.TabIndex = 19;
            // 
            // buttonLoadItem
            // 
            this.buttonLoadItem.Location = new System.Drawing.Point(318, 391);
            this.buttonLoadItem.Name = "buttonLoadItem";
            this.buttonLoadItem.Size = new System.Drawing.Size(61, 25);
            this.buttonLoadItem.TabIndex = 20;
            this.buttonLoadItem.Text = "ロード";
            this.buttonLoadItem.UseVisualStyleBackColor = true;
            this.buttonLoadItem.Click += new System.EventHandler(this.buttonLoadItem_Click);
            // 
            // radioButtonNormal
            // 
            this.radioButtonNormal.AutoSize = true;
            this.radioButtonNormal.Location = new System.Drawing.Point(218, 207);
            this.radioButtonNormal.Name = "radioButtonNormal";
            this.radioButtonNormal.Size = new System.Drawing.Size(88, 19);
            this.radioButtonNormal.TabIndex = 21;
            this.radioButtonNormal.TabStop = true;
            this.radioButtonNormal.Text = "手動保存";
            this.radioButtonNormal.UseVisualStyleBackColor = true;
            // 
            // radioButtonAuto
            // 
            this.radioButtonAuto.AutoSize = true;
            this.radioButtonAuto.Location = new System.Drawing.Point(218, 231);
            this.radioButtonAuto.Name = "radioButtonAuto";
            this.radioButtonAuto.Size = new System.Drawing.Size(88, 19);
            this.radioButtonAuto.TabIndex = 22;
            this.radioButtonAuto.TabStop = true;
            this.radioButtonAuto.Text = "自動保存";
            this.radioButtonAuto.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 523);
            this.Controls.Add(this.radioButtonAuto);
            this.Controls.Add(this.radioButtonNormal);
            this.Controls.Add(this.buttonLoadItem);
            this.Controls.Add(this.textBoxItemName);
            this.Controls.Add(this.buttonAddItem);
            this.Controls.Add(this.buttonDeleteItem);
            this.Controls.Add(this.listBoxItem);
            this.Controls.Add(this.buttonOutputSelectedFile);
            this.Controls.Add(this.buttonShowForm);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.buttonReferOutputDir);
            this.Controls.Add(this.buttonReferOutputFile);
            this.Controls.Add(this.labelOutputDir);
            this.Controls.Add(this.textBoxOutputDir);
            this.Controls.Add(this.labelOutputFile);
            this.Controls.Add(this.textBoxOutputFile);
            this.Controls.Add(this.buttonOutputFile);
            this.Controls.Add(this.pictureBoxThumbnail);
            this.Name = "MainForm";
            this.Text = "ImageCut";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseEnter += new System.EventHandler(this.MainForm_MouseEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbnail)).EndInit();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBoxThumbnail;
        private System.Windows.Forms.Button buttonOutputFile;
        private System.Windows.Forms.Label labelOutputDir;
        private System.Windows.Forms.TextBox textBoxOutputDir;
        private System.Windows.Forms.Label labelOutputFile;
        private System.Windows.Forms.TextBox textBoxOutputFile;
        private System.Windows.Forms.Button buttonReferOutputFile;
        private System.Windows.Forms.Button buttonReferOutputDir;
        private System.Windows.Forms.OpenFileDialog openOutputFileDialog;
        private System.Windows.Forms.FolderBrowserDialog outputFolderBrowserDialog;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel mainToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMain;
        private System.Windows.Forms.Button buttonShowForm;
        private System.Windows.Forms.Button buttonOutputSelectedFile;
        private System.Windows.Forms.ListBox listBoxItem;
        private System.Windows.Forms.Button buttonDeleteItem;
        private System.Windows.Forms.Button buttonAddItem;
        private System.Windows.Forms.TextBox textBoxItemName;
        private System.Windows.Forms.Button buttonLoadItem;
        private System.Windows.Forms.RadioButton radioButtonNormal;
        private System.Windows.Forms.RadioButton radioButtonAuto;
        private System.Windows.Forms.ToolTip toolTipMain;
    }
}

