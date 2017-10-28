using System;
using System.DrawingCore;
using System.DrawingCore.Drawing2D;
using System.DrawingCore.Imaging;
using System.IO;

namespace AspNetCore.Common.Web.Providers
{
    public class SecurityCodeHelper
    {
        public static string GeneralRandomCode(int codeCount)
        {
            var allChar =
                "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            var allCharArray = allChar.Split(',');
            var randomCode = "";
            var temp = -1;
            var rand = new Random();
            for (var i = 0; i < codeCount; i++)
            {
                if (temp != -1) rand = new Random(i * temp * (int) DateTime.Now.Ticks);
                var t = rand.Next(61);
                if (temp == t) return GeneralRandomCode(codeCount);
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

        public static byte[] GeneralSecurityCode(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == string.Empty)
                return null;
            var iWordWidth = 15;
            var iImageWidth = checkCode.Length * iWordWidth;
            var image = new Bitmap(iImageWidth, 25);
            var g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器 
                var random = new Random();
                //清空图片背景色 
                g.Clear(Color.White);

                //画图片的背景噪音点
                for (var i = 0; i < 20; i++)
                {
                    var x1 = random.Next(image.Width);
                    var x2 = random.Next(image.Width);
                    var y1 = random.Next(image.Height);
                    var y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                //画图片的背景噪音线 
                for (var i = 0; i < 2; i++)
                {
                    var x1 = 0;
                    var x2 = image.Width;
                    var y1 = random.Next(image.Height);
                    var y2 = random.Next(image.Height);
                    if (i == 0) g.DrawLine(new Pen(Color.Gray, 2), x1, y1, x2, y2);
                }

                for (var i = 0; i < checkCode.Length; i++)
                {
                    var Code = checkCode[i].ToString();
                    var xLeft = iWordWidth * i;
                    random = new Random(xLeft);
                    var iSeed = DateTime.Now.Millisecond;
                    var iValue = random.Next(iSeed) % 4;
                    if (iValue == 0)
                    {
                        var font = new Font("Arial", 14, FontStyle.Bold | FontStyle.Italic);
                        var rc = new Rectangle(xLeft, 0, iWordWidth, image.Height);
                        var brush = new LinearGradientBrush(rc, Color.Blue, Color.Red, 1.5f, true);
                        g.DrawString(Code, font, brush, xLeft, 2);
                    }
                    else if (iValue == 1)
                    {
                        var font = new Font("楷体", 14, FontStyle.Bold);
                        var rc = new Rectangle(xLeft, 0, iWordWidth, image.Height);
                        var brush = new LinearGradientBrush(rc, Color.Blue, Color.DarkRed, 1.3f, true);
                        g.DrawString(Code, font, brush, xLeft, 2);
                    }
                    else if (iValue == 2)
                    {
                        var font = new Font("宋体", 14, FontStyle.Bold);
                        var rc = new Rectangle(xLeft, 0, iWordWidth, image.Height);
                        var brush = new LinearGradientBrush(rc, Color.Green, Color.Blue, 1.2f, true);
                        g.DrawString(Code, font, brush, xLeft, 2);
                    }
                    else if (iValue == 3)
                    {
                        var font = new Font("黑体", 14, FontStyle.Bold | FontStyle.Bold);
                        var rc = new Rectangle(xLeft, 0, iWordWidth, image.Height);
                        var brush = new LinearGradientBrush(rc, Color.Blue, Color.Green, 1.8f, true);
                        g.DrawString(Code, font, brush, xLeft, 2);
                    }
                }
                ////画图片的前景噪音点 
                for (var i = 0; i < 8; i++)
                {
                    var x = random.Next(image.Width);
                    var y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线 
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                var ms = new MemoryStream();
                image.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}