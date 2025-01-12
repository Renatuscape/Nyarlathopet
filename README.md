# Nyarlathopet
A tamagotchi-inspired game about summoning eldritch horrors, built in Unity and C# with additional data stored in JSON format.

## Disclaimer
All assets included in this repository are either 100% free/open source or made by me. Do not redistribute.
* UI panels sourced from [BDragon](https://bdragon1727.itch.io/pixel-buttons-pack-all)

## To Do
### Data from JSON
↪️ All interface text and alerts should be stored in JSON
* Create loader class for UiText[Language].json
* Create repository for text snippets and their ID
* Implement use of the text repository across all classes that display test

**Load Horror objects**
* ✅ Create loader class for Horrors.json
* Create repository for horrors that can be called to retrieve them by stat or ID
    * Ensure only copies are handed out from the repository, to prevent edits to base blueprint

**Map locations JSON**
* ✅ Create .json data
* ✅ Create loader
* Create repository

### Gameplay Scene 👈
**Sanctuary**
* ✅ Toggle info and buttons on/off according to whether a pet exists
* ✅ Display pet stats if one exists
* Implement Book of Masks
* Implement summoning
    * "Begin Ritual" button should start a summoning sequence that consumes items/cultists/own stats and some sanity
    * Will require separate menu, or even separate scene?
* Implement worship
* Implement feeding
* Implement communing

**Sanctuary visuals**
* Add custom graphic for the Book of Masks
* Add background graphic
    * Customisable?
    * Dynamic display of cult wealth and cult members?
    * Simple, animated nodes
* Add pet graphic
    * Pets must have a simple idle animation minimum

**Ledger**
* Display leader data
* ✅ Display cultist data
* Display funds and inventory items
* Allow powering up cult members with items

**Map**
* Display locations
    * Make nodes selectable
    * Display location availability and risk-factor
* Create exploration controller
    * ↪️ Seek Artefacts
    * ↪️ Recruit Members
    * ↪️ Thwart Enemies

### Loader Scene
**Visual updates**
* Get some actually nice loading bar graphics

### Main Menu Scene
**Visual updates**
* Splash screen or animated background
* Adjust layout to look more like a main menu (reduce displayed info?)

### Misc
**Application Quit prompt** (Ensure proper quitting, which will save the session debug log)
* Either hide player 'X' button or display quit prompt when player 'X' button is clicked


## Legend

👈 Indicates the category currently being worked on

🔄 Task in progress

↪️ Return to task

✅ Task complete