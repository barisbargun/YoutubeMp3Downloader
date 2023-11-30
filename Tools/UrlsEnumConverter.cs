using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningConsoleApp.ConstantObjects;

namespace LearningConsoleApp.Tools
{
    internal class UrlsEnumConverter
    {

        internal static string GetUrl(int enumIndex)
        {
            UrlsConstant.Url urlsEnum = (UrlsConstant.Url)enumIndex;
            switch (urlsEnum)
            {
                case UrlsConstant.Url.EDGEDRIVER:
                    return "C:\\Program Files (x86)\\Microsoft\\Edge\\msedgedriver.exe";
                case UrlsConstant.Url.YOUTUBE:
                    return "https://www.youtube.com/";
                case UrlsConstant.Url.MP3_DOWNLOADER:
                    return "https://www.y2mate.com/youtube/bfCudD2OG0w";
                default:
                    return "";

            }
        }

    }
}
