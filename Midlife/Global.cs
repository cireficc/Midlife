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
             * to see if the user formed the conjugation correctly. Needs to be extended to include accented vowels.
             */
            public static readonly char[] vowels = { 'a', 'à', 'á', 'â', 'ä', 'å', 'æ', 'À', 'Á', 'Â', 'Ã', 'Æ', 'e',
                                                     'ê', 'é', 'ë', 'è', 'E', 'Ê', 'Ë', 'É', 'È', 'i', 'ï', 'í', 'î',
                                                     'ì', 'I', 'Ï', 'Í', 'Ì', 'Î', 'o', 'ô', 'ö', 'ò', 'õ', 'ó', 'ø',
                                                     'œ', 'O', 'Ó', 'Ô', 'Õ', 'Ö', 'Ò', 'Ø', 'Œ', 'u', 'ú', 'ü', 'û',
                                                     'ù', 'U', 'Ù', 'Ú', 'Ü', 'Û', 'y', 'ÿ', 'ý', 'Y', 'Ÿ', 'Ý' };
        }
    }
    // Can there be same class names in different namespaces? That's what namespaces are for... right?
    // If so, this is how I'll organize globals - each "type" in their own namespaces, for a specific purpose.
    namespace VerbMaps
    {
        class Global
        {
            public static string cool = "Cool!";
        }
    }
}
