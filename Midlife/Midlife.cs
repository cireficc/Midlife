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
            // Just testing the console.
            Console.WriteLine("Loading...");

            // Create a new word (from the XML file in actuality)
            Language.Word w = new Language.Word();
            // These instantiations (there are many many more) would be done as the XML file is read for each Word...
            //w.word = "avoir";
            w.nounTable = new Language.NounTable();
            w.adjectiveTable = new Language.AdjectiveTable();
            w.verbTable = new Language.VerbTable();
            //w.verbTable.indicativePresent.fps = "ai";
            // Test the output. It works as expected.
            w.printConjugationTables();
            // Read the output so the console window stays open.            
            Console.Read();
        }
    }
}