using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Text;

namespace GoodsService.Core.Models
{
    public class Category:BaseNamedEntity<Guid>
    {
       [DataType(DataType.ImageUrl)]
        [StringLength(64)]
        public string Description { get; set; }
    }
}
