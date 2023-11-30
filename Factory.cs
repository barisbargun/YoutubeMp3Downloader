using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningConsoleApp.ConstantObjects;
using LearningConsoleApp.Interfaces;
using LearningConsoleApp.Music_Downloader;
using LearningConsoleApp.Tools;
using Microsoft.Playwright;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace LearningConsoleApp
{
    public class Factory
    {

        private EdgeOptions options = new EdgeOptions();

        private ChromiumDriver driver;
        private SeleniumActions seleniumActions;

        private readonly ReadingMusicNames readingMusicNames = new ReadingMusicNames();
        private OpeningYoutube openingYoutube;
        private OpeningMp3Downloader mp3Downloader;

        private string musicUrl = "";
        private List<string> musicNames = new();

        private void Setup()
        {
            options.AddArgument("--disable-infobars");
            options.AddArgument("--mute-audio");

            // If you'd like to change to chrome driver,
            // change options to ChromeOptions, change EdgeDriver to ChromeDriver
            driver = new EdgeDriver(
                UrlsEnumConverter.GetUrl((int)UrlsConstant.Url.EDGEDRIVER), options);

            openingYoutube = new OpeningYoutube(driver);
            mp3Downloader = new OpeningMp3Downloader(driver);
            seleniumActions = new SeleniumActions(driver);

            CheckingElement<bool>.SetDriver(driver);
        }

        public void RunApp()
        {

            Console.Write("Dosyadan(0) mı müzikleriniz alınsın yoksa\nkendiniz(1) mi yazmak istersiniz: ");
            string WhichWayMusicTaken = Console.ReadLine();
            
            if (WhichWayMusicTaken != null)
            {
                if (WhichWayMusicTaken.Trim().Equals("1"))
                {
                    while (true)
                    {
                        Console.Write("Şarkınızı giriniz durmak için(dur): ");
                        string userInput = Console.ReadLine();

                        if (userInput.ToLower() == "dur")
                        {
                            break;
                        }

                        // Boşlukları temizle ve listeye ekle
                        if (userInput.Trim().Length > 3)
                            musicNames.Add(userInput.Trim());
                    }
                }

                else if (WhichWayMusicTaken.Trim().Equals("0"))
                    musicNames = readingMusicNames.ReadMusicNames();


                else
                {
                    Console.WriteLine("İşlem verilmedi..");
                    Thread.Sleep(2500);
                }
            }

            if (musicNames.Count > 0)
            {
                Console.WriteLine("İndirilecek olan müzikler:\n");
                foreach (string item in musicNames)
                {
                    Console.WriteLine(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item));
                }
                Console.WriteLine("\n");
                Thread.Sleep(2000);
                Setup();
                Thread.Sleep(500);
                OpenTabs();

                foreach (String musicName in musicNames)
                {
                    DownloadVideo(musicName);
                }

                Console.WriteLine("Browser kapatılıyor..");
                Thread.Sleep(1000);
                for (int i = 10; i >= 0; i--)
                {
                    Console.WriteLine(i);
                    Thread.Sleep(1000);
                }
                driver.Quit();
                ChangeFileName.ChangeName();
            }

        }

        private void OpenTabs()
        {
            seleniumActions.OpenNewTab(
                    UrlsEnumConverter.GetUrl((int)UrlsConstant.Url.YOUTUBE));
            Thread.Sleep(200);

            seleniumActions.OpenNewTab(
                UrlsEnumConverter.GetUrl((int)UrlsConstant.Url.MP3_DOWNLOADER));
            Thread.Sleep(200);

            seleniumActions.SwitchTab(0);
            Thread.Sleep(200);
        }

        private void DownloadVideo(String musicName)
        {
            Console.Out.WriteLine($"{musicName} kuyruğa giriyor..");
            Thread.Sleep(1500);

            if (openingYoutube.OpenVideo(musicName))
            {
                Thread.Sleep(1500);

                musicUrl = driver.Url;

                if (mp3Downloader.DownloadVideo(musicUrl))
                    Console.Out.WriteLine(($"İndirildi: {musicName}"));
                else Console.Out.WriteLine(($"İndirilemedi: {musicName}"));

            }
            else {
                Console.WriteLine("Video bulunamadı..");
                Console.WriteLine("İsmi düzgün yazdığınızdan emin olun.");
            }

        }
    }




    //class PeopleEnum<T> : IEnumerator<T>
    //{

    //    private List<T> people = new List<T>();
    //    private int position = -1;

    //    Object IEnumerator.Current
    //    {
    //        get
    //        {
    //            return Current;
    //        }
    //    }

    //    public T Current
    //    {
    //        get
    //        {
    //            try
    //            {
    //                return people[position];
    //            }
    //            catch (IndexOutOfRangeException)
    //            {

    //                throw new IndexOutOfRangeException("An error happened");
    //            }
    //        }
    //    }

    //    public PeopleEnum(List<T> things)
    //    {
    //        this.people = things;
    //    }

    //    public bool MoveNext()
    //    {
    //        position++;
    //        return (position < people.Count);
    //    }

    //    public void Reset()
    //    {
    //        position = -1;
    //    }

    //    public void Dispose()
    //    {
    //        people.Clear();
    //        position = -1;
    //    }
    //}




}
