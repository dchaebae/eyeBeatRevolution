# eyeBeatRevolution
![alt text](/eyeBeatLogo.jpg)
## Table of Contents
1. [Introduction](#introduction)
  1. [Music with Eyes](#music-with-eyes1)
  2. [Team Collaborators](#team-collaborators2)
  3. [How it is Made](#how-it-is-made3)
2. [GitHub Layout and Navigation](#github-layout-and-navigation)
3. [Hardware Requirements](#hardware-requirements)
4. [Drum Set Design](#drum-set-design)
5. [Acknowledgements](#acknowledgements)

## Introduction
### Music with Eyes
eyeBeat Revolution is a virtual reality (VR) app on which individuals with low hand/arm mobility can play instruments via eye-tracking. The project is aimed at making performance arts (playing music) accessible for people who would normally be unable to participate through conventional means. For instance, patients with certain types of muscular dystrophy affecting the hand and/or arms can learn how to play drums in an immersive VR platform.

## Team Collaborators
The eyeBeat Revolution team consists of three Princeton University undergraduate students enrolled in IW 07: Mobile Computing Design for Assistive Technology.
* Daniel Chae (2020) - 3D Object Design and Instrument Interface
* Leora Huebner (2019) - Music and Feedback
* Ayushi Sinha (2020) - Menu Interface and User Design

### How it is Made 
In essence, eyeBeat Revolution is a "game" built on [Unity](https://unity3d.com/). The 3D environments and objects are rendered on Unity. The main instruments are designed on [Blender](https://www.blender.org/) and imported into Unity. The [Fove 0 VR headset](https://www.getfove.com/) and C# scripts were used to utilize eye-tracking features and implement immersive environments.

## Hardware Requirements
In order to use the Fove 0 VR headset, basic hardware requirements must be met:
* OS: Windows 8.1 64-bit or Windows 10 64-bit
* GPU: NVIDIA GeForce GTX 970 / AMD R9 290 or greater
* HDMI 1.4
* USB 2.0 port x2
* USB 3.0 port
* Memory: 8GB or greater

## GitHub Layout and Navigation
Most of the files that are found here are simply byproducts and caches. The 3D objects and their blender files (and rendered images) can be found in
```sh
eyebeatRevolution/Assets/3dobjects
```
The main C# scripts are found in 
```sh
eyeBeatRevolution/Assets/Scripts
```
Textures and materials can be found in their respective folders in the Assets folder.

## Drum Set Design
The drum set is the main instrument and below is an outline of the design process on Blender.
The final rendering of the drum set was made on Blender Cycles before being imported into Unity
![alt text](/Assets/3dobjects/drumset/finalizedDrumset.png)

The design process for the drums can be seen in the [design process folder](https://github.com/dchaebae/eyeBeatRevolution/tree/master/Assets/3dobjects/drumset/DesignProcess)

## Acknowledgements
We would like to thank the following individuals/groups:
* Professor Kyle Jamieson
* Colleen Kenny
* Fellow IW 07 Students
* Princeton Computer Science Department Faculty
* Princeton Computer Science & BSE Departments (funding)
