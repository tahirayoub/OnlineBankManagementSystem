<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterPage.master" AutoEventWireup="true" CodeFile="ClientCreditCardRequest.aspx.cs" Inherits="Client_ClientCreditCardRequest" %>

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
            width: 411px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
    <table class="auto-style1">
        <tr>
            <td class="auto-style3">Client Credit Card Request</td>
            <td style="text-align: right">
                &nbsp;</td>
            <td style="text-align: right">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">Description</td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Height="48px" MaxLength="250" TextMode="MultiLine" Width="172px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">&nbsp;</td>
            <td>250 Characters(Mandatory)</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">Submit date</td>
            <td>
                <asp:Label ID="LblDate" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <hr />
    <table class="auto-style1">
        <tr>
            <td class="auto-style3">&nbsp;</td>
            <td>&nbsp;</td>
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