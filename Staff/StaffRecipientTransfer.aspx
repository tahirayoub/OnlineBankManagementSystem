<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMenu.master" AutoEventWireup="true" CodeFile="StaffRecipientTransfer.aspx.cs" Inherits="Client_RecipientTransfer" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" tagPrefix="ajax" %>

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
            width: 225px;
        }
        .auto-style4 {
            width: 256px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <table class="auto-style1">
        <tr>
            <td class="auto-style4">
                <asp:Label ID="LblClientId" runat="server" Text="Label"></asp:Label>
            </td>
            <td style="text-align: right">
                &nbsp;</td>
            <td style="text-align: right">
                &nbsp;</td>
        </tr>
              <tr>
                  <td class="auto-style4">Transfer Mooney to another recipient</td>
                  <td style="text-align: right">
                      <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Icon Images/BackButton.jpg" OnClick="ImageButton3_Click" />
                  </td>
                  <td style="text-align: right">&nbsp;</td>
              </tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">Transit Id</td>
            <td>
                <asp:Label ID="LblTransitId" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">Account Number</td>
            <td>
                <asp:Label ID="LblAccountNo" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">&nbsp;Name</td>
            <td>
                <asp:Label ID="LblCompanyName" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
              <tr>
                  <td class="auto-style4">Email</td>
                  <td>
                      <asp:Label ID="LblEmail" runat="server" Text="Label"></asp:Label>
                  </td>
                  <td>&nbsp;</td>
              </tr>
              <tr>
                  <td class="auto-style4">&nbsp;</td>
                  <td>&nbsp;</td>
                  <td>&nbsp;</td>
              </tr>
        <tr>
            <td class="auto-style4">From Account</td>
            <td>
                 <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Type" DataValueField="Account_Type_Id">
                 </asp:DropDownList>
                 <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:bankDBConnectionString1 %>" SelectCommand="SELECT AccountType.Type, Account.Account_Type_Id FROM Account INNER JOIN AccountType ON Account.Account_Type_Id = AccountType.Account_Type_Id WHERE (Account.Client_Id = @Client_Id)">
                     <SelectParameters>
                         <asp:SessionParameter Name="Client_Id" SessionField="s_c_id" Type="Int32" />
                     </SelectParameters>
                 </asp:SqlDataSource>
             
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td>
                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Note : Paying From Saving Account; 2% fee will be deducted"></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">Submit date</td>
            <td>
                <asp:Label ID="LblDate" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">Amount</td>
            <td>
                <asp:TextBox ID="TxtAmount" runat="server"  onkeypress="return isNumberKey(event)" AutoComplete ="off" MaxLength="10"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
              <tr>
                  <td class="auto-style4">&nbsp;</td>
                  <td>&nbsp;</td>
                  <td>&nbsp;</td>
              </tr>
              <tr>
                  <td class="auto-style4">&nbsp;</td>
                  <td>
                      &nbsp;</td>
                  <td>&nbsp;</td>
              </tr>
    </table>
    <hr />
    <table class="auto-style1">
        <tr>
            <td class="auto-style3">&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right" class="auto-style3">
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Icon Images/submitImage.jpg" OnClick="ImageButton2_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td align="right" class="auto-style3">&nbsp;</td>
            <td>
                <asp:Label ID="LblError" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
            </ContentTemplate>
    </asp:UpdatePanel>
   
   
</asp:Content>

