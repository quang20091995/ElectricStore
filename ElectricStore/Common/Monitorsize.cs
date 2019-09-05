using ElectricStore.Models.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Common
{
    public class Monitorsize
    {
        public const string level_1 = "Trên 27";
        public const string level_2 = "23-26";
        public const string level_3 = "20-22";
        public const string level_4 = "15-16";
        public const string level_5 = "13-14";
        public const string level_6 = "11-12";

        public ConditionResult filterByMonitorsize(string input)
        {
            ConditionResult condition = new ConditionResult();
            switch (input)
            {
                case level_1:
                    condition.Min = 27;
                    break;

                case level_2:
                    condition.Min = 23;
                    condition.Max = 26;
                    break;

                case level_3:
                    condition.Min = 20;
                    condition.Max = 22;
                    break;

                case level_4:
                    condition.Min = 15;
                    condition.Max = 16;
                    break;

                case level_5:
                    condition.Min = 11;
                    condition.Max = 12;
                    break;
            }
            return condition;
        }
    }
}