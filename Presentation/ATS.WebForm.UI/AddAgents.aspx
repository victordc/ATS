<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddAgents.aspx.cs" Inherits="ATS.WebForm.UI.AddAgents" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <fieldset>
        <legend>Add Agents</legend>
        <div class="control-group">
                <asp:Label ID="Label1" runat="server" CssClass="control-label" AssociatedControlID="UserName" Text="User name"></asp:Label>
            <div class="controls">
                <asp:TextBox runat="server" ID="UserName" />
                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="UserName" CssClass="badge badge-important"
                     ErrorMessage="User name already exists." OnServerValidate="ServerValidation"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName" 
                    CssClass="badge badge-important" ErrorMessage="The user name field is required." />
            </div>
        </div>
        <div class="control-group">
                <asp:Label ID="Label2" CssClass="control-label" runat="server" AssociatedControlID="Email" Text="Email address"></asp:Label>
            <div class="controls">
                <asp:TextBox runat="server" ID="Email" TextMode="Email" />
                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" CssClass="badge badge-important"
                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                    ControlToValidate="Email" ErrorMessage="Invalid Email Format."></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Email"
                    CssClass="badge badge-important" ErrorMessage="The email address field is required." />
            </div>
        </div>
        <div class="control-group">
            <div class="controls">
                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
            </div>
        </div>       
    </fieldset>
    <div class="control-group">
        <div class="controls">
            <asp:gridview id="dv" Width="60%"  
              autogeneratecolumns="False"
              emptydatatext="No data available." 
              allowpaging="True" 
              runat="server" DataKeyNames="UserName"
              CssClass="table table-striped table-bordered table-hover control" >
                <Columns>
                    <asp:BoundField DataField="UserName" HeaderText="Supervisor name" HeaderStyle-HorizontalAlign="Left" 
                        InsertVisible="False" ReadOnly="True" SortExpression="UserName" />
                    <asp:BoundField DataField="RoleName" HeaderText="Role" 
                        SortExpression="RoleName" />
                    <asp:BoundField DataField="Email" HeaderText="Email" 
                        SortExpression="Email" />
                </Columns>
            </asp:gridview>
        </div>
    </div>
    <div class="control-group">
        <div class="controls">
            <asp:Button ID="btnSubmitPrev" runat="server" Text="Previous" CssClass="btn" OnClick="btnSubmitPrev_Click" CausesValidation="false"  />
            <asp:Button ID="btnSubmit" runat="server" Text="Next" CssClass="btn btn-success" OnClick="btnSubmit_Click" CausesValidation="false" />
        </div>
    </div>

</asp:Content>