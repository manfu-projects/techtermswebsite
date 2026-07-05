<%@ Page Title="" Language="C#" MasterPageFile="~/guest_navigation.Master" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="GroupAssignment.signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="signup-wrapper">
        <div class="signup-container">
            <asp:Label ID="lblErrorMessage" runat="server" Visible="false" CssClass="error-message" />
            <asp:Label ID="lblSuccessMessage" runat="server" Visible="false" CssClass="success-message" />
            <div class="signup-header">
                <img src="#" />
                <h1>Create Account</h1>
                <p>Join Tech Terms to save flashcards and track your progress!</p>
            </div>

            <div class="form-group">
                <asp:Label ID="signup_username" runat="server" Text="Username:"></asp:Label>
                <asp:TextBox ID="txtUsername" runat="server" placeholder="Enter a username"></asp:TextBox>
                <span class="validation-messages">
                    <asp:RequiredFieldValidator ID="reqUsernameReg" runat="server" 
                        ControlToValidate="txtUsername" ErrorMessage="⚠ Username required." 
                        ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regexUsername" runat="server" 
                        ControlToValidate="txtUsername" ErrorMessage="⚠ Username must be 3-50 characters (letters, numbers, underscore)" 
                        ForeColor="Red" ValidationExpression="^[a-zA-Z0-9_]{3,50}$" 
                        Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="UsernameExistsCV" runat="server" 
                        ControlToValidate="txtUsername"
                        OnServerValidate="UsernameExistsCV_Validation"
                        ErrorMessage="⚠ Username already exists. Please choose another."
                        ForeColor="Red" Display="Dynamic" />
                </span>
            </div>

            <div class="form-group">
                <asp:Label ID="signup_email" runat="server" Text="Email:"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" placeholder="abc@example.com"></asp:TextBox>
                <span class="validation-messages">
                    <asp:RequiredFieldValidator ID="reqEmail" runat="server" 
                        ControlToValidate="txtEmail" ErrorMessage="⚠ Email required." 
                        ForeColor="Red" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="regexEmail" runat="server" 
                        ControlToValidate="txtEmail" ErrorMessage="⚠ Valid email required." 
                        ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" 
                        ForeColor="Red" Display="Dynamic" />
                    <asp:CustomValidator ID="EmailCVExists" runat="server" 
                        ControlToValidate="txtEmail"
                        OnServerValidate="EmailExistsCV_Validation"
                        ErrorMessage="⚠ Email already exists. Please choose another."
                        ForeColor="Red" Display="Dynamic" />
                </span>
            </div>

            <div class="form-group">
                <asp:Label ID="signup_password" runat="server" Text="Password:"></asp:Label>
                <asp:TextBox ID="txtPassword1" runat="server" TextMode="Password" 
                    placeholder="At least 8 characters with mix of symbols, letters and numbers"></asp:TextBox>
                <span class="validation-messages">
                    <asp:RequiredFieldValidator ID="reqPassword" runat="server" 
                        ControlToValidate="txtPassword1" ErrorMessage="⚠ Password required." 
                        ForeColor="Red" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="regexPassword" runat="server" 
                        ControlToValidate="txtPassword1" ErrorMessage="⚠ Password must be at least 8 characters with letters, numbers, and symbols" 
                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$"
                        ForeColor="Red" Display="Dynamic" />
                </span>
            </div>

            <div class="form-group">
                <asp:Label ID="confirm_password" runat="server" Text="Confirm Password:"></asp:Label>
                <asp:TextBox ID="txtPassword2" runat="server" TextMode="Password" 
                    placeholder="Repeat password"></asp:TextBox>
                <span class="validation-messages">
                    <asp:RequiredFieldValidator ID="reqConfirmPassword" runat="server" 
                        ControlToValidate="txtPassword2" ErrorMessage="⚠ Please confirm your password." 
                        ForeColor="Red" Display="Dynamic" />
                    <asp:CompareValidator ID="comparePasswords" runat="server" 
                        ControlToValidate="txtPassword2" ControlToCompare="txtPassword1" 
                        ErrorMessage="⚠ Passwords do not match." ForeColor="Red" Display="Dynamic" />
                </span>
            </div>
            <br />
            <div class="create-account">
                <asp:Button ID="Button1" runat="server" Text="Create Account" CssClass="btn-create" OnClick="Button1_Click" />
            </div>

            <div class="account-log-in">
                <p>Already have an account? <a href="login.aspx"><b>Log In</b></a></p>
            </div>
        </div>
    </div>
</asp:Content>