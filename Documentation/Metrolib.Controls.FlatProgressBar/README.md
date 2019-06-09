# FlatProgressBar  

A progress bar in a "flat" style.
                Displays the relative value as 'X%' in the progress bar.

**Namespace**: Metrolib.Controls  
**Assembly**: Metrolib (in Metrolib.dll)  

```C#
[System.Windows.TemplatePart]
[System.Windows.TemplatePart]
[System.Windows.TemplatePart]
public class FlatProgressBar : Metrolib.Controls.AbstractProgressBar
```

Inheritance Object -> DispatcherObject -> DependencyObject -> Visual -> UIElement -> FrameworkElement -> Control -> RangeBase -> ProgressBar -> AbstractProgressBar -> FlatProgressBar
### No progress

```xaml
<Metrolib:FlatProgressBar />

```
![Image of FlatProgressBar, No progress](No_progress.png)

### 50% progress

```xaml
<Metrolib:FlatProgressBar Value="50" />

```
![Image of FlatProgressBar, 50% progress](50__progress.png)

### 100% progress

```xaml
<Metrolib:FlatProgressBar Value="100" />

```
![Image of FlatProgressBar, 100% progress](100__progress.png)

### Indeterminate

```xaml
<Metrolib:FlatProgressBar IsIndeterminate="True" />

```
![Image of FlatProgressBar, Indeterminate](Indeterminate.png)

### Disabled

```xaml
<Metrolib:FlatProgressBar Value="50" IsEnabled="False" />

```
![Image of FlatProgressBar, Disabled](Disabled.png)

# Properties  

**IndeterminateValue**: System.Double  
The relative value used in favour of !:ProgressBar.Value when this one is
                IsIndeterminate.

