using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MVCTest.Models
{
    [Table("CategoryMaster")]
    public class CategoryMaster
    {
        public CategoryMaster()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }

        [StringLength(100, ErrorMessage = "Not more than 100 character allowed")]
        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; }

        public DateTime WhenEntered { get; set; }
        public DateTime WhenModified { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ProductMaster> ProductMasters { get; set; }
    }
}