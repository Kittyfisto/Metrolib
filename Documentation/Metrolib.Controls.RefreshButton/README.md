# RefreshButton  

A button that can be used to refresh things.

Shows a circular progress indicator while being refreshed.

**Namespace**: Metrolib.Controls  
**Assembly**: Metrolib (in Metrolib.dll)  

```C#
public class RefreshButton : Metrolib.Controls.FlatButton
```

Inheritance Object -> DispatcherObject -> DependencyObject -> Visual -> UIElement -> FrameworkElement -> Control -> ContentControl -> ButtonBase -> Button -> FlatButton -> RefreshButton
### Unfocused

```xaml
<Metrolib:RefreshButton />

```
![Image of RefreshButton, Unfocused](Unfocused.png)

### Hovered

```xaml
<Metrolib:RefreshButton />

```
![Image of RefreshButton, Hovered](Hovered.png)

### Pressed

```xaml
<Metrolib:RefreshButton />

```
![Image of RefreshButton, Pressed](Pressed.png)

### Disabled

```xaml
<Metrolib:RefreshButton IsEnabled="False" />

```
![Image of RefreshButton, Disabled](Disabled.png)

# Properties  

**IsRefreshing**: System.Boolean  
When set to true, the button will show a Metrolib.Controls.CircularProgressBar with
                IsIndeterminate
                set to true.

