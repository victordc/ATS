<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ATS.WebForm.UI._Default" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <p>
    <asp:Button ID="btnSubmit" runat="server" Text="Start setup company" CssClass="btn btn-success" OnClick="btnSubmit_Click" />
    </p>
    <asp:gridview id="dv" Width="100%"  
      autogeneratecolumns="False"
      emptydatatext="No data available." 
      allowpaging="false" 
      runat="server" DataKeyNames="UserName">
        <Columns>
            <asp:BoundField DataField="UserName" HeaderText="Agent name" HeaderStyle-HorizontalAlign="Left" 
                InsertVisible="False" ReadOnly="True" SortExpression="UserName" />
            <asp:BoundField DataField="RoleName" HeaderText="Role" 
                SortExpression="RoleName" />
        </Columns>
    </asp:gridview>
</asp:Content>
