using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavlikeDATA.Models
{
    public class Social
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ApiKey { get; set; }
        public string ApiCode { get; set; }

        public bool Active { get; set; }

    }
}
