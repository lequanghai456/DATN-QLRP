﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public class TestController : Controller
    {
        public class JTableModelCustom : JTableModel
        {
            public string Name { get; set; }
            public string Number { get; set; }
        }
        private readonly IdentityContext _context;

        public TestController(IdentityContext context)
        {
            _context = context;
        }

        public IActionResult Index() {
           
            return View();
        }

        public IActionResult CreateEdit(int? id)
        {
            if (id != null)
            {
                return View(_context.Test.Find(id));
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit([Bind("Name,Id,Number")]Test test)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (test.Id == 0)
                    {
                        _context.Add(test);
                    }
                    else
                    {
                        _context.Update(test);
                    }
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                }
            }

            return View("Index");
        }

        [HttpGet]
        public String JtableTestModel(JTableModelCustom jTablePara)
        {
            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;
            var query = from a in _context.Test
                        where ((String.IsNullOrEmpty(jTablePara.Name) || a.Name.Contains(jTablePara.Name))
                        &&(String.IsNullOrEmpty(jTablePara.Number) || a.Name.Contains(jTablePara.Number)))
                        select a;

            int count = query.Count();

            var data = query.AsQueryable()
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata =JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "Id", "Name", "Number");
            return JsonConvert.SerializeObject(jdata);
        }

        [HttpGet]
        public JsonResult GetALL(String Name, String Number) {
            var data = _context.Test.ToList();

            if (!String.IsNullOrEmpty(Name))
            {
                data = data.Where(x => x.Name.Contains(Name)).ToList();
            }
            if (!String.IsNullOrEmpty(Number))
            {
                data = data.Where(x => x.Number.Contains(Number)).ToList();
            }

            return Json(new { data=data });
        }
    }

}

