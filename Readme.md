# dlgtotxt

A command line utility to extract string information from dialog.tlk files used in the Infinity Engine relating to specific NPCs, into a csv to enable further processing.

Note: dlgtotxt does not extract dlg files from Infinity Engine archive files (BIFF files) - they must be extracted using other tools, e.g. WeiDU.


## Usage
``` 
dlgtotxt dialog.tlk listingfile.csv
```

**dialog.tlk**: path to the dialog.tlk file, e.g. /bgee/dialog.tlk

**listingfile.csv**: a csv containing a list of dlg files and npc names (1 entry per line), e.g. AJANTD.DLG,ajantis

## Download

You can [download](https://github.com/btigi/dlgtotxt/releases/) the latest version of dlgtotxt for Windows.


## Technologies

dlgtotxt is written in C# Net Core 5.0.


## Compiling

To clone and run this application, you'll need [Git](https://git-scm.com) and [.NET](https://dotnet.microsoft.com/) installed on your computer. From your command line:

```
# Clone this repository
$ git clone https://github.com/btigi/dlgtotxt

# Go into the repository
$ cd dlgtotxt

# Build  the app
$ dotnet build
```


## License

dlgtotxt is licensed under [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/)
 