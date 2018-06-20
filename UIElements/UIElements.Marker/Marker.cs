using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIElements.Marker
{
    public partial class Marker: UserControl
    {
        public string pathToChallenge { get; set; }

        public int SecondAdded { get; set; }

        public int SecondsFromStart { get; set; }

        public Marker(string pathToChallenge)
        {
            this.InitializeComponent();
            this.pathToChallenge = pathToChallenge;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics graphics = this.CreateGraphics();
            Pen pen = new Pen(Color.Black);
            pen.Width = 5f;
            graphics.DrawLine(pen, 0, this.Height, 0, 0);
            pen.Dispose();
            graphics.Dispose();
        }
    }
}
