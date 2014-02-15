using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language
{
    public class Word
    {

        public Word()
        {//DD 020614
            word = "";
            aspirate = false;

            forms = new List<GrammaticalForm>();

            nounTable = null;
            adjectiveTable = null;
            verbTable = null;
        }

        // the infinitive Word in the Dictionary
        public string word { get; set; }
        // Whether or not the Word is aspirate - adjective forms and phonetics change.
        public bool aspirate { get; set; }
        /*
         * The list of grammatical forms that this Word can have. For example, 'être':
         * 'être' --> vi (verb intransitive) "to be".
         * '(un) être' --> nm (noun masculin) "(a) being".
         */
        public List<GrammaticalForm> forms { get; set; }

        // The table of noun conjugations, if the Word has a grammatical form of a noun.
        public NounTable nounTable { get; set; }
        // The table of adjective conjugations, if the Word has a grammatical form of an adjective.
        public AdjectiveTable adjectiveTable { get; set; }
        // The table of verb conjugations, if the Word has a grammatical form of a verb.
        public VerbTable verbTable { get; set; }

        public void printConjugationTables()
        {
            nounTable.printTable();
            adjectiveTable.printTable();
            verbTable.printTable();
        }


        #region XML Functions
        //DD 02/15/2014
        //a reader/writer pair to aid serialization in the Word class       

        /// <summary>
        /// reads the xml and sets all fields 
        /// </summary>
        /// <param name="reader">the xml reader to use for reading</param>
        internal void readXML(System.Xml.XmlReader reader)
        {
            word = reader["word"];
            aspirate = Convert.ToBoolean(reader["aspirate"]);
            
            reader.ReadStartElement();//<Word>
                        
            //read grammatical forms////////////////////////////////////////////////////////////////////
            bool noforms = reader.IsEmptyElement;
            reader.ReadStartElement("GrammaticalForms");//<GrammaticalForms>
                     
            //not no forms = has forms so we read the end element
            //just incase a word was entered with no grammatical forms
            if (!noforms)
            {

                forms = new List<GrammaticalForm>();

                while (reader.Name == "GrammaticalForm")//loop till we don't have grammatical forms to read
                {
                    GrammaticalForm gf = new GrammaticalForm();
                    gf.readXML(reader);//this read here is what increments the loop
                    //otherwise this could be an infinite loop

                    forms.Add(gf);//add the form we just read to the forms list
                }                     
                
                
                reader.ReadEndElement();

            }
            //////////////////////////////////////////////


            //selectivly read conjugationtables///////////////////////////////////////////////////////
            bool notables = reader.IsEmptyElement;
            reader.ReadStartElement("ConjugationTables");

            //not no tables = has tables so we read the end element.
            //test just incase a word was entered with no conjugation tables
            if (!notables)
            {
                if (reader.Name == "NounTable")
                {
                    nounTable = new NounTable();
                    nounTable.readXML(reader);

                }

                if (reader.Name == "AdjectiveTable")
                {
                    adjectiveTable = new AdjectiveTable();
                    adjectiveTable.readXML(reader);

                }

                if (reader.Name == "VerbTable")
                {
                    verbTable = new VerbTable();
                    verbTable.readXML(reader);
                }


                reader.ReadEndElement();
            }
            ////////////////////////////////////////////////////////////////////////
         
            reader.ReadEndElement();//</Word>
            
        }

        /// <summary>
        /// writes this class to xml
        /// </summary>
        /// <param name="writer">the xml writer to use for writing</param>
        internal void writeXML(System.Xml.XmlWriter writer)
        {
            //write the word data
            writer.WriteAttributeString("word",word);
            writer.WriteAttributeString("aspirate",Convert.ToString(aspirate));

            //write <Grammaticalforms>
            writer.WriteStartElement("GrammaticalForms");

            foreach(GrammaticalForm gf in forms )
            {
                writer.WriteStartElement("GrammaticalForm");//<grammaticalform ... />
                gf.writeXML(writer);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            ////////////////////////////////////////////////////////////////////////////////

            //write the conjugation tables
            writer.WriteStartElement("ConjugationTables");

            //if the Table is null then we move on
            if (nounTable != null)
            { 
                writer.WriteStartElement("NounTable");
                nounTable.writeXML(writer);
                writer.WriteEndElement();
            }

            if (adjectiveTable != null)
            {
                writer.WriteStartElement("AdjectiveTable");
                adjectiveTable.writeXML(writer);
                writer.WriteEndElement();
            }
            
            if (verbTable != null)
            {
                writer.WriteStartElement("VerbTable");
                verbTable.writeXML(writer);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        #endregion
    }

    public class GrammaticalForm
    {
        public GrammaticalForm()
        {
            form = "";
            definition = "";
            contexts = new List<Context>();
        }
        // The grammatical identifier of the form (e.g., 'vi' or 'nm').
        public string form { get; set; }
        // The definition (meaning) of the Word in a particular form.
        public string definition { get; set; }
        public List<Context> contexts { get; set; }


        #region XML Functions
        //DD 02/15/2014
        //a reader/writer pair to aid serialization in the Word class       

        /// <summary>
        /// reads the xml and sets all fields 
        /// </summary>
        /// <param name="reader">the xml reader to use for reading</param>
        internal void readXML(System.Xml.XmlReader reader)
        {
            form = reader["form"];
            definition = reader["definition"];

            //check just incase
            if (reader.IsEmptyElement) reader.ReadStartElement();
            else
            {
                reader.ReadStartElement();
                reader.ReadEndElement();
            }
        }

        /// <summary>
        /// writes this class to xml
        /// </summary>
        /// <param name="writer">the xml writer to use for writing</param>
        internal void writeXML(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("form", form);
            writer.WriteAttributeString("definition", definition);                        
        }
        #endregion
    }

    public class Context
    {
        public Context()
        {
            context = "";
            description = "";

            examples = new List<Example>();
        }

        // The context in which the word is used (in this grammatical form)
        public string context { get; set; }
        // The description (definition) of the word in this context
        public string description { get; set; }
        // A list of examples (usages and explanations) that help to define the context
        public List<Example> examples { get; set; }

    }

    public class Example
    {
        public Example()
        {
            // A particular phrase or example of the word in context
            usage = "";
            // The explanation of the usage in context
            explanation = "";
        }

        public string usage { get; set; }
        public string explanation { get; set; }
    }
}
