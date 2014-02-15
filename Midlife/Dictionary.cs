using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language
{
    [System.Xml.Serialization.XmlRoot(ElementName = "Dictionary")]
    public class ExtendedDictionary : System.Xml.Serialization.IXmlSerializable
    {
        public ExtendedDictionary()
        {//DD 020614
            wordList = new SortedDictionary<string, List<WordForm>>();
            dictionary = new SortedDictionary<string, Word>();
        }

        /*
         * The spell-checking wordlist. Will be used in spell-checking as well
         * as in the parsing process. Structure is as follows:
         * 
         * word (conjugated; KEY) -> WordForm (rootWord, formIdentifier)
         * English: the primary key is any word (conjugated). The value
         * of the key is a List of WordForm Objects that contain the root word
         * (for easy lookup), as well as the grammatical identifier of the
         * root word (very useful in the parsing process).
         * 
         * 'suis' -> ('être', IndPreFPS)   // First person singular, present, indicative of the verb 'être'
         *           ('suivre', IndPreFPS) // First person singular, present, indicative of the verb 'suivre'
         *           ('suivre', IndPreSPS) // Second person singular, present, indicative of the verb 'suivre'
         *           ('suivre', ImpPreSPS)  // Second person singular, present, imperative of the verb 'suivre'
         */
        public SortedDictionary<string, List<WordForm>> wordList { get; set; }
        /*
         * The actual dictionary, used for lookups. It will only contain the infinitive versions of words as
         * keys - references will link to the infinitive which can be found in this SortedDictionary, and
         * further analysis can be done once the entry is found.
         */
        public SortedDictionary<string, Word> dictionary { get; set; }

        public struct WordForm
        {
            public string rootWord { get; set; }
            public string formIdentifier { get; set; }
        }


        #region IXmlSerializable
        //DD 02/15/14
        //the real and only implementation of IXmlSerializable
        

        public System.Xml.Schema.XmlSchema GetSchema()
        {//we may use this in the future but for now we won't bother with it
            throw new NotImplementedException();
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {

            bool isempty = reader.IsEmptyElement;
            reader.ReadStartElement();

            int counter = 0;

            //read words 
            while (reader.Name == "Word")
            {//this is where the money is, \
             //this has to be able to deserialize an xml file with 100,000 entries!

                Word w = new Word();
                w.readXML(reader);

                //!
                //I have assumed that the key for each Dictionary<string,Word> entry
                //is the actual word itself. otherwise this line needs changed.
                //!
                dictionary.Add(w.word + "_" + counter, w);
                counter++;
            }


            if (!isempty)
            {
                reader.ReadEndElement();
            }
            
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            /*
                Since the xml schema only shows the dictionary has storing Word objects
             * I will write the code to serialize the list of words
             * 
             * but in the reader I will need help with how the wordlist is to be constructed
             */



            foreach (Word w in dictionary.Values)
            {
                //word will always contain <ConjugationTables/>
                //smallest word entry possible is 
                /*
                 * this engine should render a completly blank Word instance as such 
                 *
                 * <Word>
                 *      <GrammaticalForms/>
                 *      <ConjugationTables/>
                 * </Word>
                 *                   
                 */

                writer.WriteStartElement("Word");
                w.writeXML(writer);
                writer.WriteEndElement();
            }


        }

        #endregion
    }
}