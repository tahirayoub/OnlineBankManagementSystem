<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ClientCheckPassword.aspx.cs" Inherits="Client_ClientCheckPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
<script type="text/jscript">

    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;


    }

    function checkLength() {
        var textbox = document.getElementById("TxtDebitCard");
        if (textbox.value.length < 7) {
            alert("make sure the input 7 digits numbers ");
        }
        else {

        }
    }
</script>

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 222px;
        }
        .auto-style3 {
            width: 222px;
            height: 26px;
        }
        .auto-style4 {
            height: 26px;
        }
        .auto-style5 {
            width: 222px;
            height: 45px;
        }
        .auto-style6 {
            height: 45px;
        }
        .auto-style7 {
            width: 222px;
            height: 52px;
        }
        .auto-style8 {
            height: 52px;
        }
    </style>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          
    <table class="auto-style1">
        <tr>
            <td style="color: #FF0000; font-size: xx-large; font-weight: bold">Client Please LogIn</td>
        </tr>
        </table>
    <hr />
    <p>
    </p>
    <table class="auto-style1">
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5">Please Enter the new password</td>
            <td class="auto-style6">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">Passoword</td>
            <td class="auto-style4">
                <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" MaxLength="8"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style7"></td>
            <td class="auto-style8">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>
                <asp:ImageButton ID="BtnSubmit" runat="server" ImageUrl="~/Icon Images/submitImage.jpg" OnClick="BtnSubmit_Click" style="height: 36px" />
&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>
                <asp:Label ID="LblError" runat="server" ForeColor="#CC0000" Text="Label" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>

       
               </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

