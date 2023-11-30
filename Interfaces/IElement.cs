using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningConsoleApp.Interfaces
{
    internal interface IElement
    {
        bool status { get; set; }
        IWebElement? element { get; set; } 
    }
}
