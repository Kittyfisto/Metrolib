# EditorTextBox  

A textbox meant to edit text.
                Displays a watermark while no text has been entered.

Supports many markdown shortcuts, when EnableMarkdownShortcuts is set to true.
                See https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet.

**Namespace**: Metrolib.Controls  
**Assembly**: Metrolib (in Metrolib.dll)  

### Unfocused

```xaml
<Metrolib:EditorTextBox Watermark="Enter comment..." />
```
![Image of EditorTextBox, Unfocused](Unfocused.png)

### Focused

```xaml
<Metrolib:EditorTextBox Watermark="Enter comment..." />
```
![Image of EditorTextBox, Focused](Focused.png)

### Text, Focused

```xaml
<Metrolib:EditorTextBox Text="The quick brown fox jumps over the lazy dog" Watermark="Enter comment..." TextWrapping="Wrap" />
```
![Image of EditorTextBox, Text, Focused](Text__Focused.png)

### Text, Unfocused

```xaml
<Metrolib:EditorTextBox Text="The quick brown fox jumps over the lazy dog" Watermark="Enter comment..." TextWrapping="Wrap" />
```
![Image of EditorTextBox, Text, Unfocused](Text__Unfocused.png)

### Disabled

```xaml
<Metrolib:EditorTextBox Text="The quick brown fox jumps over the lazy dog" Watermark="Enter comment..." TextWrapping="Wrap" IsEnabled="False" />
```
![Image of EditorTextBox, Disabled](Disabled.png)

# Properties  

**EnableMarkdownShortcuts**: System.Boolean  
Whether or not this control shall accept shortcuts (key gestures) which insert the appropriate markdown
                syntax (such as ctrl+b to make text bold, inserts **...**).
                Is disabled by default.

**Watermark**: System.String  
The watermark text that shall appear until Text is no longer empty.

