# DataEditJS Editor
This is a companion application for my Android Data Editing plugin DataEditJS. The PC tool allows to edit and test scripts (JavaScript syntax) before sending to an Android device running the DataeditJS plugin.

![](https://github.com/hjgode/DataEditJS_Editor/raw/master/doc/dataeditjs_editor_main.png)

## Usage
The upper part of the editor dialog shows the current script text.

On the lower left side you can enter test data, like the barcode data the plugin has to process later on the Android device. You may also select a barcode code ID that is also used to test the script.

The input supports binary symbols like the FNC1 (GS) code of \x1D.

### Test
By pressing the Test button, the input data and code ID is send to the script and the result is presented in the Result field. Possible error are shown below the Result field.

Non-printable characters are represented as \xAB codes (hexadecimal value of the code).

## Snippets

The tool comes with some predefined snippets. These can be choosen from the Edit-Snippets menu and will replace the current script.

## Send to device

Within the File menu you can select 'Send file to Device' to send a saved script file to the device.

# Notes

* for the DataEditJS Editing Plugin the file name has to be dataedit.js
* the script file must be stored at /storage/emulated/0/Documents. The Editor uses only this folder.
