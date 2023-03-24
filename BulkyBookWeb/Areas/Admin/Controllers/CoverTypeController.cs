using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();

            return View(objCoverTypeList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {

            //CUSTOMVALIDATION

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType create successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }




        //GET
        //EditCategory
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var categoryFromDb = _db.Categories.Find(id);
            var CoverTypeFromDbFirstOrDefault = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingleOrDefault = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (CoverTypeFromDbFirstOrDefault == null)
            {
                return NotFound();
            }

            return View(CoverTypeFromDbFirstOrDefault);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        //GET
        //Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var CoverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbFirstOrDefault = _db.Categories.FirstOrDefault(u=> u.Id == id);
            //var categoryFromDbSingleOrDefault = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (CoverTypeFromDb == null)
            {
                return NotFound();
            }

            return View(CoverTypeFromDb);
        }


        //POST
        //ActionName for in <form asp-action="Delete"></form>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {

            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
