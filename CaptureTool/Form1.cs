using System.Text.RegularExpressions;
using Tesseract;

namespace GakRehearsalCapture
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
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

        private async void buttonOCR_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap bitmap = (Bitmap)pictureBox1.Image;

                // Tesseract OCR を実行して結果を表示
                PerformOcrWithTesseract(bitmap);
                //SaveToCsv(ocrResult);
            }
            else
            {
                MessageBox.Show("画像がありません。まず範囲を選択して画像をキャプチャしてください。");
            }
        }
        private void PerformOcrWithTesseract(Bitmap bitmap)
        {
            // Tesseract OCR エンジンの初期化
            using var ocrEngine = new TesseractEngine(@"./tessdata", "eng+jpn", EngineMode.Default);
            // Bitmap を Pix に変換して OCR 実行
            using var img = BitmapToPix(bitmap);
            using var page = ocrEngine.Process(img);
            var result = ExtractScores(page.GetText());
            SaveToCsv(result);
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
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            Rectangle? area = ConfigManager.GetCaptureArea();

            if (area == null)
            {
                MessageBox.Show("保存された選択範囲がありません！");
                return;
            }

            using (Bitmap bmp = new Bitmap(area.Value.Width, area.Value.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(area.Value.X, area.Value.Y, 0, 0, area.Value.Size);
                }
                // 選択範囲をキャプチャしてプレビュー表示
                pictureBox1.Image = bmp;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                PerformOcrWithTesseract(bmp);
            }
        }
    }
}
