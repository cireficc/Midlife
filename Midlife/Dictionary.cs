/*
     Daniel de la Rosa 2/6/2014 :
     * Changes:
     *  public Constructors
     *      I have/will added public constructors marked with comment "{//DD [date]"
     *          NOTE: the .net xml serializer requires parameterless constructors 
     *          and all feilds you wish to write to xml be publicly writable
     *          and the class be public (so all classes here must be public)   
 *         
*/


namespace Language
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Global
    {
        /*
         * This list is for displaying conjugations correctly. It will also be used in grammatical analysis,
         * to see if the user formed the conjugation correctly. Needs to be extended to include accented vowels.
         */
        public static readonly char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };
    }

    public class ExtendedDictionary
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
    }

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

    public abstract class ConjugationTable
    {
        public ConjugationTable()
        {//DD 020614
            //DD this could be very usefull for optional serializing
        }

        public abstract void printTable();
    }

    public class NounTable : ConjugationTable
    {
        static readonly char[] ValidGender = new char[] { 'm', 'f', 'b' };

        public NounTable()
            : base()
        { //DD 020614
            gender = '\0';
            ms = "";
            fs = "";
            mpl = "";
            fpl = "";
        }

        public NounTable(char GENDER, string MS, string FS, string MPL, string FPL)
            : base()
        {//DD 020614
            //check for valid input
            if (NounTable.ValidGender.Contains(GENDER))
            {
                gender = GENDER;
                ms = MS;
                fs = FS;
                mpl = MPL;
                fpl = FPL;
            }
            else throw new ArgumentException("Invalid GENDER. Please only use 'm', 'f', or 'b'");
        }


        /*
         * The gender of the noun:
         * 'ms' (masculin singular)
         * 'fs' (feminin singular)
         * 'mpl' (masculin plural)
         * 'fpl' (feminin plural)
         * gender --> 'm' (masculin), 'f' (feminin), 'b' (both, m & f)
         */
        public char gender { get; set; }
        public string ms { get; set; }
        public string fs { get; set; }
        public string mpl { get; set; }
        public string fpl { get; set; }

        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Noun Inflection Table"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "========================================"));
            Console.WriteLine(); Console.WriteLine();
            Console.WriteLine("Gender            --> {0}", gender);
            Console.WriteLine("Masculin singular --> {0}", ms);
            Console.WriteLine("Feminin singular  --> {0}", fs);
            Console.WriteLine("Masculin plural   --> {0}", mpl);
            Console.WriteLine("Feminin plural    --> {0}", fpl);
            Console.WriteLine(); Console.WriteLine();
        }
    }

    public class AdjectiveTable : ConjugationTable
    {
        //
        
        static readonly char[] ValidLocation = new char[] { 'b', 'a', 'n' };

        public AdjectiveTable()
            : base()
        {//DD 020614
            ms = "";
            fs = "";
            mpl = "";
            fpl = "";
            na = "";
            location = 'n';
        }

        public AdjectiveTable(string MS, string FS, string MPL, string FPL, string NA, char LOCATION)
            : base()
        {//DD 020614
            //check for valid input
            if (AdjectiveTable.ValidLocation.Contains(LOCATION))
            {//LOCATION is valid
                ms = MS;
                fs = FS;
                mpl = MPL;
                fpl = FPL;
                na = NA;
                location = LOCATION;
            }//throw exception if Location is not valid
            else throw new ArgumentException("Invalid LOCATION. Please only use 'b', 'a', or 'n'");
        }



        /*
         * The gender of the adjective:
         * 'ms' (masculin singular)
         * 'fs' (feminin singular)
         * 'mpl' (masculin plural)
         * 'fpl' (feminin plural)
         * 'na' (non-aspirate)
         */
        
        public string ms { get; set; }
        public string fs { get; set; }
        public string mpl { get; set; }
        public string fpl { get; set; }
        public string na { get; set; }
        /*
         * The location of the adjective around the noun:
         * 'b' (before)
         * 'a' (after)
         * 'n' (neutral) --> the adjective can come before OR after the noun.
         */
        public char location { get; set; }

        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Adjective Inflection Table"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "========================================"));
            Console.WriteLine(); Console.WriteLine();
            Console.WriteLine("Masculin singular --> {0}", ms);
            Console.WriteLine("Feminin singular  --> {0}", fs);
            Console.WriteLine("Masculin plural   --> {0}", mpl);
            Console.WriteLine("Feminin plural    --> {0}", fpl);
            Console.WriteLine("Non-aspirate      --> {0}", na);
            Console.WriteLine("Location          --> {0}", location);
            Console.WriteLine(); Console.WriteLine();
        }
    }

    public class VerbTable : ConjugationTable
    {

        static readonly char[] ValidGroup = new char[] { 'f', 's', 't', 'e' };
        static readonly char[] ValidAUX = new char[] { 'e', 'a' };

        public VerbTable()
            : base()
        {//DD 020614
            group = 'f';
            auxillary = 'a';

            prepositions = new List<string>();
            transitive = false;
            pronominal = false;
        }

        public VerbTable(char GROUP, char AUX, bool TRANSITIVE, bool PRONOMINAL, params string[] PREPOSITIONS)
            : base()
        {//DD 020614
            if (VerbTable.ValidGroup.Contains(GROUP) && VerbTable.ValidAUX.Contains(AUX))
            {
                group = GROUP;
                auxillary = AUX;


                transitive = TRANSITIVE;
                pronominal = PRONOMINAL;

                prepositions.AddRange(PREPOSITIONS);
            }
            else throw new ArgumentException("GROUP or AUX is invalid");
        }

        /* 
         * The group the verb belongs to:
         * 'f' (first) --> er.
         * 's' (second) --> ir.
         * 't' (third) --> ir, oir, re.
         * 'e' (exception) --> être, avoir, etc.
         */
        public char group { get; set; }
        /*
         * The auxillary verb the verb takes:
         * 'e' (être).
         * 'a' (avoir).
         */
        public char auxillary { get; set; }
        // A list of grammatically-valid prepositions the verb can take.
        public List<string> prepositions { get; set; }
        // Whether or not the verb is transitive.
        public bool transitive { get; set; }
        /*
         * Whether or not the verb has a pronominal form. If true, a function will later
         * conjugate the pronominal infinitive of the verb for lookup in the Dictionary.
         * This saves space over allocating a string of the conjugated pronominal infinitive.
         */
        public bool pronominal { get; set; }

        /*
         * The subject of the verb determined by the markers:
         * 'fps' (first person singular)
         * 'sps' (second person singular)
         * 'tps' (third person singular)
         * 'fpp' (first person plural)
         * 'spp' (second person plural)
         * 'tpp' (third person plural)
         * 'present' (present tense)
         * 'past' (past tense)
         * and their accompanying conjugations.
         */

        // All of the different conjugation types are instantiated
        // when a VerbTable is instantiated.

        public IndicativePresent indicativePresent = new IndicativePresent();
        public IndicativeSimplePast indicativeSimplePast = new IndicativeSimplePast();
        public IndicativePresentPerfect indicativePresentPerfect = new IndicativePresentPerfect();
        public IndicativePastPerfect indicativePastPerfect = new IndicativePastPerfect();
        public IndicativeImperfect indicativeImperfect = new IndicativeImperfect();
        public IndicativePluperfect indicativePluperfect = new IndicativePluperfect();
        public IndicativeFuture indicativeFuture = new IndicativeFuture();
        public IndicativePastFuture indicativePastFuture = new IndicativePastFuture();
        public SubjunctivePresent subjunctivePresent = new SubjunctivePresent();
        public SubjunctivePast subjunctivePast = new SubjunctivePast();
        public SubjunctiveImperfect subjunctiveImperfect = new SubjunctiveImperfect();
        public SubjunctivePluperfect subjunctivePluperfect = new SubjunctivePluperfect();
        public ConditionalPresent conditionalPresent = new ConditionalPresent();
        public ConditionalFirstPast conditionalFirstPast = new ConditionalFirstPast();
        public ConditionalSecondPast conditionalSecondPast = new ConditionalSecondPast();
        public ImperativePresent imperativePresent = new ImperativePresent();
        public ImperativePast imperativePast = new ImperativePast();
        public Infinitive infinitive = new Infinitive();
        public Participle participle = new Participle();

        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Verb Conjugation Table"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "========================================"));
            Console.WriteLine(); Console.WriteLine();

            indicativePresent.printTable();
            indicativeSimplePast.printTable();
            indicativePresentPerfect.printTable();
            indicativePastPerfect.printTable();
            indicativeImperfect.printTable();
            indicativePluperfect.printTable();
            indicativeFuture.printTable();
            indicativePastFuture.printTable();
            subjunctivePresent.printTable();
            subjunctivePast.printTable();
            subjunctiveImperfect.printTable();
            subjunctivePluperfect.printTable();
            conditionalPresent.printTable();
            conditionalFirstPast.printTable();
            conditionalSecondPast.printTable();
            imperativePresent.printTable();
            imperativePast.printTable();
            infinitive.printTable();
            participle.printTable();
        }
    }

    public abstract class Indicative
    {
        public string fps { get; set; }
        public string sps { get; set; }
        public string tps { get; set; }
        public string fpp { get; set; }
        public string spp { get; set; }
        public string tpp { get; set; }

        public Indicative()
        {//DD020614
            fps = "";
            sps = "";
            tps = "";
            fpp = "";
            spp = "";
            tpp = "";
        }
        public Indicative(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
        {//DD020614
            fps = FPS;
            sps = SPS;
            tps = TPS;
            fpp = FPP;
            spp = SPP;
            tpp = TPP;
        }

        public abstract void printTable();
    }

    public abstract class Subjunctive
    {
        public string fps { get; set; }
        public string sps { get; set; }
        public string tps { get; set; }
        public string fpp { get; set; }
        public string spp { get; set; }
        public string tpp { get; set; }

        public Subjunctive()
        {//DD020614
            fps = "";
            sps = "";
            tps = "";
            fpp = "";
            spp = "";
            tpp = "";
        }

        public Subjunctive(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
        {//DD020614
            fps = FPS;
            sps = SPS;
            tps = TPS;
            fpp = FPP;
            spp = SPP;
            tpp = TPP;
        }

        public abstract void printTable();
    }

    public abstract class Conditional
    {
        public string fps { get; set; }
        public string sps { get; set; }
        public string tps { get; set; }
        public string fpp { get; set; }
        public string spp { get; set; }
        public string tpp { get; set; }

        public Conditional()
        {//DD020614
            fps = "";
            sps = "";
            tps = "";
            fpp = "";
            spp = "";
            tpp = "";
        }

        public Conditional(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
        {//DD020614
            fps = FPS;
            sps = SPS;
            tps = TPS;
            fpp = FPP;
            spp = SPP;
            tpp = TPP;
        }

        public abstract void printTable();
    }

    public abstract class Imperative
    {
        public string sps { get; set; }
        public string fpp { get; set; }
        public string spp { get; set; }

        public Imperative()
        {//DD020614
            sps = "";
            fpp = "";
            spp = "";
        }

        public Imperative(string SPS, string FPP, string SPP)
        {//DD020614
            spp = SPS;
            fpp = FPP;
            spp = SPP;
        }

        public abstract void printTable();
    }

    public class IndicativePresent : Indicative
    {

        public IndicativePresent()
            : base()
        { //DD 020614

        }

        public IndicativePresent(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
            : base(FPS, SPS, TPS, FPP, SPP, TPP)
        {//DD 020614 

        }



        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Indicative Present"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "=================="));
            Console.WriteLine();
            if (Global.vowels.Contains('x'))
                Console.WriteLine("J'         --> {0}", fps);
            else
                Console.WriteLine("Je         --> {0}", fps);
            Console.WriteLine("Tu         --> {0}", sps);
            Console.WriteLine("Il/Elle/On --> {0}", tps);
            Console.WriteLine("Nous       --> {0}", fpp);
            Console.WriteLine("Vous       --> {0}", spp);
            Console.WriteLine("Ils/Elles  --> {0}", tpp);
            Console.WriteLine();
        }
    }

    public class IndicativeSimplePast : Indicative
    {
        public IndicativeSimplePast()
            : base()
        {//DD 020614

        }

        public IndicativeSimplePast(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
            : base(FPS, SPS, TPS, FPP, SPP, TPP)
        {//DD 020614

        }

        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Indicative Simple Past"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "======================"));
            Console.WriteLine();
            if (Global.vowels.Contains('x'))
                Console.WriteLine("J'         --> {0}", fps);
            else
                Console.WriteLine("Je         --> {0}", fps);
            Console.WriteLine("Tu         --> {0}", sps);
            Console.WriteLine("Il/Elle/On --> {0}", tps);
            Console.WriteLine("Nous       --> {0}", fpp);
            Console.WriteLine("Vous       --> {0}", spp);
            Console.WriteLine("Ils/Elles  --> {0}", tpp);
            Console.WriteLine();
        }
    }

    public class IndicativePresentPerfect : Indicative
    {

        public IndicativePresentPerfect()
            : base()
        { //DD 020614

        }

        public IndicativePresentPerfect(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
            : base(FPS, SPS, TPS, FPP, SPP, TPP)
        {//DD 020614


        }

        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Indicative Present Perfect"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "=========================="));
            Console.WriteLine();
            if (Global.vowels.Contains('x'))
                Console.WriteLine("J'         --> {0}", fps);
            else
                Console.WriteLine("Je         --> {0}", fps);
            Console.WriteLine("Tu         --> {0}", sps);
            Console.WriteLine("Il/Elle/On --> {0}", tps);
            Console.WriteLine("Nous       --> {0}", fpp);
            Console.WriteLine("Vous       --> {0}", spp);
            Console.WriteLine("Ils/Elles  --> {0}", tpp);
            Console.WriteLine();
        }
    }

    public class IndicativePastPerfect : Indicative
    {
        public IndicativePastPerfect()
            : base()
        {//DD 020614 

        }

        public IndicativePastPerfect(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
            : base(FPS, SPS, TPS, FPP, SPP, TPP)
        {//DD 020614 

        }
        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Indicative Past Perfect"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "======================="));
            Console.WriteLine();
            if (Global.vowels.Contains('x'))
                Console.WriteLine("J'         --> {0}", fps);
            else
                Console.WriteLine("Je         --> {0}", fps);
            Console.WriteLine("Tu         --> {0}", sps);
            Console.WriteLine("Il/Elle/On --> {0}", tps);
            Console.WriteLine("Nous       --> {0}", fpp);
            Console.WriteLine("Vous       --> {0}", spp);
            Console.WriteLine("Ils/Elles  --> {0}", tpp);
            Console.WriteLine();
        }
    }

    public class IndicativeImperfect : Indicative
    {
        public IndicativeImperfect()
            : base()
        {//DD 020614

        }

        public IndicativeImperfect(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
            : base(FPS, SPS, TPS, FPP, SPP, TPP)
        {//DD 020614

        }


        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Indicative Imperfect"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "===================="));
            Console.WriteLine();
            if (Global.vowels.Contains('x'))
                Console.WriteLine("J'         --> {0}", fps);
            else
                Console.WriteLine("Je         --> {0}", fps);
            Console.WriteLine("Tu         --> {0}", sps);
            Console.WriteLine("Il/Elle/On --> {0}", tps);
            Console.WriteLine("Nous       --> {0}", fpp);
            Console.WriteLine("Vous       --> {0}", spp);
            Console.WriteLine("Ils/Elles  --> {0}", tpp);
            Console.WriteLine();
        }
    }

    public class IndicativePluperfect : Indicative
    {
        public IndicativePluperfect()
            : base()
        { }

        public IndicativePluperfect(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
            : base(FPS, SPS, TPS, FPP, SPP, TPP)
        { }


        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Indicative Pluperfect"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "====================="));
            Console.WriteLine();
            if (Global.vowels.Contains('x'))
                Console.WriteLine("J'         --> {0}", fps);
            else
                Console.WriteLine("Je         --> {0}", fps);
            Console.WriteLine("Tu         --> {0}", sps);
            Console.WriteLine("Il/Elle/On --> {0}", tps);
            Console.WriteLine("Nous       --> {0}", fpp);
            Console.WriteLine("Vous       --> {0}", spp);
            Console.WriteLine("Ils/Elles  --> {0}", tpp);
            Console.WriteLine();
        }
    }

    public class IndicativeFuture : Indicative
    {

        public IndicativeFuture()
            : base()
        {//DD 020614 

        }

        public IndicativeFuture(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
            : base(FPS, SPS, TPS, FPP, SPP, TPP)
        {//DD 020614

        }

        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Indicative Future"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "================="));
            Console.WriteLine();
            if (Global.vowels.Contains('x'))
                Console.WriteLine("J'         --> {0}", fps);
            else
                Console.WriteLine("Je         --> {0}", fps);
            Console.WriteLine("Tu         --> {0}", sps);
            Console.WriteLine("Il/Elle/On --> {0}", tps);
            Console.WriteLine("Nous       --> {0}", fpp);
            Console.WriteLine("Vous       --> {0}", spp);
            Console.WriteLine("Ils/Elles  --> {0}", tpp);
            Console.WriteLine();
        }
    }

    public class IndicativePastFuture : Indicative
    {

        public IndicativePastFuture()
            : base()
        {//DD 020614

        }

        public IndicativePastFuture(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
            : base(FPS, SPS, TPS, FPP, SPP, TPP)
        {//DD 020614

        }

        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Indicative Past Future"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "======================"));
            Console.WriteLine();
            if (Global.vowels.Contains('x'))
                Console.WriteLine("J'         --> {0}", fps);
            else
                Console.WriteLine("Je         --> {0}", fps);
            Console.WriteLine("Tu         --> {0}", sps);
            Console.WriteLine("Il/Elle/On --> {0}", tps);
            Console.WriteLine("Nous       --> {0}", fpp);
            Console.WriteLine("Vous       --> {0}", spp);
            Console.WriteLine("Ils/Elles  --> {0}", tpp);
            Console.WriteLine();
        }
    }

    public class SubjunctivePresent : Subjunctive
    {
        public SubjunctivePresent()
            : base()
        { //DD 020614

        }

        public SubjunctivePresent(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
            : base(FPS, SPS, TPS, FPP, SPP, TPP)
        { //DD 020614

        }

        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Subjunctive Present"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "==================="));
            Console.WriteLine();
            if (Global.vowels.Contains('x'))
                Console.WriteLine("J'         --> {0}", fps);
            else
                Console.WriteLine("Je         --> {0}", fps);
            Console.WriteLine("Tu         --> {0}", sps);
            Console.WriteLine("Il/Elle/On --> {0}", tps);
            Console.WriteLine("Nous       --> {0}", fpp);
            Console.WriteLine("Vous       --> {0}", spp);
            Console.WriteLine("Ils/Elles  --> {0}", tpp);
            Console.WriteLine();
        }
    }

    public class SubjunctivePast : Subjunctive
    {

        public SubjunctivePast()
            : base()
        {//DD 020614

        }

        public SubjunctivePast(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
            : base(FPS, SPS, TPS, FPP, SPP, TPP)
        {//DD 020614

        }

        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Subjunctive Past"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "================"));
            Console.WriteLine();
            if (Global.vowels.Contains('x'))
                Console.WriteLine("J'         --> {0}", fps);
            else
                Console.WriteLine("Je         --> {0}", fps);
            Console.WriteLine("Tu         --> {0}", sps);
            Console.WriteLine("Il/Elle/On --> {0}", tps);
            Console.WriteLine("Nous       --> {0}", fpp);
            Console.WriteLine("Vous       --> {0}", spp);
            Console.WriteLine("Ils/Elles  --> {0}", tpp);
            Console.WriteLine();
        }
    }

    public class SubjunctiveImperfect : Subjunctive
    {

        public SubjunctiveImperfect()
            : base()
        {//DD 020614

        }

        public SubjunctiveImperfect(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
            : base(FPS, SPS, TPS, FPP, SPP, TPP)
        {//DD 020614

        }

        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Subjunctive Imperfect"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "====================="));
            Console.WriteLine();
            if (Global.vowels.Contains('x'))
                Console.WriteLine("J'         --> {0}", fps);
            else
                Console.WriteLine("Je         --> {0}", fps);
            Console.WriteLine("Tu         --> {0}", sps);
            Console.WriteLine("Il/Elle/On --> {0}", tps);
            Console.WriteLine("Nous       --> {0}", fpp);
            Console.WriteLine("Vous       --> {0}", spp);
            Console.WriteLine("Ils/Elles  --> {0}", tpp);
            Console.WriteLine();
        }
    }

    public class SubjunctivePluperfect : Subjunctive
    {

        public SubjunctivePluperfect()
            : base()
        {//DD 020614

        }

        public SubjunctivePluperfect(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
            : base(FPS, SPS, TPS, FPP, SPP, TPP)
        {//DD 020614

        }


        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Subjunctive Pluperfect"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "======================"));
            Console.WriteLine();
            if (Global.vowels.Contains('x'))
                Console.WriteLine("J'         --> {0}", fps);
            else
                Console.WriteLine("Je         --> {0}", fps);
            Console.WriteLine("Tu         --> {0}", sps);
            Console.WriteLine("Il/Elle/On --> {0}", tps);
            Console.WriteLine("Nous       --> {0}", fpp);
            Console.WriteLine("Vous       --> {0}", spp);
            Console.WriteLine("Ils/Elles  --> {0}", tpp);
            Console.WriteLine();
        }
    }

    public class ConditionalPresent : Conditional
    {
        public ConditionalPresent()
            : base()
        {//DD 020614

        }

        public ConditionalPresent(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
            : base(FPS, SPS, TPS, FPP, SPP, TPP)
        {//DD 020614

        }

        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Conditional Present"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "==================="));
            Console.WriteLine();
            if (Global.vowels.Contains('x'))
                Console.WriteLine("J'         --> {0}", fps);
            else
                Console.WriteLine("Je         --> {0}", fps);
            Console.WriteLine("Tu         --> {0}", sps);
            Console.WriteLine("Il/Elle/On --> {0}", tps);
            Console.WriteLine("Nous       --> {0}", fpp);
            Console.WriteLine("Vous       --> {0}", spp);
            Console.WriteLine("Ils/Elles  --> {0}", tpp);
            Console.WriteLine();
        }
    }

    public class ConditionalFirstPast : Conditional
    {

        public ConditionalFirstPast()
            : base()
        {//DD 020614



        }

        public ConditionalFirstPast(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
            : base(FPS, SPS, TPS, FPP, SPP, TPP)
        {//DD 020614

        }


        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Conditional First Past"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "======================"));
            Console.WriteLine();
            if (Global.vowels.Contains('x'))
                Console.WriteLine("J'         --> {0}", fps);
            else
                Console.WriteLine("Je         --> {0}", fps);
            Console.WriteLine("Tu         --> {0}", sps);
            Console.WriteLine("Il/Elle/On --> {0}", tps);
            Console.WriteLine("Nous       --> {0}", fpp);
            Console.WriteLine("Vous       --> {0}", spp);
            Console.WriteLine("Ils/Elles  --> {0}", tpp);
            Console.WriteLine();
        }
    }

    public class ConditionalSecondPast : Conditional
    {

        public ConditionalSecondPast()
            : base()
        {//DD 020614



        }

        public ConditionalSecondPast(string FPS, string SPS, string TPS, string FPP, string SPP, string TPP)
            : base(FPS, SPS, TPS, FPP, SPP, TPP)
        {//DD 020614

        }

        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Conditional Second Past"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "======================="));
            Console.WriteLine();
            if (Global.vowels.Contains('x'))
                Console.WriteLine("J'         --> {0}", fps);
            else
                Console.WriteLine("Je         --> {0}", fps);
            Console.WriteLine("Tu         --> {0}", sps);
            Console.WriteLine("Il/Elle/On --> {0}", tps);
            Console.WriteLine("Nous       --> {0}", fpp);
            Console.WriteLine("Vous       --> {0}", spp);
            Console.WriteLine("Ils/Elles  --> {0}", tpp);
            Console.WriteLine();
        }
    }

    public class ImperativePresent : Imperative
    {

        public ImperativePresent()
            : base()
        {//DD 020614

        }

        public ImperativePresent(string SPS, string FPP, string SPP)
            : base(SPS, FPP, SPP)
        {//DD 020614

        }


        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Imperative Present"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "=================="));
            Console.WriteLine();
            Console.WriteLine("Tu         --> {0}", sps);
            Console.WriteLine("Nous       --> {0}", fpp);
            Console.WriteLine("Vous       --> {0}", spp);
            Console.WriteLine();
        }
    }

    public class ImperativePast : Imperative
    {

        public ImperativePast()
            : base()
        {//DD 020614

        }

        public ImperativePast(string SPS, string FPP, string SPP)
            : base(SPS, FPP, SPP)
        {//DD 020614

        }

        public override void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Imperative Past"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "==============="));
            Console.WriteLine();
            Console.WriteLine("Tu         --> {0}", sps);
            Console.WriteLine("Nous       --> {0}", fpp);
            Console.WriteLine("Vous       --> {0}", spp);
            Console.WriteLine();
        }
    }

    public class Infinitive
    {
        public string present { get; set; }
        public string past { get; set; }



        /// <summary>
        /// creates a blank Infinitive
        /// </summary>
        public Infinitive()
        {//DD 020614
            present = "";
            past = "";

        }


        /// <summary>
        /// creates a Infinitive
        /// </summary>
        /// <param name="pr">the present</param>
        /// <param name="pa">the past</param>
        public Infinitive(string pr, string pa)
        {//DD 020614
            present = pr;
            past = pa;
        }



        public void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Infinitive"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "=========="));
            Console.WriteLine();
            Console.WriteLine("Present    --> {0}", present);
            Console.WriteLine("Past       --> {0}", past);
            Console.WriteLine();
        }
    }

    public class Participle
    {
        public string present { get; set; }
        public string past { get; set; }

        #region Construction

        /// <summary>
        /// creates a blank Participle
        /// </summary>
        public Participle()
        {//DD 020614
            present = "";
            past = "";

        }


        /// <summary>
        /// creates a Participle
        /// </summary>
        /// <param name="pr">the present</param>
        /// <param name="pa">the past</param>
        public Participle(string pr, string pa)
        {//DD 020614
            present = pr;
            past = pa;
        }

        #endregion

        public void printTable()
        {
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Participle"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "=========="));
            Console.WriteLine();
            Console.WriteLine("Present    --> {0}", present);
            Console.WriteLine("Past       --> {0}", past);
            Console.WriteLine();
        }
    }
}