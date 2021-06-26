using AutomatedInvoiceGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Controllers
{
    public class YourOrderController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public  List<Bill> Bills { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<yourOrder> yourOrders { get; set; }

        public YourOrderController()
        {
            Bills = new List<Bill>();
            Tickets = new List<Ticket>();
            List<BillDetail> bills = new List<BillDetail>();
            for(int i = 0; i < 4; i++)
            {
                bills.Add(new BillDetail
                {
                    Id = i,
                    Sevice = new Sevice { Name = "abc " + i , Price=1000},
                    Amount = 1,
                    UnitPrice = 1000,                    
                });
            }
            for (int i = 0; i < 5; i++)
            {
                var bill = new Bill()
                {
                    Id = i,
                    Date = DateTime.Now.AddDays(i),
                    TotalPrice = i * 1000,
                    BillDetails=bills,
                    
                };
                var ticket = new Ticket()
                {
                    Id = i,
                    PurchaseDate = DateTime.Now.AddDays(i),
                    SeatId = i,
                    ShowTimeId = i
                };
                Tickets.Add(ticket);
                Bills.Add(bill);
            }
            yourOrders = new List<yourOrder>();
            yourOrders.AddRange((from x in Bills
                                 select new yourOrder
                                 {
                                     id = "HD" +x.Id,
                                     Objects = x,
                                     Date = x.Date
                                 }).ToList());

            yourOrders.AddRange((from x in Tickets
                                 select new yourOrder
                                 {
                                     id = "V"+x.Id,
                                     Objects = x,
                                     Date = x.PurchaseDate
                                 }).ToList());
            yourOrders = yourOrders.OrderBy(x => x.Date).ToList();
        }

        public JsonResult GetAll()
        {
            return Json(yourOrders);
        }



        [HttpGet]
        public string JtableTestModel(JTableModel jTablePara)
        {
            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;

            var query = from a in yourOrders
                        select new{
                a.id,
                Objects=JsonConvert.SerializeObject(a.Objects),
                Date=a.Date.ToShortDateString()
            };

            int count = query.Count();

            var data = query.AsQueryable()
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "id", "Objects", "Date");
            return JsonConvert.SerializeObject(jdata);
        }

    }
    public class yourOrder
    {
        public String id { get; set; }
        public object Objects { get; set; }
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime Date { get; set; }
        public bool IsTicket { get; set; }

    }
}


