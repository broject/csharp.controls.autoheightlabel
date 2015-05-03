using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace csharp.controls.autoheightlabel
{
    public class AutoHeightLabel : Label
    {
        StringFormat _fmt = new StringFormat(StringFormat.GenericTypographic);
        Rectangle _rc;
        Size oldClientSize;

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            measureHeight();
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            measureHeight();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            measureHeight();
        }

        protected override void OnAutoSizeChanged(EventArgs e)
        {
            base.OnAutoSizeChanged(e);
            measureHeight();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            measureHeight();
        }

        protected override void OnTextAlignChanged(EventArgs e)
        {
            base.OnTextAlignChanged(e);
            measureHeight();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Pen border = new Pen(ForeColor, 4);
            e.Graphics.DrawRectangle(border, this.ClientRectangle);
            border.Dispose();

            if (oldClientSize != ClientSize || _rc == null)
            {
                _rc = new Rectangle(new Point(Padding.Left, Padding.Top)
                , new Size(ClientSize.Width - Padding.Left - Padding.Right
                    , ClientSize.Height - Padding.Top - Padding.Bottom));
                oldClientSize = ClientSize;
            }

            float padx = ((float)this.Size.Width) * (0.05F);
            float pady = ((float)this.Size.Height) * (0.05F);

            using (var br = new SolidBrush(this.ForeColor))
            {
                e.Graphics.DrawString(this.Text, this.Font, br, _rc, _fmt);
            }
        }

        private void measureHeight()
        {
            switch (TextAlign)
            {
                default:
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    _fmt.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    _fmt.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    _fmt.Alignment = StringAlignment.Far;
                    break;
            }

            using (Graphics g = CreateGraphics())
            {
                SizeF size = g.MeasureString(Text, Font, Width - Padding.Left - Padding.Right, _fmt);
                this.Height = (int)Math.Ceiling(size.Height) + Padding.Top + Padding.Bottom;
            }
        }
    }
}
