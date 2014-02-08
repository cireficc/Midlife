using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//our favorite namespace
using System.Xml.Serialization;

namespace objectsTOxml
{
     
    public class AdjectiveLite :ConjugationLite
    {//this example takes code from Dictionary.cs  


        public AdjectiveLite(string MS, string FS, string MPL, string FPL, string NA, char LOCATION)
        {         
                ms = MS;
                fs = FS;
                mpl = MPL;
                fpl = FPL;
                na = NA;
                location = LOCATION;           
        }

        public AdjectiveLite()
        {
            ms = "";
            fs = "";
            mpl = "";
            fpl = "";
            na = "";
            location = 'L';//remember this is a test 

           
        }

        

        /*
         * The gender of the adjective:
         * 'ms' (masculin singular)
         * 'fs' (feminin singular)
         * 'mpl' (masculin plural)
         * 'fpl' (feminin plural)
         * 'na' (non-aspirate)
         */

        [XmlAttribute(AttributeName="ms",DataType="string")]
        public string ms { get; set; }
        
        [XmlAttribute(AttributeName = "fs", DataType = "string")]
        public string fs { get; set; }
        
        [XmlAttribute(AttributeName = "mpl", DataType = "string")]
        public string mpl { get; set; }
        
        [XmlAttribute(AttributeName = "fpl", DataType = "string")]
        public string fpl { get; set; }
        
        [XmlAttribute(AttributeName = "na", DataType = "string")]
        public string na { get; set; }
        
        /*
         * The location of the adjective around the noun:
         * 'b' (before)
         * 'a' (after)
         * 'n' (neutral) --> the adjective can come before OR after the noun.
         */
        [XmlAttribute(AttributeName = "location")]
        public char location { get; set; }//this may have to be enum or string xml does not have a char type

    }
}
