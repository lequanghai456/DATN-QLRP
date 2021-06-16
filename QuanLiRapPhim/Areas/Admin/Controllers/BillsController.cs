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
    public class BillsController : Controller
    {
        private readonly IdentityContext _context;

        public BillsController(IdentityContext context)
        {
            _context = context;
        }
        public class JTableModelCustom : JTableModel
        {
            public string NameUser { get; set; }
            public string Date { get; set; }
           
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
            var query = _context.Bills.Include(a => a.User).Where(x => x.IsDelete == false && (String.IsNullOrEmpty(jTablePara.NameUser) || x.User.FullName.Contains(jTablePara.NameUser)));
            int count = query.Count();
            var data = query.AsQueryable().Select(x => new { x.Id, Date = x.Date.ToString("MM/dd/yyyy"), x.TotalPrice,UserName = x.User.FullName})
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
