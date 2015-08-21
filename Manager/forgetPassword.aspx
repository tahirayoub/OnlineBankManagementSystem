<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerView.master" AutoEventWireup="true" CodeFile="forgetPassword.aspx.cs" Inherits="Manager_forgetPassword" %>

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
        .auto-style1 {
            width: 427px;
        }
        .auto-style2 {
            width: 341px;
        }
        .auto-style3 {
            width: 341px;
            height: 49px;
        }
        .auto-style4 {
            height: 27px;
        }
        .auto-style5 {
            height: 49px;
        }
        .auto-style6 {
            width: 341px;
            height: 21px;
        }
        .auto-style7 {
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
            <td style="color: #FF0000; font-size: xx-large; font-weight: bold">Manager Forget Password</td>
        </tr>
        </table>
    <hr />
    <p>
    </p>
    <table class="auto-style1" width =" 100%">
        <tr>
            <td colspan="2">
                <asp:Label ID="lbleml" runat="server" Enabled="False" Visible="False"></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">Enter Manager UserName</td>
            <td class="auto-style5">
                <asp:TextBox ID="TxtUserName" runat="server"    AutoComplete ="off" MaxLength="25"></asp:TextBox>
            </td>
            <td class="auto-style5">
            </td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style4">
                <asp:ImageButton ID="BtnSubmit" runat="server" ImageUrl="~/Icon Images/submitImage.jpg" OnClick="BtnSubmit_Click" Height="26px" />
            </td>
            <td class="auto-style4">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2"></td>
            <td class="auto-style8">
                <asp:Label ID="LblError" runat="server" ForeColor="#CC0000" Text="Label" Visible="False"></asp:Label>
            </td>
            <td class="auto-style8">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Label ID="LblQ1" runat="server" Text="Label" Visible="False"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="TxtA1" runat="server" AutoComplete ="off" Visible="False" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Label ID="LblQ2" runat="server" Text="Label" Visible="False"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="TxtA2" runat="server" AutoComplete ="off" Visible="False" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Label ID="LblQ3" runat="server" Text="Label" Visible="False"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="TxtA3" runat="server" AutoComplete ="off" Visible="False" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>
                <asp:ImageButton ID="BtnCheckAnswer" runat="server" ImageUrl="~/Icon Images/submitImage.jpg" OnClick="BtnCheckAnswer_Click" Height="26px" Visible="False" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style6">&nbsp;</td>
            <td class="auto-style7">
                <asp:Label ID="LblAnswerError" runat="server" ForeColor="#CC0000" Text="Label" Visible="False"></asp:Label>
            </td>
            <td class="auto-style7">
                </td>
        </tr>
        <tr>
            <td class="auto-style6">&nbsp;</td>
            <td class="auto-style7">
                &nbsp;</td>
            <td class="auto-style7">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style6">
                <asp:Label ID="LblPassword" runat="server" Text="Password" Visible="False"></asp:Label>
            </td>
            <td class="auto-style7">
                <asp:TextBox ID="TxtPassword" runat="server" Visible="False" TextMode="Password" MaxLength="8"></asp:TextBox>
            </td>
            <td class="auto-style7">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style6">&nbsp;</td>
            <td class="auto-style7">
                <asp:Label ID="LblMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
            </td>
            <td class="auto-style7">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style6">&nbsp;</td>
            <td class="auto-style7">
                <asp:ImageButton ID="BtnUpdate" runat="server" ImageUrl="~/Icon Images/Update.jpg" OnClick="BtnUpdate_Click" Height="65px" Visible="False" Width="69px" />
            </td>
            <td class="auto-style7">
                &nbsp;</td>
        </tr>
    </table>
                 </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

