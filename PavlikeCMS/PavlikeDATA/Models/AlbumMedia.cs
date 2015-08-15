using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavlikeDATA.Models
{
    public class AlbumMedia
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
        public int MediaId { get; set; }
        public virtual Media Media { get; set; }
    }
}
