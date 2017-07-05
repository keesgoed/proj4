using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Input;


namespace Moviestar.UWP
{
    public class windowsControls : EventArgs
    {
        public class windowsClick : windowsControls
        {
            public bool OnClick(object sender, MouseEventArgs e)
            {
                return true;
            }
        }

        public class windowsKeyboard
        {
            public bool OnKeyboard()
            {
                return true;
            }
        }

        public class windowsScroll
        {
            public bool OnScroll(object sender, MouseEventArgs e)
            {
                return true;
            }
        }
    }
}
