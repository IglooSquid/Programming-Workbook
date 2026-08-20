# 019 Mining Simulator

## Goal

Continue mining until the player decides to stop. Each mining attempt yields a random resource. **DONE**



## Bonus challenges

1. Occasionally find nothing. **DONE**

2. Add rare gems. **DONE**

3. Give the player limited stamina. **DONE**

### Personal notes
Managed to write a relatively crude but entirely functional loop with limited stamina, finite resource drops per node, and node-specific drop lists with varying rarity rates. My solution for item drop rarity that I couldn't figure out before is hardly elegant, but I find it works just fine for my purposes so far. I also started to experiment a little with custom namespace logic, though this particular program was small enough that it hardly made much difference, nor did it really provide an opportunity to gauge if I was using it correctly.