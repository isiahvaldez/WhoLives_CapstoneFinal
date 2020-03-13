using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WhoLives.Models
{
    public class Measures
    {

        [Key]
        public int MeasureID { get; set; }

        [Display(Name="Measure Name")]
        public string MeasureName { get; set; }     
    }
}
