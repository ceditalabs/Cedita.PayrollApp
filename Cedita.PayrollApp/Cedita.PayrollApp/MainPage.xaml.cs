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
using System.Windows.Input;
using Xamarin.Forms;

namespace Cedita.PayrollApp
{
    public partial class MainPage : ContentPage
    {
        public bool Tab1Visible { get; set; }
        public bool Tab2Visible { get; set; } = true;
        public bool Tab3Visible { get; set; }
        public bool Tab4Visible { get; set; }

        ICommand tapCommand;
        public ICommand TabTappedCommand
        {
            get { return tapCommand; }
        }

        public void OnTapped(object s)
        {
            var tabId = s.ToString();
            if (tabId == "1")
            {
                Tab1Visible = true;
                Tab2Visible = false;
                Tab3Visible = false;
                Tab4Visible = false;
            } else if (tabId == "2")
            {
                Tab1Visible = false;
                Tab2Visible = true;
                Tab3Visible = false;
                Tab4Visible = false;
            }
            else if (tabId == "3")
            {
                Tab1Visible = false;
                Tab2Visible = false;
                Tab3Visible = true;
                Tab4Visible = false;
            }
            else
            {
                Tab1Visible = false;
                Tab2Visible = false;
                Tab3Visible = false;
                Tab4Visible = true;
            }

            OnPropertyChanged(nameof(Tab1Visible));
            OnPropertyChanged(nameof(Tab2Visible));
            OnPropertyChanged(nameof(Tab3Visible));
            OnPropertyChanged(nameof(Tab4Visible));
        }

        public string TaxCode { get; set; } = "1185L";
        private decimal grossSalary = 300;
        public decimal GrossSalary {
            get => grossSalary;
            set { grossSalary = value; RefreshPayroll(); }
        }
        private string grossFrequency = "Weekly";
        public string GrossFrequency
        {
            get => grossFrequency;
            set {
                grossFrequency = value;
                OnPropertyChanged(nameof(GrossFrequency));
                OnPropertyChanged(nameof(FrequencyText));
                RefreshPayroll();
            }
        }

        public string FrequencyText
        {
            get
            {
                if (GrossFrequency == "Yearly") return string.Empty;

                return $"That's around £{PayeGross:N2} annually";
            }
        }

        public decimal PayeGross
        {
            get
            {
                switch(GrossFrequency)
                {
                    case "Daily":
                        return grossSalary * 260m;

                    case "Weekly":
                        return grossSalary * 52m;

                    case "Monthly":
                        return grossSalary * 12m;

                    default: return grossSalary;
                }
            }
        }

        public decimal PayeGrossMonthly { get => PayeGross / 12m; }
        public decimal PayeGrossWeekly { get => PayeGross / 52m; }
        public decimal PayeTax { get; set; }
        public decimal EeNi { get; set; }
        public decimal ErNi { get; set; }
        public decimal EePension { get; set; }
        public decimal ErPension { get; set; }
        public decimal NetPay => PayeGrossMonthly - PayeTax - EeNi;// - EePension;

        public decimal WeeklyPayeTax { get; set; }
        public decimal WeeklyEeNi { get; set; }
        public decimal WeeklyNetPay => PayeGrossWeekly - WeeklyPayeTax - WeeklyEeNi;


        public decimal AnnualPayeTax => PayeTax * 12m;
        public decimal AnnualEeNi => EeNi * 12m;
        public decimal AnnualNetPay => PayeGross - AnnualPayeTax - AnnualEeNi;

        public MainPage()
        {
            tapCommand = new Command(OnTapped);
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

            var grossForPaye = PayeGross / 12m;

            var pensionableEarnings = Math.Max(0, Math.Min(grossForPaye, penUpperPeriod) - penLowerPeriod);
            EePension = Math.Round(pensionableEarnings * 0.03m, 2, MidpointRounding.AwayFromZero);
            grossForPaye -= EePension;
            ErPension = Math.Round(pensionableEarnings * 0.02m, 2, MidpointRounding.AwayFromZero);


            PayeTax = paye.CalculateTaxDueForPeriod(TaxCode, grossForPaye, PayPeriods.Monthly, 1);
            var niCalc = ni.CalculateNationalInsurance(grossForPaye, 'A', PayPeriods.Monthly);
            EeNi = niCalc.EmployeeNi;
            ErNi = niCalc.EmployerNi;

            WeeklyPayeTax = paye.CalculateTaxDueForPeriod(TaxCode, PayeGrossWeekly, PayPeriods.Weekly, 1);
            niCalc = ni.CalculateNationalInsurance(PayeGrossWeekly, 'A', PayPeriods.Weekly);
            WeeklyEeNi = niCalc.EmployeeNi;

            OnPropertyChanged(nameof(FrequencyText));
            OnPropertyChanged(nameof(PayeTax));
            OnPropertyChanged(nameof(EeNi));
            OnPropertyChanged(nameof(EePension));
            OnPropertyChanged(nameof(GrossSalary));
            OnPropertyChanged(nameof(PayeGrossMonthly));
            OnPropertyChanged(nameof(ErNi));
            OnPropertyChanged(nameof(NetPay));
            OnPropertyChanged(nameof(WeeklyPayeTax));
            OnPropertyChanged(nameof(PayeGross));
            OnPropertyChanged(nameof(PayeGrossWeekly));
            OnPropertyChanged(nameof(WeeklyEeNi));
            OnPropertyChanged(nameof(WeeklyNetPay));
            OnPropertyChanged(nameof(AnnualEeNi));
            OnPropertyChanged(nameof(AnnualPayeTax));
            OnPropertyChanged(nameof(AnnualNetPay));
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
