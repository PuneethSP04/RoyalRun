# Royal Run

A 3D endless runner prototype built in Unity using Cinemachine, DOTween, TextMeshPro, Universal Render Pipeline (URP), and the New Input System.

## Scripts Overview

### Player & Camera (Assets/Scripts/Player/)
* **PlayerController.cs** - Handles player movement using the New Input System and Rigidbody physics, clamping movement within track lane boundaries.
* **PlayerCollisionHandler.cs** - Detects obstacle collisions with cooldown, triggers player hit animations and audio, and penalizes movement speed via LevelGenerator.
* **CameraController.cs** - Smoothly transitions Cinemachine camera FOV via coroutines and triggers speed-up particle VFX based on player velocity changes.

### Procedural Generation (Assets/Scripts/Procedural Gen/)
* **LevelGenerator.cs** - Generates, moves, and recycles procedural track chunks, dynamically scales world speed and Z-axis gravity, and coordinates periodic checkpoint chunks.
* **Chunk.cs** - Handles per-chunk procedural generation by randomly placing fences, collectible apples, and coin chains across designated lanes.
* **CheckPoint.cs** - Detects player arrival at milestone chunks and adds bonus time to the GameManager countdown timer.
* **ObstacleSpawner.cs** - Coroutine-based spawner that continuously instantiates randomized tumbling physics obstacles across the track.
* **ObstacleDestroyer.cs** - Trigger-based cleanup boundary that destroys passing obstacles on contact to optimize memory and performance.

### Obstacles & Physics (Assets/Scripts/)
* **Rock.cs** - Physics obstacle that generates distance-scaled Cinemachine camera impulse shake, collision audio, and particle VFX on impact.

### Pickups (Assets/Scripts/Pickups/)
* **Pickup.cs** - Abstract base class featuring continuous idle rotation, player collision detection, audio playback, and a smooth DOTween collection sequence (rise, spin, shrink, destroy).
* **Apple.cs** - Inherits from Pickup to boost chunk movement speed and trigger camera FOV expansion via LevelGenerator.
* **Coin.cs** - Inherits from Pickup to award score points to the player via ScoreManager upon collection.

### Game Flow & Managers (Assets/Scripts/Managers/)
* **GameManager.cs** - Manages the countdown timer, game-over state, slow-motion effect (`Time.timeScale`), URP Bloom post-processing tint transition, and defeat UI.
* **ScoreManager.cs** - Tracks total collected coins and updates the TextMeshPro score display.
