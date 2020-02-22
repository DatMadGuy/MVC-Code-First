using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MVCTest.Models;


namespace MVCTest.ViewModel
{
    public class ProductVM
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Product Category is required")]
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }
        [StringLength(100, ErrorMessage = "Not more than 100 character allowed")]
        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; }
        public DateTime WhenEntered { get; set; }
        public DateTime WhenModified { get; set; }
        public bool IsActive { get; set; }

        public virtual CategoryMaster CategoriesMaster { get; set; }

        public PagedList.IPagedList<ProductVM> ProductList { get; set; }
        public List<CategoryMaster> DDLCategoryList { get; set; }
    }
}