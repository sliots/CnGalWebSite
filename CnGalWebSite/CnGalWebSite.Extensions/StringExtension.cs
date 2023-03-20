﻿using System.Text.RegularExpressions;

namespace CnGalWebSite.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// 截断字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Abbreviate(this string str, int length)
        {
            if (str == null)
            {
                return "";
            }
            if (str.Length < length * 2.5)
            {
                return str;
            }
            else
            {
                return string.Concat(str.AsSpan(0, (int)(length * 2.5)), "...");
            }
        }

        /// <summary>
        /// 取中间字符串
        /// </summary>
        /// <param name="sourse"></param>
        /// <param name="startstr"></param>
        /// <param name="endstr"></param>
        /// <returns></returns>
        public static string MidStrEx(this string sourse, string startstr, string endstr)
        {
            string result = null;
            int startindex, endindex;
            try
            {
                startindex = sourse.IndexOf(startstr);
                if (startindex == -1)
                {
                    return result;
                }

                var tmpstr = sourse[(startindex + startstr.Length)..];
                endindex = tmpstr.IndexOf(endstr);
                if (endindex == -1)
                {
                    return result;
                }

                result = tmpstr.Remove(endindex);
            }
            catch
            {
                //Log.WriteLog("MidStrEx Err:" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 删除 HTML 中的链接
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DeleteHtmlLinks(this string value)
        {
            value = value.Replace("</a>", "");

            while (true)
            {
                var temp = value.MidStrEx("<a ", ">");

                if (string.IsNullOrWhiteSpace(temp))
                {
                    break;
                }

                value = value.Replace("<a " + temp + ">", "");

            }

            return value;
        }

        public static List<string> GetLinks(this string value)
        {
            return Regex.Matches(value, "http[s]?://(?:(?!http[s]?://)[a-zA-Z]|[0-9]|[$\\-_@.&+/]|[!*\\(\\),]|(?:%[0-9a-fA-F][0-9a-fA-F]))+").Select(s => s.ToString().Trim()).ToList();
        }

        /// <summary>    
        /// 取得HTML中所有图片的 URL。    
        /// </summary>    
        /// <param name="sHtmlText">HTML代码</param>    
        /// <returns>图片的URL列表</returns>    
        public static List<string> GetImageLinks(this string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签    
            Regex regImg = new Regex(@"(<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>)", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串    
            MatchCollection matches = regImg.Matches(sHtmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表    
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;

            var model = sUrlList.ToList();


            //匹配 Markdown 中的图片
            regImg = new Regex(@"(?<=\!\[.*?\]\()(.+?)(?=\))", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串    
            matches = regImg.Matches(sHtmlText);
            model.AddRange(matches.Select(s => s.Value));

            //移除空项目
            model.Purge().RemoveAll(s => string.IsNullOrWhiteSpace(s) || s.Contains("video.weibo.com") || s.Contains(".mp4"));
            return model;
        }

        /// <summary>
        /// 将时间戳字符串转化成时间
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static DateTime TransTime(this string str)
        {
            return  new DateTime(1970, 1, 1, 8, 0, 0).AddMilliseconds(long.Parse(str));
        }

        public static bool IsLocalUrl(this string url)
        {
            if (url == null)
            {
                return false;
            }

            if (url.StartsWith('/'))
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// 将传入的字符串中间部分字符替换成特殊字符
        /// </summary>
        /// <param name="value">需要替换的字符串</param>
        /// <param name="startLen">前保留长度</param>
        /// <param name="endLen">尾保留长度</param>
        /// <param name="replaceChar">特殊字符</param>
        /// <returns>被特殊字符替换的字符串</returns>
        public static string ReplaceWithSpecialChar(this string value, int startLen = 4, int endLen = 4, char specialChar = '*')
        {
            try
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }

                int lenth = value.Length - startLen - endLen;
                if (lenth < 4)
                {
                    endLen = 4;
                    startLen = 0;
                }

                lenth = value.Length - startLen - endLen;
                if (lenth < 1)
                {
                    endLen = 2;
                    startLen = 0;
                }
                lenth = value.Length - startLen - endLen;
                if (lenth < 1)
                {
                    return value;
                }

                string replaceStr = value.Substring(startLen, lenth);

                string specialStr = string.Empty;

                for (int i = 0; i < replaceStr.Length; i++)
                {
                    specialStr += specialChar;
                }

                value = value.Replace(replaceStr, specialStr);
            }
            catch (Exception)
            {
                throw;
            }

            return value;
        }

    }

   
}