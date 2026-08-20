# 017 Battle Loop

## Goal

Create a battle where the player and enemy continue taking turns until one reaches 0 HP. **DONE**



## Bonus challenges

1. Display the turn number. **DONE**

2. Randomize who attacks first. **DONE**

3. Allow the player to flee.

### Personal notes

Considering I had already done something very similar a couple of times, I turned this one into an exercise in writing logic for a more dynamic turn system, where each action takes a preset amount of time to resolve, and the participants take turns as their actions are carried out. I practiced a new type of console menu (cursor selection rather than hotkey), and more importantly tried encapsulation for my Action class, and got it to work. I neglected to stop the turn counting for the player to actually see which enemy action is resolved, but the logic seemed to work fine nonetheless.
