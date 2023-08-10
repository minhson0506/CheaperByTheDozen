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
                Console.WriteLine("Product need to change is " + ObjProduct.Id);
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
                Console.WriteLine("Product need to post is " + ObjProduct.Id);
                _unitOfWork.Product.Add(ObjProduct);
                TempData["success"] = "Product added Successfully";
            }
            //if Product exists
            else
            {
                Console.WriteLine("Product need to post is " + ObjProduct.Id);
                _unitOfWork.Product.Update(ObjProduct);
                TempData["success"] = "Product updated Successfully";
            }
            _unitOfWork.Commit();

            return RedirectToPage("./Index");
        }


    }
}
