using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace objectsTOxml
{

    

    //needs to be public
    public class WordLite
    {
        #region vars
        string word;
        /// <summary>
        /// the text of the word
        /// </summary>
        [XmlElement(DataType="string",ElementName="Word")]
        public string Word { get { return word; } set { word = value; } }// i did not want a public setter but for a simple xml demo i will use it

        string definition;
        /// <summary>
        /// the definition of the word
        /// </summary>
        [XmlElement(DataType="string",ElementName="Definition")]
        public string Definition { get { return definition; } set { definition = value; } }

        /// <summary>
        /// this will replace having all three conjugation tables in every instance of a Word class
        /// </summary>
        
        [XmlElement(Type=typeof(ConjugationLite) , ElementName = "Conjugation")]//may not need this one
        [XmlElement(Type=typeof(AdjectiveLite), ElementName = "Adjective")]
        [XmlElement(Type=typeof(NounLite), ElementName = "Noun")]
        public List<ConjugationLite> conjugations = new List<ConjugationLite>();//this will be public untill I implement IXmlSerializable




        /// <summary>
        /// adds an adjective to the conjugationTable
        /// </summary>
        /// <param name="al">the AdjectiveLite object to add to this word</param>
        /// <remarks>you can only add one of each type</remarks>
        public void addAdjective(AdjectiveLite al)
        {//if there is a Adjective already added to the conjugation table throw exception
            if (!conjugations.Select(a => a.GetType()).Contains(typeof(AdjectiveLite)))
                conjugations.Add(al);
            else throw new Exception("Already has Adjective");
            
        }

        /// <summary>
        /// adds a noun object to this word
        /// </summary>
        /// <param name="nn">the NounLite object to add</param>
        public void addNoun(NounLite nn)
        {//if there is a Noun already added to the conjugation table throw exception
            if (!conjugations.Select(a => a.GetType()).Contains(typeof(NounLite)))
                conjugations.Add(nn);
            else throw new Exception("Already has Noun");
        }




        //experimental: gets conjugation by the type you want
        /// <summary>
        /// returns the Conjugation table by the type you specify
        /// </summary>
        /// <typeparam name="CT">Conjugation Table type. only use AdjectiveLite, or NounLite</typeparam>
        /// <returns>the Conjugation Table as your request type or null</returns>
        public CT getConjugationTable<CT>() where CT : ConjugationLite//must be a conjugation table
        {
            //search for matching types; if there are no matching types; an exception will be thrown and we can return null
            try { return (CT)conjugations.Where(a => a.GetType() == typeof(CT)).First(); }
            catch { return null; }//return null if there are no tables that matche your type                        
        }



        #endregion


        #region constructors

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

        #endregion


        #region methods


        #endregion

    }
}
