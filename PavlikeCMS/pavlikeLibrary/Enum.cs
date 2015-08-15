using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pavlikeLibrary
{
    public class Enum
    {
        public enum EntityResult
        {
            Success, Failed
        }
        public enum EntityJob
        {
            Create, Delete, Update, Read
        }
        public enum ToastrType
        {
            Error, Info, Success, Warning
        }
        public enum FileType
        {
            Document,Media
        }
    }
}
