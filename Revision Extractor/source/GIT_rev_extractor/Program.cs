using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace GIT_rev_extractor
{
    class Program
    {
        static void Main(string[] args)
        {
            string revision = "";
            if (File.Exists(".git/refs/heads/master"))
            {
                StreamReader revextractor = new StreamReader(".git/refs/heads/master");
                revision = revextractor.ReadToEnd();
                Console.WriteLine("Found Revision! Revision is:");
                Console.WriteLine(revision);
            }
            else
            {
                Console.WriteLine("Cannot find revision! Using 0000000000000000000000000000000000000000 instead!");
                revision = "0000000000000000000000000000000000000000";
                Console.WriteLine("Please checkout the latest commit via git");
            }
            if (File.Exists("Framework/Configs/WorldServer.conf"))
            {
                Console.WriteLine("Found Config-File!");
                StreamReader setrev = new StreamReader("Framework/Configs/WorldServer.conf");
                string config = "";
                while (setrev.EndOfStream == false)
                {
                    config = config + setrev.ReadLine() + "\n";
                }
                Regex replace = new Regex("rev = \".*\"");
                string result = replace.Replace(config, "rev = \"" + revision + "\"");
                setrev.Close();
                Console.WriteLine("Write revision to config!");
                StreamWriter writerev = new StreamWriter("Framework/Configs/WorldServer.conf");
                writerev.Write(result);
                writerev.Close();
                Console.WriteLine("Ready! :)");
            }
            else
            {
                Console.WriteLine("Please put this file into your Arctium-Source-Directory");
            }
            Console.ReadKey();
        }
    }
}
