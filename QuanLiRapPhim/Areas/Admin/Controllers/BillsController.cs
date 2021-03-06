using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;

namespace QuanLiRapPhim.Areas.Admin.Views
{
    [Area("Admin")]
    [AuthorizeRoles("Admin")]
    public class BillsController : Controller
    {
        private readonly IdentityContext _context;

        public BillsController(IdentityContext context)
        {
            _context = context;
        }
        public class JTableModelCustom : JTableModel
        {
            public int Price { get; set; }
            public string Date { get; set; }
            public string UserName { get; set; }
           
        }
        // GET: Admin/Bills
        public async Task<IActionResult> Index()
        {
            Bill bill = new Bill();
            return View(bill);
        }
        [HttpGet]
        public async Task<String> JtableBillModel(JTableModelCustom jTablePara)
        {
            
            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;
            var query = _context.Bills.Where(x => x.IsDelete == false && (String.IsNullOrEmpty(jTablePara.Date) || x.Date.Date.CompareTo(DateTime.Parse(jTablePara.Date).Date) == 0) && ((String.IsNullOrEmpty(jTablePara.UserName)) || ((x.Username.Contains(jTablePara.UserName)))) && ((jTablePara.Price) < x.TotalPrice));
            int count = query.Count();
            var data = query.AsQueryable().Select(x => new { x.Id, Date = x.Date.ToString("MM/dd/yyyy"), x.TotalPrice,UserName = x.Username})
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "Id", "Date", "TotalPrice", "UserName");
            return JsonConvert.SerializeObject(jdata);
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.Id == id);
        }
    }
}
