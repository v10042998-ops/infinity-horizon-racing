# Infinity Horizon Racing - Architecture Guide

## System Architecture

### Core Systems

#### GameManager (Core/GameManager.cs)
- Singleton pattern for global game state
- Handles pause/resume
- Manages scene loading
- Integrates SaveSystem and AudioManager

#### SaveSystem (SaveSystem/SaveSystem.cs)
- Persistent player data storage
- JSON-based serialization
- Cross-platform save paths (Windows/Android)
- Supports multiple save slots
- Data includes: profile, vehicles, upgrades, race records

#### AudioManager (Audio/AudioManager.cs)
- Centralized audio control
- Music and SFX channels
- Dynamic engine sound pitch based on RPM
- Volume control for all audio types
- Pause/resume functionality

### Vehicle System

#### VehicleController (Vehicles/VehicleController.cs)
- Realistic physics using Unity WheelColliders
- Multi-platform input handling (PC keyboard/controller, Android touch)
- Engine simulation with RPM and gear system
- Realistic suspension and tire friction
- Damage effects placeholder
- Audio-synchronized engine sounds

**Physics Model:**
- Wheel friction curves for grip simulation
- Suspension spring and damper values
- Weight transfer through center of mass
- Motor torque distribution (30% front, 70% rear for FWD hybrid)
- Aerodynamic drag calculation

### World Generation

#### ProceduralWorldGenerator (WorldGeneration/ProceduralWorldGenerator.cs)
- Infinite procedural terrain using Perlin noise
- Chunk-based loading/unloading system
- Deterministic generation via seed
- Road network generation (cardinal directions)
- Dynamic chunk management based on player position

**Implementation:**
- World divided into chunks (100x100 units)
- Each chunk generates unique terrain mesh
- Roads placed on cardinal axes every 2 chunks
- LOD system for distant chunks
- Memory pooling for unloaded chunks

### Racing System

#### RaceManager (Racing/RaceManager.cs)
- Manages race modes and states
- Lap tracking and checkpoint detection
- Race timing and scoring
- Multiplayer-ready event system
- Position calculation for leaderboards

**Race Modes:**
1. FreeRoam - Unlimited exploration
2. Circuit - Standard lap racing
3. Sprint - Point-to-point race
4. TimeTrial - Beat the clock
5. Checkpoint - Pass waypoints in order
6. Drag - Straight-line acceleration
7. Drift - Score-based drifting
8. Highway - Traffic avoidance racing
9. OffRoad - Terrain-based racing

### AI System

#### AIOpponent (AI/AIOpponent.cs)
- Waypoint-based pathfinding
- Difficulty levels: Easy (1), Normal (2), Hard (3)
- Speed scaling based on difficulty
- Smooth turning and acceleration
- Configurable stopping distance
- Rubber-banding ready (for dynamic difficulty)

**AI Behavior:**
- Follows waypoint route in order
- Calculates optimal speed for curves
- Avoids sudden direction changes
- Respects stopping points
- Difficulty affects max speed and acceleration

### UI System

#### MainMenuUI (UI/MainMenuUI.cs)
- Button-driven navigation
- Scene transitions
- Settings panel management
- Integrates with GameManager

**Menu Flow:**
```
MainMenu
├── Play (FreeRoam)
├── Career
├── Garage
├── Settings
│   ├── Graphics
│   ├── Audio
│   └── Controls
└── Quit
```

### Input System

#### InputManager (Utilities/InputManager.cs)
- Singleton input handler
- Platform detection (PC vs Android)
- Unified input interface
- Cross-platform control mapping

**PC Inputs:**
- WASD/Arrows: Steering
- Space: Brake
- Shift: Accelerate
- E: Handbrake
- ESC: Pause
- Controller: Full Xbox gamepad support

**Android Inputs:**
- Touch swipe: Steering (left/right)
- On-screen buttons: Gas/Brake/Handbrake
- Tilt sensor: Optional steering assist

## Data Flow

```
InputManager
    ↓
VehicleController → Physics Engine → WheelColliders
    ↓
RaceManager → AI Opponents
    ↓
GameManager (Pause/Resume)
    ↓
AudioManager (Engine sounds, music)
    ↓
SaveSystem (Player progress)
```

## Serialization

### Player Save Data
```json
{
  "playerName": "Player",
  "level": 5,
  "credits": 250000,
  "experience": 4250,
  "unlockedVehicles": [0, 1, 3],
  "vehicleUpgrades": {
    "0": {
      "engineLevel": 2,
      "brakeLevel": 1,
      "suspensionLevel": 0
    }
  },
  "worldSeed": 12345,
  "playtimeSeconds": 3600,
  "raceRecords": [...]
}
```

## Performance Considerations

### PC Optimization
- **LOD System**: Terrain detail decreases with distance
- **Occlusion Culling**: Chunks behind terrain aren't rendered
- **Object Pooling**: Vehicles and particles reused
- **Draw Call Batching**: Road meshes combined where possible
- **Texture Atlasing**: Materials shared across chunks

### Mobile Optimization
- **Reduced Mesh Resolution**: Lower poly count for terrain (20x20 instead of 50x50)
- **Quality Presets**: Low/Medium/High graphics settings
- **Dynamic Resolution**: Scale rendering resolution based on frame rate
- **GPU Instancing**: Repeated objects drawn in single call
- **Memory Pooling**: Aggressive object reuse

## Extending the System

### Adding a New Vehicle
1. Create Vehicle ScriptableObject with stats
2. Assign VehicleController prefab
3. Configure wheel positions and mass
4. Register in VehicleDatabase

### Adding a New Race Mode
1. Create new mode enum value
2. Implement mode-specific logic in RaceManager
3. Create UI for mode selection
4. Add scoring rules

### Adding a New World Region
1. Extend ProceduralWorldGenerator
2. Add region-specific terrain noise
3. Configure region-specific objects
4. Add region-specific weather

## Known Limitations

- AI pathfinding is simplified (not true racing line optimization)
- Weather is visual only (doesn't affect physics yet)
- No dynamic traffic spawning (static waypoint-based)
- Multiplayer not yet implemented
- Physics simplified for mobile (no wheel slip angles)

## Future Enhancements

1. **Advanced Physics**
   - Wheel slip angle simulation
   - Tire temperature effects
   - Fuel consumption

2. **Weather System**
   - Physics-based rain (traction reduction)
   - Dynamic weather transitions
   - Time-of-day lighting

3. **AI Improvements**
   - Optimal racing line calculation
   - Tactical overtaking
   - Dynamic difficulty scaling

4. **Multiplayer**
   - Local split-screen
   - Online multiplayer
   - Leaderboards

5. **Content**
   - More vehicle types
   - Career progression events
   - Customization options
