# VR Escape Room

A single-room VR escape puzzle built in Unity, made as a learning project to get hands-on with VR development. Solve a color puzzle to reveal a code, unlock a safe, grab the key, and get through the door before the timer runs out.

*(Türkçe açıklama için: [README.tr.md](README.tr.md))*

## Gameplay

https://github.com/user-attachments/assets/1e3e0abc-7e60-4ec9-bdf3-099e69513c30

## About the project

This is a first project into VR development. The goal was to learn Unity's component system, physics/trigger interactions, and the XR Interaction Toolkit by actually building something instead of just following tutorials.

**Puzzle flow:**
1. Click the colored ball repeatedly to cycle through colors until it lands on the target color, which reveals a code on a nearby note.
2. Enter the code on the keypad.
3. The safe opens, grab the key inside.
4. Bring the key to the door to unlock and open it.
5. Reach the door before the countdown timer runs out to win.

## Features

- Color-matching puzzle (click through colors to find the target one)
- 3-digit keypad with correct/wrong feedback (panel flashes green/red)
- Readable notes scattered around the room with hints
- A safe that opens once the code is entered, with a key hidden inside
- A door that unlocks and slides open once the key is brought to it
- Countdown timer and a win screen showing your time
- Restart button to reset the scene
- Sound effects for interactions (button clicks, picking up the key, correct/wrong code, door opening, winning)
- Dark, atmospheric lighting with a few point lights around key interactables

## Controls (Editor testing)

I don't own a VR headset and I'm on macOS, so I couldn't test with real XR controllers. To still be able to test and iterate on the gameplay, I wrote `EditorTestCamera.cs`, a simple first-person controller that simulates VR-style interaction in the Unity Editor:

- **WASD**: move
- **Right-click + mouse**: look around
- **Left-click**: interact with objects (keypad buttons, notes, the color ball, the key)

The actual XR Interaction Toolkit setup is also in the project for when I do get access to a headset.

## Built with

- Unity 6 (6000.3.10f1)
- Universal Render Pipeline (URP)
- XR Interaction Toolkit
- Unity Input System
- TextMeshPro

## A note on AI assistance

I used AI assistance for a large part of the code and for scene/material work in this project. I'm learning Unity and VR development through this project, and AI helped me understand the editor, how components and scripts connect, and handled a lot of the implementation and small in-scene adjustments I couldn't do myself yet. That said, I followed every step, understand what each script does and why, and the design decisions (the puzzle logic, the game flow, what mechanics to include) are mine.

## Known limitations

- Never tested on real VR hardware, only through the in-editor test camera described above.
- Built and tested on macOS.
- This is a first project, so some things (folder structure, a few hardcoded values) are rougher than they'd be in a more polished build.

## Running the project

1. Open the project with Unity **6000.3.10f1** (or close to it).
2. Open `Assets/Scenes/SampleScene.unity`.
3. Press Play. Use the controls above to move around and interact.
