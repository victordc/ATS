<%@ Page Language="C#" MasterPageFile="~/Webforms/Site.Master" AutoEventWireup="true" CodeBehind="LeaveCredits.aspx.cs" Inherits="ATS.Webforms.UI.LeaveCredits" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="body">
    <h3>Leave Entitlement</h3>
    <asp:GridView ID="LeaveCategoryGridView" runat="server" AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered table-hover" >
        <Columns>
            <asp:BoundField DataField="LeaveCategoryDesc" HeaderText="Leave Type" SortExpression="LeaveCategoryDesc" />
            <asp:BoundField DataField="LeaveCategoryLimit" HeaderText="Yearly Entitlement" SortExpression="LeaveCategoryLimit" />
        </Columns>
    </asp:GridView>
    <h3>Leave History</h3>
    <asp:GridView ID="CreditsGridView" runat="server" AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered table-hover">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="LeavePlanId" HeaderText="Record Id" SortExpression="LeavePlanId" ReadOnly="True" />
            <asp:BoundField DataField="LeaveType" HeaderText="LeaveType" SortExpression="LeaveType" />
            <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration" />
            <asp:BoundField DataField="Credit" HeaderText="Remaining Balance" SortExpression="Credit" />
        </Columns>
    </asp:GridView>
    <asp:Label runat="server" ID="AvailmentLabel" AssociatedControlID="AvailmentLabel"></asp:Label>
</asp:Content>
