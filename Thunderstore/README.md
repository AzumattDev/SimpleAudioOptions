# SimpleAudioOptions

## Overview

The primary function of this plugin is to provide the ability to modify the speaker mode, DSP Buffer Size, and Sample
Rate settings of Unity's
AudioSettings.

`Please note that changing these values live can result in audio issues until game reboot. Also, I'm not fully aware or could find out what the defaults for Valheim were, so I just set some up. It's NOT to say that these are the best settings, but they are the ones I chose in the moment.`

## Features

- **Speaker Mode Configuration:** Allows you to specify the speaker mode you'd like to use in Unity's AudioSettings. You
  can choose between Mono, Stereo, Quad, 5.1, 7.1. Note that the RAW setting is not included, as it doesn't appear to be
  supported anymore in Unity.
- **DSP Buffer Size:** Adjust the size of the digital signal processing buffer for better audio performance. This is the
  size of the buffer in samples. Lower values result in lower latency, but require more CPU power.
- **Sample Rate:** Set the sample rate for the audio system. This is the number of samples per second of audio. Higher
  sample rates result in better audio quality, but also require more CPU power.

<details>
<summary><b>Configuration Options</b></summary>

`1 - General`

Enable Mod Control

* Enable the mod's control over audio settings. If disabled, the game's defaults will be used.
    * Default Value: On
    * Acceptable values: Off, On

Speaker Mode

* The speaker mode to use for the game. This will be applied on game start. Changing this value live can result in audio
  issues, but it is possible to change it live.
    * Default Value: Stereo
    * Acceptable values: Mono, Stereo, Quad, Surround, Mode5point1, Mode7point1, Prologic

DSP Buffer Size

* The size of the digital signal processing buffer.
    * Default Value: 1024

Sample Rate

* The audio system sample rate.
    * Default Value: 44100

</details>

<details>
<summary><b>Installation Instructions</b></summary>

***You must have BepInEx installed correctly! I can not stress this enough.***

### Manual Installation

`Note: (Manual installation is likely how you have to do this on a server, make sure BepInEx is installed on the server correctly)`

1. **Download the latest release of BepInEx.**
2. **Extract the contents of the zip file to your game's root folder.**
3. **Download the latest release of from Thunderstore.io.**
4. **Extract the contents of the zip file to the `BepInEx/plugins` folder.**
5. **Launch the game.**

### Installation through r2modman or Thunderstore Mod Manager

1. **Install [r2modman](https://valheim.thunderstore.io/package/ebkr/r2modman/)
   or [Thunderstore Mod Manager](https://www.overwolf.com/app/Thunderstore-Thunderstore_Mod_Manager).**

   > For r2modman, you can also install it through the Thunderstore site.
   ![](https://i.imgur.com/s4X4rEs.png "r2modman Download")

   > For Thunderstore Mod Manager, you can also install it through the Overwolf app store
   ![](https://i.imgur.com/HQLZFp4.png "Thunderstore Mod Manager Download")
2. **Open the Mod Manager and search for "" under the Online
   tab. `Note: You can also search for "Azumatt" to find all my mods.`**

   `The image below shows VikingShip as an example, but it was easier to reuse the image.`

   ![](https://i.imgur.com/5CR5XKu.png)

3. **Click the Download button to install the mod.**
4. **Launch the game.**

</details>

<br>
<br>

## Limitations

The speaker mode RAW is not available as an option because it is not supported by the game.

`Feel free to reach out to me on discord if you need manual download assistance.`

# Author Information

### Azumatt

`DISCORD:` Azumatt#2625

`STEAM:` https://steamcommunity.com/id/azumatt/

For Questions or Comments, find me in the Odin Plus Team Discord or in mine:

[![https://i.imgur.com/XXP6HCU.png](https://i.imgur.com/XXP6HCU.png)](https://discord.gg/Pb6bVMnFb2)
<a href="https://discord.gg/pdHgy6Bsng"><img src="https://i.imgur.com/Xlcbmm9.png" href="https://discord.gg/pdHgy6Bsng" width="175" height="175"></a>