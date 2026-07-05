<%@ Page Title="Member Profile" Language="C#" MasterPageFile="~/member_navigation.Master" AutoEventWireup="true" CodeBehind="member_profile.aspx.cs" Inherits="GroupAssignment.member_profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="css/member-profile.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="profile-wrapper">
        <div class="profile-container">
            <asp:Label ID="modifyErrorMsg" runat="server" Visible="false" CssClass="modify-error" />
            <asp:Label ID="modifySuccessMsg" runat="server" Visible="false" CssClass="modify-success" />
        
            <div class="member-profile-header">
                <h1>Edit Profile</h1>
                <p>Update your account information</p>
            </div>

            <div class="profile-update-form">
                <asp:Label ID="usernameLabel" runat="server" Text="Username:" AssociatedControlID="txtUsername"></asp:Label>
                <asp:TextBox ID="txtUsername" runat="server" placeholder="Enter a username"></asp:TextBox>
                <div class="validation-messages">
                    <asp:RequiredFieldValidator ID="reqUsername" runat="server" 
                        ControlToValidate="txtUsername" ErrorMessage="⚠ Username required" 
                        ForeColor="Red" CssClass="validator-message" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regexUsername" runat="server" 
                        ControlToValidate="txtUsername" ErrorMessage="⚠ 3-50 characters (letters, numbers, underscore)" 
                        ForeColor="Red" CssClass="validator-message" ValidationExpression="^[a-zA-Z0-9_]{3,50}$" 
                        Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="cvUsername" runat="server" 
                        ControlToValidate="txtUsername"
                        OnServerValidate="UsernameChange"
                        ErrorMessage="⚠ Username already exists"
                        ForeColor="Red" CssClass="validator-message" Display="Dynamic" />
                </div>
            </div>

            <div class="profile-update-form">
                <asp:Label ID="lblEmail" runat="server" Text="Email:" AssociatedControlID="txtEmail"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" placeholder="abc@example.com"></asp:TextBox>
                <div class="validation-messages">
                    <asp:RequiredFieldValidator ID="reqEmail" runat="server" 
                        ControlToValidate="txtEmail" ErrorMessage="⚠ Email required" 
                        ForeColor="Red" CssClass="validator-message" Display="Dynamic" SetFocusOnError="True" />
                    <asp:RegularExpressionValidator ID="regexEmail" runat="server" 
                        ControlToValidate="txtEmail" ErrorMessage="⚠ Valid email required" 
                        ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" 
                        ForeColor="Red" CssClass="validator-message" Display="Dynamic" />
                    <asp:CustomValidator ID="cvEmail" runat="server" 
                        ControlToValidate="txtEmail"
                        OnServerValidate="EmailChange"
                        ErrorMessage="⚠ Email already exists"
                        ForeColor="Red" CssClass="validator-message" Display="Dynamic" />
                </div>
            </div>

            <div class="profile-update-form">
                <asp:Label ID="confirmPWLabel1" runat="server" Text="New Password (Leave blank to keep current):" AssociatedControlID="confirmPWTxt1"></asp:Label>
                <asp:TextBox ID="confirmPWTxt1" runat="server" TextMode="Password" 
                    placeholder="At least 8 characters with mix of symbols, letters and numbers"></asp:TextBox>
                <div class="validation-messages">
                    <asp:RegularExpressionValidator ID="regexPassword" runat="server" 
                        ControlToValidate="confirmPWTxt1" ErrorMessage="⚠ Password must be at least 8 characters with letters, numbers, and symbols" 
                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$"
                        ForeColor="Red" CssClass="validator-message" Display="Dynamic" />
                </div>
            </div>

            <div class="profile-update-form">
                <asp:Label ID="confirmPWLabel2" runat="server" Text="Confirm New Password:" AssociatedControlID="confirmPWTxt2"></asp:Label>
                <asp:TextBox ID="confirmPWTxt2" runat="server" TextMode="Password" 
                    placeholder="Repeat new password"></asp:TextBox>
                <div class="validation-messages">
                    <asp:CompareValidator ID="comparePasswords" runat="server" 
                        ControlToValidate="confirmPWTxt2" ControlToCompare="confirmPWTxt1" 
                        ErrorMessage="⚠ Passwords do not match" ForeColor="Red" CssClass="validator-message" Display="Dynamic" />
                </div>
            </div>

            <div class="profile-update-form">
                <asp:Label ID="roleLabel" runat="server" Text="Role (Fixed): " AssociatedControlID="txtRole"></asp:Label>
                <asp:TextBox ID="txtRole" runat="server" ReadOnly="true"></asp:TextBox>
            </div>
        
            <div class="profile-buttons">
                <asp:Button ID="updateProfileBtn" runat="server" Text="Update Profile" CssClass="update-profile-btn" OnClick="ProfileUpdateBtn" />
            </div>
            <div class="profile-buttons">
                <asp:Button ID="signoutProfileBtn" runat="server" Text="Sign Out" CssClass="signout-profile-btn" OnClick="ProfileSignoutBtn" />
            </div>
        </div>
    </div>
</asp:Content>