#pragma checksum "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Staffs\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0c72530394501e09b6213cc32b7dfb2d6f8936fe"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Staffs_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/Staffs/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0c72530394501e09b6213cc32b7dfb2d6f8936fe", @"/Areas/Admin/Views/Staffs/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"930360e4474960a6084f8448cf0028a5a4189775", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Staffs_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<QuanLiRapPhim.Areas.Admin.Models.Staff>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "{{x.id}}", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("ng-value", new global::Microsoft.AspNetCore.Html.HtmlString("x.id"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("ng-repeat", new global::Microsoft.AspNetCore.Html.HtmlString("x in dataRole"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/View/admin/Staff/controller.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Staffs\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Areas/Admin/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div id=\'contentMain\' ng-app=\"App\" ng-controller=\"Ctroller\">\r\n    <div ng-show=\"create\">\r\n        ");
#nullable restore
#line 9 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Staffs\Index.cshtml"
   Write(await Html.PartialAsync("Create"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    </div>
    <div>
        <div class=""row"">
            <input style=""margin-left:30px;"" placeholder=""Tìm kiếm theo tên"" class=""col-3"" type=""text"" ng-model=""valueName"" />
            <button class=""icon col-1"" ng-click=""Search()""><i class=""fa fa-search""></i></button>
            <div class=""col-lg-4""></div>
            <button class=""btn btn-primary col-1"" ng-click=""Show()"">Input</button>
            <button class=""btn btn-warning col-1"" ng-click=""deleteStaffList()"" data-toggle=""modal"" data-target=""#myModal"">DeleteList</button>
        </div>
        <div class=""form-group col-3"">
            <label class=""control-label"">Tìm kiếm theo chức vụ</label><br />
            <select class=""form-control"" ng-model=""valueRole"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0c72530394501e09b6213cc32b7dfb2d6f8936fe6080", async() => {
                WriteLiteral("{{x.name}}");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </select>\r\n        </div>\r\n");
#nullable restore
#line 25 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Staffs\Index.cshtml"
          
            if (TempData.Peek("Message") != null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <h2 class=\"alert alert-success\">");
#nullable restore
#line 28 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Staffs\Index.cshtml"
                                           Write(TempData.Peek("Message"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n");
#nullable restore
#line 29 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Staffs\Index.cshtml"
            }
        

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <table class=""table dataTable table-hover table-striped""
               datatable
               dt-options=""dtOptions""
               dt-columns=""dtColumns""
               id=""tblData"" dt-instance=""dtInstance""></table>
    </div>

    <!-- Modal -->
    <div class=""modal fade"" id=""myModal"" role=""dialog"">
        <div class=""modal-dialog"">

            <!-- Modal content-->
            <div class=""modal-content"">
                <div class=""row modal-header"">
                    <h4 class=""col-3 modal-title"">Notification</h4>
                    <div class=""col-5""></div>
                    <button type=""button"" class=""col-3 close"" ng-click=""reloadData(true)"" data-dismiss=""modal"">&times;</button>

                </div>
                <div class=""modal-body"">
                    <h2 class=""alert alert-success"">{{notification}}</h2>
                </div>
                <div class=""modal-footer"">
                    <button type=""button"" class=""btn btn-default"" ng-click=""reload");
            WriteLiteral("Data(true)\" data-dismiss=\"modal\">Close</button>\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 62 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Staffs\Index.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
                WriteLiteral("    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0c72530394501e09b6213cc32b7dfb2d6f8936fe9833", async() => {
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
                WriteLiteral(@"
    <script>
        function readURL(input, idImg) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $(idImg).attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $(""#ful"").change(function () {
            readURL(this, '#imgPre');
        })
    </script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<QuanLiRapPhim.Areas.Admin.Models.Staff> Html { get; private set; }
    }
}
#pragma warning restore 1591
