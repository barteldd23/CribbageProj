from textwrap import fill
from tkinter import *
from tkinter import ttk
import tkinter
from tkinter import messagebox
from tkinter.tix import COLUMN
from turtle import bgcolor
from dataclasses import dataclass, field, asdict
import json
import uuid


from signalrcore.hub_connection_builder import HubConnectionBuilder
#

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

pythonUser = CribbageUser()
        
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
        
def newVsComputer():
    pass

def newVsPlayer():
    pass

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

    #messagebox.showinfo(message='email: ' + email + ' password: ' + password)


############### Hub Connection ###########################

# #hubAddress = "https://bigprojectapi-300089145.azurewebsites.net/CribbageHub"
hubAddress = "https://localhost:7186/CribbageHub"

hub_connection = HubConnectionBuilder()\
.with_url(hubAddress, options={"verify_ssl": False})\
.build()
    
hub_connection.on("ReceiveMessage", lambda msg: print("received message back from hub." + msg[0]))
hub_connection.on("LogInAttempt", lambda data: receivedLogInMessage(data[0],data[1],data[2]))
hub_connection.on("CreateUserAttempt", lambda data: receivedCreateUserMessage(data[0],data[1]))
hub_connection.start()


################ Set Up Window and Menu Bar ####################
window = Tk()
window.option_add('*tearOff', False)
window.geometry('1400x900')
window.title('Cribbage Game App')

#window.columnconfigure(0,weight=1)
# window.columnconfigure(1,weight=4)
# window.columnconfigure(2,weight=1)
# window.columnconfigure(3,weight=1)
#window.rowconfigure(0, weight=1)

menuBar = Menu(window)
window.config(menu = menuBar)
file = Menu(menuBar)
    
menuBar.add_cascade(menu=file, label="File")
    
file.add_command(label='New Game', command = lambda: print('NewGame'))
file.add_command(label = 'Quit', command = quit)


############ Main Frames ###############
loginFrame = tkinter.Frame(bg='blue')
gameFrame = tkinter.Frame(bg='#333333')
loggedInFrame = tkinter.Frame(bg='blue')
newPlayerFrame = tkinter.Frame(bg='blue')

#################### gameFrame Frames
cribFrame = tkinter.Frame(gameFrame, width=200, height=900, relief=RIDGE, bg='#333333')
playFrame = tkinter.Frame(gameFrame, width=800, height=900, relief=RIDGE, bg='blue')
scoreFrame = tkinter.Frame(gameFrame, width=200, height=900, relief=RIDGE, bg='green')
availableGamesFrame = tkinter.Frame(gameFrame, width=200, height=900, relief=RIDGE, bg='red')


# playFrame.rowconfigure(0, weight=1)
# playFrame.rowconfigure(1, weight=1)
# playFrame.rowconfigure(2, weight=1)
cribFrame.columnconfigure(0, weight=1)
playFrame.columnconfigure(0, weight=1)
scoreFrame.columnconfigure(0, weight=1)
availableGamesFrame.columnconfigure(0, weight=1)
loginFrame.columnconfigure(0,weight=1)

# scoreFrame.columnconfigure(0, weight=1)


cribFrame.pack(side='left')
playFrame.pack(side='left')
scoreFrame.pack(side='left')
availableGamesFrame.pack(side='left')


##################### playFrame Frames #####################
opponentFrame = tkinter.Frame(playFrame, height=300, relief=RIDGE, bg='pink')
rallyFrame = tkinter.Frame(playFrame, height=300, relief=RIDGE, bg='orange')
usersFrame = tkinter.Frame(playFrame, height=300, relief=RIDGE, bg='purple')

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

opponentFrame.grid(row=0, column=0, sticky='news')
rallyFrame.grid(row=1, column=0, sticky='news')
usersFrame.grid(row=2, column=0, sticky='news')


############# Test Lables ####################################
lblCribFrame = tkinter.Label(cribFrame, text="cribFrame")

lblScoreFrame = tkinter.Label(scoreFrame, text="scoreFrame")
lblAvailableGames = tkinter.Label(availableGamesFrame, text="AvailableGamesFrame")


################ Login Widgets ###############################
lblLogin = tkinter.Label(loginFrame, text="Login to Play", font=('Arial',30))
lblEmail = tkinter.Label(loginFrame, text="Email: ", font=('Arial',16))
txtEmail = tkinter.Entry(loginFrame, font=('Arial',16))
lblPassword = tkinter.Label(loginFrame, text="Password: ", font=('Arial',16))
txtPassword = tkinter.Entry(loginFrame, show='*', font=('Arial',16))
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


lblCribFrame.grid(row=0, column=0, sticky='news', pady=10, padx=10)

lblAvailableGames.pack()

############## LoggedInFrame Widgets ###################
lblWelcomMessage = tkinter.Label(loggedInFrame ,text='Welcome ',font=('Arial',30))
btnVsComputer = tkinter.Button(loggedInFrame, text='New Game against a Computer', command = newVsComputer)
btnVsPlayer = tkinter.Button(loggedInFrame, text='New Game against a Person', command = newVsPlayer)


lblWelcomMessage.pack()
btnVsComputer.pack()
btnVsPlayer.pack()

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
