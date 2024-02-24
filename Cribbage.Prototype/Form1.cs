using Cribbage.BL.Models;
using System.Runtime.CompilerServices;
namespace Cribbage.Prototype
{
    public partial class Form1 : Form
    {
        Game cribbageGame = new Game();
        int cribCount = 0;
        bool computerSaidGo = false;
        bool playerSaidGo = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnGo.Enabled = false;
            btnPlayCard.Enabled = false;
            btnNextHand.Enabled = false;
            btnSendToCrib.Enabled = false;
        }

        private async void btnPlayStop_Click(object sender, EventArgs e)
        {
            if (btnPlayStop.Text == "Stop Game") Close();

            Player player1 = new Player();
            player1.FirstName = "Dean";
            player1.LastName = "Bartel";
            player1.DisplayName = "Dean Machine";
            player1.Id = new Guid();

            Player player2 = new Player();
            player2.FirstName = "Computer";
            player2.LastName = "Computer";
            player2.DisplayName = "Comp Stomp";
            player2.Id = new Guid();


            cribbageGame.Player_1 = player1;
            cribbageGame.Player_2 = player2;
            cribbageGame.Dealer = 1;
            cribbageGame.PlayerTurn = cribbageGame.Player_2;

            btnPlayStop.Text = "Stop Game";

            // while (cribbageGame.Winner == null)
            //  {
            DealHand();
        }



        private void DealHand()
        {
            cribbageGame.Deck = new Deck();
            cribbageGame.ShuffleDeck();
            cribbageGame.Deal();

            foreach (Card card in cribbageGame.Player_1.Hand)
            {
                card.imgPath = "../../../images/card" + card.suit.ToString() + "_" + card.face.ToString() + ".png";
            }

            foreach (Card card in cribbageGame.Player_2.Hand)
            {
                card.imgPath = "../../../images/card" + card.suit.ToString() + "_" + card.face.ToString() + ".png";
            }
            ptbP1_C1.BackgroundImage = Image.FromFile(cribbageGame.Player_1.Hand[0].imgPath);
            ptbP2_C1.BackgroundImage = Image.FromFile(cribbageGame.Player_2.Hand[0].imgPath);
            ptbP1_C2.BackgroundImage = Image.FromFile(cribbageGame.Player_1.Hand[1].imgPath);
            ptbP2_C2.BackgroundImage = Image.FromFile(cribbageGame.Player_2.Hand[1].imgPath);
            ptbP1_C3.BackgroundImage = Image.FromFile(cribbageGame.Player_1.Hand[2].imgPath);
            ptbP2_C3.BackgroundImage = Image.FromFile(cribbageGame.Player_2.Hand[2].imgPath);
            ptbP1_C4.BackgroundImage = Image.FromFile(cribbageGame.Player_1.Hand[3].imgPath);
            ptbP2_C4.BackgroundImage = Image.FromFile(cribbageGame.Player_2.Hand[3].imgPath);
            ptbP1_C5.BackgroundImage = Image.FromFile(cribbageGame.Player_1.Hand[4].imgPath);
            ptbP2_C5.BackgroundImage = Image.FromFile(cribbageGame.Player_2.Hand[4].imgPath);
            ptbP1_C6.BackgroundImage = Image.FromFile(cribbageGame.Player_1.Hand[5].imgPath);
            ptbP2_C6.BackgroundImage = Image.FromFile(cribbageGame.Player_2.Hand[5].imgPath);

            // Select 2 to go to crib.
            btnSendToCrib.Enabled = true;
            refreshCards(cribbageGame);

            txtPlayLog.Text += "~~~~~~~~~~~~~~~~~~~\n";

            if (cribbageGame.Dealer == 1)
            {
                txtPlayLog.Text += cribbageGame.Player_1.DisplayName + " Dealt\n";
            }
            else
            {
                txtPlayLog.Text += cribbageGame.Player_2.DisplayName + " Dealt\n";
            }

            txtPlayLog.Text += "Select Cards for the crib.\n";




        }

        private void refreshCards(Game cribbageGame)
        {
            lblCount.Text = cribbageGame.CurrentCount.ToString();
            lblScore_P1.Text = cribbageGame.Player_1.Score.ToString();
            lblScore_P2.Text = cribbageGame.Player_2.Score.ToString();

            lstP1_Cards.DataSource = null;
            lstPlayed_Cards.DataSource = null;
            lstCrib_Cards.DataSource = null;

            lstP1_Cards.DataSource = cribbageGame.Player_1.Hand;
            lstP1_Cards.DisplayMember = "name";

            lstP2_Cards.DataSource = null;
            lstP2_Cards.DataSource = cribbageGame.Player_2.Hand;
            lstP2_Cards.DisplayMember = "name";

            lstP1_Cards.SelectedIndex = -1;

            if (cribbageGame.Crib.Count > 0)
            {

                lstCrib_Cards.DataSource = cribbageGame.Crib;
                lstCrib_Cards.DisplayMember = "name";
            }

            if (cribbageGame.PlayedCards.Count > 0)
            {
                lstPlayed_Cards.DataSource = cribbageGame.PlayedCards;
                lstPlayed_Cards.DisplayMember = "name";
            }

            for (int i = 1; i <= 6; i++)
            {
                if (i <= lstP1_Cards.Items.Count)
                {
                    switch (i)
                    {
                        case 1:
                            ptbP1_C1.Visible = true;
                            ptbP1_C1.BackgroundImage = Image.FromFile(cribbageGame.Player_1.Hand[i - 1].imgPath);
                            break;
                        case 2:
                            ptbP1_C2.Visible = true;
                            ptbP1_C2.BackgroundImage = Image.FromFile(cribbageGame.Player_1.Hand[i - 1].imgPath);
                            break;
                        case 3:
                            ptbP1_C3.Visible = true;
                            ptbP1_C3.BackgroundImage = Image.FromFile(cribbageGame.Player_1.Hand[i - 1].imgPath);
                            break;
                        case 4:
                            ptbP1_C4.Visible = true;
                            ptbP1_C4.BackgroundImage = Image.FromFile(cribbageGame.Player_1.Hand[i - 1].imgPath);
                            break;
                        case 5:
                            ptbP1_C5.Visible = true;
                            ptbP1_C5.BackgroundImage = Image.FromFile(cribbageGame.Player_1.Hand[i - 1].imgPath);
                            break;
                        case 6:
                            ptbP1_C6.Visible = true;
                            ptbP1_C6.BackgroundImage = Image.FromFile(cribbageGame.Player_1.Hand[i - 1].imgPath);
                            break;

                    }
                }
                else
                {
                    switch (i)
                    {
                        case 1:
                            ptbP1_C1.Visible = false;
                            ptbP1_C1.BorderStyle = BorderStyle.None;
                            break;
                        case 2:
                            ptbP1_C2.Visible = false;
                            ptbP1_C2.BorderStyle = BorderStyle.None;
                            break;
                        case 3:
                            ptbP1_C3.Visible = false;
                            ptbP1_C3.BorderStyle = BorderStyle.None;
                            break;
                        case 4:
                            ptbP1_C4.Visible = false;
                            ptbP1_C4.BorderStyle = BorderStyle.None;
                            break;
                        case 5:
                            ptbP1_C5.Visible = false;
                            ptbP1_C5.BorderStyle = BorderStyle.None;
                            break;
                        case 6:
                            ptbP1_C6.Visible = false;
                            ptbP1_C6.BorderStyle = BorderStyle.None;
                            break;
                    }
                }
                if (i <= lstP2_Cards.Items.Count)
                {
                    switch (i)
                    {
                        case 1:
                            ptbP2_C1.Visible = true;
                            ptbP2_C1.BackgroundImage = Image.FromFile(cribbageGame.Player_2.Hand[i - 1].imgPath);
                            break;
                        case 2:
                            ptbP2_C2.Visible = true;
                            ptbP2_C2.BackgroundImage = Image.FromFile(cribbageGame.Player_2.Hand[i - 1].imgPath);
                            break;
                        case 3:
                            ptbP2_C3.Visible = true;
                            ptbP2_C3.BackgroundImage = Image.FromFile(cribbageGame.Player_2.Hand[i - 1].imgPath);
                            break;
                        case 4:
                            ptbP2_C4.Visible = true;
                            ptbP2_C4.BackgroundImage = Image.FromFile(cribbageGame.Player_2.Hand[i - 1].imgPath);
                            break;
                        case 5:
                            ptbP2_C5.Visible = true;
                            ptbP2_C5.BackgroundImage = Image.FromFile(cribbageGame.Player_2.Hand[i - 1].imgPath);
                            break;
                        case 6:
                            ptbP2_C6.Visible = true;
                            ptbP2_C6.BackgroundImage = Image.FromFile(cribbageGame.Player_2.Hand[i - 1].imgPath);
                            break;

                    }
                }
                else
                {
                    switch (i)
                    {
                        case 1:
                            ptbP2_C1.Visible = false;
                            break;
                        case 2:
                            ptbP2_C2.Visible = false;
                            break;
                        case 3:
                            ptbP2_C3.Visible = false;
                            break;
                        case 4:
                            ptbP2_C4.Visible = false;
                            break;
                        case 5:
                            ptbP2_C5.Visible = false;
                            break;
                        case 6:
                            ptbP2_C6.Visible = false;
                            break;
                    }
                }
            }

            for (int i = 1; i <= 8; i++)
            {
                if (i <= cribbageGame.PlayedCards.Count)
                {
                    switch (i)
                    {
                        case 1:
                            ptbPlayed1.Visible = true;
                            ptbPlayed1.BackgroundImage = Image.FromFile(cribbageGame.PlayedCards[i - 1].imgPath);
                            break;
                        case 2:
                            ptbPlayed2.Visible = true;
                            ptbPlayed2.BackgroundImage = Image.FromFile(cribbageGame.PlayedCards[i - 1].imgPath);
                            break;
                        case 3:
                            ptbPlayed3.Visible = true;
                            ptbPlayed3.BackgroundImage = Image.FromFile(cribbageGame.PlayedCards[i - 1].imgPath);
                            break;
                        case 4:
                            ptbPlayed4.Visible = true;
                            ptbPlayed4.BackgroundImage = Image.FromFile(cribbageGame.PlayedCards[i - 1].imgPath);
                            break;
                        case 5:
                            ptbPlayed5.Visible = true;
                            ptbPlayed5.BackgroundImage = Image.FromFile(cribbageGame.PlayedCards[i - 1].imgPath);
                            break;
                        case 6:
                            ptbPlayed6.Visible = true;
                            ptbPlayed6.BackgroundImage = Image.FromFile(cribbageGame.PlayedCards[i - 1].imgPath);
                            break;
                        case 7:
                            ptbPlayed7.Visible = true;
                            ptbPlayed7.BackgroundImage = Image.FromFile(cribbageGame.PlayedCards[i - 1].imgPath);
                            break;
                        case 8:
                            ptbPlayed8.Visible = true;
                            ptbPlayed8.BackgroundImage = Image.FromFile(cribbageGame.PlayedCards[i - 1].imgPath);
                            break;
                    }
                }
                else
                {
                    switch (i)
                    {
                        case 1:
                            ptbPlayed1.Visible = false;
                            break;
                        case 2:
                            ptbPlayed2.Visible = false;
                            break;
                        case 3:
                            ptbPlayed3.Visible = false;
                            break;
                        case 4:
                            ptbPlayed4.Visible = false;
                            break;
                        case 5:
                            ptbPlayed5.Visible = false;
                            break;
                        case 6:
                            ptbPlayed6.Visible = false;
                            break; ;
                        case 7:
                            ptbPlayed7.Visible = false;
                            break;
                        case 8:
                            ptbPlayed8.Visible = false;
                            break;
                    }
                }

            }
        }



        private void btnSendToCrib_Click(object sender, EventArgs e)
        {
            if (lstP1_Cards.SelectedItems.Count == 2)
            {
                List<Card> selectedCards = new List<Card>();
                foreach (Card card in lstP1_Cards.SelectedItems)
                {
                    selectedCards.Add(card);
                }
                cribbageGame.Give_To_Crib(selectedCards, cribbageGame.Player_1);

                List<Card> computerCards_To_Crib = cribbageGame.Pick_Cards_To_Crib(cribbageGame.Player_2.Hand);
                cribbageGame.Give_To_Crib(computerCards_To_Crib, cribbageGame.Player_2);

                txtPlayLog.Text += "Crib is set.\n";

                cribbageGame.Cut();
                cribbageGame.CutCard.imgPath = "../../../images/card" + cribbageGame.CutCard.suit.ToString() + "_" + cribbageGame.CutCard.face.ToString() + ".png";
                ptbDeck.BackgroundImage = Image.FromFile(cribbageGame.CutCard.imgPath);

                txtPlayLog.Text += "Cut card is " + cribbageGame.CutCard.name + "\n";

                btnSendToCrib.Enabled = false;

                btnGo.Enabled = false;
                btnNextHand.Enabled = false;
                refreshCards(cribbageGame);

                txtPlayLog.Text += cribbageGame.PlayerTurn.DisplayName + "'s turn.\n";
                if (cribbageGame.PlayerTurn == cribbageGame.Player_1)
                {
                    btnPlayCard.Enabled = true;
                }
                else
                {
                    // Computers turn
                    Computer_Play_Card();
                }
            }
            else
            {
                MessageBox.Show("Select 2 cards only.");
            }
        }

        private void Computer_Play_Card()
        {
            // Check if it has any cards.
            // Check if it has a card to be played.
            // Play that card
            // Check if Winner
            // check if p1 can play.
            // enable buttons or next step.

            // CanPlay() method checks if current player can play, has cards and a card that can play.
            if (cribbageGame.CanPlay())
            {
                Card cardToPlay = cribbageGame.Pick_Card_To_Play();
                cribbageGame.PlayCard(cardToPlay);

                txtPlayLog.Text += cribbageGame.Player_2.DisplayName + " played " + cardToPlay.name + "\n";

                refreshCards(cribbageGame);

                if (cribbageGame.Complete)
                {
                    EndGame();
                }
            }
            else
            {
                if (!cribbageGame.Player_2.SaidGo)
                {
                    cribbageGame.Go();
                    txtPlayLog.Text += cribbageGame.Player_2.DisplayName + " said Go.\n";
                    if (cribbageGame.CurrentCount == 0)
                    {
                        txtPlayLog.Text += "Rally Ended\n";
                        txtPlayLog.Text += cribbageGame.LastPlayerPlayed.DisplayName + " played last.\n";
                        txtPlayLog.Text += cribbageGame.PlayerTurn.DisplayName + "'s turn.\n";
                        refreshCards(cribbageGame);
                    }
                }
                else
                {
                    cribbageGame.PlayerTurn = cribbageGame.Player_1;
                }


            }

            if (cribbageGame.Player_1.Hand.Count == 0 && cribbageGame.Player_2.Hand.Count == 0)
            {
                CountHands();
            }
            else
            {
                if (cribbageGame.PlayerTurn == cribbageGame.Player_1 && cribbageGame.PlayerTurn.SaidGo == false)
                {
                    if (cribbageGame.CanPlay())
                    {
                        btnPlayCard.Enabled = true;
                        btnGo.Enabled = false;
                    }
                    else
                    {
                        btnPlayCard.Enabled = false;
                        btnGo.Enabled = true;
                    }
                }
                else
                {
                    cribbageGame.PlayerTurn = cribbageGame.Player_2;
                    Computer_Play_Card();
                }
            }
        }

        private void EndGame()
        {
            txtPlayLog.Text += "--------------";
            Player winner;

            if (cribbageGame.Winner == cribbageGame.Player_1.Id)
            {
                winner = cribbageGame.Player_1;
            }
            else
            {
                winner = cribbageGame.Player_2;
            }

            txtPlayLog.Text += winner.DisplayName + " wins! They scored " + winner.Score.ToString() + " points.\n";
        }

        private void CountHands()
        {
            int handScore = 0;
            cribbageGame.Player_1.Hand = cribbageGame.Player_1.PlayedCards;
            cribbageGame.Player_2.Hand = cribbageGame.Player_2.PlayedCards;
            refreshCards(cribbageGame);
            if (cribbageGame.Dealer == 1)
            {
                handScore = cribbageGame.CountHand(cribbageGame.Player_2.Hand);
                cribbageGame.Player_2.Score += handScore;
                txtPlayLog.Text += cribbageGame.Player_2.DisplayName + "'s hand scored " + handScore.ToString() + " points.\n";
                if (cribbageGame.CheckWinner())
                {
                    refreshCards(cribbageGame);
                    EndGame();
                }
                else
                {
                    handScore = cribbageGame.CountHand(cribbageGame.Player_1.Hand);
                    cribbageGame.Player_1.Score += handScore;
                    txtPlayLog.Text += cribbageGame.Player_1.DisplayName + "'s hand scored " + handScore.ToString() + " points.\n";
                }
            }
            else
            {
                handScore = cribbageGame.CountHand(cribbageGame.Player_1.Hand);
                cribbageGame.Player_2.Score += handScore;
                txtPlayLog.Text += cribbageGame.Player_1.DisplayName + "'s hand scored " + handScore.ToString() + " points.\n";
                if (cribbageGame.CheckWinner())
                {
                    refreshCards(cribbageGame);
                    EndGame();
                }
                else
                {
                    handScore = cribbageGame.CountHand(cribbageGame.Player_2.Hand);
                    cribbageGame.Player_1.Score += handScore;
                    txtPlayLog.Text += cribbageGame.Player_2.DisplayName + "'s hand scored " + handScore.ToString() + " points.\n";
                    if (cribbageGame.CheckWinner())
                    {
                        EndGame();
                    }

                }
            }

            handScore = cribbageGame.CountHand(cribbageGame.Crib);
            if (cribbageGame.Dealer == 1)
            {
                cribbageGame.Player_1.Score += handScore;
                txtPlayLog.Text += cribbageGame.Player_1.DisplayName + "'s crib scored " + handScore.ToString() + "  points.\n";
            }
            else
            {
                cribbageGame.Player_2.Score += handScore;
                txtPlayLog.Text += cribbageGame.Player_2.DisplayName + "'s crib scored " + handScore.ToString() + "  points.\n";
            }


            refreshCards(cribbageGame);
            if (cribbageGame.CheckWinner())
            {
                EndGame();
            }
            else
            {
                if (cribbageGame.Dealer == 1) cribbageGame.Dealer = 2;
                else cribbageGame.Dealer = 1;

                btnGo.Enabled = false;
                btnPlayCard.Enabled = false;
                btnSendToCrib.Enabled = false;
                btnNextHand.Enabled = true;
            }
        }

        private void btnPlayCard_Click(object sender, EventArgs e)
        {
            if (lstP1_Cards.SelectedItems.Count == 1)
            {
                Card playedCard = (Card)lstP1_Cards.SelectedItem;
                if (cribbageGame.PlayCard(playedCard))
                {
                    txtPlayLog.Text += cribbageGame.LastPlayerPlayed.DisplayName + " played " + playedCard.name + "\n";
                    refreshCards(cribbageGame);



                    if (cribbageGame.Complete)
                    {
                        EndGame();
                    }

                    if (cribbageGame.Player_1.Hand.Count == 0 && cribbageGame.Player_2.Hand.Count == 0)
                    {
                        CountHands();
                        return;
                    }

                    if (cribbageGame.PlayerTurn == cribbageGame.Player_2)
                    {
                        Computer_Play_Card();
                    }
                    else if (cribbageGame.CanPlay())
                    {
                        btnPlayCard.Enabled = true;
                    }
                    else
                    {
                        btnPlayCard.Enabled = false;
                        btnGo.Enabled = true;
                    }

                }
                else
                {
                    MessageBox.Show("Can not play that card");
                }
            }
            else
            {
                MessageBox.Show("Can only play 1 Card");
            }
        }

        private void restartCount()
        {
            lblCount.Text = "0";
            if (cribbageGame.PlayerTurn == cribbageGame.Player_2)
            {
                if (cribbageGame.CanPlay())
                {
                    for (int i = 0; i < cribbageGame.PlayerTurn.Hand.Count; i++)
                    {
                        if (cribbageGame.PlayCard(cribbageGame.PlayerTurn.Hand[i]))
                        {
                            lblCount.Text = cribbageGame.CurrentCount.ToString();
                            lblScore_P1.Text = cribbageGame.Player_1.Score.ToString();
                            lblScore_P2.Text = cribbageGame.Player_2.Score.ToString();
                            break;
                        }
                    }
                }
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            txtPlayLog.Text += cribbageGame.PlayerTurn.DisplayName + " said Go.\n";
            cribbageGame.Go();

            if (cribbageGame.CurrentCount == 0)
            {
                txtPlayLog.Text += "Rally Ended\n";
                txtPlayLog.Text += cribbageGame.LastPlayerPlayed.DisplayName + " played last.\n";
                txtPlayLog.Text += cribbageGame.PlayerTurn.DisplayName + "'s turn.\n";
            }
            if (cribbageGame.PlayerTurn == cribbageGame.Player_2)
            {
                Computer_Play_Card();
            }
        }

        private void ptbP1_C1_Click(object sender, EventArgs e)
        {
            if (lstP1_Cards.GetSelected(0))
            {
                ptbP1_C1.BorderStyle = BorderStyle.None;
                lstP1_Cards.SetSelected(0, false);
            }
            else
            {
                ptbP1_C1.BorderStyle = BorderStyle.FixedSingle;
                lstP1_Cards.SetSelected(0, true);
            }
        }

        private void ptbP1_C2_Click(object sender, EventArgs e)
        {
            if (lstP1_Cards.GetSelected(1))
            {
                ptbP1_C2.BorderStyle = BorderStyle.None;
                lstP1_Cards.SetSelected(1, false);
            }
            else
            {
                ptbP1_C2.BorderStyle = BorderStyle.FixedSingle;
                lstP1_Cards.SetSelected(1, true);
            }
        }

        private void ptbP1_C3_Click(object sender, EventArgs e)
        {
            if (lstP1_Cards.GetSelected(2))
            {
                ptbP1_C3.BorderStyle = BorderStyle.None;
                lstP1_Cards.SetSelected(2, false);
            }
            else
            {
                ptbP1_C3.BorderStyle = BorderStyle.FixedSingle;
                lstP1_Cards.SetSelected(2, true);
            }
        }

        private void ptbP1_C4_Click(object sender, EventArgs e)
        {
            if (lstP1_Cards.GetSelected(3))
            {
                ptbP1_C4.BorderStyle = BorderStyle.None;
                lstP1_Cards.SetSelected(3, false);
            }
            else
            {
                ptbP1_C4.BorderStyle = BorderStyle.FixedSingle;
                lstP1_Cards.SetSelected(3, true);
            }
        }

        private void ptbP1_C5_Click(object sender, EventArgs e)
        {
            if (lstP1_Cards.GetSelected(4))
            {
                ptbP1_C5.BorderStyle = BorderStyle.None;
                lstP1_Cards.SetSelected(4, false);
            }
            else
            {
                ptbP1_C5.BorderStyle = BorderStyle.FixedSingle;
                lstP1_Cards.SetSelected(4, true);
            }
        }

        private void ptbP1_C6_Click(object sender, EventArgs e)
        {
            if (lstP1_Cards.GetSelected(5))
            {
                ptbP1_C6.BorderStyle = BorderStyle.None;
                lstP1_Cards.SetSelected(5, false);
            }
            else
            {
                ptbP1_C6.BorderStyle = BorderStyle.FixedSingle;
                lstP1_Cards.SetSelected(5, true);
            }
        }

        private void btnNextHand_Click(object sender, EventArgs e)
        {
            btnNextHand.Enabled = false;
            DealHand();
        }
    }
}
