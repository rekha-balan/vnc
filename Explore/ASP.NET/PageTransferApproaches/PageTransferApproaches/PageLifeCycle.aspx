<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageLifeCycle.aspx.cs" Inherits="PageTransferApproaches.PageLifeCycle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body>
    <form id="form1" runat="server" 
        ondatabinding="form1_DataBinding" 
        ondisposed="form1_Disposed" 
        oninit="form1_Init" 
        onload="form1_Load"
        onprerender="form1_PreRender" 
        onunload="form1_UnLoad">

        <div>This is PageLifeCycle</div>

        <asp:button ID="btnButton" runat="server" text="Button" 
            OnClick="btnButton_Click" 
            OnCommand="btnButton_Command" 
            OnDataBinding="btnButton_DataBinding" 
            OnDisposed="btnButton_Disposed" 
            OnClientClick="btnButton_ClientClick" 
            OnInit="btnButton_Init" 
            OnLoad="btnButton_Load" 
            OnPreRender="btnButton_PreRender" 
            OnUnload="btnButton_UnLoad"/>
    </form>
</body>
</html>
