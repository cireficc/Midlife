using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace objectsTOxml
{
    
    class Program
    {

        //this attribute is so we can call SaveFileDialog
        [STAThread]
        static void Main(string[] args)
        {
            string word = "";
            string def = "";
            Console.Write("type a word: ");
            word = Console.ReadLine();

            Console.Write("type the definition: ");
            def = Console.ReadLine();

            Console.Write("Are you ready to save an xml file? (y or n): ");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "XML|*.xml";

                if (sfd.ShowDialog() == DialogResult.OK)
                { //save the file

                    XmlSerializer xml = new XmlSerializer(typeof(WordLite));
                    WordLite wl = new WordLite(word, def);

                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write))
                    {//create the xml file
                        xml.Serialize(fs, wl);
                    }

                    Console.WriteLine("YOU have made an xml file check it out at " + sfd.FileName);
                }
                else 
                {
                    Console.WriteLine("Please leave. You have hurt my feelings.\r\n All I did was try to make a simple xml file for you \r\n and you just quit. THANKS!");                    
                }

                Console.ReadLine();

            }

            



        }
    }
}
