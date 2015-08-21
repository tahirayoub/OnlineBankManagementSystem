<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMenu.master" AutoEventWireup="true" CodeFile="StaffClientAccountTransfer.aspx.cs" Inherits="Client_AccountTransfer" %>

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
                    <td class="auto-style2" style="color: #800000">&nbsp;</td>
                    <td class="auto-style2" style="color: #800000">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="False" ForeColor="Red" Text="Transfer Of Funds"></asp:Label>
                        &nbsp;(with in client Account)</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="LblClientId" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
            </table>
            <hr />
          <table class="auto-style1">
                <tr>
                    <td class="auto-style3">
                        Client : From Account</td>
                    
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Type" DataValueField="Account_Type_Id">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:bankDBConnectionString1 %>" SelectCommand="SELECT AccountType.Type, Account.Account_Type_Id FROM Account INNER JOIN AccountType ON Account.Account_Type_Id = AccountType.Account_Type_Id WHERE (Account.Client_Id = @Client_Id)">
                            <SelectParameters>
                                <asp:SessionParameter Name="Client_Id" SessionField="s_c_id" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
           
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td>
                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="Note :Transfer From Saving Account to Current Account will cost you extra 2% fee"></asp:Label>
                    </td>
                </tr>
           
                <tr>
                    <td class="auto-style3">Client : To Account</td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource2" DataTextField="Type" DataValueField="Account_Type_Id">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:bankDBConnectionString1 %>" SelectCommand="SELECT AccountType.Type, Account.Account_Type_Id FROM Account INNER JOIN AccountType ON Account.Account_Type_Id = AccountType.Account_Type_Id WHERE (Account.Client_Id = @Client_Id)">
                            <SelectParameters>
                                <asp:SessionParameter Name="Client_Id" SessionField="s_c_id" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">Amount</td>
                    <td>
                        <asp:TextBox ID="TxtAmount" runat="server" AutoComplete ="off" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">Date</td>
                    <td>
                        <asp:Label ID="LblDate" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
           
            </table>
            <hr />
            <table class="auto-style1">
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LblError" runat="server" Text="Label" ForeColor="Red" Visible="False"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Icon Images/submitImage.jpg" OnClick="ImageButton2_Click" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
          
     
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

