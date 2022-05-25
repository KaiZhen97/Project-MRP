<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MRP.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %> - MRP System</title>

    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/styleBundle/Login") %>
        <%: Styles.Render("~/styleBundle/iziToast") %>
        <%: Styles.Render("~/styleBundle/Component") %>
    </asp:PlaceHolder>
</head>
<body>
    <%------------------------------------------------------------------------------------------%>
    <%----------------------------------     Pace Overlay     ----------------------------------%>
    <%------------------------------------------------------------------------------------------%>
    <div class="pace-overlay">
        <%--<img class="overlay-style" src="<%= ResolveUrl("~/Content/images/Pace_BackgroundStyle.png") %>" />--%>
        <img class="overlay-logo" src="<%= ResolveUrl("~/Content/images/Aimflex_Logo_White.png") %>" />
        <div class="overlay-footer">POWERED BY AIMFLEX</div>

        <div class="pace-progress-cont">
            <div class="progress-label">Loading...</div>
            <div class="progress-item"></div>
            <div class="progress-footer">Redirect to <%: Page.Title %></div>
        </div>
    </div>

    <div class="bg"/>

    <%------------------------------------------------------------------------------------------%>
    <%----------------------------------     Main Content     ----------------------------------%>
    <%------------------------------------------------------------------------------------------%>
    <form id="form1" runat="server" class="container-wrapper">
        <div class="container">
            <img class="logo" src="./Content/images/Login_Logo.svg" />

            <div class="title">MATRIX ONE</div>
            <div class="title">MRP SYSTEM</div>

            <div class="input-row" style="margin-top: 30px">
                <i class="fa-solid fa-user solid-icon"></i>
                <input runat="server" id="Input_Username" placeholder="Username..."/>
            </div>

            <div class="input-row" style="margin-bottom: 40px">
                <i class="fa-solid fa-key solid-icon"></i>
                <input runat="server" type="password" id="Input_Password" placeholder="Password..."/>
            </div>

            <div runat="server" id="Output_ErrorMsg"></div>

            <div class="input-button-row">
                <button class="text-button" id="Button_ForgotPassword">Forgot Password?</button>
                <button type="submit" runat="server" class="solid-button normal" id="Button_Login" onserverclick="Button_Login_ServerClick">
                    <i class="fa-solid fa-unlock-keyhole"></i>
                    Login
                </button>
            </div>
        </div>

        <asp:HiddenField ID="antiforgery" runat="server" />

        <script>
            window.paceOptions = {
                ajax: false,
                target: '.pace-progress-cont .progress-item',
                easeFactor: 2.5,
                elements: {
                    checkInterval: 50,
                }
            }
        </script>

        <asp:PlaceHolder runat="server">
            
            <%: Scripts.Render("~/scriptBundle/Login") %>
            <%: Scripts.Render("~/scriptBundle/iziToast") %>
        </asp:PlaceHolder>

        <script>
            Pace.on('start', function () { $(".pace-overlay").removeClass("overlay-inactive") });
            Pace.on('hide', function () {
                $(".pace-overlay").addClass("overlay-inactive");

                setTimeout(() => {
                    $(".pace-overlay").css("display", "none");
                }, 500);
            });
        </script>

        <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/scriptBundle/Component") %>
        </asp:PlaceHolder>
    </form>
</body>
</html>
