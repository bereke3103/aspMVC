using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {


            //вместо этого будем вставлять datatable from datatables.net
            //IEnumerable<Product> objCategoryList = _unitOfWork.Product.GetAll();

            return View();
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {

            //CUSTOMVALIDATION
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category create successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }




        //GET
        //EditCategory
        public IActionResult Upsert(int? id)
        {

            ProductVM productVM = new()
            {
                product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text= i.Name,
                    Value = i.Id.ToString(),
                }),

                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text= i.Name,
                    Value = i.Id.ToString(),
                })

            };


            //БЫЛА ПЕРЕНЕСЕНА В PRODUCT VIEWS MODELS
            //Product product = new();

            ////select List dropdown
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll()
            //    .Select(u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString(),
            //    });

            //IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll()
            //    .Select(u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    });


            if (id == null || id == 0)
            {
                //ViewBag for HTML
                //ViewBag.CategoryList = CategoryList;

                //ViewData for HTML
                //ViewData["CoverTypeList"] = CoverTypeList;


                //create product
                return View(productVM);
            }
            else
            {
                productVM.product = _unitOfWork.Product.GetFirstOrDefault(i => i.Id == id);
                //update product

                return View(productVM);
            }

            

        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");

                    var extension = Path.GetExtension(file.FileName);

                    if (obj.product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.product.ImageUrl = @"\images\products\" + fileName + extension;
                }

                if (obj.product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.product);

                }

                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        #region API CALLS
        [HttpGet]

        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");

            return Json(new { data = productList });
        }
        #endregion


        //POST
        //ActionName for in <form asp-action="Delete"></form>
        [HttpDelete]
        //[ValidateAntiForgeryToken] -> для HttpDelete нежелательно
        public IActionResult Delete(int? id)
        {

            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successfull" });

        }
    }


}
