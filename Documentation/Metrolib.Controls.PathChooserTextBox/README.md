# PathChooserTextBox  

This textbox allows a user to manually enter a path to a folder/file or click on a "more" button.
                Clients of this control must hook up this command to dialogs of their choice.

**Namespace**: Metrolib.Controls  
**Assembly**: Metrolib (in Metrolib.dll)  

```C#
[System.Windows.TemplatePart]
public class PathChooserTextBox : System.Windows.Controls.TextBox
```

Inheritance Object -> DispatcherObject -> DependencyObject -> Visual -> UIElement -> FrameworkElement -> Control -> TextBoxBase -> TextBox -> PathChooserTextBox
### Unfocused

```xaml
<Metrolib:PathChooserTextBox Watermark="Enter path" />

```
![Image of PathChooserTextBox, Unfocused](Unfocused.png)

### Focused

```xaml
<Metrolib:PathChooserTextBox Watermark="Enter path" />

```
![Image of PathChooserTextBox, Focused](Focused.png)

### Text, Focused

```xaml
<Metrolib:PathChooserTextBox Text="C:\foo\bar" Watermark="Enter path" />

```
![Image of PathChooserTextBox, Text, Focused](Text__Focused.png)

### Text, Unfocused

```xaml
<Metrolib:PathChooserTextBox Text="C:\foo\bar" Watermark="Enter path" />

```
![Image of PathChooserTextBox, Text, Unfocused](Text__Unfocused.png)

### Disabled

```xaml
<Metrolib:PathChooserTextBox Text="C:\foo\bar" Watermark="Enter path" IsEnabled="False" />

```
![Image of PathChooserTextBox, Disabled](Disabled.png)

# Properties  

**Watermark**: System.String  
The watermark which is displayed if Text is empty.

**PathChooserCommand**: System.Windows.Input.ICommand  
The command which is invoked when the more button is pressed.
            Clients of this control MUST hook up this command with a folder chooser
            dialog of their choice.

