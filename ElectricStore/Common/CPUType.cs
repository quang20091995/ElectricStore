using ElectricStore.Models.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Common
{
    public class CPUType
    {
        public const string level_1 = "Dưới 1.6GHz";
        public const string level_2 = "1.61GHz-1.8GHz";
        public const string level_3 = "1.81GHz-2.0GHz";
        public const string level_4 = "2.01GHz-2.5GHz";
        public const string level_5 = "2.51GHz-3.0GHz";
        public const string level_6 = "3.01GHz-3.4GHz";

        public ConditionResult filterByCPU(string input)
        {
            ConditionResult condition = new ConditionResult();
            switch (input)
            {
                case level_1:
                    condition.Min = 0;
                    condition.Max = 1.61;
                    break;

                case level_2:
                    condition.Min = 1.61;
                    condition.Max = 1.8;
                    break;

                case level_3:
                    condition.Min = 1.81;
                    condition.Max = 2.0;
                    break;

                case level_4:
                    condition.Min = 2.01;
                    condition.Max = 2.5;
                    break;

                case level_5:
                    condition.Min = 2.51;
                    condition.Max = 3;
                    break;

                case level_6:
                    condition.Min = 3.01;
                    condition.Max = 3.4;
                    break;
            }
            return condition;
        }
    }
}