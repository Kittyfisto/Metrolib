# PinToggleButton  

A toggle button in the style of a pin. When !:ToggleButtonBase.IsChecked is set to false, then
                the pin is crossed out (PinOff), otherwise not (Pin).

**Namespace**: Metrolib.Controls  
**Assembly**: Metrolib (in Metrolib.dll)  

```C#
public class PinToggleButton : Metrolib.Controls.ToggleButtonBase
```

Inheritance Object -> DispatcherObject -> DependencyObject -> Visual -> UIElement -> FrameworkElement -> Control -> ContentControl -> ButtonBase -> ToggleButton -> ToggleButtonBase -> PinToggleButton
### Unfocused

```xaml
<Metrolib:PinToggleButton />

```
![Image of PinToggleButton, Unfocused](Unfocused.png)

### Checked

```xaml
<Metrolib:PinToggleButton IsChecked="True" />

```
![Image of PinToggleButton, Checked](Checked.png)

### Disabled

```xaml
<Metrolib:PinToggleButton IsEnabled="False" />

```
![Image of PinToggleButton, Disabled](Disabled.png)

### Disabled Checked

```xaml
<Metrolib:PinToggleButton IsEnabled="False" />

```
![Image of PinToggleButton, Disabled Checked](Disabled_Checked.png)

### RotateWhenUnchecked Checked

```xaml
<Metrolib:PinToggleButton RotateWhenUnchecked="True" IsChecked="True" />

```
![Image of PinToggleButton, RotateWhenUnchecked Checked](RotateWhenUnchecked_Checked.png)

### RotateWhenUnchecked Unchecked

```xaml
<Metrolib:PinToggleButton RotateWhenUnchecked="True" IsChecked="False" />

```
![Image of PinToggleButton, RotateWhenUnchecked Unchecked](RotateWhenUnchecked_Unchecked.png)

# Properties  

**RotateWhenUnchecked**: System.Boolean  
When set to true, then this toggle button will display the Pin rotated by 90° clockwise when
               !:ToggleButtonBase.IsChecked is set to false (e.g. it will behave more like Visual Studio's Toggle Buttons).

