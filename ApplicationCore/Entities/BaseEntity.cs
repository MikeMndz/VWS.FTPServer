using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{
    public class BaseEntity
    {
        [Display(Name = "Fecha de creación")]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Última modificación")]
        public DateTime? ModifiedOn { get; set; }
        //public string CreatedBy { get; set; }
        //public string ModifiedBy { get; set; }
    }
}
