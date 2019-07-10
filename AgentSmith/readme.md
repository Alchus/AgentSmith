# Agent Smith
Thomas Sowders 2019

A sandbox for building AI agents for games


## Games

### Tic-Tac-Toe
Simple test for brute minimax search

### Connect 4
A game where brute-forcing the whole game is time-prohibitive, but moderate branching factor.
Used as a place to add efficiency

Added:
* Depth limiting
* Heuristics framework
* Alpha-Beta pruning

Game-specific issues:
* Checking if the game is over is inefficient; could be improved by only checking lines containing the most recent move.


## Framework

### Issues

* No comments at all
* Documentation is useless to other people (or future me)
* MiniMax Agent:
  * Calculating if game.IsOver can duplicate effort from GetScore (as in Connect 4). 
  * Agent doesn't make the "best effort play" if it knows it will lose (all routes to victory get A-B pruned)
    * Add a "Terminal Heuristic" check for the agent to apply to end states, eg +/- for game length. 
* The indirection of cloning the game, then cloning its state, is inefficient. Either make the state part of the game directly, or make the Game class static and refactor to use GameStates
* No multithreading available
  * Use an explicit stack


### Future

* Make Connect 4 performant enough to exceed human  play (in particular, *me*)
  * Cache previously-explored game states
  * Cull states into equivalence classes
* Add some useful heuristics
* Monte Carlo search
* Multiplayer games
  * Esp. free-for-all or "kill the player to your right"
* Symmetric hidden information / expectimax
  * Minesweeper? SORRY?
* Asymmetric hidden information
  * Love Letter
* Cooperative games
* Texas 42 (The dream!)


