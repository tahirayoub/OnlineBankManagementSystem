<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterPage.master" AutoEventWireup="true" CodeFile="WithDrawInvestment.aspx.cs" Inherits="Client_WithDrawInvestment" %>

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
            <td>Please Select Investment you want to withdraw : </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LblId" runat="server" Text="Label" Visible="False"></asp:Label>
                <asp:Label ID="LblInvType" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
            <asp:GridView ID="GVClientBillList" runat="server" AutoGenerateSelectButton="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVClientBillList_PageIndexChanging" OnPreRender="GVClientBillList_PreRender" OnSelectedIndexChanged="GVClientBillList_SelectedIndexChanged" Style="text-align: center">
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
    <br />
    <hr />
    <br />
     <table class="auto-style1" style="width: 100%">
        <tr>
            <td class="auto-style2">
                <asp:Label ID="LblDate" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="auto-style2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">Amount</td>
            <td class="auto-style2">
              <asp:TextBox id="TxtAmount" runat="server" onkeypress="return isNumberKey(event)" AutoComplete ="off" Enabled="False" OnTextChanged="TxtAmount_TextChanged"/>


                &nbsp;
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Red" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="LblInterest" runat="server" Text="0"></asp:Label>


            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                 &nbsp;</td>
        </tr>
         <tr>
             <td>
                 <asp:Label ID="Label4" runat="server" Text="Total Return Amount"></asp:Label>
             </td>
             <td>
                 <asp:Label ID="LblAmount" runat="server" Text="0"></asp:Label>
             </td>
         </tr>
         <tr>
             <td>&nbsp;</td>
             <td>&nbsp;</td>
         </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                 <asp:Label ID="LblAccountType" runat="server" Text="Label" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Note : Withdrawing from Fix Investment 5% fee will be charged"></asp:Label>
             
                <br />
                <br />
                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="Note : Click Submit to withdraw amount"></asp:Label>
             
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Label ID="LblError" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:ImageButton ID="ImageButton2" runat="server" Height="27px" ImageUrl="~/Icon Images/submitImage.jpg" Width="65px" OnClick="ImageButton2_Click" />
            </td>
        </tr>
    </table>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
