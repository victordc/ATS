<%@ Page Title="Report" Language="C#" MasterPageFile="~/Webforms/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="ATS.Webforms.UI.Report" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="body">

    <asp:GridView ID="LeaveCategoryGridView" runat="server" AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered table-hover" AllowSorting="True" OnSorting="LeaveCategoryGridView_Sorting">
        <Columns>
            <asp:BoundField DataField="LeaveCategoryDesc" HeaderText="Leave Types" SortExpression="LeaveCategoryDesc" />
            <asp:TemplateField HeaderText="Leaves">
                <ItemTemplate>
                    <asp:GridView ID="LeaveItemsGridView" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-striped table-bordered table-hover"
                        DataSource='<%# Eval("LeavePlans") %>'>
                        <Columns>
                            <asp:BoundField DataField="LeavePlanId" HeaderText="LeavePlanId" SortExpression="LeavePlanId" />
                            <asp:BoundField DataField="PersonId" HeaderText="PersonId" SortExpression="PersonId" />
                            <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" DataFormatString='{0:MM/dd/yyyy}' />
                            <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" DataFormatString='{0:MM/dd/yyyy}' />
                            <asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration" />
                            <asp:CheckBoxField DataField="Admitted" HeaderText="Approved" SortExpression="Admitted" />
                        </Columns>
                    </asp:GridView>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
