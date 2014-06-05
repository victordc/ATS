<%@ Page Title="Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="ATS.Webforms.UI.Report" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="body">

    <asp:GridView ID="LeavesGridView" runat="server" AutoGenerateColumns="False" 
        CssClass="table table-striped table-bordered table-hover"
        AllowSorting="True" OnSorting="LeavesGridView_Sorting" >
        <Columns>
            <asp:BoundField DataField="LeavePlanId" HeaderText="LeavePlan Id" SortExpression="LeavePlanId" />
            <asp:BoundField DataField="PersonId" HeaderText="PersonId" Visible="False" />
            <asp:templatefield headertext="Person" SortExpression="PersonId"><itemtemplate><%#Eval("Person.PersonName")%></itemtemplate></asp:templatefield>
            <asp:BoundField DataField="LeaveCategoryId" HeaderText="LeaveCategoryId" SortExpression="LeaveCategoryId" Visible="False" />
            <asp:templatefield headertext="Leave Category" SortExpression="LeaveCategoryId"><itemtemplate><%#Eval("LeaveCategory.LeaveCategoryDesc")%></itemtemplate></asp:templatefield>
            <asp:BoundField DataField="StartDate" HeaderText="Start Date" SortExpression="StartDate" DataFormatString='{0:d}' />
            <asp:BoundField DataField="EndDate" HeaderText="End Date" SortExpression="EndDate" DataFormatString='{0:d}' />
            <asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration" />
            <asp:CheckBoxField DataField="Admitted" HeaderText="Admitted" SortExpression="Admitted" />
        </Columns>
    </asp:GridView>
    </asp:Content>
