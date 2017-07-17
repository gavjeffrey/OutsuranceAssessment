using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsurance.Assessment
{
    public class Program
    {
        public List<Person> People { get; set; }

        static void Main(string[] args)
        {
            Program p = new Program();

            p.LoadData();

            var names = p.GetFrequencyOfName();
            p.SaveToFile(names, "names.txt");

            var addresses = p.GetAddresses();
            p.SaveToFile(addresses, "addresses.txt");
        }

        /// <summary>
        /// Method used to load data. In a real world scenario I would probably download a more robust csv reader to parse csv for me but because it is straight forward I have opted to read it manually.
        /// </summary>
        public void LoadData()
        {
            string[] lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "data.csv"));

            People = new List<Person>();

            for (int i = 0; i < lines.Count(); i++)
            {
                if (i == 0) //don't process header row
                    continue;

                var columns = lines[i].Split(',');

                People.Add(new Person
                {
                    FirtsName = columns[0],
                    Surname = columns[1],
                    Address = new Address(columns[2]),
                    PhoneNumber = columns[3]
                });
            }
        }

        /// <summary>
        /// Uses Linq to query the People collection and return the ordered string output
        /// </summary>
        /// <returns>List of strings correctly formatted and ordered as expected</returns>
        public List<string> GetFrequencyOfName()
        {
            //first get all the surnames and firstnames
            var firstNames = People.Select(x => new { Name = x.FirtsName });
            var surnames = People.Select(x => new { Name = x.Surname });

            //concatenate the two collections, group by name and then order as expected
            var groupCount = firstNames.Concat(surnames)
                .Where(x => !string.IsNullOrEmpty(x.Name))
                .GroupBy(x => x.Name)
                .Select(x => new { Name = x.Key, Count = x.Count() })
                .OrderBy(x => x.Name)
                .OrderByDescending(x => x.Count);

            return groupCount.Select(x => string.Concat(x.Name, ", ", x.Count)).ToList();
        }

        /// <summary>
        /// Because the Address is correctly populated when the People list is populated the Linq below is pretty easy to order by street
        /// </summary>
        /// <returns>List of strings correctly formatted and ordered as expected</returns>
        public List<string> GetAddresses()
        {
            var addresses = People.OrderBy(x => x.Address.Street)
                .Select(x => string.Concat(x.Address.Number, " ", x.Address.Street))
                .ToList();

            return addresses;
        }

        /// <summary>
        /// Method used to save the items to file
        /// </summary>
        /// <param name="Items">List of strings to save</param>
        /// <param name="FileName">Name of file (file is saved to current directory)</param>
        public void SaveToFile(List<string> Items, string FileName)
        {
            File.WriteAllLines(Path.Combine(Directory.GetCurrentDirectory(), FileName), Items.ToArray());
        }
    }
}
