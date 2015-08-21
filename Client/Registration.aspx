<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Client_Registration" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/jscript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function alphaOnly(event) {
            var key = event.keyCode;
            return ((key >= 97 && key <= 122) || key == 8 || (key >= 65 && key <= 90));
        };
</script>

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 267px;
            height: 41px;
        }
    .auto-style3 {
        height: 39px;
    }
    .auto-style4 {
        height: 35px;
    }
    .auto-style5 {
        height: 42px;
    }
    .auto-style6 {
        height: 45px;
    }
    .auto-style7 {
        height: 46px;
    }
    .auto-style8 {
        height: 51px;
    }
    .auto-style9 {
        height: 41px;
    }
    .auto-style10 {
        height: 41px;
    }
        .auto-style11 {
            height: 42px;
            width: 479px;
        }
        .auto-style12 {
            width: 479px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table class="auto-style1">
        <tr>
            <td style="color: #FF0000; font-size: medium;">Registration:</td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
    </table>
     <table class="auto-style1">
        <tr>
            <td style="color: #808000" class="auto-style3">Passport No<asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td style="color: #FF0000" class="auto-style3">
                <asp:TextBox ID="TxtPassportNo" runat="server" onkeypress="return isNumberKey(event)" AutoComplete="off" OnTextChanged="TxtPassportNo_TextChanged" MaxLength="7"></asp:TextBox>
&nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Check" />
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style3"></td>
            <td style="color: #FF0000" class="auto-style3">
                <asp:Label ID="LblMsg" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style3">&nbsp;</td>
            <td style="color: #FF0000" class="auto-style3">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style3">&nbsp;</td>
            <td style="color: #FF0000" class="auto-style3">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style3">First Name<asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td style="color: #FF0000" class="auto-style3">
                <asp:TextBox ID="TxtFirstName" runat="server" AutoComplete="off" MaxLength="15"  onkeypress="return alphaOnly(event)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style4">
             
                Last Name<asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red" Text="*" ></asp:Label>
            </td>
            <td style="color: #808000" class="auto-style4">
             
                <asp:TextBox ID="TxtLastName" runat="server" AutoComplete="off" MaxLength="15" onkeypress="return alphaOnly(event)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style3">
             
                Date Of Birth</td>
            <td style="color: #800000" class="auto-style3">
             
                 <fieldset style="width:331px">
    <legend>Select Date</legend>
    Year: <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True"
            onselectedindexchanged="ddlYear_SelectedIndexChanged" ></asp:DropDownList> 
           
    Month: <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True"
            onselectedindexchanged="ddlMonth_SelectedIndexChanged">
        </asp:DropDownList> 
       
    Day: <asp:DropDownList ID="ddlDay" runat="server">
        </asp:DropDownList>
    </fieldset>

            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style5">
             
                Occupation</td>
            <td style="color: #808000" class="auto-style5">
             
                <asp:TextBox ID="TxtOccupation" runat="server" AutoComplete="off" MaxLength="25" onkeypress="return alphaOnly(event)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style6">
             
                Designation</td>
            <td style="color: #808000" class="auto-style6">
             
                <asp:TextBox ID="TxtDesignation" runat="server" AutoComplete="off" MaxLength="25" onkeypress="return alphaOnly(event)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style6">
             
                Email<asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td style="color: #808000" class="auto-style6">
             
                <asp:TextBox ID="TxtEmail" runat="server" AutoComplete="off"  TextMode="Email" MaxLength="50" Height="19px" Width="220px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style7">
             
                Monthly Salary</td>
            <td style="color: #808000" class="auto-style7">
             
                <asp:TextBox ID="TxtSalary" AutoComplete="off" onkeypress="return isNumberKey(event)" runat="server" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style8">
             
                Address</td>
            <td style="color: #808000" class="auto-style8">
             
                <asp:TextBox ID="TxtAddress" AutoComplete="off" runat="server" Height="22px" MaxLength="250" Width="291px" onkeypress="return alphaOnly(event)"></asp:TextBox>
            </td>
        </tr>
        </table>
<p>
</p>
<table class="auto-style1">
        <tr>
            <td class="auto-style2" style="color: #808000">
             
                Country</td>
            <td style="color: #808000" class="auto-style9">
             
                <asp:TextBox ID="TxtCountry" runat="server"  AutoComplete="off" MaxLength ="15" onkeypress="return alphaOnly(event)"></asp:TextBox>
            </td>
            <td style="color: #808000" class="auto-style9">
             
                City</td>
            <td style="color: #808000" class="auto-style10">
             
                <asp:TextBox ID="TxtCity" runat="server" AutoComplete="off" MaxLength="15" onkeypress="return alphaOnly(event)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2" style="color: #808000">
             
                Postal Code</td>
            <td style="color: #808000" class="auto-style9">
             
                <asp:TextBox ID="TxtPostalCode" runat="server" AutoComplete="off" MaxLength="7"></asp:TextBox>
            </td>
            <td style="color: #808000" class="auto-style9" colspan="2">
             
                &nbsp;</td>
        </tr>
        </table>
<p>
</p>
<table class="auto-style1">
        <tr>
            <td style="color: #808000" class="auto-style6">
             
                Phone Number </td>
            <td style="color: #808000" class="auto-style6">
             
                <asp:TextBox ID="TxtPhoneNo" runat="server" onkeypress="return isNumberKey(event)" MaxLength="12"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style5">
             
                Client Current Status</td>
            <td style="color: #808000" class="auto-style5">
             
                <asp:DropDownList ID="TxtStatus" runat="server">
                    <asp:ListItem>International Student</asp:ListItem>
                    <asp:ListItem>Citizen</asp:ListItem>
                    <asp:ListItem>Refuge</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        </table>
    <p style="color: #FF0000; font-size: medium;">
        Security</p>
    <table class="auto-style1">
        <tr>
            <td style="color: #808000" class="auto-style11">
             
                Password<asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td style="color: #808000"  class="auto-style5">
             
                <asp:TextBox ID="TxtPassword" runat="server" Height="21px" TextMode="Password" Width="307px" MaxLength="8"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style11">
             
                &nbsp;</td>
            <td style="color: #808000" class="auto-style5">
             
                &nbsp;</td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style11">
             
                Security Question 1
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td style="color: #808000" class="auto-style5">
             
                <asp:DropDownList ID="TxtQ1" runat="server">
                    <asp:ListItem>What was your childhood nickname? </asp:ListItem>
                    <asp:ListItem>In what city did you meet your spouse/significant other?</asp:ListItem>
                    <asp:ListItem>What is the name of your favorite childhood friend? </asp:ListItem>
                    <asp:ListItem>What street did you live on in third grade?</asp:ListItem>
                    <asp:ListItem>What is the middle name of your oldest child?</asp:ListItem>
                    <asp:ListItem>What is your oldest sibling&#39;s middle name?</asp:ListItem>
                    <asp:ListItem>What school did you attend for sixth grade?</asp:ListItem>
                    <asp:ListItem>What is your oldest cousin&#39;s first and last name?</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style11">
             
                Answer 1<asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td style="color: #808000" class="auto-style5">
             
                <asp:TextBox ID="TxtA1" runat="server" AutoComplete="off" height="27px" width="315px" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style11">
             
                Security Question 2<asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td style="color: #808000" class="auto-style5">
             
                <asp:DropDownList ID="TxtQ2" runat="server">
                    <asp:ListItem>What is your oldest sibling&#39;s middle name?</asp:ListItem>
                    <asp:ListItem>What was your childhood nickname? </asp:ListItem>
                    <asp:ListItem>In what city did you meet your spouse/significant other?</asp:ListItem>
                    <asp:ListItem>What is the name of your favorite childhood friend? </asp:ListItem>
                    <asp:ListItem>What street did you live on in third grade?</asp:ListItem>
                    <asp:ListItem>What is the middle name of your oldest child?</asp:ListItem>
                    <asp:ListItem>What school did you attend for sixth grade?</asp:ListItem>
                    <asp:ListItem>What is your oldest cousin&#39;s first and last name?</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style11">
             
                Answer 2<asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td style="color: #808000" class="auto-style5">
             
                <asp:TextBox ID="TxtA2" runat="server" AutoComplete="off"  height="27px" width="315px" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style11">
             
                Security Question 3<asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td style="color: #808000" class="auto-style5">
             
                <asp:DropDownList ID="TxtQ3" runat="server">
                    <asp:ListItem>What school did you attend for sixth grade?</asp:ListItem>
                    <asp:ListItem>What was your childhood nickname? </asp:ListItem>
                    <asp:ListItem>In what city did you meet your spouse/significant other?</asp:ListItem>
                    <asp:ListItem>What is the name of your favorite childhood friend? </asp:ListItem>
                    <asp:ListItem>What street did you live on in third grade?</asp:ListItem>
                    <asp:ListItem>What is the middle name of your oldest child?</asp:ListItem>
                    <asp:ListItem>What is your oldest sibling&#39;s middle name?</asp:ListItem>
                    <asp:ListItem>What is your oldest cousin&#39;s first and last name?</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style11">
             
                Answer 3<asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td style="color: #808000" class="auto-style5">
             
                <asp:TextBox ID="TxtA3" runat="server" height="27px" AutoComplete="off" width="315px" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color: #808000; text-align: right;" class="auto-style11">
             
                <asp:Label ID="LblError" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
            </td>
            <td style="color: #808000" class="auto-style5">
             
                &nbsp;</td>
        </tr>
        <tr>
            <td style="color: #808000; text-align: right;" class="auto-style12">
             
                &nbsp;</td>
            <td style="color: #808000">
             
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Icon Images/submitImage.jpg" OnClick="ImageButton1_Click" />
            </td>
        </tr>
    </table>
               
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

