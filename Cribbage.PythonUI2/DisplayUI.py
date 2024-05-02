from asyncio.windows_events import NULL
from gc import disable
from textwrap import fill
from tkinter import *
from tkinter import ttk
import tkinter
from tkinter import messagebox
from tkinter.tix import COLUMN
from turtle import bgcolor, seth
from dataclasses import dataclass, field, asdict
import json
import uuid


from signalrcore.hub_connection_builder import HubConnectionBuilder
#
################################# Classes ##############


@dataclass
class CribbageGame:
    data: [] 

@dataclass
class CribbageUser:
    Id: str = ''
    Email: str = ''
    DisplayName: str = ''
    FirstName: str = ''
    LastName: str = ''
    FullName: str = field(init=False)
    LastFirstName: str = field(init=False)
    Password: str = ''
    GamesStarted: int = 0
    GamesWon: int = 0
    GamesLost: int = 0
    WinStreak: int = 0
    AvgPtsPerGame: float = 0
    
    def insertJson(self, j):
        self.__dict__ = json.loads(j)
        
    def __post_init__(self) -> None:
        self.FullName = self.FirstName + " " + self.LastName
        self.LastFirstName = self.LastName + ", " + self.FirstName
    
@dataclass
class Card:
    face: int = 0
    suit: str = ''
    value: int =0
    imgPath: str = ''
    name: str = ''
@dataclass
class Hand:
    cards: [] 
    def insertJson(self, j):
        self.__dict__ = json.loads(j)
    

        
###################### Modular Variables #################

pythonUser = CribbageUser()
pythonUserJson = json.dumps(asdict(pythonUser))
playerHand = Hand([])
gameData = CribbageGame([])
opponentHand = Hand([])
selectedCards = []
playedCards = Hand([])
currentRallyCards = Hand([])
cribCards = Hand([])

def setGameData(dataJson):
    cribbageGame = json.loads(dataJson)
    gameData.data = NULL
    gameData.data = cribbageGame
    

###################### Methods for Received Hub Messages ##############

def receivedPlayerLeftMessage(message):
    setMessage(message)
    forgetButtons()

def receivedReadyToStartMessage(gameJson, message):
    pass

def receivedWaitingForPlayerMessage(gameJson, message):
    loggedInFrame.pack_forget()
    gameFrame.pack()
    setMessage(message)
    setGameOnly(gameJson)

def receivedGameFinishedMessage(gameJson, message):
    setMessage(message)
    setGameData(gameJson)
    refreshScreen(True, True)
def receivedStartNewHandMessage(message, gameJson):
    setMessage(message)
    setGameData(gameJson)
    displayCutCard(False)
    refreshScreen(False, False)
def receivedHandsCountedMessage(gameJson, message):
    setMessage(message)
    setGameData(gameJson)
    displayCutCard(True)
    refreshScreen(True, True)
def receivedCardWasCutMessage(gameJson, message):
    setMessage(message)
    setGameData(gameJson)
    displayCutCard(True)
    refreshScreen(False, False)
    
def receivedCutCardMessage(gameJson, message):
    setMessage(message)
    setGameData(gameJson)
    displayCutCard(False)
    refreshScreen(False, False)
    
def receivedActionMessage(gameJson, message):
    setMessage(message)
    setGameData(gameJson)
    refreshScreen(False, False)
        
def receivedCreateUserMessage(isCreated, messageInfo):
    if(isCreated):
        newPlayerFrame.pack_forget()
        lblErrorMessage.config(text="User Created")
        loginFrame.pack()
    else:
        lblCreateUserError.config(text=messageInfo)

def receivedLogInMessage(isLoggedIn, messageInfo, userJson):
    if(isLoggedIn):
        loginFrame.pack_forget()
        welcomeString = 'Welcome' + userJson
        print(userJson)
        pythonUser.insertJson(userJson)
        lblWelcomMessage.config(text=pythonUser.DisplayName )
        loggedInFrame.pack()
    else:
        lblErrorMessage.config(text=messageInfo)

def receivedStartGameMessage(message, gameJson):
    loggedInFrame.pack_forget()
    gameFrame.pack()
    setMessage(message)
    #setMessage(gameJson)
    #cribbageGame = CribbageGame()
    #cribbageGame.insertJson(gameJson)
    #cribbageGame.insertJson(gameJson)
    #print(cribbageGame);
    setGameData(gameJson)
    
    #print(playerHand)
    #print(opponentHand)
    
    setStartGameFrame(playerHand, opponentHand)
    print('setStartGameFrame')
    refreshScreen(False, False)
    
def setGameData(gameJson):
    cribbageGame = json.loads(gameJson)
    gameData.data = cribbageGame
    print(cribbageGame)
    hands = setHands()
    playerHand.cards = NULL
    opponentHand.cards = NULL
    playerHand.cards =hands[0]
    opponentHand.cards = hands[1]
    playedCards.cards = gameData.data["PlayedCards"]
    currentRallyCards = gameData.data["CurrentRally"]
    cribCards.cards = gameData.data["Crib"]
    print('*********************************')
    print(playedCards)
    print('*********************************')
    print(currentRallyCards)

def setGameOnly(gameJson):
    cribbageGame = json.loads(gameJson)
    gameData.data = cribbageGame
    
def setHands():
    if(pythonUser.Id == gameData.data["Player_1"]["Id"] and (gameData.data["WhatToDo"] != 'waitingforplayer2' and gameData.data["WhatToDo"] != 'readytostart')):
        return [gameData.data["Player_1"]["Hand"], gameData.data["Player_2"]["Hand"] ]
    elif(pythonUser.Id == gameData.data["Player_2"]["Id"] and (gameData.data["WhatToDo"] != 'waitingforplayer2' and gameData.data["WhatToDo"] != 'readytostart')):
        return [gameData.data["Player_2"]["Hand"], gameData.data["Player_1"]["Hand"] ]
    else:
        return ['','']
    
def setStartGameFrame(playerHand, opponentHand):
    if(pythonUser.Id == gameData.data["Player_1"]["Id"]):
        userDisplayName = gameData.data["Player_1"]["DisplayName"]
        opponentDisplayName = gameData.data["Player_2"]["DisplayName"]
    else:
        userDisplayName = gameData.data["Player_2"]["DisplayName"]
        opponentDisplayName = gameData.data["Player_1"]["DisplayName"]
        
    playerLabel.config(text=userDisplayName)
    opponentLabel.config(text=opponentDisplayName)
    
    displayPlayerScores()
    
    
    playerLabel.grid(row=0, column=2, columnspan=2, padx=5, pady=5, sticky='news')
    opponentLabel.grid(row=0, column=2, columnspan=2, padx=5, pady=5, sticky='news')
    lblP1DisplayName.grid(row=1, column=0, padx=5, pady=5, sticky='news')
    lblP1Score.grid(row=1, column=1, padx=5, pady=5, sticky='news')
    lblP2DisplayName.grid(row=2, column=0, padx=5, pady=5, sticky='news')
    lblP2Score.grid(row=2, column=1, padx=5, pady=5, sticky='news')
    btnBackToMenu.grid(row=3, column=1, columnspan=2, padx=5, pady=5, sticky='news')
    
    

    # if(gameData['WhatToDo'] == 'SelectCribCards'):
    #     displayOpponentHand(gameData, opponentHand, True)
    #     displayPlayerHand(gameData, playerHand)
    #     btnSendToCrib.grid(row=2, column=1, padx=5, pady=5, sticky='news')
    #     setMessage("in the if for selectCribCards")
    
def unselectCards():
    myCard1.config(border=0)
    myCard2.config(border=0)
    myCard3.config(border=0)
    myCard4.config(border=0)
    myCard5.config(border=0)
    myCard6.config(border=0)
    selectedCards.clear()
    
    print(selectedCards)
def forgetButtons():
    btnSendToCrib.grid_forget()
    btnGo.grid_forget()
    btnPlayCard.grid_forget()
    btnCountHand.grid_forget()
    btnNextHand.grid_forget()
    btnCutPosition.grid_forget()
    txtCutPosition.grid_forget()
    lblCutPosition.grid_forget()
    btnReadyToStart.grid_forget()

def refreshScreen(showOpponent, showCrib):
   # print('refresh start')
    unselectCards()
   # print('unselected cards')
    forgetButtons()
   # print('forgot buttons')
    if(gameData.data["WhatToDo"] != 'waitingforplayer2' and gameData.data["WhatToDo"] != 'readytostart' and gameData.data['PlayerTurn']['Id'] == pythonUser.Id):
        myTurn = True
    elif (gameData.data["WhatToDo"] != 'waitingforplayer2' and gameData.data["WhatToDo"] != 'readytostart' and gameData.data['PlayerTurn']['Id'] != pythonUser.Id):
        myTurn = False
    
    #print('myturn: ' + str(myTurn))
    print('******************************')
    print('Refresh Screen')
    print('******************************')
    displayCurrentCount()
   # displayOpponentHand(showOpponent)
    displayOpponentHand(True) 
    displayPlayerHand()
    displayPlayedCads()
    displayPlayerScores()
    #displayCribCards(showCrib)
    displayCribCards(True)
    
    if(gameData.data['WhatToDo'] == 'SelectCribCards' and len(playerHand.cards) > 4):
        btnSendToCrib.grid(row=2, column=1, padx=5, pady=5, sticky='news')
        setMessage("in the if for selectCribCards")
    if(gameData.data['WhatToDo'] == 'cutdeck' and myTurn):
        print('Inside cutdeck and myturn')
    if(gameData.data['WhatToDo'] == 'playcard' and myTurn):
        btnPlayCard.grid(row=3, column=3, padx=5, pady=5, sticky='news')
    if(gameData.data['WhatToDo'] == 'go' and myTurn):
        btnGo.grid(row=3, column=4, padx=5, pady=5, sticky='news')
    if(gameData.data['WhatToDo'] == 'counthands'):
        btnCountHand.grid(row=3, column=10, padx=5, pady=5, sticky='news')
    if(gameData.data['WhatToDo'] == 'cutdeck'):
        lblCutPosition.grid(row=1, column=2,padx=5, pady=5, sticky='ew')
        txtCutPosition.grid(row=1, column=3,padx=5, pady=5, sticky='e')
        btnCutPosition.grid(row=1, column=4,padx=5, pady=5, sticky='ew')
    if(gameData.data['WhatToDo'] == 'startnewhand'):
        btnNextHand.grid(row=3, column=3, padx=5, pady=5, sticky='news' )
    if(gameData.data['WhatToDo'] == 'startnewgame'):
        btnNewGame.grid(row=3, column=0, columnspan=10, padx=5, pady=5, sticky='news')
    if(gameData.data['WhatToDo'] == 'readytostart'):
        btnReadyToStart.grid(row=3, column=0, columnspan=10, padx=5, pady=5, sticky='news')
    
def displayCurrentCount():
    currentCountMsg = 'Current Count: ' + str(gameData.data["CurrentCount"])
    lblCurrentCount.config(text=currentCountMsg)
    lblCurrentCount.grid(row=0, column=0, columnspan=10, sticky='news')
    
def displayOpponentHand(isShown):
    print('in display Opp Hand. Hand Count: ' + str(len(opponentHand.cards)))
    if(isShown == False):
        if(len(opponentHand.cards) >= 1):
            opponentCard1.img = cardBack.subsample(5,5)
            opponentCard1.config(image= opponentCard1.img)
            opponentCard1.grid(row=1, column=0, sticky='news', padx=10)
        else:
            opponentCard1.grid_forget()
        if(len(opponentHand.cards) >= 2):
            opponentCard2.img = cardBack.subsample(5,5)
            opponentCard2.config(image= opponentCard2.img)
            opponentCard2.grid(row=1, column=1, sticky='news', padx=10)
        else:
            opponentCard2.grid_forget()
        if(len(opponentHand.cards) >= 3):
            opponentCard3.img = cardBack.subsample(5,5)
            opponentCard3.config(image= opponentCard3.img)
            opponentCard3.grid(row=1, column=2, sticky='news', padx=10)
        else:
            opponentCard3.grid_forget()
        if(len(opponentHand.cards) >= 4):
            opponentCard4.img = cardBack.subsample(5,5)
            opponentCard4.config(image= opponentCard4.img)
            opponentCard4.grid(row=1, column=3, sticky='news', padx=10)
        else:
            opponentCard4.grid_forget()
        if(len(opponentHand.cards) >= 5):
            opponentCard5.img = cardBack.subsample(5,5)
            opponentCard5.config(image= opponentCard5.img)
            opponentCard5.grid(row=1, column=4, sticky='news', padx=10)
        else:
            opponentCard5.grid_forget()
        if(len(opponentHand.cards) >= 6):
            opponentCard6.img = cardBack.subsample(5,5)
            opponentCard6.config(image= opponentCard6.img)
            opponentCard6.grid(row=1, column=5, sticky='news', padx=10)
        else:
            opponentCard6.grid_forget()
        
    else:
        if(len(opponentHand.cards) >= 1):
            card = PhotoImage(file="./images/" + opponentHand.cards[0]["imgPath"])
            opponentCard1.img = card.subsample(5,5)
            opponentCard1.config(image= opponentCard1.img)
            opponentCard1.grid(row=1, column=0, sticky='news', padx=10)
        else:
            opponentCard1.grid_forget()
        if(len(opponentHand.cards) >= 2):
            card = PhotoImage(file="./images/" + opponentHand.cards[1]["imgPath"])
            opponentCard2.img = card.subsample(5,5)
            opponentCard2.config(image= opponentCard2.img)
            opponentCard2.grid(row=1, column=1, sticky='news', padx=10)
        else:
            opponentCard2.grid_forget()
        if(len(opponentHand.cards) >= 3):
            card = PhotoImage(file="./images/" + opponentHand.cards[2]["imgPath"])
            opponentCard3.img = card.subsample(5,5)
            opponentCard3.config(image= opponentCard3.img)
            opponentCard3.grid(row=1, column=2, sticky='news', padx=10)
        else:
            opponentCard3.grid_forget()
        if(len(opponentHand.cards) >= 4):
            card = PhotoImage(file="./images/" + opponentHand.cards[3]["imgPath"])
            opponentCard4.img = card.subsample(5,5)
            opponentCard4.config(image= opponentCard4.img)
            opponentCard4.grid(row=1, column=3, sticky='news', padx=10)
        else:
            opponentCard4.grid_forget()
        if(len(opponentHand.cards) >= 5):
            card = PhotoImage(file="./images/" + opponentHand.cards[4]["imgPath"])
            opponentCard5.img = card.subsample(5,5)
            opponentCard5.config(image= opponentCard5.img)
            opponentCard5.grid(row=1, column=4, sticky='news', padx=10)
        else:
            opponentCard5.grid_forget()
        if(len(opponentHand.cards) >= 6):
            card = PhotoImage(file="./images/" + opponentHand.cards[5]["imgPath"])
            opponentCard6.img = card.subsample(5,5)
            opponentCard6.config(image= opponentCard6.img)
            opponentCard6.grid(row=1, column=5, sticky='news', padx=10)
        else:
            opponentCard6.grid_forget()
            

def displayPlayerHand():
    print('in display Player Hand. Hand Count: ' + str(len(opponentHand.cards)))
    
    if(len(playerHand.cards) >= 1):
        card = PhotoImage(file="./images/" + playerHand.cards[0]["imgPath"])
        myCard1.img = card.subsample(5,5)
        myCard1.config(image= myCard1.img)
        myCard1.grid(row=1, column=0, sticky='news', padx=10)
    else:
        myCard1.grid_forget()
    if(len(playerHand.cards) >= 2):
        card = PhotoImage(file="./images/" + playerHand.cards[1]["imgPath"])
        myCard2.img = card.subsample(5,5)
        myCard2.config(image= myCard2.img)
        myCard2.grid(row=1, column=1, sticky='news', padx=10)
    else:
        myCard2.grid_forget()
    if(len(playerHand.cards) >= 3):
        card = PhotoImage(file="./images/" + playerHand.cards[2]["imgPath"])
        myCard3.img = card.subsample(5,5)
        myCard3.config(image= myCard3.img)
        myCard3.grid(row=1, column=2, sticky='news', padx=10)
    else:
        myCard3.grid_forget()
    if(len(playerHand.cards) >= 4):
        card = PhotoImage(file="./images/" + playerHand.cards[3]["imgPath"])
        myCard4.img = card.subsample(5,5)
        myCard4.config(image= myCard4.img)
        myCard4.grid(row=1, column=3, sticky='news', padx=10)
    else:
        myCard4.grid_forget()
    if(len(playerHand.cards) >= 5):
        card = PhotoImage(file="./images/" + playerHand.cards[4]["imgPath"])
        myCard5.img = card.subsample(5,5)
        myCard5.config(image= myCard5.img)
        myCard5.grid(row=1, column=4, sticky='news', padx=10)
    else:
        myCard5.grid_forget()
    if(len(playerHand.cards) >= 6):
        card = PhotoImage(file="./images/" + playerHand.cards[5]["imgPath"])
        myCard6.img = card.subsample(5,5)
        myCard6.config(image= myCard6.img)
        myCard6.grid(row=1, column=5, sticky='news', padx=10)
    else:
        myCard6.grid_forget()

def displayPlayedCads():
    if(len(playedCards.cards) >= 1):
        card = PhotoImage(file="./images/" + playedCards.cards[0]["imgPath"])
        playedCard1.img = card.subsample(5,5)
        playedCard1.config(image= playedCard1.img)
        playedCard1.grid(row=1, column=0, sticky='news', padx=2)
    else:
        playedCard1.grid_forget()
    if(len(playedCards.cards) >= 2):
        card = PhotoImage(file="./images/" + playedCards.cards[1]["imgPath"])
        playedCard2.img = card.subsample(5,5)
        playedCard2.config(image= playedCard2.img)
        playedCard2.grid(row=1, column=1, sticky='news', padx=2)
    else:
        playedCard2.grid_forget()
    if(len(playedCards.cards) >= 3):
        card = PhotoImage(file="./images/" + playedCards.cards[2]["imgPath"])
        playedCard3.img = card.subsample(5,5)
        playedCard3.config(image= playedCard3.img)
        playedCard3.grid(row=1, column=2, sticky='news', padx=2)
    else:
        playedCard3.grid_forget()
    if(len(playedCards.cards) >= 4):
        card = PhotoImage(file="./images/" + playedCards.cards[3]["imgPath"])
        playedCard4.img = card.subsample(5,5)
        playedCard4.config(image= playedCard4.img)
        playedCard4.grid(row=1, column=3, sticky='news', padx=2)
    else:
        playedCard4.grid_forget()
    if(len(playedCards.cards) >= 5):
        card = PhotoImage(file="./images/" + playedCards.cards[4]["imgPath"])
        playedCard5.img = card.subsample(5,5)
        playedCard5.config(image= playedCard5.img)
        playedCard5.grid(row=1, column=4, sticky='news', padx=2)
    else:
        playedCard5.grid_forget()
    if(len(playedCards.cards) >= 6):
        card = PhotoImage(file="./images/" + playedCards.cards[5]["imgPath"])
        playedCard6.img = card.subsample(5,5)
        playedCard6.config(image= playedCard6.img)
        playedCard6.grid(row=1, column=5, sticky='news', padx=2)
    else:
        playedCard6.grid_forget()
    if(len(playedCards.cards) >= 7):
        card = PhotoImage(file="./images/" + playedCards.cards[6]["imgPath"])
        playedCard7.img = card.subsample(5,5)
        playedCard7.config(image= playedCard7.img)
        playedCard7.grid(row=1, column=6, sticky='news', padx=2)
    else:
        playedCard7.grid_forget()
    if(len(playedCards.cards) >= 8):
        card = PhotoImage(file="./images/" + playedCards.cards[7]["imgPath"])
        playedCard8.img = card.subsample(5,5)
        playedCard8.config(image= playedCard8.img)
        playedCard8.grid(row=1, column=7, sticky='news', padx=2)
    else:
        playedCard8.grid_forget()
        
def displayCribCards(isShowing):
    if(gameData.data["Dealer"] == 1):
        cribMsg = gameData.data["Player_1"]["DisplayName"] + '\'s Crib'
    else:
        cribMsg = gameData.data["Player_2"]["DisplayName"]
    txtCrib.config(text=cribMsg)
    txtCrib.grid(row=0, column=0, columnspan=2)
    
    if(isShowing):
        if(len(cribCards.cards) >= 1):
            card = PhotoImage(file="./images/" + cribCards.cards[0]["imgPath"])
            cribCard1.img = card.subsample(5,5)
            cribCard1.config(image= cribCard1.img)
            cribCard1.grid(row=1, column=0, sticky='news', padx=2)
        else:
            cribCard1.grid_forget()
        if(len(cribCards.cards) >= 2):
            card = PhotoImage(file="./images/" + cribCards.cards[1]["imgPath"])
            cribCard2.img = card.subsample(5,5)
            cribCard2.config(image= cribCard2.img)
            cribCard2.grid(row=1, column=1, sticky='news', padx=2)
        else:
            cribCard2.grid_forget()
        if(len(cribCards.cards) >= 3):
            card = PhotoImage(file="./images/" + cribCards.cards[2]["imgPath"])
            cribCard3.img = card.subsample(5,5)
            cribCard3.config(image= cribCard3.img)
            cribCard3.grid(row=2, column=0, sticky='news', padx=2)
        else:
            cribCard3.grid_forget()
        if(len(cribCards.cards) >= 4):
            card = PhotoImage(file="./images/" + cribCards.cards[3]["imgPath"])
            cribCard4.img = card.subsample(5,5)
            cribCard4.config(image= cribCard4.img)
            cribCard4.grid(row=2, column=1, sticky='news', padx=2)
        else:
            cribCard4.grid_forget()
    else:
        if(len(cribCards.cards) >= 1):
            cribCard1.img = cardBack.subsample(5,5)
            cribCard1.config(image= cribCard1.img)
            cribCard1.grid(row=1, column=0, sticky='news', padx=2)
        else:
            cribCard1.grid_forget()
        if(len(cribCards.cards) >= 2):
            cribCard2.img = cardBack.subsample(5,5)
            cribCard2.config(image= cribCard2.img)
            cribCard2.grid(row=1, column=1, sticky='news', padx=2)
        else:
            cribCard2.grid_forget()
        if(len(cribCards.cards) >= 3):
            cribCard3.img = cardBack.subsample(5,5)
            cribCard3.config(image= cribCard3.img)
            cribCard3.grid(row=2, column=0, sticky='news', padx=2)
        else:
            cribCard3.grid_forget()
        if(len(cribCards.cards) >= 4):
            cribCard4.img = cardBack.subsample(5,5)
            cribCard4.config(image= cribCard4.img)
            cribCard4.grid(row=2, column=1, sticky='news', padx=2)
        else:
            cribCard4.grid_forget()
def displayPlayerScores():
    lblP1DisplayName.config(text = gameData.data["Player_1"]["DisplayName"])
    lblP2DisplayName.config(text = gameData.data["Player_2"]["DisplayName"])
    lblP1Score.config(text = gameData.data["Player_1"]["Score"])
    lblP2Score.config(text = gameData.data["Player_2"]["Score"])
    
def displayCutCard(isShowing):
    cutCardLabel.grid(row=0, column=10, sticky='s')
    if(isShowing):
        print('in display cut card true')
        card = PhotoImage(file="./images/" + gameData.data["CutCard"]["imgPath"])
        cutCard.img = card.subsample(5,5)
        cutCard.config(image= cutCard.img)
        cutCard.grid(row=1, column=10, sticky='n', padx=5)
    else:
        print('in display cut card false')
        cutCard.img = cardBack.subsample(5,5)
        cutCard.config(image= opponentCard1.img)
        cutCard.grid(row=1, column=10, sticky='news', padx=5)
        
def setMessage(msg):
    txtGameMessages.config(state='normal')
    txtGameMessages.insert('end', msg + '\n')
    txtGameMessages.config(state='disabled')
###################### Methods for Button clicks ###################

def onclick_Card1(event):
    if(myCard1.cget('border') == 0):
        myCard1.config(border=5)
        selectedCards.append(0)
    else:
        myCard1.config(border=0)
        selectedCards.remove(0)
def onclick_Card2(event):
    if(myCard2.cget('border') == 0):
        myCard2.config(border=5)
        selectedCards.append(1)
    else:
        myCard2.config(border=0)
        selectedCards.remove(1)
def onclick_Card3(event):
    if(myCard3.cget('border') == 0):
        myCard3.config(border=5)
        selectedCards.append(2)
    else:
        myCard3.config(border=0)
        selectedCards.remove(2)
def onclick_Card4(event):
    if(myCard4.cget('border') == 0):
        myCard4.config(border=5)
        selectedCards.append(3)
    else:
        myCard4.config(border=0)
        selectedCards.remove(3)
def onclick_Card5(event):
    if(myCard5.cget('border') == 0):
        myCard5.config(border=5)
        selectedCards.append(4)
    else:
        myCard5.config(border=0)
        selectedCards.remove(4)
def onclick_Card6(event):
    if(myCard6.cget('border') == 0):
        myCard6.config(border=5)
        selectedCards.append(5)
    else:
        myCard6.config(border=0)
        selectedCards.remove(5)

  
def getGameJson():
    return json.dumps(asdict(gameData))[9:-1]
def getUserJson():
    return json.dumps(asdict(pythonUser))

def onClick_NewGame():
    if(gameData.data["Computer"]):
        messagebox.showinfo('','Start Game vs Computer')
        newVsComputer()
    else:
        messagebox.showinfo('','Start Game vs Player')

def onClick_CountHand():
    gameToSendJson = getGameJson()
    print('******Pushed GO Button ********')
    hub_connection.send("CountHands",[gameToSendJson])
def onClick_Go():
    gameToSendJson = getGameJson()
    print('******Pushed GO Button ********')
    hub_connection.send("Go",[gameToSendJson])
def onClick_PlayCard():
    if(len(selectedCards) == 1):
        cardsToSend= [playerHand.cards[selectedCards[0]]]
        cardsToSendJson = json.dumps(cardsToSend)
        gameToSendJson = getGameJson()
       # print(cardsToSendJson)
        print('*********')
        hub_connection.send("PlayCard",[gameToSendJson, cardsToSendJson])
    else:
        messagebox.showerror('Select Cards To Send To The Crib', 'Please select exactly two cards to send to the crib')
def onClickSendToCrib():
    if(len(selectedCards) == 2):
        cardsToSend= [playerHand.cards[selectedCards[0]],playerHand.cards[selectedCards[1]]]
        cardsToSendJson = json.dumps(cardsToSend)
        gameToSendJson = getGameJson()
       # print(cardsToSendJson)
        print('*********')
        hub_connection.send("CardsToCrib",[gameToSendJson, cardsToSendJson, pythonUserJson])
    else:
        messagebox.showerror('Select Cards To Send To The Crib', 'Please select exactly two cards to send to the crib')
    #
   # print(gameToSendJson)
   
def onClick_NextHand():
    gameToSendJson = getGameJson()
    print('******Pushed NextHand Button ********')
    hub_connection.send("NewHand",[gameToSendJson])
def newVsComputer():
    pythonUserJson = json.dumps(asdict(pythonUser))
    hub_connection.send("NewGameVsComputer",[pythonUserJson])

def newVsPlayer():
    pythonUserJson = json.dumps(asdict(pythonUser))
    hub_connection.send("NewGameVsPlayer",[pythonUserJson])

def onClick_ReadyToStart():
    print("hit ready to start")
    
def onClickNewUser():
    loginFrame.pack_forget()
    newPlayerFrame.pack()

def onClickLogin():
    lblErrorMessage.config(text='')
    email = txtEmail.get()
    password = txtPassword.get()
    
    if(email.isspace() or email.find('@') == -1):
        lblErrorMessage.config(text="Enter an Email")
        return
        
    if(len(password) == 0 or password.isspace()):
        lblErrorMessage.config(text="Enter a password")
        return
    
    hub_connection.send("Login",[email, password])
    
def onClickCreateUser():
    #clear error message
    lblCreateUserError.config(text='')

    ### check for valid entries
    firstName = txtFirstName.get()
    lastName = txtLastName.get()
    displayName = txtDisplayName.get()
    email = txtCreateUserEmail.get()
    password = txtNewPlayerPassword.get()
    password2 = txtNewPlayerVerifyPassword.get()
    if(firstName.isspace()):
        lblCreateUserError.config(text="Enter a First Name")
        return
    if(len(lastName)==0 or lastName.isspace()):
        lblCreateUserError.config(text="Enter a Last Name")
        return
    if(len(displayName)==0 or displayName.isspace()):
        lblCreateUserError.config(text="Enter a Display Name")
        return
    if( len(email) == 0 or email.isspace() or email.find('@') == -1 ):
        lblCreateUserError.config(text="Enter an Email")
        return
    if(len(password)==0 or password.isspace()):
        lblCreateUserError.config(text="Enter a Password")
        return
    if( password != password2):
        lblCreateUserError.config(text="Passwords not Matching")
        return
    
    user = CribbageUser()
    user.Id = str(uuid.uuid4())
    user.FirstName = firstName
    user.LastName = lastName
    user.DisplayName = displayName
    user.Email = email
    user.Password = password

    userJson = json.dumps(asdict(user))
    hub_connection.send("CreateUser",[userJson])
    #messagebox.showinfo(message=userJson)

def onClickCancelUser():
    newPlayerFrame.pack_forget()
    loginFrame.pack()

def onClick_btnCutPosition():
    cutposition = txtCutPosition.get()
    if(cutposition.isdigit() and int(cutposition) > 0 and int(cutposition) < 41):
        gameToSendJson = getGameJson()
        print('******Pushed CutCard Button ********')
        hub_connection.send("CutDeck",[gameToSendJson])
    else:
        messagebox.showerror("Invalid cut card number", "Enter a number from 1 to 40 to use as the cut card")
    #messagebox.showinfo(message='email: ' + email + ' password: ' + password)

def onClick_MainMenu():
    print('hit main menu button')
    pythonUserJson = json.dumps(asdict(pythonUser))
    gameToSendJson = getGameJson()
    hub_connection.send("QuitGame",[gameToSendJson, pythonUserJson])
    
def onClick_FileMainMenu():
    print('hit file main menu')
    print(gameFrame.winfo_ismapped())
    if(gameFrame.winfo_ismapped() == 0):
        pass
    else:
        pythonUserJson = json.dumps(asdict(pythonUser))
        gameToSendJson = getGameJson()
        hub_connection.send("QuitGame",[gameToSendJson, pythonUserJson])
        gameFrame.pack_forget()
        loggedInFrame.pack()
    
    
def onClick_Quit():
    print('hit the file quit')
    print(gameFrame.winfo_ismapped())
    if(gameFrame.winfo_ismapped() == 0):
        window.destroy()
    else:
        pythonUserJson = json.dumps(asdict(pythonUser))
        gameToSendJson = getGameJson()
        hub_connection.send("QuitGame",[gameToSendJson, pythonUserJson])
        window.destroy()

############### Hub Connection ###########################

# #hubAddress = "https://bigprojectapi-300089145.azurewebsites.net/CribbageHub"
hubAddress = "https://localhost:7186/CribbageHub"

hub_connection = HubConnectionBuilder()\
.with_url(hubAddress, options={"verify_ssl": False})\
.build()
    
hub_connection.on("ReceiveMessage", lambda msg: print("received message back from hub." + msg[0]))
# book isloggedin, bool issuccess (savedgames), string message, string userJson, string userGames Json
hub_connection.on("LogInAttempt", lambda data: receivedLogInMessage(data[0],data[2],data[3]))
hub_connection.on("CreateUserAttempt", lambda data: receivedCreateUserMessage(data[0],data[1]))
hub_connection.on("StartGame", lambda data: receivedStartGameMessage(data[0],data[1]))
hub_connection.on("CardWasCut", lambda data: receivedCardWasCutMessage(data[0], data[1]))
hub_connection.on("Action", lambda data: receivedActionMessage(data[0], data[1]))
hub_connection.on("CutCard", lambda data: receivedCutCardMessage(data[0],data[1]))
hub_connection.on("HandsCounted", lambda data: receivedHandsCountedMessage(data[0],data[1]))
hub_connection.on("StartNewHand", lambda data: receivedStartNewHandMessage(data[0],data[1]))
hub_connection.on("GameFinished", lambda data: receivedGameFinishedMessage(data[0], data[1]))
hub_connection.on("WaitingForPlayer", lambda data: receivedWaitingForPlayerMessage(data[0], data[1]))
hub_connection.on("ReadyToStart", lambda data: receivedStartGameMessage(data[1], data[0]))
hub_connection.on("PlayerLeft", lambda data: receivedPlayerLeftMessage(data[0]))

hub_connection.start()


################ Set Up Window and Menu Bar ####################
window = Tk()
window.option_add('*tearOff', False)
window.geometry('1400x900')
window.title('Cribbage Game App')
window.resizable(0,0)
window.protocol('WM_DELETE_WINDOW', onClick_Quit)
#window.overrideredirect(1)

#window.columnconfigure(0,weight=1)
# window.columnconfigure(1,weight=4)
# window.columnconfigure(2,weight=1)
# window.columnconfigure(3,weight=1)
#window.rowconfigure(0, weight=1)

menuBar = Menu(window)
window.config(menu = menuBar)
file = Menu(menuBar)
    
menuBar.add_cascade(menu=file, label="File")
    
file.add_command(label='Main Menu', command = onClick_FileMainMenu)
file.add_command(label = 'Quit', command = onClick_Quit)


############ Main Frames ###############
loginFrame = tkinter.Frame(bg='blue')
gameFrame = tkinter.Frame(bg='#333333', height=900, width=1400)
loggedInFrame = tkinter.Frame(bg='blue')
newPlayerFrame = tkinter.Frame(bg='blue')

#################### gameFrame Frames
cribFrame = tkinter.Frame(gameFrame, width=300, height=900, relief=RIDGE, bg='#333333')
playFrame = tkinter.Frame(gameFrame, width=800, height=900, relief=RIDGE, bg='blue')
scoreFrame = tkinter.Frame(gameFrame, width=300, height=900, relief=RIDGE, bg='green')
availableGamesFrame = tkinter.Frame(gameFrame, width=200, height=900, relief=RIDGE, bg='red')


playFrame.columnconfigure(0,weight=1)
playFrame.columnconfigure(1,weight=4)
playFrame.columnconfigure(2,weight=1)
playFrame.columnconfigure(3,weight=1)
playFrame.rowconfigure(0, weight=1)

# playFrame.rowconfigure(0, weight=1)
# playFrame.rowconfigure(1, weight=1)
# playFrame.rowconfigure(2, weight=1)
cribFrame.columnconfigure(0, weight=1)
playFrame.columnconfigure(0, weight=1)
scoreFrame.columnconfigure(0, weight=1)
availableGamesFrame.columnconfigure(0, weight=1)
loginFrame.columnconfigure(0,weight=1)

# scoreFrame.columnconfigure(0, weight=1)

cribFrame.grid_propagate(0)
playFrame.grid_propagate(0)
scoreFrame.grid_propagate(0)
availableGamesFrame.grid_propagate(0)

cribFrame.grid(row=0, column=0, sticky='news')
playFrame.grid(row=0, column=1, sticky='news')
scoreFrame.grid(row=0, column=2, sticky='news')

#availableGamesFrame.grid(row=0, column=3, sticky='news')

# cribFrame.pack(side='left')
# playFrame.pack(side='left')
# scoreFrame.pack(side='left')
# availableGamesFrame.pack(side='left')


##################### playFrame Frames #####################
opponentFrame = tkinter.Frame(playFrame, height=300, width=800, relief=RIDGE, bg='pink')
rallyFrame = tkinter.Frame(playFrame, height=300, width=800, relief=RIDGE, bg='orange')
usersFrame = tkinter.Frame(playFrame, height=300, width=800, relief=RIDGE, bg='purple')

# opponentFrame.columnconfigure(0, weight=1)
# opponentFrame.columnconfigure(1, weight=1)
# opponentFrame.columnconfigure(2, weight=1)
# opponentFrame.columnconfigure(3, weight=1)
# opponentFrame.columnconfigure(4, weight=1)
# opponentFrame.columnconfigure(5, weight=1)
# opponentFrame.columnconfigure(6, weight=1)

# rallyFrame.columnconfigure(0, weight=1)
# rallyFrame.columnconfigure(1, weight=1)
# rallyFrame.columnconfigure(2, weight=1)
# rallyFrame.columnconfigure(3, weight=1)
# rallyFrame.columnconfigure(4, weight=1)
# rallyFrame.columnconfigure(5, weight=1)
# rallyFrame.columnconfigure(6, weight=1)
# rallyFrame.columnconfigure(7, weight=1)
# rallyFrame.columnconfigure(8, weight=1)

# rallyFrame.rowconfigure(0, weight=1)
# rallyFrame.rowconfigure(1, weight=3)
# rallyFrame.rowconfigure(2, weight=2)

# usersFrame.columnconfigure(0, weight=1)
# usersFrame.columnconfigure(1, weight=1)
# usersFrame.columnconfigure(2, weight=1)
# usersFrame.columnconfigure(3, weight=1)
# usersFrame.columnconfigure(4, weight=1)
# usersFrame.columnconfigure(5, weight=1)
# usersFrame.columnconfigure(6, weight=1)

opponentLabel = tkinter.Label(opponentFrame, text="Oppenents Hand");
    
cardBack = PhotoImage(file="./images/cardBackBlue.png")
smallCardBack = cardBack.subsample(5,5);


txtCrib = tkinter.Label(cribFrame, text="Player1's Crib:")
cribCard1 = tkinter.Label(cribFrame, width=50);
cribCard1.img = smallCardBack;
cribCard1.config(image = cribCard1.img);
cribCard2 = tkinter.Label(cribFrame, width=50);
cribCard2.img = smallCardBack;
cribCard2.config(image = cribCard2.img);
cribCard3 = tkinter.Label(cribFrame, width=50);
cribCard3.img = smallCardBack;
cribCard3.config(image = cribCard3.img);
cribCard4 = tkinter.Label(cribFrame, width=50);
cribCard4.img = smallCardBack;
cribCard4.config(image = cribCard4.img);

btnBackToMenu = tkinter.Button(cribFrame, text='Main Menu', command=onClick_MainMenu)

#     # Setting the image this way should prevent garbage collection of the image.
opponentCard1 = tkinter.Label(opponentFrame);
opponentCard1.img = cardBack.subsample(5,5);
opponentCard1.config(image= opponentCard1.img)
opponentCard2 = tkinter.Label(opponentFrame, width=100);
opponentCard2.img = cardBack.subsample(5,5);
opponentCard2.config(image= opponentCard2.img)
opponentCard3 = tkinter.Label(opponentFrame, width=100);
opponentCard3.img = cardBack.subsample(5,5);
opponentCard3.config(image= opponentCard3.img)
opponentCard4 = tkinter.Label(opponentFrame, width=100);
opponentCard4.img = cardBack.subsample(5,5);
opponentCard4.config(image= opponentCard4.img)
opponentCard5 = tkinter.Label(opponentFrame, width=100);
opponentCard5.img = cardBack.subsample(5,5);
opponentCard5.config(image= opponentCard5.img)
opponentCard6 = tkinter.Label(opponentFrame, width=100);
opponentCard6.img = cardBack.subsample(5,5);
opponentCard6.config(image= opponentCard6.img)


currentCountLabel = tkinter.Label(rallyFrame, text="Current Count: 0");
    
playedCard1 = tkinter.Label(rallyFrame, width=75);
playedCard1.img = smallCardBack;
playedCard1.config(image = playedCard1.img);
playedCard2 = tkinter.Label(rallyFrame, width=75);
playedCard2.img = smallCardBack;
playedCard2.config(image = playedCard2.img);
playedCard3 = tkinter.Label(rallyFrame, width=75);
playedCard3.img = smallCardBack;
playedCard3.config(image = playedCard3.img);
playedCard4 = tkinter.Label(rallyFrame, width=75);
playedCard4.img = smallCardBack;
playedCard4.config(image = playedCard4.img);
playedCard5 = tkinter.Label(rallyFrame, width=75);
playedCard5.img = smallCardBack;
playedCard5.config(image = playedCard5.img);
playedCard6 = tkinter.Label(rallyFrame, width=75);
playedCard6.img = smallCardBack;
playedCard6.config(image = playedCard6.img);
playedCard7 = tkinter.Label(rallyFrame, width=75);
playedCard7.img = smallCardBack;
playedCard7.config(image = playedCard7.img);
playedCard8 = tkinter.Label(rallyFrame, width=75);
playedCard8.img = smallCardBack;
playedCard8.config(image = playedCard8.img);

lblCurrentCount = tkinter.Label(rallyFrame, text='Current Count:', font=('Arial',14))

txtGameMessages = tkinter.Text(rallyFrame, width=80, height=5, state='disabled')
txtGameMessages.config(state='normal')
txtGameMessages.insert('1.0', 'Initial Message\n')
txtGameMessages.config(state='disabled')
txtGameMessages.grid(row=2, column=0, columnspan=8, sticky='ews')
scrollbar = tkinter.Scrollbar(rallyFrame, orient=VERTICAL, command=txtGameMessages.yview)
scrollbar.grid(row=2, column=8, sticky='ns')
txtGameMessages.config(yscrollcommand = scrollbar.set)

lblCutPosition = tkinter.Label(rallyFrame, text='Enter card position to cut (1-40)', font=('Arial',14))
def validateNumerical(P):
    if str.isdigit(P) or P == "":
        return True
    else:
        return False

# Create an Entry widget
txtCutPosition = Entry(rallyFrame, validate='all', validatecommand=(validateNumerical, '%P'), font=('Arial',14))
btnCutPosition = tkinter.Button(rallyFrame, text='Cut', font=('Arial',14), command=onClick_btnCutPosition)

cutCardLabel = tkinter.Label(rallyFrame, text='Cut Card', font=('Arial',18))
cutCard = tkinter.Label(rallyFrame, width=150)
cutCardImg = PhotoImage(file="./images/cardClubs_Jack.png")
cutCard.img = cutCardImg.subsample(5,5);
cutCard.config(image = cutCard.img);

btnCountHand = tkinter.Button(rallyFrame, text='Count Hands', font=('Arial',12), command=onClick_CountHand)

playerLabel = tkinter.Label(usersFrame, text="User's Hand");

myCard1 = tkinter.Label(usersFrame, width=100, border=0, relief=SOLID);
myCard1.img = cardBack.subsample(5,5);
myCard1.config(image= myCard1.img)
myCard1.bind("<Button-1>", onclick_Card1)
myCard2 = tkinter.Label(usersFrame, width=100, border=0, relief=SOLID);
myCard2.img = cardBack.subsample(5,5);
myCard2.config(image= myCard2.img)
myCard2.bind("<Button-1>", onclick_Card2)
myCard3 = tkinter.Label(usersFrame, width=100, border=0, relief=SOLID);
myCard3.img = cardBack.subsample(5,5);
myCard3.config(image= myCard3.img)
myCard3.bind("<Button-1>", onclick_Card3)
myCard4 = tkinter.Label(usersFrame, width=100, border=0, relief=SOLID);
myCard4.img = cardBack.subsample(5,5);
myCard4.config(image= myCard4.img)
myCard4.bind("<Button-1>", onclick_Card4)
myCard5 = tkinter.Label(usersFrame, width=100, border=0, relief=SOLID);
myCard5.img = cardBack.subsample(5,5);
myCard5.config(image= myCard5.img)
myCard5.bind("<Button-1>", onclick_Card5)
myCard6 = tkinter.Label(usersFrame, width=100, border=0, relief=SOLID);
myCard6.img = cardBack.subsample(5,5);
myCard6.config(image= myCard6.img)
myCard6.bind("<Button-1>", onclick_Card6)

btnSendToCrib = tkinter.Button(usersFrame, text="Send To Crib", command=onClickSendToCrib)
btnNextHand = tkinter.Button(usersFrame, text="Next Hand", command=onClick_NextHand)
btnPlayCard = tkinter.Button(usersFrame, text="Play Card", command=onClick_PlayCard)
btnGo = tkinter.Button(usersFrame, text="Go", width=100, command=onClick_Go);
btnNewGame = tkinter.Button(usersFrame, text="Another Game", command=onClick_NewGame)
btnReadyToStart = tkinter.Button(usersFrame, text="Ready", command = onClick_ReadyToStart)

opponentFrame.grid_propagate(0)
rallyFrame.grid_propagate(0)
usersFrame.grid_propagate(0)

opponentFrame.grid(row=0, column=0, sticky='news')
rallyFrame.grid(row=1, column=0, sticky='news')
usersFrame.grid(row=2, column=0, sticky='news')


############# Test Lables ####################################
#lblCribFrame = tkinter.Label(cribFrame, text="cribFrame")

lblScoreFrame = tkinter.Label(scoreFrame, text="scoreFrame")
lblAvailableGames = tkinter.Label(availableGamesFrame, text="AvailableGamesFrame")

#lblCribFrame.grid(row=0, column=0, sticky='news')
lblScoreFrame.grid(row=0, column=0, sticky='news')
lblAvailableGames.grid(row=0, column=0, sticky='news')

############## Score Frame ###################################
lblP1DisplayName = tkinter.Label(scoreFrame, font=('Arial',14))
lblP1Score = tkinter.Label(scoreFrame, font=('Arial',14))

lblP2DisplayName = tkinter.Label(scoreFrame, font=('Arial',14))
lblP2Score = tkinter.Label(scoreFrame, font=('Arial',14))


################ Login Widgets ###############################
lblLogin = tkinter.Label(loginFrame, text="Login to Play", font=('Arial',30))
lblEmail = tkinter.Label(loginFrame, text="Email: ", font=('Arial',16))
lblPassword = tkinter.Label(loginFrame, text="Password: ", font=('Arial',16))

###Change default values after development
txtEmail = tkinter.Entry(loginFrame, font=('Arial',16))
txtPassword = tkinter.Entry(loginFrame, show='*', font=('Arial',16))
txtEmail.insert(END,'dean@dean.com')
txtPassword.insert(END,'password')

btnLogin = tkinter.Button(loginFrame, text='Login', font=('Arial',16), command=onClickLogin)
btnNewUser = tkinter.Button(loginFrame, text='New Player', font=('Arial',16), command=onClickNewUser)
lblErrorMessage = tkinter.Label(loginFrame, font=('Arial',30), fg='red')

lblLogin.grid(row=0, column=0, columnspan=2, sticky='news', pady=20, padx=20)
lblEmail.grid(row=1, column=0, sticky=W, pady=5, padx=5)
txtEmail.grid(row=1, column=1, sticky=W, pady=5, padx=5)
lblPassword.grid(row=2, column=0, sticky=W, pady=5, padx=5)
txtPassword.grid(row=2, column=1, sticky=W, pady=5, padx=5)
btnLogin.grid(row=3, column=0, pady=5, padx=5, sticky='e')
btnNewUser.grid(row=3, column=1, pady=5, padx=5, sticky='w')
lblErrorMessage.grid(row=4, column=0, columnspan=2, pady=5, padx=5)


#lblCribFrame.grid(row=0, column=0, sticky='news', pady=10, padx=10)

lblAvailableGames.pack()

############## LoggedInFrame Widgets ###################
lblWelcomMessage = tkinter.Label(loggedInFrame ,text='Welcome ',font=('Arial',40))
lblPlayCribbageMessage = tkinter.Label(loggedInFrame, text='Start Playing a Cribbage Game!', font=('Arial', 20))
btnVsComputer = tkinter.Button(loggedInFrame, text='New Game against a Computer', command = newVsComputer, font=('Arial',16))
btnVsPlayer = tkinter.Button(loggedInFrame, text='New Game against a Person', command = newVsPlayer, font=('Arial',16))


lblWelcomMessage.grid(row=0, column=0, sticky='news', pady=20, padx=20)
lblPlayCribbageMessage.grid(row=1, column=0, sticky='news', pady=20, padx=20)
btnVsComputer.grid(row=2, column=0, sticky='news', pady=20, padx=20)
btnVsPlayer.grid(row=3, column=0, sticky='news', pady=20, padx=20)

######################### New Player Frame ######################

lblCreatePlayer = tkinter.Label(newPlayerFrame, text="New Player Info", font=('Arial',30))
lblFirstName = tkinter.Label(newPlayerFrame, text="First Name:", font=('Arial',16))
lblLastName = tkinter.Label(newPlayerFrame, text="Last Name:", font=('Arial',16))
lblDisplayName = tkinter.Label(newPlayerFrame, text="Display Name:", font=('Arial',16))
lblEmail = tkinter.Label(newPlayerFrame, text="Email:", font=('Arial',16))
lblNewPlayerPassword = tkinter.Label(newPlayerFrame, text="Password", font=('Arial',16))
lblNewPlayerVerifyPassword = tkinter.Label(newPlayerFrame, text="Verify Password", font=('Arial',16))
btnCreateUser = tkinter.Button(newPlayerFrame,text="Create", command=onClickCreateUser, font=('Arial',16))
btnCancel = tkinter.Button(newPlayerFrame,text="Cancel", command=onClickCancelUser, font=('Arial',16))
lblCreateUserError = tkinter.Label(newPlayerFrame, font=('Arial',16), fg='red')

txtFirstName = tkinter.Entry(newPlayerFrame, font=('Arial',16))
txtLastName = tkinter.Entry(newPlayerFrame, font=('Arial',16))
txtDisplayName = tkinter.Entry(newPlayerFrame, font=('Arial',16))
txtCreateUserEmail = tkinter.Entry(newPlayerFrame, font=('Arial',16))
txtNewPlayerPassword = tkinter.Entry(newPlayerFrame, show='*', font=('Arial',16))
txtNewPlayerVerifyPassword = tkinter.Entry(newPlayerFrame, show='*', font=('Arial',16))

lblCreatePlayer.grid(row=0, column=0, columnspan=2)
lblFirstName.grid(row=1, column=0)
lblLastName.grid(row=2, column=0)
lblDisplayName.grid(row=3, column=0)
lblEmail.grid(row=4, column=0)
lblNewPlayerPassword.grid(row=5, column=0)
lblNewPlayerVerifyPassword.grid(row=6, column=0)
btnCreateUser.grid(row=7, column=0, sticky='e')
btnCancel.grid(row=7, column=1, sticky='w')
lblCreateUserError.grid(row=9, column=0, columnspan=2)

txtFirstName.grid(row=1, column=1)
txtLastName.grid(row=2, column=1)
txtDisplayName.grid(row=3, column=1)
txtCreateUserEmail.grid(row=4, column=1)
txtNewPlayerPassword.grid(row=5, column=1)
txtNewPlayerVerifyPassword.grid(row=6, column=1)



# cribbageBoard = tkinter.Label(scoreFrame);
# boardImg = PhotoImage(file="../images/cribbageboard.png");
# cribbageBoard.img = boardImg;
# cribbageBoard.config(image=cribbageBoard.img);
# cribbageBoard.grid(row=0, column=0, pady=20)


# cribFrame.grid(row=0, column=0, sticky='news')
# playFrame.grid(row=0, column=1, sticky='news')
# scoreFrame.grid(row=0, column=2, sticky='news')
# availableGamesFrame.grid(row=0, column=3, sticky='news')


gameFrame.pack_propagate(0)
#gameFrame.pack()

loginFrame.pack()

window.mainloop()


# root = Tk()
# root.option_add('*tearOff', False)
    
# menuBar = Menu(root)
# root.config(menu = menuBar)
# file = Menu(menuBar)
    
# menuBar.add_cascade(menu=file, label="File")
    
# file.add_command(label='New Game', command = lambda: print('NewGame'))
# file.add_command(label = 'Quit', command = quit)

# cardBack = PhotoImage(file="../images/cardBackBlue.png")
# smallCardBack = cardBack.subsample(5,5);

# menuAndCribFrame = tkinter.Frame(root);
# menuAndCribFrame.config(height=900, width=300, relief=RIDGE);
# menuAndCribFrame.pack(side=LEFT, fill=BOTH, expand=True);
# paddingFrame = tkinter.Frame(menuAndCribFrame)
# paddingFrame.config(height=300, width=300, relief=RIDGE)
# paddingFrame.pack(side=TOP, fill=X)

# #lblMenuCribFrame = tkinter.Label(menuAndCribFrame, text="Menu and crib Frame")
# #lblMenuCribFrame.pack()

# lblPaddingFrame = tkinter.Label(paddingFrame, text="PaddingFrame")
# lblPaddingFrame.pack()

# cribFrame = tkinter.Frame(menuAndCribFrame)
# cribFrame.config(height=300, width=300, relief=RIDGE)
# cribFrame.pack(side=TOP,fill=X)

# lblCribFrame = tkinter.Label(cribFrame, text="CribFrame")
# lblCribFrame.pack()
    
# playFrame = tkinter.Frame(root);
# playFrame.config(height=900, width=900, relief=RIDGE);
# playFrame.pack(side=LEFT, fill=BOTH, expand=True);
    
# scoreFrame = tkinter.Frame(root);
# scoreFrame.config(height=900, width=300, relief=RIDGE);
# scoreFrame.pack(side=LEFT, fill=BOTH, expand=True);
    
# availableGamesFrame = tkinter.Frame(root);
# availableGamesFrame.config(height=900, width=200);
# availableGamesFrame.pack(side=LEFT, fill=BOTH, expand=True)
    
# txtTestDisplay = tkinter.Label(availableGamesFrame, text="Test Message")
# txtTestDisplay.pack()

# lblEmail = tkinter.Label(playFrame, text="Email:")
# txtEmail = tkinter.Entry(playFrame)
    
# lblPassword = tkinter.Label(playFrame, text = "Password:")
# txtPassword = tkinter.Entry(playFrame)
    
# btnLogin = tkinter.Button(playFrame, text = "Login")
    
# lblEmail.grid(row = 0, column=0, pady=5)
# txtEmail.grid(row = 0, column=1, pady=5)
# lblPassword.grid(row = 1, column=0, pady=5)
# txtPassword.grid(row=1, column=1, pady=5)
# btnLogin.grid(row=2, columnspan=2, pady=5)
# root.mainloop();

# # hub_connection = HubConnectionBuilder()\
# # .with_url(hubAddress, options={"verify_ssl": False})\
# # .build()
    
# # hub_connection.on("ReceiveMessage", lambda msg: print("received message back from hub." + msg[0]))
# # hub_connection.start()





# def displayUI():
    

    
    
#     txtCrib = tkinter.Label(cribFrame, text="Player1's Crib:")
#     cribCard1 = tkinter.Label(cribFrame, width=50);
#     cribCard1.img = smallCardBack;
#     cribCard1.config(image = cribCard1.img);
#     cribCard2 = tkinter.Label(cribFrame, width=50);
#     cribCard2.img = smallCardBack;
#     cribCard2.config(image = cribCard2.img);
#     cribCard3 = tkinter.Label(cribFrame, width=50);
#     cribCard3.img = smallCardBack;
#     cribCard3.config(image = cribCard3.img);
#     cribCard4 = tkinter.Label(cribFrame, width=50);
#     cribCard4.img = smallCardBack;
#     cribCard4.config(image = cribCard4.img);
    
#     txtCrib.grid(row=0, column=0, columnspan=2)
#     cribCard1.grid(row=1, column=0);
#     cribCard2.grid(row=1, column=1);
#     cribCard3.grid(row=2, column=0);
#     cribCard4.grid(row=2, column=1);
    
#     opponentFrame = tkinter.Frame(playFrame,height=300, width=900, relief=RIDGE)
#     rallyFrame = tkinter.Frame(playFrame,height=300, width=900, relief=RIDGE)
#     myFrame = tkinter.Frame(playFrame,height=300, width=900, relief=RIDGE)
    
#     opponentFrame.pack(fill=BOTH, expand=True);
#     rallyFrame.pack(fill=BOTH, expand=True);
#     myFrame.pack(fill=BOTH, expand=True);

#     opponentLabel = tkinter.Label(opponentFrame, text="Oppenents Hand");
#     opponentLabel.grid(column=2, row=0, columnspan=2, pady=5, padx=5);
    
#     cardBack = PhotoImage(file="./images/cardBackBlue.png")
#     smallCardBack = cardBack.subsample(5,5);

#     # Setting the image this way should prevent garbage collection of the image.
#     opponentCard1 = tkinter.Label(opponentFrame);
#     opponentCard1.img = cardBack.subsample(5,5);
#     opponentCard1.config(image= opponentCard1.img)
#     opponentCard2 = tkinter.Label(opponentFrame, width=100);
#     opponentCard2.img = cardBack.subsample(5,5);
#     opponentCard2.config(image= opponentCard2.img)
#     opponentCard3 = tkinter.Label(opponentFrame, width=100);
#     opponentCard3.img = cardBack.subsample(5,5);
#     opponentCard3.config(image= opponentCard3.img)
#     opponentCard4 = tkinter.Label(opponentFrame, width=100);
#     opponentCard4.img = cardBack.subsample(5,5);
#     opponentCard4.config(image= opponentCard4.img)
#     opponentCard5 = tkinter.Label(opponentFrame, width=100);
#     opponentCard5.img = cardBack.subsample(5,5);
#     opponentCard5.config(image= opponentCard5.img)
#     opponentCard6 = tkinter.Label(opponentFrame, width=100);
#     opponentCard6.img = cardBack.subsample(5,5);
#     opponentCard6.config(image= opponentCard6.img)
    
#     opponentCard1.grid(row=1, column=0, padx=25);
#     opponentCard2.grid(row=1, column=1, padx=25);
#     opponentCard3.grid(row=1, column=2, padx=25);
#     opponentCard4.grid(row=1, column=3, padx=25);
#     opponentCard5.grid(row=1, column=4, padx=25);
#     opponentCard6.grid(row=1, column=5, padx=25);
    

#     currentCountLabel = tkinter.Label(rallyFrame, text="Current Count: 0");
#     currentCountLabel.grid(row=0, column=3, columnspan=2, pady=5);
    
#     playedCard1 = tkinter.Label(rallyFrame, width=100);
#     playedCard1.img = smallCardBack;
#     playedCard1.config(image = playedCard1.img);
#     playedCard2 = tkinter.Label(rallyFrame, width=100);
#     playedCard2.img = smallCardBack;
#     playedCard2.config(image = playedCard2.img);
#     playedCard3 = tkinter.Label(rallyFrame, width=100);
#     playedCard3.img = smallCardBack;
#     playedCard3.config(image = playedCard3.img);
#     playedCard4 = tkinter.Label(rallyFrame, width=100);
#     playedCard4.img = smallCardBack;
#     playedCard4.config(image = playedCard4.img);
#     playedCard5 = tkinter.Label(rallyFrame, width=100);
#     playedCard5.img = smallCardBack;
#     playedCard5.config(image = playedCard5.img);
#     playedCard6 = tkinter.Label(rallyFrame, width=100);
#     playedCard6.img = smallCardBack;
#     playedCard6.config(image = playedCard6.img);
#     playedCard7 = tkinter.Label(rallyFrame, width=100);
#     playedCard7.img = smallCardBack;
#     playedCard7.config(image = playedCard7.img);
#     playedCard8 = tkinter.Label(rallyFrame, width=100);
#     playedCard8.img = smallCardBack;
#     playedCard8.config(image = playedCard8.img);
    
#     cutCard = tkinter.Label(rallyFrame, width=100)
#     cutCardImg = PhotoImage(file="./images/cardClubs_Jack.png")
#     cutCard.img = cutCardImg.subsample(5,5);
#     cutCard.config(image = cutCard.img);
    
#     playedCard1.grid(row=1, column=0, padx=5);
#     playedCard2.grid(row=1, column=1, padx=5);
#     playedCard3.grid(row=1, column=2, padx=5);
#     playedCard4.grid(row=1, column=3, padx=5);
#     playedCard5.grid(row=1, column=4, padx=5);
#     playedCard6.grid(row=1, column=5, padx=5);
#     playedCard7.grid(row=1, column=6, padx=5);
#     playedCard8.grid(row=1, column=7, padx=5);
    
#     cutCard.grid(row=1, column=8, padx=5)
    
#     outputText = tkinter.Label(rallyFrame, text="Testing output text.Testing output text.Testing output text.Testing output text.Testing output text.Testing output text.Testing output text.", wraplength=600)
#     btnCountHand = tkinter.Button(rallyFrame, text="Count Hands");
    
#     outputText.grid(row=2, column=0, columnspan=7, padx=5, pady=5, sticky=NW);
#     btnCountHand.grid(row=2, column=8, sticky=NW, padx=5, pady=5);
    
#     myLabel = tkinter.Label(myFrame, text="My Hand");
#     myLabel.grid(column=2, row=0, columnspan=2, pady=5, padx=5);
    
    

#     # Setting the image this way should prevent garbage collection of the image.
#     myCard1 = tkinter.Label(myFrame);
#     myCard1.img = cardBack.subsample(5,5);
#     myCard1.config(image= myCard1.img)
    

#     myCard2 = tkinter.Label(myFrame, width=100);
#     myCard2.img = cardBack.subsample(5,5);
#     myCard2.config(image= myCard2.img)
#     myCard3 = tkinter.Label(myFrame, width=100);
#     myCard3.img = cardBack.subsample(5,5);
#     myCard3.config(image= myCard3.img)
#     myCard4 = tkinter.Label(myFrame, width=100);
#     myCard4.img = cardBack.subsample(5,5);
#     myCard4.config(image= myCard4.img)
#     myCard5 = tkinter.Label(myFrame, width=100);
#     myCard5.img = cardBack.subsample(5,5);
#     myCard5.config(image= myCard5.img)
#     myCard6 = tkinter.Label(myFrame, width=100);
#     myCard6.img = cardBack.subsample(5,5);
#     myCard6.config(image= myCard6.img)
    
#     myCard1.grid(row=1, column=0, padx=25);
#     myCard2.grid(row=1, column=1, padx=25);
#     myCard3.grid(row=1, column=2, padx=25);
#     myCard4.grid(row=1, column=3, padx=25);
#     myCard5.grid(row=1, column=4, padx=25);
#     myCard6.grid(row=1, column=5, padx=25);
    
#     btnSendToCrib = tkinter.Button(myFrame, text="Send To Crib");
#     btnNextHand = tkinter.Button(myFrame, text="Next Hand");
#     btnPlayCard = tkinter.Button(myFrame, text="Play Card");
#     btnGo = tkinter.Button(myFrame, text="Go");
    
#     btnSendToCrib.grid(row=2, column=1, padx=5, pady=5);
#     btnNextHand.grid(row=2, column=2, padx=5, pady=5);
#     btnPlayCard.grid(row=2, column=3, padx=5, pady=5);
#     btnGo.grid(row=2, column=4, padx=5, pady=5);
    

#     cribbageBoard = tkinter.Label(scoreFrame);
#     boardImg = PhotoImage(file="../images/cribbageboard.png");
#     cribbageBoard.img = boardImg;
#     cribbageBoard.config(image=cribbageBoard.img);
    
#     lblP1Score = tkinter.Label(scoreFrame, text="Player1 Score: 0");
#     lblP2Score = tkinter.Label(scoreFrame, text="Player2 Score: 0");
    
#     cribbageBoard.pack();
#     lblP1Score.pack();
#     lblP2Score.pack();
    
#     # hub_connection = HubConnection(hubAddress)
#     # hub_connection.build()
#     # hub_connection.on("ReceiveMessage", lambda: print("connected to hub, received message back"))
#     # hub_connection.start()
    

# #displayUI()
