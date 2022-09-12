# This is a sample Python script.

# Press Shift+F10 to execute it or replace it with your code.
# Press Double Shift to search everywhere for classes, files, tool windows, actions, and settings.
# poker

# def print_hi(name):
# Use a breakpoint in the code line below to debug your script.
# print(f'Hi, {name}') # Press Ctrl+F8 to toggle the breakpoint.


# Press the green button in the gutter to run the script.
# if __name__ == '__main__':
# print_hi('Lyam')
import random
import os
import time


# See PyCharm help at https://www.jetbrains.com/help/pycharm/
class PlayerHand:
    def __init__(self):
        self.player_hand = []
        self.doubled = False


class Card:
    def __init__(self, suit, value):
        self.HEARTS = 1
        self.CLUBS = 2
        self.SPADES = 3
        self.DIAMONDS = 4
        self.valueString = ""
        self.suitString = ""
        self.suit = suit
        self.value = value
        if 2 <= value <= 10:
            self.valueString = f"{value}"
        elif value == 11:
            self.valueString = "Jack"
        elif value == 12:
            self.valueString = "Queen"
        elif value == 13:
            self.valueString = "King"
        else:
            self.valueString = "Ace"
        if suit == self.HEARTS:
            self.suitString = "Hearts"
        elif suit == self.SPADES:
            self.suitString = "Spades"
        elif suit == self.CLUBS:
            self.suitString = "Clubs"
        else:
            self.suitString = "Diamonds"


# def print card
class Deck:
    def __init__(self):
        self.deck = []
        self.NUM_VALUES = 13
        self.NUM_SUITS = 4
        self.NUM_CARDS = 52

        self.initialize()

    def shuffle(self):
        for card in range(len(self.deck)):
            new_pos = card + random.randint(0, len(self.deck) - card - 1)

            temp = self.deck[new_pos]
            self.deck[new_pos] = self.deck[card]
            self.deck[card] = temp

    def initialize(self):
        self.deck.clear()
        for value in range(self.NUM_VALUES):
            for suit in range(self.NUM_SUITS):
                self.deck.append(Card(suit + 1, value + 1))
        self.shuffle()

    def take_card(self):
        return self.deck.pop(0)





# def print_card(card):
#     print(f"----------\n{card.valueString:-^10}\n----of----\n{card.suitString:-^10}\n----------")


# print_card(playerHand[0])

def print_board(printDealerFirstCard):
    print(f"Bankroll: ${bankroll}")
    print("Dealer's Cards")
    for x in range(len(dealerHand)):
        print("----------", end=" ")
    print()
    if (printDealerFirstCard):
        print(f"{dealerHand[0].valueString:-^10}", end=" ")
    else:
        print ("----------", end=" ")
    for x in range(len(dealerHand)-1):
        print(f"{dealerHand[x+1].valueString:-^10}", end=" ")
    print()
    if (printDealerFirstCard):
        print("----of----", end=" ")
    else:
        print ("----------", end=" ")
    for x in range(len(dealerHand) - 1):
        print("----of----", end=" ")
    print()
    if (printDealerFirstCard):
        print(f"{dealerHand[0].suitString:-^10}", end=" ")
    else:
        print ("----------", end=" ")
    for x in range(len(dealerHand)-1):
        print(f"{dealerHand[x+1].suitString:-^10}", end=" ")
    print()
    for x in range(len(dealerHand)):
        print("----------", end=" ")
    print("\n")

    print("Your Cards")

    for i in range(len(playerHands)):
        print(f"Hand {i + 1}:")
        for x in range(len(playerHands[i].player_hand)):
            print("----------", end=" ")
        print()

        for x in range(len(playerHands[i].player_hand)):
            print(f"{playerHands[i].player_hand[x].valueString:-^10}", end=" ")
        print()
        for x in range(len(playerHands[i].player_hand)):
            print("----of----", end=" ")
        print()

        for x in range(len(playerHands[i].player_hand) ):
            print(f"{playerHands[i].player_hand[x].suitString:-^10}", end=" ")
        print()
        for x in range(len(playerHands[i].player_hand)):
            print("----------", end=" ")
        print()

def hand_value(playerHand):
    val = 0
    numA = 0
    for i in range(len(playerHand)):
        if playerHand[i].value == 1:
            numA +=1
        else:
            val += min(playerHand[i].value, 10)
    while numA > 0:
        if val + 11 + numA - 1 <= 21:
            val += 11
            numA -= 1
        else:
            val += 1
            numA -= 1
    return val




    # if (printDealerFirstCard):
    #     print_card(dealerHand[0])
    # else:
    #     print("----------\n----------\n----------\n----------\n----------")
    # for x in range(len(dealerHand) - 1):
    #     print_card(dealerHand[x + 1])
    #     print("\t")



#playerHand.clear()
# playerHand.append(Card(1, 1))
# playerHand.append(Card(1, 1))
while True:
    bankroll = 1000.0
    while bankroll > 0:

        deck = Deck()
        playerHands = []
        dealerHand = []
        bet = 0.0
        os.system('cls')
        while bet == 0:
            bet = input(f"Enter your bet (Max ${bankroll}):\n")
            try:
                bet = float(bet)
            except:
                print("Invalid bet")
                bet = 0.0
            if bet > bankroll:
                print("Your bet is greater than your bankroll. Try a lower bet.")
                bet = 0.0
            elif bet <= 0:
                print("Your bet is too small. Enter a bet greater then 0")
                bet = 0.0
        bankroll -= bet



        playerHands.append(PlayerHand())
        playerHands[0].player_hand.append(deck.take_card())
        playerHands[0].player_hand.append(deck.take_card())

        dealerHand.append(deck.take_card())
        dealerHand.append(deck.take_card())

        # playerHand.clear()
        # playerHand.append(Card(2, 1))
        # playerHand.append(Card(1, 1))

        #print (hand_value())

        def game_play(j):
            os.system('cls')
            print_board(False)
            while hand_value(playerHands[j].player_hand) < 21:
                global leave
                leave = False
                while (True):
                    os.system('cls')
                    print_board(False)
                    print(f"Choose an option for hand {j+1}:\n1. Hit\n2. Stay")
                    splitAllowed = False
                    doubleAllowed = False
                    global bankroll
                    if len(playerHands[j].player_hand) == 2 and initBet <= bankroll:
                        print("3. Double down")
                        doubleAllowed = True
                        if min(playerHands[j].player_hand[0].value, 10) == min(playerHands[j].player_hand[1].value, 10):
                            print("4. Split")
                            splitAllowed = True
                    choice = input()
                    if choice == "1":
                        playerHands[j].player_hand.append(deck.take_card())
                        break
                    elif choice == "2":
                        leave = True
                        break
                    elif doubleAllowed and choice == "3":
                        playerHands[j].player_hand.append(deck.take_card())
                        playerHands[j].doubled = True
                        bankroll -= initBet
                        leave = True
                        break
                    elif splitAllowed and choice == "4":
                        playerHands.append(PlayerHand())
                        playerHands[j+1].player_hand.append(playerHands[j].player_hand.pop(1))
                        playerHands[j].player_hand.append(deck.take_card())
                        playerHands[j+1].player_hand.append(deck.take_card())
                        bankroll -= initBet
                        break
                    else:
                        print("Invalid choice; choose again.")

                if leave:
                    break


        currentHand = 1
        initBet = bet
        while currentHand <= len(playerHands):
            game_play(currentHand-1)
            currentHand += 1
        print_board(True)
        time.sleep(6.5)
        while hand_value(dealerHand) < 17:
            dealerHand.append(deck.take_card())
            os.system('cls')

            print_board(True)
            time.sleep(6.5)
        for j in range (len(playerHands)):
            if playerHands[j].doubled:
                bet += initBet
            if hand_value(playerHands[j].player_hand) > 21:
                print(f"Hand {j + 1}: Bust, you lose.\n")
            elif hand_value(playerHands[j].player_hand) == 21:
                print(f"Hand {j + 1}: Blackjack!")
                if hand_value(dealerHand) == 21:
                    print("Sorry, the dealer also has blackjack, push\n")
                    bankroll += bet
                else:
                    bankroll += bet + bet * 1.5
                    print()
            else:
                if hand_value(dealerHand) > 21 or hand_value(playerHands[j].player_hand) > hand_value(dealerHand):
                    print(f"Hand {j + 1}: You win!\n")
                    bankroll += bet + bet
                elif hand_value(playerHands[j].player_hand) == hand_value(dealerHand):
                    print(f"Hand {j + 1}: Push\n")
                    bankroll += bet
                else:
                    print(f"Hand {j + 1}: Sorry, you lose\n")
            bet = initBet
            time.sleep(6.5)
    if input("Sorry, you lost all your money. Enter q to quit or anything else to restart\n") == "q":
        break

# myDeck = Deck()
# for i in range(3):
#     print(f"suit:{myDeck.deck[i].suitString}\nvalue:{myDeck.deck[i].valueString}")
# print()
# for i in range(3):
#     print(f"suit:{myDeck.deck[i].suitString}\nvalue:{myDeck.deck[i].valueString}")
# myDeck.shuffle()
# print()
# for i in range(3):
#     print(f"suit:{myDeck.deck[i].suitString}\nvalue:{myDeck.deck[i].valueString}")
# print()
# for i in range(3):
#     print(f"suit:{myDeck.deck[i].suitString}\nvalue:{myDeck.deck[i].valueString}")
#
# myCard = myDeck.take_card()
# print("hi " + myCard.suitString + "  " + myCard.valueString)
