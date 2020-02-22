using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTest.Models;
using PagedList;
using System.Data.Entity;
using MVCTest.ViewModel;

namespace MVCTest.Controllers
{
    public class CategoryController : Controller
    {
        TestContext _Context = new TestContext();
        //
        // GET: /Category/

        public ActionResult Index(int? CategoryID, int? page)
        {
            CategoryVM category = new CategoryVM();

            var tempCategory = (from cm in _Context.CategoriesMaster
                                select cm);

            if (page == null)
            {
                page = 1;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            tempCategory = tempCategory.OrderBy(x => x.CategoryID);
            category.CategoryList = tempCategory.Select(x => new CategoryVM()
            {
                CategoryID = x.CategoryID,
                CategoryName = x.CategoryName,
                WhenEntered = x.WhenEntered,
                WhenModified = x.WhenModified,
                IsActive = x.IsActive
            }).ToPagedList(pageNumber, pageSize);

            if (CategoryID != null && CategoryID != 0)
            {
                var cat = _Context.CategoriesMaster.Where(x => x.CategoryID == CategoryID).FirstOrDefault();
                category.CategoryID = cat.CategoryID;
                category.CategoryName = cat.CategoryName;
                category.WhenEntered = cat.WhenEntered;
                category.WhenModified = cat.WhenModified;
                category.IsActive = cat.IsActive;
            }
            return View(category);
        }

        public ActionResult Create(CategoryVM category)
        {
            if (ModelState.IsValid)
            {
                using (DbContextTransaction dbTran = _Context.Database.BeginTransaction())
                {
                    try
                    {
                        if (category.CategoryID == 0)
                        {

                            CategoryMaster insert = new CategoryMaster();
                            insert.CategoryName = category.CategoryName;
                            insert.WhenEntered = System.DateTime.UtcNow;
                            insert.WhenModified = System.DateTime.UtcNow;
                            insert.IsActive = true;
                            _Context.CategoriesMaster.Add(insert);
                            _Context.SaveChanges();
                            dbTran.Commit();
                        }
                        else
                        {
                            CategoryMaster update = _Context.CategoriesMaster.Find(category.CategoryID);
                            update.CategoryName = category.CategoryName;
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
                return RedirectToAction("Index", "Category");
            }
            else
            {
                return View("Index", category);
            }
        }

        public ActionResult Delete(int CategoryID, int page)
        {
            using (DbContextTransaction dbTran = _Context.Database.BeginTransaction())
            {
                try
                {
                    CategoryMaster delete = _Context.CategoriesMaster.Find(CategoryID);
                    _Context.CategoriesMaster.Remove(delete);
                    _Context.SaveChanges();
                    dbTran.Commit();
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    throw ex;
                }
            }
            return RedirectToAction("Index", "Category", routeValues: new { page = page });
        }
    }
}
