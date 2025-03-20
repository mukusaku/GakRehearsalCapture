using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Tesseract;

namespace GakRehearsalCapture
{
    public partial class Form1 : Form
    {
        private Point startPoint;
        private Rectangle selectionRectangle;
        private bool isSelecting = false; // 範囲選択中かどうかを判断するフラグ


        public Form1()
        {
            InitializeComponent();

            // フォームのMouseDown, MouseMove, MouseUpイベントを設定
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonSelectRange_Click(object sender, EventArgs e)
        {
            using (OverlayForm overlay = new OverlayForm())
            {
                if (overlay.ShowDialog() == DialogResult.OK)
                {
                    // 選択範囲をキャプチャしてプレビュー表示
                    Bitmap capturedImage = overlay.CaptureSelectedArea();
                    pictureBox1.Image = capturedImage;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                    // PictureBoxのサイズをキャプチャした画像に合わせる（任意）
                    //pictureBox1.Size = new Size(capturedImage.Width, capturedImage.Height);
                }
            }
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // マウス左ボタンが押されたら、ドラッグ開始位置を記録
            if (isSelecting && e.Button == MouseButtons.Left)
            {
                // グローバル座標を取得
                startPoint = Control.MousePosition;
                selectionRectangle = new Rectangle(startPoint.X, startPoint.Y, 0, 0);
                Invalidate();
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // ドラッグ中に範囲を更新
            if (isSelecting && e.Button == MouseButtons.Left)
            {
                // 現在のグローバル座標を取得
                Point currentPoint = Control.MousePosition;
                selectionRectangle.X = Math.Min(startPoint.X, currentPoint.X);
                selectionRectangle.Y = Math.Min(startPoint.Y, currentPoint.Y);
                selectionRectangle.Width = Math.Abs(currentPoint.X - startPoint.X);
                selectionRectangle.Height = Math.Abs(currentPoint.Y - startPoint.Y);
                Invalidate();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            // マウス左ボタンが離されたら、範囲を確定
            if (isSelecting && e.Button == MouseButtons.Left)
            {
                // 範囲選択が完了したので座標を表示
                textBox1.Text = $"X: {selectionRectangle.X}, Y: {selectionRectangle.Y}, Width: {selectionRectangle.Width}, Height: {selectionRectangle.Height}";

                // 範囲選択を終了
                CaptureSelectedArea();
                isSelecting = false;
                labelStatus.Visible = false;
                Invalidate();
            }
        }

        private void CaptureSelectedArea()
        {
            // 選択範囲のスクリーンショットを取得
            Bitmap screenshot = new Bitmap(selectionRectangle.Width, selectionRectangle.Height);
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(selectionRectangle.Location, Point.Empty, selectionRectangle.Size);
            }

            // PictureBoxに表示
            pictureBox1.Image = screenshot;

            // PictureBoxのサイズを選択範囲のサイズに合わせる
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Size = new Size(selectionRectangle.Width, selectionRectangle.Height);
        }

        private async void buttonOCR_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap bitmap = (Bitmap)pictureBox1.Image;

                // Tesseract OCR を実行して結果を表示
                string ocrResult = PerformOcrWithTesseract(bitmap);
                SaveToCsv(ocrResult);
                //MessageBox.Show(ocrResult);
            }
            else
            {
                MessageBox.Show("画像がありません。まず範囲を選択して画像をキャプチャしてください。");
            }
        }
        private string PerformOcrWithTesseract(Bitmap bitmap)
        {
            // Tesseract OCR エンジンの初期化
            using var ocrEngine = new TesseractEngine(@"./tessdata", "eng+jpn", EngineMode.Default);
            // Bitmap を Pix に変換して OCR 実行
            using var img = BitmapToPix(bitmap);
            using var page = ocrEngine.Process(img);
            var result = ExtractScores(page.GetText());
            return result;
        }

        private Pix BitmapToPix(Bitmap bitmap)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                stream.Position = 0;
                return Pix.LoadFromMemory(stream.ToArray());
            }
        }

        private String ExtractScores(string ocrText)
        {
            // カンマ付きの5桁の数字が3つ並んでいるパターン
            string pattern = @"\b\d{1,3},\d{3}\s\d{1,3},\d{3}\s\d{1,3},\d{3}\b";

            MatchCollection matches = Regex.Matches(ocrText, pattern);

            string extractedData = "";
            foreach (Match match in matches)
            {
                // そのままだとCSVに吐かせた際に意図しないカンマが行われるので、ダブルクォートで囲む
                extractedData += "\"" + match.Value.Replace(" ", "\",\"") + "\"" + "\n";
                //extractedData += match.Value + "\n";
            }
            // そのままだとCSVに吐かせた際に意図しないカンマが行われるので、ダブルクォートで囲む
            //extractedData = "\"" + extractedData.Replace(" ", "\",\"") + "\"";
            return extractedData;
        }

        private void SaveToCsv(string extractedData)
        {
            string filePath = "output.csv";

            try
            {
                // 追記モードで書き込む
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(extractedData);
                }
                MessageBox.Show("データをCSVに保存しました: " + filePath + "\n" + extractedData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存中にエラーが発生しました: " + ex.Message + "\n" + extractedData);
            }
        }
    }
}
