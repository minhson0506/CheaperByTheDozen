using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Models;
using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTD.Pages.Manufacturers
{
    public class DeleteModel : PageModel
    {

        private readonly UnitOfWork _unitOfWork;
        [BindProperty]  //synchonizes form fields with values in code behind
        public Manufacturer objManufacturer { get; set; }


        public DeleteModel(UnitOfWork unitOfWork)  //dependency injection
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(int? id)
        {
            objManufacturer = new Manufacturer();

            objManufacturer = _unitOfWork.Manufacturer.GetById(id);

            return Page();
        }

        public IActionResult OnPost()
        {
            _unitOfWork.Manufacturer.Delete(objManufacturer);
            TempData["success"] = "Manufacturer deleted Successfully";

            _unitOfWork.Commit();

            return RedirectToPage("./Index");
        }

    }
}
