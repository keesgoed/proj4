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

namespace Moviestar.Droid
{
    interface IMovies<T> 
    {
        IOption<T> GetNext();
    }

    interface IOption<T>
    {

    }

    class Movie<T> : IMovies<T>
    {
        public IOption<T> GetNext()
        {
            throw new NotImplementedException();
        }
    }
}