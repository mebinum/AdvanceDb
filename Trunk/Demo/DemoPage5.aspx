<%@ Page Language="C#" AutoEventWireup="True" Inherits="SearchDemo.Scripts.DemoPage5"
   CodeBehind="DemoPage5.aspx.cs" MasterPageFile="~/Demo/Demo.Master" %>

<asp:Content ContentPlaceHolderID="mainPH" runat="server">
   <h3>
      Search Parameters:</h3>
   Date Range 1:
   <asp:TextBox ID="FieldName1TextBox" runat="server" />
   Start:
   <asp:TextBox ID="StartDate1TextBox" runat="server" />
   End:
   <asp:TextBox ID="EndDate1TextBox" runat="server" />
   <br />
   Date Range 2:
   <asp:TextBox ID="FieldName2TextBox" runat="server" />
   Start:
   <asp:TextBox ID="StartDate2TextBox" runat="server" />
   End:
   <asp:TextBox ID="EndDate2TextBox" runat="server" />
</asp:Content>
