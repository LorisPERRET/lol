# League of Legend

Notre projet est un application MAUI développer en C# Utilisant une WebAPI et une base de données Entity Framework
Vous trouverez une explication de l'architecture de notre projet dans le fichier `Schéma_architecture` dans le dossier `Assets`

## Installation du projet

- Clonez le dépot à l'aide de la commande `git clone https://codefirst.iut.uca.fr/git/hugo.livet/lol.git`
- Ouvrez le projet dans Visual Studio
- Restaurez les packages NuGet en utilisant la commande `dotnet restore` dans le terminal ou en utilisant l'interface graphique de Visual Studio
- Compilez le projet en utilisant la commande `dotnet build` ou en utilisant l'interface graphique de Visual Studio

## Web API

Executez notre Web API avec la branche `master` sélectionnée.

### Informations :

Nos entités sont identifiées par leur nom.
Nous avons pris le choix de séparer l'image et le champion pour gagner en performance. Nous appellons l'image que lorsque nous en avons besoin.

L'API fournit les routes suivants :
La differance entre la V1 et la V2 est le nombre de routes et la pagination n'est pas présente dans la V1

Gestion des Champions (V1) : 
- `/api/v1/Champions` (GET) : Récupération de tous les champions
- `/api/v1/Champions/{nom}` (GET) : Récupération d'un champion grâce à son nom
- `/api/v1/Champions` (POST) : Ajout d'un champion
- `/api/v1/Champions/{nom}` (PUT) : Modification d'un champion grâce à son nom
- `/api/v1/Champions/{nom}` (DELETE) : Suppression d'un champion grâce à son nom

Gestion des Champions (V2) : 
- `/api/v2/Champions` (GET) : Récupération de tous les champions
- `/api/v2/Champions/{nom}` (GET) : Récupération d'un champion grâce à son nom
- `/api/v2/Champions/{nom}/image` (GET) : Récupération de l'image d'un champion grâce au nom du chapion
- `/api/v2/Champions/{nom}/skins` (GET) : Récupération des skins d'un champion grâce au nom du champion
- `/api/v2/Champions` (POST) : Ajout d'un champion
- `/api/v2/Champions/{nom}` (PUT) : Modification d'un champion grâce à son nom
- `/api/v2/Champions/{nom}` (DELETE) : Suppression d'un champion grâce à son nom

Gestion des Skins (V2) :
- `/api/v2/Skins` (GET) : Récupération de tous les skins
- `/api/v2/Skins/{nom}` (GET) : Récupération d'un skin grâce à son nom
- `/api/v2/Skins/{nom}/image` (GET) : Récupération de l'image d'un skin grâce au nom du chapion
- `/api/v2/Skins` (POST) : Ajout d'un skin
- `/api/v2/Skins/{nom}` (PUT) : Modification d'un skin grâce à son nom
- `/api/v2/Skins/{nom}` (DELETE) : Suppression d'un skin grâce à son nom

## Entity Framework

Nos entités sont identifiées par leur nom.
Nous avons pris le choix de séparer l'image et le champion pour gagner en performance. Nous appellons l'image que lorsque nous en avons besoin.

Entités présentes dans notre base de données :
- Champions 
- Skins
- Runes
- RunePages
- Images

Dans notre base de données, nous disposons de plusieurs relations. Tout d'abord, un champion dispose de plusieurs skin et un skin est lié qu'à un et un seul champion. 
Ensuite il existe une relation entre champion, rune et runePage. Un champion possède une liste de RunePages et une RunePage peut être possédée par plusieurs champions. 
De plus une Rune est présente dans plusieurs RunePage et une RunePage dispose de plusieurs Rune. 

Voici un Modèle Logique de notre base.

<p align="center"> 
    <img src="Assets/MLD.png" alt="MLD" height="200">  
</p>

## Diagramme de classes du modèle
```mermaid
classDiagram
class LargeImage{
    +/Base64 : string
}
class Champion{
    +/Name : string
    +/Bio : string
    +/Icon : string
    +/Characteristics : Dictionary~string, int~
    ~ AddSkin(skin : Skin) bool
    ~ RemoveSkin(skin: Skin) bool
    + AddSkill(skill: Skill) bool
    + RemoveSkill(skill: Skill) bool
    + AddCharacteristics(someCharacteristics : params Tuple~string, int~[])
    + RemoveCharacteristics(label : string) bool
    + this~label : string~ : int?
}
Champion --> "1" LargeImage : Image
class ChampionClass{
    <<enumeration>>
    Unknown,
    Assassin,
    Fighter,
    Mage,
    Marksman,
    Support,
    Tank,
}
Champion --> "1" ChampionClass : Class
class Skin{
    +/Name : string    
    +/Description : string
    +/Icon : string
    +/Price : float
}
Skin --> "1" LargeImage : Image
Champion "1" -- "*" Skin 
class Skill{
    +/Name : string    
    +/Description : string
}
class SkillType{
    <<enumeration>>
    Unknown,
    Basic,
    Passive,
    Ultimate,
}
Skill --> "1" SkillType : Type
Champion --> "*" Skill
class Rune{
    +/Name : string    
    +/Description : string
}
Rune --> "1" LargeImage : Image
class RuneFamily{
    <<enumeration>>
    Unknown,
    Precision,
    Domination
}
Rune --> "1" RuneFamily : Family
class Category{
    <<enumeration>>
    Major,
    Minor1,
    Minor2,
    Minor3,
    OtherMinor1,
    OtherMinor2
}
class RunePage{
    +/Name : string
    +/this[category : Category] : Rune?
    - CheckRunes(newRuneCategory : Category)
    - CheckFamilies(cat1 : Category, cat2 : Category) bool?
    - UpdateMajorFamily(minor : Category, expectedValue : bool)
}
RunePage --> "*" Rune : Dictionary~Category,Rune~
```

## Diagramme de classes des interfaces de gestion de l'accès aux données
```mermaid
classDiagram
direction LR;
class IGenericDataManager~T~{
    <<interface>>
    GetNbItems() Task~int~
    GetItems(index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~T~~
    GetNbItemsByName(substring : string)
    GetItemsByName(substring : string, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~T~~
    UpdateItem(oldItem : T, newItem : T) Task~T~~
    AddItem(item : T) Task~T~
    DeleteItem(item : T) Task~bool~
}
class IChampionsManager{
    <<interface>>
    GetNbItemsByCharacteristic(charName : string)
    GetItemsByCharacteristic(charName : string, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~Champion?~~
    GetNbItemsByClass(championClass : ChampionClass)
    GetItemsByClass(championClass : ChampionClass, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~Champion?~~
    GetNbItemsBySkill(skill : Skill?)
    GetItemsBySkill(skill : Skill?, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~Champion?~~
    GetNbItemsBySkill(skill : string)
    GetItemsBySkill(skill : string, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~Champion?~~
    GetNbItemsByRunePage(runePage : RunePage?)
    GetItemsByRunePage(runePage : RunePage?, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~Champion?~~
}
class ISkinsManager{
    <<interface>>
    GetNbItemsByChampion(champion : Champion?)
    GetItemsByChampion(champion : Champion?, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~Skin?~~
}
class IRunesManager{
    <<interface>>
    GetNbItemsByFamily(family : RuneFamily)
    GetItemsByFamily(family : RuneFamily, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~Rune?~~
}
class IRunePagesManager{
    <<interface>>
    GetNbItemsByRune(rune : Rune?)
    GetItemsByRune(rune : Rune?, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~RunePage?~~
    GetNbItemsByChampion(champion : Champion?)
    GetItemsByChampion(champion : Champion?, index : int, count : int, orderingPropertyName : string?, descending : bool) Task~IEnumerable~RunePage?~~
}

IGenericDataManager~Champion?~ <|.. IChampionsManager : T--Champion?
IGenericDataManager~Skin?~ <|.. ISkinsManager : T--Skin?
IGenericDataManager~Rune?~ <|.. IRunesManager : T--Rune?
IGenericDataManager~RunePage?~ <|.. IRunePagesManager : T--RunePage?
class IDataManager{
    <<interface>>
}
IChampionsManager <-- IDataManager : ChampionsMgr
ISkinsManager <-- IDataManager : SkinsMgr
IRunesManager <-- IDataManager : RunesMgr
IRunePagesManager <-- IDataManager : RunePagesMgr
```

## Diagramme de classes simplifié du Stub
```mermaid
classDiagram
direction TB;

IDataManager <|.. StubData

ChampionsManager ..|> IChampionsManager
StubData --> ChampionsManager

RunesManager ..|> IRunesManager
StubData --> RunesManager

RunePagesManager ..|> IRunePagesManager
StubData --> RunePagesManager

SkinsManager ..|> ISkinsManager
StubData --> SkinsManager

StubData --> RunesManager
StubData --> "*" Champion
StubData --> "*" Rune
StubData --> "*" RunePages
StubData --> "*" Skins
```