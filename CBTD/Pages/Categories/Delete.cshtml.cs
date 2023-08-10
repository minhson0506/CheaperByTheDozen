using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Models;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTD.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        //private readonly ApplicationDbContext _db;
        [BindProperty]  //synchonizes form fields with values in code behind
        public Category objCategory { get; set; }


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
            objCategory = new Category();


            //objCategory = _db.Categories.Find(id);
            objCategory = _unitOfWork.Category.GetById(id);

            return Page();
        }

        public IActionResult OnPost()
        {

            //_db.Categories.Remove(objCategory);
            _unitOfWork.Category.Delete(objCategory);
            TempData["success"] = "Category deleted Successfully";

            //_db.SaveChanges();
            _unitOfWork.Commit();

            return RedirectToPage("./Index");
        }

    }
}
