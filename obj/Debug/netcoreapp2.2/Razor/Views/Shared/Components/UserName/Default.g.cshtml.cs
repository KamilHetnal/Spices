#pragma checksum "D:\Visual Studio\w3resorce\spices\spices\Views\Shared\Components\UserName\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e03dc4c52a5b64a68f67cbc935b62cc141891405"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_UserName_Default), @"mvc.1.0.view", @"/Views/Shared/Components/UserName/Default.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Components/UserName/Default.cshtml", typeof(AspNetCore.Views_Shared_Components_UserName_Default))]
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
#line 1 "D:\Visual Studio\w3resorce\spices\spices\Views\_ViewImports.cshtml"
using spices;

#line default
#line hidden
#line 2 "D:\Visual Studio\w3resorce\spices\spices\Views\_ViewImports.cshtml"
using spices.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e03dc4c52a5b64a68f67cbc935b62cc141891405", @"/Views/Shared/Components/UserName/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"216fc05eb1c9f8a4db8d4f15c50805e82a2ad566", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_UserName_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<spices.Models.ApplicationUser>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(38, 6, true);
            WriteLiteral("\r\nHi! ");
            EndContext();
            BeginContext(45, 10, false);
#line 3 "D:\Visual Studio\w3resorce\spices\spices\Views\Shared\Components\UserName\Default.cshtml"
Write(Model.Name);

#line default
#line hidden
            EndContext();
            BeginContext(55, 28, true);
            WriteLiteral(" <i class=\"far fa-user\"></i>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<spices.Models.ApplicationUser> Html { get; private set; }
    }
}
#pragma warning restore 1591
