# Nyarlathopet
A tamagotchi-inspired game about summoning eldritch horrors, built in Unity and C# with additional data stored in JSON format.

## Disclaimer
All assets included in this repository are either 100% free/open source or made by me. Do not redistribute.
* UI panels sourced from [BDragon](https://bdragon1727.itch.io/pixel-buttons-pack-all)

## To Do
### Data from JSON
All interface text and alerts should be stored in JSON
* Create loader class for UiText[Language].json
* Create repository for text snippets and their ID
* Implement use of the text repository across all classes that display test

Load Horror objects
* Create loader class for Horrors.json
* Create repository for horrors that can be called to retrieve them by stat or ID

Map locations JSON
* Create .json data
* Create loader
* Create repository

### Gameplay Scene
Sanctuary
* Toggle info and buttons on/off according to whether a pet exists
* Display pet stats if one exists
* Implement Book of Masks
* Implement summoning
* Implement worship
* Implement feeding

Ledger
* Display leader data
* Display cultist data
* Display funds and inventory items

Map
* Plan visual layout
* Create exploration controller
    * Seek Artefacts
    * Recruit Members
    * Thwart Enemies

### Loader Scene
Visual updates
* Get some actually nice loading bar graphics

### Main Menu Scene
Visual updates
* Splash screen or animated background
* Adjust layout to look more like a main menu (reduce displayed info?)

### Misc
Application Quit prompt (Ensure proper quitting, which will save the session debug log)
* Either hide player 'X' button or display quit prompt when player 'X' button is clicked