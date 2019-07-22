# MarkdownPresenter  

This control is responsible for presenting markdown text.
                 !:https://stackoverflow.com/editing-help for a description of markdown.

Currently, this control *only* supports the following constructs:
                 - Bold
                 - Italic
                 - Strikethrough
                 - Hyperlinks
            
                 If there's a construct you're interested in, please open an issue and I will reprioritize
                 accordingly.

**Namespace**: Metrolib.Controls  
**Assembly**: Metrolib (in Metrolib.dll)  

```C#
[System.Windows.TemplatePart]
public sealed class MarkdownPresenter : System.Windows.Controls.Control
```

Inheritance Object -> DispatcherObject -> DependencyObject -> Visual -> UIElement -> FrameworkElement -> Control -> MarkdownPresenter
### Bold

```xaml
<Metrolib:MarkdownPresenter Markdown="**NO** expectations" />

```
![Image of MarkdownPresenter, Bold](Bold.png)

### Italic

```xaml
<Metrolib:MarkdownPresenter Markdown="What's up *danger*" />

```
![Image of MarkdownPresenter, Italic](Italic.png)

### Bold and Italic

```xaml
<Metrolib:MarkdownPresenter Markdown="Spider Man Into the __*Spider-Verse*__" />

```
![Image of MarkdownPresenter, Bold and Italic](Bold_and_Italic.png)

### Strikethrough

```xaml
<Metrolib:MarkdownPresenter Markdown="This movie is ~~awful~~ **awesome**" />

```
![Image of MarkdownPresenter, Strikethrough](Strikethrough.png)

### Hyperlink

```xaml
<Metrolib:MarkdownPresenter Markdown="Check out [this](https://www.youtube.com/watch?v=4-5WwgTnXnA)" />

```
![Image of MarkdownPresenter, Hyperlink](Hyperlink.png)

# Properties  

**TextWrapping**: System.Windows.TextWrapping  
Gets or sets how this control should wrap text.

**Markdown**: System.String  
Gets or sets the markdown text this control should display.

