#pragma checksum "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Shared\_Layout3.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "721764183b2256a59759cf6464eacc53bf067064"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Shared__Layout3), @"mvc.1.0.view", @"/Areas/Admin/Views/Shared/_Layout3.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\_ViewImports.cshtml"
using QuanLiRapPhim;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\_ViewImports.cshtml"
using QuanLiRapPhim.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"721764183b2256a59759cf6464eacc53bf067064", @"/Areas/Admin/Views/Shared/_Layout3.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"930360e4474960a6084f8448cf0028a5a4189775", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Shared__Layout3 : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Shared\_Layout3.cshtml"
Write(await Html.PartialAsync("_HeadPartial"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Shared\_Layout3.cshtml"
Write(RenderSection("css", required: false));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"modal-header\">\r\n    <h3 class=\"modal-title\" id=\"modal-title\">");
#nullable restore
#line 5 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Shared\_Layout3.cshtml"
                                        Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n</div>\r\n<div class=\"modal-body\" id=\"modal-body\">\r\n    ");
#nullable restore
#line 8 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Shared\_Layout3.cshtml"
Write(RenderBody());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n<div class=\"modal-footer\">\r\n    <button class=\"btn btn-warning\" type=\"button\" ng-click=\"cancel()\">Cancel</button>\r\n</div>\r\n");
#nullable restore
#line 13 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Shared\_Layout3.cshtml"
Write(await Html.PartialAsync("_LibJSPartial"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 14 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Shared\_Layout3.cshtml"
Write(RenderSection("Scripts", required: false));

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
