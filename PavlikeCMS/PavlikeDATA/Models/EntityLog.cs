using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enum = pavlikeLibrary.Enum;

namespace PavlikeDATA.Models
{
    public class EntityLog
    {
        public int Id { get; set; }
        public int ErrorId { get; set; }
        public string Detail { get; set; }
        public string Class { get; set; }
        public string Method { get; set; }
        public string EntityModel { get; set; }
        public Enum.EntityJob Job { get; set; }
        public Enum.EntityResult EntityResult { get; set; }

    }
}
