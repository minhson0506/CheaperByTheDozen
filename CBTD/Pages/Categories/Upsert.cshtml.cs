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
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        //public IEnumerable<Category> objCategoryList;

        //private readonly ApplicationDbContext _db;
        [BindProperty]  //synchonizes form fields with values in code behind
        public Category objCategory { get; set; }


        //public UpsertModel(ApplicationDbContext db)  //dependency injection
        //{
        //    _db = db;
        //}

        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult OnGet(int? id)
        {
            objCategory = new Category();

            //edit mode
            if (id != 0)
            {
                //objCategory = _db.Categories.Find(id);
                objCategory = _unitOfWork.Category.GetById(id);
            }

            if (objCategory == null)
            {
                return NotFound();
            }

            //create new mode
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //if this is a new category
            if (objCategory.CategoryId == 0)
            {
                //_db.Categories.Add(objCategory);
                _unitOfWork.Category.Add(objCategory);
                TempData["success"] = "Category added Successfully";
            }
            //if category exists
            else
            {
                //_db.Categories.Update(objCategory);
                _unitOfWork.Category.Update(objCategory);
                TempData["success"] = "Category updated Successfully";
            }
            //_db.SaveChanges();
            _unitOfWork.Commit();

            return RedirectToPage("./Index");
        }

    }
}
