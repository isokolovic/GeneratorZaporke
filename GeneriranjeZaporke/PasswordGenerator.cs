using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneriranjeZaporke
{
    /// <summary>
    /// Class containing methods related to password generation with user-defined customization options
    /// </summary>
    internal class PasswordGenerator
    {
        #region Internal methods

        /// <summary>
        /// Returns basic password, without lower/Upper letters, numbers or symbols (only Uppercase letters)
        /// </summary>
        /// <param name="initialPass">Uppercase letters</param>
        /// <param name="length">Password length</param>
        /// <returns>Generated password</returns>
        /// <exception cref="ArgumentOutOfRangeException">If password length is not large enough</exception>
        internal StringBuilder GenerateBasicPassword(StringBuilder initialPass, int length)
        {
            if (length < 1)
            {
                throw new ArgumentOutOfRangeException("1");
            }

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
        /// <returns>Generated password</returns>
        /// <exception cref="ArgumentOutOfRangeException">If password length is not large enough</exception>
        internal StringBuilder GeneratePassWithLowUp(StringBuilder letters, int length)
        {
            // At least one lower/Upper letter is needed, so min. password length = 2
            if (length < 2)
            {
                throw new ArgumentOutOfRangeException("2");
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
        /// <param name="length">Password length</param>
        /// <returns>Generated password</returns>
        /// <exception cref="ArgumentOutOfRangeException">If password length is not large enough</exception>
        internal StringBuilder GeneratePassWithNum(StringBuilder letters, IEnumerable<int> numbers, int length)
        {
            // At least one letter and one number is needed, so min. password length = 2
            if (length < 2)
            {
                throw new ArgumentOutOfRangeException ("2");
            }

            var result = GenerateBasicPassword(letters, length);

            Random random = new Random();

            // At least one letter and one number is needed, so max. count of numbers cannot exceed password length - 1
            int numberCount = random.Next(1, length - 1);

            for (int i = 0; i < numberCount; i++)
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

        /// <summary>
        /// Returns password with symbols
        /// </summary>
        /// <param name="letters">String with Uppercase letters A - Z</param>
        /// <param name="symbols">Non alpha-numeric characters</param>
        /// <param name="length">Password length</param>
        /// <returns>Generated password</returns>
        /// <exception cref="ArgumentOutOfRangeException">If password length is not large enough</exception>
        internal StringBuilder GeneratePassWithSymb(StringBuilder letters, StringBuilder symbols, int length)
        {
            // At least one letter and one symbol is needed, so min. password length = 2
            if (length < 2)
            {
                throw new ArgumentOutOfRangeException("2");
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

        /// <summary>
        /// Returns password with lower/Upper letters na numbers
        /// </summary>
        /// <param name="letters">String with Uppercase letters A - Z</param>
        /// <param name="numbers">List of numbers 0 - 9</param>
        /// <param name="length">Password length</param>
        /// <returns>Generated password</returns>
        /// <exception cref="ArgumentOutOfRangeException">If password length is not large enough</exception>
        internal StringBuilder GeneratePassWithLowUpNum(StringBuilder letters, IEnumerable<int> numbers, int length)
        {
            // At leas one lower letter, one Upper letter and one number is needed, so min. password length = 3
            if (length < 3)
            {
                throw new ArgumentOutOfRangeException("3");
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

        /// <summary>
        /// Returns password with lower/Upper letters na symbols
        /// </summary>
        /// <param name="letters">String with Uppercase letters A - Z</param>
        /// <param name="symbols">Non alpha-numeric characters</param>
        /// <param name="length">Password length</param>
        /// <returns>Generated password</returns>
        /// <exception cref="ArgumentOutOfRangeException">If password length is not large enough</exception>
        internal StringBuilder GeneratePassWithLowUpSymb(StringBuilder letters, StringBuilder symbols, int length)
        {
            // At leas one lower letter, one Upper letter and one symbol is needed, so min. password length = 3
            if (length < 3)
            {
                throw new ArgumentOutOfRangeException("3");
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

        /// <summary>
        /// Returns password with numbers and symbols
        /// </summary>
        /// <param name="letters">String with Uppercase letters A - Z</param>
        /// <param name="numbers">List of numbers 0 - 9</param>
        /// <param name="symbols">Non alpha-numeric characters</param>
        /// <param name="length">Password length</param>
        /// <returns>Generated password</returns>
        /// <exception cref="ArgumentOutOfRangeException">If password length is not large enough</exception>
        internal StringBuilder GeneratePassWithNumSymb(StringBuilder letters, IEnumerable<int> numbers, StringBuilder symbols, int length)
        {
            // At leas one Upper letter, one number and one symbol is needed, so min. password length = 3
            if (length < 3)
            {
                throw new ArgumentOutOfRangeException("3");
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

        /// <summary>
        /// Returns password with lower/Upper letters, numbers na symbols
        /// </summary>
        /// <param name="letters">String with Uppercase letters A - Z</param>
        /// <param name="numbers">List of numbers 0 - 9</param>
        /// <param name="symbols">Non alpha-numeric characters</param>
        /// <param name="length">Password length</param>
        /// <returns>Generated password</returns>
        /// <exception cref="ArgumentOutOfRangeException">If password length is not large enough</exception>
        internal StringBuilder GeneratePassWithLowUpNumSymb(StringBuilder letters, IEnumerable<int> numbers, StringBuilder symbols, int length)
        {
            // At leas one lower letter, one Upper letter, one number and one symbol is needed, so min. password length = 4
            if (length < 4)
            {
                throw new ArgumentOutOfRangeException("4");
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

            for (int i = 0; i < numSmallLetters; i++)
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

        #endregion
    }
}
