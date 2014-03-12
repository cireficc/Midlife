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
                Auxiliary,
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
                Feminine,
                Invariable,
                Masculine,
                MasculineFeminine,
                VowelNonAspirate
            }

            enum Number
            {
                Invariable,
                Plural,
                Singular
            }

            enum Location
            {
                After, // normal, regular
                Before, // B.A.G.S
                Invariable // figurative vs literal
            }

            enum Type
            {
                Descriptive, // couleurs, beauté
                Demonstrative, // ce, cette, ces, cet
                Indefinite, // autre(s), certain(e)(s)
                IndefiniteNegative, // ne... aucun(e), ne... pas un(e)
                Interrogative, // quel, quels, quelle, quelles
                ParticiplePresent, // interessant, glissant
                ParticiplePast, // étonné, 
                Possessive, // mon, ma, mes, ton, ta, tes
            }

            struct Descriptor
            {
                Gender gender;
                Number number;
                Location location;
                Type type;
            }
        }
        namespace Adverb
        {
            enum Something
            {
                // Stuff
            }

            struct Descriptor
            {
                // Stuff
            }
        }

        namespace Auxiliary
        {
            enum Type
            {
                Avoir,
                Etre
            }
        }
        namespace Noun
        {
            enum Gender
            {
                Feminine,
                Invariable,
                Masculine,
                MasculineFeminine,
                VowelNonAspirate
            }

            enum Number
            {
                Invariable,
                Plural,
                Singular
            }

            struct Descriptor
            {
                Gender gender;
                Number number;
            }
        }
        namespace Preposition
        {
            enum Something
            {
                // Stuff
            }
            
            struct Descriptor
            {
                // Stuff
            }
        }

        namespace Pronoun
        {
            enum Type
            {
                Subject,
                Reflexive,
                DirectObject,
                IndirectObject,
                Disjunctive // Emphatic, stressed
            }

            enum Replaces
            {
                DefiniteDirectObject,
                IndirectObject,
                IndefiniteDirectObject
            }

            struct Descriptor
            {
                Type type;
                Replaces replaces;
            }
        }

        namespace Verb
        {
            enum Mood
            {
                Indicative,
                Subjunctive,
                Conditional,
                Imperative,
                Infinitive,
                Participle
            }

            enum Tense
            {
                Present, // Présent
                Past, // Passé composé
                Imperfect, // Imparfait
                Pluperfect, // Plus-que-parfait
                FutureSimple, // Futur simple
                FuturePast, // Futur antérieur
                LiteraryImperfect, // Passé simple
                LiteraryPluperfect, // Passé antérieur
                SecondPast, // Conditionnel passé II
            }

            enum Transitivity
            {
                Intransitive,
                Transitive,
                TransitiveIntransitive,
                TransitiveInseparable,
                TransitiveSeparable
            }

            enum Pronominality
            {
                Idiomatic, // e.g. s'amuser
                Reciprical, // e.g. se raser
                Reflexive // e.g. s'aimer
            }

            struct Descriptor
            {
                Mood mood;
                Tense tense;
                Transitivity transitivity;
                Pronominality pronominality;
                Auxiliary.Type auxiliary;
            }
        }
    }
}
