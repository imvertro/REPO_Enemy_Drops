# R.E.P.O Enemy Drops

## Overview

The R.E.P.O Enemy Drops is a mod for the game [R.E.P.O](https://store.steampowered.com/app/3241660/REPO/), This mod drops items at random on death of an enemy. Fully Configurable.

## Dependencies 
- [R.E.P.O-Mod-Library](https://github.com/Lillious-Networks/R.E.P.O-Mod-Library) by Lillious_Networks

## Compatibility
- Supports the latest version of [R.E.P.O](https://store.steampowered.com/app/3241660/REPO/)

- Compatible with [MelonLoader v0.7.0](https://github.com/LavaGang/MelonLoader/releases/tag/v0.7.0) modding framework

- Compatibility may vary with [BepInEx](https://github.com/BepInEx/BepInEx) modding framework using the [BepInEx.MelonLoader.Loader](https://github.com/BepInEx/BepInEx.MelonLoader.Loader) plugin

## Key Features

Enemies Drop Random Items on death. The items and spawn rates per enemy is completely configurable. The items that can be used are below. The config file is default to only drop shop upgrades per each enemy killed.
- Item Cart Medium
- Item Cart Small
- Item Drone Battery
- Item Drone Feather
- Item Drone Indestructible
- Item Drone Torque
- Item Drone Zero Gravity
- Item Extraction Tracker
- Item Grenade Duct Taped
- Item Grenade Explosive
- Item Grenade Human
- Item Grenade Shockwave
- Item Grenade Stun
- Item Gun Handgun
- Item Gun Shotgun
- Item Gun Tranq
- Item Health Pack Large
- Item Health Pack Medium
- Item Health Pack Small
- Item Melee Baseball Bat
- Item Melee Frying Pan
- Item Melee Inflatable Hammer
- Item Melee Sledge Hammer
- Item Melee Sword
- Item Mine Explosive
- Item Mine Shockwave
- Item Mine Stun
- Item Orb Zero Gravity
- Item Power Crystal
- Item Rubber Duck
- Item Upgrade Map Player Count
- Item Upgrade Player Energy
- Item Upgrade Player Extra Jump
- Item Upgrade Player Grab Range
- Item Upgrade Player Grab Strength
- Item Upgrade Player Health
- Item Upgrade Player Sprint Speed
- Item Upgrade Player Tumble Launch
- Item Valuable Tracker
## Example Config Code

{
  "Entities": {
    "Gnome": {
      "Items": {
        "Item Upgrade Player Extra Jump": {
          "Chance": 0.1
        },
        "Item Upgrade Player Grab Range": {
          "Chance": 0.1
        }
      }
    },
  }
}

## Installation
1. Install mod into mod folder
2. If using BepInEx you will need to install the MelonLoader plugin

## Credits
Created by [ImVertro](https://github.com/imvertro) 
Special Thanks to [Lillious](https://github.com/lillious) for using his [Mod Library](https://github.com/Lillious-Networks/R.E.P.O-Mod-Library)