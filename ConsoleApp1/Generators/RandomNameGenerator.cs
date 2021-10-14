using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleApp1.Generators
{
    /// <summary>
    /// RandomName class, used to generate a random name.
    /// </summary>
    public class RandomNameGenerator: INameGenerator
    {
        /// <summary>
        /// Class for holding the lists of names from names.json
        /// </summary>
        class NameList
        {
            public string[] boys { get; set; }
            public string[] girls { get; set; }
            public string[] last { get; set; }

            public NameList()
            {
                boys = new string[] { };
                girls = new string[] { };
                last = new string[] { };
            }
        }

        Random rand;
        List<string> Male;
        List<string> Female;
        List<string> Last;

        /// <summary>
        /// Initialises a new instance of the RandomName class.
        /// </summary>
        /// <param name="rand">A Random that is used to pick names</param>
        public RandomNameGenerator(Random rand)
        {
            this.rand = rand;
            NameList l = new NameList();

            JsonSerializer serializer = new JsonSerializer();

            //"C:/Users/ruily_g40rtk5/RiderProjects/ConsoleApp1/ConsoleApp1/resources/names.json"
            using (StreamReader reader = new StreamReader("names.json"))
            using (JsonReader jreader = new JsonTextReader(reader))
            {
                l = serializer.Deserialize<NameList>(jreader);
            }

            Male = new List<string>(l.boys);
            Female = new List<string>(l.girls);
            Last = new List<string>(l.last);
        }

        /// <summary>
        /// Returns a new random name
        /// </summary>
        /// <param name="sex">The sex of the person to be named. true for male, false for female</param>
        /// <param name="middle">How many middle names do generate</param>
        /// <param name="isInital">Should the middle names be initials or not?</param>
        /// <returns>The random name as a string</returns>
        public string Generate()
        {
            var sex = rand.Next() % 2 == 0 ? Sex.Male : Sex.Female;
            string first = sex == Sex.Male ? Male[rand.Next(Male.Count)] : Female[rand.Next(Female.Count)]; // determines if we should select a name from male or female, and randomly picks
            string last = Last[rand.Next(Last.Count)]; // gets the last name

            List<string> middles = new List<string>();

            StringBuilder b = new StringBuilder();
            b.Append(first + " "); // put a space after our names;
            foreach (string m in middles)
            {
                b.Append(m + " ");
            }
            b.Append(last);

            return b.ToString();
        }

        /// <summary>
        /// Generates a list of random names
        /// </summary>
        /// <param name="number">The number of names to be generated</param>
        /// <param name="maxMiddleNames">The maximum number of middle names</param>
        /// <param name="sex">The sex of the names, if null sex is randomised</param>
        /// <param name="initials">Should the middle names have initials, if null this will be randomised</param>
        /// <returns>List of strings of names</returns>
        public List<string> RandomNames(int number, int maxMiddleNames, Sex? sex = null, bool? initials = null)
        {
            List<string> names = new List<string>();

            for (int i = 0; i < number; i++)
            {
                if (sex != null && initials != null)
                {
                    names.Add(Generate());
                }
                else if (sex != null)
                {
                    bool init = rand.Next(0, 2) != 0;
                    names.Add(Generate());
                }
                else if (initials != null)
                {
                    Sex s = (Sex)rand.Next(0, 2);
                    names.Add(Generate());
                }
                else
                {
                    Sex s = (Sex)rand.Next(0, 2);
                    bool init = rand.Next(0, 2) != 0;
                    names.Add(Generate());
                }
            }

            return names;
        }
    }

    public enum Sex
    {
        Male,
        Female
    }
}
