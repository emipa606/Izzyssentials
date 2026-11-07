# GitHub Copilot Instructions for RimWorld Mod: Izzyssentials (Continued)

## Mod Overview and Purpose
Izzyssentials is a RimWorld mod that combines various utility and aesthetic features into a single mod. It aims to enhance the gameplay experience by adding essential elements that improve resource management and environmental decoration.

## Key Features and Systems
- **Linked Hydroponics**: A one-tile hydroponics system that links with adjacent hydroponics to synchronize plant types.
- **Linked Stools**: A one-tile stool that connects to itself, with a fancier variant available after the Beautification Project.
- **Linked Tables**: A one-tile table that connects to itself, with a fancier variant available after the Beautification Project.
- **Linked Solar Panels**: A one-tile solar panel with a central power output indicator that changes color based on the time of day.
- **Lamps**: Lamps available in all material types, with color customization after the Coloured-Lights research.
- **Wall Lights**: Built-in wall lights available in all material types, with color customization after the Coloured-Lights research.
- **Wall Switches**: Wall switches with built-in LED indicators for power status, available in all material types.

## Coding Patterns and Conventions
- **C# Files**: The mod primarily consists of C# files that extend the RimWorld game logic. Key files include:
  - `Colour.cs`: Manages color-related functionalities.
  - `Graphic_ColorWall.cs`: Handles graphics for wall lights.
  - `ListOColours.cs`: Contains a list of colors.
  - `PlaceWorker_CeilingLamp.cs`: Manages placement of ceiling lamps.
  - `ThingWithComps_Extensions.cs`: Provides extensions for managing components of things.
- **XML Files**: XML files define the mod's research projects and thing definitions. Key files include:
  - `ResearchProjects_Furniture.xml`: Defines research projects related to furniture.
  - `ResearchProjects_Switch.xml`: Defines research projects related to switches.
  - `HRC_Buildings_Furniture.xml`: Defines furniture thing definitions.
  - `HRC_Buildings_Hydro.xml`: Defines hydroponics thing definitions.
  - `HRC_Buildings_Lights.xml`: Defines light-related thing definitions.

## XML Integration
The mod uses XML files to define various game elements such as research projects and thing definitions. These XML files are located in the `Defs` folder and are essential for the mod's functionality. When making changes to these XML files, ensure that they are correctly formatted and that they do not conflict with other mods.

## Harmony Patching
The mod uses Harmony patching to modify existing RimWorld game logic. Harmony patches are used to extend or change the behavior of existing methods in the game. When writing Harmony patches, ensure that they are well-documented and that they do not introduce performance issues.

## Suggestions for Copilot
- **C# Development**: Use Copilot to generate code snippets for new features or to extend existing features. For example, you can use Copilot to generate code for new thing definitions or to modify existing methods.
- **XML Definition**: Use Copilot to generate XML definitions for new research projects or thing definitions. For example, you can use Copilot to generate XML for a new furniture item or a new solar panel.
- **Harmony Patching**: Use Copilot to generate Harmony patches for modifying existing game logic. For example, you can use Copilot to generate a patch for changing the behavior of a method that handles hydroponics synchronization.

## Conclusion
Izzyssentials is a comprehensive mod that adds various utility and aesthetic features to RimWorld. By following the coding patterns and conventions outlined above, and by leveraging Copilot for code generation, you can efficiently develop and maintain the mod. Remember to always back up your world before installing new mods and to use the Discord-channel for error-reporting.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

