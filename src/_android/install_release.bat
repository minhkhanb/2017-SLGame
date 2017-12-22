call config.bat
adb uninstall topebox.game.shadeless
adb install  build\outputs\apk\release\shadeless-release-v1.4.5.apk
::adb install  build\outputs\apk\Atomic-release-v1.0.0-unsigned-signed.apk