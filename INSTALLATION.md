# Infinity Horizon Racing - Installation & Setup Guide

## System Requirements

### Windows PC
- **OS**: Windows 10/11 64-bit
- **Processor**: Intel i5 8th gen or AMD Ryzen 5 2600 equivalent
- **RAM**: 8GB minimum (16GB recommended)
- **GPU**: NVIDIA GTX 1050 or AMD RX 560 (1GB VRAM minimum)
- **Storage**: 10GB SSD space
- **DirectX**: Version 12
- **Unity**: 2022.3 LTS or later

### Android
- **OS**: Android 8.0 (API 26) or later
- **RAM**: 4GB minimum (6GB+ recommended)
- **Storage**: 2GB free space
- **GPU**: Qualcomm Adreno 509 or equivalent
- **Processor**: Snapdragon 670 or equivalent

## Installation Steps

### Step 1: Install Unity

1. Download [Unity Hub](https://unity.com/download)
2. Install Unity Hub
3. Open Unity Hub → Click "Installs" in the left panel
4. Click "Install Editor"
5. Select **Unity 2022.3 LTS** (or latest LTS version)
6. Choose optional modules:
   - ✅ Windows Build Support
   - ✅ Android Build Support
   - ✅ Android SDK & NDK Tools
7. Click Install (takes 10-20 minutes)

### Step 2: Clone the Repository

**Using Git:**
```bash
git clone https://github.com/v10042998-ops/infinity-horizon-racing.git
cd infinity-horizon-racing
```

**Using GitHub Desktop:**
1. Go to https://github.com/v10042998-ops/infinity-horizon-racing
2. Click "Code" → "Open with GitHub Desktop"
3. Choose local path
4. Click "Clone"

**Using ZIP Download:**
1. Go to https://github.com/v10042998-ops/infinity-horizon-racing
2. Click "Code" → "Download ZIP"
3. Extract the ZIP file

### Step 3: Open Project in Unity

1. Open **Unity Hub**
2. Click **"Add"** button in the top right
3. Navigate to the `infinity-horizon-racing` folder
4. Select it and click **Open**
5. Unity will appear in the list - click it to open
6. Wait for project to load (first time takes 5-10 minutes for package installation)
7. You should see a "Converting Materials" dialog - this is normal, let it complete

### Step 4: Import Required Packages

After opening the project:

1. **TextMesh Pro** (if prompted):
   - Window → TextMesh Pro → Import TMP Essentials
   - Click Import

2. **Verify packages are installed**:
   - Window → General → Package Manager
   - Look for these (should already be listed in manifest.json):
     - Universal RP: 14.0.8+
     - Input System: 1.7.0+
     - TextMesh Pro: 3.0.6+
     - Addressables: 1.21.17+

### Step 5: First Run

1. In Project panel, navigate to: `Assets/Scenes/`
2. Double-click `MainMenu.unity` to open the scene
3. Click the **Play** button (or press Ctrl+P / Cmd+P)
4. You should see the main menu appear

## Troubleshooting

### Project won't open
**Problem**: "Error: Could not find a part of the path"

**Solutions**:
- Ensure you cloned to a path without special characters or spaces
- Try: `C:/Games/infinity-horizon-racing/` (not `C:/Games/My Racing Game/`)
- Restart Unity Hub

### Missing scripts or compilation errors
**Problem**: "The associated script cannot be loaded."

**Solutions**:
1. Window → General → Console (check for errors)
2. Assets → Reimport All
3. Close and reopen Unity
4. Delete `Library/` folder and reopen project

### "Unsupported API for OpenGL" error
**Problem**: Graphics card not supported

**Solution**:
1. Edit → Project Settings → Graphics
2. Remove incompatible APIs
3. Keep only: OpenGL, DirectX 11, DirectX 12

### Package Manager issues
**Problem**: Packages fail to download

**Solution**:
1. Window → General → Package Manager
2. Click the settings ⚙️ icon
3. Click "Reset Packages to Defaults"
4. Wait for reinstall

## Build Instructions

### Build for Windows PC

1. File → Build Settings
2. Scenes in Build section:
   - Click "Add Open Scenes" (or drag scenes)
   - Order: MainMenu, then other scenes
3. Platform: Select "PC, Mac & Linux Standalone"
4. Player Settings → Other Settings:
   - Set Company Name and Product Name
   - Set Version (1.0.0)
5. Click "Build and Run"
6. Choose output folder
7. Wait for build (2-10 minutes)
8. Game will launch automatically

**Output**: `InfinityHorizonRacing.exe`

### Build for Android

#### Prerequisites
1. Open Unity Hub → Android module → install if missing
2. Edit → Preferences → External Tools
3. Set paths for:
   - Android SDK: (auto-detected usually)
   - Android NDK: (auto-detected usually)
   - OpenJDK: (auto-detected usually)

#### Build Steps
1. File → Build Settings
2. Platform: Select "Android"
3. Click "Switch Platform" (takes 1-2 minutes)
4. Player Settings:
   - Company Name: Your company
   - Product Name: Infinity Horizon Racing
   - Version: 1.0.0
   - Bundle Version Code: 1
   - Minimum API Level: API 26
   - Target API Level: API 31+
5. Connect Android device via USB (or use Android emulator)
6. Click "Build and Run"
7. Choose output folder and filename (e.g., `game.apk`)
8. Wait for build (5-15 minutes)
9. App will install and launch on device

**Output**: `game.apk` (app package)

## Configuration

### Graphics Settings

1. Edit → Project Settings → Graphics
2. Current settings are optimized for all platforms
3. To adjust quality:
   - Edit → Project Settings → Quality
   - Select quality level and adjust:
     - Texture Quality
     - Anti-aliasing
     - Shadow Distance

### Input Settings

1. Edit → Project Settings → Input Manager
2. Default controls are configured
3. To customize:
   - Expand Axes
   - Edit existing or add new inputs

### Mobile Settings

1. File → Build Settings → Player Settings (Android)
2. Resolution and Presentation:
   - Set default aspect ratio
   - Enable portrait/landscape as needed
3. Splash Screen: Customize or disable
4. Performance:
   - Set target frame rate (30, 60, 90)
   - Enable/disable vsync

## Performance Optimization

### For Lower-End PCs
1. Edit → Project Settings → Quality
2. Select "Low" quality level
3. Or adjust individually:
   - Shadow Distance: 50
   - Texture Quality: Half Resolution
   - Anti-aliasing: Disabled

### For Mobile
1. File → Build Settings → Player Settings
2. Graphics:
   - Disable: Post-processing, shadows, advanced lighting
   - Resolution: Auto scale (down to 720p)
   - Target FPS: 30

## Verifying Installation

Run this checklist:

- [ ] Project opens without errors
- [ ] MainMenu scene plays
- [ ] Can navigate menus
- [ ] Can see game world in scene
- [ ] Audio manager initializes
- [ ] No missing script errors
- [ ] Physics objects respond to simulation
- [ ] Can build for your target platform

## Next Steps

1. **Play the game**: Open MainMenu scene and press Play
2. **Explore the code**: Check `Assets/Scripts/` to understand systems
3. **Modify settings**: Edit ScriptableObjects in `Assets/ScriptableObjects/`
4. **Create content**: Add new vehicles, races, or world regions
5. **Test on device**: Build for Android/PC and test on target hardware

## Getting Help

- **GitHub Issues**: Report bugs at https://github.com/v10042998-ops/infinity-horizon-racing/issues
- **Documentation**: See ARCHITECTURE.md for system design
- **Contributing**: See CONTRIBUTING.md for development guidelines

## Additional Resources

- [Unity Manual](https://docs.unity3d.com/Manual/)
- [Universal RP Documentation](https://docs.unity3d.com/Manual/universal-render-pipeline.html)
- [Input System Guide](https://docs.unity3d.com/Packages/com.unity.inputsystem@latest/)
- [Physics Documentation](https://docs.unity3d.com/Manual/PhysicsSection.html)

---

**Still having issues?** Check the [GitHub Discussions](https://github.com/v10042998-ops/infinity-horizon-racing/discussions) or open an issue!
