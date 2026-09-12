# Contributing to Infinity Horizon Racing

Thank you for your interest in contributing! This document outlines how to contribute to the project.

## Code Standards

### C# Style Guide
- Use PascalCase for class and method names
- Use camelCase for private fields and local variables
- Use UPPER_SNAKE_CASE for constants
- Prefix private fields with underscore: `private float _speed`
- Use meaningful variable names
- Keep methods focused and under 50 lines
- Add XML comments to public methods

### Example:
```csharp
public class VehicleController : MonoBehaviour
{
    /// <summary>
    /// Gets the current vehicle speed in km/h
    /// </summary>
    public float GetSpeed() => _rigidbody.velocity.magnitude;
    
    private float _currentSpeed;
    private const float MAX_SPEED = 300f;
}
```

## Git Workflow

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/your-feature-name`
3. **Make your changes** with descriptive commits
4. **Push to your fork**: `git push origin feature/your-feature-name`
5. **Create a Pull Request** with a clear description

### Commit Messages
- Start with a verb: "Add", "Fix", "Improve", "Refactor"
- Be descriptive: `Add vehicle suspension physics` not `Update code`
- Reference issues: `Fix #123: Improve AI pathfinding`

## Pull Request Process

1. **Description**: Explain what changed and why
2. **Testing**: Describe how to test your changes
3. **Screenshots**: Include before/after for visual changes
4. **Documentation**: Update relevant docs
5. **Tests**: Add unit tests for new functionality

## Areas for Contribution

### High Priority
- [ ] Vehicle physics improvements
- [ ] AI pathfinding optimization
- [ ] Graphics optimization
- [ ] Mobile performance tuning

### Medium Priority
- [ ] New race modes
- [ ] Vehicle customization options
- [ ] Achievement system
- [ ] Leaderboards

### Low Priority (Nice to Have)
- [ ] Multiplayer systems
- [ ] Advanced weather simulation
- [ ] Custom race creator
- [ ] Community features

## Development Environment Setup

1. **Install Unity 2022 LTS** from Unity Hub
2. **Clone the repository**: `git clone https://github.com/v10042998-ops/infinity-horizon-racing.git`
3. **Open project** in Unity
4. **Install packages**: Window → TextMesh Pro → Import TMP Essentials
5. **Open MainMenu scene** and press Play

## Testing Guidelines

### Before Submitting PR
- [ ] Code compiles without errors
- [ ] No new warnings introduced
- [ ] Tested on PC (Windows)
- [ ] Tested on Android if relevant
- [ ] Performance acceptable (60+ FPS on target hardware)
- [ ] No memory leaks (use Profiler)

### Test Checklist
```
Physics Systems:
- [ ] Vehicle accelerates/brakes smoothly
- [ ] Steering responsive and stable
- [ ] Handbrake causes drifting
- [ ] Collision detection working

World Generation:
- [ ] Terrain generates without gaps
- [ ] Roads align properly
- [ ] Chunks load/unload smoothly
- [ ] No performance drops on generation

AI System:
- [ ] AI follows waypoints
- [ ] Difficulty levels work
- [ ] No pathfinding bugs

UI/Input:
- [ ] All buttons responsive
- [ ] Controls configurable
- [ ] Works on both PC and Android
```

## Performance Targets

- **PC (High-end)**: 144+ FPS at 1440p
- **PC (Mid-range)**: 60+ FPS at 1080p
- **Android (High-end)**: 60 FPS at 1080p
- **Android (Mid-range)**: 30-45 FPS at 720p
- **Memory**: <1GB on mobile, <2GB on PC

## Documentation

When contributing:
- Update README if adding new features
- Document new systems in ARCHITECTURE.md
- Add inline code comments for complex logic
- Update CHANGELOG.md

## Code Review

All PRs require review before merging. Reviewers will check:
- Code quality and style
- Performance impact
- Platform compatibility
- Test coverage
- Documentation

## Questions?

Open an issue or start a discussion on GitHub. The community is here to help!

## License

By contributing, you agree that your contributions will be licensed under the MIT License.
