using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using Language;

namespace objectsTOxml
{
    
    class Program
    {
        
        //this attribute is so we can call SaveFileDialog
        [STAThread]
        static void Main(string[] args)
        {
            
            XmlSerializer xml = new XmlSerializer(typeof(WordLite));

            NounTable NT = new NounTable();
            NT.gender = 'm';
            NT.ms = "MALE Singular";
            NT.fs = "FEMALE Singular";
            NT.mpl = "MALE Plural";
            NT.fpl = "FEMALE Plural";

            AdjectiveTable AT = new AdjectiveTable();
            AT.ms = "MAN FORM";
            AT.fs = "CHICK FORM";
            AT.mpl = "MEN FORM";
            AT.fpl = "CHICKS FORM";
            AT.na = "NOT Aspirate";
            AT.location = 'n';


            WordLite wl = new WordLite();           

            AdjectiveWrapper at = new AdjectiveWrapper(AT);
            NounWrapper nt = new NounWrapper(NT);


            ///////////////////////////////////////////////
            //Edit CoDE here /////////////////////////////////////////////////////////////////////////////////////////

           

            wl.setConjugation(at);
            wl.setConjugation(nt);


            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////


            using (FileStream fs = File.Open("brightchild.xml", FileMode.Create, FileAccess.ReadWrite))
            {
                //test the xml writer
                xml.Serialize(fs, wl);

                fs.Flush();//make sure everything is written to file
                fs.Seek(0, SeekOrigin.Begin);//go back to begining

                using (StreamReader sr = new StreamReader(fs))
                {
                    //view the output of the writer
                    Console.WriteLine(sr.ReadToEnd());

                    //reset the stream
                    fs.Flush();
                    fs.Seek(0, SeekOrigin.Begin);


                    //reset the word
                    wl = null;
                    //test the xml reader
                    wl = (WordLite)xml.Deserialize(fs);

                    //view result of deserialization
                    Console.WriteLine("\r\n\n");
                    wl.printTable();//print table rocks!!
                }

            }

            Console.ReadLine();
        }
    }
}
