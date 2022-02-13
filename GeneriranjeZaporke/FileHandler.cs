using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace GeneriranjeZaporke
{
    /// <summary>
    /// Class containing methods providing functionalities regarding file handling
    /// </summary>
    internal class FileHandler
    {

        #region Internal methods

        /// <summary>
        /// Saves generated password to .txt file
        /// </summary>
        /// <param name="password">Password to be saved to file</param>
        internal void SaveToFile(string password)
        {
            var directory = Directory.GetCurrentDirectory();

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(directory, "Password.txt"), true))
            {
                outputFile.WriteLine(password);
            }
            
        }

        /// <summary>
        /// Checks if file containing saved passwords is empty
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">Thrown if no file is available to check</exception>
        internal bool IsFileEmpty()
        {
            var directory = Directory.GetCurrentDirectory();
            var file = Path.Combine(directory, "Password.txt");

            if (!File.Exists(file))
            {
                throw new FileNotFoundException();
            }
            else
            {
                if(new FileInfo(file).Length != 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Opens file containing generated passwords in notepad
        /// </summary>
        internal void OpenFile()
        {
            var directory = Directory.GetCurrentDirectory();

            Process.Start("notepad.exe", Path.Combine(directory, "Password.txt"));
        }
            
        #endregion
    }
}
