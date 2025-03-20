namespace リハーサルキャプチャ
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
            label1 = new Label();
            button2 = new Button();
            textBox1 = new TextBox();
            pictureBox1 = new PictureBox();
            labelStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // buttonSelectRange
            // 
            buttonSelectRange.Location = new Point(12, 12);
            buttonSelectRange.Name = "buttonSelectRange";
            buttonSelectRange.Size = new Size(139, 51);
            buttonSelectRange.TabIndex = 0;
            buttonSelectRange.Text = "範囲を選択";
            buttonSelectRange.UseVisualStyleBackColor = true;
            buttonSelectRange.Click += buttonSelectRange_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // button2
            // 
            button2.Location = new Point(12, 69);
            button2.Name = "button2";
            button2.Size = new Size(139, 52);
            button2.TabIndex = 3;
            button2.Text = "OCR実行";
            button2.UseVisualStyleBackColor = true;
            button2.Click += buttonOCR_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(289, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 4;
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
            labelStatus.Location = new Point(12, 134);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(273, 15);
            labelStatus.TabIndex = 6;
            labelStatus.Text = "範囲を選択ボタンでリハーサル画面をキャプチャしてください";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 730);
            Controls.Add(labelStatus);
            Controls.Add(pictureBox1);
            Controls.Add(textBox1);
            Controls.Add(button2);
            Controls.Add(label1);
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
        private Label label1;
        private Button button2;
        private TextBox textBox1;
        private PictureBox pictureBox1;
        private Label labelStatus;
    }
}
