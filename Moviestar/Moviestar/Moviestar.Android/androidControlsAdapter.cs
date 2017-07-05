using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Moviestar.Interfaces;

namespace Moviestar.Droid
{
    public class androidControlsAdapter : IControls
    {
        androidControls androidControls;

        public androidControlsAdapter(androidControls controls)
        {
            androidControls = controls;
        }
   
        public void selectControl()
        { 
            var touch = new androidControls.androidTouch();  
        }

        public void scrollControl()
        {
            var scroll = new androidControls.androidScroll();
        }

        public void keyboardControl()
        {
            var keyboard = new androidControls.androidKeyboard();
        }


    }
}