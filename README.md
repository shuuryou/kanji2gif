Kanji2GIF
=========

Generates GIF files with Japanese kanji stroke order animations. Written early 2011. In addition to kanji, it supports hiragana, katakana, number, and alphabet animations. It was intended to be used with something like [Anki](http://ankisrs.net/).

Please note that this pogram is no longer maintained and considered feature-complete. A quick test indicates that it still works fine on modern versions of Windows. Please note the conditions of the LICENSE file if you decide to fork.

There are two modes of operation; command-line and GUI. The command-line mode works as follows:

    Usage: Kanji2GIF.exe [Wordlist] [OutDir] </c> </s:N> </w:N>
    
    Wordlist - A UTF-8-encoded plain text file with one word per line.
    OutDir   - The directory to place created GIF images into.
    /c       - Makes every drawn stroke use a different color.
    /s:N     - Wait this many seconds between strokes (default: 0.5).
    /w:N     - Wait this many seconds before looping animation (default: 5).

The GUI looks like this:

![Screenshot](http://i.imgur.com/rmnVrar.png)
