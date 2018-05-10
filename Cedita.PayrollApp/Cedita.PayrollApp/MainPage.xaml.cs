using Cedita.Payroll;
using Cedita.Payroll.Engines;
using Cedita.Payroll.Engines.NationalInsurance;
using Cedita.Payroll.Engines.Paye;
using Cedita.Payroll.Models.TaxYearSpecifics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cedita.PayrollApp
{
	public partial class MainPage : ContentPage
	{
        public string TaxCode { get; set; } = "1185L";
        private decimal grossSalary = 300;
        public decimal GrossSalary {
            get => grossSalary;
            set { grossSalary = value; RefreshPayroll(); }
        }
        public string GrossFrequency { get; set; } = "Monthly";

        public decimal PayeTax { get; set; }
        public decimal EeNi { get; set; }
        public decimal ErNi { get; set; }
        public decimal EePension { get; set; }
        public decimal ErPension { get; set; }
        public decimal NetPay => GrossSalary - PayeTax - EeNi;// - EePension;

        public MainPage()
		{
			InitializeComponent();
            BindingContext = this;
		}

        private void RefreshPayroll()
        {
            var tys = new JsonTaxYearSpecificProvider();
            var paye = DefaultEngineResolver.GetEngine<IPayeCalculationEngine>(2018);
            paye.SetTaxYearSpecificsProvider(tys);
            paye.SetTaxYear(2018);

            var ni = DefaultEngineResolver.GetEngine<INiCalculationEngine>(2018);
            ni.SetTaxYearSpecificsProvider(tys);
            ni.SetTaxYear(2018);

            var penLower = tys.GetSpecificValue<decimal>(TaxYearSpecificValues.PensionLowerThreshold);
            var penUpper = tys.GetSpecificValue<decimal>(TaxYearSpecificValues.PensionUpperThreshold);
            var penLowerPeriod = TaxMath.Factor(penLower, 1, 52);
            var penUpperPeriod = TaxMath.Factor(penUpper, 1, 52);

            var grossForPaye = GrossSalary;

            var pensionableEarnings = Math.Max(0, Math.Min(GrossSalary, penUpperPeriod) - penLowerPeriod);
            EePension = Math.Round(pensionableEarnings * 0.03m, 2, MidpointRounding.AwayFromZero);
            grossForPaye -= EePension;
            ErPension = Math.Round(pensionableEarnings * 0.02m, 2, MidpointRounding.AwayFromZero);


            PayeTax = paye.CalculateTaxDueForPeriod(TaxCode, grossForPaye, PayPeriods.Weekly, 1);
            var niCalc = ni.CalculateNationalInsurance(GrossSalary, 'A', PayPeriods.Weekly);
            EeNi = niCalc.EmployeeNi;
            ErNi = niCalc.EmployerNi;

            OnPropertyChanged(nameof(PayeTax));
            OnPropertyChanged(nameof(EeNi));
            OnPropertyChanged(nameof(EePension));
            OnPropertyChanged(nameof(ErNi));
            OnPropertyChanged(nameof(NetPay));
        }

        private void CeditaTapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.cedita.com/"));
        }

        /*

        public Thickness HeaderPadding
        {
            get
            {
                return new Thickness(0, Math.Max(5, 20 - ScrollContainer.ScrollY), 0, Math.Max(5, 20 - ScrollContainer.ScrollY));
            }
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            OnPropertyChanged(nameof(HeaderPadding));
            Header.BeginRefresh();
        }*/
    }
}
