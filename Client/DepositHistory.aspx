<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterPage.master" AutoEventWireup="true" CodeFile="DepositHistory.aspx.cs" Inherits="Client_DepositHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style3 {
            width: 411px;
        }
        .auto-style4 {
            width: 411px;
            height: 15px;
        }
        .auto-style5 {
            height: 15px;
        }
        .auto-style6 {
            height: 15px;
            text-align: left;
        }
        .auto-style7 {
            height: 15px;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
     
           <table class="auto-style1">
        <tr>
            <td class="auto-style4">Pay Bill</td>
            <td style="text-align: right" class="auto-style5">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style7" colspan="2">
                <br />
                 <fieldset style="width:331px" >
    <legend _>Select start Date</legend>
    Year: <asp:DropDownList ID="ddlYear1" runat="server" AutoPostBack="True"
            onselectedindexchanged="ddlYear_SelectedIndexChanged" ></asp:DropDownList> 
           
    Month: <asp:DropDownList ID="ddlMonth1" runat="server" AutoPostBack="True"
            onselectedindexchanged="ddlMonth_SelectedIndexChanged">
        </asp:DropDownList> 
       
    Day: <asp:DropDownList ID="ddlDay1" runat="server">
        </asp:DropDownList>
    </fieldset></td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td class="auto-style6">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style7" colspan="2">
                <br />
                 <fieldset style="width:331px" >
    <legend >Select End Date</legend>
    Year: <asp:DropDownList ID="ddlYear2" runat="server" AutoPostBack="True"
            onselectedindexchanged="ddlYear_SelectedIndexChanged" ></asp:DropDownList> 
           
    Month: <asp:DropDownList ID="ddlMonth2" runat="server" AutoPostBack="True"
            onselectedindexchanged="ddlMonth_SelectedIndexChanged">
        </asp:DropDownList> 
       
    Day: <asp:DropDownList ID="ddlDay2" runat="server">
        </asp:DropDownList>
    </fieldset></td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td class="auto-style6">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">&nbsp;</td>
            <td>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Icon Images/submitImage.jpg" OnClick="ImageButton2_Click" />
            </td>
        </tr>
        </table>
      
         
          
     
        
      
    <hr />
    <table class="auto-style1">
        <tr>
            <td align="right" class="auto-style3">&nbsp;</td>
            <td>
                <asp:Label ID="LblError" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
         
          
     
        
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2" style="color: #800000">&nbsp;</td>
                    <td class="auto-style2" style="color: #800000">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="GVClientBillList" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" style="text-align: center">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </td>
                </tr>
               
                <tr>
                    <td colspan="2">
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Icon Images/Export.png" OnClick="ImageButton3_Click" style="text-align: center" Visible="False" />
                    </td>
                </tr>
            </table>
            <hr />

     
</asp:Content>