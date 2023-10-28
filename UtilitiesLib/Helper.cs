using System;

namespace UtilitiesLib
{
    public class Helper
    {
        public bool StringToInt(string input, out int output)
        {
            return int.TryParse(input, out output);
        }

        public bool StringToFloat(string input, out float output)
        {
            return float.TryParse(input, out output);
        }

        public bool StringToInt(string input, out int output, int lowLimit, int highLimit)
        {
            if (int.TryParse(input, out output))
            {
                if (output >= lowLimit && output <= highLimit)
                {
                    return true;
                }
            }
            return false;
        }

        bool StringToFloat(string input, out float output, float lowLimit, float highLimit)
        {
            if (float.TryParse(input, out output))
            {

                if (output >= lowLimit && output <= highLimit)
                {
                    return true;
                }
            }
            return false;
        }

        public bool WithinMinMaxValueCheck(int minVal, int maxVal, int valToCheck)
        {
            if (valToCheck >= minVal && valToCheck <= maxVal)
            {
                return true;
            }
            return false;
        }
    }
}
