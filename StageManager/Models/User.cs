using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StageManager.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }

    public enum Role
    {
        [Display(Name = "Maitre De Stage")]
        maitreStage,

        [Display(Name = "Stagiaire")]
        stagiaire,
    }
}