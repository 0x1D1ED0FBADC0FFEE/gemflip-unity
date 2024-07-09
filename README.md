GemFlip is a turn based board game that I created solely for the purpose of having a project that I can port into different frameworks and contexts I would like to learn.
A version written in C using SDLC exists. 

This version features a client written in C#, using .NET MAUI which will embed the actual game (Unity) shortly. A server to connect two player clients will be written in ASP.NET. A machine learning project (likely supervised learning) will be added to develop a single player AI. 

The graphics are AI generated and arbitrary and there is no financial ambition outside of using the products in job applications.

RULES:

Based on the dimensions of the GameBoard (dynamic in this version) a number of gems are placed on the inner diagonal, they start yellow, means none of the players has flipped them yet. Player colors are red and blue. The objective is to finish the game with more gems flipped than the opponent (winning condition).

To flip a gem, a player needs to have one of their drones traverse the tile that the gem is on. Drones can only be placed on the outer edge tiles of the board, and only one per turn. Each drone moves one tile/turn in their player set direction (up, down, left, right).

To add some complexity, there are currently two game mechanics that determine movement and collision behavior of drones: barriers and phases. The phase of an object (barrier or drone) determines what other objects it can obstruct or interact with. Solid objects collide/interact with solid objects, ether ones collide with other etherreal ones. 

The presence of a barrier on a tile that a drone is about to traverse will cause it to permanently change its direction by 90 degrees clockwise (but only if the drone has the same phase as the barrier, otherwise it will pass through it). Players have a set number of barriers they can place, but in order to place a barrier on a tile one of their drones needs to be present on that tile. a player can only place one barrier per turn.

Phases of any objects can be changed using etherbombs. A player has a set number of those and can use up to one per turn. This can be used defensively or offensively, as a player can change the phase of any drone or barrier on the gameboard if they use an etherbomb, independent from where their own drones are.

When two drones of opposite color meet on a tile, they destroy each other if they have the same phase. In cases where more than two drones converge on a tile, a one-for-one destruction rule will apply. 

Another way to destroy an opponents drone without sacrificing one of their own will be to change the phase of a barrier on a tile into the phase of a drone that is currently on it (which is then in the opposite phase currently, and would traverse it in the next turn).

A drone that has crossed the edge of the board is removed.

The game ends after a set number of turns, in which each player has to place exactly one drone.

In terms of game theory, I do not know if this game would always end in a draw for two perfect players, or if draws between experienced players would be exceedingly common, which would make the game somewhat redundant. Once I get AI players, this may be answered.
