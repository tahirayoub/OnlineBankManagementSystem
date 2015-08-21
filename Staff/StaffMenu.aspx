<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMenu.master" AutoEventWireup="true" CodeFile="StaffMenu.aspx.cs" Inherits="Staff_StaffMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <style type="text/css">
        .auto-style3 {
            width: 574px;
        }
    </style>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GVacct" runat="server" AutoGenerateColumns="false" AutoGenerateSelectButton="True" CellPadding="4" ForeColor="#333333" GridLines="None" Height="200px" style="text-align: center" Width="636px" OnSelectedIndexChanged="GVacct_SelectedIndexChanged" AllowPaging="True" PageSize="5" OnPageIndexChanging="GVacct_PageIndexChanging">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
                <Columns>
                    <asp:TemplateField HeaderText="Deposite Id">
                        <ItemTemplate>
                            <asp:Label  ID="Id" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Images">
                        <ItemTemplate>
                            <asp:Image ID="Img" runat="server" Height="100" ImageUrl=' <%#Eval("cheque") %>' Width="100" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

             
            <table class="auto-style1">
               
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="ImageButton2" runat="server" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>

  
          
     
            <br />
             <hr />
            <table class="auto-style1">
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="LblId" runat="server" Text="Label" Visible="False"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label6" runat="server" Text="Client Id"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblClientId" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label11" runat="server" Text="Amount"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtAmount" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label7" runat="server" Text="date"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblDate" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label8" runat="server" Text="Time"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblTime" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label9" runat="server" Text="Account Number"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblAccountNo" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label10" runat="server" Text="Status"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblStatus" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <hr />
            <table class="auto-style1">
                <tr>
                    <td>
                        <asp:ImageButton ID="ImgDelete" runat="server" height="64px" ImageUrl="~/Icon Images/DeleteButton.jpg" width="64px" OnClick="ImgDelete_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ImgUpdate" runat="server" ImageUrl="~/Icon Images/Update.jpg" OnClick="ImgUpdate0_Click" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
    <br /> 
        </ContentTemplate>
    </asp:UpdatePanel>
         

  
   
</asp:Content>

