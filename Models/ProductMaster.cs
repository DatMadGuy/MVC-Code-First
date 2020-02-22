using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCTest.Models
{
    [Table("ProductMaster")]
    public class ProductMaster
    {
        public ProductMaster()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        public int CategoryID { get; set; }

        [StringLength(100, ErrorMessage = "Not more than 100 character allowed")]
        public string ProductName { get; set; }

        public DateTime WhenEntered { get; set; }
        public DateTime WhenModified { get; set; }
        public bool IsActive { get; set; }

        //Foreign key
        [ForeignKey("CategoryID")]
        public virtual CategoryMaster CategoriesMaster { get; set; }
    }
}