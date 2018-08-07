using GenFu;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Googlekeep.Model
{

    public class Note
    {
        [Key]
        public int NoteID { get; set; }
        public string title { get; set; }
        public string plain_text { get; set; }
        public bool Ispinnned { get; set; }
        public List<checklist> ListofChecks { get; set; }
        public List<label> ListofLabels { get; set; }
    }
}   
