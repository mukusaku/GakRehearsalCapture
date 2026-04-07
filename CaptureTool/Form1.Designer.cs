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
            labelOcrCount = new Label();
            buttonResetCount = new Button();
            buttonIncrement = new Button();
            buttonDecrement = new Button();
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
            buttonRunOcr.Text = "CSV保存";
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
            // labelOcrCount
            //
            labelOcrCount.AutoSize = true;
            labelOcrCount.Location = new Point(12, 187);
            labelOcrCount.Name = "labelOcrCount";
            labelOcrCount.TabIndex = 8;
            labelOcrCount.Text = "OCR実行回数: 0";
            //
            // buttonResetCount
            //
            buttonResetCount.Location = new Point(12, 207);
            buttonResetCount.Name = "buttonResetCount";
            buttonResetCount.Size = new Size(218, 40);
            buttonResetCount.TabIndex = 9;
            buttonResetCount.Text = "回数をリセット";
            buttonResetCount.UseVisualStyleBackColor = true;
            buttonResetCount.Click += buttonResetCount_Click;
            //
            // buttonIncrement
            //
            buttonIncrement.Location = new Point(12, 252);
            buttonIncrement.Name = "buttonIncrement";
            buttonIncrement.Size = new Size(109, 40);
            buttonIncrement.TabIndex = 10;
            buttonIncrement.Text = "加算";
            buttonIncrement.UseVisualStyleBackColor = true;
            buttonIncrement.Click += buttonIncrement_Click;
            //
            // buttonDecrement
            //
            buttonDecrement.Location = new Point(121, 252);
            buttonDecrement.Name = "buttonDecrement";
            buttonDecrement.Size = new Size(109, 40);
            buttonDecrement.TabIndex = 11;
            buttonDecrement.Text = "減算";
            buttonDecrement.UseVisualStyleBackColor = true;
            buttonDecrement.Click += buttonDecrement_Click;
            //
            // labelStatus
            //
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(12, 300);
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
            Controls.Add(buttonDecrement);
            Controls.Add(buttonIncrement);
            Controls.Add(buttonResetCount);
            Controls.Add(labelOcrCount);
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
        private Label labelOcrCount;
        private Button buttonResetCount;
        private Button buttonIncrement;
        private Button buttonDecrement;
    }
}
