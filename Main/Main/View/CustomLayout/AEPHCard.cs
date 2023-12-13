using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Main.View.CustomLayout
{
    public partial class AEPHCard : UserControl
    {
        /// <summary>
        /// A cor do retangulo superior.
        /// </summary>
        public Color TopColor { get; set; }
        /// <summary>
        /// Altura do retangulo superior
        /// </summary>
        public float TopHeight { get; set; }
        /// <summary>
        /// Cor da Borda do componente
        /// </summary>
        public Color BorderColor { get; set; }

        public AEPHCard()
        {
            InitializeComponent();
            this.BackColor = Color.Transparent;
            BorderColor = Color.Black;
            this.ResizeRedraw = true;
            this.AutoSize = false;
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// Essa função desenha o objeto novamente, colocando a borda arredondada no objeto
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            GraphicsPath path = new GraphicsPath();
            int radius = 20;
            int diameter = radius * 2;
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1); // subtrair 1 do Width e Height para evitar que a borda seja cortada
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90); // canto superior esquerdo
            path.AddArc(rect.X + rect.Width - diameter, rect.Y, diameter, diameter, 270, 90); // canto superior direito
            path.AddArc(rect.X + rect.Width - diameter, rect.Y + rect.Height - diameter, diameter, diameter, 0, 90); // canto inferior direito
            path.AddArc(rect.X, rect.Y + rect.Height - diameter, diameter, diameter, 90, 90); // canto inferior esquerdo
            path.CloseAllFigures();

            this.Region = new Region(path);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawPath(new Pen(BorderColor, 2), path);

            e.Graphics.DrawLine(new Pen(TopColor, TopHeight), 0, 0, this.Width, 0);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate(); // forçar o redesenho do controle quando o tamanho é alterado
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            Invalidate(); // forçar o redesenho do controle quando a posição é alterada
        }

    }
}
