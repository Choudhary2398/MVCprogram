using Microsoft.Ajax.Utilities;
using ministore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ministore.Controllers
{
    public class DemoController : Controller
    {
        Logic lo = new Logic();
        public const string CartSessionKey = "Cartitems"; // Unique key for the cart in session
        // Helper Method: Gets the current cart from session, or creates a new one if it doesn't exist.
        public List<Cartitem> GetCart()
        {
            var cart = Session[CartSessionKey] as List<Cartitem>;
            if (cart == null)
            {
                cart = new List<Cartitem>();
                /*                Session[CartSessionKey] = cart;*/ // Store the new empty cart in session
            }
            return cart;
        }
        // GET: Demo
        public ActionResult Index()
        {
            List<Listofproducts> products = lo.GetAll();
            return View(products);
        }

        // GET: Demo/Details/5
        public ActionResult Details(int id)
        {
            var products = lo.GetAll().Find(s => s.productid == id);
            return View(products);
        }

        // GET: Demo/Create
        public ActionResult Create() => View();

        // POST: Demo/Create
        [HttpPost]
        public ActionResult Create(Listofproducts products, HttpPostedFileBase imageFile)
        {
        
                if (ModelState.IsValid)
                {
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                        string serverPath = Server.MapPath("~/Content/assets/");
                        string relativePath = "/Content/assets/";

                        string savedPath = lo.SaveImage(imageFile, serverPath, relativePath);
                        products.images = savedPath;
                    }
                    lo.Insert(products);
                    return RedirectToAction("Index");
                }
            return View(products);
        }

            // GET: Demo/Edit/5
            public ActionResult Edit(int id)
            {
                List<Listofproducts> products = lo.GetAll();
                var product = lo.GetAll().Find(s => s.productid == id);
                return View(product);
            }

        // POST: Demo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Listofproducts products,HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string serverPath = Server.MapPath("~/Content/assets/");
                    string relativePath = "/Content/assets/";

                    // ✅ Call your helper method
                    string savedPath = lo.SaveImage(imageFile, serverPath, relativePath);
                    products.images = savedPath;
                }
                lo.Update(products);
                return RedirectToAction("Index");
            }
            return View(products);
        }

                // GET: Demo/Delete/5
                public ActionResult Delete(int id)
                {
                    var product = lo.GetAll().Find(s => s.productid == id);
                    return View(product);
                }

                // POST: Demo/Delete/5
                [HttpPost]
                public ActionResult Delete(int id, Listofproducts products)
                {
                    products.productid = id;
                    lo.Delete(products);
                    return RedirectToAction("Index");
                }

                public ActionResult MyCart()
                {
                    var cart = GetCart();
                    return View(cart); // Pass the list of CartItems to the view
                }

                public ActionResult AddToCart(int id, int quantity = 1)
                {
                    // 1. Get the product details from the database
                    var product = lo.GetByproductid(id); // Using the corrected GetByProductId
                    if (product == null)
                    {
                        return HttpNotFound(); // Product not found
                    }

                    // 2. Get the current cart from the session
                    var cart = GetCart();

                    // 3. Check if the item already exists in the cart
                    var existingCartItem = cart.Find(item => item.productid == id);

                    if (existingCartItem != null)
                    {
                        // If it exists, just update the quantity
                        existingCartItem.quantity += quantity;
                    }
                    else
                    {
                        // If it's a new item, add it to the cart
                        cart.Add(new Cartitem()
                        {
                            productid = product.productid,
                            price = product.price
                        });
                    }
                    // 4. Update the session with the modified cart
                    Session[CartSessionKey] = cart;
                    return RedirectToAction("MyCart");
                }


                // POST: Demo/RemoveFromCart/5
                [HttpPost]
                public ActionResult RemoveFromCart(int id)
                {
                    var cart = GetCart();
                    var itemToRemove = cart.Find(item => item.productid == id);

                    if (itemToRemove != null)
                    {
                        cart.Remove(itemToRemove);
                        Session[CartSessionKey] = cart; // Update session
                    }
                    return RedirectToAction("MyCart");
                }

                // POST: Demo/UpdateCartQuantity/5
                [HttpPost]
                public ActionResult UpdateCartQuantity(int id, int quantity)
                {
                    if (quantity < 0) quantity = 0; // Prevent negative quantities

                    var cart = GetCart();
                    var itemToUpdate = cart.Find(item => item.productid == id);

                    if (itemToUpdate != null)
                    {
                        if (quantity == 0)
                        {
                            cart.Remove(itemToUpdate); // Remove if quantity is 0
                        }
                        else
                        {
                            itemToUpdate.quantity = quantity;
                        }
                        Session[CartSessionKey] = cart; // Update session
                    }
                    return RedirectToAction("MyCart");
                }

                // Optional: Action to clear the entire cart
                public ActionResult ClearCart()
                {
                    Session.Remove(CartSessionKey);
                    return RedirectToAction("MyCart");
                }
            }
        }
    

