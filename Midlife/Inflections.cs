using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language
{
    public abstract class ConjugationTable
    {
        public ConjugationTable()
        {//DD 020614
            //DD this could be very usefull for optional serializing
        }

        public abstract void printTable();
    }

    public class NounTable : ConjugationTable
    {
        static readonly char[] ValidGender = new char[] { 'm', 'f', 'b' };

        public NounTable()
            : base()
        { //DD 020614
            gender = '\0';
            ms = "";
            fs = "";
            mpl = "";
            fpl = "";
        }

        public NounTable(char GENDER, string MS, string FS, string MPL, string FPL)
            : base()
        {//DD 020614
            //check for valid input
            if (NounTable.ValidGender.Contains(GENDER))
            {
                gender = GENDER;
                ms = MS;
                fs = FS;
                mpl = MPL;
                fpl = FPL;
            }
            else throw new ArgumentException("Invalid GENDER. Please only use 'm', 'f', or 'b'");
        }


        /*
         * The gender of the noun:
         * 'ms' (masculin singular)
         * 'fs' (feminin singular)
         * 'mpl' (masculin plural)
         * 'fpl' (feminin plural)
         * gender --> 'm' (masculin), 'f' (feminin), 'b' (both, m & f)
         */
        public char gender { get; set; }
        public string ms { get; set; }
        public string fs { get; set; }
        public string mpl { get; set; }
        public string fpl { get; set; }

        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Noun Inflection Table"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "========================================"));
            Console.WriteLine(); Console.WriteLine();
            Console.WriteLine("Gender            --> {0}", gender);
            Console.WriteLine("Masculin singular --> {0}", ms);
            Console.WriteLine("Feminin singular  --> {0}", fs);
            Console.WriteLine("Masculin plural   --> {0}", mpl);
            Console.WriteLine("Feminin plural    --> {0}", fpl);
            Console.WriteLine(); Console.WriteLine();
        }
    }

    public class AdjectiveTable : ConjugationTable
    {
        //

        static readonly char[] ValidLocation = new char[] { 'b', 'a', 'n' };

        public AdjectiveTable()
            : base()
        {//DD 020614
            ms = "";
            fs = "";
            mpl = "";
            fpl = "";
            na = "";
            location = 'n';
        }

        public AdjectiveTable(string MS, string FS, string MPL, string FPL, string NA, char LOCATION)
            : base()
        {//DD 020614
            //check for valid input
            if (AdjectiveTable.ValidLocation.Contains(LOCATION))
            {//LOCATION is valid
                ms = MS;
                fs = FS;
                mpl = MPL;
                fpl = FPL;
                na = NA;
                location = LOCATION;
            }//throw exception if Location is not valid
            else throw new ArgumentException("Invalid LOCATION. Please only use 'b', 'a', or 'n'");
        }



        /*
         * The gender of the adjective:
         * 'ms' (masculin singular)
         * 'fs' (feminin singular)
         * 'mpl' (masculin plural)
         * 'fpl' (feminin plural)
         * 'na' (non-aspirate)
         */

        public string ms { get; set; }
        public string fs { get; set; }
        public string mpl { get; set; }
        public string fpl { get; set; }
        public string na { get; set; }
        /*
         * The location of the adjective around the noun:
         * 'b' (before)
         * 'a' (after)
         * 'n' (neutral) --> the adjective can come before OR after the noun.
         */
        public char location { get; set; }

        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Adjective Inflection Table"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "========================================"));
            Console.WriteLine(); Console.WriteLine();
            Console.WriteLine("Masculin singular --> {0}", ms);
            Console.WriteLine("Feminin singular  --> {0}", fs);
            Console.WriteLine("Masculin plural   --> {0}", mpl);
            Console.WriteLine("Feminin plural    --> {0}", fpl);
            Console.WriteLine("Non-aspirate      --> {0}", na);
            Console.WriteLine("Location          --> {0}", location);
            Console.WriteLine(); Console.WriteLine();
        }
    }
}
