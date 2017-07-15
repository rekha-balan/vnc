using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerationWithRoslyn
{
    class Underwriting
    {
        public enum LoanPurpose
        {
            [Description("Launch a new business")]
            BusinessLaunching = 1,
            [Description("Buy a home")]
            HomePurchase,
            [Description("Make home improvements")]
            HomeImprovement,
            [Description("Finance investments")]
            Investment,
            [Description("Cash out to pay off debts")]
            DebtConsolidation,
            [Description("Finance education")]
            Education,
            [Description("Emergency funds to handle unforseen expense")]
            EmergencyExpenditure,
            [Description("Buy a car")]
            CarPurchase,
            [Description("Finance a wedding")]
            Wedding,
            [Description("Finance travel plans")]
            Travel = 10
        }

        enum Occupancy
        {
            [Description("Owner Occupied")]
            OwnerOccupied = 1,
            [Description("Second Home")]
            SecondHome = 2,
            [Description("Investment Property")]
            InvestmentProperty = 3
        }

        enum PropertyTypes
        {
            [Description("Detached single family residence")]
            DetachedHouse = 1,
            [Description("Semi-detached houses with front, rear and any one side or both sides open")]
            SemiDetachedHouse = 2,
            [Description("An attached dwelling that is not a condo")]
            Townhome = 3,
            [Description("Single, individually-owned housing unit in a multi-unit building")]
            Condominium = 4,
            [Description("Multi floor row house with a brown sandstone facade")]
            BrownStone = 5
        }

        bool Rule1(ILoanCodes data)
        {
            var status = false;

            if (data.Code1 == 1)
            {
                if (data.Code3 == 7)
                {
                    if (data.Code4 == 10 || data.Code4 == 11)
                    {
                        if (data.Code2 == 4 || data.Code4 == 5 || data.Code4 == 6)
                        {
                            status = true;
                        }
                    }
                }
            }

            return status;
        }

        bool Rule1A(ILoanCodes data)
        {
            if (data.Code4 != 1) return false;
            if (data.Code3 != 7) return false;
            if (data.Code4 != 10 && data.Code4 != 11) return false;
            if (data.Code2 == 4 || data.Code2 == 5 || data.Code2 == 6) return true;

            return false;
        }


    }
}
