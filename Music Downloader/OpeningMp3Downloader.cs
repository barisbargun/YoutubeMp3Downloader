using LearningConsoleApp.ConstantObjects;
using LearningConsoleApp.Tools;
using OpenQA.Selenium;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningConsoleApp.Music_Downloader
{
    internal class OpeningMp3Downloader
    {
        ChromiumDriver driver;
        string url = "";

        internal OpeningMp3Downloader(ChromiumDriver driver)
        {
            this.driver = driver;
        }

        internal bool DownloadVideo(String url)
        {
            this.url = url;
            if (SearchVideo())
                return DownloadMp3Video();

            return false;
        }

        private bool SearchVideo()
        {
            return CheckingElement<bool>.CheckElement<String>
                (driver, (int)CheckingElementsEnum.CheckingElement.MP3_SEARCH_VIDEO, url);
        }

        private bool DownloadMp3Video()
        {
            if (
                CheckingElement<bool>.CheckElement<String>
                (driver, (int)CheckingElementsEnum.CheckingElement.MP3_DOWNLOAD_VIDEO_ROAD2, url)
                )
            {
                return CheckingElement<bool>.CheckElement<String>
                (driver, (int)CheckingElementsEnum.CheckingElement.MP3_DOWNLOAD_VIDEO_ROAD3, url);
            }
            else if (
                CheckingElement<bool>.CheckElement<String>
                (driver, (int)CheckingElementsEnum.CheckingElement.MP3_DOWNLOAD_VIDEO_ROAD1, url) &&
                CheckingElement<bool>.CheckElement<String>
                (driver, (int)CheckingElementsEnum.CheckingElement.MP3_DOWNLOAD_VIDEO_ROAD2, url)
                )
            {
                return CheckingElement<bool>.CheckElement<String>
                   (driver, (int)CheckingElementsEnum.CheckingElement.MP3_DOWNLOAD_VIDEO_ROAD3, url);
            }

            return false;

        }
    }
}
