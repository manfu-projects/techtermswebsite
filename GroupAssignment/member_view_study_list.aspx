<%@ Page Title="" Language="C#" MasterPageFile="~/member_navigation.Master" AutoEventWireup="true" CodeBehind="member_view_study_list.aspx.cs" Inherits="GroupAssignment.member_view_study_list" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="flashcard-container">
        <div class="flashcard-header">
            <asp:Label ID="studyListName" runat="server" CssClass="deck-title"></asp:Label>
            <asp:Label ID="progress" runat="server" CssClass="learning-progress"></asp:Label>
        </div>

        <div class="flashcard-wrapper">
            <div class="flashcard" id="flashcard" onclick="flipCard()">
                <div class="card-front">
                    <div class="card-content">
                        <asp:Label ID="lblTerm" runat="server" CssClass="term-text"></asp:Label>
                        <p class="flip-flashcard-txt">Click to flip</p>
                    </div>
                </div>
                <div class="card-back">
                    <div class="card-content">
                        <asp:Label ID="lblDefinition" runat="server" CssClass="definition-text"></asp:Label>
                        <p class="flip-flashcard-txt">Click to flip back</p>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="flashcard-nav-buttons">
            <asp:Button ID="btnPrevious" runat="server" Text="◀ Previous" CssClass="list-prev-flashcard-btn" OnClick="prevCardBtn" />
            <asp:Button ID="btnNext" runat="server" Text="Next ▶" CssClass="list-next-flashcard-btn" OnClick="nextCardBtn" />
        </div>
    </div>

    <asp:HiddenField ID="hdnCurrentIndex" runat="server" Value="0" />
    <asp:HiddenField ID="hdnTotalCards" runat="server" Value="0" />
    <asp:HiddenField ID="hdnUserId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnListName" runat="server" Value="" />
    <asp:HiddenField ID="hdnCurrentCardId" runat="server" Value="0" />

    <script type="text/javascript">
        function flipCard() {
            const card = document.getElementById('flashcard');
            card.classList.toggle('flipped');
        }
    </script>
</asp:Content>
