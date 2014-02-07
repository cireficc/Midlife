using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace objectsTOxml
{

    //needs to be public
    public class WordLite
    {
        /*
         NOTE:  for the word and definition feilds if you do not want a public setter
         * you have to use IXmlSerializable interface and implement it 
         * 
         * which is really not that difficult. and with a complex class like Word we may want to 
         * so we can optimize it.
         
         */


        string word;
        /// <summary>
        /// the text of the word
        /// </summary>
        public string Word { get { return word; } set { word = value; } }

        string definition;
        /// <summary>
        /// the definitino of the word
        /// </summary>
        public string Definition { get { return definition; } set { definition = value; } }
        
        /// <summary>
        /// creates a light version of midlife's Word class
        /// </summary>
        /// <param name="w">the word to define</param>
        /// <param name="d">the definition of the word</param>
        public WordLite(string w,string d)
        {
            word = w;
            definition = d;
        }

        //needs a parameterless constructor for serialization
        public WordLite()
        {
            word = "";
            definition = "";
        }
    }
}
