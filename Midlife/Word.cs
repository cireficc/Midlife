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

            nounTable = new NounTable();
            adjectiveTable = new AdjectiveTable();
            verbTable = new VerbTable();
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
