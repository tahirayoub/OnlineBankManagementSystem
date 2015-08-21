<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterPage.master" AutoEventWireup="true" CodeFile="ClientStatement.aspx.cs" Inherits="Client_ClientStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style3 {
            height: 21px;
            width: 345px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

         
          
     
        
            <table class="auto-style1">
                <tr>
                    <td class="auto-style3" style="color: #800000">Statement</td>
                    <td class="auto-style2" style="color: #800000">&nbsp;</td>
                    <td class="auto-style2" style="color: #800000">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3" style="color: #800000">Please Select Account to display statement</td>
                    <td class="auto-style2" style="color: #800000">
                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Type" DataValueField="Account_No">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:bankDBConnectionString1 %>" SelectCommand="SELECT Account.Account_No, AccountType.Type FROM Account INNER JOIN AccountType ON Account.Account_Type_Id = AccountType.Account_Type_Id WHERE (Account.Client_Id = @Client_Id)">
                            <SelectParameters>
                                <asp:SessionParameter Name="Client_Id" SessionField="c_id" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td class="auto-style2" style="color: #800000">&nbsp;</td>
                </tr>
            </table>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2" style="color: #800000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                    <td class="auto-style2" style="color: #800000">
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Icon Images/submitImage.jpg" OnClick="ImageButton3_Click" />
                    </td>
                    <td class="auto-style2" style="color: #800000">&nbsp;</td>
                </tr>
            </table>
            <p>
                <asp:Label ID="LblError" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Icon Images/Export.png" OnClick="ImageButton4_Click" style="text-align: center" Visible="False" />
            </p>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2" style="color: #800000">&nbsp;</td>
                    <td class="auto-style2" style="color: #800000">&nbsp;</td>
                    <td class="auto-style2" style="color: #800000">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="GVClientBillList" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Visible="False" Width="768px" OnPreRender="GVClientBillList_PreRender">
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
                        </asp:GridView>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <hr />
            

             
         
          
     
        
         
</asp:Content>


