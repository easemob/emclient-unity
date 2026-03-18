# AGENTS.md

## Project Positioning
This is a hybrid repository combining Unity + C# + Windows SDK, which mainly includes four categories of content:

- `AgoraChatSDK`: C# SDK API and encapsulation implementation.
- `UnityChatDemo`: Unity integration and sample project.
- `Win_SDK`: Windows SDK packaging and test project.
- `backup`: Historical/backup code (not involved in development and retrieval by default).

## Directory Responsibilities and Modification Entries
- `AgoraChatSDK/AgoraChat`: Core SDK C# code (Managers/Models/Delegates/SDKClient, etc.).
- `UnityChatDemo/Assets/AgoraChat/AgoraChat`: Mirrored code that is basically consistent with `AgoraChatSDK/AgoraChat`.
- `Win_SDK/FrameworkTest/Testor.cs`: Entry point for testing calls to the `AgoraChatSDK` API.
- `UnityChatDemo/Assets/Script`: C# test and UI sample scripts on the Unity side.

### Convention for Handling Duplicate Code (Very Important)
`AgoraChatSDK/AgoraChat` and `UnityChatDemo/Assets/AgoraChat/AgoraChat` contain duplicate isomorphic code:

1. `AgoraChatSDK/AgoraChat` is used as the primary modification source by default.
2. Any changes to SDK behavior/API must be synced to the corresponding files in `UnityChatDemo/Assets/AgoraChat/AgoraChat`.
3. If discrepancies are found between the two sides, do not overwrite them without authorization; first align the source of discrepancies and mark them in the change description.

## Scope of Retrieval and Analysis (Must Be Ignored)
When performing searches, static analysis, batch replacement, and code reviews, ignore the following directories:

1. `backup`
2. `AgoraChatSDK/AgoraChatDemo`
3. `AgoraChatSDK/CWrapper/Wrapper/Wrapper_iOS/build_sim_xxxx` (and similar `build_sim*` directories)
4. `AgoraChatSDK/CWrapper/Wrapper/Wrapper_iOS/HyphenateChat.framework`
5. `AgoraChatSDK/CWrapper/Wrapper/Wrapper_Android/JavaWrapperDemo/wrapper/build`
6. `AgoraChatSDK/CWrapper/Wrapper/Wrapper_Android/JavaWrapperDemo/app`

## Workflow Recommendations
1. **First Locate the Attribution of Changes**
   - SDK internal logic/API: Prioritize modifying `AgoraChatSDK/AgoraChat`, then mirror and sync to the Unity copy directory.
   - Windows call verification: Add or adjust call scenarios in `Win_SDK/FrameworkTest/Testor.cs`.
   - Unity interaction verification: Supplement verification in the corresponding test scripts in `UnityChatDemo/Assets/Script`.

2. **Self-Check Before Submission**
   - Whether content in ignored directories was modified by mistake.
   - Whether syncing of the dual SDK directories was missed.
   - Whether necessary call examples (`Testor.cs` or `Assets/Script`) were updated simultaneously.

## Execution Constraints for Agents
- Unless explicitly required, do not perform search result analysis or code modification in ignored directories.
- When involving public APIs (such as `Managers/*`, `SDKClient.cs`, `Models/*`), prioritize considering the compatibility impact on `Win_SDK` and Unity test scripts.
- For external behavior changes (parameters, callbacks, error code semantics), supplement minimal call example updates, covering at least:
  - `Win_SDK/FrameworkTest/Testor.cs` or
  - `UnityChatDemo/Assets/Script/*ManagerTest.cs`

## Standard Steps for Adding New APIs (Using `FetchHistoryMessagesFromServerBy` as a Template)
When a new API needs to be added to this hybrid project, follow the below workflow to avoid platform incompatibility caused by only modifying the C# layer:

1. **First Confirm the Existing Implementation Workflow (Template API)**
   - C# exposure layer: `AgoraChatSDK/AgoraChat/Managers/ChatManager.cs`
   - C# method name constant: `AgoraChatSDK/AgoraChat/Helper/SDKMethod.cs`
   - CWrapper public distribution: `AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/common_wrapper/common_cwrapper.cpp`
   - Android distribution and implementation: `Wrapper_Android/.../util/EMSDKMethod.java`, `Wrapper_Android/.../EMChatManagerWrapper.java`
   - iOS distribution and implementation: `Wrapper_iOS/.../util/EMSDKMethod.h`, `Wrapper_iOS/.../managers/EMChatManagerWrapper.m`
   - Call verification: `Win_SDK/FrameworkTest/Testor.cs`, `UnityChatDemo/Assets/Script/ChatManagerTest.cs`

2. **Add New C# SDK API (Main Directory)**
   - Add public methods (including Chinese and English comments, default parameter values, and parameter assembly) in `AgoraChatSDK/AgoraChat/Managers/*.cs`.
   - Call `NativeCall`/`NativeGet`, and supplement `process` parsing logic (e.g., `CursorResult<Message>`) according to the return structure.
   - If new data structures or parameter objects are involved, synchronously supplement serialization/deserialization in `Models/*`.

3. **Supplement Method String Constants**
   - Add new method constants in `AgoraChatSDK/AgoraChat/Helper/SDKMethod.cs`.
   - Ensure the constant value is exactly consistent (including case) with the routing string on the CWrapper side.

4. **Connect CWrapper Routing**
   - Register the new method to the target function in the corresponding `func_map_*` of `Wrapper_Common/common_wrapper/common_cwrapper.cpp`.
   - If there is no existing underlying function, complete the corresponding bridge implementation for `ChatManager_*` (or other Managers).

5. **Connect Android Wrapper**
   - Add the same-named constant in `Wrapper_Android/.../util/EMSDKMethod.java`.
   - Add a branch in the `onMethodCall` distribution of `Wrapper_Android/.../EMChatManagerWrapper.java`.
   - Implement specific methods: parameter reading, SDK calling, and encapsulating callback results into JSON for return.

6. **Connect iOS Wrapper**
   - Add the same-named constant in `Wrapper_iOS/.../util/EMSDKMethod.h`.
   - Add a branch in the method distribution of `Wrapper_iOS/.../managers/EMChatManagerWrapper.m`.
   - Implement specific methods: parameter parsing, SDK calling, and ensuring the callback return format is consistent with the existing style.

7. **Sync Mirrored Code (Mandatory)**
   - Sync the additions/modifications in `AgoraChatSDK/AgoraChat` to the corresponding files in `UnityChatDemo/Assets/AgoraChat/AgoraChat`.
   - Do not modify only one side; the dual copies of code must be kept consistent.

8. **Supplement Call Verification (At Least One Side)**
   - Windows side: Add menus, parameter reading, and call use cases in `Win_SDK/FrameworkTest/Testor.cs`.
   - Unity side: Add button actions or test entries in `UnityChatDemo/Assets/Script/*ManagerTest.cs`.
   - Callbacks need to cover success/failure logs to facilitate troubleshooting cross-layer issues.

9. **Pre-Submission Checklist**
   - Whether C# API, `SDKMethod`, Common CWrapper, Android, and iOS are all connected.
   - Whether the dual SDK directories have been synced.
   - Whether `Testor.cs` or Unity test scripts have been updated.
   - Ignored directories (`backup`, `AgoraChatDemo`, iOS/Android build product directories) are not touched.

## Common Retrieval Recommendations (Examples)
> The following are only recommendations; be sure to include ignored directory filtering during execution.

- Finding API definitions: Focus on `AgoraChatSDK/AgoraChat/**/*.cs`
- Finding Windows calls: Focus on `Win_SDK/FrameworkTest/Testor.cs`
- Finding Unity-side calls: Focus on `UnityChatDemo/Assets/Script/**/*.cs`
