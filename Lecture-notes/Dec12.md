# December 12

Vi kollar på två centrala **WPF-kontroller för att presentera data** från en databas (via EF Core):

**TreeView** – för hierarkisk/navigerbar data

**DataGrid** – för tabellbaserad visning av listor/rader

Båda kontrollerna är starkt kopplade till data binding, ItemsSource och templates, vilket gör dem särskilt relevanta i kombination med databaser och EF Core.

**Code-along:**  
[L008_DataGrid_and_TreeView (xaml)](https://github.com/everyloop/NEU25G-Databases/blob/main/Code-alongs/L008_DataGrid_and_TreeView/MainWindow.xaml)  
[L008_DataGrid_and_TreeView (code-behind)](https://github.com/everyloop/NEU25G-Databases/blob/main/Code-alongs/L008_DataGrid_and_TreeView/MainWindow.xaml.cs)  


## 📌 TreeView – hierarkisk data och navigation

### Vad är TreeView?

TreeView används för att **visa hierarkisk data** – t.ex. trädstrukturer där objekt kan innehålla child-objects.

**Exempel:**

```
Artist
 └── Album
      └── (Tracks laddas vid klick)
```

TreeView fungerar främst som en **navigationskontroll**, där användaren väljer vad som ska visas någon annanstans i UI:t.

## 📌 TreeView - Koncept från code-along

### 1️⃣ ItemsSource

```cs
myTreeView.ItemsSource = new ObservableCollection<Artist>(artists);
```

- ```ItemsSource``` binder TreeView till en samling.

- ```ObservableCollection``` gör att UI:t uppdateras automatiskt om innehållet ändras.

- Varje objekt i samlingen blir en **root-nod**.

### 2️⃣ HierarchicalDataTemplate

```cs
<HierarchicalDataTemplate DataType="{x:Type model:Artist}"
                          ItemsSource="{Binding Albums}">
```

- ```HierarchicalDataTemplate``` talar om:

    - **Hur ett objekt ska visas**.

    - **Vilken property som innehåller dess barn**.

- ```DataType``` gör att WPF automatiskt väljer rätt template baserat på objektets typ

- ```ItemsSource="{Binding Albums}"``` skapar nästa nivå i trädet

Samma sak görs för ```Album → Tracks```

### 3️⃣ Visuell presentation

```xml
<TextBlock>
    <Run Text="{Binding Name}" />
    <Run Text="(" />
    <Run Text="{Binding Albums.Count}" />
    <Run Text=" albums)" />
</TextBlock>
```
- Visar hur man kan:

    - kombinera flera bindings

    - visa beräknad information (t.ex. ```Albums.Count```)

- All logik ligger i modellen → TreeView är ren presentation

### 4️⃣ SelectedItemChanged

```cs
private void myTreeView_SelectedItemChanged(...)
{
    if (e.NewValue is Album album)
    {
        LoadTracks(album);
    }
}
```

- TreeView används som **trigger**

- När användaren väljer ett ```Album```:

    - laddas relaterade ```Tracks``` från databasen

    - dessa visas i DataGrid

Detta är ett tydligt exempel på **master → detail-UI**

## 📌 DataGrid – tabellbaserad datavisning

### Vad är DataGrid?

- DataGrid används för att visa **listor av objekt i tabellform**:

    - **Rader** = objekt

    - **Kolumner** = properties

- Den är idealisk för:

    - databasresultat

    - sökresultat

    - listor som ska sorteras eller markeras

## 📌 DataGrid - Koncept från code-along

### 1️⃣ ItemsSource

```cs
myDataGrid.ItemsSource = new ObservableCollection<object>(tracks);
```

- DataGrid visar **en rad per objekt**.

- Properties i objekten binds till kolumner

- I exemplet projicerar vi EF-entiteter till ett anonymt objekt → bra separation UI/databas.

### 2️⃣ AutoGenerateColumns = False

```cs
AutoGenerateColumns="False"
```

- Kolumner skapas manuellt

- Ger full kontroll över:

    - rubriker

    - ordning

    - bredd

    - binding

### 3️⃣ Definiera kolumner

```xml
<DataGridTextColumn Header="Track Name"
                    Binding="{Binding Name}" />
```
- Varje kolumn:
    - har en Header
    - binder till en property

- DataGrid behöver **inte känna till klassen**, bara property-namn

### 4️⃣ Read-only och användarinteraktion

```cs
IsReadOnly="True"
CanUserAddRows="False"
CanUserDeleteRows="False"
```

- Gör DataGrid till en **ren visningskomponent**.

- Viktigt i databassammanhang när man inte vill editera direkt.

### 5️⃣Sortering och markering
```cs
CanUserSortColumns="True"
SelectionMode="Extended"
SelectionUnit="CellOrRowHeader"
```

- Sortering sker automatiskt via binding

- ```Extended``` tillåter multi-select

DataGrid har mycket funktionalitet “gratis” och går att konfigurera för olika behov.

### 6️⃣ Utseende

```cs
AlternationCount="2"
RowBackground="White"
AlternatingRowBackground="NavajoWhite"
```
- Visar hur man enkelt förbättrar läsbarhet

- Varannan rad får annan färg

- Inget extra kod-behind behövs

## 📌 Sammanfattning

**TreeView**

- Visar hierarkisk data

- Använder ```HierarchicalDataTemplate```

- Binder barn via ```ItemsSource```

- Lämplig för navigation och master-val

- Reagerar på användarens val (```SelectedItemChanged```)

**DataGrid**

- Visar listor i tabellform

- Binder till valfri samling via ```ItemsSource```

- Kolumner binds till properties

- Har inbyggt stöd för sortering, markering och styling

- Passar perfekt för databasresultat