# TDShooter
TopDown Shooter Source Files
Download and play here: https://sias888.itch.io/starbound
A small 2D Pixel Art bullet-hell style action shooter I developed in my spare time. The objective is to beat the level without losing all your health, kill the boss, and get as high a score as possible!

## Controls

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

## Level Design

The game features a single level that acts as both a tutorial and a challenge. Level design is based on an enemy-wave structure, where a collection of enemies is sequentially spawned until the requirements of the given wave is satisfied. The enemies stay on the screen for an alotted time, activate their respective AI, then when they have exhausted their bullet firing pattern, move downwards offscreen. Once all enemies in a given wave are offscreen, the wave ends end the next wave starts. If the player destroys all the enemies in a given wave before they independently move themselves offscreen, the wave ends early and the next wave spawns. This way a limited number of enemies can be on screen at a time, and players are given the choice to take their time and destroy enemies at their pace, or to risk taking damage to kill enemies quickly and improve their score.

The level structure attempts to ease the player into the game with a sequence of 1 and 2-enemy waves in the beginning. After the player has developed fluency with the controls, movement, combat, and mechanics, more complex enemies and waves spawn. The level culmintes with a boss encounter once all enemy waves are completed, after which the player's score is revealed.

## Enemy Wave Implementation

As explained in the Level Design section, a level is a sequence of enemy waves. The implementation of this structure can be found in the script Assets/Scripts/EnemyPatterns/SimpleEnemyPattern.cs. The script defines a class called SimpleEnemyPattern, which is used to represent a wave. SimpleEnemyPattern contains two lists of Unity GameObjects currentEnemies and enemyPool. enemyPool is a list of predefined handmade enemy gameobjects, and currentEnemies is a list of enemies that will be spawned by the SimpleEnemyPattern. SimpleEnemyPattern selects an arbitrary number of GameObjects from enemyPool and instantiates a copy, adding the copy to the currentEnemies list.

The SimpleEnemyPattern also contains three public coroutine methods used to sequentially move the enemies found in currentEnemies. MoveToMiddle moves all GameObject enemies in currentEnemies to the center of the screen and activates their respective shooting patterns sequentially. EnemyMoveRoutine does the same thing, but also moves the enemies offscreen after a certain amount of time. SideToSide moves the enemies to the middle, activates the shooting patterns, and moves enemies side to side until a certain amount of time has passed, after which the enemies are sent offscreen. This class also contains a method called AnnounceCompletion(), which, using the [Observer Pattern]([url](https://en.wikipedia.org/wiki/Observer_pattern)) alerts all listeners that the current pattern has been completed, meaning all enemies in currentEnemies are either destroyed by the player of moved offscreen and destroyed by the SimpleEnemyPattern class. Using these methods, multiple enemy patterns can be implemented by extending the SimpleEnemyPattern class

One such extention (used most commonly in the level) is found in the class Assets/Scripts/EnemyPatterns/ThreeBasicEnemies.cs. The class contains a public coroutine method called StartSequence. This method sets all GameObjects in currentEnemies to active and runs the EnemyMoveRoutine coroutine, passing currentEnemies as the parameter. Additionally, (again using the Observer Pattern) the class is a listner to the EnemyDeathEventHandler.instance.onEnemyDeath event. When any enemy is destroyed by the player or a class method, ThreeBasicEnemies.OnEnemyDisable() is called. The OnEnemyDisable() method checks whether the destroyed enemy is in currentEnemies, and if all enemies in currentEnemies are destroyed, calls the AnnounceCompletion() method extended from the SimpleEnemyPattern class.

Using this framework, multiple enemy patterns/waves can be created and sequentially destroyed by a wave manager class (Assets/Scripts/EnemyPatterns/Level1EnemyController.cs). When compared to a simple timer-based wave spawning method, this implementation allows for greater player agency, as (in a format with a finite number of waves) a stronger player can be rewarded for efficiently destroying enemies with a quicker next-wave spawn, while a weaker player can take their time in destroying a limited number of enemies without being worried about the next wave spawning before they are ready.

## Enemy Implementation

(WIP)

An enemy is represented in the project as a GameObject with a combination of two classes attached: EnemyHP, and an extension of EnemyAI. EnemyHP handles the health and damage related methods of an enemy unit, and EnemyAI concerns the basic methods and functions universal to all enemies that extend this type. 