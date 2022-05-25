<%@ Page Title="File Type Attachment Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FileTypeAttachment.aspx.cs" Inherits="MRP.Views.Administrator.FileTypeAttachment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/FileTypeAttachmentSetup") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h4 class="_pageTitle"><%= Page.Title %></h4>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <%: Scripts.Render("~/scriptBundle/FileTypeAttachmentSetup") %>
</asp:Content>