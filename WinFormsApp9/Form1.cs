using System.Windows.Forms;

namespace WinFormsApp9
{
    public partial class Form1 : Form
    {
        private List<Action<Graphics>> drawActions = new List<Action<Graphics>>();
        private Font selectedFont;
        private string textToDraw = "Ваш текст тут";
        private Point textPosition;
        public Form1()
        {
            InitializeComponent();
        }

        private Graphics g;
        private ColorDialog colorDialog;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var action in drawActions)
            {
                action(e.Graphics);
            }
            if (selectedFont != null)
            {
                e.Graphics.DrawString(textToDraw, selectedFont, Brushes.Black, textPosition);
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            textPosition = e.Location;
            colorDialog = new ColorDialog();
            g = CreateGraphics();

            colorDialog.AllowFullOpen = true;
            colorDialog.SolidColorOnly = false;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Color selectedColor = colorDialog.Color;
                Pen pen = new Pen(selectedColor);
                drawActions.Add(g => g.DrawEllipse(pen, 100, 100, 300, 200));
                drawActions.Add(g => g.DrawRectangle(pen, 300, 300, 300, 200));
                drawActions.Add(g => g.DrawLine(pen, 600, 600, 700, 600));

                this.Invalidate();
            }



        }


        private void button2_Click(object sender, EventArgs e)
        {
            drawActions.Clear();

            this.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFont = fontDialog.Font;
                    this.Invalidate();
                }
            }
        }
    }
}
