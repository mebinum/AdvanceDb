<%@ Page Language="C#" AutoEventWireup="True" Inherits="SearchDemo.Scripts.DemoPage4"
   CodeBehind="DemoPage4.aspx.cs" MasterPageFile="~/Demo/Demo.Master" %>

<asp:Content ContentPlaceHolderID="mainPH" runat="server">
   <h3>
      Search Parameters:</h3>
   Numeric Range 1:
   <asp:TextBox ID="FieldName1TextBox" runat="server" />
   Start:
   <asp:TextBox ID="Start1TextBox" runat="server" />
   End:
   <asp:TextBox ID="End1TextBox" runat="server" />
   <br />
   Numeric Range 2:
   <asp:TextBox ID="FieldName2TextBox" runat="server" />
   Start:
   <asp:TextBox ID="Start2TextBox" runat="server" />
   End:
   <asp:TextBox ID="End2TextBox" runat="server" />
</asp:Content>
