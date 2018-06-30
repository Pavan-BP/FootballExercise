using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballExerciseUtilities
{
    public static class Extensions
    {
        public static int ToNumber(this string value)
        {
            int number = 0;
            if (int.TryParse(value, out number))
                return number;
            else throw new InvalidCastException();
        }
    }
}
