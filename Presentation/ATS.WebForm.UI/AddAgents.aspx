<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddAgents.aspx.cs" Inherits="ATS.WebForm.UI.AddAgents" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <fieldset>
        <legend>Add Agents</legend>
        <div class="control-group">
                <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName">User name</asp:Label>
            <div class="controls">
                <asp:TextBox runat="server" ID="UserName" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName"
                    CssClass="field-validation-error" ErrorMessage="The user name field is required." />
            </div>
        </div>
        <div class="control-group">
                <asp:Label ID="Label2" runat="server" AssociatedControlID="Email">Email address</asp:Label>
            <div class="controls">
                <asp:TextBox runat="server" ID="Email" TextMode="Email" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Email"
                    CssClass="field-validation-error" ErrorMessage="The email address field is required." />
            </div>
        </div>
        <div class="control-group">
            <div class="controls">
                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-success" OnClick="btnAdd_Click" />
            </div>
        </div>       
    </fieldset>
    <asp:gridview id="dv" Width="50%"  
      autogeneratecolumns="False"
      emptydatatext="No data available." 
      allowpaging="True" 
      runat="server" DataKeyNames="UserName">
        <Columns>
            <asp:BoundField DataField="UserName" HeaderText="Agent name" HeaderStyle-HorizontalAlign="Left" 
                InsertVisible="False" ReadOnly="True" SortExpression="UserName" />
            <asp:BoundField DataField="RoleName" HeaderText="Role" 
                SortExpression="RoleName" />
        </Columns>
    </asp:gridview>
    <div class="control-group">
        <div class="controls">
            <asp:Button ID="btnSubmitPrev" runat="server" Text="Previous" CssClass="btn btn-success" OnClick="btnSubmitPrev_Click" CausesValidation="false"  />
            <asp:Button ID="btnSubmit" runat="server" Text="Next" CssClass="btn btn-success" OnClick="btnSubmit_Click" CausesValidation="false" />
        </div>
    </div>

</asp:Content>