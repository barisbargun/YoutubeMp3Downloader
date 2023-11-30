using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningConsoleApp.Tools
{
    public class ReadingMusicNames
    {
        List<string> musicNames = new List<string>();
        public List<string> ReadMusicNames()
        {
            string path = "../../../Assets/MusicUrls.txt";

            using (StreamReader reader = new StreamReader
                (path, Encoding.UTF8, true))
            {

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    musicNames.Add(line);
                }

                return musicNames;
            }

        }

    }
}