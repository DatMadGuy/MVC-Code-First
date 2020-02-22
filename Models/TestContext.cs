using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace MVCTest.Models
{
    public class TestContext : DbContext
    {
        public TestContext()
            : base("name=MVCTestDBConn")
        {
            //Calling Custom DB Initializer
            Database.SetInitializer<TestContext>(new MVCTestDBInitializer());
        }

        public DbSet<CategoryMaster> CategoriesMaster { get; set; }
        public DbSet<ProductMaster> ProductMasters { get; set; }

        //Creating Custom DB Initializer
        public class MVCTestDBInitializer : CreateDatabaseIfNotExists<TestContext>
        {
            protected override void Seed(TestContext context)
            {
                base.Seed(context);
                IList<CategoryMaster> category = new List<CategoryMaster>();
                //Adding Row
                category.Add(new CategoryMaster() { CategoryName = "Men", WhenEntered = System.DateTime.UtcNow, WhenModified = System.DateTime.UtcNow, IsActive = true });
                category.Add(new CategoryMaster() { CategoryName = "Women", WhenEntered = System.DateTime.UtcNow, WhenModified = System.DateTime.UtcNow, IsActive = true });
                category.Add(new CategoryMaster() { CategoryName = "Kid", WhenEntered = System.DateTime.UtcNow, WhenModified = System.DateTime.UtcNow, IsActive = true });
                category.Add(new CategoryMaster() { CategoryName = "Jewellery", WhenEntered = System.DateTime.UtcNow, WhenModified = System.DateTime.UtcNow, IsActive = true });
                foreach (CategoryMaster cm in category)
                {
                    context.CategoriesMaster.Add(cm);
                    context.SaveChanges();
                }

                IList<ProductMaster> product = new List<ProductMaster>();
                //Adding Row
                for (int i = 0; i < 100; i++)
                {
                    product.Add(new ProductMaster()
                    {
                        CategoryID = 1,
                        ProductName = "Product " + (i + 1),
                        WhenEntered = System.DateTime.UtcNow,
                        WhenModified = System.DateTime.UtcNow,
                        IsActive = true
                    });
                }
                foreach (ProductMaster pm in product)
                {
                    context.ProductMasters.Add(pm);
                    context.SaveChanges();
                }
            }
        }
    }
}