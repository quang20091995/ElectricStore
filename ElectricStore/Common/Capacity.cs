using ElectricStore.Models.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Common
{
    public class Capacity
    {
        public const string level_1 = "120";
        public const string level_2 = "121-250";
        public const string level_3 = "251-500";
        public const string level_4 = "501-1000";
        public const string level_5 = "1000";

        public ConditionResult filterByCapacity(string input)
        {
            ConditionResult condition = new ConditionResult();
            switch (input)
            {
                case level_1:
                    condition.Min = 0;
                    condition.Max = 120;
                    break;

                case level_2:
                    condition.Min = 121;
                    condition.Max = 250;
                    break;

                case level_3:
                    condition.Min = 251;
                    condition.Max = 500;
                    break;

                case level_4:
                    condition.Min = 501;
                    condition.Max = 1000;
                    break;

                case level_5:
                    condition.Min = 1001;
                    break;
            }
            return condition;
        }
    }
}