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

namespace QuanLiRapPhim.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillDetailsController : Controller
    {
        private readonly IdentityContext _context;
        public class JTableModelCustom : JTableModel
        {
            public int IdBill { get; set; }
           
        }
        public BillDetailsController(IdentityContext context)
        {
            _context = context;
        }

        // GET: Admin/BillDetails
        public async Task<IActionResult> Index()
        {
            BillDetail billDetail = new BillDetail();
            return View(billDetail);
        }
        [HttpGet]
        public async Task<String> JtableBillDetailModel(JTableModelCustom jTablePara)
        {
            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;
            var query = _context.BillDetails.Include(a => a.Sevice).Where(x=>(jTablePara.IdBill == 0 || x.BillId == jTablePara.IdBill));
            int count = query.Count();
            var data = query.AsQueryable().Select(x => new { x.Id, x.Amount ,x.UnitPrice , NameSevice = x.Sevice.Name,x.BillId })
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "Id", "Amount", "UnitPrice", "NameSevice", "BillId");
            return JsonConvert.SerializeObject(jdata);
        }
    }
}
