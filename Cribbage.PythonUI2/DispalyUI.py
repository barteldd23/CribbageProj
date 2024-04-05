from textwrap import fill
from tkinter import *
from tkinter import ttk



def displayUI():
    

    root = Tk()
    root.option_add('*tearOff', False)
    
    menuBar = Menu(root)
    root.config(menu = menuBar)
    file = Menu(menuBar)
    
    menuBar.add_cascade(menu=file, label="File")
    
    file.add_command(label='New Game', command = lambda: print('NewGame'))
    file.add_command(label = 'Quit', command = quit)
    
    
    
    cardBack = PhotoImage(file="../images/cardBackBlue.png")
    smallCardBack = cardBack.subsample(5,5);

    menuAndCribFrame = ttk.Frame(root);
    menuAndCribFrame.config(height=900, width=300, relief=RIDGE);
    menuAndCribFrame.pack(side=LEFT, fill=BOTH, expand=True);
    paddingFrame = ttk.Frame(menuAndCribFrame, height=300)
    paddingFrame.pack(fill=X)
    cribFrame = ttk.Frame(menuAndCribFrame, height=300)
    cribFrame.pack(fill=X)
    
    playFrame = ttk.Frame(root);
    playFrame.config(height=900, width=900, relief=RIDGE);
    playFrame.pack(side=LEFT, fill=BOTH, expand=True);
    
    scoreFrame = ttk.Frame(root);
    scoreFrame.config(height=900, width=300, relief=RIDGE);
    scoreFrame.pack(side=LEFT, fill=BOTH, expand=True);
    
    availableGamesFrame = ttk.Frame(root);
    availableGamesFrame.config(height=900, width=200);
    availableGamesFrame.pack(side=LEFT, fill=BOTH, expand=True)
    
    txtCrib = ttk.Label(cribFrame, text="Player1's Crib:")
    cribCard1 = ttk.Label(cribFrame, width=50);
    cribCard1.img = smallCardBack;
    cribCard1.config(image = cribCard1.img);
    cribCard2 = ttk.Label(cribFrame, width=50);
    cribCard2.img = smallCardBack;
    cribCard2.config(image = cribCard2.img);
    cribCard3 = ttk.Label(cribFrame, width=50);
    cribCard3.img = smallCardBack;
    cribCard3.config(image = cribCard3.img);
    cribCard4 = ttk.Label(cribFrame, width=50);
    cribCard4.img = smallCardBack;
    cribCard4.config(image = cribCard4.img);
    
    txtCrib.grid(row=0, column=0, columnspan=2)
    cribCard1.grid(row=1, column=0);
    cribCard2.grid(row=1, column=1);
    cribCard3.grid(row=2, column=0);
    cribCard4.grid(row=2, column=1);
    
    opponentFrame = ttk.Frame(playFrame,height=300, width=900, relief=RIDGE)
    rallyFrame = ttk.Frame(playFrame,height=300, width=900, relief=RIDGE)
    myFrame = ttk.Frame(playFrame,height=300, width=900, relief=RIDGE)
    
    opponentFrame.pack(fill=BOTH, expand=True);
    rallyFrame.pack(fill=BOTH, expand=True);
    myFrame.pack(fill=BOTH, expand=True);

    opponentLabel = ttk.Label(opponentFrame, text="Oppenents Hand");
    opponentLabel.grid(column=2, row=0, columnspan=2, pady=5, padx=5);
    
    cardBack = PhotoImage(file="./images/cardBackBlue.png")
    smallCardBack = cardBack.subsample(5,5);

    # Setting the image this way should prevent garbage collection of the image.
    opponentCard1 = ttk.Label(opponentFrame);
    opponentCard1.img = cardBack.subsample(5,5);
    opponentCard1.config(image= opponentCard1.img)
    

    opponentCard2 = ttk.Label(opponentFrame, width=100);
    opponentCard2.img = cardBack.subsample(5,5);
    opponentCard2.config(image= opponentCard2.img)
    opponentCard3 = ttk.Label(opponentFrame, width=100);
    opponentCard3.img = cardBack.subsample(5,5);
    opponentCard3.config(image= opponentCard3.img)
    opponentCard4 = ttk.Label(opponentFrame, width=100);
    opponentCard4.img = cardBack.subsample(5,5);
    opponentCard4.config(image= opponentCard4.img)
    opponentCard5 = ttk.Label(opponentFrame, width=100);
    opponentCard5.img = cardBack.subsample(5,5);
    opponentCard5.config(image= opponentCard5.img)
    opponentCard6 = ttk.Label(opponentFrame, width=100);
    opponentCard6.img = cardBack.subsample(5,5);
    opponentCard6.config(image= opponentCard6.img)
    
    opponentCard1.grid(row=1, column=0, padx=25);
    opponentCard2.grid(row=1, column=1, padx=25);
    opponentCard3.grid(row=1, column=2, padx=25);
    opponentCard4.grid(row=1, column=3, padx=25);
    opponentCard5.grid(row=1, column=4, padx=25);
    opponentCard6.grid(row=1, column=5, padx=25);
    

    currentCountLabel = ttk.Label(rallyFrame, text="Current Count: 0");
    currentCountLabel.grid(row=0, column=3, columnspan=2, pady=5);
    
    playedCard1 = ttk.Label(rallyFrame, width=100);
    playedCard1.img = smallCardBack;
    playedCard1.config(image = playedCard1.img);
    playedCard2 = ttk.Label(rallyFrame, width=100);
    playedCard2.img = smallCardBack;
    playedCard2.config(image = playedCard2.img);
    playedCard3 = ttk.Label(rallyFrame, width=100);
    playedCard3.img = smallCardBack;
    playedCard3.config(image = playedCard3.img);
    playedCard4 = ttk.Label(rallyFrame, width=100);
    playedCard4.img = smallCardBack;
    playedCard4.config(image = playedCard4.img);
    playedCard5 = ttk.Label(rallyFrame, width=100);
    playedCard5.img = smallCardBack;
    playedCard5.config(image = playedCard5.img);
    playedCard6 = ttk.Label(rallyFrame, width=100);
    playedCard6.img = smallCardBack;
    playedCard6.config(image = playedCard6.img);
    playedCard7 = ttk.Label(rallyFrame, width=100);
    playedCard7.img = smallCardBack;
    playedCard7.config(image = playedCard7.img);
    playedCard8 = ttk.Label(rallyFrame, width=100);
    playedCard8.img = smallCardBack;
    playedCard8.config(image = playedCard8.img);
    
    cutCard = ttk.Label(rallyFrame, width=100)
    cutCardImg = PhotoImage(file="./images/cardClubs_Jack.png")
    cutCard.img = cutCardImg.subsample(5,5);
    cutCard.config(image = cutCard.img);
    
    playedCard1.grid(row=1, column=0, padx=5);
    playedCard2.grid(row=1, column=1, padx=5);
    playedCard3.grid(row=1, column=2, padx=5);
    playedCard4.grid(row=1, column=3, padx=5);
    playedCard5.grid(row=1, column=4, padx=5);
    playedCard6.grid(row=1, column=5, padx=5);
    playedCard7.grid(row=1, column=6, padx=5);
    playedCard8.grid(row=1, column=7, padx=5);
    
    cutCard.grid(row=1, column=8, padx=5)
    
    outputText = ttk.Label(rallyFrame, text="Testing output text.Testing output text.Testing output text.Testing output text.Testing output text.Testing output text.Testing output text.", wraplength=600)
    btnCountHand = ttk.Button(rallyFrame, text="Count Hands");
    
    outputText.grid(row=2, column=0, columnspan=7, padx=5, pady=5, sticky=NW);
    btnCountHand.grid(row=2, column=8, sticky=NW, padx=5, pady=5);
    
    myLabel = ttk.Label(myFrame, text="My Hand");
    myLabel.grid(column=2, row=0, columnspan=2, pady=5, padx=5);
    
    

    # Setting the image this way should prevent garbage collection of the image.
    myCard1 = ttk.Label(myFrame);
    myCard1.img = cardBack.subsample(5,5);
    myCard1.config(image= myCard1.img)
    

    myCard2 = ttk.Label(myFrame, width=100);
    myCard2.img = cardBack.subsample(5,5);
    myCard2.config(image= myCard2.img)
    myCard3 = ttk.Label(myFrame, width=100);
    myCard3.img = cardBack.subsample(5,5);
    myCard3.config(image= myCard3.img)
    myCard4 = ttk.Label(myFrame, width=100);
    myCard4.img = cardBack.subsample(5,5);
    myCard4.config(image= myCard4.img)
    myCard5 = ttk.Label(myFrame, width=100);
    myCard5.img = cardBack.subsample(5,5);
    myCard5.config(image= myCard5.img)
    myCard6 = ttk.Label(myFrame, width=100);
    myCard6.img = cardBack.subsample(5,5);
    myCard6.config(image= myCard6.img)
    
    myCard1.grid(row=1, column=0, padx=25);
    myCard2.grid(row=1, column=1, padx=25);
    myCard3.grid(row=1, column=2, padx=25);
    myCard4.grid(row=1, column=3, padx=25);
    myCard5.grid(row=1, column=4, padx=25);
    myCard6.grid(row=1, column=5, padx=25);
    
    btnSendToCrib = ttk.Button(myFrame, text="Send To Crib");
    btnNextHand = ttk.Button(myFrame, text="Next Hand");
    btnPlayCard = ttk.Button(myFrame, text="Play Card");
    btnGo = ttk.Button(myFrame, text="Go");
    
    btnSendToCrib.grid(row=2, column=1, padx=5, pady=5);
    btnNextHand.grid(row=2, column=2, padx=5, pady=5);
    btnPlayCard.grid(row=2, column=3, padx=5, pady=5);
    btnGo.grid(row=2, column=4, padx=5, pady=5);
    

    cribbageBoard = ttk.Label(scoreFrame);
    boardImg = PhotoImage(file="../images/cribbageboard.png");
    cribbageBoard.img = boardImg;
    cribbageBoard.config(image=cribbageBoard.img);
    
    lblP1Score = ttk.Label(scoreFrame, text="Player1 Score: 0");
    lblP2Score = ttk.Label(scoreFrame, text="Player2 Score: 0");
    
    cribbageBoard.pack();
    lblP1Score.pack();
    lblP2Score.pack();
    

    

    root.mainloop();


displayUI()
