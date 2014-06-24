<%@ Page Language="C#" MasterPageFile="~/Webforms/Site.Master" AutoEventWireup="true" CodeBehind="LeaveCredits.aspx.cs" Inherits="ATS.Webforms.UI.LeaveCredits" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="body">
    <asp:GridView ID="CodeGridView" runat="server" AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered table-hover" >
        <Columns>
            <asp:BoundField DataField="CodeTableId" HeaderText="CodeTable Id" ReadOnly="True" SortExpression="CodeTableId" />
            <asp:BoundField DataField="Code" HeaderText="LeaveType" SortExpression="Code" />
            <asp:BoundField DataField="CodeDesc" HeaderText="Eligiblity" SortExpression="CodeDesc" />
        </Columns>
    </asp:GridView>
    <asp:GridView ID="CreditsGridView" runat="server" AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered table-hover">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="LeavePlanId" HeaderText="Record Id" SortExpression="LeavePlanId" ReadOnly="True" />
            <asp:BoundField DataField="LeaveType" HeaderText="LeaveType" SortExpression="LeaveType" />
            <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
            <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
            <asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration" />
            <asp:BoundField DataField="Credit" HeaderText="Remaining Balance" SortExpression="Credit" />
        </Columns>
    </asp:GridView>
</asp:Content>
