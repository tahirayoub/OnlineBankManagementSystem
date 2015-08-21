<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterPage.master" AutoEventWireup="true" CodeFile="InvestmentProgress.aspx.cs" Inherits="Client_InvestmentProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style3 {
            width: 716px;
        }
        .auto-style4 {
            width: 347px;
        }
        .auto-style5 {
            width: 347px;
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
            <td class="auto-style3">Investment</td>
            <td style="text-align: right">&nbsp;</td>
            <td style="text-align: right">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="LblId" runat="server" Text="Label" Visible="False"></asp:Label>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3" colspan="2">
                <asp:GridView ID="GVClientBillList" runat="server" AutoGenerateSelectButton="True" CellPadding="4" ForeColor="#333333" GridLines="None" Style="text-align: center" OnPageIndexChanging="GVClientBillList_PageIndexChanging" OnPreRender="GVClientBillList_PreRender" OnSelectedIndexChanged="GVClientBillList_SelectedIndexChanged">
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
            <td>&nbsp;</td>
        </tr>



        <tr>
            <td class="auto-style3">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>



        <tr>
            <td class="auto-style3">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>



        <tr>
            <td class="auto-style3">
                <asp:Label ID="LblDate" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <hr />
    <table class="auto-style1">
        <tr>
            <td class="auto-style4">
                <asp:Label ID="Label1" runat="server" Text="Investment Type"></asp:Label>
            </td>
            <td>
                <asp:Label ID="LblInvType" runat="server" Text="Label" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">
                <asp:Label ID="Label2" runat="server" Text="Payed from Account"></asp:Label>
            </td>
            <td>
                <asp:Label ID="LblAccountType" runat="server" Text="Label" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">
                <asp:Label ID="Label3" runat="server" Text="Start Date"></asp:Label>
            </td>
            <td>
                <asp:Label ID="LblStartDate" runat="server" Text="Label" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">
                <asp:Label ID="Label4" runat="server" Text="End Date"></asp:Label>
            </td>
            <td>
                <asp:Label ID="LblEndDate" runat="server" Text="Label" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style5">
                <asp:Label ID="Label5" runat="server" Text="Total Year for Investment"></asp:Label>
            </td>
            <td class="auto-style2">
                <asp:Label ID="LblYears" runat="server" Text="LblYear" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style5">
                <asp:Label ID="Label6" runat="server" Text="Amount"></asp:Label>
            </td>
            <td class="auto-style2">
                <asp:Label ID="LblAmount" runat="server" Text="Label" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;</td>
            <td class="auto-style2">
                <asp:Label ID="LblTotalProfit" runat="server" Text="Label" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style5"></td>
            <td class="auto-style2">
                <asp:Label ID="LblMsg" runat="server" ForeColor="#009900" Text="*Note : If you want to see your progress Please press submit" Visible="False"></asp:Label>
                <br />
                <br />
                <asp:Label ID="LblMsg0" runat="server" ForeColor="#009900" Text="*Note : If it is a Variable account type you will see the progress on the spot" Visible="False"></asp:Label>
                <br />
                <br />
                <asp:Label ID="LblMsg1" runat="server" ForeColor="#009900" Text="*Note : The variable Amount you can withdraw any time no fee will be deducted" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;</td>
            <td class="auto-style2">&nbsp;</td>
        </tr>
        <tr>
            <td align="right" class="auto-style4">

                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Icon Images/submitImage.jpg" OnClick="ImageButton2_Click" Visible="False" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td align="right" class="auto-style4">&nbsp;</td>
            <td>
                <asp:Label ID="LblError" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
                    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

