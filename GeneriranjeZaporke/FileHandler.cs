using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GeneriranjeZaporke
{
    /// <summary>
    /// 
    /// </summary>
    internal class FileHandler
    {

        #region Internal methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        internal void SaveToFile(string password)
        {
            var directory = Directory.GetCurrentDirectory();

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(directory, "Password.txt"), true))
            {
                outputFile.WriteLine(password);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
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
        /// 
        /// </summary>
        internal void OpenFile()
        {
            var directory = Directory.GetCurrentDirectory();

            Process.Start("notepad.exe", Path.Combine(directory, "Password.txt"));
        }
            
        #endregion
    }
}
