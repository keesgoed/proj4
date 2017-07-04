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
using MySql.Data.MySqlClient;
using System.Data;

namespace Moviestar.Droid.ViewModels
{
    class LoginViewModel
    {
        private object con;

        public LoginViewModel() { }

        public void storeUserSession()
        {
            // store user logged in session
        }

    }
}