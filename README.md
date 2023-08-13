# Geometry-Dash-Clone
Geometry Dash Clone Unity Game: Giant Avocado Task
## Assets/GameAssets folder is the folder where I work; the other folders are came with initial project or the assets that i downloaded!

# Game Mechanics

The player uses Rigidbody2D for gravity scale and RB.AddForce($something,ForceMode2D.Impulse) for Jumping.

For changing the game mode, I created mode changer prefabs that have OnTriggerEnter2D function and checks if it collided with tag: Player.

# Game Mode Changers

## Slow Move Speed Mode
![alt text](https://github.com/akincemtutal9/Geometry-Dash-Clone/blob/main/Assets/ReadmeFiles!/SlowMoveSpeedTrigger.png)

## Fast Move Speed Mode
![alt text](https://github.com/akincemtutal9/Geometry-Dash-Clone/blob/main/Assets/ReadmeFiles!/FastMoveSpeedTrigger.png)

## Very Fast Move Speed Mode
![alt text](https://github.com/akincemtutal9/Geometry-Dash-Clone/blob/main/Assets/ReadmeFiles!/VeryFastMoveSpeedTrigger.png)

## Gravity Reverse Mode
![alt text](https://github.com/akincemtutal9/Geometry-Dash-Clone/blob/main/Assets/ReadmeFiles!/GravityReverseTrigger.png)

## Ship Mode
![alt text](https://github.com/akincemtutal9/Geometry-Dash-Clone/blob/main/Assets/ReadmeFiles!/GameModeShipTrigger.png)

## Cube Mode
![alt text](https://github.com/akincemtutal9/Geometry-Dash-Clone/blob/main/Assets/ReadmeFiles!/GameModeCubeTrigger.png)

# Dying

## Killer Objects
If Player collides with this objects player will die.

![alt text](https://github.com/akincemtutal9/Geometry-Dash-Clone/blob/main/Assets/ReadmeFiles!/KillTriangle.png)
![alt text](https://github.com/akincemtutal9/Geometry-Dash-Clone/blob/main/Assets/ReadmeFiles!/KillZone.png)

## Player Kill Box
In object below we can observe the blue box in right of white square if that hit something in the scene player dies.

![alt text](https://github.com/akincemtutal9/Geometry-Dash-Clone/blob/main/Assets/ReadmeFiles!/Player.png)



 
