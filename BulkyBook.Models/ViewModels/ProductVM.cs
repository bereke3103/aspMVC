using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModels
{
    public class ProductVM
    {

        public Product product { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CoverTypeList { get; set; }




        //БЫЛА ВЫРВАНА ИЗ КОНТРОЛЯ И ПЕРЕНЕСЕНА СЮДА, ПЕРЕПИСАВ ЕГО
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
    }
}
