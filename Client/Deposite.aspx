<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterPage.master" AutoEventWireup="true" CodeFile="Deposite.aspx.cs" Inherits="Client_Deposite" %>

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
        .auto-style2 {
            height: 29px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    
             
    <table class="auto-style1">
        <tr>
            <td>Please enter Amount:</td>
        </tr>
        <tr>
            <td>*The Amount will be submitted to Staff he will validate and confirm it.</td>
        </tr>
    </table>
    <br />
    <hr />
    <br />
     <table class="auto-style1" style="width: 100%">
        <tr>
            <td class="auto-style2">Cheque Image</td>
            <td class="auto-style2">
                <asp:FileUpload ID="FileUpload1" runat="server" />


            </td>
        </tr>
        <tr>
            <td class="auto-style2">Amount</td>
            <td class="auto-style2">
              <asp:TextBox id="tbox1" runat="server" AutoComplete ="off"  onkeypress="return isNumberKey(event)" MaxLength="10" />


            </td>
        </tr>
        <tr>
            <td>Confirmed Amount</td>
            <td>
                 <asp:TextBox id="tbox2" runat="server" AutoComplete ="off" onkeypress="return isNumberKey(event)" MaxLength="10" />
             
            </td>
        </tr>
        <tr>
            <td>Account</td>
            <td>
                 <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Type" DataValueField="Account_No">
                 </asp:DropDownList>
                 <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:bankDBConnectionString1 %>" SelectCommand="SELECT Account.Account_No, AccountType.Type FROM Account INNER JOIN AccountType ON Account.Account_Type_Id = AccountType.Account_Type_Id WHERE (Account.Client_Id = @Client_Id)">
                     <SelectParameters>
                         <asp:SessionParameter Name="Client_Id" SessionField="c_id" Type="Int32" />
                     </SelectParameters>
                 </asp:SqlDataSource>
             
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
     
      

    </asp:Content>

