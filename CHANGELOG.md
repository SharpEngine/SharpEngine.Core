# Changelog

### Caption 
[+] Add<br>
[\~] Modification<br>
[-] Suppression<br>
[#] Bug Fixes<br>
[.] Others

### V 2.2.3 - 17/05/2025
[#] ControlComponent : Reset Direction when no movement

### V 2.2.2 - 11/05/2025
[\~] AutoComponent : Now check CollisionComponent when movements

### V 2.2.1 - 11/05/2025
[+] Window : OpenUrl, SetIcon<br/>
[+] ProgressBar, Slider : Horizontal<br/>
[#] ControlComponent : Error on position pass to collision CanGo<br/>
[#] CollisionComponent : Only one collision by entity is used

### V 2.2.0 - 07/05/2025
[\~] DataTableManager : Get will now get DataTable directly, not an object<br/>
[\~] IDataTable / JsonDataTable : Rework complete with Add, Remove and Get<br/>
[\~] SERender : Allow float on many render functions<br/>
[\~] SERender : Better manage of types<br/>
[#] LineInput : Caret is hide when text is empty<br/>
[#] MultiLineInput : Caret is hide when text is empty<br/>
[#] MultiLineInput : Caret is render outside input frame<br/>
[#] MultiLineInput : Fix Y Offset with empty last line (after return line)

### V 2.1.1 - 06/05/2025
[\~] TextureButton : Allow use empty text

### V 2.1.0 - 18/04/2025
[\~] TextureButton : Remove black background

### V 2.0.4 - 02/04/2025
[+] LineInput / MultiLineInput : Secret

### V 2.0.3 - 10/03/2025
[+] Window : Constructor parameters redirectLogs

### V 2.0.2 - 06/02/2025
[.] Raylib-cs : 7.0.1<br/>
[.] ImGui.NET : 1.91.6.1

### V 2.0.1 - 11/01/2025
[#] ControlComponent : Fix using ClassicJump Control Type

### V 2.0.0 - 25/12/2024
[+] Entity : Children, AddChild, RemoveChild, Parent<br>
[+] TransformComponent : LocalPosition, LocalRotation, LocalScale, LocalZLayer<br>
[+] Animation : Loop<br>
[\~] Animation : Indices, Timer are now modifiable<br>
[\~] SpriteSheetComponent : CurrentImage is now publicly readable

### V 1.10.0 - 24/12/2024
[+] SEShader<br>
[\~] ShaderManager : Use SEShader instead of Shader<br>
[\~] SpriteComponent : Use SEShader instead of Shader

### V 1.9.3 - 23/12/2024
[+] SpriteSheetComponent : AnimationEnded<br>
[\~] SpriteSheetComponent : currentAnim, internalTimer, currentImage are protected

### V 1.9.2 - 24/11/2024
[+] SpriteComponent : TintColor<br/>
[+] Rect : Intersect

### V 1.9.1 - 21/11/2024
[+] DebugManager : AddConsoleCommand<br/>
[#] Fix versions in DebugManager

### V 1.9.0 - 08/11/2024
[+] Manage resizing window<br/>
[.] Raylib-cs : 6.1.1<br/>
[.] ImGui.NET : 1.91.0.1

### V 1.8.7 - 16/04/2024
[#] Widgets : Fix update when not displayed

### V 1.8.6 - 15/04/2024
[#] Fix all 1.8.5

### V 1.8.5 - 15/04/2024
[+] DataTableManager : HasDataTable, RemoveDataTable<br/>
[+] FontManager : HasFont, RemoveFont<br/>
[+] ShaderManager : HasShader, RemoveShader<br/>
[+] TextureManager : HasTexture, RemoveTexture<br/>
[.] Improve comments

### V 1.8.4 - 15/04/2024
[#] Widgets : Fix activation without displaying

### v 1.8.3 - 21/01/2024
[.] Raylib-cs : 6.0.0

### V 1.8.2 - 29/12/2023
[#] Resize Window doesn't resize imgui and camera render

### V 1.8.1 - 28/12/2023
[\~] Rename DefaultRenderImGui to SeRenderImGui and move it to DebugManager

### V 1.8.0 - 26/12/2023
[+] Console<br/>
[\~] Modification of ImGui systems (F7 for Debug Window - F8 for Console)<br/>
[.] ImGui.NET : 1.89.9.4

### V 1.7.3 - 05/12/2023
[+] CameraManager : Position

### V 1.7.2 - 27/11/2023
[#] DebugManager : Other packages versions cannot be added

### V 1.7.1 - 27/11/2023
[#] ControlComponent : Keys inversed between Up and Down

### V 1.7.0 - 25/11/2023
[+] ParticleParameters<br/>
[+] ParticleEmitterParameters<br/>
[\~] Particle : Constructor use ParticleParameters<br/>
[\~] ParticleEmitter : Constructor use ParticleEmitterParameters<br/>
[-] SpriteSheetComponent : flipX and flipY in Constructor<br/>
[.] Refactor with SonarCloud<br/>
[.] ImGui.NET : 1.90.0.1

### V 1.6.0 - 21/11/2023
[.] Net 8

### V 1.5.2 - 19/11/2023
[#] SoundManager : Can't use functions without throwing exceptions<br/>
[#] MusicManager : Can't use functions without throwing exceptions

### V 1.5.1 - 18/11/2023
[.] Update Raylib-cs to 5.0.0<br/>
[.] Update ImGui.NET to 1.89.9.3

### V 1.5.0 - 09/10/2023
[+] TimerManager, Timer<br/>
[+] InputManager : UpdateInput<br/>
[+] Window : Guard when no scene<br/>
[\~] Instructions : Move instructions into records<br/>
[#] ControlComponent : Fix MouseFollow<br/>
[#] DataTableManager : Remove null warning<br/>
[.] Reformat<br/>
[.] Refactor

### V 1.4.3 - 20/09/2023
[+] Color : FromVec4<br/>
[.] Update ImGui.NET to 1.89.9.1

### V 1.4.2 - 17/09/2023
[+] Entity : Name<br/>
[+] Widget : Name

### V 1.4.1 - 16/09/2023
[+] SeImGui : ImageRenderTexture, ImageRect<br/>
[\~] Window : Make SeImGui public 

### V 1.4.0 - 10/09/2023
[+] ShaderManager<br>
[+] SERender : ShaderMode<br>
[+] Color : ToVec4<br>
[+] SpriteComponent : Shader

### V 1.3.3 - 04/09/2023
[+] CollisionComponent : Add DebugDraw

### V 1.3.2 - 16/08/2023
[#] Scene : Add Widget or Entity with delay doesn't make loading<br>
[#] Scene : Delay removing or adding lists aren't clear

### V 1.3.1 - 26/08/2023
[+] Scene : delay parameter in AddWidget and AddEntity

### V 1.3.0 - 20/08/2023
[\~] Move some utils to own folder<br>
[-] VecI<br>
[-] RectI<br>
[.] Refactor

### V 1.2.2 - 16/08/2023
[#] CollisionComponent : CanGo doesn't calculate all collisions

### V 1.2.1 - 16/08/2023
[\~] ControlComponent : Slide to collisions

### V 1.2.0 - 15/08/2023
[+] CollisionComponent

### V 1.1.4 - 14/08/2023
[#] JsonSave : Fix deserialization

### V 1.1.3 - 14/08/2023
[#] JsonSave : Fix casting<br>
[#] BinarySave : Fix casting

### V 1.1.2 - 14/08/2023
[+] DebugManager : Versions<br>
[\~] ControlComponent : Allow Moving and Direction modification by children<br>
[-] DebugManager : SeVersion

### V 1.1.1 - 13/08/2023
[+] Scene : GetSceneSystem

### V 1.1.0 - 13/08/2023
[+] Scene : AddSceneSystem<br>
[+] ISceneSystem<br>
[\~] DebugManager : showAssemblyVersion in CreateSeImGuiWindow<br>
[-] Internal<br>
[.] Raylib-cs : Update to 4.5.0.4

### V 1.0.0 - 13/08/2023
[.] First version (Created from http://github.com/AlexisHuvier/SharpEngine)
