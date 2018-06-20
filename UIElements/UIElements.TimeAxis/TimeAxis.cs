using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIElements.TimeAxis
{
    public partial class TimeAxis : UserControl
    {
        private List<Marker.Marker> markers;
        private int elapsedTimeFromStart;
        private ToolTip toolTip;

        public string ElapsedTimeFromStart
        {
            get
            {
                return this.ConvertSecondsToStringTimestamp(this.elapsedTimeFromStart);
            }
        }

        public TimeAxis()
        {
            this.InitializeComponent();
            this.markers = new List<Marker.Marker>();
            this.elapsedTimeFromStart = 0;
            this.toolTip = new ToolTip();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            this.CreateGraphics().DrawLine(new Pen(Color.Gray)
            {
                Width = 2f
            }, 0, 10, this.Width, 10);
        }

        public void AddMarkerOnTimeAxis(Marker.Marker marker)
        {
            this.CompleteMarkerInitialization(marker);
            this.AddMarkerToList(marker);
            this.AddMarkerToForm(this.markers.Last<Marker.Marker>());
        }

        private void CompleteMarkerInitialization(Marker.Marker marker)
        {
            marker.Location = new Point(this.Width, this.Location.Y + 3);
            marker.SecondsFromStart = 1;
            marker.SecondAdded = this.elapsedTimeFromStart;
            marker.MouseHover += new EventHandler(this.Marker_MouserHovered);
        }

        private void Marker_MouserHovered(object sender, EventArgs e)
        {
            Marker.Marker marker = (Marker.Marker)sender;
            string caption = "Added time - " + this.ConvertSecondsToStringTimestamp(marker.SecondAdded);
            this.toolTip.SetToolTip((Control)marker, caption);
        }

        private string ConvertSecondsToStringTimestamp(int seconds)
        {
            return TimeSpan.FromSeconds((double)seconds).ToString("hh\\:mm\\:ss");
        }

        private void AddMarkerToList(Marker.Marker marker)
        {
            this.markers.Add(marker);
        }

        private void AddMarkerToForm(Marker.Marker marker)
        {
            this.Parent.Controls.Add((Control)marker);
        }

        private void DrawMarkersList()
        {
            foreach (Marker.Marker marker in this.markers)
            {
                this.Parent.Controls.Add((Control)marker);
                marker.BringToFront();
            }
        }

        private void RecalculateMarkersPositions(int elapsedTimeFromStart)
        {
            foreach (Marker.Marker marker in this.markers)
            {
                float num = (float)this.Width * ((float)marker.SecondsFromStart / (float)elapsedTimeFromStart);
                ++marker.SecondsFromStart;
                marker.Location = new Point(this.Width - (int)num, this.Location.Y + 3);
            }
        }

        private void IncreaseTimeFromStart()
        {
            this.elapsedTimeFromStart = this.elapsedTimeFromStart + 1;
        }

        public void UpdateTimeAxis()
        {
            this.IncreaseTimeFromStart();
            this.RecalculateMarkersPositions(this.elapsedTimeFromStart);
            this.DrawMarkersList();
        }

        public void Reset()
        {
            this.elapsedTimeFromStart = 0;
            foreach (Control marker in this.markers)
                this.Parent.Controls.Remove(marker);
            this.markers.Clear();
        }
    }
}
