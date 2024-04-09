from dataclasses import dataclass, field
import uuid
import json


@dataclass
class User:
    # Id: str 
    # Email: str
    # DisplayName: str
    # FirstName: str
    # LastName: str
    # FullName: str #= field(init=False)
    # LastFirstName: str #= field(init=False)
    # Password: str
    # GamesStarted: int
    # GamesWon: int
    # GamesLost: int
    # WinStreak: int
    # AvgPtsPerGame: float
    
    def __init__(self, j):
        self.__dict__ = json.loads(j)
    

    # def __post_init__(self) -> None:
    #     self.FullName = self.FirstName + " " + self.LastName
    #     self.LastFirstName = self.LastName + ", " + self.FirstName
            
    
        #     public Guid Id { get; set; }
        # public string Email { get; set; }
        # public string DisplayName { get; set; }
        # public string FirstName { get; set; }
        # public string LastName { get; set; }
        # public string FullName { get { return FirstName + " " + LastName; } }
        # public string LastFirstName { get { return LastName + ", " + FirstName; } }
        # public string Password { get; set; }
        # public int GamesStarted { get; set; }
        # public int GamesWon { get; set; }
        # public int GamesLost { get; set;}
        # public int WinStreak { get; set; }
        # public double AvgPtsPerGame { get; set; }


