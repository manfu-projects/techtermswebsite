<%@ Page Title="" Language="C#" MasterPageFile="~/guest_navigation.Master" AutoEventWireup="true" CodeBehind="guest_flashcard.aspx.cs" Inherits="GroupAssignment.guest_flashcard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="guest-flashcard-container">
        <div class="guest-flashcard-header">
            <asp:Label ID="guestDeckName" runat="server" CssClass="guest-deck-title" />
            <asp:Label ID="guestProgress" runat="server" CssClass="guest-learning-progress"></asp:Label>
        </div>

        <div class="guest-flashcard-wrapper">
            <div class="guest-flashcard" id="guestFlashcard" onclick="guestFlipCard()">
                <div class="guest-card-front">
                    <div class="guest-card-content">
                        <asp:Label ID="guestLblTerm" runat="server" CssClass="guest-term-text"></asp:Label>
                        <p class="guest-flip-flashcard-txt">Click to flip</p>
                    </div>
                </div>
                <div class="guest-card-back">
                    <div class="guest-card-content">
                        <asp:Label ID="guestLblDefinition" runat="server" CssClass="guest-definition-text"></asp:Label>
                        <p class="guest-flip-flashcard-txt">Click to flip back</p>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="guest-flashcard-nav-buttons">
            <asp:Button ID="guestBtnPrevious" runat="server" Text="◀ Previous" CssClass="guest-prev-flashcard-btn" OnClick="guestPrevCardBtn" />
            <asp:Button ID="guestBtnNext" runat="server" Text="Next ▶" CssClass="guest-next-flashcard-btn" OnClick="guestNextCardBtn" />
        </div>
    </div>

    <asp:HiddenField ID="guestHdnCurrentIndex" runat="server" Value="0" />
    <asp:HiddenField ID="guestHdnTotalCards" runat="server" Value="0" />
    <asp:HiddenField ID="guestHdnDeckId" runat="server" Value="0" />
    <asp:HiddenField ID="guestHdnCurrentCardId" runat="server" Value="0" />

    <script type="text/javascript">
        function guestFlipCard() {
            const card = document.getElementById('guestFlashcard');
            card.classList.toggle('guest-flipped');
        }
    </script>
</asp:Content>