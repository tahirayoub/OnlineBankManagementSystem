<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterPage.master" AutoEventWireup="true" CodeFile="MainMenu.aspx.cs" Inherits="Client_MainMenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 73px;
        }
        .auto-style3 {
            height: 73px;
            width: 724px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="auto-style1">
    <tr>
        <td style="text-align: right">
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            Account Overview</td>
    </tr>
</table>

<table class="auto-style1" runat="server" align="center" >
    
    <tr>
        <td style="text-align: right">
            <asp:GridView ID="GVacct" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="200px" style="text-align: center" Width="636px" OnSelectedIndexChanged="GVacct_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>
        </td>
    </tr>
    
    </table>
    <p>
    </p>
    <table class="auto-style1" runat="server" align="center" >
    
    <tr>
        <td style="text-align: center" class="auto-style4">
          
            <asp:ImageButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" ImageUrl="~/Icon Images/Export.png" style="text-align: center" />
            </td>
    </tr>
    </table>
    <br />

    <hr />
    Credit Card overview  <td colspan="2" style="text-align: left">
            <asp:Label ID="LblCCmessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
            <asp:GridView ID="GVCreditCard" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="200px" style="text-align: center" Width="636px" OnSelectedIndexChanged="GVCreditCard_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>
        </td>
    
    <br />

    <hr />
     <br />
    
<table class="auto-style1" runat="server" align="center" >
    
    <tr>
        <td style="text-align: left">
            &nbsp;</td>
        <td style="text-align: left">
            &nbsp;</td>
    </tr>
    
    <tr>
        <td style="text-align: center; font-size: large; font-weight: bold; color: #FF0000;">
            Summary</td>
        <td style="text-align: left">
            &nbsp;</td>
    </tr>
    
    <tr>
        <td colspan="2" style="text-align: left; font-weight: bold; font-size: large; color: #800000;" class="auto-style2">
            Assets</td>
    </tr>
    </table>
    <table class="auto-style1" align="center" >
    
    <tr>
        <td style="text-align: left; font-weight: normal; font-size: medium; color: #808080;" class="auto-style2">
           
            
             <div id="div1" runat = "server" >
                 
                 <br />

              </div>

        </td>
        <td style="text-align: left" class="auto-style2">
            
       <div id="div2" runat = "server" >
                 
                 <br />

              </div>
          
        </td>
    </tr>
    
    <tr>
        <td style="text-align: left; font-weight: bold; font-size: medium; color: black;" class="auto-style2">
            Total</td>
        <td style="text-align: left" class="auto-style2">
            <asp:Label ID="LblAccountTotal" runat="server" ForeColor="Red" Text="Label"></asp:Label>
        </td>
    </tr>
    
    </table>
    <br />
    <hr />
    <br />
    
</asp:Content>

