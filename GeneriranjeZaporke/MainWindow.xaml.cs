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

        /// <summary>
        /// Main window initialization
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();            
        }

        #region Password generation
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(passwordLength.Text, out userPasswordLength))
            {
                throw new ArgumentException(passwordLength.Text.ToString());
            }

            int.TryParse(passwordLength.Text, out userPasswordLength);
        }

        private void letterSizeOption_Checked(object sender, RoutedEventArgs e)
        {
            //lambda
            if(letterSizeOption.IsChecked == true) userLetterSizeOption=true;
            if(letterSizeOption.IsChecked == false) userLetterSizeOption=false;
        }

        private void numbersOption_Checked(object sender, RoutedEventArgs e)
        {
            if(numbersOption.IsChecked == true) userNumberOption = true;
            if(numbersOption.IsChecked == false) userNumberOption = false;
        }

        private void symbolsOption_Checked(object sender, RoutedEventArgs e)
        {
            if(symbolsOption.IsChecked == true) userSymbolOption = true;
            if(symbolsOption.IsChecked == false) userSymbolOption=false;
        }

        private void saveToFileOption_Checked(object sender, RoutedEventArgs e)
        {
            if(saveToFileOption.IsChecked == true) userSaveToFileOption = true;
            if(saveToFileOption.IsChecked == false) userSaveToFileOption = false;
        }

        private void openFile_Click(object sender, RoutedEventArgs e)
        {
            fileHandler.OpenFile();
        }

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
            catch(ArgumentException exception)
            {
                MessageBox.Show("Vrijednost mora biti broj, a ne " + exception.ParamName);
            }
        }
    }
}
