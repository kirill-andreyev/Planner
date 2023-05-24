using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class EventPL
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Promoter { get; set; }
        public string Description { get; set; }
        public string Speaker { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
    }
}
