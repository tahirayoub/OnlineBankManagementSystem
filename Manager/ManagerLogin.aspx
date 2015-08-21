<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerView.master" AutoEventWireup="true" CodeFile="ManagerLogin.aspx.cs" Inherits="Manager_ManagerLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
<script type="text/jscript">

   
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
            <td style="color: #FF0000; font-size: xx-large; font-weight: bold">Manager Please LogIn</td>
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
            <td class="auto-style5">Manager UserName</td>
            <td class="auto-style6">
                <asp:TextBox ID="TxtUserName" runat="server"   AutoComplete="off"  MaxLength="25"></asp:TextBox>
            </td>
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
                <asp:HyperLink ID="hlink" runat="server" ForeColor="#0000CC" NavigateUrl="~/Manager/forgetPassword.aspx">Forget Password</asp:HyperLink>
            </td>
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


