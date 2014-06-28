<%@ Page Title="Report" Language="C#" MasterPageFile="~/Webforms/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="ATS.Webforms.UI.Report" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="body">
    <asp:GridView ID="LeaveCategoryGridView" runat="server" AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered table-hover" >
        <Columns>
            <asp:BoundField DataField="LeavePlanId" HeaderText="LeavePlanId" SortExpression="LeavePlanId" />
            <asp:BoundField DataField="LeaveType" HeaderText="Leave Types" SortExpression="LeaveType" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration" />
            <asp:CheckBoxField DataField="Admitted" HeaderText="Approved" SortExpression="Admitted" />
        </Columns>
    </asp:GridView>
    <asp:Label runat="server" ID="ReportLabel" Text=""></asp:Label>
</asp:Content>
