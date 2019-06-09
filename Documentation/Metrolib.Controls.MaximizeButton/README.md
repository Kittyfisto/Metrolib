# MaximizeButton  

A button to maximize something, for example a window.

**Namespace**: Metrolib.Controls  
**Assembly**: Metrolib (in Metrolib.dll)  

```C#
public class MaximizeButton : Metrolib.Controls.FlatButton
```

Inheritance Object -> DispatcherObject -> DependencyObject -> Visual -> UIElement -> FrameworkElement -> Control -> ContentControl -> ButtonBase -> Button -> FlatButton -> MaximizeButton
### Unfocused

```xaml
<Metrolib:MaximizeButton />

```
![Image of MaximizeButton, Unfocused](Unfocused.png)

### Hovered

```xaml
<Metrolib:MaximizeButton />

```
![Image of MaximizeButton, Hovered](Hovered.png)

### Pressed

```xaml
<Metrolib:MaximizeButton />

```
![Image of MaximizeButton, Pressed](Pressed.png)

### Disabled

```xaml
<Metrolib:MaximizeButton IsEnabled="False" />

```
![Image of MaximizeButton, Disabled](Disabled.png)

# Properties  

**IsMaximized**: System.Boolean  
Whether or not this button shall represent the maximized state.
                When set to true, the button will show the icon for restore, otherwise for maximize.

