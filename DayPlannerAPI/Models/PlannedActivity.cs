using System;
using System.ComponentModel.DataAnnotations;

namespace DayPlannerAPI.Models
{
    public class PlannedActivity
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }

        [DisplayFormat(DataFormatString = "yyyy\\MM\\dd\\HH\\mm\\ss", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [DisplayFormat(DataFormatString = "yyyy\\MM\\dd\\HH\\mm\\ss", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        // Foreign key
        public int UserId { get; set; }
    }
}
