﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeamReport.aspx.cs" Inherits="ATS.Webforms.UI.TeamReport" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="body">
    <asp:GridView ID="LeavesGridView" runat="server"
                CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" >
        <Columns>
            <asp:BoundField DataField="LeavePlanId" HeaderText="LeavePlanId" ReadOnly="True" SortExpression="LeavePlanId" />
            <asp:BoundField DataField="PersonId" HeaderText="PersonId" SortExpression="PersonId" />
            <asp:BoundField DataField="LeaveCategoryId" HeaderText="LeaveCategoryId" SortExpression="LeaveCategoryId" />
            <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
            <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
            <asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration" />
            <asp:CheckBoxField DataField="Admitted" HeaderText="Admitted" SortExpression="Admitted" />
        </Columns>
    </asp:GridView>
    </asp:Content>
