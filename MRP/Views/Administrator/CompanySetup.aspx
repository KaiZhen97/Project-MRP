<%@ Page Title="Company Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanySetup.aspx.cs" Inherits="MRP.Views.Administrator.CompanySetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/CompanySetup") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h4 class="_pageTitle"><%= Page.Title %></h4>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <%: Scripts.Render("~/scriptBundle/CompanySetup") %>
</asp:Content>
