﻿using System;
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
using Language;


namespace WORDmaker
{
    using GrammaticalForm = Word.GrammaticalForm;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //store the current word in memory
        //however most of the time this will be empty except for any lists that need populating
        //and during adding to the xml file there we will fill it and run through the XML engine
        Word currentWord = new Word() 
        { 
            forms = new List<GrammaticalForm>(), 
            verbTable = new VerbTable() 
            {
                prepositions = new List<string>()
            } 
        };



        public MainWindow()
        {
            InitializeComponent();

            //databind our lists to the internal lists of the current Word
            formsList.DataContext = currentWord.forms;
            verbprepostionsList.DataContext = currentWord.verbTable.prepositions;

            //set up comboboxes with lists of values defined for these feilds in dictionary.cs
            //located in MidLife project
            auxillaryCombobox.Items.Add("e");//(être)
            auxillaryCombobox.Items.Add("a");//(avoir)            
            auxillaryCombobox.IsEditable = false;//only use predefined texts

            groupCombobox.Items.Add("f");//er
            groupCombobox.Items.Add("s");//ir
            groupCombobox.Items.Add("t");//ir, oir, re
            groupCombobox.Items.Add("e");//être, avoir, etc.
            groupCombobox.IsEditable = false;//only use predefined texts
        }

        
        //occurs when the add grammatical form button is pressed
        private void addgrammerformButton_Click(object sender, RoutedEventArgs e)
        {
            currentWord.forms.Add(new Word.GrammaticalForm()             
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
            currentWord.verbTable.prepositions.Add(verbprepostionsTextBox.Text);
            verbprepostionsTextBox.Clear();
            verbprepostionsTextBox.Focus();
            verbprepostionsList.Items.Refresh();
        }
      

    }
}