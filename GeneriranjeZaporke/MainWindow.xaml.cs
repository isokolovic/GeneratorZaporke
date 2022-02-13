using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace GeneriranjeZaporke
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private members

        private int userPasswordLength;
        private bool userLetterSizeOption;
        private bool userNumberOption;
        private bool userSymbolOption;
        private bool userSaveToFileOption;

        InitialValuesGenerator valueGenerator = new InitialValuesGenerator();
        PasswordGenerator passwordGenerator = new PasswordGenerator();
        FileHandler fileHandler = new FileHandler();

        #endregion

        #region Main window initialization

        /// <summary>
        /// Main window initialization
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();            
        }

        #endregion

        #region Password generation

        /// <summary>
        /// Method calls password generation process according to selected options 
        /// </summary>
        /// <param name="length"></param>
        /// <param name="letterSizeOpt"></param>
        /// <param name="numberOpt"></param>
        /// <param name="symbolOpt"></param>
        /// <returns></returns>
        private string GeneratePassword(int length, bool letterSizeOpt, bool numberOpt, bool symbolOpt)
        {
            StringBuilder letters = valueGenerator.Letters;
            IEnumerable<int> numbers = valueGenerator.Numbers;
            StringBuilder symbols = valueGenerator.Symbols;

            if (!letterSizeOpt && !numberOpt && !symbolOpt) //Basic password, without lower/Upper letters, numbers or symbols
            {
                return passwordGenerator.GenerateBasicPassword(letters, length).ToString();
            }
            else if (letterSizeOpt && !numberOpt && !symbolOpt) //Password with lower/Upper letters 
            {
                return passwordGenerator.GeneratePassWithLowUp(letters, length).ToString();
            }
            else if (!letterSizeOpt && numberOpt && !symbolOpt) //Password with numbers
            {
                return passwordGenerator.GeneratePassWithNum(letters, numbers, length).ToString();                
            }
            else if (!letterSizeOpt && !numberOpt && symbolOpt) //Password with symbols
            {
                return passwordGenerator.GeneratePassWithSymb(letters, symbols, length).ToString();
            }
            else if (letterSizeOpt && numberOpt && !symbolOpt) //Password with lower/Upper letters and numbers
            {
                return passwordGenerator.GeneratePassWithLowUpNum(letters, numbers, length).ToString();
            }
            else if (letterSizeOpt && !numberOpt && symbolOpt) //Password with lower/Upper letters and symbols
            {
                return passwordGenerator.GeneratePassWithLowUpSymb(letters, symbols, length).ToString();
            }
            else if (!letterSizeOpt && numberOpt && symbolOpt) //Password with numbers and symbols
            {
                return passwordGenerator.GeneratePassWithNumSymb(letters, numbers, symbols, length).ToString();
            }
            else //Password with lower/Upper letters, numbers and symbols
            {
                return passwordGenerator.GeneratePassWithLowUpNumSymb(letters, numbers, symbols, length).ToString();
            }
        }
        #endregion

        #region User action methods

        /// <summary>
        /// Actions related to chages in password length textBlock
        /// </summary>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                userPasswordLength = int.Parse(passwordLength.Text);
            }
            catch (FormatException)
            {
                if(passwordLength.Text.Length != 0)
                {
                    MessageBox.Show("Duljina lozinke mora biti broj!");
                    passwordLength.Text = null;
                }
            }
        }

        /// <summary>
        /// Actions related to chages lower/Upper letters checkbox
        /// </summary>
        private void letterSizeOption_Checked(object sender, RoutedEventArgs e)
        {
            if(letterSizeOption.IsChecked == true) userLetterSizeOption=true;
            if(letterSizeOption.IsChecked == false) userLetterSizeOption=false;
        }

        /// <summary>
        /// Actions related to chages numbers checkbox
        /// </summary>
        private void numbersOption_Checked(object sender, RoutedEventArgs e)
        {
            if(numbersOption.IsChecked == true) userNumberOption = true;
            if(numbersOption.IsChecked == false) userNumberOption = false;
        }

        /// <summary>
        /// Actions related to chages symbols checkbox
        /// </summary>        
        private void symbolsOption_Checked(object sender, RoutedEventArgs e)
        {
            if(symbolsOption.IsChecked == true) userSymbolOption = true;
            if(symbolsOption.IsChecked == false) userSymbolOption=false;
        }

        /// <summary>
        /// Actions related to changes in checkbox providing option to save password in file
        /// </summary>
        private void saveToFileOption_Checked(object sender, RoutedEventArgs e)
        {
            if(saveToFileOption.IsChecked == true) userSaveToFileOption = true;
            if(saveToFileOption.IsChecked == false) userSaveToFileOption = false;
        }

        /// <summary>
        /// Action opening a file containing saved passwords
        /// </summary>
        private void openFile_Click(object sender, RoutedEventArgs e)
        {
            fileHandler.OpenFile();
        }

        /// <summary>
        /// Action calling password generation
        /// </summary>
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string password = GeneratePassword(userPasswordLength, userLetterSizeOption, userNumberOption, userSymbolOption);

                generatedPassword.Text = password;

                if (userSaveToFileOption == true)
                {
                    fileHandler.SaveToFile(password);

                    if (fileHandler.IsFileEmpty())
                    {
                        openFile.Visibility = Visibility.Visible;
                    }
                }
            }
            catch(ArgumentOutOfRangeException exception)
            {
                MessageBox.Show("Prema opcijama, lozinka mora biti najmanje duljine " + exception.ParamName);
            }
        }

        #endregion
    }
}
