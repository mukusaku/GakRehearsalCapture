namespace GakRehearsalCapture
{
    public partial class OverlayForm : Form
    {
        public Rectangle SelectionRectangle { get; private set; }
        private Point startPoint;
        private bool isSelecting = false;

        public OverlayForm()
        {
            InitializeComponent();
            // 全スクリーンの作業領域を取得して、フォームの範囲を設定
            Rectangle totalBounds = Rectangle.Empty;
            foreach (var screen in Screen.AllScreens)
            {
                totalBounds = Rectangle.Union(totalBounds, screen.Bounds);
            }
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;
            this.Opacity = 0.2; // 半透明
            //this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            // 全ディスプレイをカバーするように設定
            this.Bounds = totalBounds;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isSelecting = true;
                // グローバル座標のマウス位置を取得
                startPoint = Control.MousePosition;
                SelectionRectangle = new Rectangle(startPoint.X, startPoint.Y, 0, 0);
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (isSelecting)
            {
                // 現在のグローバル座標のマウス位置を取得
                Point currentPoint = Control.MousePosition;
                SelectionRectangle = new Rectangle(
                    Math.Min(startPoint.X, currentPoint.X),
                    Math.Min(startPoint.Y, currentPoint.Y),
                    Math.Abs(currentPoint.X - startPoint.X),
                    Math.Abs(currentPoint.Y - startPoint.Y)
                );
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (isSelecting && e.Button == MouseButtons.Left)
            {
                isSelecting = false;
                this.DialogResult = DialogResult.OK;
                this.Close(); // 選択が完了したらオーバーレイを閉じる
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (isSelecting)
            {
                using (Pen pen = new Pen(Color.Red, 3))
                {
                    e.Graphics.DrawRectangle(pen, SelectionRectangle);
                }
            }
        }

        public Bitmap CaptureSelectedArea()
        {
            // 選択範囲のスクリーンショットを取得
            Bitmap screenshot = new Bitmap(SelectionRectangle.Width, SelectionRectangle.Height);
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(SelectionRectangle.Location, Point.Empty, SelectionRectangle.Size);
            }
            ConfigManager.SaveCaptureArea(SelectionRectangle);
            return screenshot;
        }
    }
}
