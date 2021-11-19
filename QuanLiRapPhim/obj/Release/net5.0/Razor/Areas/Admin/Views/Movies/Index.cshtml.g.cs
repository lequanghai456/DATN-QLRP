#pragma checksum "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Movies\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "48037b8adfc8e1afdea76f80eda9a639c575abaa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Movies_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/Movies/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"48037b8adfc8e1afdea76f80eda9a639c575abaa", @"/Areas/Admin/Views/Movies/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"930360e4474960a6084f8448cf0028a5a4189775", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Movies_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<QuanLiRapPhim.Areas.Admin.Models.Movie>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/View/admin/Movie/controller.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Movies\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Areas/Admin/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div id=\'contentMain\' ng-app=\"App\" ng-controller=\"Ctroller\">\r\n    <div ng-show=\"create\">\r\n        ");
#nullable restore
#line 8 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Movies\Index.cshtml"
   Write(await Html.PartialAsync("Create"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div>\r\n");
#nullable restore
#line 11 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Movies\Index.cshtml"
          
            if (TempData.Peek("Message") != null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <h2 class=\"alert alert-success\">");
#nullable restore
#line 14 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Movies\Index.cshtml"
                                           Write(TempData.Peek("Message"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n");
#nullable restore
#line 15 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Movies\Index.cshtml"
            }
        

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""row"">
            <input style=""margin-left:30px;"" placeholder=""Tìm kiếm theo tên"" class=""col-3"" type=""text"" ng-model=""valueName"" />
            <button class=""icon col-1"" ng-click=""Search()""><i class=""fa fa-search""></i></button>
            <div class=""col-lg-4""></div>
            <button class=""btn btn-primary col-1"" ng-click=""Show()"">Nhập dữ liệu</button>
            <button class=""btn btn-warning col-1"" ng-click=""deleteMovieList()"" data-toggle=""modal"" data-target=""#myModal"">Xóa nhanh</button>
        </div>

        <table class=""table dataTable table-hover table-striped""
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
                <div class=""row modal-he");
            WriteLiteral(@"ader"">
                    <h4 class=""col-3 modal-title"">Thông báo</h4>
                    <div class=""col-5""></div>
                    <button type=""button"" class=""col-3 close"" ng-click=""reloadData(true)"" data-dismiss=""modal"">&times;</button>

                </div>
                <div class=""modal-body"">
                    <h2 class=""alert alert-success"">{{notification}}</h2>
                </div>
                <div class=""modal-footer"">
                    <button type=""button"" class=""btn btn-default"" ng-click=""reloadData(true)"" data-dismiss=""modal"">Close</button>
                </div>
            </div>

        </div>
    </div>
");
#nullable restore
#line 54 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Movies\Index.cshtml"
       if (ViewContext.RouteData.Values["id"] != null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""modal fade"" id=""ModalDetail"" role=""dialog"">
                <div class=""modal-dialog"">

                    <!-- Modal content-->
                    <div class=""modal-content"">
                        <div class=""row modal-header"">
                            <h4 class=""col-3 modal-title"">Thông báo</h4>
                            <div class=""col-5""></div>
                            <button type=""button"" class=""col-3 close"" data-dismiss=""modal"">&times;</button>

                        </div>
                        <div class=""modal-body"">
                            <dl class=""row"">
                                <dt class=""col-sm-2"">

                                </dt>
                                <dd class=""col-sm-10"">

                                </dd>
                            </dl>
                        </div>
                        <div class=""modal-footer"">
                            <button type=""button"" class=""btn btn-default"" data-di");
            WriteLiteral("smiss=\"modal\">Đóng</button>\r\n                        </div>\r\n                    </div>\r\n\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 84 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Movies\Index.cshtml"
        }
    

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 88 "D:\DATN\Use case\QuanLiRapPhim\Areas\Admin\Views\Movies\Index.cshtml"
       await Html.RenderPartialAsync("_ValidationScriptsPartial"); 

#line default
#line hidden
#nullable disable
                WriteLiteral("    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "48037b8adfc8e1afdea76f80eda9a639c575abaa8774", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
    <script>

        $(document).ready(function () {
            $('.js-example-basic-single').select2();
        });


        var trailer = $('#video').val();
        console.log(trailer);
        function readURLa(input, idImg) {
            console.log(input.files[0].type);
            if (input.files[0].type.includes(""image"", 0)) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $(idImg).attr('src', e.target.result);
                    }
                    reader.readAsDataURL(input.files[0]);
                    $('#Poster').html("""");
                }
            } else {
                $('#ful').val(null);
                $('#Poster').html(""Chỉ chấp nhận file ãnh"");
            }
        }
        $(""#ful"").change(function () {
            readURLa(this, '#imgPre');
        })
        function readURL(input, idVideo) {
            if (inp");
                WriteLiteral(@"ut.files[0].type.includes(""video"", 0) && input.files[0].size > 20000000) {
                $(""#video"").val(null);

            }
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $(idVideo).attr('src', e.target.result);

                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $(""#video"").change(function () {
            readURL(this, '#Prevideo');
            if ($(""#video"").val() == trailer) {
                debugger;
                $('#Trailer').text(""Video có dung lượng dưới 20MB và phải có định dạng file video"");
            }
            else {
                $('#Trailer').text("""");
            }
         
        });


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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<QuanLiRapPhim.Areas.Admin.Models.Movie> Html { get; private set; }
    }
}
#pragma warning restore 1591