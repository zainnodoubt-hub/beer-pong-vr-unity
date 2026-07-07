# Beer Pong VR

A Unity-based virtual reality Beer Pong game developed as an AR/VR coursework project. The project was built in Unity for Meta Quest-style VR interaction, focusing on physics-based gameplay, hand/controller interaction, teleportation, scoring, timer logic, and a complete playable game loop.

## Repository Name Suggestion

Recommended GitHub repository name:

```text
beer-pong-vr-unity
```

Alternative names:

```text
unity-vr-beer-pong
meta-quest-beer-pong-vr
vr-beer-pong-game
xr-beer-pong-unity
```

## Project Overview

Beer Pong VR is an interactive VR game where the player throws a ball toward cups using Unity physics. The game includes movement support, gameplay UI, score tracking, a timer, ball respawning, and retry/start controls. The project demonstrates core VR development concepts such as XR interaction, physics-based mechanics, user interface integration, audio feedback, and scene management.

## Key Features

- Physics-based ball throwing system
- VR interaction support for Meta Quest-style gameplay
- Hand/controller-based interaction
- Teleportation movement system
- Cup collision and scoring logic
- Timer and score display
- Start and retry user interface
- Ball respawn system
- Audio feedback for gameplay events
- Trajectory guidance for aiming support
- B-button return/reset functionality
- Built Unity scene with interactive game objects, prefabs, scripts, and assets

## Technologies Used

- Unity
- C#
- Unity XR Interaction Toolkit
- Visual Studio
- Meta Quest / VR development workflow
- Unity physics system
- Unity UI system

## Project Structure

The important folders for the Unity project are:

```text
Assets/           Main Unity assets, scenes, scripts, prefabs, materials, audio, and UI
Packages/         Unity package dependencies and package manifest
ProjectSettings/  Unity project settings required to reopen the project correctly
```

Your local Unity folder may also contain generated folders such as:

```text
Library/
Logs/
obj/
.vs/
UserSettings/
BeerPongFinalBuild/
```

These folders are not required for the source repository. They are generated locally by Unity or Visual Studio, or they contain local build output. They should usually be excluded from GitHub using a `.gitignore` file.

## What Should Be Uploaded to GitHub

Upload these files and folders:

```text
Assets/
Packages/
ProjectSettings/
README.md
.gitignore
```

Do not upload these generated/local folders:

```text
.vs/
Library/
Logs/
obj/
UserSettings/
*.csproj
*.sln
```

The `BeerPongFinalBuild/` folder should only be uploaded if you specifically want to share a playable build. For a normal source-code repository, keep the build folder out of GitHub and instead upload builds separately as GitHub Releases.

## How to Open the Project

1. Clone or download the repository.
2. Open Unity Hub.
3. Click **Add** or **Open**.
4. Select the root folder of this project.
5. Open the project with the Unity version used during development.
6. Allow Unity to restore packages if prompted.
7. Open the main game scene from the `Assets/` folder.
8. Press Play in the Unity Editor or build the project for a VR headset.

## How to Play

1. Start the game from the in-game UI.
2. Use VR hand/controller interaction to pick up or throw the ball.
3. Aim toward the cups using the trajectory guide.
4. Score points by landing the ball in the cups.
5. Use the retry option to restart the game.
6. Use the return/reset control where supported.

## Development Notes

This project was developed as a Unity VR coursework project to demonstrate:

- VR interaction design
- Game-object scripting in C#
- Unity physics-based mechanics
- XR locomotion and teleportation
- Game state management
- UI integration in a VR scene
- Audio and feedback systems

## Recommended GitHub Description

```text
A Unity VR Beer Pong game featuring physics-based throwing, XR interaction, teleportation, scoring, timer, audio feedback, and interactive UI.
```

## Author

Zain Shahid

## License

This project is for academic and portfolio use. Add a license file if you want to make the repository open-source.
