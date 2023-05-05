using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.Helpers
{
    internal class CopyImage
    {
        public static void Copy(string start, string des, string filename)
        {
            Debug.WriteLine(start);
            Debug.WriteLine(des);
            Debug.WriteLine(filename);
            string _finalPath;
            if (System.IO.Directory.Exists(des))
            {
                _finalPath = System.IO.Path.Combine(des, filename);
                System.IO.File.Copy(start, _finalPath, true);
            }
        }
    }
}
