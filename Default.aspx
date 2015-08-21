<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>Online Banking system</h1>
            </hgroup>
            <p>
                This is online Banking System.
            </p>
        </div>
    </section>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <table class="auto-style1">
        <tr>
            <td>
                <p class="MsoNormal">
                    The goal of this project is to develop an online banking and investment system with graphical interface that allow bank to offer several types of services and products and allow the users to search, select and perform banking operations and investment<o:p></o:p></p>
            </td>
            <td>
                <asp:Image ID="Image2" runat="server" ImageUrl="~/BankHomeImages/3.jpg" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="MsoNormal">
                    The system should allow&nbsp;online operations and services&nbsp;in secure manner.
                </p>
            </td>
            <td>
                <asp:Image ID="Image3" runat="server" ImageUrl="~/BankHomeImages/antdvn.jpg" Width="247px" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>We offer Debit Card so that the clients can pay bills and can tranfer money from one account to another.</td>
            <td>
                <asp:Image ID="Image4" runat="server" ImageUrl="~/BankHomeImages/images (3).jpg" Width="247px" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>You can save your money in our system.</td>
            <td>
                <asp:Image ID="Image5" runat="server" ImageUrl="~/BankHomeImages/images (6).jpg" Width="247px" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>You can do investment to earn profit.</td>
            <td>
                <asp:Image ID="Image6" runat="server" ImageUrl="~/BankHomeImages/images.jpg" Width="247px" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
   
   
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .MsoNormal {
            text-align: left;
        }
    </style>
</asp:Content>
