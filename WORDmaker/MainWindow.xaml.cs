using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Language;
using System.Xml.Serialization;

namespace WORDmaker
{
    // CC 2/7/14 updated namespace from Word.GrammaticalForm
    using GrammaticalForm = Language.GrammaticalForm;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //store the current word in memory
        //however most of the time this will be empty except for any lists that need populating
        //and during adding to the xml file there we will fill it and run through the XML engine
        Word currentWord = new Word();


        ExtendedDictionary dict = new ExtendedDictionary();


        public MainWindow()
        {
            InitializeComponent();

            //databind our lists to the internal lists of the current Word
            formsList.DataContext = currentWord.forms;
            verbprepositionsList.DataContext = currentWord.verbTable.prepositions;
            dictionaryListBox.DataContext = dict.dictionary.Keys;


            //set up comboboxes with lists of values defined for these feilds in dictionary.cs
            //located in MidLife project
            auxillaryCombobox.Items.Add("e");//(être)
            auxillaryCombobox.Items.Add("a");//(avoir)            
            auxillaryCombobox.IsEditable = false;//only use predefined texts
            auxillaryCombobox.SelectedIndex = 0;

            groupCombobox.Items.Add("f");//er
            groupCombobox.Items.Add("s");//ir
            groupCombobox.Items.Add("t");//ir, oir, re
            groupCombobox.Items.Add("e");//être, avoir, etc.
            groupCombobox.IsEditable = false;//only use predefined texts
            groupCombobox.SelectedIndex = 0;
        }

        
        //occurs when the add grammatical form button is pressed
        private void addgrammerformButton_Click(object sender, RoutedEventArgs e)
        {
            // CC 2/7/14 updated namespace from Word.GrammaticalForm
            currentWord.forms.Add(new Language.GrammaticalForm()             
            { //grab text from fields
                definition = grammerdefinitionTextBox.Text ,
                form = grammerformTextBox.Text
            });

            //clear text and focus on grammerformTextBox after add
            grammerdefinitionTextBox.Clear();
            grammerformTextBox.Clear();
            grammerformTextBox.Focus();


            //make list box refresh itself otherwise it takes awhile 
            formsList.Items.Refresh();

        }

        private void addverbprepostionsButton_Click(object sender, RoutedEventArgs e)
        {
            //same pattern as above add , clear, focus
            currentWord.verbTable.prepositions.Add(verbprepositionsTextBox.Text);
            verbprepositionsTextBox.Clear();
            verbprepositionsTextBox.Focus();
            verbprepositionsList.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            #region test
            /*testing code
             * 
            //the main button that writes to XML
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Title = "TEST: save one word to XML will rewrite";
            sfd.Filter = "XML|*.xml";

            if (sfd.ShowDialog() ?? false)//?? will turn null into false HERE
            {

                XmlSerializer xml = new XmlSerializer(typeof(Word));

                //construct word
                //maybe this weekend 



                using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write))
                {//create the xml file
                    xml.Serialize(fs, currentWord);
                }
            }

            */
            #endregion


            //construct the word
            currentWord.word = wordTextBox.Text;

            //check for enabled conjugations

            if (adjectiveEnabledCheckBox.IsChecked ?? false)
            { 
            
            }

            if (nounEnabledCheckBox.IsChecked ?? false)
            { 
                
            }

            if (verbEnabledCheckBox.IsChecked ?? false)
            { 
                
            }

            //add to dict

            dict.dictionary.Add(currentWord.word, currentWord);
            dictionaryListBox.Items.Refresh();
       
            
            //reset fields
            currentWord = new Word();
            clearAllTextBoxes();    


        }


        void clearAllTextBoxes()
        {
            //clear all text

            wordTextBox.Clear();
            grammerformTextBox.Clear();
            grammerdefinitionTextBox.Clear();
            noun_fplTextBox.Clear();
            noun_fsTextBox.Clear();
            noun_genderTextBox.Clear();
            noun_mplTextBox.Clear();
            noun_msTextBox.Clear();
            adj_fplTextBox.Clear();
            adj_fsTextBox.Clear();
            adj_locationTextBox.Clear();
            adj_mplTextBox.Clear();
            adj_msTextBox.Clear();
            adj_naTextBox.Clear();
            verbprepositionsTextBox.Clear();
            inpr_fppTextBox.Clear();
            inpr_fpsTextBox.Clear();
            inpr_sppTextBox.Clear();
            inpr_spsTextBox.Clear();
            inpr_tppTextBox.Clear();
            inpr_tpsTextBox.Clear();
            inprp_fppTextBox.Clear();
            inprp_fpsTextBox.Clear();
            inprp_sppTextBox.Clear();
            inprp_spsTextBox.Clear();
            inprp_tppTextBox.Clear();
            inprp_tpsTextBox.Clear();
            inpp_fppTextBox.Clear();
            inpp_fppTextBox.Clear();
            inpp_fpsTextBox.Clear();
            inpp_sppTextBox.Clear();
            inpp_spsTextBox.Clear();
            inpp_tppTextBox.Clear();
            inpp_tpsTextBox.Clear();
            im_fppTextBox.Clear();            
            im_fpsTextBox.Clear();
            im_sppTextBox.Clear();
            im_spsTextBox.Clear();
            im_tppTextBox.Clear();
            im_tpsTextBox.Clear();
            inpl_fppTextBox.Clear();
            inpl_fpsTextBox.Clear();
            inpl_sppTextBox.Clear();
            inpl_spsTextBox.Clear();
            inpl_tppTextBox.Clear();
            inpl_tpsTextBox.Clear();
            inf_fppTextBox.Clear();
            inf_fpsTextBox.Clear();
            inf_sppTextBox.Clear();
            inf_spsTextBox.Clear();
            inf_tppTextBox.Clear();
            inf_tpsTextBox.Clear();
            infi_pastTextBox.Clear();
            infi_presentTextBox.Clear();
            inpf_fppTextBox.Clear();
            inpf_fpsTextBox.Clear();
            inpf_sppTextBox.Clear();
            inpf_spsTextBox.Clear();
            inpf_tppTextBox.Clear();
            inpf_tpsTextBox.Clear();
            spr_fppTextBox.Clear();
            spr_fpsTextBox.Clear();
            spr_sppTextBox.Clear();
            spr_spsTextBox.Clear();
            spr_tppTextBox.Clear();
            spr_tpsTextBox.Clear();
            part_pastTextBox.Clear();
            part_presentTextBox.Clear();
            spa_fppTextBox.Clear();
            spa_fpsTextBox.Clear();
            spa_sppTextBox.Clear();
            spa_spsTextBox.Clear();
            spa_tppTextBox.Clear();
            spa_tpsTextBox.Clear();
            si_fppTextBox.Clear();
            si_fpsTextBox.Clear();
            si_sppTextBox.Clear();
            si_spsTextBox.Clear();
            si_tppTextBox.Clear();
            si_tpsTextBox.Clear();
            spl_fppTextBox.Clear();
            spl_fpsTextBox.Clear();
            spl_sppTextBox.Clear();
            spl_spsTextBox.Clear();
            spl_tppTextBox.Clear();
            spl_tpsTextBox.Clear();
            cp_fppTextBox.Clear();
            cp_fpsTextBox.Clear();
            cp_sppTextBox.Clear();
            cp_spsTextBox.Clear();
            cp_tppTextBox.Clear();
            cp_tpsTextBox.Clear();
            cfp_fppTextBox.Clear();
            cfp_fpsTextBox.Clear();
            cfp_sppTextBox.Clear();
            cfp_spsTextBox.Clear();
            cfp_tppTextBox.Clear();
            cfp_tpsTextBox.Clear();
            csp_fppTextBox.Clear();
            csp_fpsTextBox.Clear();
            csp_sppTextBox.Clear();
            csp_spsTextBox.Clear();
            csp_tppTextBox.Clear();
            csp_tpsTextBox.Clear();
            ipr_fppTextBox.Clear();
            ipr_sppTextBox.Clear();
            ipr_spsTextBox.Clear();
            insp_fppTextBox.Clear();
            insp_fpsTextBox.Clear();
            insp_sppTextBox.Clear();
            insp_spsTextBox.Clear();
            insp_tppTextBox.Clear();
            insp_tpsTextBox.Clear();
        }
      

    }
}
