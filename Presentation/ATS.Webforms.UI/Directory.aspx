<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Directory.aspx.cs" Inherits="ATS.Webforms.UI.Directory" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="body">
    <asp:GridView ID="PersonGridView" runat="server"
                CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" >
        <Columns>
            <asp:BoundField DataField="PersonId" HeaderText="PersonId" ReadOnly="True" SortExpression="PersonId" />
            <asp:BoundField DataField="PersonName" HeaderText="PersonName" SortExpression="PersonName" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
            <asp:BoundField DataField="HomeAddress" HeaderText="HomeAddress" SortExpression="HomeAddress" />
        </Columns>
    </asp:GridView>
    </asp:Content>
