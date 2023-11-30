using OpenQA.Selenium.DevTools.V117.Debugger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningConsoleApp.Tools
{
    internal class ChangeFileName
    {
        private static bool hasDownloadDirectory = true;
        private static string downloadsPath;
        private static readonly string[] wordsWillBeRemove = {"y2mate.com - "};

        private static string[] files;
        private static string newFileName;

        internal static bool ChangeName()
        {
            if (hasDownloadDirectory != false) {
                downloadsPath = GetDownloadsPath();
                if (downloadsPath == null)
                {
                    hasDownloadDirectory = false;
                }
                else {
                    SearchFiles(downloadsPath, "*.mp3");
                    return true;
                }
            }
                
            return false;
        }

        private static string GetDownloadsPath()
        {
            // Kullanıcının profili
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            // İndirilenler klasörünün yolu
            string downloadsPath = Path.Combine(userProfile, "Downloads");

            // Klasörün var olup olmadığını kontrol etme
            if (Directory.Exists(downloadsPath))
            {
                return downloadsPath;
            }
            else
            {
                return null;
            }
        }

        private static void SearchFiles(string folderPath, string searchPattern)
        {
            // folderPath = klasörün yeri
            // searchPattern aranacak dosyanın uzantısı *.txt, *.mp3
            files = Directory.GetFiles(folderPath, searchPattern);

            foreach (var path in files)
            {
                foreach (var word in wordsWillBeRemove)
                {
                    if (path.Contains(word.Trim())) {
                        newFileName = path.Replace(word, "");
                        File.Move(path, newFileName);
                    }
 
                }  
            }
        }


    }
}
