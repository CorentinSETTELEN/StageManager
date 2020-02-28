using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StageManager.Models
{
    public class NoteStagiaire
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public bool Valid { get; set; }
        public string Comment { get; set; }
        public string Date { get; set; }
        public int Note { get; set; }
    }
}