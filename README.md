# 👻 Body Hop

> **Death is not the end. Hop between two brothers' bodies and rescue a soul from the abyss before time runs out.**

**Body Hop** is a first-person 3D puzzle-platformer made in **Unity** by **Dual Soul Games** for the *Video Game Programming* university course, built around the Game Jam theme *"Death is not the end"*.

[![Watch the tutorial on YouTube](https://img.shields.io/badge/▶_Watch_tutorial-YouTube-red?style=for-the-badge&logo=youtube)](https://youtu.be/cs7xN2K4kso)
![Unity](https://img.shields.io/badge/Unity-3D-black?style=for-the-badge&logo=unity)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![Platform](https://img.shields.io/badge/Platform-PC-blue?style=for-the-badge&logo=windows)

> ℹ️ The game's text and the code are in Spanish.

---

## 📑 Table of Contents

- [Story](#-story)
- [Game Concept](#-game-concept)
- [Core Mechanics](#-core-mechanics)
- [Controls](#️-controls)
- [Levels and Challenges](#️-levels-and-challenges)
- [HUD](#️-hud)
- [Art and Sound](#-art-and-sound)
- [Programming](#-programming)
- [Project Structure](#-project-structure)
- [Third-Party Assets](#-third-party-assets)
- [Use of AI](#-use-of-ai)
- [Team](#-team)

---

## 📖 Story

**A spiritual odyssey.** After a tragic car crash on an ordinary road, two brothers lose their lives. At that very moment, **Death** appears and kidnaps one brother's soul to lock it away in the abyss of the underworld forever.

**The goal.** The remaining brother must beat the challenges Death sets and rescue his brother's soul, cooperating with his own lifeless body through a spiritual bond that goes beyond death.

### Characters

- **🔴 Red Brother / 🟢 Green Brother:** the player's two spiritual vessels. They share the brotherly bond and the colour-polarity mechanics.
- **💀 Death:** the antagonist, and the reason the clock is ticking.

---

## 🎮 Game Concept

Body Hop is a **"single-player co-op"** game: there's no multiplayer and no AI companions. You control both brothers yourself and progress linearly through a series of interconnected logic and spatial puzzles.

⏱️ **Against the clock:** you have **20 minutes** to finish the whole game. If the timer hits zero, your brother's soul is lost and it's *Game Over*.

> *Inspired by first-person spatial puzzle games such as the **Portal** series.*

---

## ✨ Core Mechanics

### 🔄 Soul Transfer
- **Exclusivity:** the soul can only control one body at a time.
- **Lifeless state:** when you switch brothers, the body you leave collapses and lies motionless where it was.
- **Self-cooperation:** place one brother somewhere strategic, switch to the other and use whatever the first one changed in the environment.

### 🎨 Colour Polarity
Both brothers have the same physical abilities but a different polarity: **red** and **green**.
- Buttons, levers and bridges are made of one of the two materials.
- Each brother **can only interact with objects of his colour and walk on structures of his colour**. If the Green Brother steps on a red bridge, he falls straight through it, and vice versa.

---

## ⌨️ Controls

| Input | Action | Behaviour |
|---|---|---|
| `W` `A` `S` `D` | Move | Moves the active brother |
| `Left Ctrl` | Sprint | Increases movement speed |
| `Space` | Jump | Clears gaps and gains height |
| `E` | Interact | Pulls nearby levers and presses buttons |
| `Tab` | Soul transfer | Switches control to the other brother, at any distance |
| `Mouse` | Look | First-person camera |

---

## 🗺️ Levels and Challenges

The game is linear and split into two levels with four challenges of increasing difficulty.

### 🌳 Level 1: The Real World (The Origin)
A realistic version of the stretch of road where the crash happened. It works as an introduction to the controls.

1. **The colour bridge and the maze.** A river blocks the way. A green button raises a red bridge that only the Red Brother can cross. After solving a maze, he presses a red button that turns the bridge green so his brother can join him.
2. **Opening the portal to hell.** A large open area has a red lever and a green lever. Once each brother pulls his lever, a mystical portal to the underworld opens.

### 🔥 Level 2: Hell (The Abyss)
A nightmarish world of giant rocks and rivers of lava. If you fall, you respawn at the start of the puzzle and lose time.

3. **The invisible grid.** A 3 × 9 grid of platforms floats over a sea of lava, and some of them are fake. Only the Green Brother can read the sign with the safe route, so you have to memorise it and guide the Red Brother across. On the other side, a second sign shows the way back for the Green Brother.
4. **Crossed paths and sequential combination.** The brothers split up: one walks down a corridor blocked by 3 doors, while the other reaches a control room with levers that open and close those doors in combination. You have to keep switching to clear the way.

### 🏁 Ending
Both paths meet at a staircase of dark stone leading up to an altar where Death guards the trapped soul. Reaching the top stops the timer and wins the game.

---

## 🖥️ HUD

A minimal HUD that keeps the first-person view clean:

- **Dynamic portrait** (top corner): shows which brother you're controlling, red or green, and switches instantly when you press `Tab`.
- **Timer** (top centre): counts down in `MM:SS` from `20:00`. It doesn't pause during interactions or scene changes.
- **Contextual prompt:** *"Pulsa [E] para interactuar"* ("Press [E] to interact"), which only appears when the active brother is near something interactive.

---

## 🎨 Art and Sound

- **Style:** *low-poly* 3D with sharp edges and clean textures. Visual reference: the clean geometry of *Human: Fall Flat*.
- **Contrast:** the Real World is natural, colourful and realistic; Hell is dark, dominated by glowing orange lava and black rocks.
- **Music:** minimalist piano and strings that convey the loneliness of the spirit world, speeding up in the last minutes of the timer.
- **Sound effects:** the click of levers and buttons, footsteps, and a crisp chime every time the soul switches bodies.

---

## 💻 Programming

- **Dynamic Cinemachine.** Automatic camera priority control (*vCam Priority*) for smooth transitions between first and third person during possession.
- **Polarity filters with tags.** *Triggers* (`OnTriggerEnter`) check whether the player's tag (`Player1` / `Player2`) matches the allowed material colour.
- **Global data persistence.** A static `GameData` class keeps the 20-minute timer and the active character across scenes.
- **Conditional rendering (asymmetry).** GameObjects are enabled or disabled depending on which brother is active, to show or hide the maps on the signs.

---

## 📁 Project Structure

```
Assets/
├── Scenes/
│   ├── EscenaInicial.unity       # Level 1: the Real World
│   └── EscenaInfierno.unity      # Level 2: Hell
├── Scripts/                      # Game logic (C#)
│   ├── CharacterSwitcher.cs      # Soul transfer (Tab)
│   ├── KidsController.cs         # Brothers' controller
│   ├── GameData.cs, TimerHUD.cs  # Global data and timer
│   ├── PuzzleManager.cs, AN_Button.cs, AN_ButtonPuzzle.cs
│   ├── VisibilidadPorPersonaje.cs, PortalScene.cs, PortalParallax.cs
│   └── SpawnReset.cs, VictoriaTrigger.cs, DynamicMenuManager.cs, ...
├── Animations/ · Audio/ · Materials/ · Models/ · Prefabs/
└── Images/ · Sprites/ · Shaders/ · Textures/ · Settings/
```

### ▶️ Opening the Project

1. Clone the repository:
   ```bash
   git clone https://github.com/franra18/Body-Hop-Game.git
   ```
2. Open it with **Unity Hub**.
3. Open `Assets/Scenes/EscenaInicial.unity` and press **Play**.

---

## 📦 Third-Party Assets

| Asset | Author | Used for |
|---|---|---|
| **Kids Character Free** | Sigmoid Button Assets | Red Brother and Green Brother models |
| **Stylized Low Poly Nature Lite** | JustCreate | Natural environment of the Real World |
| **Inferno World Free** | ithappy | Rocks and lava lake in Hell |

---

## 🤖 Use of AI

- **Gemini:** optimising and debugging scripts (body switching with `Tab`, keeping the lifeless bodies in place, timer sync) and support with art direction and music.
- **Unity AI:** generating and texturing key *prefabs*: the **Hell Portal** and the **final cage with the soul**.

---

## 👥 Team

**Dual Soul Games**

| Member | Roles |
|---|---|
| **Francisco Ramírez Cañadas** | Game design, 3D art, sound, level design and production |
| **Marcos Luque Montiel** | Game design, programming, UI and level design |
