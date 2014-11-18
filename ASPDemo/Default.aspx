<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASPDemo.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 107px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Group ID: "></asp:Label>
        <asp:TextBox ID="TextBoxID" runat="server"></asp:TextBox>
        <asp:LinkButton ID="LinkButtonGet" runat="server" OnClick="LinkButtonGet_Click">Go</asp:LinkButton>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="LinkButtonAdd" runat="server" OnClick="LinkButtonAdd_Click">Add</asp:LinkButton>
        <br />
    
    </div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">Name</td>
                <td>
                    <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=MIKE-PC;Initial Catalog=Jerky;Persist Security Info=True;User ID=mike;Password=mike" ProviderName="System.Data.SqlClient" SelectCommand="select * from Groups"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Description</td>
                <td>
                    <asp:TextBox ID="TextBoxDesc" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:GridView ID="GridView1" runat="server" Width="284px">
                    </asp:GridView>
                </td>
                <td>
                    <asp:DataList ID="DataList1" runat="server">
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
