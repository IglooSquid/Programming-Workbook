# 006 Experience Counter

## Goal

A character defeats a monster and gains experience.
**DONE**

## Bonus challenges

1. ~~Defeat multiple monsters.~~ **DONE**

2. ~~Display remaining XP until level up.~~ **DONE**

3. ~~Allow different enemies to grant different XP.~~ **DONE**

### Personal notes

I used a Dictionary again to associate monsters with individual XP values -- again not what I imagine would be done in an actual game, but works fine here, and it's good practice for the Dictionary which I'm not good at using yet. I got the code correct on my first try (aside from a semicolon typo), including passing an argument from one method to another, which I was certain I wouldn't format correctly. I had one method handling the "encounter", accessing the dictionary for the monster and its XP reward, and also triggering a separate method for handling experience gain. The former method passed the experience reward to the latter method, so only the first method needed access to the actual Dictionary.