<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page1.aspx.cs" Inherits="PageTransferApproaches.Page1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>This is Page1</div>
        <div><asp:TextBox ID="txtErrorMessage" runat="server" Width="800" Height="200">Error Message</asp:TextBox></div>
        <div><asp:TextBox ID="txtInfo" runat="server" Width="800" Height="200" TextMode="MultiLine">Info</asp:TextBox></div>
    </form>
</body>
</html>
