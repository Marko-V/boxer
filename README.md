This is a Unity project openable with Unity 2019.3.6f1 Personal. 

It consists of two scenes: the boxes scene and the pathfinding scene. Code is also separated by scene. The game contains no external assets.

The Boxes Scene:
- code is separated into components (visual, monobehaviour) and controllers (functional) parts
- DirectorComponent is the initializing behavior for the scene
- most elements can be repositioned prior to launching the game by moving around spawn points for components
- the robot has the goal of launching boxes into containers and is almost fully automated
- pressing space will make the robot jump

The Pathfinding Scene:
- the Map object holds the initializing behavior for the scene
- elements are somewhat reconfigurable before game start
- pathfinding controls: left mouse click to trace a path
- level draw controls: right mouse click to leave impassable terrain
- other controls: (R)eset the level, (I)nvert the level, +/- increase/decrease brush size
