<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterPage.master" AutoEventWireup="true" CodeFile="TransferHistory.aspx.cs" Inherits="Client_Transfer_History" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style4">Pay Bill</td>
            <td style="text-align: right" class="auto-style5">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style7">
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
            <td class="auto-style6">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td class="auto-style6">
                &nbsp;</td>
        </tr>
       <tr>
           <td class="auto-style7">&nbsp;</td>
           <td class="auto-style6">&nbsp;</td>
           </tr>
        <tr>
            <td class="auto-style7">
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
           <td class="auto-style6">
               &nbsp;</td>
       </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td class="auto-style6">
                </td>
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
                    <td class="auto-style2" style="color: #800000">
                        <asp:GridView ID="GVClientBillList" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
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
                    <td class="auto-style2" style="color: #800000">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:bankDBConnectionString1 %>" SelectCommand="SELECT Recipient.Name AS [Recipient Name], Recipient.Account_No AS [Recipient Account No], Recipient.Bank_Transit_No AS [Bank Transit Number], Transit.Amount, Transit.Date, AccountType.Type, Account.Account_No FROM AccountType INNER JOIN Account ON AccountType.Account_Type_Id = Account.Account_Type_Id INNER JOIN Client ON Account.Client_Id = Client.Client_Id INNER JOIN Transit INNER JOIN Recipient ON Transit.Recipient_Id = Recipient.Recipient_Id ON Client.Client_Id = Recipient.Client_Id WHERE (Transit.Date BETWEEN '4/2/2014' AND '4/2/2014') AND (Recipient.Client_Id = @Cid) ORDER BY Account.Account_No">
                            <SelectParameters>
                                <asp:SessionParameter Name="cid" SessionField="c_id" />
                            </SelectParameters>
                        </asp:SqlDataSource>
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

