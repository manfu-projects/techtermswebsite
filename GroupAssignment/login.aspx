<%@ Page Title="" Language="C#" MasterPageFile="~/guest_navigation.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="GroupAssignment.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="login-wrapper">
        <div class="login-container">
            <asp:Label ID="lblErrorMessage" runat="server" Visible="false" CssClass="error-message" />
            <div class="login-header">
                <img src="#" />
                <h1>Welcome Back!</h1>
                <p>Log in to access your study list & track your progress.</p>
            </div>

            <div class="form-group">
                <asp:Label ID="login_username" runat="server" Text="Username:"></asp:Label>
                <asp:TextBox ID="txtUsername" runat="server" placeholder="Enter username"></asp:TextBox>
                <span class="validation-messages">
                    <asp:RequiredFieldValidator ID="reqUsernameLogin" runat="server" 
                    ControlToValidate="txtUsername" ErrorMessage="⚠ Username required." 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </span>
            </div>
            <div class="form-group">
                <asp:Label ID="login_password" runat="server" Text="Password:"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" placeholder="Enter password" TextMode="Password"></asp:TextBox>
                <span class="validation-messages">
                    <asp:RequiredFieldValidator ID="reqPasswordLogin" runat="server" 
                    ControlToValidate="txtPassword" ErrorMessage="⚠ Password required." 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </span>
            </div>
            <br />
            <div class="sign-in">
                <asp:Button ID="Button1" runat="server" Text="Log In" CssClass="btn-login" OnClick="Button1_Click" />
            </div>
            <div class="account-register">
                <p>Don't have an account? <a href="signup.aspx"><b>Register</b></a></p>
            </div>
        </div>
    </div>
</asp:Content>
