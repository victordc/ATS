<%@ Page Language="C#" MasterPageFile="~/Webforms/Site.Master" AutoEventWireup="true" CodeBehind="TeamReport.aspx.cs" Inherits="ATS.Webforms.UI.TeamReport" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="body">
    <asp:GridView ID="LeavesGridView" runat="server"
                CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" >
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Staff Name" SortExpression="Name" />
            <asp:BoundField DataField="LeaveType" HeaderText="Leave Types" SortExpression="LeaveType" />
            <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration" />
            <asp:CheckBoxField DataField="Admitted" HeaderText="Approved" SortExpression="Admitted" />
        </Columns>
    </asp:GridView>
    <asp:Label runat="server" ID="TeamReportLabel" AssociatedControlID="TeamReportLabel"></asp:Label>
</asp:Content>
