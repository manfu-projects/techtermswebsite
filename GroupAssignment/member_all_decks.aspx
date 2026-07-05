<%@ Page Title="" Language="C#" MasterPageFile="~/member_navigation.Master" AutoEventWireup="true" CodeBehind="member_all_decks.aspx.cs" Inherits="GroupAssignment.member_all_decks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="member-decks">
        <div class="member-all-decks">
            <div class="member-decks-header">
                <h2>All Decks</h2>
            </div>
            <div class="member-decks-wrapper">
                <a href="member_all_decks.aspx" class="member-all-category">All Decks</a>
                <asp:Repeater ID="rptCategories" runat="server">
                    <ItemTemplate>
                        <a href="member_all_decks.aspx?category=<%# Eval("deckName") %>" 
                           class="member-all-category">
                            <%# Eval("deckName") %>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="member-decks-grid">
            <asp:Repeater ID="rptDecks" runat="server" OnItemCommand="rptAllDecks">
                <ItemTemplate>
                    <div class="member-deck-cards">
                        <div class="member-deck-header">
                            <h3><%# Eval("deckName") %></h3>
                        </div>
                        <p><%# Eval("deckDesc") %></p>
                        <asp:Button ID="btnViewDeck" runat="server" 
                            Text="View Deck" 
                            CssClass="view-deck-btn"
                            CommandName="ViewDeck"
                            CommandArgument='<%# Eval("deckId") %>' />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <script type="text/javascript"> 
        const categoriesWrapper = document.querySelector('.member-decks-wrapper');

        if (categoriesWrapper) {
            categoriesWrapper.addEventListener('wheel', function (e) {
                e.preventDefault();
                this.scrollLeft += e.deltaY;
            });
        }

    </script>
</asp:Content>