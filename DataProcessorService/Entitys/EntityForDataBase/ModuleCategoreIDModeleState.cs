using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessorService.Entitys.EntityForDataBase
{
    public class ModuleCategoreIDModeleState
    {
        [Key]
        public int ModuleCategoreIDModeleStateID { get; set; }
        public string ModuleCategoreID { get; set; }
        public string ModeleState { get; set; }
    }
}
