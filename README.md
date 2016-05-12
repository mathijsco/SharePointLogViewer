# SharePointLogViewer
ULS log viewer for SharePoint.

This version is designed for SP2010, so not sure if it works for later versions.
If you have tried it out, let me know. That is much appreciated!

## Features
- Go directly to an entry that has an exception for quick lookups.
- Filter on category and correlation ID's.
- It will automatically load your ULS logs if SharePoint is installed on running environment.
- Limit the viewed entries in minutes. If increased, older ULS files are automatically scanned.
- Support file shares to retrieve ULS logs, or even use the system shares (like C$).
- Pretty fast, if I may say... Even over a slower network by only reading what is necessary.

*Screenshot*

![](https://raw.githubusercontent.com/mathijsco/SharePointLogViewer/master/resources/screenshot.png)
