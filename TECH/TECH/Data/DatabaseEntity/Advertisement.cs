using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TECH.SharedKernel;

namespace TECH.Data.DatabaseEntity
{
    [Table("advertisement")]
    public class Advertisement: DomainEntity<int>
    {
        [Column(TypeName = "varchar(500)")]
        public string? image { get; set; }       
    }
}
