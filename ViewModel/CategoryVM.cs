using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MVCTest.Models;


namespace MVCTest.ViewModel
{
    public class CategoryVM
    {
        public int CategoryID { get; set; }

        [StringLength(100, ErrorMessage = "Not more than 100 character allowed")]
        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; }

        public DateTime WhenEntered { get; set; }
        public DateTime WhenModified { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ProductMaster> ProductMasters { get; set; }

        public PagedList.IPagedList<CategoryVM> CategoryList { get; set; }

    }
}