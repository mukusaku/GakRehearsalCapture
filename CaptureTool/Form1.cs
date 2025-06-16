using System.Text.RegularExpressions;
using System.Text;
using Tesseract;

namespace GakRehearsalCapture
{
    public partial class Form1 : Form
    {

        private string lastOcrResult = string.Empty;

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
                    Bitmap capturedImage = overlay.CaptureSelectedArea();
                    pictureBox1.Image?.Dispose();
                    pictureBox1.Image = capturedImage;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    PerformOcrAndPreview(capturedImage);
                }
            }
        }

        private void buttonRunOcr_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lastOcrResult))
            {
                MessageBox.Show("まず範囲を選択してOCRを実行してください。");
                return;
            }
            SaveToCsv(lastOcrResult);
        }

        private (string Csv, string Preview) PerformOcr(Bitmap bitmap)
        {
            using var ocrEngine = new TesseractEngine(@"./tessdata", "eng+jpn", EngineMode.Default);
            using var img = BitmapToPix(bitmap);
            using var page = ocrEngine.Process(img);
            return ExtractScores(page.GetText());
        }

        private void PerformOcrAndPreview(Bitmap bitmap)
        {
            var result = PerformOcr(bitmap);
            lastOcrResult = result.Csv;
            if (string.IsNullOrEmpty(lastOcrResult))
            {
                labelStatus.Text = "数字が検出されませんでした";
            }
            else
            {
                labelStatus.Text = result.Preview;
            }
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

        private (string Csv, string Preview) ExtractScores(string ocrText)
        {
            // カンマ付きの5桁の数字が3つ並んでいるパターン
            string pattern = @"\b\d{1,3},\d{3}\s\d{1,3},\d{3}\s\d{1,3},\d{3}\b";

            MatchCollection matches = Regex.Matches(ocrText, pattern);

            var csvBuilder = new System.Text.StringBuilder();
            var previewBuilder = new System.Text.StringBuilder();

            foreach (Match match in matches)
            {
                string formatted = "\"" + match.Value.Replace(" ", "\",\"") + "\"";
                csvBuilder.Append(formatted).Append(',');
                previewBuilder.AppendLine(formatted);
            }

            return (csvBuilder.ToString(), previewBuilder.ToString());
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

        private void buttonReread_Click(object sender, EventArgs e)
        {
            Rectangle? area = ConfigManager.GetCaptureArea();

            if (area == null)
            {
                MessageBox.Show("保存された選択範囲がありません！");
                return;
            }

            Bitmap bmp = new Bitmap(area.Value.Width, area.Value.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(area.Value.X, area.Value.Y, 0, 0, area.Value.Size);
            }
            // 以前の画像を解放してから更新
            pictureBox1.Image?.Dispose();
            // 選択範囲をキャプチャしてプレビュー表示
            pictureBox1.Image = bmp;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            PerformOcrAndPreview(bmp);
        }
    }
}
