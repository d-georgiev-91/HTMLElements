using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TagExtractor
{
    class TagExtractor
    {
        static void Main()
        {
            Console.Write("File name: ");
            string fileName = Console.ReadLine();
            StreamReader fileToRead = new StreamReader(fileName);
            List<string> tags = new List<string>();
            string line = fileToRead.ReadToEnd();
            fileToRead.Close();
            var allTags = Regex.Matches(line.Trim(), @"<\s*\w.*?>");
            foreach (var item in allTags)
            {
                if (item.ToString().IndexOf(' ') > 0)
                {
                    tags.Add(item.ToString().Substring(item.ToString().IndexOf('<') + 1, item.ToString().IndexOf(' ')).Trim());
                }
                else
                {
                    tags.Add(item.ToString().Substring(item.ToString().IndexOf('<') + 1, item.ToString().IndexOf('>') - 1).Trim());
                }

            }
            for (int i = 0; i < tags.Count; i++)
            {
                for (int j = tags.Count - 1; j > i; j--)
                {
                    if (tags[i] == tags[j])
                        tags.RemoveAt(j);
                }
            }
            StreamWriter fileToWrite = new StreamWriter("tags.txt");
            for (int i = 0; i < tags.Count; i++)
            {
                if (i == tags.Count - 1)
                {
                    fileToWrite.Write(tags[i]);
                }
                else
                {
                    fileToWrite.Write(tags[i]+ ", ");
                }
            }
            fileToWrite.Close();
        }
    }
}
