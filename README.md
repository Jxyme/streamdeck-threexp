# streamdeck-threexp

Display what map is currently being played on the 3xP' Codjumper server, and more.

### Table of Contents

**[Info](#info)**<br>
**[Features](#features)**<br>
**[Usage Instructions](#usage-instructions)**<br>
**[Credits](#credits)**<br>

## Info

> What is Codjumper?

Codjumper is a mod, and a home to Call of Duty glitches, exploits, jumps, nade throws, rpg jumps and bounces.

* You can read and learn more on the official Codjumper [website](https://www.codjumper.com/faqs.php#what1), and find videos showcasing Codjumper on [YouTube](https://www.youtube.com/CoDJumper).

> What is threexp? (3xP)

3xP' is an international mutligaming clan. You can learn, and see more of what we do on our [Website](https://3xp-clan.com/) or [YouTube](https://www.youtube.com/channel/UCIFzjEtaUue7Ldirx8me6lg).

* You can watch a video, presented by 3xP' showcasing some of the posiblities. 3xP' Clan presents.., [Some Bounces X](https://www.youtube.com/watch?v=vUG_2WJmnco).

> I'm intrigued... how do I get started?

In order to play on the 3xP' Codjumper server, you're required to have Call of Duty 4. [CDKeys](https://www.cdkeys.com/pc/games/call-of-duty-4-modern-warfare-pc-cd-key-steam) or [Steam](https://store.steampowered.com/app/7940/Call_of_Duty_4_Modern_Warfare/) has it available.

Once you own Call of Duty 4, head over to the server browser -> add favourite and enter `s.3xp-clan.com:1337`.

You can find server statistics, and more, on the 3xP' Statistics [webpage](https://stats.3xp-clan.com/server/7). (the plugin uses the 3xP' API)

## Features

### Available Features

#### Display the Current Map

The map currently playing will display on the Stream Deck key of your choice, simply drag the plugin onto the deck.

#### Display the Current Map & Player Count

When this display setting is enabled, you'll be able to cycle through the currnet map, and the player count.

#### Launch *any* Application

Locate a file, folder and/or executable when the key is pressed. (this doesn't necessarily need to be **iw3mp.exe**)

#### Launch *any* Application (with Arguments)

Add arguments, for example, `+connect s.3xp-clan.com:1337` to connect to the 3xP' Codjumper server automatically.

* Advanced arguments are supported too. Example: `+set fs_game "mods/3xp_cj" +g_gametype "cj" +devmap mp_race`.

#### Search for a walkthrough (of the current map playing)

If you're ever stuck, searching for a walkthrough hasn't been easier. **Tap & hold** the key to search for a walkthrough.

## Usage Instructions

As explained briefly above, the plugin currently supports short and long press actions. (configuration isn't *yet* available)

#### Short Press

* If you have configured a file, folder and/or application to launch, tapping the key will launch an instance.

  * An alert will display if you haven't configured anything and try to launch a process.

#### Long Press (roughly 1 second)

* You can **tap & hold** the key to search for a walkthrough (guide) to show you how a jump/bounce is done. (if available)

## Credits

Thank you to [BarRaider's Stream Deck Tools](https://github.com/BarRaider/streamdeck-tools), which provides developers with a wrapper written in C# for Stream Deck's [SDK](https://developer.elgato.com/documentation/stream-deck/sdk/overview/).
