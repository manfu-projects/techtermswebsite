<%@ Page Title="My Study Lists" Language="C#" MasterPageFile="~/member_navigation.Master" AutoEventWireup="true" CodeBehind="member_study_list.aspx.cs" Inherits="GroupAssignment.member_study_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/study-list.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="member-study-lists">
        <div class="study-lists-header">
            <h2>My Study Lists</h2>
            <a href="member_create_list.aspx" class="create-list-btn" onclick="showCreateListModal()">+ Create New List</a>
        </div>
        
        <div class="study-lists-grid">
            <asp:Repeater ID="StudyListsSect" runat="server" OnItemCommand="studyListsRpt">
                <ItemTemplate>
                    <div class="study-list-card">
                        <div class="study-list-header">
                            <h3><%# Eval("listName") %></h3>
                            <div class="list-actions">
                                <asp:Button ID="deleteListBtn" runat="server" 
                                    Text="🗑️" 
                                    CssClass="delete-list-btn"
                                    CommandName="DeleteList"
                                    CommandArgument='<%# Eval("listName") %>'
                                    OnClientClick="return confirm('Are you sure you want to delete this list?');" />
                            </div>
                        </div>
                        <div class="study-list-info">
                            <p><strong>Cards:</strong> <%# Eval("cardCount") %></p>
                            <p><strong>Created:</strong> <%# Convert.ToDateTime(Eval("dateAdded")).ToString("MMM dd, yyyy") %></p>
                        </div>
                        <asp:Button ID="btnViewList" runat="server" 
                            Text="View List" 
                            CssClass="view-list-btn"
                            CommandName="ViewList"
                            CommandArgument='<%# Eval("listId") %>' />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>