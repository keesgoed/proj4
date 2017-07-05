using Moviestar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moviestar.UWP
{
    class windowsControlsAdapter : IControls
    {
        windowsControls windowsControls;

        public windowsControlsAdapter(windowsControls controls)
        {
            windowsControls = controls;
        }

        public void keyboardControl()
        {
            var keyboard = new windowsControls.windowsKeyboard();
        }

        public void selectControl()
        {
            var click = new windowsControls.windowsClick();
        }

        public void scrollControl()
        {
            var scroll = new windowsControls.windowsScroll();
        }

    }
}
    



