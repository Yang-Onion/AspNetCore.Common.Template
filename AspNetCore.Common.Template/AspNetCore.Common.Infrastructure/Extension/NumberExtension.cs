using System;

namespace AspNetCore.Common.Infrastructure.Extension
{
    public static class NumberExtension
    {
        /// <summary>
        /// 按指定小数点位数截取
        /// </summary>
        /// <param name="d"></param>
        /// <param name="s">小数点后位数</param>
        /// <returns></returns>
        public static decimal ToFixed(this decimal d, int s) {
            decimal sp = Convert.ToDecimal(Math.Pow(10, s));
            if (d < 0)
                return Math.Truncate(d) + Math.Ceiling((d - Math.Truncate(d)) * sp) / sp;
            return Math.Truncate(d) + Math.Floor((d - Math.Truncate(d)) * sp) / sp;
        }

        public static string ToDisplay(this decimal number) {
            if ((int)number == number) {
                return string.Format("{0:N0}", number);
            }
            else {
                return string.Format("{0:N2}", number);
            }
        }

        public static string ToDisplay(this bool value) {
            if (value) {
                return "✔";
            }
            return string.Empty;
        }

    }
}