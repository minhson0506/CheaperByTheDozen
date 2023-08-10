using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Models;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CBTD.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        //private readonly ApplicationDbContext _db;
        [BindProperty]  //synchonizes form fields with values in code behind
        public Product ObjProduct { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> ManufacturerList { get; set; }


        //public DeleteModel(ApplicationDbContext db)  //dependency injection
        //{
        //    _db = db;
        //}
        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(int? id)
        {
            ObjProduct = new Product();
            CategoryList = _unitOfWork.Category.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.CategoryId.ToString()
                }
                );
            ManufacturerList = _unitOfWork.Manufacturer.GetAll()
                .Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                }
                );

            //objCategory = _db.Categories.Find(id);
            ObjProduct = _unitOfWork.Product.GetById(id);

            return Page();
        }

        public IActionResult OnPost()
        {

            //_db.Categories.Remove(objCategory);
            _unitOfWork.Product.Delete(ObjProduct);
            TempData["success"] = "Product deleted Successfully";

            //_db.SaveChanges();
            _unitOfWork.Commit();

            return RedirectToPage("./Index");
        }

    }
}
