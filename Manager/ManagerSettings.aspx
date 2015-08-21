<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMenu.master" AutoEventWireup="true" CodeFile="ManagerSettings.aspx.cs" Inherits="Manager_ManagerSettings" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table class="auto-style1">
        <tr>
            <td style="color: #FF0000; font-size: medium;">Please Update Below Information</td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
    </table>
     <table class="auto-style1">
        <tr>
            <td style="color: #FF0000" class="auto-style3">
             
                Please Enter the Old Password</td>
            <td style="color: #FF0000" class="auto-style3">
             
                 &nbsp;<asp:TextBox ID="TxtOldPwd" runat="server" AutoComplete="off" MaxLength="7" TextMode="Password"></asp:TextBox>

            </td>
        </tr>
         <tr>
             <td class="auto-style3" style="color: #808000">
                 <asp:Label ID="Lblpwd" runat="server" Text="Label" Visible="False"></asp:Label>
             </td>
             <td class="auto-style3" style="color: #FF0000">
                 <asp:ImageButton ID="ImgSubmit" runat="server" ImageUrl="~/Icon Images/submitImage.jpg" OnClick="ImgSubmit_Click" />
             </td>
         </tr>
         <tr>
             <td class="auto-style3" style="color: #808000">&nbsp;</td>
             <td class="auto-style3" style="color: #FF0000">
                 <asp:Label ID="LblMsgPwd" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
             </td>
         </tr>
         <tr>
             <td class="auto-style3" style="color: #808000">&nbsp;</td>
             <td class="auto-style3" style="color: #FF0000">
                 &nbsp;</td>
         </tr>
        <tr>
            <td style="color: #808000" class="auto-style3">&nbsp;</td>
            <td style="color: #FF0000" class="auto-style3">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style3">First Name</td>
            <td style="color: #FF0000" class="auto-style3">
                <asp:TextBox ID="TxtFirstName" runat="server" AutoComplete="off" MaxLength="15"   onkeypress="return alphaOnly(event)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style4">
             
                Last Name</td>
            <td style="color: #808000" class="auto-style4">
             
                <asp:TextBox ID="TxtLastName" runat="server" AutoComplete="off" MaxLength="15"  onkeypress="return alphaOnly(event)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color: #FF0000" class="auto-style3">
            </td>
            <td style="color: #800000" class="auto-style3">&nbsp;</td>
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
             
                Email</td>
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
             
                <asp:TextBox ID="TxtAddress" AutoComplete="off" runat="server" Height="22px" MaxLength="250" Width="291px"></asp:TextBox>
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
             
                &nbsp;</td>
            <td style="color: #808000" class="auto-style5">
             
                &nbsp;</td>
        </tr>
        </table>
    <p style="color: #FF0000; font-size: medium;">
        Security</p>
    <table class="auto-style1">
        <tr>
            <td style="color: #808000" class="auto-style11">
             
                Password</td>
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
             
                Answer 1</td>
            <td style="color: #808000" class="auto-style5">
             
                <asp:TextBox ID="TxtA1" runat="server" AutoComplete="off" height="27px" width="315px" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style11">
             
                Security Question 2</td>
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
             
                Answer 2</td>
            <td style="color: #808000" class="auto-style5">
             
                <asp:TextBox ID="TxtA2" runat="server" AutoComplete="off"  height="27px" width="315px" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color: #808000" class="auto-style11">
             
                Security Question 3</td>
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
             
                Answer 3</td>
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
             
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Icon Images/Update.jpg" OnClick="ImageButton1_Click" Visible="False" />
            </td>
        </tr>
    </table>
        
             
               </ContentTemplate>
    </asp:UpdatePanel>    
      
</asp:Content>

