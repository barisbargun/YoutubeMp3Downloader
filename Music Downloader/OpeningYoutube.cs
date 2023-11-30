using LearningConsoleApp.ConstantObjects;
using LearningConsoleApp.Interfaces;
using LearningConsoleApp.Models;
using LearningConsoleApp.Tools;
using OpenQA.Selenium;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LearningConsoleApp.Music_Downloader
{
    internal class OpeningYoutube
    {
        private readonly ChromiumDriver driver;
        private string musicName = "";

        internal OpeningYoutube(ChromiumDriver driver)
        {
            this.driver = driver;
        }

        internal bool OpenVideo(string musicName)
        {
            this.musicName = musicName;
            if (SearchVideo())
            {
                return ClickVideo();
            }
            return false;
            
        }

        private bool SearchVideo()
        {
            return CheckingElement<bool>.CheckElement<String>(driver,
                (int)CheckingElementsEnum.CheckingElement.YOUTUBE_SEARCH_VIDEO,musicName);

        }

        private bool ClickVideo()
        {
            return CheckingElement<bool>.CheckElement<String>(driver,
                (int)CheckingElementsEnum.CheckingElement.YOUTUBE_CLICK_VIDEO, musicName);
        }

    }
}
