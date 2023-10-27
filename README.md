# TDShooter
TopDown Shooter Source Files

A small 2D Pixel Art bullet-hell style action shooter I developed in my spare time. The objective is to beat the level without losing all your health, kill the boss, and get as high a score as possible!


[WASD] to move

[L SHIFT] to shoot

[ENTER] to dodge

['] to melee

[;] to heal

dodge while healing to "break" enemy bullets.

Destroy enemies to increase your score. A high score multiplier multiplies your damage, but taking damage decreases the mutiplier. Use a melee attack to build energy, which you can spend to dodge or heal your missing HP. You can spend all your energy to destroy all enemy bullets for a few seconds of relief.


You can also use a xbox/playtation controller, in which case the controls are as follows:

Left analog stick to move.

Right trigger to shoot.

East face button to dodge.

West face button to melee.

North face button to heal.

Download and play here: https://sias888.itch.io/starbound

## Level Design

The game features a single level that acts as both a tutorial and a challenge. Level design is based on an enemy-wave structure, where a collection of enemies is sequentially spawned until the requirements of the given wave is satisfied. The enemies stay on the screen for an alotted time, activate their respective AI, then when they have exhausted their bullet firing pattern, move downwards offscreen. Once all enemies in a given wave are offscreen, the wave ends end the next wave starts. If the player destroys all the enemies in a given wave before they independently move themselves offscreen, the wave ends early and the next wave spawns. This way a limited number of enemies can be on screen at a time, and players are given the choice to take their time and destroy enemies at their pace, or to risk taking damage to kill enemies quickly and improve their score.

The level structure attempts to ease the player into the game with a sequence of 1 and 2-enemy waves in the beginning. After the player has developed fluency with the controls, movement, combat, and mechanics, more complex enemies and waves spawn. The level culmintes with a boss encounter once all enemy waves are completed.
