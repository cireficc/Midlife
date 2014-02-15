using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace objectsTOxml
{
    [XmlRoot(ElementName="Word")]
    public class WordLite : IXmlSerializable
    {

        
        AdjectiveWrapper AW = null;
        NounWrapper NW = null;

        public WordLite()
        {
            //for the xml stuff
        }

        /// <summary>
        /// sets an adjective if the adjective is null
        /// </summary>
        /// <param name="aw">the Adjective wrapper</param>
        /// <returns>true if successful and false if it failed</returns>
        public bool setConjugation(AdjectiveWrapper aw)
        {
            if (AW == null) AW = aw;
            else return false;
            return true;
        }


        /// <summary>
        /// sets an Noun if the Noun is null
        /// </summary>
        /// <param name="aw">the Noun wrapper</param>
        /// <returns>true if successful and false if it failed</returns>
        public bool setConjugation(NounWrapper nw)
        {
            if (NW == null) NW = nw;
            else return false;
            return true;
        }

        internal void printTable()
        {
            if (AW != null) AW.printTable();
            if (NW != null) NW.printTable();
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            reader.ReadStartElement();//read word
            reader.ReadStartElement();//read conjugation table

            string value = reader.Value;


            //test and read
            if (reader.Name == "Noun")
            {
                NW = new NounWrapper();
                NW.ReadXml(reader);
            }

            //test and read
            if(reader.Name == "Adjective")
            {
                AW = new AdjectiveWrapper();
                AW.ReadXml(reader);
            }


            reader.ReadEndElement();
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteStartElement("ConjugationTables");

            if (NW != null)
            {
                writer.WriteStartElement("Noun");
                NW.WriteXml(writer);//noun will always come before adjective 
                writer.WriteEndElement();
            }

            if (AW != null)
            {
                writer.WriteStartElement("Adjective");
                AW.WriteXml(writer);//adjective will always be last
                writer.WriteEndElement();

            }

            //we will have to test and read the same way

            writer.WriteEndElement();
        }
    }
}
