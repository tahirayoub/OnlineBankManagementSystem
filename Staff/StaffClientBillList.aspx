﻿<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMenu.master" AutoEventWireup="true" CodeFile="StaffClientBillList.aspx.cs" Inherits="Client_BillList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <script type="text/jscript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            

             
         
          
     
        
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2" style="color: #800000">
                        <asp:Label ID="LblClientId" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="auto-style2" style="color: #800000">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2" style="color: #800000">Common Billing list</td>
                    <td class="auto-style2" style="color: #800000">Client Personal Billing list</td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GVBillList" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVBillList_PageIndexChanging" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GVBillList_SelectedIndexChanged">
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
                    <td>
                        <asp:GridView ID="GVClientBillList" runat="server" AutoGenerateSelectButton="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVClientBillList_PageIndexChanging" OnPreRender="GVClientBillList_PreRender" OnSelectedIndexChanged="GVClientBillList_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" />
                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <SortedAscendingCellStyle BackColor="#FDF5AC" />
                            <SortedAscendingHeaderStyle BackColor="#4D0000" />
                            <SortedDescendingCellStyle BackColor="#FCF6C0" />
                            <SortedDescendingHeaderStyle BackColor="#820000" />
                        </asp:GridView>
                        
                    </td>
                </tr>
            </table>
            <hr />
            <table class="auto-style1">
                <tr>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td style="text-align: right">
                        <asp:Image ID="Image3" runat="server" Height="99px" ImageUrl="~/Images/sample cheque.JPG" style="text-align: right" Width="365px" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3" style="color: #FF0000">Enter Client Personal List if not existed above</td>
                    <td style="text-align: right">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label11" runat="server" Text="Company Transit Number "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtTransitNo" runat="server" onkeypress="return isNumberKey(event)" AutoComplete ="off" height="31px" width="175px" MaxLength="6"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label12" runat="server" Text="Company Account number"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtAccountNo" runat="server" onkeypress="return isNumberKey(event)" AutoComplete ="off" height="31px" width="175px" MaxLength="7"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label8" runat="server" Text="Company Name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtName" runat="server" AutoComplete ="off" Height="31px" Width="368px" MaxLength="30"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="LblError" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <hr />
            <table class="auto-style1">
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                    <td>
                        <asp:ImageButton ID="ImgUpdate" runat="server" ImageUrl="~/Icon Images/submitImage.jpg" OnClick="ImgUpdate0_Click" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            

             
         
          
     
        
        </ContentTemplate>
    </asp:UpdatePanel>
         
</asp:Content>

