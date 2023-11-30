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
            UrlsEnum.Url urlsEnum = (UrlsEnum.Url)enumIndex;
            switch (urlsEnum)
            {
                case UrlsEnum.Url.EDGEDRIVER:
                    return "\"C:\\Program Files (x86)\\Microsoft\\Edge\\msedgedriver.exe\"";
                case UrlsEnum.Url.YOUTUBE:
                    return "https://www.youtube.com/";
                case UrlsEnum.Url.MP3_DOWNLOADER:
                    return "https://www.y2mate.com/youtube/bfCudD2OG0w";
                default:
                    return "";

            }
        }

    }
}
