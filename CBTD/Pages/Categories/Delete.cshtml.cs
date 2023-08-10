using DataAccess.Models;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTD.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]  //synchonizes form fields with values in code behind
        public Category objCategory { get; set; }

        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(int? id)
        {
            objCategory = new Category();

            objCategory = _unitOfWork.Category.GetById(id);

            return Page();
        }

        public IActionResult OnPost()
        {
            _unitOfWork.Category.Delete(objCategory);
            TempData["success"] = "Category deleted Successfully";

            _unitOfWork.Commit();

            return RedirectToPage("./Index");
        }

    }
}
