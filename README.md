# Infinity Horizon Racing

A premium, open-world racing game built with Unity and C# for Windows PC and Android.

## Features

- **Realistic Vehicle Physics**: Suspension, weight transfer, grip, drifting, and multiple drivetrain configurations
- **Infinite Procedural World**: Seed-based terrain generation with infinite road systems
- **Multiple Race Modes**: Career, Circuit, Sprint, Time Trial, Checkpoint, Drag, Drift, Highway, Off-road, and Free Roam
- **Intelligent AI Opponents**: Waypoint navigation, overtaking, traffic avoidance, difficulty levels
- **Career Progression**: Vehicle unlocks, upgrades, achievements, and milestones
- **Dynamic Weather & Day-Night Cycles**: Procedural weather transitions with visual effects
- **Vehicle Customization**: Engine, brakes, suspension, gearbox upgrades, and visual customization
- **Cross-Platform**: Optimized for Windows PC and Android with platform-specific controls
- **Professional UI/UX**: Menus, HUD, garage, settings, and progression systems
- **Audio System**: RPM-based engine sounds, tire squealing, collisions, and dynamic music
- **Save System**: Persistent player progress with offline support

## Project Structure

```
Assets/
├── Art/                      # 3D models, materials, textures, VFX
├── Audio/                    # Engine sounds, music, SFX
├── Prefabs/                  # Vehicle, environment, AI, UI prefabs
├── Scenes/                   # Game scenes (Menu, Racing, Garage, etc.)
├── Scripts/                  # All C# source code
│   ├── Core/                 # Game managers, state machines
│   ├── Vehicles/             # Vehicle physics and systems
│   ├── WorldGeneration/      # Procedural terrain and roads
│   ├── Racing/               # Race modes and management
│   ├── AI/                   # AI opponents and pathfinding
│   ├── Traffic/              # Traffic systems
│   ├── UI/                   # UI controllers and managers
│   ├── SaveSystem/           # Save/load functionality
│   ├── Audio/                # Audio management
│   ├── Graphics/             # Visual effects and settings
│   ├── Mobile/               # Android-specific systems
│   └── Utilities/            # Helper classes
├── ScriptableObjects/        # Game configuration and data
├── Settings/                 # Project and quality settings
├── Resources/                # Runtime-loaded resources
└── Tests/                    # Unit and integration tests
```

## System Requirements

### Windows PC
- Unity 2022 LTS or later
- Minimum: GTX 1050, 8GB RAM, SSD
- Recommended: GTX 1080 or better, 16GB RAM

### Android
- Android 8.0 or later
- Minimum: 4GB RAM
- Recommended: 6GB+ RAM, Snapdragon 888 or equivalent

## Installation & Setup

### Prerequisites
1. Install [Unity Hub](https://unity.com/download)
2. Install Unity 2022 LTS
3. Clone this repository:
   ```bash
   git clone https://github.com/v10042998-ops/infinity-horizon-racing.git
   ```

### Project Setup
1. Open Unity Hub
2. Click "Open" → Select the project folder
3. Wait for all packages to import (5–10 minutes on first load)
4. Open the `MainMenu` scene from `Assets/Scenes/`
5. Click Play to start the game

### First Run
- The game generates the procedural world on first play
- Press ESC to open the menu
- Select "Free Roam" to explore or "Career Mode" for guided races
- Use arrow keys (PC) or touch controls (Android) to drive

## Build Instructions

### Windows PC
1. File → Build Settings
2. Select "PC, Mac & Linux Standalone" as target platform
3. Click "Build and Run"
4. Choose output folder and build

### Android
1. File → Build Settings
2. Select "Android" as target platform
3. Ensure "Android SDK Tools" is installed via Unity Hub
4. Click "Build and Run" (requires device or emulator connected)
5. Game will build as APK and install automatically

## Controls

### Windows PC
- **WASD** or **Arrow Keys**: Steering
- **Space**: Brake/Reverse
- **Shift**: Accelerate
- **E**: Handbrake
- **R**: Restart race
- **ESC**: Pause/Menu
- **Controller**: Full Xbox controller support

### Android
- **Left/Right Swipe**: Steering
- **Accelerator Pedal**: Gas (bottom right)
- **Brake Pedal**: Braking (bottom left)
- **Handbrake Button**: Drift assist (top right)
- **Tilt Steering** (optional): Device tilt for steering

## Game Modes

### Career Mode
- 50+ progressively difficult races
- 4 difficulty tiers: Beginner → Professional
- Vehicle unlocks every 5 races
- Championship events with bonus rewards

### Free Roam
- Explore infinite procedural world
- Unlimited driving time
- Earn credits for distance traveled

### Circuit Racing
- Standard lap-based racing
- AI opponents scale to difficulty
- Finish line bonus for clean driving

### Time Trial
- Race against the clock
- Beat personal best times
- Leaderboard rankings (local)

### Drag Racing
- Straight-line acceleration races
- Gear shifting challenges
- Perfect shift bonus rewards

### Drift Challenges
- Score points for drifting
- Combo multipliers for consecutive drifts
- Difficulty-based targets

## Vehicle Classes

- **Sports Cars**: Balanced handling and speed
- **Supercars**: Extreme speed, high cost
- **Muscle Cars**: High acceleration, poor handling
- **Off-Road**: Excellent traction, slower top speed
- **Performance Tuned**: Customizable hybrid vehicles

## Settings & Graphics

### Graphics Presets
- **Ultra**: Maximum visual quality (PC only)
- **High**: High quality with good performance
- **Medium**: Balanced quality and performance
- **Low**: Optimized for mobile and older hardware

### Configurable Settings
- Resolution and aspect ratio
- Shadow quality (Off, Low, Medium, High)
- Texture quality and LOD bias
- Draw distance (chunk loading distance)
- Weather effects intensity
- Motion blur and bloom
- Frame rate cap (30, 60, 144, unlimited)

### Audio Settings
- Master volume
- Engine sound volume
- Music volume
- SFX volume
- Environmental audio toggle

## Save System

Saves are stored locally in:
- **Windows**: `%APPDATA%/InfinityHorizonRacing/`
- **Android**: `/Android/data/com.infinityhorizon.racing/files/`

Each save contains:
- Player profile (name, level, credits)
- Unlocked vehicles and upgrades
- Career progress and race records
- Custom control settings
- Graphics preferences
- World seed for reproducible generation

## Development Roadmap

### Phase 1 (Current)
- ✅ Core vehicle physics
- ✅ Procedural world generation
- ✅ Basic race modes
- ✅ AI opponents
- ✅ Career progression
- ✅ Save system

### Phase 2
- 🔄 Advanced weather system
- 🔄 Vehicle damage visualization
- 🔄 Police pursuit events
- 🔄 Achievements system
- 🔄 Daily challenges

### Phase 3
- ⏳ Multiplayer (local split-screen)
- ⏳ Online multiplayer
- ⏳ Community leaderboards
- ⏳ Custom race creator

## Performance Optimization

### PC Optimization
- Dynamic resolution scaling
- Occlusion culling for terrain chunks
- Level of detail (LOD) system for vehicles
- Texture atlasing and compression
- Object pooling for vehicles and particles

### Mobile Optimization
- Adaptive quality based on frame rate
- Reduced draw calls via instancing
- Mobile-friendly shader variants
- Memory-efficient asset streaming
- Thermal and battery monitoring

### Typical Performance
- **PC (GTX 1080)**: 100+ FPS at 1440p High
- **PC (GTX 1050)**: 60 FPS at 1080p Medium
- **Android (Snapdragon 888)**: 60 FPS at 1080p High
- **Android (Mid-range)**: 30–45 FPS at 720p Medium

## Contributing

Contributions are welcome! Please:

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/your-feature`
3. Commit your changes: `git commit -m "Add your feature"`
4. Push to the branch: `git push origin feature/your-feature`
5. Submit a pull request

See [CONTRIBUTING.md](CONTRIBUTING.md) for detailed guidelines.

## Architecture

The game uses several key architectural patterns:

- **Manager Pattern**: GameManager, UIManager, AudioManager, SaveManager
- **State Machine**: RaceState, VehicleState, GameState
- **Object Pooling**: Vehicles, particles, traffic
- **Observer Pattern**: Event system for race events, collisions
- **Scriptable Objects**: Configuration and data storage
- **Dependency Injection**: Core systems initialized in Startup scene

See [ARCHITECTURE.md](ARCHITECTURE.md) for detailed system diagrams.

## Testing

Run tests via Unity Test Runner:
- Window → General → Test Runner
- All tests in `Assets/Tests/`
- Unit tests for physics, pathfinding, save system

## Known Limitations

- Procedural generation is deterministic but visually varied
- AI uses simplified pathfinding (not true racing line prediction)
- Weather effects are visual (don't affect physics yet)
- Multiplayer is not yet implemented
- Placeholder audio and textures (replace with licensed assets)

## License

This project is licensed under the MIT License—see [LICENSE](LICENSE) for details.

## Support & Issues

Report issues on [GitHub Issues](https://github.com/v10042998-ops/infinity-horizon-racing/issues).

## Credits

**Development Team**:
- Game Architecture & Core Systems
- Vehicle Physics & Handling
- Procedural World Generation
- AI & Pathfinding
- UI/UX Design
- Audio Engineering

**Built with**:
- [Unity](https://unity.com/)
- [Universal Render Pipeline](https://docs.unity3d.com/Manual/universal-render-pipeline.html)
- [Unity Input System](https://docs.unity3d.com/Packages/com.unity.inputsystem@latest)

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for version history and updates.

---

**Latest Version**: 1.0.0-alpha  
**Last Updated**: December 2024

**Ready to drive? Start the game and enjoy Infinity Horizon Racing!**
