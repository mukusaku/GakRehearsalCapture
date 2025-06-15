namespace GakRehearsalCapture
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonSelectRange = new Button();
            buttonRunOcr = new Button();
            pictureBox1 = new PictureBox();
            labelStatus = new Label();
            buttonReread = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // buttonSelectRange
            // 
            buttonSelectRange.Location = new Point(12, 12);
            buttonSelectRange.Name = "buttonSelectRange";
            buttonSelectRange.Size = new Size(218, 51);
            buttonSelectRange.TabIndex = 0;
            buttonSelectRange.Text = "範囲を選択";
            buttonSelectRange.UseVisualStyleBackColor = true;
            buttonSelectRange.Click += buttonSelectRange_Click;
            // 
            // buttonRunOcr
            //
            buttonRunOcr.Location = new Point(12, 69);
            buttonRunOcr.Name = "buttonRunOcr";
            buttonRunOcr.Size = new Size(218, 52);
            buttonRunOcr.TabIndex = 3;
            buttonRunOcr.Text = "OCR実行";
            buttonRunOcr.UseVisualStyleBackColor = true;
            buttonRunOcr.Click += buttonRunOcr_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(395, 13);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(373, 705);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(12, 182);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(273, 15);
            labelStatus.TabIndex = 6;
            labelStatus.Text = "範囲を選択ボタンでリハーサル画面をキャプチャしてください";
            // 
            // buttonReread
            //
            buttonReread.Location = new Point(12, 127);
            buttonReread.Name = "buttonReread";
            buttonReread.Size = new Size(218, 52);
            buttonReread.TabIndex = 7;
            buttonReread.Text = "今の選択範囲で読み取り直す";
            buttonReread.UseVisualStyleBackColor = true;
            buttonReread.Click += buttonReread_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 730);
            Controls.Add(buttonReread);
            Controls.Add(labelStatus);
            Controls.Add(pictureBox1);
            Controls.Add(buttonRunOcr);
            Controls.Add(buttonSelectRange);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonSelectRange;
        private Button buttonRunOcr;
        private PictureBox pictureBox1;
        private Label labelStatus;
        private Button buttonReread;
    }
}
