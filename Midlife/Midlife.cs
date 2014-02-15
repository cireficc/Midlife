namespace Midlife
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Diagnostics;

    class Midlife
    {
        static void Main(string[] args)
        {
            //cool style
            Console.SetWindowPosition(0,0);
            Console.WindowWidth = Console.LargestWindowWidth - 40;
            Console.WindowHeight = Console.LargestWindowHeight - 5;
            Console.Title = "Midlife - French Dictionary";       
            Console.ForegroundColor = ConsoleColor.Green;//<----------------If you don't like the Green change it here
            Console.SetBufferSize(Console.LargestWindowWidth - 40, 750);
            


            // Create a new word
            Language.Word w = new Language.Word();
            // These instantiations (there are many many more) would be done as the XML file is read for each Word...
            w.word = "avoir";
            w.nounTable = new Language.NounTable();
            w.nounTable.gender = 'm';
            w.adjectiveTable = new Language.AdjectiveTable();
            w.verbTable = new Language.VerbTable();
            w.verbTable.indicativePresent = new Language.IndicativePresent("ai", "as", "a", "avons", "avez", "ont");
            w.adjectiveTable = new Language.AdjectiveTable("beau", "belle", "beaux", "belles", "beaux", 'b');
            // Test the output.
            w.printConjugationTables();


            //DD 02/15/14////////
            //create extended dictionary and fill with more than one Word (for testing)
            Language.ExtendedDictionary ED = new Language.ExtendedDictionary();
            ED.dictionary.Add(w.word, w);
            ED.dictionary.Add(w.word + "_A", w);
            ED.dictionary.Add(w.word + "_B", w);
            ED.dictionary.Add(w.word + "_C", w);

            //create the serializer
            System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(typeof(Language.ExtendedDictionary));

            
            //create a file to output the xml to
            using (System.IO.FileStream fs = new System.IO.FileStream("Midlife.xml", System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite))
            {
                //write xml file
                xml.Serialize(fs, ED);    

                //reset the stream to the begining (or: go to the begining of the file)
                fs.Flush();//make sure everything has been written to the file
                fs.Seek(0, System.IO.SeekOrigin.Begin);//go to begining of stream

                //output file to console to save steps during testing
                using (System.IO.StreamReader sr = new System.IO.StreamReader(fs))
                {
                    //output file contents to console
                    Console.WriteLine(sr.ReadToEnd());

                    //reset
                    fs.Seek(0, System.IO.SeekOrigin.Begin);

                    //test deserialization
                    ED = null;//reset Extended dictionary
                    ED = (Language.ExtendedDictionary)xml.Deserialize(fs);

                }
            }
            /////////////
            
            // Read the output so the console window stays open.            
            Console.Read();
        }
    }
}