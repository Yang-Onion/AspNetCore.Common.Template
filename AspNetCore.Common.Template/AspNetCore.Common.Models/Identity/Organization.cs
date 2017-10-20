using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AspNetCore.Common.Models.Identity
{
    public class Organization
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrgId { get; set; }
        public int ParentOrgId { get; set; }
        public int OrgOrder { get; set; }
        public string OrgName { get; set; }
    }
}
