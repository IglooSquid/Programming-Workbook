# Chapter 001 Checkpoint

## Goal

Create a tiny five-minute text adventure.
- Character creation
- One random encounter
- Random combat outcome
- Experience
- Level Up
- Final summary

**DONE**

### Personal notes

27.7. Programmed a character creator, wherein I tried to be a little bit more elegant than in the previous project. For one, I used race and class dictionaries rather than hardcoding strings into the various options, and the options themselves were displayed with a foreach loop instead of being handwritten. I'm planning to eventually plug in some kind of race and class logic that contains data beyond the race and class names, and I'll use that same type of logic for later enemy generation.

30.7. After struggling with understanding how to use classes and objects for a couple of days, I finally cracked it and managed to use the concept for player races, player classes, and monsters. The problem I had related mostly to code structure, and not understanding you can't define a class within another class. I had a little more troubleshooting to do with the "active enemy" setup, wherein things weren't updating quite as intended, but ultimately I figured it out, and managed to put together a functional pseudo-roguelike.