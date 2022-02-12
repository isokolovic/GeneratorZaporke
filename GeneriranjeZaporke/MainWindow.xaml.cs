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

namespace GeneriranjeZaporke
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //postaviti regione
        //iskomentirati metode
        //podijeliti kod u klase
        //  -> vratiti polja preko gettera settera
        //  -> konstruktori i pizdarije 

        //pretvoriti sve if/else u lambda expression


        //napraviti konstruktore za metode...
        //

        //napisat NUnit testove -> za sve opcije mala velika slova, pre dugački password i sl.




        private int userPasswordLength;
        private bool userLetterSizeOption = false;
        private bool userNumberOption = false;
        private bool userSymbolOption = false;
        private bool userSaveToFileOption = false;

        public MainWindow()
        {
            InitializeComponent();

            //string a = GeneratePassword(8, false, false, false);//No options FFF
            //string b = GeneratePassword(8, true, false, false);//letters TFF
            //string c = GeneratePassword(8, false, true, false);//numbers FTF
            //string d = GeneratePassword(8, false, false, true);//symbols FFT
            //string e = GeneratePassword(8, true, true, false);//letter and numbers TTF
            //string f = GeneratePassword(8, true, false, true);//letters and symbols TFT
            //string g = GeneratePassword(8, false, true, true);//numbers and symbols  FTT
            string h = GeneratePassword(8, true, true, true);//all options TTT

        }

        private string GeneratePassword(int length, bool letterSizeOpt, bool numberOpt, bool symbolOpt)
        {            
            StringBuilder letters = GetLetters();
            IEnumerable<int> numbers = GetNumbers();
            StringBuilder symbols = GetSymbols();

            if (!letterSizeOpt && !numberOpt && !symbolOpt) //Basic password, without lower/Upper letters, numbers or symbols
            {
                return GenerateBasicPassword(letters, length).ToString();
            }
            else if (letterSizeOpt && !numberOpt && !symbolOpt) //Password with lower/Upper letters 
            {
                return GeneratePassWithLowUp(letters, length).ToString();
            }
            else if(!letterSizeOpt && numberOpt && !symbolOpt) //Password with numbers
            {
                return GeneratePassWithNum(letters, numbers, length).ToString();
            }
            else if(!letterSizeOpt && !numberOpt && symbolOpt) //Password with symbols
            {
                GeneratePassWithSymb(letters, symbols, length).ToString();
            }
            else if(letterSizeOpt && numberOpt && !symbolOpt) //Password with lower/Upper letters and numbers
            {
                GeneratePassWithLowUpNum(letters, numbers, length).ToString();
            }
            else if(letterSizeOpt && !numberOpt && symbolOpt) //Password with lower/Upper letters and symbols
            {
                GeneratePassWithLowUpSymb(letters, symbols, length).ToString();
            }
            else if(!letterSizeOpt && numberOpt && symbolOpt) //Password with numbers and symbols
            {
                GeneratePassWithNumSymb(letters, numbers, symbols, length).ToString();
            }
            else //Password with lower/Upper letters, numbers and symbols
            {
                GeneratePassWithLowUpNumSymb(letters, numbers, symbols, length).ToString();
            }




            //ako je samo simbol, zamijeniti postojeće sa max 50% karaktera

            //ako su obje opcije, zamijeniti sa max. 33%
            //mora biti min. 1


            return "adf";
        }



        //pretvoriti barem jedno veliko u malo slovo 

        //ako je opcija uključena(mora imati barem jedno veliko i malo slovo), potrebna duljina min. 2


        //zamijeniti postojeće karaktere sa max 50% brojeva

        /// <summary>
        /// Returns basic password, without lower/Upper letters, numbers or symbols (only Uppercase letters)
        /// </summary>
        /// <param name="initialPass">Uppercase letters</param>
        /// <param name="length">Password length</param>
        /// <returns></returns>
        private StringBuilder GenerateBasicPassword(StringBuilder initialPass, int length)
        {
            Random random = new Random();

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(initialPass.Length);
                result.Append(initialPass[index]);
            }

            return result;
        }

        /// <summary>
        /// Returns password with lower/Upper letters
        /// </summary>
        /// <param name="letters">String with Uppercase letters A - Z</param>
        /// <param name="length">Password length</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private StringBuilder GeneratePassWithLowUp(StringBuilder letters, int length)
        {
            // At least one lower/Upper letter is needed, so min. password length = 2
            if (length < 2)
            {
                throw new Exception("Mora biti min. 2 duljine -> u trenutku kad klikne na kvačicu; potrebno provjeriti koje druge opcije su uključene da bi se znala opcija ispisati");//Definirati bolju iznimku
            }

            var result = GenerateBasicPassword(letters, length);

            Random random = new Random();

            // At leas one lower and one Upper letter is needed, so max. number of small letters cannot exceed password length - 1
            int numSmallLetters = random.Next(1, length - 1);

            for (int i = 0; i < numSmallLetters; i++)
            {
                int index = random.Next(result.Length);
                var character = result[index];

                if (char.IsUpper(character))
                {
                    result.Replace(result[index], char.ToLower(character), index, 1);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns password with numbers
        /// </summary>
        /// <param name="letters">String with Uppercase letters A - Z</param>
        /// <param name="numbers">List of numbers 0 - 9</param>
        /// <param name="length">PasswordLength</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private StringBuilder GeneratePassWithNum(StringBuilder letters, IEnumerable<int> numbers, int length)
        {
            // At least one letter and one number is needed, so min. password length = 2
            if (length < 2)
            {
                throw new Exception("Mora biti min. 2 duljine -> u trenutku kad klikne na kvačicu; potrebno provjeriti koje druge opcije su uključene da bi se znala opcija ispisati");//Definirati bolju iznimku
            }

            var result = GenerateBasicPassword(letters, length);

            Random random = new Random();

            // At least one letter and one number is needed, so max. count of numbers cannot exceed password length - 1
            int numberCount = random.Next(1, length - 1);

            for(int i = 0; i < numberCount; i++)
            {
                //Select a random character to be replaced by the number
                int charIndex = random.Next(result.Length);
                var character = result[charIndex];

                //Select a random number from list of numbers
                int number = random.Next(numbers.Count());

                if (!char.IsNumber(character))
                {
                    result.Replace(result[charIndex].ToString(), number.ToString(), charIndex, 1);
                }                    
            }

            return result;
        }

        private StringBuilder GeneratePassWithSymb(StringBuilder letters, StringBuilder symbols, int length)
        {
            // At least one letter and one symbol is needed, so min. password length = 2
            if (length < 2)
            {
                throw new Exception("Mora biti min. 2 duljine -> u trenutku kad klikne na kvačicu; potrebno provjeriti koje druge opcije su uključene da bi se znala opcija ispisati");//Definirati bolju iznimku
            }

            var result = GenerateBasicPassword(letters, length);

            Random random = new Random();

            // At least one letter and one symbol is needed, so max. count of symbols cannot exceed password length - 1
            int symbolCount = random.Next(1, length - 1);

            for (int i = 0; i < symbolCount; i++)
            {
                //Select a random character to be replaced by the symbol
                int charIndex = random.Next(result.Length);
                var character = result[charIndex];

                //Select a random symbol from symbols
                int symbolIndex = random.Next(symbols.Length);
                var symbol = symbols[symbolIndex];

                if (char.IsLetter(character))
                {
                    result.Replace(result[charIndex].ToString(), symbol.ToString(), charIndex, 1);
                }
            }

            return result;
        }

        private StringBuilder GeneratePassWithLowUpNum(StringBuilder letters,IEnumerable<int> numbers, int length)
        {
            // At leas one lower letter, one Upper letter and one number is needed, so min. password length = 3
            if (length < 3)
            {
                throw new Exception("Mora biti min. 3 duljine -> u trenutku kad klikne na kvačicu; potrebno provjeriti koje druge opcije su uključene da bi se znala opcija ispisati");//Definirati bolju iznimku
            }

            var result = GenerateBasicPassword(letters, length);

            Random random = new Random();

            // At least one lower letter, one Upper letter and one number is needed, so max. count of numbers cannot exceed password length - 2
            int numberCount = random.Next(1, length - 2);

            // Max. number of small letters depends on the numbers count
            int numSmallLetters = random.Next(1, length - numberCount);

            List<int> indexCache = Enumerable.Range(0, result.Length).ToList<int>(); //Index for available (not replaced) characters

            for (int i = 0; i < numberCount; i++)
            {
                //Select a random character to be replaced by the number
                int charCacheIndex = random.Next(indexCache.Count());
                int charCacheIndexValue = indexCache.ElementAt(charCacheIndex);

                var character = result[charCacheIndexValue];

                indexCache.RemoveAt(charCacheIndex); 

                //Select a random number from list of numbers
                int number = random.Next(numbers.Count()); 

                if (!char.IsNumber(character))
                {
                    result.Replace(result[charCacheIndexValue].ToString(), number.ToString(), charCacheIndexValue, 1);
                }
            }

            for(int i = 0;i < numSmallLetters; i++)
            {
                int charCacheIndex = random.Next(indexCache.Count());
                int charCacheIndexValue = indexCache.ElementAt(charCacheIndex);

                var character = result[charCacheIndexValue];

                indexCache.RemoveAt(charCacheIndex);

                if(char.IsUpper(character) && char.IsLetter(character))
                {
                    result.Replace(result[charCacheIndexValue], char.ToLower(character), charCacheIndexValue, 1); 
                }
            }

            return result;
        }

        private StringBuilder GeneratePassWithLowUpSymb(StringBuilder letters, StringBuilder symbols, int length)
        {
            // At leas one lower letter, one Upper letter and one symbol is needed, so min. password length = 3
            if (length < 3)
            {
                throw new Exception("Mora biti min. 3 duljine -> u trenutku kad klikne na kvačicu; potrebno provjeriti koje druge opcije su uključene da bi se znala opcija ispisati");//Definirati bolju iznimku
            }

            var result = GenerateBasicPassword(letters, length);

            Random random = new Random();

            // At least one lower letter, one Upper letter and one symbol is needed, so max. count of symbols cannot exceed password length - 2
            int symbolCount = random.Next(1, length - 2);

            // Max. number of small letters depends on the numbers count
            int numSmallLetters = random.Next(1, length - symbolCount);

            List<int> indexCache = Enumerable.Range(0, result.Length).ToList<int>(); //Index for available (not replaced) characters

            for (int i = 0; i < symbolCount; i++)
            {
                //Select a random character to be replaced by the number
                int charCacheIndex = random.Next(indexCache.Count());
                int charCacheIndexValue = indexCache.ElementAt(charCacheIndex);

                var character = result[charCacheIndexValue];

                indexCache.RemoveAt(charCacheIndex);

                //Select a random symbol from symbols
                int symbolIndex = random.Next(symbols.Length);
                var symbol = symbols[symbolIndex];

                if (char.IsLetter(character))
                {
                    result.Replace(result[charCacheIndexValue].ToString(), symbol.ToString(), charCacheIndexValue, 1);
                }
            }

            for (int i = 0; i < numSmallLetters; i++)
            {
                int charCacheIndex = random.Next(indexCache.Count());
                int charCacheIndexValue = indexCache.ElementAt(charCacheIndex);

                var character = result[charCacheIndexValue];

                indexCache.RemoveAt(charCacheIndex);

                if (char.IsUpper(character) && char.IsLetter(character))
                {
                    result.Replace(result[charCacheIndexValue], char.ToLower(character), charCacheIndexValue, 1);
                }
            }

            return result;
        }

        private StringBuilder GeneratePassWithNumSymb(StringBuilder letters, IEnumerable<int> numbers, StringBuilder symbols, int length)
        {
            // At leas one Upper letter, one number and one symbol is needed, so min. password length = 3
            if (length < 3)
            {
                throw new Exception("Mora biti min. 3 duljine -> u trenutku kad klikne na kvačicu; potrebno provjeriti koje druge opcije su uključene da bi se znala opcija ispisati");//Definirati bolju iznimku
            }

            var result = GenerateBasicPassword(letters, length);

            Random random = new Random();

            // At least one Upper letter and one symbol and one number is needed, so max. count of symbols cannot exceed password length - 2
            int numberCount = random.Next(1, length - 2);

            // Max. number of symbols depends on the numbers count
            int symbolCount = random.Next(1, length - numberCount);

            List<int> indexCache = Enumerable.Range(0, result.Length).ToList<int>(); //Index for available (not replaced) characters

            for (int i = 0; i < numberCount; i++)
            {
                //Select a random character to be replaced by the number
                int charCacheIndex = random.Next(indexCache.Count());
                int charCacheIndexValue = indexCache.ElementAt(charCacheIndex);

                var character = result[charCacheIndexValue];

                indexCache.RemoveAt(charCacheIndex);

                //Select a random number from list of numbers
                int number = random.Next(numbers.Count());

                if (!char.IsNumber(character))
                {
                    result.Replace(result[charCacheIndexValue].ToString(), number.ToString(), charCacheIndexValue, 1);
                }
            }

            for (int i = 0; i < symbolCount; i++)
            {
                //Select a random character to be replaced by the number
                int charCacheIndex = random.Next(indexCache.Count());
                int charCacheIndexValue = indexCache.ElementAt(charCacheIndex);

                var character = result[charCacheIndexValue];

                indexCache.RemoveAt(charCacheIndex);

                //Select a random symbol from symbols
                int symbolIndex = random.Next(symbols.Length);
                var symbol = symbols[symbolIndex];

                if (char.IsLetter(character))
                {
                    result.Replace(result[charCacheIndexValue].ToString(), symbol.ToString(), charCacheIndexValue, 1);
                }
            }

            return result;
        }

        private StringBuilder GeneratePassWithLowUpNumSymb(StringBuilder letters, IEnumerable<int> numbers, StringBuilder symbols, int length)
        {
            // At leas one lower letter, one Upper letter, one number and one symbol is needed, so min. password length = 4
            if (length < 4)
            {
                throw new Exception("Mora biti min. 3 duljine -> u trenutku kad klikne na kvačicu; potrebno provjeriti koje druge opcije su uključene da bi se znala opcija ispisati");//Definirati bolju iznimku
            }

            var result = GenerateBasicPassword(letters, length);

            Random random = new Random();

            // At least one Upper letter and one symbol and one number is needed, so max. count of symbols cannot exceed password length - 3
            int numberCount = random.Next(1, length - 3);

            // Max. number of symbols depends on the numbers count
            int symbolCount = random.Next(1, length - numberCount);

            // Max. number of small letters depends symbol (and number) count
            int numSmallLetters = random.Next(1, length - (numberCount + symbolCount));

            List<int> indexCache = Enumerable.Range(0, result.Length).ToList<int>(); //Index for available (not replaced) characters

            for (int i = 0; i < numberCount; i++)
            {
                //Select a random character to be replaced by the number
                int charCacheIndex = random.Next(indexCache.Count());
                int charCacheIndexValue = indexCache.ElementAt(charCacheIndex);

                var character = result[charCacheIndexValue];

                indexCache.RemoveAt(charCacheIndex);

                //Select a random number from list of numbers
                int number = random.Next(numbers.Count());

                if (!char.IsNumber(character))
                {
                    result.Replace(result[charCacheIndexValue].ToString(), number.ToString(), charCacheIndexValue, 1);
                }
            }

            for (int i = 0; i < symbolCount; i++)
            {
                //Select a random character to be replaced by the number
                int charCacheIndex = random.Next(indexCache.Count());
                int charCacheIndexValue = indexCache.ElementAt(charCacheIndex);

                var character = result[charCacheIndexValue];

                indexCache.RemoveAt(charCacheIndex);

                //Select a random symbol from symbols
                int symbolIndex = random.Next(symbols.Length);
                var symbol = symbols[symbolIndex];

                if (char.IsLetter(character))
                {
                    result.Replace(result[charCacheIndexValue].ToString(), symbol.ToString(), charCacheIndexValue, 1);
                }
            }

            for(int i = 0; i < numSmallLetters; i++)
            {
                //Select a random character to be replaced by the number
                int charCacheIndex = random.Next(indexCache.Count());
                int charCacheIndexValue = indexCache.ElementAt(charCacheIndex);

                var character = result[charCacheIndexValue];

                indexCache.RemoveAt(charCacheIndex);

                if (char.IsUpper(character) && char.IsLetter(character))
                {
                    result.Replace(result[charCacheIndexValue], char.ToLower(character), charCacheIndexValue, 1);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns Uppercase letters A-Z
        /// </summary>
        /// <returns></returns>
        private StringBuilder GetLetters()
        {
            StringBuilder result = new StringBuilder(); 

            for (char i = 'A'; i <= 'Z'; i++)
            {
                result.Append(i.ToString());
            }

            return result;
        }

        /// <summary>
        /// Returns list of numbers 0-9
        /// </summary>
        /// <returns></returns>
        private IEnumerable<int> GetNumbers()
        {
            List<int> result = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                result.Add(i);
            }

            return result;
        }

        /// <summary>
        /// Returns non-alphaNumeric characters
        /// </summary>
        /// <returns></returns>
        private StringBuilder GetSymbols()
        {
            return new StringBuilder("!@#%*()$?+-=");
        }


        private void SaveToFile()
        {
            // Dodati link Otvori datoteku
            // Ako datoteka nije prazna, klikom na link se otvara datoteka
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            userPasswordLength = int.Parse(passwordLength.Text);

            //exception problem 
            //možda popup window da 
        }

        private void letterSizeOption_Checked(object sender, RoutedEventArgs e)
        {
            if(letterSizeOption.IsChecked == true) userLetterSizeOption=true;   

            //else if is unchecked -> da vrati nazad na false prije generiranja passworda
        }

        private void numbersOption_Checked(object sender, RoutedEventArgs e)
        {
            if (numbersOption.IsChecked == true) userNumberOption = true;
        }

        private void symbolsOption_Checked(object sender, RoutedEventArgs e)
        {
            if (symbolsOption.IsChecked == true) userSymbolOption = true;
        }

        private void saveToFileOption_Checked(object sender, RoutedEventArgs e)
        {
            if (saveToFileOption.IsChecked == true) userNumberOption = true;
        }
    }
}
