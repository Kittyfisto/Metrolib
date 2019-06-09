# CircularProgressBar  

A circular progress bar (full circle).
                Displays the current percentage (if determinate) as well as an optional content in the center.

**Namespace**: Metrolib.Controls  
**Assembly**: Metrolib (in Metrolib.dll)  

```C#
[System.Windows.TemplatePart]
public class CircularProgressBar : Metrolib.Controls.AbstractProgressBar
```

Inheritance Object -> DispatcherObject -> DependencyObject -> Visual -> UIElement -> FrameworkElement -> Control -> RangeBase -> ProgressBar -> AbstractProgressBar -> CircularProgressBar
### No progress

```xaml
<Metrolib:CircularProgressBar />

```
![Image of CircularProgressBar, No progress](No_progress.png)

### 50% progress

```xaml
<Metrolib:CircularProgressBar Value="50" />

```
![Image of CircularProgressBar, 50% progress](50__progress.png)

### 100% progress

```xaml
<Metrolib:CircularProgressBar Value="100" />

```
![Image of CircularProgressBar, 100% progress](100__progress.png)

### Indeterminate

```xaml
<Metrolib:CircularProgressBar IsIndeterminate="True" />

```
![Image of CircularProgressBar, Indeterminate](Indeterminate.png)

### Disabled

```xaml
<Metrolib:CircularProgressBar Value="50" IsEnabled="False" />

```
![Image of CircularProgressBar, Disabled](Disabled.png)

### Indeterminate, Content

```xaml
<Metrolib:CircularProgressBar Content="Busy" IsIndeterminate="True" />

```
![Image of CircularProgressBar, Indeterminate, Content](Indeterminate__Content.png)

# Properties  

**IndeterminateAngle**: System.Double  
The current angle of of the circle segment when this progress bar is indeterminate.

**ContentTemplate**: System.Windows.DataTemplate  
The data template, if any, that is used to present the Content.

**Content**: System.Object  
The content being displayed in the center of the circular progress bar.

**Thickness**: System.Double  
The thickness of the circular progress bar.

