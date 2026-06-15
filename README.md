# Lazy Engine Modding Guide

This guide explains how to add songs to Lazy Engine.

## Included Content

Lazy Engine includes Bopeebo as an example song. You can use its folder structure as a reference when creating your own songs.

## Running Lazy Engine

### Option 1 (Recommended)
Run `Server.exe` and open the address shown in the window.

### Option 2
Use VS Code Live Server or any other local web server.

## Creating a Song



Lazy Engine uses prerecorded footage instead of rendering gameplay in real time. Because of this, songs are added differently from traditional FNF engines.



### Requirements



You need:



* A chart from Funkin Legacy or Psych Engine 0.4–0.7.3

* Charts should not contain opponent notes

* VS Code, Notepad++, Sublime Text, or any text editor

* A local web server (such as VS Code Live Server)



---



## Step 1: Convert Your Chart



If your chart comes from:



### Psych Engine or Funkin Legacy



Use the Cabalex song converter:



https://cabalex.github.io/fnf-song-converter



Convert your chart into Cabalex's format.



### Codename Engine



Use Codename Engine's built-in chart converter to export your chart as a Psych Engine or Legacy chart.



**Important:** Remove opponent notes before using the chart.



---

### V-Slice or Psych V1

If you are using V-Slice, use the Psych Engine 1.0 built-in V-Slice to Psych Engine 1.0 converter.

Then use the psych v1 to old psych chart converter https://gamebanana.com/mods/681509
To convert the chart into the psych 0.4-0.7.3 format

Don't forget to remove the opponent notes!

---

## Step 2: Record Footage



Create a script for your song that hides:



* `camHUD`

* `camOther`



Enable BotPlay and record gameplay footage of the song.



After recording:



1. Trim the video so only the song remains.

2. Save the video as:



`video.mp4`



---



## Step 3: Get the Opponent Icon



Save your opponent's icon as:



`icon.png`



---



## Step 4: Create the Song Folder



Go to:



`assets/songs/`



Create a folder with your song's name:



`assets/songs/YourSong/`



Place these files inside:



* `video.mp4`

* `chart.txt`

* `icon.png`



---



## Step 5: Add `index.html`



Copy the `index.html` file from an existing song and paste it into your new song folder.



Open the file with your preferred text editor.



Change the page title:



```html

<title>Lazy Engine - Your Song Name!</title>

```



---



## Step 6: Configure the Song



Inside the `<script>` section, set:



```javascript

const bpm = YOUR_BPM;

const scrollSpeed = YOUR_SCROLL_SPEED;

```



Replace the values with your song's BPM and scroll speed.



---



## Step 7: Test the Song



Open the project using a local web server.



If the notes are not synced with the video's audio, adjust:



```javascript

const AUDIO_OFFSET = 100;

```



Increase or decrease the value until the chart matches the video audio.



---

## Step 8: Add the Song to the Menu

Open the root `index.html` file with your preferred text editor.

Copy this code into the file:

```html
<button onclick="location.href='assets/songs/YourSong/index.html'">
    <img src="./assets/songs/YourSong/icon.png" width="80" height="80">
    YourSong
</button>
```

Replace `YourSong` with your song's folder name.

Make sure you haven't made any mistakes.

---


## Finished!



Your song should now work in Lazy Engine.



Because Lazy Engine uses prerecorded videos, it is recommended to use BotPlay recordings with hidden HUD elements for the best results.
**Important:** Lazy Engine uses prerecorded footage, so opponent notes are not needed.


---


## Credits

- Cabalex Engine (by Cabalex) - Original chart format and song converter.
- Psych 1.0 to Legacy Chart Converter (by melodiekit) - Allows you to convert psych engine v1 chart files into the legacy psych engine format
- Funkin' Team - Friday Night Funkin'.

---