# Windows98-ScreenSaver By Hinan
 ## A Unity3d project and reimplement the Windows98 screensaver with following points:
 
 **1 -** pipes gradually fill a limited three-dimensional space without crossing each other.
 
 **2 -** Each pipe should be given a random new color.
 
 **3 -** When a pipe reaches a dead end, it should be continued with a new pipe elsewhere. 
 
 **4 -** The pipes do not have to look exactly like the original and the bends can be greatly simplified. 
 
 For example, all bends could be represented by a sphere, and all straight parts of the pipes by a cube each.

## Summary of the Process 
**Initialization:** Set initial direction, position, and color.

**Pipe Generation:** The coroutine GeneratePipe handles the timed creation of segments, bends, and color changes. It also handles the logic to avoid overlapping positions and creates a dynamic, growing pipe.

**Creating Bends:** Randomly decide if a bend should occur and then change direction accordingly, while also changing color.

**Handling Overlaps:** If a segment’s position is already occupied, start a new pipe at a random position.

**Scene Reloading:** A function to reload the scene is provided to reset everything and start over.




> This script demonstrates procedural generation using Unity's capabilities, creating dynamic and visually interesting patterns with randomness and real-time updates.







