# 009 Attack Simulator

## Goal

Create a simple combat action where one character attacks another.
**DONE**

## Bonus challenges

1. Make damage vary randomly. **DONE**

2. Prevent HP from dropping below zero. **DONE**

3. Display a different message if the attack defeats the enemy. **DONE**

### Personal notes

31.7.2026 By this point I've written multiple pseudo-combat-simulation programs already, but it's good to rehearse to reinforce the concepts. I took a slightly different approach to the challenge this time, and focused not so much on whether the attack hits or the alternating turns, but rather damage variance and critical chance. I also achieved preventing health from decreasing below zero differently than before -- earlier, I'd just set a negative value back to zero before any further processes, but this time I decreased health by one point at a time with a for loop, which is surely less efficient, but by far more precise, and also could be used to display the health running down gradually as opposed to a sudden update to the processed new health value.