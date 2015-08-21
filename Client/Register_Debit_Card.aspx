<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register_Debit_Card.aspx.cs" Inherits="Client_Register_Debit_Card" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            height: 143px;
        }
        .auto-style2 {
            width: 222px;
            height: 21px;
        }
        .auto-style4 {
            height: 129px;
        }
        .auto-style5 {
            width: 89px;
            height: 129px;
        }
        .auto-style6 {
            width: 118px;
            height: 129px;
        }
        .auto-style7 {
            width: 103px;
            height: 129px;
            float: right;
        }
        .auto-style8 {
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
            <td style="color: #FF0000; font-size: medium;">Debit Card</td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
    </table>
    <p>
   
&nbsp;
        <asp:Label ID="LabelClient" runat="server" ForeColor="#CC0000" Text="Client"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp;
        <asp:Label ID="LblClientId" runat="server" Text="Label"></asp:Label>
    </p>
    <table class="auto-style1">
        <tr>
            <td class="auto-style5" style="background-color: #FFFFFF; background-image: url('../Img/s.JPG');">
                <asp:Image ID="Image1" runat="server" Height="137px" ImageUrl="~/Images/images.jpg" style="text-align: center" Width="103px" />
&nbsp;</td>
            <td class="auto-style6" style="background-color: #FFFFFF; background-image: url('../Img/s.JPG');">
                <asp:Label ID="Label1" runat="server" Text="Debit Card" Font-Bold="True" Font-Size="Large" ForeColor="Red"></asp:Label>
            </td>
            <td class="auto-style4" style="background-color: #000000; background-image: url('../Img/cardImage.JPG'); color: #000000;">
                <img class="auto-style7" src="../Img/world%20image.jpg" /></td>
        </tr>
        <tr>
            <td class="auto-style2" colspan="2" style="background-color: #FFFFFF; background-image: url('../Img/cardImage.JPG'); color: #00FF00;">&nbsp;</td>
            <td style="background-color: #FFFFFF; background-image: url('../Img/cardImage.JPG'); color: #00FF00;" class="auto-style8">Debit Card number :
                <asp:Label ID="LblTransiId" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="Red" Text="Label"></asp:Label>
&nbsp;
                <asp:Label ID="LblDebitCard" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="#FF3300" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style2" colspan="2" style="background-color: #FFFFFF; background-image: url('../Img/cardImage.JPG');">&nbsp;</td>
            <td style="background-color: #FFFFFF; background-image: url('../Img/cardImage.JPG'); color: #00FF00;" class="auto-style8">Expiery Date : <asp:Label ID="LblIssueDate" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#CC0000" Text="Label"></asp:Label>
&nbsp; Issue Date :
                <asp:Label ID="LblExpieryDate" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Red" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style2" colspan="2" style="background-color: #FFFFFF; background-image: url('../Img/cardImage.JPG');">&nbsp;</td>
            <td style="background-color: #FFFFFF; background-image: url('../Img/cardImage.JPG'); color: #00FF00;" class="auto-style8">&nbsp;</td>
        </tr>
    </table>
     </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

