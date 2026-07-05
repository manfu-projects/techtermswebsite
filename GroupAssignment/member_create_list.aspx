<%@ Page Title="Create Study List" Language="C#" MasterPageFile="~/member_navigation.Master" AutoEventWireup="true" CodeBehind="member_create_list.aspx.cs" Inherits="GroupAssignment.member_create_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/study-list.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="study-list-creation">
        <div class="list-creation-header">
            <h2>Create New Study List</h2>
        
            <asp:Label ID="lblMessage" runat="server" CssClass="list-creation-success" Visible="false" />
            <asp:Label ID="lblError" runat="server" CssClass="list-creation-error" Visible="false" />
        </div>
        
        <div class="list-creation-form">
            <label for="txtListName">Study List Name:</label>
            <asp:TextBox ID="txtListName" runat="server" placeholder="Enter list name (e.g., Cybersecurity)" MaxLength="100"></asp:TextBox>
        </div>
        
        <asp:Button ID="btnCreate" runat="server" Text="Create Study List" CssClass="btn-create" OnClick="btnCreate_Click" />
    </div>
</asp:Content>