<%@ Page Title="" Language="C#" MasterPageFile="~/member_navigation.Master" AutoEventWireup="true" CodeBehind="member_home.aspx.cs" Inherits="GroupAssignment.member_home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="hero-container">
        <div class="hero-content">
            <h1>Learn Smarter.<br>Remember Faster.</h1>
            <p>Interactie flashcards that adapt to your learning style. Master web development, cybersecurity, data structures and more - one card at a time.</p>
            <div class="hero-btn">
                <a href="member_all_decks.aspx" class="hero-start-learning">Start Learning -></a>
            </div>
            <div class="hero-stats">
                <div class="hero-stat">
                    <span class="hero-stat-number">200+</span>
                    <span class="hero-stat-label">FLASHCARDS</span>
                </div>
                <div class="hero-stat">
                    <span class="hero-stat-number">20+</span>
                    <span class="hero-stat-label">CATEGORIES</span>
                </div>
                <div class="hero-stat">
                    <span class="hero-stat-number">1k+</span>
                    <span class="hero-stat-label">LEARNERS</span>
                </div>
            </div>
        </div>
        <div class="hero-img">
            <img src="hero_image.jpg"/>
        </div>
    </div>
    <div class="explore-deck">
        <div class="explore-deck-header">
            <h2>Explore Decks</h2>
            <p>Choose a category and start studying</p>
        </div>
        <div class="explore-deck-grid">
            <a href="member_all_decks.aspx" class="explore-deck-card">
                <div class="explore-card-icon">🌐</div>
                <h3>Web Development</h3>
                <p>HTML, CSS, JavaScript, React & More</p>
                <div class="card-stats">
                    <span>📚 30 Cards</span>
                </div>
            </a>
            <a href="member_all_decks.aspx" class="explore-deck-card">
                <div class="explore-card-icon">🔒</div>
                <h3>Cybersecurity</h3>
                <p>Threats, encryption, network security</p>
                <div class="card-stats">
                    <span>📚 36 cards</span>
                </div>
            </a>
            <a href="member_all_decks.aspx" class="explore-deck-card">
                <div class="explore-card-icon">📊</div>
                <h3>Data Structures</h3>
                <p>Arrays, trees, graphs, algorithms</p>
                <div class="card-stats">
                    <span>📚 52 cards</span>
                </div>
            </a>
            <a href="member_all_decks.aspx" class="explore-deck-card">
                <div class="explore-card-icon">☁️</div>
                <h3>Cloud Computing</h3>
                <p>AWS, Azure, deployment, DevOps</p>
                <div class="card-stats">
                    <span>📚 44 cards</span>
                </div>
            </a>
            <a href="member_all_decks.aspx" class="explore-deck-card">
                <div class="explore-card-icon">⚙️</div>
                <h3>Artifical Intelligence</h3>
                <p>Machine learning, neural networks, intelligent systems design</p>
                <div class="card-stats">
                    <span>📚 35 cards</span>
                </div>
            </a>
            <a href="member_all_decks.aspx" class="explore-deck-card">
                <div class="explore-card-icon">📁</div>
                <h3>Active Directory</h3>
                <p>Users, groups, permissions, domains, and authentication management</p>
                <div class="card-stats">
                    <span>📚 29 cards</span>
                </div>
            </a>
        </div>
    </div>
    <div class="why-us">
        <div class="why-us-header">
            <h2>Why Tech Terms?</h2>
            <p>Built for effective learnign & techniques</p>
        </div>
        <div class="explore-deck-grid">
            <div class="explore-deck-card">
                <div class="explore-card-icon">🌐</div>
                <h3>Card Saving</h3>
                <p1>Users can save flash cards to a personal study list.<br><br></p1>
            </div>
            <div class="explore-deck-card">
                <div class="explore-card-icon">🔒</div>
                <h3>Card Availability</h3>
                <p1>A large number of flash cards are provided for studying.<br><br></p1>
            </div>
            <div class="explore-deck-card">
                <div class="explore-card-icon">📊</div>
                <h3>Active Updates</h3>
                <p1>New flashcard and deck content are constantly added.<br><br></p1>
            </div>
        </div>
    </div>
</asp:Content>
