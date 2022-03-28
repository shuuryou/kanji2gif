Kanji2GIF
=========

Generates GIF files with Japanese kanji stroke order animations. In addition to kanji, it supports hiragana, katakana, number, and alphabet animations. It was intended to be used with something like [Anki](http://ankisrs.net/).

Make some flash cards showing a kanji word as the "question" and the GIF animation as the "answer". When you then work through the flash cards, write out the kanji word shown and then check whether you used the correct stroke order afterwards.

This was written in early 2011 but there was a one-off update in 2022 from 1.0.0.5 to 1.1.0.0 to replace the ancient `convert.exe` from the ImageMagick project with direct calls to the [Magick.NET](https://github.com/dlemstra/Magick.NET/) library and to update the ancient build of the [SharpVectors](https://github.com/ElinamLLC/SharpVectors/) library from the long-gone CodePlex with a more recent version from GitHub. These are both now pulled in via NuGet packages, so you'll need a fairly recent version of Visual Studio to build the project. Ancient Visual Studio 2010 will no longer build this.

There's a ready to use release for Windows you can download and play with. It should run just fine on any version of Windows (x86 or x64) that supports the .NET Framework 4.0.

Usage
-----

There are two modes of operation; command-line and GUI. The command-line mode works as follows:

    Usage: Kanji2GIF.exe [Wordlist] [OutDir] </c> </s:N> </w:N>
    
    Wordlist - A UTF-8-encoded plain text file with one word per line.
    OutDir   - The directory to place created GIF images into.
    /c       - Makes every drawn stroke use a different color.
    /s:N     - Wait this many seconds between strokes (default: 0.5).
    /w:N     - Wait this many seconds before looping animation (default: 5).

The GUI looks like this:

![Screenshot](http://i.imgur.com/rmnVrar.png)


Output Samples
--------------

    kanji2gif wordlist.txt c:\out /c /s:0.5 /w:5

![Strawberry](http://i.imgur.com/mZG1vcb.gif)

![Transistor](http://i.imgur.com/BPoIpGa.gif)

![Harmony](http://i.imgur.com/Yfb898A.gif)

