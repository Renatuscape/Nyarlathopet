# Nyarlathopet
*Nyar-la-tho-pet*

A tamagotchi-inspired game about summoning eldritch horrors, built in Unity and C# with additional data stored in JSON format.

## Disclaimer
All assets included in this repository are either 100% free/open source or made by me. Do not redistribute.
* UI panels sourced from [BDragon](https://bdragon1727.itch.io/pixel-buttons-pack-all)

## To Do
### Data from JSON
↪️ All interface text and alerts should be stored in JSON
* ✅ Create loader class for Text[Language].json
* ✅ Create repository for text snippets and their ID
* 🔄 Implement use of the text repository across all classes that display text

**Load Horror objects**
* ✅ Create loader class for Horrors.json
* ✅ Create repository for horrors
    * Ensure only copies are handed out from the repository, to prevent edits to base blueprint

**Map locations JSON**
* ✅ Create .json data
* ✅ Create loader
* ✅ Create repository

### Gameplay Scene 👈
**Round Mechanics**
* The pet must be fed every round, or rage will increase. Add some incentive as to why the ritual should be commenced as soon as possible
    * Chance of gaining bonuses during events when a pet is active
    * Chance of losing cultists, network, or other detrimental effects without a pet
* Summoning costs max EP and can only be done at the beginning of the round

**Sanctuary** 
* ✅ Toggle info and buttons on/off according to whether a pet exists
* ✅ Display pet stats if one exists
* Implement Book of Masks
* ↪️ Implement summoning
    * ✅ "Begin Ritual" button should start a summoning sequence that consumes items/cultists/own stats and some sanity
    * Implement final summoning of the mask
        * Where should this button go?
        * When should it become available?
* Implement worship
* ✅ Implement feeding
    * Track whether pet was fed this round, and increase rage if it was not. Clearly alert the player
* Implement communing
    * Chance of leveling up when stats are high enough and enough masks have been found
* Decide whether pet activities should consume EP, and consider whether more than 3 EP per round is necessary

**Sanctuary visuals**
* Add custom graphic for the Book of Masks
* Add background graphic
    * 🔒 Customisable?
    * 🔒 Dynamic display of cult wealth and cult members?
    * 🔒 Simple, animated nodes
* Add pet graphic
    * Pets must have a simple idle animation minimum

**Ritual**
* ✅ Implement cultist sacrifice
* ✅ Implement cultist selection
* ✅ Implement item selection
* ✅ Implement item sacrifice
* 🪲 Fix bugs
    * Ritual stats must not drop below 0
    * Player should lose minimum 1 sanity when sacrificing cultists
    * ✅ !! Fix issue where active cultist/artefact are not nulled out when a sacrifice is completed
    * !! Using up all artefacts on page 2 will erroneously show an empty inventory
    * !! Using up all cultists on page 2 will erroneously show an empty inventory
    * Sacrificing a cultist will sometimes show the same skill increase twice. Compound values into one
    * Do not display cultist reactions when there are no cult members
    * Ensure that a summoning gives at least one skill point and at least one sanity loss
    * Summoned pet had 0 rage

**Ledger**
* ✅ Display leader data
* ✅ Display cultist data
* 📌 Display funds and inventory items
    * Items should be sorted by type
* Display item icons to indicate their type
* Allow powering up cult members with tomes

**Map**
* ✅ Display locations
    * ✅  Make nodes selectable
    * ✅  Display location availability and risk-factor
* Create exploration controller
    * 🔄 Seek Artefacts
        * Give options for spending members, network, or funds
    * 📌 Recruit Members
        * Implement cost
    * 📌 Thwart Enemies
        * Give options for spending members, network, or funds
* 🪲 Fix bugs
    * Notoriety should not drop below 1 when thwarting
    * Network must be reduced by at least 1 when thwarting

**Misc**
* ✅ Add pop-out panel to view a compressed version of stats anywhere

### Loader Scene
**Visual updates**
* Get some actually nice loading bar graphics

### Main Menu Scene
**Visual updates**
* Splash screen or animated background
* Adjust layout to look more like a main menu (reduce displayed info?)

### Game Over Scene
* Add this to the game
* Should display some statistics and graphic
* Handle old data erasure and store some history about the failed cult for a hall of fame
    * ❓ Scores and scoreboard?

### Event Scene
For events which occur between rounds, or forced early under certain conditions
* Create framework for handling events
    * Method for building event queue
    * ✅ Library for holding event data
    * ✅ Methods for managing event queue
* Event methods
    * Promotion of a new leader (leader is insane or otherwise dead/missing)
        * This condition should force the event scene regardless of remaining EP
    * ❓ Feature to depose the leader, with better benefits than letting the leader go insane?
    * Attacks by police or investigators (high notoriety)
    * Pet tantrums (high rage)

### Misc
**Magick and Occultism**
* Rename human stat magick to occultism in code and displays
* Horrors should use magick stat, humans should use occultism

**Application Quit prompt** (Ensure proper quitting, which will save the session debug log)
* Either hide player 'X' button or display quit prompt when player 'X' button is clicked


## Legend

👈 Indicates the category currently being worked on

🔄 Task in progress

↪️ Unfinished. Return later

🪲 Bug entry

📌 Works well enough, but should be improved later

❓ Consider whether feature should be included

🔒 Stretch goal

✅ Task complete