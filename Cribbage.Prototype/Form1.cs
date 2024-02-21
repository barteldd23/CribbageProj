using Cribbage.BL.Models;
using System.Runtime.CompilerServices;
namespace Cribbage.Prototype
{
    public partial class Form1 : Form
    {
        Game cribbageGame = new Game();
        int cribCount = 0;
        bool computerGo = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
            player1.FirstName = "Computer";
            player1.LastName = "Computer";
            player1.DisplayName = "Comp Stomp";
            player1.Id = new Guid();


            cribbageGame.Player_1 = player1;
            cribbageGame.Player_2 = player2;
            cribbageGame.PlayerTurn = cribbageGame.Player_1;

            btnPlayStop.Text = "Stop Game";

            // while (cribbageGame.Winner == null)
            //  {
            StartHand();

            //while(cribCount < 2)
            //{
            btnGo.Enabled = false;
            btnPlayCard.Enabled = false;
            // }


            ChooseComputerCrib();

            // }
        }



        private void StartHand()
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
            refreshCards(cribbageGame);




        }

        private void refreshCards(Game cribbageGame)
        {
            lstP1_Cards.DataSource = null;
            lstPlayed_Cards.DataSource = null;
            lstCrib_Cards.DataSource = null;

            lstP1_Cards.DataSource = cribbageGame.Player_1.Hand;
            lstP1_Cards.DisplayMember = "name";

            lstP2_Cards.DataSource = null;
            lstP2_Cards.DataSource = cribbageGame.Player_2.Hand;
            lstP2_Cards.DisplayMember = "name";

            if (cribbageGame.Crib.Count > 0)
            {
                
                lstCrib_Cards.DataSource = cribbageGame.Crib;
                lstCrib_Cards.DisplayMember = "name";
            }

            if (cribbageGame.PlayedCards.Count > 0)
            {
                lstPlayed_Cards.DataSource = cribbageGame.PlayedCards;
                lstPlayed_Cards.DisplayMember= "name";
            }
        }

        private void ptbP1_C1_Click(object sender, EventArgs e)
        {

        }

        private void btnSendToCrib_Click(object sender, EventArgs e)
        {
            if (lstP1_Cards.SelectedItems.Count == 2)
            {
                foreach (Card card in lstP1_Cards.SelectedItems)
                {
                    cribbageGame.Crib.Add(card);
                    cribbageGame.Player_1.Hand.Remove(card);
                }

                btnSendToCrib.Enabled = false;
                btnPlayCard.Enabled = true;
                btnGo.Enabled = true;
                refreshCards(cribbageGame);
            }
            else
            {
                MessageBox.Show("Select 2 cards. Use alt key.");
            }
        }

        private void ChooseComputerCrib()
        {
            Random rnd = new Random();
            Card card = cribbageGame.Player_2.Hand[rnd.Next(6)];
            cribbageGame.Crib.Add(card);
            cribbageGame.Player_2.Hand.Remove(card);
            card = cribbageGame.Player_2.Hand[rnd.Next(5)];
            cribbageGame.Crib.Add(card);
            cribbageGame.Player_2.Hand.Remove(card);

            refreshCards(cribbageGame);


        }

        private void btnPlayCard_Click(object sender, EventArgs e)
        {
            if(lstP1_Cards.SelectedItems.Count == 1) 
            {
               if(cribbageGame.PlayCard((Card)lstP1_Cards.SelectedItem)) 
                {
                    lblCount.Text = cribbageGame.CurrentCount.ToString();
                    lblScore_P1.Text = cribbageGame.Player_1.Score.ToString();
                    lblScore_P2.Text = cribbageGame.Player_2.Score.ToString();
                    refreshCards(cribbageGame);

                    Random rnd = new Random();
                    int index = rnd.Next(cribbageGame.Player_2.Hand.Count);
                    if (cribbageGame.CanPlay() && cribbageGame.CurrentCount != 0)
                    {
                        for(int i = 0; i < cribbageGame.PlayerTurn.Hand.Count; i++)
                        {
                            if (cribbageGame.PlayCard(cribbageGame.PlayerTurn.Hand[i]))
                            {
                                lblCount.Text = cribbageGame.CurrentCount.ToString();
                                lblScore_P1.Text = cribbageGame.Player_1.Score.ToString();
                                lblScore_P2.Text = cribbageGame.Player_2.Score.ToString();
                                refreshCards(cribbageGame);
                                break;
                            }
                        }
                    }
                    else if(!computerGo)
                    {
                        computerGo = true;
                        cribbageGame.Go();
                        if(cribbageGame.GoCount == 0)
                        {
                            restartCount();
                        }
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
            if(cribbageGame.PlayerTurn == cribbageGame.Player_2)
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
    }
}
