// Own namespace - contains grammar-specific variables
using Language.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language
{
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




        #region XML Functions
        //DD 02/15/2014
        //a reader/writer pair to aid serialization in the Word class       

        /// <summary>
        /// reads the xml and sets all fields 
        /// </summary>
        /// <param name="reader">the xml reader to use for reading</param>
        internal void readXML(System.Xml.XmlReader reader)
        {
            group = reader["group"][0];
            auxillary = reader["auxillary"][0];

            //read the prepositions delimited with space character             
            prepositions.Clear();
            prepositions.AddRange(reader["prepositions"].Split(' '));
            //////////////////////////////////////////////////////

            transitive = Convert.ToBoolean(reader["transitive"]);
            pronominal = Convert.ToBoolean(reader["pronominal"]);


            //start the fun
            reader.ReadStartElement();//<Verb>


            reader.ReadStartElement("Indicative");//we don't need the names but it will make the code more readable
            
            //reset
            indicativePresent = new IndicativePresent();
            indicativeSimplePast = new IndicativeSimplePast();
            indicativePresentPerfect = new IndicativePresentPerfect();
            indicativePastPerfect = new IndicativePastPerfect();
            indicativeImperfect = new IndicativeImperfect();
            indicativePluperfect = new IndicativePluperfect();
            indicativeFuture = new IndicativeFuture();
            indicativePastFuture = new IndicativePastFuture();

            //set data
            indicativePresent.readXML(reader);
            indicativeSimplePast.readXML(reader);
            indicativePresentPerfect.readXML(reader);
            indicativePastPerfect.readXML(reader);
            indicativeImperfect.readXML(reader);
            indicativePluperfect.readXML(reader);
            indicativeFuture.readXML(reader);
            indicativePastFuture.readXML(reader);

            reader.ReadEndElement();


            reader.ReadStartElement("Subjunctive");///////////////////////////////////////

            subjunctivePresent = new SubjunctivePresent();
            subjunctivePast = new SubjunctivePast();
            subjunctiveImperfect = new SubjunctiveImperfect();
            subjunctivePluperfect = new SubjunctivePluperfect();
            
            subjunctivePresent.readXML(reader);
            subjunctivePast.readXML(reader);
            subjunctiveImperfect.readXML(reader);
            subjunctivePluperfect.readXML(reader);           

            reader.ReadEndElement();

            
            reader.ReadStartElement("Conditional");///////////////////////////////////////

            conditionalPresent = new  ConditionalPresent();
            conditionalFirstPast = new ConditionalFirstPast();
            conditionalSecondPast = new ConditionalSecondPast();

            conditionalPresent.readXML(reader);
            conditionalFirstPast.readXML(reader);
            conditionalSecondPast.readXML(reader);            

            reader.ReadEndElement();

            
            reader.ReadStartElement("Imperative");////////////////////////////////////////

            imperativePresent = new ImperativePresent();
            imperativePast = new ImperativePast();

            imperativePresent.readXML(reader);
            imperativePast.readXML(reader);

            reader.ReadEndElement();

            

            //we will not use the safe method for these empty elements


            //infinitive //////////////////////////
            infinitive.present = reader["present"];
            infinitive.past = reader["past"];
            reader.ReadStartElement("Infinitive");////////////////////////////////////////


            //participle///////////////////////////
            participle.present = reader["present"];
            participle.past = reader["past"];
            reader.ReadStartElement("Participle");////////////////////////////////////////


            reader.ReadEndElement();//</verb>
        }

        /// <summary>
        /// writes this class to xml
        /// </summary>
        /// <param name="writer">the xml writer to use for writing</param>
        internal void writeXML(System.Xml.XmlWriter writer)
        {
            
            //write attributes

            writer.WriteAttributeString("group", new string(group,1));
            writer.WriteAttributeString("auxillary", new string(auxillary, 1));           
            
            //write the prepositions delimited with space character           
            string pre = "";
            foreach(string s in prepositions)
            {
                pre +=  s + " ";
            }
            pre.TrimEnd(' ');//get rid of the trailing ' ' char
            writer.WriteAttributeString("prepositions", pre);
            /////////////////////////////////////////////////////////
                        
            writer.WriteAttributeString("transitive",Convert.ToString(transitive));
            writer.WriteAttributeString("pronominal",Convert.ToString(pronominal));
            

            //element contents

            writer.WriteStartElement("Indicative");////////////////////////////////////
            writer.WriteStartElement("Present");
            indicativePresent.writeXML(writer);
            writer.WriteEndElement();
            
            writer.WriteStartElement("SimplePast");
            indicativeSimplePast.writeXML(writer);
            writer.WriteEndElement();
            
            writer.WriteStartElement("PresentPerfect");
            indicativePresentPerfect.writeXML(writer);
            writer.WriteEndElement();
            
            writer.WriteStartElement("PastPerfect");
            indicativePastPerfect.writeXML(writer);
            writer.WriteEndElement();
            
            writer.WriteStartElement("Imperfect");
            indicativeImperfect.writeXML(writer);
            writer.WriteEndElement();
            
            writer.WriteStartElement("Pluperfect");
            indicativePluperfect.writeXML(writer);
            writer.WriteEndElement();
            
            writer.WriteStartElement("Future");
            indicativeFuture.writeXML(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("PastFuture");
            indicativePastFuture.writeXML(writer);
            writer.WriteEndElement();
            
            writer.WriteEndElement();


            writer.WriteStartElement("Subjunctive");////////////////////////////////////

            writer.WriteStartElement("Present");
            subjunctivePresent.writeXML(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("Past");
            subjunctivePast.writeXML(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("Imperfect");
            subjunctiveImperfect.writeXML(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("Pluperfect");
            subjunctivePluperfect.writeXML(writer);
            writer.WriteEndElement();

            writer.WriteEndElement();


            writer.WriteStartElement("Conditional");////////////////////////////////////

            writer.WriteStartElement("Present");
            conditionalPresent.writeXML(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("FirstPast");
            conditionalFirstPast.writeXML(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("SecondPast");
            conditionalSecondPast.writeXML(writer);
            writer.WriteEndElement();

            writer.WriteEndElement();
            

            writer.WriteStartElement("Imperative");////////////////////////////////////

            writer.WriteStartElement("Present");
            imperativePresent.writeXML(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("Past");
            imperativePast.writeXML(writer);
            writer.WriteEndElement();

            writer.WriteEndElement();


            writer.WriteStartElement("Infinitive");////////////////////////////////////
            writer.WriteAttributeString("present", infinitive.present);
            writer.WriteAttributeString("past", infinitive.past);
            writer.WriteEndElement();


            writer.WriteStartElement("Participle");////////////////////////////////////
            writer.WriteAttributeString("present", participle.present);
            writer.WriteAttributeString("past", participle.past);
            writer.WriteEndElement();           

        }
        #endregion

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

        #region XML Functions
        //DD 02/15/2014
        //a reader/writer pair to aid serialization in the Word class       

        /// <summary>
        /// reads the xml and sets all fields 
        /// </summary>
        /// <param name="reader">the xml reader to use for reading</param>
        internal void readXML(System.Xml.XmlReader reader)
        {
            fps = reader["fps"];
            sps = reader["sps"];
            tps = reader["tps"];
            fpp = reader["fpp"];
            spp = reader["spp"];
            tpp = reader["tpp"];

            //check just incase
            if (reader.IsEmptyElement) reader.ReadStartElement();
            else
            {
                reader.ReadStartElement();
                reader.ReadEndElement();
            }
        }

        /// <summary>
        /// writes this class to xml
        /// </summary>
        /// <param name="writer">the xml writer to use for writing</param>
        internal void writeXML(System.Xml.XmlWriter writer)
        {

            writer.WriteAttributeString("fps", fps);
            writer.WriteAttributeString("sps", sps);
            writer.WriteAttributeString("tps", tps);
            writer.WriteAttributeString("fpp", fpp);
            writer.WriteAttributeString("spp", spp);
            writer.WriteAttributeString("tpp", tpp);

        }
        #endregion

        
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

        #region XML Functions
        //DD 02/15/2014
        //a reader/writer pair to aid serialization in the Word class       

        /// <summary>
        /// reads the xml and sets all fields 
        /// </summary>
        /// <param name="reader">the xml reader to use for reading</param>
        internal void readXML(System.Xml.XmlReader reader)
        {
            fps = reader["fps"];
            sps = reader["sps"];
            tps = reader["tps"];
            fpp = reader["fpp"];
            spp = reader["spp"];
            tpp = reader["tpp"];

            //check just incase
            if (reader.IsEmptyElement) reader.ReadStartElement();
            else
            {
                reader.ReadStartElement();
                reader.ReadEndElement();
            }
        }

        /// <summary>
        /// writes this class to xml
        /// </summary>
        /// <param name="writer">the xml writer to use for writing</param>
        internal void writeXML(System.Xml.XmlWriter writer)
        {

            writer.WriteAttributeString("fps", fps);
            writer.WriteAttributeString("sps", sps);
            writer.WriteAttributeString("tps", tps);
            writer.WriteAttributeString("fpp", fpp);
            writer.WriteAttributeString("spp", spp);
            writer.WriteAttributeString("tpp", tpp);

        }
        #endregion
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

        #region XML Functions
        //DD 02/15/2014
        //a reader/writer pair to aid serialization in the Word class       

        /// <summary>
        /// reads the xml and sets all fields 
        /// </summary>
        /// <param name="reader">the xml reader to use for reading</param>
        internal void readXML(System.Xml.XmlReader reader)
        {
            fps = reader["fps"];
            sps = reader["sps"];
            tps = reader["tps"];
            fpp = reader["fpp"];
            spp = reader["spp"];
            tpp = reader["tpp"];

            //check just incase
            if (reader.IsEmptyElement) reader.ReadStartElement();
            else
            {
                reader.ReadStartElement();
                reader.ReadEndElement();
            }
        }

        /// <summary>
        /// writes this class to xml
        /// </summary>
        /// <param name="writer">the xml writer to use for writing</param>
        internal void writeXML(System.Xml.XmlWriter writer)
        {

            writer.WriteAttributeString("fps", fps);
            writer.WriteAttributeString("sps", sps);
            writer.WriteAttributeString("tps", tps);
            writer.WriteAttributeString("fpp", fpp);
            writer.WriteAttributeString("spp", spp);
            writer.WriteAttributeString("tpp", tpp);

        }
        #endregion
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

        #region XML Functions
        //DD 02/15/2014
        //a reader/writer pair to aid serialization in the Word class       

        /// <summary>
        /// reads the xml and sets all fields 
        /// </summary>
        /// <param name="reader">the xml reader to use for reading</param>
        internal void readXML(System.Xml.XmlReader reader)
        {           
            sps = reader["sps"];          
            fpp = reader["fpp"];
            spp = reader["spp"];          

            //check just incase
            if (reader.IsEmptyElement) reader.ReadStartElement();
            else
            {
                reader.ReadStartElement();
                reader.ReadEndElement();
            }
        }

        /// <summary>
        /// writes this class to xml
        /// </summary>
        /// <param name="writer">the xml writer to use for writing</param>
        internal void writeXML(System.Xml.XmlWriter writer)
        {           
            writer.WriteAttributeString("sps", sps);           
            writer.WriteAttributeString("fpp", fpp);
            writer.WriteAttributeString("spp", spp);
        }
        #endregion
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
            if (!String.IsNullOrEmpty(fps) && Global.vowels.Contains(fps[0]))
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
            if (!String.IsNullOrEmpty(fps) && Global.vowels.Contains(fps[0]))
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
            if (!String.IsNullOrEmpty(fps) && Global.vowels.Contains(fps[0]))
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
            if (!String.IsNullOrEmpty(fps) && Global.vowels.Contains(fps[0]))
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
            if (!String.IsNullOrEmpty(fps) && Global.vowels.Contains(fps[0]))
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
            if (!String.IsNullOrEmpty(fps) && Global.vowels.Contains(fps[0]))
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
            if (!String.IsNullOrEmpty(fps) && Global.vowels.Contains(fps[0]))
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
            if (!String.IsNullOrEmpty(fps) && Global.vowels.Contains(fps[0]))
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
            if (!String.IsNullOrEmpty(fps) && Global.vowels.Contains(fps[0]))
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
            if (!String.IsNullOrEmpty(fps) && Global.vowels.Contains(fps[0]))
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
            if (!String.IsNullOrEmpty(fps) && Global.vowels.Contains(fps[0]))
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
            if (!String.IsNullOrEmpty(fps) && Global.vowels.Contains(fps[0]))
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
            if (!String.IsNullOrEmpty(fps) && Global.vowels.Contains(fps[0]))
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
            if (!String.IsNullOrEmpty(fps) && Global.vowels.Contains(fps[0]))
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
            if (!String.IsNullOrEmpty(fps) && Global.vowels.Contains(fps[0]))
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
