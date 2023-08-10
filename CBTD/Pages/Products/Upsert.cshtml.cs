using DataAccess.Models;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CBTD.Pages.Products
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;


        [BindProperty]
        public Product ObjProduct { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> ManufacturerList { get; set; }

        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CategoryList = new List<SelectListItem>();
            ManufacturerList = new List<SelectListItem>();

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

            if (id == null || id == 0) //create mode
            {
                return Page();
            }

            //edit mode

            if (id != 0)  //retreive product from DB
            {
                ObjProduct = _unitOfWork.Product.GetById(id);
            }

            if (ObjProduct == null) //maybe nothing returned
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

            //if this is a new Product
            if (ObjProduct.Id == 0)
            {
                _unitOfWork.Product.Add(ObjProduct);
                TempData["success"] = "Product added Successfully";
            }
            //if Product exists
            else
            {
                _unitOfWork.Product.Update(ObjProduct);
                TempData["success"] = "Product updated Successfully";
            }
            _unitOfWork.Commit();

            return RedirectToPage("./Index");
        }


    }
}
