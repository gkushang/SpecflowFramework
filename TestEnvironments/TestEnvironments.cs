using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using TechTalk.SpecFlow;
using NUnit.Framework;


namespace SpecFlowFramework.TestEnvironments
{
    public static class Environments
    {
        public static void Load()
        {

            string testDirectory = TestContext.CurrentContext.TestDirectory;
            string binDirectory = Path.GetFileName(testDirectory);
            string testDirectoryWithoutBin = testDirectory.Substring(0, testDirectory.Length - binDirectory.Length);

            Console.WriteLine("Test Directory: " + testDirectoryWithoutBin);
        
            string delimiter = "bin";
            
            string[] _parts = testDirectory.Split(new[] { delimiter }, StringSplitOptions.None);

            string firstPart = _parts[0];

            Console.WriteLine(firstPart);
             string filePath = firstPart + "TestEnvironments/staging_1.txt"; // Replace with the actual file path

                
                string[] lines = File.ReadAllLines(filePath);

                
                foreach (string line in lines)
                {
                    string[] parts = line.Split("="); 

                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();

                        Environment.SetEnvironmentVariable(key, value);
                        Console.WriteLine("ENV: " + key + "=" + value);
                    }
                }
        }
    }

}

