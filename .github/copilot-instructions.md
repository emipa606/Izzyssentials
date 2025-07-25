# GitHub Copilot Instructions for RimWorld Mod Project

## Mod Overview and Purpose
The project, titled "Izzyssentials," is a mod that enhances the gameplay of RimWorld by adding new functionalities and improving existing systems, particularly focusing on building and color management. The mod's core purpose is to provide players with greater control over their environment through customizable structures and enhanced visual aesthetics.

## Key Features and Systems

1. **Building Enhancements:**
   - **Building_Glow:** Adds the ability for structures to emit customizable glow using different colors to enhance atmosphere and gameplay.
   - **Building_HRC_Switch:** Implements a switch mechanism affecting energy networks and textures within the game.

2. **Plant Management:**
   - **Building_PlantBetterGrower:** Enhances plant growing mechanics by integrating a more efficient and controllable growing system.

3. **Color Customization:**
   - **CompGlowerColour:** Offers advanced color customization for glowing components, allowing players to cycle through colors seamlessly.
   - **Graphic_ColorWall:** Applies graphics with adjustable color settings, giving walls a dynamic and personalized appearance.

4. **Utility Components:**
   - **CompSensor:** Provides components that react based on environment, offering commands for automatic adjustments.
   - **CompPowerPlantSolarCon:** An improved solar power plant component designed for better solar energy management.

## Coding Patterns and Conventions

- **Class Inheritance:** Most functional classes inherit from base game classes, allowing for extended behaviors while maintaining compatibility.
- **Method Naming:** The methods follow a consistent pattern of verb-leading names that clearly indicate actions (e.g., `UpdateTex`, `ChangeColour`).
- **Encapsulation:** Use of private methods to protect internal logic, exposing functionality through controlled interfaces.

## XML Integration

- Although XML files were not explicitly listed, integrations are typically done through XML to define ThingDefs, BuildingDefs, and more. XML tags are expected to follow standard RimWorld schema conventions for easy integration of new building and item definitions.

## Harmony Patching

- **Harmony:** The mod likely leverages the Harmony library to patch existing methods, allowing modifications to the game's core mechanics without directly altering base code. Patching allows for reversed, prefix, and postfix hooks to adjust game behavior seamlessly.

## Suggestions for Copilot

1. **Pattern Recognition:** As Copilot generates suggestions, it should adhere to established naming conventions and class structures to maintain coherence across the project.
   
2. **XML Suggestions:** When generating XML definitions, ensure compliance with RimWorld's XML schema, focusing on ThingDefs, BuildingDefs, and other essential aspects for mod integration.
   
3. **Harmony Integration:** Suggest appropriate Harmony patches for methods where behavior adjustment is necessary. Provide examples for prefixes and postfixes in typical RimWorld patching scenarios.
   
4. **Error Handling:** Propose strategies for robust error handling, particularly for methods interacting with game components to ensure stability.
   
5. **Custom Commands:** For user interface elements like `Command_Action` buttons, suggest intuitive names and actions that align with the mod's customization goals.

By adhering to these guidelines, Copilot can assist in extending the capabilities of the mod while maintaining a consistent and reliable codebase.
