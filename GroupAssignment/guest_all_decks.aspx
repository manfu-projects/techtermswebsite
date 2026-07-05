<%@ Page Title="" Language="C#" MasterPageFile="~/guest_navigation.Master" AutoEventWireup="true" CodeBehind="guest_all_decks.aspx.cs" Inherits="GroupAssignment.guest_all_decks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="guest-decks">
        <div class="guest-all-decks">
            <div class="guest-decks-header">
                <h2>All Decks</h2>
            </div>
            <div class="guest-decks-wrapper">
                <a href="guest_all_decks.aspx" class="guest-all-category">All Decks</a>
                <asp:Repeater ID="rptCategories" runat="server">
                    <ItemTemplate>
                        <a href="guest_all_decks.aspx?category=<%# Eval("deckName") %>" 
                           class="guest-all-category">
                            <%# Eval("deckName") %>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="guest-decks-grid">
            <asp:Repeater ID="rptDecks" runat="server" OnItemCommand="rptAllDecks">
                <ItemTemplate>
                    <div class="guest-deck-cards">
                        <div class="guest-deck-header">
                            <h3><%# Eval("deckName") %></h3>
                        </div>
                        <p><%# Eval("deckDesc") %></p>
                        <asp:Button ID="btnViewDeck" runat="server" 
                            Text="View Deck" 
                            CssClass="guest-view-deck-btn"
                            CommandName="ViewDeck"
                            CommandArgument='<%# Eval("deckId") %>' />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            
            <asp:Panel ID="pnlLockedDecks" runat="server" Visible="false" CssClass="locked-decks-banner">
                <div class="locked-banner-content">
                    <div class="lock-icon">🔒</div>
                    <div class="locked-text">
                        <h3>Ready to Learn More?</h3>
                        <p>You've seen some of our decks. <strong>Register now</strong> to unlock all decks and full flashcard content!</p>
                        <div class="benefits">
                            <span>✓ Access to all categories</span>
                            <span>✓ Create custom study lists</span>
                            <span>✓ Track your progress</span>
                        </div>
                    </div>
                    <a href="signup.aspx" class="register-now-btn">Sign Up</a>
                </div>
            </asp:Panel>
        </div>
    </div>
    <script type="text/javascript"> 
        const categoriesWrapper = document.querySelector('.guest-decks-wrapper');

        if (categoriesWrapper) {
            categoriesWrapper.addEventListener('wheel', function (e) {
                e.preventDefault();
                this.scrollLeft += e.deltaY;
            });
        }
    </script>
</asp:Content>