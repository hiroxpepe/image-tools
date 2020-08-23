namespace ImageCut {
    partial class ImageForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.statusStripImage = new System.Windows.Forms.StatusStrip();
            this.numericUpDownTop = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLeft = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            this.labelTop = new System.Windows.Forms.Label();
            this.labelLeft = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.buttonDummy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBoxImage.Location = new System.Drawing.Point(2, 2);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(960, 540);
            this.pictureBoxImage.TabIndex = 0;
            this.pictureBoxImage.TabStop = false;
            this.pictureBoxImage.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBoxImage_DragDrop);
            this.pictureBoxImage.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBoxImage_DragEnter);
            this.pictureBoxImage.DragOver += new System.Windows.Forms.DragEventHandler(this.pictureBoxImage_DragOver);
            this.pictureBoxImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxImage_MouseDown);
            this.pictureBoxImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxImage_MouseMove);
            this.pictureBoxImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxImage_MouseUp);
            // 
            // statusStripImage
            // 
            this.statusStripImage.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.statusStripImage.Location = new System.Drawing.Point(0, 575);
            this.statusStripImage.Name = "statusStripImage";
            this.statusStripImage.Size = new System.Drawing.Size(965, 22);
            this.statusStripImage.TabIndex = 1;
            this.statusStripImage.Text = "statusStrip1";
            // 
            // numericUpDownTop
            // 
            this.numericUpDownTop.Location = new System.Drawing.Point(38, 546);
            this.numericUpDownTop.Maximum = new decimal(new int[] {
            540,
            0,
            0,
            0});
            this.numericUpDownTop.Name = "numericUpDownTop";
            this.numericUpDownTop.Size = new System.Drawing.Size(60, 21);
            this.numericUpDownTop.TabIndex = 0;
            this.numericUpDownTop.TabStop = false;
            this.numericUpDownTop.Click += new System.EventHandler(this.numericUpDownTop_Click);
            this.numericUpDownTop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numericUpDownTop_KeyUp);
            // 
            // numericUpDownLeft
            // 
            this.numericUpDownLeft.Location = new System.Drawing.Point(141, 546);
            this.numericUpDownLeft.Maximum = new decimal(new int[] {
            960,
            0,
            0,
            0});
            this.numericUpDownLeft.Name = "numericUpDownLeft";
            this.numericUpDownLeft.Size = new System.Drawing.Size(60, 21);
            this.numericUpDownLeft.TabIndex = 0;
            this.numericUpDownLeft.TabStop = false;
            this.numericUpDownLeft.Click += new System.EventHandler(this.numericUpDownLeft_Click);
            this.numericUpDownLeft.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numericUpDownLeft_KeyUp);
            // 
            // numericUpDownWidth
            // 
            this.numericUpDownWidth.Location = new System.Drawing.Point(260, 546);
            this.numericUpDownWidth.Maximum = new decimal(new int[] {
            960,
            0,
            0,
            0});
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Size = new System.Drawing.Size(60, 21);
            this.numericUpDownWidth.TabIndex = 0;
            this.numericUpDownWidth.TabStop = false;
            this.numericUpDownWidth.Click += new System.EventHandler(this.numericUpDownWidth_Click);
            this.numericUpDownWidth.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numericUpDownWidth_KeyUp);
            // 
            // numericUpDownHeight
            // 
            this.numericUpDownHeight.Location = new System.Drawing.Point(376, 546);
            this.numericUpDownHeight.Maximum = new decimal(new int[] {
            540,
            0,
            0,
            0});
            this.numericUpDownHeight.Name = "numericUpDownHeight";
            this.numericUpDownHeight.Size = new System.Drawing.Size(60, 21);
            this.numericUpDownHeight.TabIndex = 0;
            this.numericUpDownHeight.TabStop = false;
            this.numericUpDownHeight.Click += new System.EventHandler(this.numericUpDownHeight_Click);
            this.numericUpDownHeight.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numericUpDownHeight_KeyUp);
            // 
            // labelTop
            // 
            this.labelTop.AutoSize = true;
            this.labelTop.Location = new System.Drawing.Point(3, 549);
            this.labelTop.Name = "labelTop";
            this.labelTop.Size = new System.Drawing.Size(29, 14);
            this.labelTop.TabIndex = 6;
            this.labelTop.Text = "Top";
            // 
            // labelLeft
            // 
            this.labelLeft.AutoSize = true;
            this.labelLeft.Location = new System.Drawing.Point(104, 549);
            this.labelLeft.Name = "labelLeft";
            this.labelLeft.Size = new System.Drawing.Size(31, 14);
            this.labelLeft.TabIndex = 7;
            this.labelLeft.Text = "Left";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(215, 549);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(39, 14);
            this.labelWidth.TabIndex = 8;
            this.labelWidth.Text = "Width";
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(326, 549);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(44, 14);
            this.labelHeight.TabIndex = 9;
            this.labelHeight.Text = "Height";
            // 
            // buttonDummy
            // 
            this.buttonDummy.Location = new System.Drawing.Point(461, 546);
            this.buttonDummy.Name = "buttonDummy";
            this.buttonDummy.Size = new System.Drawing.Size(124, 23);
            this.buttonDummy.TabIndex = 10;
            this.buttonDummy.Text = "移動・サイズ変更";
            this.buttonDummy.UseVisualStyleBackColor = true;
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(965, 597);
            this.Controls.Add(this.buttonDummy);
            this.Controls.Add(this.labelHeight);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.labelLeft);
            this.Controls.Add(this.labelTop);
            this.Controls.Add(this.numericUpDownHeight);
            this.Controls.Add(this.numericUpDownWidth);
            this.Controls.Add(this.numericUpDownLeft);
            this.Controls.Add(this.numericUpDownTop);
            this.Controls.Add(this.statusStripImage);
            this.Controls.Add(this.pictureBoxImage);
            this.Name = "ImageForm";
            this.Text = "ImageForm";
            this.Load += new System.EventHandler(this.ImageForm_Load);
            this.MouseEnter += new System.EventHandler(this.ImageForm_MouseEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.StatusStrip statusStripImage;
        private System.Windows.Forms.NumericUpDown numericUpDownTop;
        private System.Windows.Forms.NumericUpDown numericUpDownLeft;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.Label labelTop;
        private System.Windows.Forms.Label labelLeft;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Button buttonDummy;
    }
}