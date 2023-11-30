using LearningConsoleApp.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningConsoleApp.Models
{
    internal class Element:IElement
    {
        public bool status { get; set; }
        public IWebElement? element { get; set; }

        internal Element(bool status, IWebElement element) {
            this.status = status;
            this.element = element;
        }
    }
}
