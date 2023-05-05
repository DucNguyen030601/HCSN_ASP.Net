using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.Enums
{
    public class ValidationAttribute
    {
        public struct FixedAsset
        {
            public const int MaxLenghtFixedAssetCode = 100;

            public const int MaxLenghtFixedAssetName = 255;

            public const decimal RangeMinCost = 1;

            public const decimal RangeMaxCost = decimal.MaxValue;

            public const int RangeMinQuantity = 1;

            public const int RangeMaxQuantity = int.MaxValue;

            public const double RangeMinDepreciationRate = 0.0000;

            public const double RangeMaxDeperciationRate = 1;

            public const int RangeMinTrackedYear = 2000;

            public const int RangeMaxTrackedYear = 2050;

            public const int RangeMinLifeTime = 1;

            public const int RangeMaxLifeTime = int.MaxValue;
        }
        public struct FixedAssetIncrement
        {
            public const int MaxLenghtFixedAssetIncrementCode = 100;
        }


    }
}
