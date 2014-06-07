<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LeaveCredits.aspx.cs" Inherits="ATS.Webforms.UI.LeaveCredits" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="body">

    <asp:GridView ID="CodeGridView" runat="server" AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered table-hover" >
        <Columns>
            <asp:BoundField DataField="CodeTableId" HeaderText="CodeTableId" ReadOnly="True" SortExpression="CodeTableId" />
            <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
            <asp:BoundField DataField="CodeDesc" HeaderText="CodeDesc" SortExpression="CodeDesc" />
        </Columns>
    </asp:GridView>

    <asp:GridView ID="CreditsGridView" runat="server" AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered table-hover">
        <Columns>
            <asp:BoundField DataField="LeaveCategoryId" HeaderText="LeaveCategoryId" SortExpression="LeaveCategoryId" ReadOnly="True" />
            <asp:BoundField DataField="LeaveCategoryDesc" HeaderText="LeaveCategoryDesc" SortExpression="LeaveCategoryDesc" />
        </Columns>
    </asp:GridView>

    <asp:GridView ID="LeavesGridView" runat="server" AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered table-hover">
        <Columns>
            <asp:BoundField DataField="LeavePlanId" HeaderText="LeavePlan Id" SortExpression="LeavePlanId" />
            <asp:BoundField DataField="PersonId" HeaderText="PersonId" Visible="False" />
            <asp:TemplateField HeaderText="Person" SortExpression="PersonId">
                <ItemTemplate><%#Eval("Person.PersonName")%></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="LeaveCategoryId" HeaderText="LeaveCategoryId" SortExpression="LeaveCategoryId" Visible="False" />
            <asp:TemplateField HeaderText="Leave Category" SortExpression="LeaveCategoryId">
                <ItemTemplate><%#Eval("LeaveCategory.LeaveCategoryDesc")%></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="StartDate" HeaderText="Start Date" SortExpression="StartDate" DataFormatString='{0:d}' />
            <asp:BoundField DataField="EndDate" HeaderText="End Date" SortExpression="EndDate" DataFormatString='{0:d}' />
            <asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration" />
            <asp:CheckBoxField DataField="Admitted" HeaderText="Admitted" SortExpression="Admitted" />
        </Columns>
    </asp:GridView>

</asp:Content>
