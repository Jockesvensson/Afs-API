using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appTechV1.Models
{
    public class JobItem
    {
        public int Id { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string WorkTitle { get; set; }
        public string WorkingHours { get; set; }
        public string WorkingDuration { get; set; }
        public DateTime PublicationDate { get; set; }

    }
}
