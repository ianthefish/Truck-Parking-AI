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
<img src="https://github.com/ianthefish/Truck-Parking-AI/assets/72810883/bf1a29f2-f603-4f98-8e41-16f1c08ab65b" alt="drawing" width="400"/>\
Then, we write the script for the semi trailer truck, making it fully functional\
<img src="https://github.com/ianthefish/Truck-Parking-AI/assets/72810883/b47577d6-34b1-437d-a501-fc4c93468d2a" alt="drawing" width="350"/>\
### Parking lot:
<img src="https://github.com/ianthefish/Truck-Parking-AI/assets/72810883/47c251dc-cb53-49fc-b544-2db98648430d" alt="drawing" width="400"/>

### Project goal:
In this project, we use different reinforcement learning algorithm to let the truck agent park into the parking spot with collision.


## Prerequisite
- `Python==3.9`
- `tensorflow==2.16.1`
- `PyTorch==2.2.2+cu121`
- `ml-agents==0.30.0`
  
## Usage
### Here's how to play with the project
1. Download Unity project and mlagent tools from [Google drive](https://github.com/ianthefish/Truck-Parking-AI/blob/main/file.md) (Files are too large so we put it on Google drive)
2. `Open Unity project`
3. If you want to train the agent:
   - Use cmd, go to `TruckParking` folder
   - If you want to use PPO algorithm`mlagents-learn.exe .\config\ppo\TruckControl.yaml --run-id=RUNID`
   - If you want to use SAC algorithm`mlagents-learn.exe .\config\sac\TruckControl.yaml --run-id=RUNID`
4. If you want to use trained model, drag [`oxnn`](https://github.com/ianthefish/Truck-Parking-AI/tree/main/TrainedNetwork) into `Model` in the `Behavior Parameter`: ![image](https://github.com/ianthefish/Truck-Parking-AI/assets/72810883/d7a5d78a-1434-4996-95bb-31c137bbe106)
5. Enjoy truck driving~~!

## Results
![Untitled video - Made with Clipchamp (1)](https://github.com/ianthefish/Truck-Parking-AI/assets/72810883/a90a1b93-2b6b-48c8-9c08-bba0c9f591f0) 
![Untitled video - Made with Clipchamp (3)](https://github.com/ianthefish/Truck-Parking-AI/assets/72810883/e2526d7a-dee9-47bd-8c4d-4b1bd22535b4)
<img src="https://github.com/ianthefish/Truck-Parking-AI/assets/72810883/33a1d906-98a9-4a4c-b48a-7f1995ec248b" alt="drawing" width="750"/>


## References
https://assetstore.unity.com/packages/3d/vehicles/land/single-detailed-truck-895
https://github.com/Unity-Technologies/ml-agents
https://www.youtube.com/playlist?list=PLyh3AdCGPTSLg0PZuD1ykJJDnC1mThI42
https://www.tutorialspoint.com/unity/index.htm
https://ieeexplore.ieee.org/document/9590169


