using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: Program <string to replace>");
                return;
            }

            string url = "https://mail.univ.net.ua/forum/forum.html";
            string originalFilePath = "Forum.txt";
            string lightFilePath = "Forum-LIGHT.txt";

            
            if (File.Exists(originalFilePath))
            {
                File.Delete(originalFilePath);
            }
            if (File.Exists(lightFilePath))
            {
                File.Delete(lightFilePath);
            }

           
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.GetEncoding("windows-1251"); 
                    client.DownloadFile(url, originalFilePath);
                    Console.WriteLine("File uploaded successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading file: " + ex.Message);
                return;
            }

            string replacementString = args[0];

            try
            {
                using (StreamReader reader = new StreamReader(originalFilePath, Encoding.GetEncoding("windows-1251"))) 
                using (StreamWriter writer = new StreamWriter(lightFilePath, false, Encoding.UTF8)) 
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {

                        line = Regex.Replace(line, "(youtube|youtu\\.be)", replacementString, RegexOptions.IgnoreCase);

                        writer.WriteLine(line);
                    }
                }
                Console.WriteLine($"File {lightFilePath} successfully created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error processing the file: " + ex.Message);
            }
        }
    }
}
