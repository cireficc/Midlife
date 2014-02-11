using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language
{
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
}