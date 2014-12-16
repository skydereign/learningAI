using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace learningAI_win
{
    static class Utility
    {
        public static float AngleBetween(float a, float b)
        {
            return (float)Math.Atan2(Math.Sin(a - b), Math.Cos(a - b));
        }

        public static float DegToRad(float ang)
        {
            return (float)(ang * Math.PI / 180f);
        }
        public static float RadToDeg(float ang)
        {
            return (float)(ang / Math.PI * 180f);
        }
    }
}
