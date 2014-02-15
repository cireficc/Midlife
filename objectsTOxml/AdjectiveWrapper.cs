using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;
using Language;
using System.IO;

namespace objectsTOxml
{
    
    public class AdjectiveWrapper 
    {
        AdjectiveTable AT;

        public void printTable()
        {
            AT.printTable();
        }

        public AdjectiveWrapper(AdjectiveTable at)
        {
            AT = at;
        }


        public AdjectiveWrapper()
        {
            AT = new AdjectiveTable();
        }




        public void ReadXml(System.Xml.XmlReader reader)
        {
            //read attributes
            AT = new AdjectiveTable();
            AT.ms = reader["ms"];
            AT.fs = reader["fs"];
            AT.mpl = reader["mpl"];
            AT.fpl = reader["fpl"];
            AT.na = reader["na"];

            AT.location = reader["location"][0];

            //read element
            bool isempty = reader.IsEmptyElement;//check if empty
            reader.ReadStartElement();
            if (!isempty) reader.ReadEndElement();//if empty we don't read end node
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {

            writer.WriteAttributeString("ms", AT.ms);
            writer.WriteAttributeString("fs", AT.fs);
            writer.WriteAttributeString("mpl", AT.mpl);
            writer.WriteAttributeString("fpl", AT.fpl);

            writer.WriteAttributeString("na", AT.na);
            writer.WriteAttributeString("location", new string(new char[] { AT.location }));



        }
    }
}
