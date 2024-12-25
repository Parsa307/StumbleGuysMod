# Stumble Guys Mod

This mod for Stumble Guys utilizes MelonLoader.

As of December 25, 2024, development of this mod has been officially discontinued. It was a great experience working on the project, but Scopely’s recent security upgrades have made further progress much more difficult and time-consuming. Additionally, the mod hasn't been updated in over three months, making it no longer feasible to continue development. Thanks to everyone who supported the project. Your support has been truly appreciated!

## Features

* Uses MelonLoader to mod Stumble Guys
* Press `F7` to open the mod menu

## Installation

1. **Download and Install MelonLoader:**
   - Download MelonLoader from [this link](https://github.com/LavaGang/MelonLoader/releases/latest/download/MelonLoader.x64.zip)
   - Extract all files to the game directory

2. **Mod Usage:**
   - Press `F7` in-game to open the mod menu

## Important Note

**Scopley** is known for promoting gambling to kids and allowing their game to be accessed by children. This mod prevents kids from buying skins, which are only available through gambling. If you're a parent, it is recommended to install this mod into your child's game and not disclose how to access the mod.

## Run the Game

1. Launch Stumble Guys and wait for MelonLoader to generate the necessary files.

2. After the files are generated, modify the `Stumble Guys Mod.csproj` file to ensure the correct paths to the generated MelonLoader files are set.

## Build

To build the project, follow these steps:

1. **Install .NET SDK:**
   - Download the .NET SDK from [this link](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.425-windows-x64-installer)
   - If you're using a Linux distribution, refer to the .NET installation guide for your specific distribution.

2. **Clone the Repository and Build:**
   - Open a terminal or command prompt in the project directory and execute:

     ```
     git clone https://github.com/Parsa307/StumbleGuysMod.git
     cd StumbleGuysMod
     cd "Stumble Guys Mod"
     dotnet build -c Release
     ```

And you're done!
