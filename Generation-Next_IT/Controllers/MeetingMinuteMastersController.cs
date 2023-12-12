using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Generation_Next_IT.Data;
using Generation_Next_IT.Models;
using Generation_Next_IT.Models.ViewModel;

namespace Generation_Next_IT.Controllers
{
    public class MeetingMinuteMastersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeetingMinuteMastersController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: MeetingMinute/Index
        public async Task<IActionResult> Index()
        {
            var meetingMinutes = await _context.Meeting_Minutes_Master_Tbl
                .Include(m => m.CorporateCustomer)
                .Include(m => m.IndividualCustomer)
                .Include(m => m.MeetingMinuteDetails)
                .ThenInclude(d => d.ProductService)
                .ToListAsync();

            var viewModelList = meetingMinutes.Select(m => new MeetingMinute_ViewModel
            {
                MeetingMinuteMasterID = m.MeetingMinuteMasterID,
                CustomerType = m.CustomerType,
                Date = m.Date,
                Time = m.Time,
                MeetingPlace = m.MeetingPlace,
                AttendsFromClient = m.AttendsFromClient,
                AttendsFromHost = m.AttendsFromHost,
                Agenda = m.Agenda,
                Discussion = m.Discussion,
                Decision = m.Decision,


                MeetingMinuteDetailID = m.MeetingMinuteDetails.FirstOrDefault()?.MeetingMinuteDetailID ?? 0,
                Quantity = m.MeetingMinuteDetails.FirstOrDefault()?.Quantity ?? 0,
                Unit = m.MeetingMinuteDetails.FirstOrDefault()?.ProductService.Unit ?? 0,
                ProductServiceName = m.MeetingMinuteDetails.FirstOrDefault()?.ProductService?.ProductServiceName ?? "N/A",

                SelectedCustomerName = m.CustomerType == CustomerType.Corporate ? m.CorporateCustomer?.CorporateCustomerName ?? "N/A" : m.IndividualCustomer?.IndividualCustomerName ?? "N/A",
                MeetingMinuteDetails = m.MeetingMinuteDetails.ToList()
            }).ToList();

            return View(viewModelList);
        }
        //[HttpGet("AddNewProduct/{id}")]
        public IActionResult AddNewProduct(int? id)
        {
          
            ViewBag.Products = new SelectList(_context.Products_Service_Tbl, "ProductServiceID", "ProductServiceName", id.ToString() ?? "");
            return PartialView("_Add");
        }
        public IActionResult unit(int? id)
        {
            ViewBag["id"] = id;
            return PartialView("_Add");
        }
        [HttpGet]
        public IActionResult GetCustomerNames(CustomerType customerType)
        {
            IEnumerable<SelectListItem> customers;

            if (customerType == CustomerType.Corporate)
            {
                customers = _context.Corporate_Customer_Tbl
                    .Select(c => new SelectListItem { Value = c.CorporateCustomerID.ToString(), Text = c.CorporateCustomerName })
                    .ToList();
            }
            else if (customerType == CustomerType.Individual)
            {
                customers = _context.Individual_Customer_Tbl
                    .Select(c => new SelectListItem { Value = c.IndividualCustomerID.ToString(), Text = c.IndividualCustomerName })
                    .ToList();
            }
            else
            {
                customers = Enumerable.Empty<SelectListItem>();
            }

            return Json(customers);
        }

        [HttpGet]
        public IActionResult GetUnit(int productServiceID)
        {
            var unit = _context.Products_Service_Tbl
                            .Where(p => p.ProductServiceID == productServiceID)
                            .Select(p => p.Unit)
                            .FirstOrDefault();

       
            Console.WriteLine($"ProductServiceID: {productServiceID}, Unit: {unit}");

            return Json(new { unit });
        }


        // GET: MeetingMinute/Create
        public IActionResult Create()
        {
            ViewData["ProductsServices"] = new SelectList(_context.Products_Service_Tbl, "ProductServiceID", "ProductServiceName");

            var meetingMinuteInputModel = new MeetingMinute_InputModel();

   
            ViewData["Customers"] = ViewData["Customers"] ?? Enumerable.Empty<SelectListItem>();


            return View(meetingMinuteInputModel);
        }

        // POST: MeetingMinute/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MeetingMinute_InputModel meetingMinute_InputModel)
        {
            ViewData["CorporateCustomers"] = new SelectList(await _context.Corporate_Customer_Tbl.ToListAsync(), "CorporateCustomerID", "CorporateCustomerName");

            ViewData["IndividualCustomers"] = new SelectList(await _context.Individual_Customer_Tbl.ToListAsync(), "IndividualCustomerID", "IndividualCustomerName");

            ViewData["ProductsServices"] = new SelectList(await _context.Products_Service_Tbl.ToListAsync(), "ProductServiceID", "ProductServiceName");

            if (ModelState.IsValid)
            {
                
                Console.WriteLine($"CustomerType: {meetingMinute_InputModel.CustomerType}, CorporateCustomerID: {meetingMinute_InputModel.CorporateCustomerID}, IndividualCustomerID: {meetingMinute_InputModel.IndividualCustomerID}");

                var meetingMinuteMaster = new MeetingMinuteMaster
                {
                    
                    CustomerType = meetingMinute_InputModel.CustomerType,
                    CorporateCustomerID = meetingMinute_InputModel.CustomerType == CustomerType.Corporate ? meetingMinute_InputModel.CorporateCustomerID : null,
                    IndividualCustomerID = meetingMinute_InputModel.CustomerType == CustomerType.Individual ? meetingMinute_InputModel.IndividualCustomerID : null,
                    Date = meetingMinute_InputModel.Date,
                    Time = meetingMinute_InputModel.Time,
                    MeetingPlace = meetingMinute_InputModel.MeetingPlace,
                    AttendsFromClient = meetingMinute_InputModel.AttendsFromClient,
                    AttendsFromHost = meetingMinute_InputModel.AttendsFromHost,
                    Agenda = meetingMinute_InputModel.Agenda,
                    Discussion = meetingMinute_InputModel.Discussion,
                    Decision = meetingMinute_InputModel.Decision
                };

               
                _context.Meeting_Minutes_Master_Tbl.Add(meetingMinuteMaster);
                await _context.SaveChangesAsync();

               
                var meetingMinuteDetail = new MeetingMinuteDetail
                {
                    
                    MeetingMinuteMasterID = meetingMinuteMaster.MeetingMinuteMasterID,
                    ProductServiceID = meetingMinute_InputModel.ProductServiceID,
                    Quantity = meetingMinute_InputModel.Quantity
                };

                
                _context.Meeting_Minutes_Details_Tbl.Add(meetingMinuteDetail);
                await _context.SaveChangesAsync();

               
                return RedirectToAction(nameof(Create));
            }



            return View(meetingMinute_InputModel);
        }

    }
}
