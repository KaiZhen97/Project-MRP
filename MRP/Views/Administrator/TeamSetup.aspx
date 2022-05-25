<%@ Page Title="Team Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeamSetup.aspx.cs" Inherits="MRP.Views.Administrator.TeamSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/TeamSetup") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h4 class="_pageTitle"><%= Page.Title %></h4>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <%: Scripts.Render("~/scriptBundle/TeamSetup") %>
</asp:Content>