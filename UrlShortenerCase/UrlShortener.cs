using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortenerCase
{
    public class UrlShortener
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Url { get; set; }
        public DateTime Expires { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
