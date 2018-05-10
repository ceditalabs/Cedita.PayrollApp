using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cedita.PayrollApp.Controls
{
    public class CurrencyEditor : Editor
    {
        public CurrencyEditor()
        {
            Keyboard = Keyboard.Numeric;
            TextChanged += OnTextChanged;
        }

        ~CurrencyEditor()
        {
            TextChanged -= OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // If we have more than 2 decimal places, strip that
            if (e.NewTextValue.Contains(".") && e.NewTextValue.Substring(e.NewTextValue.IndexOf('.')).Length > 3)
            {
                Text = e.NewTextValue.Substring(0, e.NewTextValue.IndexOf('.') + 3);
            }
            if (decimal.TryParse(e.NewTextValue, out decimal newValue))
            {
                if (newValue > 999999.99m)
                {
                    Text = "999999.99";
                }

                InvalidateMeasure();
            }
        }
    }
}
