using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Graphics;
using Android.Views.InputMethods;
using BookLibrary.Components;
using BookLibrary.Droid.CustomRenderers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AndroidX.Core.Content;
using Android.Runtime;
using Android.Widget;
using Android.OS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace BookLibrary.Droid.CustomRenderers
{
    class CustomEntryRenderer : EntryRenderer
    {
        CustomEntry element;
        public CustomEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            element = (CustomEntry)this.Element;


            var editText = this.Control;
            if (!string.IsNullOrEmpty(element.Image))
            {
                switch (element.ImageAlignment)
                {
                    case ImageAlignment.Left:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.Image), null, null, null);
                        break;
                    case ImageAlignment.Right:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(null, null, GetDrawable(element.Image), null);
                        break;
                }
            }
            editText.CompoundDrawablePadding = 25;
            Control.ImeOptions = (ImeAction)ImeFlags.NoExtractUi;
#pragma warning disable CS0618 // Type or member is obsolete
            Control.Background.SetColorFilter(element.LineColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
#pragma warning restore CS0618 // Type or member is obsolete

            if (element.IsCurvedCornersEnabled)
            {
                // creating gradient drawable for the curved background  
                var _gradientBackground = new GradientDrawable();
                _gradientBackground.SetShape(ShapeType.Rectangle);
                _gradientBackground.SetColor(element.BackgroundColor.ToAndroid());

                // Thickness of the stroke line  
                _gradientBackground.SetStroke(element.BorderWidth, element.BorderColor.ToAndroid());

                // Radius for the curves  
                _gradientBackground.SetCornerRadius(
                    DpToPixels(this.Context, Convert.ToSingle(element.CornerRadius)));

                // set the background of the   
                Control.SetBackground(_gradientBackground);
            }

            //set cursor color

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
            {
                Control.SetTextCursorDrawable(0); //This API Intrduced in android 10
            }
            else
            {
                IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
                JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, 0);
            }
            // Set padding for the internal text from border  
            Control.SetPadding(
                    (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingTop,
                    (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingBottom);
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }

        private BitmapDrawable GetDrawable(string imageEntryImage)
        {
            int resID = Resources.GetIdentifier(imageEntryImage, "drawable", this.Context.PackageName);
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, element.ImageWidth * 2, element.ImageHeight * 2, true));
        }
    }
}