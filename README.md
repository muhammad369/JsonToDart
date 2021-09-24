# JsonToDart

A Dotnet cross-platform App to create dart class from json, taking null-safety into consideration

The dart class extends `Serializable` base class

Here is the `Serializable` class code

```dart
import 'dart:convert';

abstract class Serializable {

  String serialize() {
    return json.encode(this.toMap());
  }

  void deserialize(String text) {
    this.fromMap(json.decode(text));
  }

  //abstract
  Map<String, dynamic> toMap();

  //abstract
  void fromMap(Map<String, dynamic> map);
}

```



## How to use

### Windows

For windows the app is available as Windows Forms app, and as console app,

for the console app you can provide your json input via a file, the clipboard or just write it in the console, and you can take the output dart code by any of the three ways,

to run it, open the CMD, go to the app folder, and write `j2d`, you will see help text.

### MacOS

For MacOS the app is available as console only, with the same options available for windows console

to run it, open the Terminal app, go to the app folder, write `dotnet j2d.dll`, you will see the help text

### Linux

it should be easy to compile the console app for linux, but it's not yet tested :)
