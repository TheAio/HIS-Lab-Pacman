### Player character
- [x] Can be moved by player
	- [x] Left
	- [x] Right
	- [x] Up
	- [x] Down
- [x] Cant move through walls
- [x] Loops around when going outside the edge of the screen
### Ghosts
- [x] Moves through the maze at random
- [x] Cant move through walls
- [ ] Can be turned between enemy and pray state
	- [ ] Has a different graphical representation for each state
	- [ ] Is "killed" when colliding with pacman as prey, but not as enemy
	- [ ] Is reset to start position when killed
### Coins & Score
- [ ] Player gains points when pacman collides with coins
- [ ] When the last coin is eaten the level should reset
	- [ ] but the score and health should remain
### Candy
- [ ] Pacman can "eat" candy
- [ ] Eating candy turns the ghost from enemies to pray
	- [ ] for a set amount of time, then they turn back to enemies
### Health
- [ ] Player loose health when colliding with enemy ghosts
- [ ] Loosing health should reset the player to starting possition
- [ ] When health is 0, the game restarts completely
### Level
- [x] Walls that neither the player or the ghosts can move through
- [x] Is loaded from a text-file
### Others and bonus
- [ ] All of the above elements has a functional graphical representation
- [x] At least one of the bonus features
	- [x] Animate the ghosts,
		- [x] and have pacman face in the movement direction
	- [ ] When ghosts and player is reset to start position, make them invulnerable 
		- [ ] and unable to move for a set amount of time
	- [ ] Add a highscore that is shown when the game is lost (health = 0) 
		- [ ] and is saved between sessions (written to a file)