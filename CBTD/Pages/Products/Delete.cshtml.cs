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
        [BindProperty]  //synchonizes form fields with values in code behind
        public Product ObjProduct { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> ManufacturerList { get; set; }

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

            ObjProduct = _unitOfWork.Product.GetById(id);

            return Page();
        }

        public IActionResult OnPost()
        {

            _unitOfWork.Product.Delete(ObjProduct);
            TempData["success"] = "Product deleted Successfully";

            _unitOfWork.Commit();

            return RedirectToPage("./Index");
        }

    }
}
