#pragma checksum "D:\Visual Studio\w3resorce\spices\spices\Areas\Admin\Views\Coupon\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2c02263fa543f07396fd895bb4dabe92010402b6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Coupon_Details), @"mvc.1.0.view", @"/Areas/Admin/Views/Coupon/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Admin/Views/Coupon/Details.cshtml", typeof(AspNetCore.Areas_Admin_Views_Coupon_Details))]
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
#line 1 "D:\Visual Studio\w3resorce\spices\spices\Areas\Admin\Views\_ViewImports.cshtml"
using spices;

#line default
#line hidden
#line 2 "D:\Visual Studio\w3resorce\spices\spices\Areas\Admin\Views\_ViewImports.cshtml"
using spices.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2c02263fa543f07396fd895bb4dabe92010402b6", @"/Areas/Admin/Views/Coupon/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"216fc05eb1c9f8a4db8d4f15c50805e82a2ad566", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Coupon_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<spices.Models.Coupon>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_DetailsAndBackToListButton", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(29, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\Visual Studio\w3resorce\spices\spices\Areas\Admin\Views\Coupon\Details.cshtml"
  
    ViewData["Title"] = "Details";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(121, 129, true);
            WriteLiteral("\r\n<h1>Details</h1>\r\n\r\n<div>\r\n    <h4>Coupon</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(251, 40, false);
#line 15 "D:\Visual Studio\w3resorce\spices\spices\Areas\Admin\Views\Coupon\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(291, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(355, 36, false);
#line 18 "D:\Visual Studio\w3resorce\spices\spices\Areas\Admin\Views\Coupon\Details.cshtml"
       Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(391, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(454, 46, false);
#line 21 "D:\Visual Studio\w3resorce\spices\spices\Areas\Admin\Views\Coupon\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.CouponType));

#line default
#line hidden
            EndContext();
            BeginContext(500, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(564, 42, false);
#line 24 "D:\Visual Studio\w3resorce\spices\spices\Areas\Admin\Views\Coupon\Details.cshtml"
       Write(Html.DisplayFor(model => model.CouponType));

#line default
#line hidden
            EndContext();
            BeginContext(606, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(669, 44, false);
#line 27 "D:\Visual Studio\w3resorce\spices\spices\Areas\Admin\Views\Coupon\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Discount));

#line default
#line hidden
            EndContext();
            BeginContext(713, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(777, 40, false);
#line 30 "D:\Visual Studio\w3resorce\spices\spices\Areas\Admin\Views\Coupon\Details.cshtml"
       Write(Html.DisplayFor(model => model.Discount));

#line default
#line hidden
            EndContext();
            BeginContext(817, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(880, 49, false);
#line 33 "D:\Visual Studio\w3resorce\spices\spices\Areas\Admin\Views\Coupon\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.MinimumAmount));

#line default
#line hidden
            EndContext();
            BeginContext(929, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(993, 45, false);
#line 36 "D:\Visual Studio\w3resorce\spices\spices\Areas\Admin\Views\Coupon\Details.cshtml"
       Write(Html.DisplayFor(model => model.MinimumAmount));

#line default
#line hidden
            EndContext();
            BeginContext(1038, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1101, 44, false);
#line 39 "D:\Visual Studio\w3resorce\spices\spices\Areas\Admin\Views\Coupon\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Pictures));

#line default
#line hidden
            EndContext();
            BeginContext(1145, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1209, 40, false);
#line 42 "D:\Visual Studio\w3resorce\spices\spices\Areas\Admin\Views\Coupon\Details.cshtml"
       Write(Html.DisplayFor(model => model.Pictures));

#line default
#line hidden
            EndContext();
            BeginContext(1249, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1312, 44, false);
#line 45 "D:\Visual Studio\w3resorce\spices\spices\Areas\Admin\Views\Coupon\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.IsActive));

#line default
#line hidden
            EndContext();
            BeginContext(1356, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1420, 40, false);
#line 48 "D:\Visual Studio\w3resorce\spices\spices\Areas\Admin\Views\Coupon\Details.cshtml"
       Write(Html.DisplayFor(model => model.IsActive));

#line default
#line hidden
            EndContext();
            BeginContext(1460, 108, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div class=\"form-group row\">\r\n    <div class=\"col-5 offset-2\">\r\n        ");
            EndContext();
            BeginContext(1568, 63, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "2c02263fa543f07396fd895bb4dabe92010402b69212", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 54 "D:\Visual Studio\w3resorce\spices\spices\Areas\Admin\Views\Coupon\Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model.Id;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1631, 22, true);
            WriteLiteral("\r\n    </div>\r\n</div>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<spices.Models.Coupon> Html { get; private set; }
    }
}
#pragma warning restore 1591
