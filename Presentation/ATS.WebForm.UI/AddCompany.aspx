<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AddCompany.aspx.cs" Inherits="ATS.WebForm.UI.AddCompany" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <fieldset>
        <legend>Add Company</legend>

        <div class="control-group">
            <asp:Label ID="lblName" runat="server" CssClass="control-label"  Text="Company Name" ></asp:Label>
            <div class="controls">
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" CssClass="badge badge-important"
                     ErrorMessage="The company name field is required." />
            </div>
        </div>

        <div class="control-group">
            <asp:Label ID="lblAddress" runat="server" CssClass="control-label" Text="Address" ></asp:Label>
            <div class="controls">
                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddress" CssClass="badge badge-important"
                     ErrorMessage="The address field is required." />
            </div>
        </div>
        <div class="control-group">
            <div class="controls">
                <asp:Button ID="btnSubmit" runat="server" Text="Next" CssClass="btn btn-success" OnClick="btnSubmit_Click" />
            </div>
        </div>
        
    </fieldset>

</asp:Content>
