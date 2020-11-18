# LiquidAPI

A tModLoader library mod for creating custom modded liquids.

## Building
Vanilla Terraria liquids use unsafe code, so LiquidAPI has to as well. By default, the in-game mod builder does not allow unsafe code, so you'll have to either
A) Patch your game to accept code with unsafe blocks
B) Build using the solution in VS (recommended)
  - Use the release setup unless you want some extra debugging bloat to be enabled
  - As a note, if the mod is enabled in game, the command line built system won't be able to create a new `.tmod` file and will keep giving you error code 1s. Disable the mod in-game, reload, then build, then reload your mods menu, enable LiquidAPI, reload, and you should be good to go.

## Usage
Check out the [wiki](https://github.com/TUA-Team/LiquidAPI/wiki) for more instructions on how to get started.
