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

[assembly: ExportRenderer(typeof(CurrencyEditor), typeof(CurrencyEditorRenderer))]
namespace Cedita.PayrollApp.Android.Controls.Renderers
{
    public class CurrencyEditorRenderer : EditorRenderer
    {
        public CurrencyEditorRenderer(Context context) : base(context)
        {
        }

        public static void Init() { }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Gravity = GravityFlags.Center;

                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
            }
        }
    }
}