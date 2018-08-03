using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Googlekeep.Model
{
    public class label
    {
        [Key]
        public int labelID { get; set; }

        //public int NoteID { get; set; }
        //public virtual Note note { get; set; }

        public string labelstring { get; set; }

    }
}
