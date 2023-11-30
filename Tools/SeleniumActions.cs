using OpenQA.Selenium;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.DevTools.V119.Browser;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningConsoleApp.Tools
{
    public class SeleniumActions
    {

        private Actions keyActions;
        private ChromiumDriver driver;
        private bool isWindowOpened = false;
        private IJavaScriptExecutor javaScript;


        public SeleniumActions(ChromiumDriver driver)
        {
            keyActions = new Actions(driver);
            this.driver = driver;
            javaScript = (IJavaScriptExecutor)driver;
        }

        public void OpenNewTab(string url)
        {
            if (isWindowOpened)
            {
                SwitchTab(0);
                driver.SwitchTo().NewWindow(WindowType.Tab);
            }
            else isWindowOpened = true;
            driver.Navigate().GoToUrl(url);
        }

        // We don't use this
        public void OpenLinkInNewTab(IWebElement link)
        {
            keyActions.KeyDown(Keys.Control).Click(link).KeyUp(Keys.Control).Build().Perform();
        }

        public bool SwitchTab(int tab)
        {
            try
            {
                driver.SwitchTo().Window(driver.WindowHandles[tab]);
                return true;
            }
            catch { }
            return false;
        }

        public void CloseOtherTabs()
        {
            for (int i = 0; i < 12; i++)
            {
                Thread.Sleep(300);
                SwitchTab(0);
                Thread.Sleep(500);

                if (i != 0 && driver.WindowHandles.Count == 2) break;
                try
                {
                    driver.Close();
                }
                catch {
                    break;
                }
            }
        }

        public void ExecuteJS(String query, String action = null, bool querySelector = true)
        {
            if (querySelector)
            {
                if (action != null)
                {
                    javaScript.ExecuteScript(
                        $"document.querySelector('{query}').{action}");
                }
                else
                {
                    javaScript.ExecuteScript(
                    $"document.querySelector('{query}'))");
                }
            }
            else
            {
                javaScript.ExecuteScript(query);
            }
        }

        public R_Type ReturnTypeExecuteJS<R_Type>(String musicName)
        {
            var a = javaScript.ExecuteScript(@" 
                var musicName = " + musicName + @"

                var a = [...document.querySelectorAll('#video-title yt-formatted-string')].slice(0, 5);

                try{
                a.forEach(function myFunction(item){ 
                musicName.split(' ').forEach(function myFunc(txt){
	                if(item.textContent.includes(txt)) {
                        item.click()
                    throw BreakException
                }

                })


                })
                }catch(e) {

                }

            ");
            return (R_Type)a;
        }

    }
}
