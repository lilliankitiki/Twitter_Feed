using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadFiles
{
    public class ErrorHandling
    {
        public static void LogError(string error)
        {
            try
            {
                File.WriteAllText("log.txt", $"Error: {error}");
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", $"Error: {error}");
                throw;
            }
        }
    }
}
