using ElectricStore.Models.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Common
{
    public class PriceLevel
    {
        public const string level_1 = "1-5";
        public const string level_2 = "6-10";
        public const string level_3 = "11-15";
        public const string level_4 = "16-20";
        public const string level_5 = "21-25";
        public const string level_6 = "25";

        public ConditionResult filterByPrice(string input)
        {
            ConditionResult condition = new ConditionResult();
            switch (input)
            {
                case level_1:
                    condition.Min = 1000000;
                    condition.Max = 5000000;
                    break;

                case level_2:
                    condition.Min = 6000000;
                    condition.Max = 10000000;
                    break;

                case level_3:
                    condition.Min = 11000000;
                    condition.Max = 15000000;
                    break;

                case level_4:
                    condition.Min = 16000000;
                    condition.Max = 20000000;
                    break;

                case level_5:
                    condition.Min = 21000000;
                    condition.Max = 25000000;
                    break;

                case level_6:
                    condition.Min = 26000000;
                    break;
            }
            return condition;
        }
    }
}