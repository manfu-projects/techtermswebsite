<%@ Page Title="" Language="C#" MasterPageFile="~/member_navigation.Master" AutoEventWireup="true" CodeBehind="member_flashcard.aspx.cs" Inherits="GroupAssignment.member_flashcard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="flashcard-container">
        <div class="flashcard-header">
            <asp:Label ID="deckName" runat="server" CssClass="deck-title" />
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
            <asp:Button ID="btnPrevious" runat="server" Text="◀ Previous" CssClass="prev-flashcard-btn" OnClick="prevCardBtn" />
            <asp:Button ID="btnSaveToList" runat="server" Text="💾" CssClass="save-to-list-btn" OnClick="saveToListBtn" />
            <asp:Button ID="btnNext" runat="server" Text="Next ▶" CssClass="next-flashcard-btn" OnClick="nextCardBtn" />
        </div>
    </div>

        <!-- Save to list popup -->

        <div id="saveModal" class="card-save-to-list" style="display:none;">
            <div class="card-save-content">
                <span class="close-save-page" onclick="closeSavePage()">&times;</span>
                <h3>Save Card to Study List</h3>
                
                <asp:DropDownList ID="ddlStudyLists" runat="server" CssClass="study-list-dropdown" />
                
                <div class="list-save-divider">OR</div>
                
                <div class="new-list-save-section">
                    <asp:TextBox ID="txtNewListName" runat="server" placeholder="Enter new list name" CssClass="new-list-save-input" />
                    <asp:Button ID="btnCreateAndSave" runat="server" Text="Create New List & Save" CssClass="create-list-btn" OnClick="createSaveBtn" />
                </div>
                
            </div>
        </div>

    <asp:HiddenField ID="hdnCurrentIndex" runat="server" Value="0" />
    <asp:HiddenField ID="hdnTotalCards" runat="server" Value="0" />
    <asp:HiddenField ID="hdnDeckId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCurrentCardId" runat="server" Value="0" />

    <script type="text/javascript">
        function flipCard() {
            const card = document.getElementById('flashcard');
            card.classList.toggle('flipped');
        }

        function openSavePage() {
            document.getElementById('saveModal').style.display = 'block';
        }

        function closeSavePage() {
            document.getElementById('saveModal').style.display = 'none';
        }

        window.onclick = function (event) {
            var modal = document.getElementById('saveModal');
            if (event.target == modal) {
                modal.style.display = 'none';
            }
        }
    </script>
</asp:Content>