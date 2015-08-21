<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMenu.master" AutoEventWireup="true" CodeFile="ClientCreditCard.aspx.cs" Inherits="Staff_ClientCreditCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <script type="text/jscript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
</script>

    <style type="text/css">
        .auto-style3 {
            width: 574px;
        }
        .auto-style4 {
            width: 574px;
            height: 21px;
        }
    </style>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

             
            <table class="auto-style1">
               
                <tr>
                    <td>
                        Client Credit Card Request :</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GVacct" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVacct_PageIndexChanging" OnSelectedIndexChanged="GVacct_SelectedIndexChanged" AutoGenerateSelectButton="True">
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
                    <td>&nbsp;</td>
                </tr>
            </table>

  
          
     
            <br />
             <hr />
            <table class="auto-style1">
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="LblId" runat="server" Text="Label" Visible="False"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        <asp:Label ID="lblCid" runat="server" Text="Client Id"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="LblClientId" runat="server" Text="Label" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="LblFN" runat="server" Text="First Name" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblFirstName" runat="server" Text="First Name" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="LblLN" runat="server" Text="Label" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblLastName" runat="server" Text="Last Name" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="LblEM" runat="server" Text="Email" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblEmail" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="LblMS" runat="server" Text="Monthly Salary"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblMonthlySalary" runat="server" Text="Label" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="LblStat" runat="server" Text="Status" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblStatus" runat="server" Text="Label" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label11" runat="server" Text="Credit Card Amount Limit"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <hr />
            <table class="auto-style1">
                <tr>
                    <td>
                        <asp:ImageButton ID="ImgDelete" runat="server" height="64px" ImageUrl="~/Icon Images/DeleteButton.jpg" width="64px" OnClick="ImgDelete_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ImgUpdate" runat="server" ImageUrl="~/Icon Images/Credit card.png" OnClick="ImgUpdate0_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style2"></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblCN" runat="server" Text="Credit Card" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblCardNo" runat="server" Text="Label" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblBT" runat="server" Text="Bank Transit Number" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblBankTransit" runat="server" Text="Label" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblAL" runat="server" Text="Credit Card limit" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtAmountLimit" runat="server" onkeypress="return isNumberKey(event)" Visible="False"></asp:TextBox>
                        <strong>&nbsp;</strong></td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="Label14" runat="server" Text="Issue Date" Visible="False"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="LblIssueDate" runat="server" Text="Label" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="Expiery Date" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblExpieryDate" runat="server" Text="Label" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Icon Images/submitImage.jpg" OnClick="ImageButton4_Click" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
    <br /> 
        </ContentTemplate>
    </asp:UpdatePanel>
         

  
   
</asp:Content>
