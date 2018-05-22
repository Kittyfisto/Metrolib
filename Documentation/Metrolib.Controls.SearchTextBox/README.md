# SearchTextBox

A text-box meant to input a search term.
                * offers a dedicated button to perform the search
                * offers a dedicated button to clear the search term
                * offers a display of the number of matches
                * offers buttons and shortcuts to advance to the next/previous location

### Unfocused

```xaml
<Metrolib:SearchTextBox Watermark="Enter search term..." />
```
![Image of SearchTextBox, Unfocused](Unfocused.png)

### Focused

```xaml
<Metrolib:SearchTextBox Watermark="Enter search term..." />
```
![Image of SearchTextBox, Focused](Focused.png)

### FilterText, Focused

```xaml
<Metrolib:SearchTextBox Text="Luke" Watermark="Enter search term..." />
```
![Image of SearchTextBox, FilterText, Focused](FilterText__Focused.png)

# Properties

**RequiresExplicitSearchStart**: System.Boolean  
Whether or not the search must be started by the user explicitly by pressing enter or clicking the search button.

**IsPerformingSearch**: System.Boolean  
Is set to true if the search started, but not (yet) stopped.

**OccurenceCount**: System.Int32  
The number of occurences of the search term in the data set.
                Must be supplied by the user of this class.

**CurrentOccurenceIndex**: System.Int32  
The index of the currently focused occurence of the search term in the data set.

**StartSearchCommand**: System.Windows.Input.ICommand  
The command that is executed when the user hits enter or presses the search button.

**StopSearchCommand**: System.Windows.Input.ICommand  
The command that is executed when the user wants to stop/abort the search.

**Watermark**: System.String  
The watermark that is displayed until Text becomes non-empty.

