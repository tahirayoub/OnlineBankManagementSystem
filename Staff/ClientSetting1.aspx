<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMenu.master" AutoEventWireup="true" CodeFile="ClientSetting1.aspx.cs" Inherits="Staff_ClientSetting1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
               <asp:Label ID="LblClientId" runat="server" Text="Label"></asp:Label>
               </ContentTemplate>
    </asp:UpdatePanel>
  
</asp:Content>

