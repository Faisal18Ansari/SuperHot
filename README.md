# SUPERHOT Time Mechanic Clone

> "Time moves only when you move" - A Unity recreation of SUPERHOT's iconic time manipulation mechanic with advanced AI and physics-based combat.

## Overview

This project recreates the core SUPERHOT mechanic in Unity, featuring sophisticated AI state machines, smooth time scaling, and physics-based shooting. It goes beyond basic tutorials to implement production-quality systems with clean, modular architecture.

## Features

### Core Time Mechanic
- **Smooth Time Scaling**: Gradual transitions between slow-motion (0.05x) and normal speed (1.0x)
- **Input Filtering**: Only gameplay-relevant keys trigger time acceleration
- **Physics Synchronization**: Proper `fixedDeltaTime` adjustment prevents physics glitches

### Advanced AI System
- **StateMachineBehaviour Architecture**: Modular AI states for clean, maintainable code
- **Multi-State Behavior**: Idle → Walking → Chasing → Attacking with distance-based transitions
- **NavMesh Pathfinding**: Intelligent enemy navigation with waypoint-based patrolling
- **Detection Zones**: Layered AI awareness (2.5f attack, 13f detection, 21f stop chasing)

### Combat System
- **Physics-Based Shooting**: Realistic bullet trajectories with gravity and collision
- **Screen-to-World Targeting**: Accurate shooting from camera center to world space
- **Modular Damage System**: Tag-based collision detection with health management
- **Weapon Spread**: Realistic shooting inaccuracy for balanced gameplay

### Technical Implementation
- **Modular Player Systems**: Separated Input, Movement, Look, and Combat components
- **Sound Integration**: Centralized audio management with singleton pattern
- **Performance Optimization**: Efficient bullet lifecycle and memory management
- **Debug Visualization**: Gizmo-based AI range debugging

## Project Structure

```
Assets/
├── Scripts/
│   ├── AI States/
│   │   ├── Idle.cs
│   │   ├── Walking.cs
│   │   ├── Chasing.cs
│   │   └── Attacking.cs
│   ├── Player/
│   │   ├── InputManager.cs
│   │   ├── PlayerMotor.cs
│   │   ├── MouseMovement.cs
│   │   └── PlayerHealth.cs
│   ├── Combat/
│   │   ├── Weapon.cs
│   │   └── Bullet.cs
│   ├── Enemy/
│   │   └── EnemyRed.cs
│   └── Core/
│       ├── TimeController.cs
│       └── SoundManager.cs
├── Prefabs/
├── Materials/
└── Scenes/
```

## Getting Started

### Prerequisites
- Unity 2022.3 LTS or newer
- Basic understanding of Unity components and scripting

### Setup Instructions

1. **Clone the Repository**
   ```bash
   git clone https://github.com/Faisal18Ansari/SuperHot.git
   cd superhot-clone
   ```

2. **Open in Unity**
   - Open Unity Hub
   - Click "Add" and select the project folder
   - Open the project

3. **Scene Setup**
   - Open the main scene from `Assets/Scenes/`
   - Ensure NavMesh is baked (Window → AI → Navigation → Bake)

4. **Configure Input**
   - Check `TimeController` component on TimeManager GameObject
   - Adjust `AllowedKeys` list as needed (default: WASD, Mouse buttons)

### Controls
- **WASD**: Movement (triggers normal time)
- **Mouse**: Look around (triggers normal time)  
- **Left Click**: Shoot
- **Space**: Jump

## Key Components

### TimeController.cs
The heart of the time manipulation system. Handles smooth time scaling and physics synchronization.

```csharp
// Core time control logic
foreach (KeyCode key in AllowedKeys)
    if (Input.GetKey(key)) {
        Time.timeScale = Mathf.MoveTowards(Time.timeScale, 1, timeScaleAccelrationSpeed);
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
```

### AI State Machine
Each AI state is implemented as a separate `StateMachineBehaviour`:
- **Idle.cs**: Base state with player detection
- **Walking.cs**: Random waypoint patrolling
- **Chasing.cs**: Player pursuit with NavMesh
- **Attacking.cs**: Close-range combat behavior

### Physics-Based Combat
Realistic bullet system with trajectory calculation:

```csharp
// Screen center to world point shooting
Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
Vector3 targetPoint = Physics.Raycast(ray, out hit) ? hit.point : ray.GetPoint(100);
```

## Customization

### Time Scaling
Adjust time parameters in `TimeController`:
- `timeScaleAccelrationSpeed`: How fast time transitions occur
- Modify `AllowedKeys` list to change which inputs trigger time acceleration

### AI Behavior
Tune AI parameters in individual state scripts:
- Detection ranges in `Idle.cs` and `Walking.cs`
- Chase and attack distances in `Chasing.cs` and `Attacking.cs`
- Movement speeds and timers

### Combat System
Modify weapon behavior in `Weapon.cs`:
- `bulletVelocity`: Projectile speed
- `spreadIntensity`: Shot accuracy
- `shootingDelay`: Fire rate

## Technical Insights

### Physics Synchronization
The critical line most tutorials miss:
```csharp
Time.fixedDeltaTime = 0.02f * Time.timeScale;
```
This prevents physics glitches by keeping collision detection in sync with time scaling.

### Input Filtering
Using `AllowedKeys` prevents accidental time acceleration from UI interactions, maintaining game flow integrity.

### Modular Architecture
Component-based design allows for easy debugging, testing, and feature expansion without monolithic scripts.

## Known Issues & Solutions

- **Enemy Sliding**: Ensure animation speeds match NavMesh agent speeds
- **Bullet Phasing**: Verify `fixedDeltaTime` is being updated with `timeScale`
- **Performance**: Consider object pooling for bullets in extended gameplay


## Contact

- **Blog**: [Your Blog URL]
- **GitHub**: [@yourusername](https://github.com/yourusername)
- **Email**: your.email@example.com

---

**Note**: This is a learning project and tech demo. All rights to SUPERHOT intellectual property belong to SUPERHOT Team.
