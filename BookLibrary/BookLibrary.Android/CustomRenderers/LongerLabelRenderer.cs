using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BookLibrary.Components;
using BookLibrary.Droid.CustomRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Label), typeof(LongerLabelRenderer))]
namespace BookLibrary.Droid.CustomRenderers
{
    public class LongerLabelRenderer : LabelRenderer
    {
        public LongerLabelRenderer(Context context) : base(context) { }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e) 
        { 
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                TextView textView = this.Control as Android.Widget.TextView;
                textView.SetSingleLine(false);
                textView.SetMaxLines(int.MaxValue);
            }
        }
    }
}