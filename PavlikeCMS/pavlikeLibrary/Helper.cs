using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;


namespace pavlikeLibrary
{
    public class Helper : Controller
    {
        public static string CharacterCorrection(string text)
        {

            char[] incompatible = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü', ' ' };
            char[] compatible = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U', '-' };
            for (var i = 0; i < incompatible.Length; i++)
            {
                text = text.Replace(incompatible[i], compatible[i]);
            }
            return text;
        }
    }
}
