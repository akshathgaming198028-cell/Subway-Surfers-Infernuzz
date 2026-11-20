# Subway Surfers Resurferd: project status (parked)

## Working
- Builds Android 64-bit (arm64-v8a) with IL2CPP via GitHub Actions ("Build Multi-Platform").
- Runs on Samsung M56, no crashes reported.
- Frame rate capped at 60 and MSAA disabled (PerformanceSettings.cs).
- Facebook, Chartboost (ads) and Flurry native hooks are stubbed out (iOS-only bridges), so login and ads do nothing.

## Open issues
- Game over: after hitting an obstacle the score screen / home / play buttons do not show.
  Flow: Game.Die -> DieSequence -> UIScreenController.GameOverTriggered -> GameoverUI.
  Needs a device logcat (adb logcat -s Unity) to trace.
- Pause screen: home button goes through MessageCenter -> GoToMainMenuFromGame, not working.
- UI layouts: text and buttons overlap.
- Text looks too sharp, loading screen looks low quality.
- Track bends: the original has gentle curves, this one is straight. Look at camera/track code.
- Old phones (armeabi-v7a only, e.g. Samsung J7 Prime): would need a universal build (ARMv7 + ARM64). Not yet tested with IL2CPP.

## Build notes
- Unity 2019.4.41f2. ProjectSettings: AndroidTargetArchitectures 2 (ARM64), scriptingBackend Android 1 (IL2CPP).
- Mono cannot build ARM64, so IL2CPP is required for 64-bit.
- Keystore must be JKS format (PKCS12 fails in Unity's Java).
