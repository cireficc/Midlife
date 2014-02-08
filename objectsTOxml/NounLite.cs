using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace objectsTOxml
{
    public class NounLite : ConjugationLite
    {        
        public NounLite()          
        { 
            gender = 'g';
            ms = "";
            fs = "";
            mpl = "";
            fpl = "";
        }
        public NounLite(char GENDER, string MS, string FS, string MPL, string FPL)
        {
            gender = GENDER;
            ms = MS;
            fs = FS;
            mpl = MPL;
            fpl = FPL;
        }        

        /*
         * The gender of the noun:
         * 'ms' (masculin singular)
         * 'fs' (feminin singular)
         * 'mpl' (masculin plural)
         * 'fpl' (feminin plural)
         * gender --> 'm' (masculin), 'f' (feminin), 'b' (both, m & f)
         */
        [XmlAttribute(AttributeName="gender")]
        public char gender { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = "ms")]
        public string ms { get; set; }
        
        [XmlAttribute(DataType = "string", AttributeName = "fs")]
        public string fs { get; set; }
        
        [XmlAttribute(DataType = "string", AttributeName = "mpl")]
        public string mpl { get; set; }
        
        [XmlAttribute(DataType = "string", AttributeName = "fpl")]
        public string fpl { get; set; }

    }
}
