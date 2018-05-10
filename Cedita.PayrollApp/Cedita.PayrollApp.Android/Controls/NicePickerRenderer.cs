using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cedita.PayrollApp.Android.Controls.Renderers;
using Cedita.PayrollApp.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NicePicker), typeof(NicePickerRenderer))]
namespace Cedita.PayrollApp.Android.Controls.Renderers
{
    public class NicePickerRenderer : PickerRenderer
    {
        public NicePickerRenderer(Context context) : base(context)
        {
        }

        public static void Init() { }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                //Control.Background = Context.GetDrawable("downarrow");
                Control.Background = GetLayeredBg();

                Control.Gravity = GravityFlags.Center;

                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(16, 8, 52, 8);
                SetPadding(0, 0, 0, 0);
            }
        }
        private LayerDrawable GetLayeredBg()
        {
            var border = new ShapeDrawable();
            border.Paint.Color = global::Android.Graphics.Color.Argb(175, 255, 255, 255);
            border.SetPadding(0, 0, -6, 0);
            border.Paint.SetStyle(Paint.Style.Stroke);

            Drawable[] layers = { border, Context.GetDrawable("downarrow") };
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

            return layerDrawable;
        }
    }
}