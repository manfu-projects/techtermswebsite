<%@ Page Title="" Language="C#" MasterPageFile="~/member_navigation.Master" AutoEventWireup="true" CodeBehind="member_dashboard.aspx.cs" Inherits="GroupAssignment.member_dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="member-welcome-msg">
            <h2>Welcome Back, <asp:Label ID="dashboard_username" runat="server" CssClass="db-username" >hi</asp:Label>.</h2>
            <p>Keep up the streak and sharpen your knowledge</p>
        </div>
        <div class="member-activity-summary">
            <div class="cards-learnt">
                <h2>100</h2>
                <p>Cards Learned</p>
            </div>
            <div class="total-study-list">
                <h2>5</h2>
                <p>Study List</p>
            </div>
        </div>
        <div class="member-activities">
            <div class="browse-deck-dir">
                <h2>Browse Decks</h2>
                <div class="activity-deck-card">
                    <a href="member_all_decks.aspx"
                        <p> Click to View Decks List</p>
                    </a>
                </div>
            </div>
            <div class="study-list-dir">
                <h2>My Study List</h2>
                <div class="activity-deck-card">
                    <a href="member_study_list.aspx"
                        <p>Click to View Study List</p>
                    </a>
                </div>
            </div>
            <div class="recent-activiy-dir">
                <h2>Recent Activites</h2>
                <div class="activity-deck-card">
                    <a href="member_study_list.aspx"
                        <p>Click to View Activities</p>
                    </a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
