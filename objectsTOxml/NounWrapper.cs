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
    
    public class NounWrapper 
    {

        NounTable nt;

        public void printTable()
        {
            nt.printTable();
        }

        public NounWrapper(NounTable n)
        {
            nt = n;
        }

        public NounWrapper()
        { nt = null; }



        public void ReadXml(System.Xml.XmlReader reader)
        {
            nt = new NounTable();

            //read attributes before we read the node
            try { nt.gender = reader["gender"][0]; }
            catch (Exception EX) { Console.WriteLine(EX.Message + "\r\n" + EX.StackTrace + "\r\n\n"); }

            nt.ms = reader["ms"];
            nt.fs = reader["fs"];
            nt.mpl = reader["mpl"];
            nt.fpl = reader["fpl"];

            //read the element
            bool isempty = reader.IsEmptyElement;
            reader.ReadStartElement();
            if(!isempty)reader.ReadEndElement(); //check to see if we need to read the closing element
            //however the schema says this will always be an empty element
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            

            writer.WriteAttributeString("gender",new string(new char[] { nt.gender }));//trick it into being a string
            writer.WriteAttributeString("ms", nt.ms);
            writer.WriteAttributeString("fs", nt.fs);
            writer.WriteAttributeString("mpl", nt.mpl);
            writer.WriteAttributeString("fpl", nt.fpl);


        }
    }
}
