#pragma checksum "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\ShowTimes\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3198709e4db38b239b9d1908b474ddb65c37509f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_ShowTimes_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/ShowTimes/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3198709e4db38b239b9d1908b474ddb65c37509f", @"/Areas/Admin/Views/ShowTimes/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"930360e4474960a6084f8448cf0028a5a4189775", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_ShowTimes_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CreateTimes", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("display: flex; justify-content: space-between;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/View/admin/Showtimes/controller.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\ShowTimes\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Areas/Admin/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div id=\'contentMain\' ng-app=\"App\" ng-controller=\"Ctroller\">\r\n    <div class=\"row\">\r\n        <div class=\"col-12\">\r\n");
#nullable restore
#line 10 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\ShowTimes\Index.cshtml"
              
                if (TempData.Peek("Message") != null)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h2 class=\"alert alert-success\">");
#nullable restore
#line 13 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\ShowTimes\Index.cshtml"
                                               Write(TempData.Peek("Message"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n");
#nullable restore
#line 14 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\ShowTimes\Index.cshtml"
                }
                if (ViewBag.ListMovies != null)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\ShowTimes\Index.cshtml"
               Write(await Html.PartialAsync("CreateTimes"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\ShowTimes\Index.cshtml"
                                                           
                }
            

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <h4>Search and create new showtimes</h4>\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3198709e4db38b239b9d1908b474ddb65c37509f6628", async() => {
                WriteLiteral(@"
                <div style=""display:inline-flex"">
                    <input type=""date"" ng-change=""Search()"" name=""Date"" class=""form-control"" ng-model=""Date"" />
                    <button class=""icon"" ><i class=""fa fa-search""></i></button>
                </div>
                <div>
                    <button class=""btn btn-primary"">Input</button>
                    <button class=""btn btn-warning"" ng-click=""deleteShowTimeList()""
                            data-toggle=""modal"" data-target=""#myModal"">
                        DeleteList
                    </button>
                </div>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
        </div>
    </div>
    <table class=""table dataTable table-hover table-striped""
           datatable
           dt-options=""dtOptions""
           dt-columns=""dtColumns""
           id=""tblData"" dt-instance=""dtInstance""></table>

    <!-- Modal -->
    <div class=""modal fade"" id=""myModal"" role=""dialog"">
        <div class=""modal-dialog"">

            <!-- Modal content-->
            <div class=""modal-content"">
                <div class=""row modal-header"">
                    <h4 class=""col-3 modal-title"">Notification</h4>
                    <div class=""col-5""></div>
                    <button type=""button"" class=""col-3 close"" ng-click=""reloadData(true)"" 
                            data-dismiss=""modal"">&times;</button>
                </div>
                <div class=""modal-body"">
                    <h2 class=""alert alert-success"">{{notification}}</h2>
                </div>
                <div class=""modal-footer"">
                    <button type=""button"" class=""btn btn");
            WriteLiteral("-default\" ng-click=\"reloadData(true)\" data-dismiss=\"modal\">Close</button>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 66 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\ShowTimes\Index.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
                WriteLiteral("    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3198709e4db38b239b9d1908b474ddb65c37509f10608", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
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
