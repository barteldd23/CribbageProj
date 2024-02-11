using Cribbage.BL.Models;
namespace Cribbage.Prototype
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnPlayStop_Click(object sender, EventArgs e)
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

            CribbageGame cribbageGame = new CribbageGame();
            cribbageGame.Player_1 = player1;
            cribbageGame.Player_2 = player2;

            btnPlayStop.Text = "Stop Game";

            while (cribbageGame.Winner == null)
            {
                PlayHand(cribbageGame);
            }
        }

        private void PlayHand(CribbageGame cribbageGame)
        {
            cribbageGame.ShuffleDeck();
            cribbageGame.Deal();

            foreach(Card card in cribbageGame.Player_1.Hand)
            {
                card.imgPath = "../images/card" + card.suit.ToString() + "_" + card.face.ToString();
            }

            foreach (Card card in cribbageGame.Player_2.Hand)
            {
                card.imgPath = "../images/card" + card.suit.ToString() + "_" + card.face.ToString();
            }
            ptbP1_C1.Image = Image.FromFile(cribbageGame.Player_1.Hand[0].imgPath);
            ptbP2_C1.Image = Image.FromFile(cribbageGame.Player_2.Hand[0].imgPath);
            ptbP1_C2.Image = Image.FromFile(cribbageGame.Player_1.Hand[1].imgPath);
            ptbP2_C2.Image = Image.FromFile(cribbageGame.Player_2.Hand[1].imgPath);
            ptbP1_C3.Image = Image.FromFile(cribbageGame.Player_1.Hand[2].imgPath);
            ptbP2_C3.Image = Image.FromFile(cribbageGame.Player_2.Hand[2].imgPath);
            ptbP1_C4.Image = Image.FromFile(cribbageGame.Player_1.Hand[3].imgPath);
            ptbP2_C4.Image = Image.FromFile(cribbageGame.Player_2.Hand[3].imgPath);
            ptbP1_C5.Image = Image.FromFile(cribbageGame.Player_1.Hand[4].imgPath);
            ptbP2_C5.Image = Image.FromFile(cribbageGame.Player_2.Hand[4].imgPath);
            ptbP1_C6.Image = Image.FromFile(cribbageGame.Player_1.Hand[5].imgPath);
            ptbP2_C6.Image = Image.FromFile(cribbageGame.Player_2.Hand[5].imgPath);
        }
    }
}
