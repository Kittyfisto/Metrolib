# CircularProgressBar  

A circular progress bar (full circle).

Namespace: Metrolib.Controls  
Assembly: Metrolib (in Metrolib.dll)  

### No progress

```xaml
<Metrolib:CircularProgressBar />
```
![Image of CircularProgressBar, No progress](No_progress.png)

### 50% progress

```xaml
<Metrolib:CircularProgressBar Value="50" />
```
![Image of CircularProgressBar, 50% progress](50%_progress.png)

### 100% progress

```xaml
<Metrolib:CircularProgressBar Value="100" />
```
![Image of CircularProgressBar, 100% progress](100%_progress.png)

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

# Properties  

**IndeterminateAngle**: System.Double  
The current angle of of the circle segment when this progress bar is indeterminate.

**ContentTemplate**: System.Windows.DataTemplate  
The data template, if any, that is used to present the Content.

**Content**: System.Object  
The content being displayed in the center of the circular progress bar.

**Thickness**: System.Double  
The thickness of the circular progress bar.

