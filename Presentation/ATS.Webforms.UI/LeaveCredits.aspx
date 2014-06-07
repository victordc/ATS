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

    <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=ATSCEEntities" DefaultContainerName="ATSCEEntities" EnableFlattening="False" EntitySetName="LeaveCategories" Include="LeavePlans">
    </asp:EntityDataSource>

    <asp:GridView ID="CreditsGridView" runat="server" AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered table-hover">
        <Columns>
            <asp:BoundField DataField="LeaveCategoryId" HeaderText="LeaveCategoryId" SortExpression="LeaveCategoryId" ReadOnly="True" />
            <asp:BoundField DataField="LeaveCategoryDesc" HeaderText="LeaveCategoryDesc" SortExpression="LeaveCategoryDesc" />
            <asp:TemplateField HeaderText="Leaves">
                <ItemTemplate>
                    <asp:GridView ID="LeaveItemsGridView" runat="server" 
                        CssClass="table table-striped table-bordered table-hover"
                        DataSource='<%# Eval("LeavePlans") %>'>
                    </asp:GridView>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
