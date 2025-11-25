<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Laboratorio154.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        Introduzca dos números:<p>
            <asp:TextBox ID="TextBox1" runat="server" style="margin-left: 0px" Width="135px"></asp:TextBox>
            <asp:TextBox ID="TextBox2" runat="server" style="margin-left: 24px; margin-top: 0px" Width="135px"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="margin-left: 29px" Text="Sumar!" Width="103px" />
        </p>
        <p>
            <asp:Label ID="Label1" runat="server" Text="Respuesta!"></asp:Label>
        </p>
    </form>
</body>
</html>
