<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="ATS.Webforms.UI.Report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="LeavePlanId" DataSourceID="LeavePlanDataSource" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="LeavePlanId" HeaderText="LeavePlanId" ReadOnly="True" SortExpression="LeavePlanId" />
                <asp:BoundField DataField="PersonId" HeaderText="PersonId" SortExpression="PersonId" />
                <asp:BoundField DataField="LeaveCategoryId" HeaderText="LeaveCategoryId" SortExpression="LeaveCategoryId" />
                <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
                <asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration" />
                <asp:CheckBoxField DataField="Admitted" HeaderText="Admitted" SortExpression="Admitted" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <asp:EntityDataSource ID="LeavePlanDataSource" runat="server" ConnectionString="name=ATSCEEntities" DefaultContainerName="ATSCEEntities" EnableFlattening="False" EntitySetName="LeavePlans">
        </asp:EntityDataSource>
    
    </div>
    </form>
</body>
</html>
