﻿using DataAccess.Models;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTD.Pages.Manufacturers
{
    public class IndexModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;  //local instance of the database service

        public IEnumerable<Manufacturer> objManufacturerList;  //our UI front end will support looping through and displaying Categories retrieved from the database and stored in a List

        public IndexModel(UnitOfWork unitOfWork)  //dependency injection of the database service
        {
            _unitOfWork = unitOfWork;
            objManufacturerList = new List<Manufacturer>();
        }

        public IActionResult OnGet()
        //There are five major sets of IActionResult Types the can be returned
        //1. Server Status Code Results
        //2. Server Status Code and Object Results
        //3. Redirect (to another webpage) Results
        //4. File Results
        //5. Content Results (like another Razor Web Page)
        {
            objManufacturerList = _unitOfWork.Manufacturer.GetAll();
            return Page();  // render page
        }

    }
}
