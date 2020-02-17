# A media player control for WPF or UWP, that can play almost anything

## Introduction, problems

When WPF came out it was revolutionary in terms of media. You could easily embed video files to your application, because it came with a MediaPlayer control. This media player control relies on DirectX and the Windows Media player infrastructure for codecs. This means in theory (and in practice, if you're lucky) you can play any video file that you have a codec for.

Unfortunately the currently popular and standard (H.264, H.265, WebM) codecs might not be installed on any system, so if you want to ship media files with your application you will have to install codecs too, which might not be ideal in every case.

Suppose you have to include a video in your app, but want to have multiple audio tracks and subtitles with it. It's not a problem for the MKV or WEBM containers, but unfortunately WPF doesn't support multiple audio stream and subtitle handling, so you either create your own subtitle loading/displaying and audio playing engine that can do the same thing from multiple files.

## My Solution
Luckily we have multiple options to solve the problems mentioned in the introduction, each with a different cost.

A good starting point would be to implement a player using FFmpeg.

> FFmpeg is the leading multimedia framework, able to decode, encode, transcode, mux, demux, stream, filter and play pretty much anything that humans and machines have created. It supports the most obscure ancient formats up to the cutting edge. No matter if they were designed by some standards committee, the community or a corporation.

It's highly portable, runs on Linux, OS-X and Windows, and It's extremely configurable. However the big drawback with this approach is that you would have to implement most of your player in C/C++ and you would have to solve rendering on your own, because FFmpeg is "just" a codec.

However there are existing video players that use FFmpeg for video decoding and support embedding. 

Such a player is [MPV](https://mpv.io/), which is a modern and actively developed fork of [Mplayer](http://www.mplayerhq.hu), which was one of the first FFmpeg video players.

The MPV player provides a simple C style API and can compile all of the needed code into a single DLL file that is sufficient to ship with your application. The functions in the DLL file can be accessed via [Platform invoke](https://docs.microsoft.com/en-us/dotnet/standard/native-interop/pinvoke), so it's easy to use in a C# application.

## Getting MPV

To get started you will to get the mpv-1.dll file. This is the embeddable form of the player and can be downloaded from this site: https://sourceforge.net/projects/mpv-player-windows/files/libmpv/

The builds target X64 systems, so if it's still a requirement that your app needs to run on X86, then you will need to compile it manually.  Also it's worth mentioning that for customization and because of code control purposes you might want to compile your own version from source for your release version.

Compiling MPV from source on Windows is quite a challenge, because it depends on a lot of libraries. However you can download the official build environment that they use to compile the Windows version from the following GIT repository: https://github.com/shinchiro/mpv-winbuild-cmake

This comes with handy instructions on how to set up your development machine.

## The Control

I implemented the control in two parts. `MpvPlayer` is the player controller and `MpvDisplay` is responsible for displaying the video. This two part solution allows flexible app layout creation. 
