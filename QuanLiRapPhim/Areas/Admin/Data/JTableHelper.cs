using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

namespace QuanLiRapPhim.Areas.Admin.Data
{
    public class JTableHelper
    {
        public static JObject JObjectTable(ICollection collection, int draw, int totalRecord, params string[] columns)
        {
            var jobjectTalbe = new JObject();
            var aaData = new JArray();
            var index = 0;
            foreach (var item in collection)
            {
                index++;
                var objtype = item.GetType();
                var properties = item.GetType().GetProperties();
                var jitem = new JObject();
                foreach (var pro in properties)
                {
                    if ((from x in columns where x.ToLower().Equals(pro.Name.ToLower()) select x).Any() || !columns.Any())
                    {
                        string value = string.Empty;
                        try
                        {
                            value = pro.GetValue(item, null) != null ? pro.GetValue(item, null).ToString() : "";
                        }
                        catch (Exception)
                        { }
                        jitem.Add(pro.Name, value);
                    }
                }
                jitem.Add("_STT", index);
                aaData.Add(jitem);
            }
            jobjectTalbe.Add("draw", draw);
            jobjectTalbe.Add("recordsTotal", totalRecord);
            jobjectTalbe.Add("recordsFiltered", totalRecord);
            jobjectTalbe.Add("data", aaData);
            return jobjectTalbe;
        }

    }
}