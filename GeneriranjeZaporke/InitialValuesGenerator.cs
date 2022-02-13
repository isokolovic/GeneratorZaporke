using System.Collections.Generic;
using System.Text;

namespace GeneriranjeZaporke
{
    /// <summary>
    /// Provides default values for members used in password generation
    /// </summary>
    internal class InitialValuesGenerator 
    {
        #region Public properties

        /// <summary>
        /// Uppercase letters A - Z property
        /// </summary>
        internal StringBuilder Letters
        {
            get => GetLetters();            
        }
        /// <summary>
        /// Numbers 0 - 9 property
        /// </summary>
        internal IEnumerable<int> Numbers
        {
            get => GetNumbers();            
        }
        /// <summary>
        /// Non-alphaNumeric characters property
        /// </summary>
        internal StringBuilder Symbols
        {
            get => GetSymbols();            
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Returns Uppercase letters A-Z
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetLetters()
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

        #endregion
    }
}
