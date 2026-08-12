# 007 Level Up

## Goal

When enough experience has been earned, increase the player's level.
**DONE**

## Bonus challenges

1. ~~Increase HP when leveling.~~ **DONE**

2. ~~Allow multiple level-ups.~~ **DONE**

3. ~~Increase the XP requirement for every level.~~ **DONE**

### Personal notes

I accidentally did most of this with the previous exercise already, but I remade the whole thing from scratch for practice. I reused the Dictionary setup, and this time learned to use a rounding and conversion routine to flatten the increasing XP requirement per level into a clean integer. I initially locked the program into an infinite loop by forgetting to call the method to actually increment the XP, meaning the "player" would "fight" infinite enemies and never actually level up. I also added a bit of code to allow gaining multiple levels at once, if the XP accumulated exceeds the next level's requirement.
