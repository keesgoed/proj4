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
using Android.Views.InputMethods;

namespace Moviestar.Droid
{
    public class androidControls : GestureDetector.SimpleOnGestureListener
    {

        public class androidTouch : androidControls
        {
            public override bool OnDown(MotionEvent e)
            {
                return true;
            }
        }

        public class androidScroll : androidControls
        {
            public override bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
            {
                return base.OnScroll(e1, e2, distanceX, distanceY);
            }
        }


        public class androidKeyboard : androidControls
        {
            public bool keyboard()
            {
                return true;
            }

        }
    }
}