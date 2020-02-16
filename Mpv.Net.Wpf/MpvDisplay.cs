using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;

namespace Mpv.Net.Wpf
{
    /// <summary>
    /// Mpv display. Video will be rendered to this control
    /// </summary>
    public class MpvDisplay : Control
    {
        static MpvDisplay()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MpvDisplay), new FrameworkPropertyMetadata(typeof(MpvDisplay)));
        }

        public override void OnApplyTemplate()
        {
            if (GetTemplateChild("PART_Host") is WindowsFormsHost host
                && host.Child is System.Windows.Forms.Panel panel)
            {
                panel.BackColor = System.Drawing.Color.Black;
            }
        }

        public IntPtr DisplayHandle
        {
            get
            {
                if (GetTemplateChild("PART_Host") is WindowsFormsHost host
                    && host.Child is System.Windows.Forms.Panel panel)
                {
                    return panel.Handle;
                }
                return IntPtr.Zero;
            }
        }
    }
}
