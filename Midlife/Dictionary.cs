using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dictionarySet
{

    class Dictionary
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
        public List<GrammaticalForms> forms { get; set; }

        public struct GrammaticalForms
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
    }

    class NounTable
    {
        /*
         * The gender of the noun:
         * 'ms' (masculin singular)
         * 'fs' (feminin singular)
         * 'mpl' (masculin plural)
         * 'fpl' (feminin plural)
         */
        public string gender { get; set; }
        public string masculinSingular { get; set; }
        public string femininSingular { get; set; }
        public string masculinPlural { get; set; }
        public string femininPlural { get; set; }
    }

    class AdjectiveTable
    {
        public string masculinSingular { get; set; }
        public string femininSingular { get; set; }
        public string masculinPlural { get; set; }
        public string femininPlural { get; set; }
        public string nonAspirate { get; set; }
        /*
         * The location of the adjective around the noun:
         * 'b' (before)
         * 'a' (after)
         * 'n' (neutral) --> the adjective can come before OR after the noun.
         */
        public char location { get; set; }
    }

    class VerbTable
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
        public string[] prepositions { get; set; }
        // Whether or not the verb is transitive.
        public bool transitive { get; set; }
        /*
         * Whether or not the verb has a pronominal form. If true, a function will later
         * conjugate the pronominal infinitive of the verb for lookup in the Dictionary.
         * This saves space over allocating a string of the conjugated pronominal infinitive.
         */
        public bool pronominalForm { get; set; }

        struct IndicativePresent
        {
            public string firstPersonSingular { get; set; }
            public string secondPersonSingular { get; set; }
            public string thirdPersonSingular { get; set; }
            public string firstPersonPlural { get; set; }
            public string secondPersonPlural { get; set; }
            public string thirdPersonPlural { get; set; }
        }
        struct IndicativeSimplePast
        {
            public string firstPersonSingular { get; set; }
            public string secondPersonSingular { get; set; }
            public string thirdPersonSingular { get; set; }
            public string firstPersonPlural { get; set; }
            public string secondPersonPlural { get; set; }
            public string thirdPersonPlural { get; set; }
        }
        struct IndicativePresentPerfect
        {
            public string firstPersonSingular { get; set; }
            public string secondPersonSingular { get; set; }
            public string thirdPersonSingular { get; set; }
            public string firstPersonPlural { get; set; }
            public string secondPersonPlural { get; set; }
            public string thirdPersonPlural { get; set; }
        }
        struct IndicativePastPerfect
        {
            public string firstPersonSingular { get; set; }
            public string secondPersonSingular { get; set; }
            public string thirdPersonSingular { get; set; }
            public string firstPersonPlural { get; set; }
            public string secondPersonPlural { get; set; }
            public string thirdPersonPlural { get; set; }
        }
        struct IndicativeImperfect
        {
            public string firstPersonSingular { get; set; }
            public string secondPersonSingular { get; set; }
            public string thirdPersonSingular { get; set; }
            public string firstPersonPlural { get; set; }
            public string secondPersonPlural { get; set; }
            public string thirdPersonPlural { get; set; }
        }
        struct IndicativePluperfect
        {
            public string firstPersonSingular { get; set; }
            public string secondPersonSingular { get; set; }
            public string thirdPersonSingular { get; set; }
            public string firstPersonPlural { get; set; }
            public string secondPersonPlural { get; set; }
            public string thirdPersonPlural { get; set; }
        }
        struct IndicativeFuture
        {
            public string firstPersonSingular { get; set; }
            public string secondPersonSingular { get; set; }
            public string thirdPersonSingular { get; set; }
            public string firstPersonPlural { get; set; }
            public string secondPersonPlural { get; set; }
            public string thirdPersonPlural { get; set; }
        }
        struct IndicativePastFuture
        {
            public string firstPersonSingular { get; set; }
            public string secondPersonSingular { get; set; }
            public string thirdPersonSingular { get; set; }
            public string firstPersonPlural { get; set; }
            public string secondPersonPlural { get; set; }
            public string thirdPersonPlural { get; set; }
        }

        struct SubjunctivePresent
        {
            public string firstPersonSingular { get; set; }
            public string secondPersonSingular { get; set; }
            public string thirdPersonSingular { get; set; }
            public string firstPersonPlural { get; set; }
            public string secondPersonPlural { get; set; }
            public string thirdPersonPlural { get; set; }
        }
        struct SubjunctivePast
        {
            public string firstPersonSingular { get; set; }
            public string secondPersonSingular { get; set; }
            public string thirdPersonSingular { get; set; }
            public string firstPersonPlural { get; set; }
            public string secondPersonPlural { get; set; }
            public string thirdPersonPlural { get; set; }
        }
        struct SubjunctiveImperfect
        {
            public string firstPersonSingular { get; set; }
            public string secondPersonSingular { get; set; }
            public string thirdPersonSingular { get; set; }
            public string firstPersonPlural { get; set; }
            public string secondPersonPlural { get; set; }
            public string thirdPersonPlural { get; set; }
        }
        struct SubjunctivePluperfect
        {
            public string firstPersonSingular { get; set; }
            public string secondPersonSingular { get; set; }
            public string thirdPersonSingular { get; set; }
            public string firstPersonPlural { get; set; }
            public string secondPersonPlural { get; set; }
            public string thirdPersonPlural { get; set; }
        }
        struct ConditionalPresent
        {
            public string firstPersonSingular { get; set; }
            public string secondPersonSingular { get; set; }
            public string thirdPersonSingular { get; set; }
            public string firstPersonPlural { get; set; }
            public string secondPersonPlural { get; set; }
            public string thirdPersonPlural { get; set; }
        }
        struct ConditionalFirstPast
        {
            public string firstPersonSingular { get; set; }
            public string secondPersonSingular { get; set; }
            public string thirdPersonSingular { get; set; }
            public string firstPersonPlural { get; set; }
            public string secondPersonPlural { get; set; }
            public string thirdPersonPlural { get; set; }
        }
        struct ConditionalSecondPast
        {
            public string firstPersonSingular { get; set; }
            public string secondPersonSingular { get; set; }
            public string thirdPersonSingular { get; set; }
            public string firstPersonPlural { get; set; }
            public string secondPersonPlural { get; set; }
            public string thirdPersonPlural { get; set; }
        }
        struct ImperativePresent
        {
            public string secondPersonSingular { get; set; }
            public string firstPersonPlural { get; set; }
            public string secondPersonPlural { get; set; }
        }
        struct ImperativePast
        {
            public string secondPersonSingular { get; set; }
            public string firstPersonPlural { get; set; }
            public string secondPersonPlural { get; set; }
        }
        struct Infinitive
        {
            public string present { get; set; }
            public string past { get; set; }
        }
        struct Participle
        {
            public string present { get; set; }
            public string past { get; set; }
        }
    }
}