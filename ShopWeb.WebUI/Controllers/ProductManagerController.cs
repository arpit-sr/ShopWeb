using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWeb.Core.Contracts;
using ShopWeb.Core.Models;
using ShopWeb.Core.ViewModels;
using ShopWeb.DataAccess.InMemory;

namespace ShopWeb.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModels = new ProductManagerViewModel();
            viewModels.Product = new Product();
            viewModels.ProductCategories = productCategories.Collection();

            Product product = new Product();
            return View(viewModels);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(String Id)
        {
            Product product = context.Find(Id);
                if(product==null)
            {
                return HttpNotFound();
            }
                else
            {
                ProductManagerViewModel viewModels = new ProductManagerViewModel();
                viewModels.Product = product;
                viewModels.ProductCategories = productCategories.Collection();

                return View(viewModels);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, String Id)
        {
            Product productToEdit = context.Find(Id);

            if(productToEdit==null)
            {
                return HttpNotFound();
            }
            else
            {
                if(!ModelState.IsValid)
                {
                    return View(product);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(String Id)
        {
            Product productToDelete = context.Find(Id);

            if(productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("DELETE")]
        public ActionResult ConfirmDelete(String Id)
        {
            Product productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}