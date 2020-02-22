using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTest.Models;
using MVCTest.ViewModel;
using PagedList;
using System.Data.Entity;

namespace MVCTest.Controllers
{
    public class ProductController : Controller
    {
        TestContext _Context = new TestContext();
        //
        // GET: /Product/

        public ActionResult Index(int? ProductID, int? page)
        {
            ProductVM product = new ProductVM();

            var tempProduct = (from pm in _Context.ProductMasters
                                select pm);

            if (page == null)
            {
                page = 1;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            tempProduct = tempProduct.OrderBy(x => x.ProductID);
            product.ProductList = tempProduct.Select(x => new ProductVM()
            {
                ProductID = x.ProductID,
                ProductName = x.ProductName,
                CategoryID = x.CategoryID,
                CategoryName = x.CategoriesMaster.CategoryName,
                WhenEntered = x.WhenEntered,
                WhenModified = x.WhenModified,
                IsActive = x.IsActive
            }).ToPagedList(pageNumber, pageSize);

            product.DDLCategoryList = _Context.CategoriesMaster.ToList();

            if (ProductID != null && ProductID != 0)
            {
                var cat = _Context.ProductMasters.Where(x => x.ProductID == ProductID).FirstOrDefault();
                product.ProductID = cat.ProductID;
                product.CategoryID = cat.CategoryID;
                product.ProductName = cat.ProductName;
                product.WhenEntered = cat.WhenEntered;
                product.WhenModified = cat.WhenModified;
                product.IsActive = cat.IsActive;
            }
            return View(product);
        }

        public ActionResult Create(ProductVM product)
        {
            if (ModelState.IsValid)
            {
                using (DbContextTransaction dbTran = _Context.Database.BeginTransaction())
                {
                    try
                    {
                        if (product.ProductID == 0)
                        {
                            ProductMaster insert = new ProductMaster();
                            insert.ProductName = product.ProductName;
                            insert.CategoryID = product.CategoryID;
                            insert.WhenEntered = System.DateTime.UtcNow;
                            insert.WhenModified = System.DateTime.UtcNow;
                            insert.IsActive = true;
                            _Context.ProductMasters.Add(insert);
                            _Context.SaveChanges();
                            dbTran.Commit();
                        }
                        else
                        {
                            ProductMaster update = _Context.ProductMasters.Find(product.ProductID);
                            update.ProductName = product.ProductName;
                            update.CategoryID = product.CategoryID;
                            update.WhenModified = System.DateTime.UtcNow;
                            update.IsActive = true;
                            _Context.Entry(update).State = EntityState.Modified;
                            _Context.SaveChanges();
                            dbTran.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        dbTran.Rollback();
                        throw ex;
                    }
                }
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return View("Index", product);
            }
        }

        public ActionResult Delete(int ProductID, int page)
        {
            using (DbContextTransaction dbTran = _Context.Database.BeginTransaction())
            {
                try
                {
                    ProductMaster delete = _Context.ProductMasters.Find(ProductID);
                    _Context.ProductMasters.Remove(delete);
                    _Context.SaveChanges();
                    dbTran.Commit();
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    throw ex;
                }
            }
            return RedirectToAction("Index", "Product", routeValues: new { page = page });
        }
    }
}
