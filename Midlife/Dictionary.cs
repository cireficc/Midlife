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
        // List will need to be extended to include accented vowels
        public static char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };
    }

    class ExtendedDictionary
    {

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

    class Word
    {
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

        public struct GrammaticalForm
        {
            // The grammatical identifier of the form (e.g., 'vi' or 'nm').
            public string form { get; set; }
            // The definition (meaning) of the Word in a particular form.
            public string definition { get; set; }
        }

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

    abstract class ConjugationTable
    {
        public abstract void printTable();
    }

    class NounTable : ConjugationTable
    {
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

    class AdjectiveTable : ConjugationTable
    {
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

    class VerbTable : ConjugationTable
    {
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
        //changed to list<string> type 2/14/14 Daniel; was string[]
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

    abstract class Indicative
    {
        public string fps { get; set; }
        public string sps { get; set; }
        public string tps { get; set; }
        public string fpp { get; set; }
        public string spp { get; set; }
        public string tpp { get; set; }

        public abstract void printTable();
    }

    abstract class Subjunctive
    {
        public string fps { get; set; }
        public string sps { get; set; }
        public string tps { get; set; }
        public string fpp { get; set; }
        public string spp { get; set; }
        public string tpp { get; set; }

        public abstract void printTable();
    }

    abstract class Conditional
    {
        public string fps { get; set; }
        public string sps { get; set; }
        public string tps { get; set; }
        public string fpp { get; set; }
        public string spp { get; set; }
        public string tpp { get; set; }

        public abstract void printTable();
    }

    abstract class Imperative
    {
        public string sps { get; set; }
        public string fpp { get; set; }
        public string spp { get; set; }

        public abstract void printTable();
    }

    class IndicativePresent : Indicative
    {

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

    class IndicativeSimplePast : Indicative
    {

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

    class IndicativePresentPerfect : Indicative
    {

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

    class IndicativePastPerfect : Indicative
    {

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

    class IndicativeImperfect : Indicative
    {

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

    class IndicativePluperfect : Indicative
    {

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

    class IndicativeFuture : Indicative
    {

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

    class IndicativePastFuture : Indicative
    {

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

    class SubjunctivePresent : Subjunctive
    {

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

    class SubjunctivePast : Subjunctive
    {

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

    class SubjunctiveImperfect : Subjunctive
    {

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

    class SubjunctivePluperfect : Subjunctive
    {

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

    class ConditionalPresent : Conditional
    {

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

    class ConditionalFirstPast : Conditional
    {

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

    class ConditionalSecondPast : Conditional
    {

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

    class ImperativePresent : Imperative
    {

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

    class ImperativePast : Imperative
    {

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

    class Infinitive
    {
        public string present { get; set; }
        public string past { get; set; }

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

    class Participle
    {
        public string present { get; set; }
        public string past { get; set; }

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