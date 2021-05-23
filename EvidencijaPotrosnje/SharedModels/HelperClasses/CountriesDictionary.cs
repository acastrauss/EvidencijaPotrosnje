using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;


namespace SharedModels.HelperClasses
{
    public class CountriesDictionary
    {
        public Dictionary<string, string> CountriesShort = new Dictionary<string, string>();
    
        public CountriesDictionary()
        {
            string path = "../AppData/CountriesData/drzave.csv";
            //SharedModels.

            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    //StateConsumptionModel scm = new StateConsumptionModel();
                    string[] fields = csvParser.ReadFields();

                    // KEY IS SHORT COUNTRY NAME, VALUE IS FULL NAME
                    CountriesShort.Add(fields[1], fields[0]);

                }
            }
        }   
        

    }
}
