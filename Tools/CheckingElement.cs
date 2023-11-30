using LearningConsoleApp.ConstantObjects;
using LearningConsoleApp.Interfaces;
using LearningConsoleApp.Models;
using Microsoft.Playwright;
using OpenQA.Selenium;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static LearningConsoleApp.ConstantObjects.UrlsConstant;

namespace LearningConsoleApp.Tools
{
    internal class CheckingElement<T>
    {
        private static ChromiumDriver driver;
        private static SeleniumActions seleniumActions;
        private static String previousUrl = "";

        internal static void SetDriver(ChromiumDriver chromiumDriver) {
            driver = chromiumDriver;
            seleniumActions = new SeleniumActions(driver);
        }

        internal static T CheckElement<V>(ChromiumDriver chromiumDriver, int enumIndex, V? value)
        {
            //driver = chromiumDriver;
            //seleniumActions = new SeleniumActions(driver);

            StepDownloaderConstants.StepDownloader elementsEnum =
                (StepDownloaderConstants.StepDownloader)enumIndex;
            Thread.Sleep(2000);
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    switch (elementsEnum)
                    {

                        case StepDownloaderConstants.StepDownloader.YOUTUBE_SEARCH_VIDEO:
                            Console.WriteLine("Youtube video aratılıyor..");
                            return ReturnMethod<bool>(SearchYoutubeVideo(value.ToString()));

                        case StepDownloaderConstants.StepDownloader.YOUTUBE_CLICK_VIDEO:
                            Console.WriteLine("Youtube video tıklatılıyor..");
                            return ReturnMethod<bool>(ClickYoutubeVideo(value.ToString()));

                        case StepDownloaderConstants.StepDownloader.MP3_SEARCH_VIDEO:
                            Console.WriteLine("Mp3 video aratılıyor..");
                            return ReturnMethod<bool>(SearchMp3Video(value.ToString()));

                        case StepDownloaderConstants.StepDownloader.MP3_DOWNLOAD_VIDEO_ROAD1:
                            Console.WriteLine("MP3 videolardan bulunuyor..");
                            return ReturnMethod<bool>(DownloadMp3Video_Road1());

                        case StepDownloaderConstants.StepDownloader.MP3_DOWNLOAD_VIDEO_ROAD2:
                            Console.WriteLine("MP3 Audio sekmesinden indire tıklatılıyor..");
                            return ReturnMethod<bool>(DownloadMp3Video_Road2());

                        case StepDownloaderConstants.StepDownloader.MP3_DOWNLOAD_VIDEO_ROAD3:
                            Console.WriteLine("Gelen popupta indir tuşuna basılıyor..");
                            return ReturnMethod<bool>(DownloadMp3Video_Road3());

                        default:
                            return ReturnMethod<bool>(false);
                    }

                }
                catch
                {
                    Thread.Sleep(500);
                }

            }

            switch (elementsEnum)
            {
                default:
                    return ReturnMethod<bool>(false);
            }

        }

        private static T ReturnMethod<R>(R returnMethod)
        {
            return (T)(object)(returnMethod);
        }

        private static bool SearchYoutubeVideo(String musicName)
        {
            seleniumActions.SwitchTab(0);

            driver.FindElement
                (By.CssSelector("div#container input#search")).Clear();
            Thread.Sleep(200);

            driver.FindElement
                (By.CssSelector("div#container input#search")).SendKeys(musicName);
            Thread.Sleep(800);

            seleniumActions.ExecuteJS("button#search-icon-legacy", "click()");

            return true;
        }

        private static bool ClickYoutubeVideo(String musicName)
        {
            previousUrl = driver.Url;
            String query = @" 
                var musicName = '" + musicName + @"'

                var a = [...document.querySelectorAll('#video-title yt-formatted-string')].slice(0, 5);

                try {
                  musicName.split(' ').reverse().forEach((txt) => {
                    if (txt.length > 1) {
                      a.forEach((item) => {
                        if (item.textContent.toLowerCase().includes(txt.toLowerCase())) {
                          item.click();
                          throw new Error('BreakException');
                        }
                      });
                    }
                  });

                } catch (e) {}

            ";
            
            seleniumActions.ExecuteJS(query, null, false);
            Thread.Sleep(500);
            if (previousUrl.Equals(driver.Url)) return false;
            return true;
        }

        private static bool SearchMp3Video(String url)
        {
            try
            {
                SearchAds();
            }
            catch (Exception)
            {
                seleniumActions.ExecuteJS("form input#txt-url", $"value = '{url}'");
                Thread.Sleep(800);


                seleniumActions.ExecuteJS("form button#btn-submit", "click()");
                return true;
            }
            throw new Exception();
        }

        private static bool DownloadMp3Video_Road1()
        {
            try
            {
                SearchAds();
            }
            catch (Exception) { }

            seleniumActions.ExecuteJS("#list-video > div:nth-child(1) > div > div > a", "click()");
            return true;
        }

        private static bool DownloadMp3Video_Road2()
        {
            try
            {
                SearchAds();
            }
            catch { }
            seleniumActions.ExecuteJS("#selectTab > li:nth-child(2) > a", "click()");
            Thread.Sleep(800);
            seleniumActions.ExecuteJS("#audio > table > tbody > tr > td.txt-center > button", "click()");
            return true;

        }

        private static bool DownloadMp3Video_Road3()
        {
            try
            {
                SearchAds();
            }
            catch { }

            seleniumActions.ExecuteJS("form button#btn-submit", "click()");
            Thread.Sleep(800);
            seleniumActions.ExecuteJS("#process-result > div > a", "click()");
            return true;
        }

        private static void SearchAds()
        {
            seleniumActions.SwitchTab(1);
            Thread.Sleep(600);
            try
            {
                seleniumActions.ExecuteJS("html > div", "remove()");
            }
            catch { }
            Thread.Sleep(300);
            seleniumActions.ExecuteJS("iframe", "remove()");
            Thread.Sleep(1000);
        }
    }
}
