<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddStaffs.aspx.cs" Inherits="ATS.WebForm.UI.AddStaffs" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <fieldset>
        <legend>Add Staffs</legend>
        <div class="control-group">
                <asp:Label ID="Label1"  CssClass="control-label" runat="server" AssociatedControlID="UserName" >User name</asp:Label>
            <div class="controls">
                <asp:TextBox runat="server" ID="UserName" />
                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="UserName" CssClass="badge badge-important"
                     ErrorMessage="User name already exists." OnServerValidate="ServerValidation"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName"
                    CssClass="badge badge-important" ErrorMessage="The user name field is required." />
            </div>
        </div>
        <div class="control-group">
                <asp:Label ID="Label2"  CssClass="control-label" runat="server" AssociatedControlID="Email">Email address</asp:Label>
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
            <asp:Label ID="Label6"  CssClass="control-label" runat="server" AssociatedControlID="Supervisor">Supervisor</asp:Label>
            <div class="controls">
                <asp:DropDownList ID="Supervisor" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Supervisor"
                        CssClass="badge badge-important" Display="Dynamic" ErrorMessage="The supervisor is required." />
            </div>
        </div>
        <div class="control-group">
            <asp:Label ID="Label5"  CssClass="control-label" runat="server">Agent</asp:Label>
            <div class="controls">
                <asp:DropDownList ID="Agent" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Agent"
                        CssClass="badge badge-important" Display="Dynamic" ErrorMessage="The agent is required." />
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
            <asp:gridview id="dv" Width="70%"  
              autogeneratecolumns="False"
              emptydatatext="No data available." 
              allowpaging="True" 
              runat="server" DataKeyNames="UserName"
              CssClass ="table table-striped table-bordered table-hover" >
                <Columns>
                    <asp:BoundField DataField="UserName" HeaderText="Agent name" HeaderStyle-HorizontalAlign="Left" 
                        InsertVisible="False" ReadOnly="True" SortExpression="UserName" />
                    <asp:BoundField DataField="RoleName" HeaderText="Role" 
                        SortExpression="RoleName" />
                    <asp:BoundField DataField="SupervisorName" HeaderText="Supervisor" 
                        SortExpression="SupervisorName" />
                    <asp:BoundField DataField="AgentName" HeaderText="Agent" 
                        SortExpression="AgentName" />
                    <asp:BoundField DataField="Email" HeaderText="Email" 
                        SortExpression="Email" />
                </Columns>
            </asp:gridview>
        </div>
    </div> 
    <div class="control-group">
        <div class="controls">
            <asp:Button ID="btnSubmitPrev" runat="server" Text="Previous" CssClass="btn" OnClick="btnSubmitPrev_Click" CausesValidation="false"  />
            <asp:Button ID="btnSubmit" runat="server" Text="Finish" CssClass="btn btn-success" OnClick="btnSubmit_Click" CausesValidation="false" />
        </div>
    </div>

</asp:Content>

