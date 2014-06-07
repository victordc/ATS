<%@ Page Title="Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="ATS.Webforms.UI.Report" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="body">

    <asp:GridView ID="LeaveCategoryGridView" runat="server" AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered table-hover">
        <Columns>
            <asp:BoundField DataField="LeaveCategoryDesc" HeaderText="Leave Types" SortExpression="LeaveCategoryDesc" />
            <asp:TemplateField HeaderText="Leaves">
                <ItemTemplate>
                    <asp:GridView ID="LeaveItemsGridView" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-striped table-bordered table-hover"
                        DataSource='<%# Eval("LeavePlans") %>'>
                        <Columns>
                            <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" DataFormatString='{0:MM/dd/yyyy}' />
                            <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" DataFormatString='{0:MM/dd/yyyy}' />
                            <asp:BoundField DataField="Admitted" HeaderText="Approved" SortExpression="Admitted" />
                        </Columns>
                    </asp:GridView>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
