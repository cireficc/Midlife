using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language
{
    namespace Grammar
    {
        class Global
        {
            /*
             * This list is for displaying conjugations correctly. It will also be used in grammatical analysis,
             * to see if the user formed the conjugation correctly. Needs to be extended to include vowels from
             * other languages/character sets (for proper nouns, particularly).
             */
            public static readonly char[] vowels = { 'a', 'à', 'á', 'â', 'ä', 'å', 'æ', 'À', 'Á', 'Â', 'Ã', 'Æ', 'e',
                                                     'ê', 'é', 'ë', 'è', 'E', 'Ê', 'Ë', 'É', 'È', 'i', 'ï', 'í', 'î',
                                                     'ì', 'I', 'Ï', 'Í', 'Ì', 'Î', 'o', 'ô', 'ö', 'ò', 'õ', 'ó', 'ø',
                                                     'œ', 'O', 'Ó', 'Ô', 'Õ', 'Ö', 'Ò', 'Ø', 'Œ', 'u', 'ú', 'ü', 'û',
                                                     'ù', 'U', 'Ù', 'Ú', 'Ü', 'Û', 'y', 'ÿ', 'ý', 'Y', 'Ÿ', 'Ý' };

            enum GrammarForm
            {
                Adjective,
                Adverb,
                Noun,
                Preposition,
                Pronoun,
                Verb
            }
        }
        namespace Adjective
        {
            enum Gender
            {
                Masculine,
                Feminine,
                MasculineFeminine
            }

            enum Number
            {
                Singular,
                Plural
            }

            enum Location
            {
                Before,
                After,
                Both
            }
        }
        namespace Adverb
        {
            
        }
        namespace Noun
        {
            enum Gender
            {
                Masculine,
                Feminine,
                MasculineFeminine
            }

            enum Number
            {
                Singular,
                Plural
            }
        }
        namespace Preposition
        {
            // Stuff
        }
        namespace Pronoun
        {
            enum PronounType
            {
                Subject,
                Reflexive,
                DirectObject,
                IndirectObject,
                Disjunctive
            }

            enum PronounReplaces
            {
                DefiniteDirectObject,
                IndirectObject,
                IndefiniteDirectObject
            }
        }
        namespace Verb
        {
            // Adding stuff later
        }
    }
}
