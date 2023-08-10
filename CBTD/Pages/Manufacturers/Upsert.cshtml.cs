using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Models;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTD.Pages.Manufacturers
{
    public class UpsertModel : PageModel
    {

        private readonly UnitOfWork _unitOfWork;
        [BindProperty]  //synchonizes form fields with values in code behind
        public Manufacturer objManufacturer { get; set; }


        public UpsertModel(UnitOfWork unitOfWork)  //dependency injection
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(int? id)
        {
            objManufacturer = new Manufacturer();

            //edit mode
            if (id != 0)
            {
                objManufacturer = _unitOfWork.Manufacturer.GetById(id);
            }

            if (objManufacturer == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //if this is a new category
            if (objManufacturer.Id == 0)
            {
                _unitOfWork.Manufacturer.Add(objManufacturer);
                TempData["success"] = "Manufacturer added Successfully";
            }
            //if category exists
            else
            {
                _unitOfWork.Manufacturer.Update(objManufacturer);
                TempData["success"] = "Manufacturer updated Successfully";
            }
            _unitOfWork.Commit();

            return RedirectToPage("./Index");
        }

    }
}
