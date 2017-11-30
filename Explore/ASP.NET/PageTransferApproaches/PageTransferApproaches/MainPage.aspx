<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="PageTransferApproaches.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div><asp:TextBox ID="txtInfo" runat="server" Width="800" Height="200" TextMode="MultiLine">Error Message</asp:TextBox></div>
        <div><asp:TextBox ID="txtPage1URL" runat="server" Text="Page1.aspx"></asp:TextBox></div>

        <div><asp:Button ID="btn1" runat="server" Text="Response.Redirect(url)" OnClick="btn1_Click" /></div>
        <div><asp:Button ID="btn2" runat="server" Text="Response.Redirect(url,false)" OnClick="btn2_Click" /></div>
        <div><asp:Button ID="btn3" runat="server" Text="Response.Redirect(url,true)" OnClick="btn3_Click" /></div>

        <div><asp:TextBox ID="txtPage2URL" runat="server">Page2.aspx</asp:TextBox></div>

        <div><asp:Button ID="btn4" runat="server" Text="Server.Transfer(url)" OnClick="btn4_Click" /></div>
        <div><asp:Button ID="btn5" runat="server" Text="Server.Transfer(url, false)" OnClick="btn5_Click" /></div>
        <div><asp:Button ID="btn6" runat="server" Text="Server.Transfer(url, true)" OnClick="btn6_Click" /></div>

        <div><asp:Button ID="btnPageLifeCycle" runat="server" Text="Response.Redirect(PageLifeCycle.aspx)" OnClick="btnPageLifeCycle_Click" /></div>

        <div><asp:TextBox ID="txtErrorMessage" runat="server" Width="800" Height="200">Error Message</asp:TextBox></div>
    </form>
</body>
</html>
