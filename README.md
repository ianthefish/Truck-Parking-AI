# Truck-Parking-AI
## Introduction
Truck-Parking-AI is automatic truck parking agent using Reinforcement Learning.\
3D environment is built With Unity.
### Group members
- 111550056 陳晉祿
- 111550150 俞祺譯
- 111550047 黃昱承
- 111550137 林宏儒
## Overview
### Semi-trailer truck: 
Model downloaded from: https://assetstore.unity.com/packages/3d/vehicles/land/single-detailed-truck-895
![image](https://github.com/ianthefish/Truck-Parking-AI/assets/72810883/bf1a29f2-f603-4f98-8e41-16f1c08ab65b)
![螢幕擷取畫面 2024-06-11 001951](https://github.com/ianthefish/Truck-Parking-AI/assets/72810883/b47577d6-34b1-437d-a501-fc4c93468d2a)
### Parking lot:
![螢幕擷取畫面 2024-06-10 223749](https://github.com/ianthefish/Truck-Parking-AI/assets/72810883/47c251dc-cb53-49fc-b544-2db98648430d)
### Project goal:
In this project, we use different reinforcement learning algorithm to let the truck agent park into the parking spot.
![Untitled video - Made with Clipchamp (1)](https://github.com/ianthefish/Truck-Parking-AI/assets/72810883/a90a1b93-2b6b-48c8-9c08-bba0c9f591f0) 
![Untitled video - Made with Clipchamp (3)](https://github.com/ianthefish/Truck-Parking-AI/assets/72810883/e2526d7a-dee9-47bd-8c4d-4b1bd22535b4)

## Prerequisite
- Python==3.9
- tensorflow==2.16.1
- PyTorch==2.2.2+cu121
- ml-agents==0.30.0
  
## Usage
### Here's how to play with the project
1. Download Unity project and mlagent tools from: [file](https://github.com/ianthefish/Truck-Parking-AI/blob/main/file.md)
2. `Open Unity project`
3. If you want to train the agent:
   - Use cmd, go to `TruckParking` folder
   - If you want to use PPO algorithm`mlagents-learn.exe .\config\ppo\TruckControl.yaml --run-id=RUNID`
   - If you want to use SAC algorithm`mlagents-learn.exe .\config\sac\truck.yaml --run-id=RUNID`
4. If you want to use trained model, drag `oxnn` into `Model` in the `Behavior Parameter`: ![image](https://github.com/ianthefish/Truck-Parking-AI/assets/72810883/d7a5d78a-1434-4996-95bb-31c137bbe106)
5. Enjoy truck driving~~!


