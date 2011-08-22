<%@ Page Language="C#" AutoEventWireup="True" Inherits="Sitecore.SharedSource.SearchDemo.DemoPage2" MasterPageFile="~/sitecore modules/Web/SearchDemo/Demo.Master" CodeBehind="DemoPage2.aspx.cs" %>

<asp:Content ContentPlaceHolderID="header" runat="server">
   <h2>
      Scenario 2. Search for relations
   </h2>
   <p>
       Search for relations and refine by location, language, template, full text query
   </p>
</asp:Content>

<asp:Content ContentPlaceHolderID="mainPH" runat="server">
<table>
   <tr>
      <td>
         Find relations to:
      </td>
      <td>
         <asp:Textbox id="RelatedIdsTextBox" width="300px" runat="server" />
      </td>
   </tr>
</table>
</asp:Content>

