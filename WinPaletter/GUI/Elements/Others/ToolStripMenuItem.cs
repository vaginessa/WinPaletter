﻿using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [Description("ToolStripMenuItem fixed to respect dark/light mode")]
    public class ToolStripMenuItem : System.Windows.Forms.ToolStripMenuItem
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.TextRenderingHint = Program.Style.RenderingHint;
            e.Graphics.Clear(BackColor);
            using (SolidBrush br = new(ForeColor))
            {
                e.Graphics.DrawString(Text, Font, br, new Rectangle(0, 0, Width, Height), base.TextAlign.ToStringFormat());
            }
        }
    }
}