# Changelog

### Caption 
[+] Add<br>
[\~] Modification<br>
[-] Suppression<br>
[#] Bug Fixes<br>
[.] Others

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
