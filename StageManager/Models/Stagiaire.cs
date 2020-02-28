using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace StageManager.Models
{
    public class Stagiaire
    {
        public int ID { get; set; }

        [Display(Name = "Nom du Stage")]
        public string StageName { get; set; }
        public virtual User User { get; set; }
        public virtual List<DateStage> FormationDates { get; set; }
        public virtual List<DateStage> EntrepriseDates { get; set; }
        public virtual List<NoteStagiaire> ListNoteStagiaire { get; set; }
        public string DateStart { get; set; }        
        public string DateEnd { get; set; }
    }
}