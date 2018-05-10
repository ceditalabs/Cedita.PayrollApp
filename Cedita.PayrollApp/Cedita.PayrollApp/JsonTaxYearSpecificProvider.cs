using Cedita.Payroll.Engines;
using System;
using Cedita.Payroll.Models.TaxYearSpecifics;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

using System;
using System.Collections.Generic;
using System.Text;
using Cedita.Payroll;

namespace Cedita.PayrollApp
{
    public class JsonConfigTaxYear
    {
        public int TaxYear { get; set; }
        public string DefaultTaxCode { get; set; }
        public decimal LowerEarningsLimit { get; set; }
        public decimal UpperEarningsLimit { get; set; }
        public decimal PrimaryThreshold { get; set; }
        public decimal SecondaryThreshold { get; set; }
        public decimal UpperAccrualPoint { get; set; }
        public decimal UpperSecondaryThreshold { get; set; }
        public decimal ApprenticeUpperSecondaryThreshold { get; set; }
        public decimal Plan1StudentLoanThreshold { get; set; }
        public decimal Plan1StudentLoanRate { get; set; }
        public decimal Plan2StudentLoanThreshold { get; set; }
        public decimal Plan2StudentLoanRate { get; set; }
        public decimal DeaProtectedEarnings { get; set; }
        public decimal PensionLowerThreshold { get; set; }
        public decimal PensionAutomaticEnrolment { get; set; }
        public decimal PensionUpperThreshold { get; set; }

        public List<FixedCode> FixedCodes { get; set; }
        public List<FixedCode> ScottishFixedCodes { get; set; }
        public List<NationalInsuranceCode> NiRates { get; set; }
        public List<TaxBracket> Brackets { get; set; }
        public List<TaxBracket> ScottishBrackets { get; set; }
    }
    /// <summary>
    /// JSON Tax Year Provider for Cedita.Payroll
    /// </summary>
    public class JsonTaxYearSpecificProvider : IProvideTaxYearSpecifics
    {
        protected string Json { get; } = @"[
  {
    ""TaxYear"": 2012,
    ""DefaultTaxCode"": ""810L"",

    ""LowerEarningsLimit"": 5564,
    ""UpperEarningsLimit"": 42475,
    ""PrimaryThreshold"": 7605,
    ""SecondaryThreshold"": 7605,
    ""UpperAccrualPoint"": 40040,
    ""UpperSecondaryThreshold"": 0,
    ""ApprenticeUpperSecondaryThreshold"": 0,

    ""Plan1StudentLoanThreshold"": 15795,
    ""Plan1StudentLoanRate"": 0.09,
    ""Plan2StudentLoanThreshold"": 0,
    ""Plan2StudentLoanRate"": 0.09,

    ""DeaProtectedEarnings"": 0.6,

    ""PensionLowerThreshold"": 5564,
    ""PensionAutomaticEnrolment"": 8105,
    ""PensionUpperThreshold"": 42475,

    ""FixedCodes"": [
      {
        ""Code"": ""BR"",
        ""Rate"": 0.2
      },
      {
        ""Code"": ""D0"",
        ""Rate"": 0.4
      },
      {
        ""Code"": ""D1"",
        ""Rate"": 0.5
      },
      {
        ""Code"": ""N1"",
        ""Rate"": 0
      },
      {
        ""Code"": ""NT"",
        ""Rate"": 0
      }
    ],
    ""NiRates"": [
      {
        ""Code"": ""A"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 12,
        ""EeE"": 12,
        ""EeF"": 2,
        ""ErB"": 0,
        ""ErC"": 13.8,
        ""ErD"": 13.8,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""B"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 5.85,
        ""EeE"": 5.85,
        ""EeF"": 2,
        ""ErB"": 0,
        ""ErC"": 13.8,
        ""ErD"": 13.8,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""C"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 0,
        ""EeE"": 0,
        ""EeF"": 0,
        ""ErB"": 0,
        ""ErC"": 13.8,
        ""ErD"": 13.8,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""D"",
        ""EeB"": 1.4,
        ""EeC"": 1.4,
        ""EeD"": 10.6,
        ""EeE"": 12,
        ""EeF"": 2,
        ""ErB"": 3.4,
        ""ErC"": 10.4,
        ""ErD"": 10.4,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""E"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 5.85,
        ""EeE"": 5.85,
        ""EeF"": 2,
        ""ErB"": 3.4,
        ""ErC"": 10.4,
        ""ErD"": 10.4,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""J"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 2,
        ""EeE"": 2,
        ""EeF"": 2,
        ""ErB"": 0,
        ""ErC"": 13.8,
        ""ErD"": 13.8,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""L"",
        ""EeB"": 1.4,
        ""EeC"": 1.4,
        ""EeD"": 2,
        ""EeE"": 2,
        ""EeF"": 2,
        ""ErB"": 3.4,
        ""ErC"": 10.4,
        ""ErD"": 10.4,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      }
    ],
    ""Brackets"": [
      {
        ""From"": 0,
        ""To"": 34370,
        ""Multiplier"": 0.2
      },
      {
        ""From"": 34370,
        ""To"": 150000,
        ""Multiplier"": 0.4
      },
      {
        ""From"": 150000,
        ""To"": 2147483647,
        ""Multiplier"": 0.5
      }
    ]
  },
  {
    ""TaxYear"": 2013,
    ""DefaultTaxCode"": ""944L"",

    ""LowerEarningsLimit"": 5668,
    ""UpperEarningsLimit"": 41450,
    ""PrimaryThreshold"": 7755,
    ""SecondaryThreshold"": 7696,
    ""UpperAccrualPoint"": 40040,
    ""UpperSecondaryThreshold"": 0,
    ""ApprenticeUpperSecondaryThreshold"": 0,

    ""Plan1StudentLoanThreshold"": 16365,
    ""Plan1StudentLoanRate"": 0.09,
    ""Plan2StudentLoanThreshold"": 21000,
    ""Plan2StudentLoanRate"": 0.09,

    ""DeaProtectedEarnings"": 0.6,

    ""PensionLowerThreshold"": 5668,
    ""PensionAutomaticEnrolment"": 9440,
    ""PensionUpperThreshold"": 41450,

    ""FixedCodes"": [
      {
        ""Code"": ""BR"",
        ""Rate"": 0.2
      },
      {
        ""Code"": ""D0"",
        ""Rate"": 0.4
      },
      {
        ""Code"": ""D1"",
        ""Rate"": 0.45
      },
      {
        ""Code"": ""N1"",
        ""Rate"": 0
      },
      {
        ""Code"": ""NT"",
        ""Rate"": 0
      }
    ],
    ""NiRates"": [
      {
        ""Code"": ""A"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 12,
        ""EeE"": 12,
        ""EeF"": 2,
        ""ErB"": 0,
        ""ErC"": 13.8,
        ""ErD"": 13.8,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""B"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 5.85,
        ""EeE"": 5.85,
        ""EeF"": 2,
        ""ErB"": 0,
        ""ErC"": 13.8,
        ""ErD"": 13.8,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""C"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 0,
        ""EeE"": 0,
        ""EeF"": 0,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""D"",
        ""EeB"": 1.4,
        ""EeC"": 1.4,
        ""EeD"": 10.6,
        ""EeE"": 12,
        ""EeF"": 2,
        ""ErB"": 3.4,
        ""ErC"": 10.4,
        ""ErD"": 10.4,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""E"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 5.85,
        ""EeE"": 5.85,
        ""EeF"": 2,
        ""ErB"": 3.4,
        ""ErC"": 10.4,
        ""ErD"": 10.4,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""J"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 2,
        ""EeE"": 2,
        ""EeF"": 2,
        ""ErB"": 0,
        ""ErC"": 13.8,
        ""ErD"": 13.8,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""L"",
        ""EeB"": 1.4,
        ""EeC"": 1.4,
        ""EeD"": 2,
        ""EeE"": 2,
        ""EeF"": 2,
        ""ErB"": 3.4,
        ""ErC"": 10.4,
        ""ErD"": 10.4,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      }
    ],
    ""Brackets"": [
      {
        ""From"": 0,
        ""To"": 32010,
        ""Multiplier"": 0.2
      },
      {
        ""From"": 32010,
        ""To"": 150000,
        ""Multiplier"": 0.4
      },
      {
        ""From"": 150000,
        ""To"": 2147483647,
        ""Multiplier"": 0.45
      }
    ]
  },
  {
    ""TaxYear"": 2014,
    ""DefaultTaxCode"": ""1000L"",

    ""LowerEarningsLimit"": 5772,
    ""UpperEarningsLimit"": 41865,
    ""PrimaryThreshold"": 7956,
    ""SecondaryThreshold"": 7956,
    ""UpperAccrualPoint"": 40040,
    ""UpperSecondaryThreshold"": 0,
    ""ApprenticeUpperSecondaryThreshold"": 0,

    ""Plan1StudentLoanThreshold"": 16910,
    ""Plan1StudentLoanRate"": 0.09,
    ""Plan2StudentLoanThreshold"": 21000,
    ""Plan2StudentLoanRate"": 0.09,

    ""DeaProtectedEarnings"": 0.6,

    ""PensionLowerThreshold"": 5772,
    ""PensionAutomaticEnrolment"": 10000,
    ""PensionUpperThreshold"": 41865,

    ""FixedCodes"": [
      {
        ""Code"": ""BR"",
        ""Rate"": 0.2
      },
      {
        ""Code"": ""D0"",
        ""Rate"": 0.4
      },
      {
        ""Code"": ""D1"",
        ""Rate"": 0.45
      },
      {
        ""Code"": ""N1"",
        ""Rate"": 0
      },
      {
        ""Code"": ""NT"",
        ""Rate"": 0
      }
    ],
    ""NiRates"": [
      {
        ""Code"": ""A"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 12,
        ""EeE"": 12,
        ""EeF"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""B"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 5.85,
        ""EeE"": 5.85,
        ""EeF"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""C"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 0,
        ""EeE"": 0,
        ""EeF"": 0,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""D"",
        ""EeB"": 1.4,
        ""EeC"": 1.4,
        ""EeD"": 10.6,
        ""EeE"": 12,
        ""EeF"": 2,
        ""ErB"": 3.4,
        ""ErC"": 3.4,
        ""ErD"": 10.4,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""E"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 5.85,
        ""EeE"": 5.85,
        ""EeF"": 2,
        ""ErB"": 3.4,
        ""ErC"": 3.4,
        ""ErD"": 10.4,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""J"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 2,
        ""EeE"": 2,
        ""EeF"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""L"",
        ""EeB"": 1.4,
        ""EeC"": 1.4,
        ""EeD"": 2,
        ""EeE"": 2,
        ""EeF"": 2,
        ""ErB"": 3.4,
        ""ErC"": 3.4,
        ""ErD"": 10.4,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      }
    ],
    ""Brackets"": [
      {
        ""From"": 0,
        ""To"": 31865,
        ""Multiplier"": 0.2
      },
      {
        ""From"": 31865,
        ""To"": 150000,
        ""Multiplier"": 0.4
      },
      {
        ""From"": 150000,
        ""To"": 2147483647,
        ""Multiplier"": 0.45
      }
    ]
  },
  {
    ""TaxYear"": 2015,
    ""DefaultTaxCode"": ""1060L"",

    ""LowerEarningsLimit"": 5824,
    ""UpperEarningsLimit"": 42385,
    ""PrimaryThreshold"": 8060,
    ""SecondaryThreshold"": 8112,
    ""UpperAccrualPoint"": 40040,
    ""UpperSecondaryThreshold"": 0,
    ""ApprenticeUpperSecondaryThreshold"": 0,

    ""Plan1StudentLoanThreshold"": 17335,
    ""Plan1StudentLoanRate"": 0.09,
    ""Plan2StudentLoanThreshold"": 21000,
    ""Plan2StudentLoanRate"": 0.09,

    ""DeaProtectedEarnings"": 0.6,

    ""PensionLowerThreshold"": 5824,
    ""PensionAutomaticEnrolment"": 10000,
    ""PensionUpperThreshold"": 42385,

    ""FixedCodes"": [
      {
        ""Code"": ""BR"",
        ""Rate"": 0.2
      },
      {
        ""Code"": ""D0"",
        ""Rate"": 0.4
      },
      {
        ""Code"": ""D1"",
        ""Rate"": 0.45
      },
      {
        ""Code"": ""N1"",
        ""Rate"": 0
      },
      {
        ""Code"": ""NT"",
        ""Rate"": 0
      }
    ],
    ""NiRates"": [
      {
        ""Code"": ""A"",
        ""EeB"": 0,
        ""EeC"": 12,
        ""EeD"": 12,
        ""EeE"": 12,
        ""EeF"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""B"",
        ""EeB"": 0,
        ""EeC"": 5.85,
        ""EeD"": 5.85,
        ""EeE"": 5.85,
        ""EeF"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""C"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 0,
        ""EeE"": 0,
        ""EeF"": 0,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""D"",
        ""EeB"": 1.4,
        ""EeC"": 10.6,
        ""EeD"": 10.6,
        ""EeE"": 12,
        ""EeF"": 2,
        ""ErB"": 3.4,
        ""ErC"": 3.4,
        ""ErD"": 10.4,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""E"",
        ""EeB"": 0,
        ""EeC"": 5.85,
        ""EeD"": 5.85,
        ""EeE"": 5.85,
        ""EeF"": 2,
        ""ErB"": 3.4,
        ""ErC"": 3.4,
        ""ErD"": 10.4,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""I"",
        ""EeB"": 1.4,
        ""EeC"": 10.6,
        ""EeD"": 10.6,
        ""EeE"": 12,
        ""EeF"": 2,
        ""ErB"": 3.4,
        ""ErC"": 3.4,
        ""ErD"": 3.4,
        ""ErE"": 0,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""J"",
        ""EeB"": 0,
        ""EeC"": 2,
        ""EeD"": 2,
        ""EeE"": 2,
        ""EeF"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""K"",
        ""EeB"": 1.4,
        ""EeC"": 2,
        ""EeD"": 2,
        ""EeE"": 2,
        ""EeF"": 2,
        ""ErB"": 3.4,
        ""ErC"": 3.4,
        ""ErD"": 3.4,
        ""ErE"": 0,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""L"",
        ""EeB"": 1.4,
        ""EeC"": 2,
        ""EeD"": 2,
        ""EeE"": 2,
        ""EeF"": 2,
        ""ErB"": 3.4,
        ""ErC"": 3.4,
        ""ErD"": 10.4,
        ""ErE"": 13.8,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""M"",
        ""EeB"": 0,
        ""EeC"": 12,
        ""EeD"": 12,
        ""EeE"": 12,
        ""EeF"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 0,
        ""ErE"": 0,
        ""ErF"": 13.8
      },
      {
        ""Code"": ""Z"",
        ""EeB"": 0,
        ""EeC"": 2,
        ""EeD"": 2,
        ""EeE"": 2,
        ""EeF"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 0,
        ""ErE"": 0,
        ""ErF"": 13.8
      }
    ],
    ""Brackets"": [
      {
        ""From"": 0,
        ""To"": 31785,
        ""Multiplier"": 0.2
      },
      {
        ""From"": 31785,
        ""To"": 150000,
        ""Multiplier"": 0.4
      },
      {
        ""From"": 150000,
        ""To"": 2147483647,
        ""Multiplier"": 0.45
      }
    ]
  },
  {
    ""TaxYear"": 2016,
    ""DefaultTaxCode"": ""1100L"",

    ""LowerEarningsLimit"": 5824,
    ""PrimaryThreshold"": 8060,
    ""SecondaryThreshold"": 8112,
    ""UpperEarningsLimit"": 43000,
    ""UpperSecondaryThreshold"": 43000,
    ""ApprenticeUpperSecondaryThreshold"": 43000,
    ""UpperAccrualPoint"": 0,

    ""Plan1StudentLoanThreshold"": 17495,
    ""Plan1StudentLoanRate"": 0.09,
    ""Plan2StudentLoanThreshold"": 21000,
    ""Plan2StudentLoanRate"": 0.09,

    ""DeaProtectedEarnings"": 0.6,

    ""PensionLowerThreshold"": 5824,
    ""PensionAutomaticEnrolment"": 10000,
    ""PensionUpperThreshold"": 43000,

    ""FixedCodes"": [
      {
        ""Code"": ""BR"",
        ""Rate"": 0.2
      },
      {
        ""Code"": ""D0"",
        ""Rate"": 0.4
      },
      {
        ""Code"": ""D1"",
        ""Rate"": 0.45
      },
      {
        ""Code"": ""N1"",
        ""Rate"": 0
      },
      {
        ""Code"": ""NT"",
        ""Rate"": 0
      }
    ],
    ""NiRates"": [
      {
        ""Code"": ""A"",
        ""EeB"": 0,
        ""EeC"": 12,
        ""EeD"": 12,
        ""EeE"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8,
        ""ErE"": 13.8
      },
      {
        ""Code"": ""B"",
        ""EeB"": 0,
        ""EeC"": 5.85,
        ""EeD"": 5.85,
        ""EeE"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8,
        ""ErE"": 13.8
      },
      {
        ""Code"": ""C"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 0,
        ""EeE"": 0,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8,
        ""ErE"": 13.8
      },
      {
        ""Code"": ""H"",
        ""EeB"": 0,
        ""EeC"": 12,
        ""EeD"": 12,
        ""EeE"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 0,
        ""ErE"": 13.8
      },
      {
        ""Code"": ""J"",
        ""EeB"": 0,
        ""EeC"": 2,
        ""EeD"": 2,
        ""EeE"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8,
        ""ErE"": 13.8
      },
      {
        ""Code"": ""M"",
        ""EeB"": 0,
        ""EeC"": 12,
        ""EeD"": 12,
        ""EeE"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 0,
        ""ErE"": 13.8
      },
      {
        ""Code"": ""Z"",
        ""EeB"": 0,
        ""EeC"": 2,
        ""EeD"": 2,
        ""EeE"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 0,
        ""ErE"": 13.8
      }
    ],
    ""Brackets"": [
      {
        ""From"": 0,
        ""To"": 32000,
        ""Multiplier"": 0.2
      },
      {
        ""From"": 32000,
        ""To"": 150000,
        ""Multiplier"": 0.4
      },
      {
        ""From"": 150000,
        ""To"": 2147483647,
        ""Multiplier"": 0.45
      }
    ]
  },
  {
    ""TaxYear"": 2017,
    ""DefaultTaxCode"": ""1150L"",

    ""LowerEarningsLimit"": 5876,
    ""PrimaryThreshold"": 8164,
    ""SecondaryThreshold"": 8164,
    ""UpperEarningsLimit"": 45000,
    ""UpperSecondaryThreshold"": 45000,
    ""ApprenticeUpperSecondaryThreshold"": 45000,

    ""Plan1StudentLoanThreshold"": 17775,
    ""Plan1StudentLoanRate"": 0.09,
    ""Plan2StudentLoanThreshold"": 21000,
    ""Plan2StudentLoanRate"": 0.09,

    ""DeaProtectedEarnings"": 0.6,

    ""PensionLowerThreshold"": 5876,
    ""PensionAutomaticEnrolment"": 10000,
    ""PensionUpperThreshold"": 45000,

    ""FixedCodes"": [
      {
        ""Code"": ""BR"",
        ""Rate"": 0.2
      },
      {
        ""Code"": ""D0"",
        ""Rate"": 0.4
      },
      {
        ""Code"": ""D1"",
        ""Rate"": 0.45
      },
      {
        ""Code"": ""N1"",
        ""Rate"": 0
      },
      {
        ""Code"": ""NT"",
        ""Rate"": 0
      }
    ],
    ""NiRates"": [
      {
        ""Code"": ""A"",
        ""EeB"": 0,
        ""EeC"": 12,
        ""EeD"": 2,
        ""ErB"": 0,
        ""ErC"": 13.8,
        ""ErD"": 13.8
      },
      {
        ""Code"": ""B"",
        ""EeB"": 0,
        ""EeC"": 5.85,
        ""EeD"": 2,
        ""ErB"": 0,
        ""ErC"": 13.8,
        ""ErD"": 13.8
      },
      {
        ""Code"": ""C"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 0,
        ""ErB"": 0,
        ""ErC"": 13.8,
        ""ErD"": 13.8
      },
      {
        ""Code"": ""H"",
        ""EeB"": 0,
        ""EeC"": 12,
        ""EeD"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8
      },
      {
        ""Code"": ""J"",
        ""EeB"": 0,
        ""EeC"": 2,
        ""EeD"": 2,
        ""ErB"": 0,
        ""ErC"": 13.8,
        ""ErD"": 13.8
      },
      {
        ""Code"": ""M"",
        ""EeB"": 0,
        ""EeC"": 12,
        ""EeD"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8
      },
      {
        ""Code"": ""Z"",
        ""EeB"": 0,
        ""EeC"": 2,
        ""EeD"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8
      },
      {
        ""Code"": ""X"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 0,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 0
      }
    ],
    ""Brackets"": [
      {
        ""From"": 0,
        ""To"": 33500,
        ""Multiplier"": 0.2
      },
      {
        ""From"": 33500,
        ""To"": 150000,
        ""Multiplier"": 0.4
      },
      {
        ""From"": 150000,
        ""To"": 2147483647,
        ""Multiplier"": 0.45
      }
    ],
    ""ScottishBrackets"": [
      {
        ""From"": 0,
        ""To"": 31500,
        ""Multiplier"": 0.2
      },
      {
        ""From"": 31500,
        ""To"": 150000,
        ""Multiplier"": 0.4
      },
      {
        ""From"": 150000,
        ""To"": 2147483647,
        ""Multiplier"": 0.45
      }
    ]
  },
  {
    ""TaxYear"": 2018,
    ""DefaultTaxCode"": ""1185L"",

    ""LowerEarningsLimit"": 6032,
    ""PrimaryThreshold"": 8424,
    ""SecondaryThreshold"": 8424,
    ""UpperEarningsLimit"": 46350,
    ""UpperSecondaryThreshold"": 46350,
    ""ApprenticeUpperSecondaryThreshold"": 46350,

    ""Plan1StudentLoanThreshold"": 18330,
    ""Plan1StudentLoanRate"": 0.09,
    ""Plan2StudentLoanThreshold"": 25000,
    ""Plan2StudentLoanRate"": 0.09,

    ""DeaProtectedEarnings"": 0.6,

    ""PensionLowerThreshold"": 6032,
    ""PensionAutomaticEnrolment"": 10000,
    ""PensionUpperThreshold"": 46350,

    ""FixedCodes"": [
      {
        ""Code"": ""BR"",
        ""Rate"": 0.2
      },
      {
        ""Code"": ""D0"",
        ""Rate"": 0.4
      },
      {
        ""Code"": ""D1"",
        ""Rate"": 0.45
      },
      {
        ""Code"": ""N1"",
        ""Rate"": 0
      },
      {
        ""Code"": ""NT"",
        ""Rate"": 0
      }
    ],
    ""NiRates"": [
      {
        ""Code"": ""A"",
        ""EeB"": 0,
        ""EeC"": 12,
        ""EeD"": 2,
        ""ErB"": 0,
        ""ErC"": 13.8,
        ""ErD"": 13.8
      },
      {
        ""Code"": ""B"",
        ""EeB"": 0,
        ""EeC"": 5.85,
        ""EeD"": 2,
        ""ErB"": 0,
        ""ErC"": 13.8,
        ""ErD"": 13.8
      },
      {
        ""Code"": ""C"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 0,
        ""ErB"": 0,
        ""ErC"": 13.8,
        ""ErD"": 13.8
      },
      {
        ""Code"": ""H"",
        ""EeB"": 0,
        ""EeC"": 12,
        ""EeD"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8
      },
      {
        ""Code"": ""J"",
        ""EeB"": 0,
        ""EeC"": 2,
        ""EeD"": 2,
        ""ErB"": 0,
        ""ErC"": 13.8,
        ""ErD"": 13.8
      },
      {
        ""Code"": ""M"",
        ""EeB"": 0,
        ""EeC"": 12,
        ""EeD"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8
      },
      {
        ""Code"": ""Z"",
        ""EeB"": 0,
        ""EeC"": 2,
        ""EeD"": 2,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 13.8
      },
      {
        ""Code"": ""X"",
        ""EeB"": 0,
        ""EeC"": 0,
        ""EeD"": 0,
        ""ErB"": 0,
        ""ErC"": 0,
        ""ErD"": 0
      }
    ],
    ""Brackets"": [
      {
        ""From"": 0,
        ""To"": 34500,
        ""Multiplier"": 0.2
      },
      {
        ""From"": 34500,
        ""To"": 150000,
        ""Multiplier"": 0.4
      },
      {
        ""From"": 150000,
        ""To"": 2147483647,
        ""Multiplier"": 0.45
      }
    ],
    ""ScottishBrackets"": [
      {
        ""From"": 0,
        ""To"": 2000,
        ""Multiplier"": 0.19
      },
      {
        ""From"": 2000,
        ""To"": 12150,
        ""Multiplier"": 0.2
      },
      {
        ""From"": 12150,
        ""To"": 31580,
        ""Multiplier"": 0.21
      },
      {
        ""From"": 31580,
        ""To"": 150000,
        ""Multiplier"": 0.41
      },
      {
        ""From"": 150000,
        ""To"": 2147483647,
        ""Multiplier"": 0.46
      }
    ],
    ""ScottishFixedCodes"": [
      {
        ""Code"": ""BR"",
        ""Rate"": 0.2
      },
      {
        ""Code"": ""D0"",
        ""Rate"": 0.21
      },
      {
        ""Code"": ""D1"",
        ""Rate"": 0.41
      },
      {
        ""Code"": ""D2"",
        ""Rate"": 0.46
      },
      {
        ""Code"": ""N1"",
        ""Rate"": 0
      },
      {
        ""Code"": ""NT"",
        ""Rate"": 0
      }
    ]
  }
]
";
        protected Dictionary<int, JsonConfigTaxYear> ParsedConfig { get; set; }

        protected JsonConfigTaxYear CurrentTaxYear { get; set; }

        public JsonTaxYearSpecificProvider()
        {
            LoadJson();
        }

        public JsonTaxYearSpecificProvider(string jsonFileName) : this()
        {
            //JsonFileName = jsonFileName;
        }

        protected void LoadJson()
        {
            var fileContents = Json;
            var convertedYears = JsonConvert.DeserializeObject<List<JsonConfigTaxYear>>(fileContents);
            ParsedConfig = convertedYears.ToDictionary(m => m.TaxYear, m => m);
        }

        public void SetTaxYear(int taxYear)
        {
            if (!ParsedConfig.ContainsKey(taxYear))
                throw new ArgumentException($"Could not find tax year specifics for tax year {taxYear}.", nameof(taxYear));

            CurrentTaxYear = ParsedConfig[taxYear];
        }

        protected void EnsureTaxYearSet()
        {
            if (CurrentTaxYear == null)
                throw new InvalidOperationException("You must set tax year before calling this method.");
        }

        public NationalInsuranceCode GetCodeSpecifics(char niCode)
        {
            EnsureTaxYearSet();

            return CurrentTaxYear.NiRates.SingleOrDefault(m => m.Code == niCode);
        }

        public FixedCode GetFixedCode(string taxCode, bool scottish = false)
        {
            EnsureTaxYearSet();

            return ((scottish ? CurrentTaxYear.ScottishFixedCodes : null) ?? CurrentTaxYear.FixedCodes).SingleOrDefault(m => m.Code == taxCode);
        }

        public bool IsFixedCode(string taxCode, bool scottish = false)
        {
            EnsureTaxYearSet();

            return ((scottish ? CurrentTaxYear.ScottishFixedCodes : null) ?? CurrentTaxYear.FixedCodes).Any(m => m.Code == taxCode);
        }

        public T GetSpecificValue<T>(TaxYearSpecificValues specificValueType)
        {
            EnsureTaxYearSet();

            object retValue;
            switch (specificValueType)
            {
                case TaxYearSpecificValues.ApprenticeUpperSecondaryThreshold:
                    retValue = CurrentTaxYear.ApprenticeUpperSecondaryThreshold;
                    break;
                case TaxYearSpecificValues.DeaProtectedEarnings:
                    retValue = CurrentTaxYear.DeaProtectedEarnings;
                    break;
                case TaxYearSpecificValues.LowerEarningsLimit:
                    retValue = CurrentTaxYear.LowerEarningsLimit;
                    break;
                case TaxYearSpecificValues.UpperEarningsLimit:
                    retValue = CurrentTaxYear.UpperEarningsLimit;
                    break;
                case TaxYearSpecificValues.PrimaryThreshold:
                    retValue = CurrentTaxYear.PrimaryThreshold;
                    break;
                case TaxYearSpecificValues.SecondaryThreshold:
                    retValue = CurrentTaxYear.SecondaryThreshold;
                    break;
                case TaxYearSpecificValues.UpperAccrualPoint:
                    retValue = CurrentTaxYear.UpperAccrualPoint;
                    break;
                case TaxYearSpecificValues.UpperSecondaryThreshold:
                    retValue = CurrentTaxYear.UpperSecondaryThreshold;
                    break;
                case TaxYearSpecificValues.Plan1StudentLoanThreshold:
                    retValue = CurrentTaxYear.Plan1StudentLoanThreshold;
                    break;
                case TaxYearSpecificValues.Plan1StudentLoanRate:
                    retValue = CurrentTaxYear.Plan1StudentLoanRate;
                    break;
                case TaxYearSpecificValues.Plan2StudentLoanThreshold:
                    retValue = CurrentTaxYear.Plan2StudentLoanThreshold;
                    break;
                case TaxYearSpecificValues.Plan2StudentLoanRate:
                    retValue = CurrentTaxYear.Plan2StudentLoanRate;
                    break;
                case TaxYearSpecificValues.PensionLowerThreshold:
                    retValue = CurrentTaxYear.PensionLowerThreshold;
                    break;
                case TaxYearSpecificValues.PensionAutomaticEnrolment:
                    retValue = CurrentTaxYear.PensionAutomaticEnrolment;
                    break;
                case TaxYearSpecificValues.PensionUpperThreshold:
                    retValue = CurrentTaxYear.PensionUpperThreshold;
                    break;
                case TaxYearSpecificValues.DefaultTaxCode:
                    retValue = CurrentTaxYear.DefaultTaxCode;
                    break;
                default:
                    throw new NotImplementedException($"Could not provide a value for {specificValueType} using this provider. Ensure you are using the latest version.");
            }

            return (T)retValue;
        }

        /// <summary>
        /// Returns a rounded period value
        /// </summary>
        /// <param name="specificValueType"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public decimal GetPeriodTaxYearValue(TaxYearSpecificValues specificValueType, PayPeriods period)
        {
            // Get the annual value
            decimal annualValue = GetSpecificValue<decimal>(specificValueType);

            // By default for weekly we have 52 weeks in our period
            int periodCnt = 52;
            int weeksInPeriod = 1;
            if (period == PayPeriods.Monthly)
                periodCnt = 12;
            else
                weeksInPeriod = (int)Math.Round((decimal)periodCnt / (int)period);

            return TaxMath.PeriodRound((annualValue * weeksInPeriod) / periodCnt, weeksInPeriod);
        }

        public IEnumerable<TaxBracket> GetTaxBrackets(bool scottish = false)
        {
            EnsureTaxYearSet();

            return scottish ? CurrentTaxYear.ScottishBrackets : CurrentTaxYear.Brackets;
        }
    }
}
