using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavlikeDATA.Models
{
    public class Seo
    {
        public int Id { get; set; }
        public int Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public bool Active { get; set; }
    }
}
