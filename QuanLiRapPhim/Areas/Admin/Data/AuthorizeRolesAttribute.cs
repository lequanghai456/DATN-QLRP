using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using QuanLiRapPhim.Areas.Admin.Models;
using System;


namespace QuanLiRapPhim.Areas.Admin.Data
{
    public class AuthorizeRolesAttribute : ResultFilterAttribute
    {

        public String[] _acceptedRoles { get; set; }
        public AuthorizeRolesAttribute(String roles)
        {
            this._acceptedRoles = roles.Split(',');
        }

        public String Roles { get; set; }
        private RoleManager<Role> RoleMgr { get; }
        private UserManager<Staff> StaffMgr { get; }
        private SignInManager<Staff> SignInMgr { get; }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            try
            {
                bool flag = false;
                if (context.HttpContext.User.Identity.IsAuthenticated)
                {
                    var role = context.HttpContext.User.FindFirst("Role").Value;
                    foreach (string item in _acceptedRoles)
                    {
                        if (item.ToUpper().Contains(role.ToUpper()))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == true)
                    { }
                    else
                    {
                        context.Result = new RedirectResult("~/admin/home");
                    }
                }
                else
                {
                    context.Result = new RedirectResult("~/home");
                }
            }
            catch(Exception ex)
            {
                if (context.HttpContext.User.Identity.IsAuthenticated)
                {

                }else
                    context.Result = new RedirectResult("~/home");
            }
        }
     
    }
}