# README #

# Tigra 
## Unity TIC-80 graphical API experiment

The Tigra project is an implementation of the graphics API functions of TIC-80 fantasy console.
The project is a plugin for Unity that allows you to experiment with TIC-80 graphics using the full power of C# and Unity!

Tigra will allow you to plunge into the atmosphere of minimalism of fantasy consoles.
Small screen resolution, border, a palette of 16 colors, all code in a single file and a simple TIC-80 API functions is what creates limitations and emphasizes the spirit of retro-gaming consoles. A professional development environment Unity and powerful object-oriented language C# is the tools for easy and rapid creating, debugging, and publishing your creative ideas under a variety of execution platforms. The project also includes some scripts for integrating free and open source code editor Microsoft Visual Code: snippets for autocompletion TIC-80 API functions, Unity integration script, settings of ignored resources files.

It is enough to have one GameObject with a custom script inherited from `Tic80` class in scene in order to work. `Tic80Config` script and graphical representation will be added automatically.

The project includes source code of some demos for TIC-80, rewritten in C#.
The project is open source and freely redistributable.
TIC-80 logo and brand (c) Copyright 2017 Nesbox https://tic.computer/

![](/Gifs/tigra_demos.gif)

## Description
You can select the font and colours in the editor in `Tic80Config` script.
It also allows you to set the display of debug information (currently it can only be displayed if the script is not used the text output using print function).
Settings can be changed in the editor and in runtime.
Only one custom script inherited from `Tic80` class can be active at a time. If the scene will has other active `Tic80` scripts they will be turned off.
When you activate a custom script during runtime the remaining scripts will be turned off automatically.
No need to create a camera - the representation  will be added at the start of the game.

![](/Gifs/tigra_config_settings.gif)

## Project from scratch.
1. Create a new scene: File -> New Scene. Add to the scene a new GameObject: GameObject -> Create Empty. Then add this object to our game script or drag it (if it is already exist), or using the Add Component button in the Inspector tab when gameObject is selected.
In addition to our script the `Tic80Config` script will be automatically added to the gameObject.

2. Create our script - descendant class of `Tic80`-class.
In addition to creating class manually, you can use the class creation template. 
For this the script `99-Tic80 Script-NewTic80Script.cs` must already be in the specified folder (see "Placement and editing template scripts."). 
Further, in the Project tab, select the folder where we want to place the script (but NOT in the Editor folder), click the right mouse button and from the context menu, select Create -> Tic80 Script. It will create a new script, which should be given a name.

![](/Gifs/tigra_new_script.gif)

3. Fill our script with game logic and launch the game!

## Placing and editing template scripts.
To create scripts according to the template you want, copy the file `99-Tic80 Script-NewTic80Script.cs` from the folder `Assets/Scripts/Editor` to the script templates folder.
To display the source folder and target folder, you can use the command from the context menu `Show in explorer Tic80 Script Templates` or selecting it from the `Assets > Show in explorer Tic80 Script Templates`.
### The path to the script templates folder : ### 
* **Windows** `C:\Program Files\Unity\Editor\Data\Resources\ScriptTemplates`
* **Mac** `/Applications/Unity.app/Contents/Resources/ScriptTemplates`
* **Linux** `/opt/Unity/Editor/Data/Resources/ScriptTemplates`

![](/Gifs/tigra_copy_template.gif)

## Using snippets of TIC-80 API functions in Microsoft Visual Studio Code.
In order to use autocompletion of code (snippets) of TIC-80 API functions do the following:
1. Open the file csharp.json with the desired language. The following languages are available: EN, RU, FR. They are here: `Assets/Plugins/VSCodeSnippets`.
2. Open in Visual Studio Code: File->Preferences->User Snippets, locate the C# language, copy and paste the contents of the corresponding json.

## Changelog Tigra v.0.1.0
The following graphical TIC-80 API functions are implemented:
* pix
* line
* circ
* circb
* rect
* rectb
* tri
* trib [missing in API]
* cls
* border [API equivalent: poke(#03FF8, color)]

Other implemented API functions:
* TIC
* init [mandatory]
* btn
* btnp
* mouse
* trace
* time
* exit

Partially implemented:
* print - prints the text once per tick

## Thanks.
I want to thank for the contribution and for the motivation:
 * [TIC-80](http://twitter.com/@tic_computer)
 * [GI](http://twitter.com/@Gi972)
 * [Sim](http://twitter.com/@_scambier)
 * [Trevor Martin](http://twitter.com/@trelemar)
 * [Filippo](http://twitter.com/@HomineLudens)
 * [MonstersGoBoom](http://twitter.com/@MonstersGo)
 * [Dark Hole](http://twitter.com/@darkh01e)
 * [Mike](https://twitter.com/@torabora08)

---

# Tigra 
## Unity TIC-80 graphical API experiment

Проект "Tigra" - это реализация графических функций API виртуальной игровой консоли TIC-80.
Проект представляет из себя плагин для Unity, позволяющий экспериментировать с графикой TIC-80 используя всю мощь C# и среды Unity!

"Tigra" позволит вам окунуться в атмосферу минималистичности фантазийных консолей.
Малое разрешение экрана, бордюр, палитра из 16 цветов, весь код в единственном файле и простые функции API TIC-80 - это то, что создаёт ограничения и подчеркивает дух игровых ретро-консолей. Профессиональная среда разработки игр Unity и мощный ООП язык C# - это инструменты для удобного и быстрого создания, отладки и публикации ваших креативных идей под множество платформ исполнения. В проект также влючены скрипты для интеграции бесплатного и открытого редактора кода Microsoft Visual Code: сниппеты для автодополнения функций API TIC-80, скрипт интеграции в Unity, настройки игнорируемых файлов ресурсов.

Для того чтобы всё заработало достаточно иметь на сцене один GameObject с пользовательским скриптом, унаследованным от класса `Tic80`. Скрипт `Tic80Config` и отображение будут добавлены автоматически.

В проект включены исходные коды демок для TIC-80, переписанные на C#.
Проект является открытым и свободно распространяемым.
Логотип и марка TIC-80 (c) Copyright 2017 Nesbox https://tic.computer/

![](/Gifs/tigra_demos.gif)

## Описание
Шрифт и цветовую палитру можно выбирать в редакторе - в скрипте `Tic80Config`.
Также в нем можно установить отображение отладочной информации (сейчас она может выводиться только если в скрипте не использован вывод текста через функцию print).
Настройки можно менять как в редакторе, так и во время исполнения игры.
Пользовательский скрипт, унаследованный от класса `Tic80`, может быть активен только один. Если на сцене будут активны другие скрипты `Tic80` - они будут выключены.
При активации пользовательского скрипта во время исполнения - остальные скрипты будут выключены автоматически.
Создавать камеру не нужно, отображение само добавляется при старте игры.

![](/Gifs/tigra_config_settings.gif)

## Создание проекта с нуля.
1. Создаём новую сцену: File -> New Scene. Добавляем на сцену новый GameObject: GameObject -> Create Empty. Далее добавляем на этот объект наш игровой скрипт - либо перетягиванием (если он уже есть), либо нажав кнопку Add Component во вкладке Inspector при выделенном gameObject-е.
Кроме нашего скрипта, на gameObject будет автоматически добавлен скрипт `Tic80Config`.

2. Создаём наш скрипт - класс-потомок класса `Tic80`.
Кроме создания класса вручную, можно использовать создание класса по шаблону. 
Для этого скрипт `99-Tic80 Script-NewTic80Script.cs` уже должен находиться в указанной папке (см. пункт "Размещение и правка шаблона скриптов."). 
Далее, во вкладке Project выбираем папку куда мы хотим поместить скрипт (но НЕ папку Editor), жмем правую кнопку мыши и в контекстном меню выбираем Create -> Tic80 Script. Будет создан новый скрипт, которому нужно дать имя.

![](/Gifs/tigra_new_script.gif)

3. Наполняем "логикой" наш скрипт и запускаем игру, готово!

## Размещение и правка шаблона скриптов.
Для того чтобы создавать скрипты по шаблону, нужно скопировать файл `99-Tic80 Script-NewTic80Script.cs` из папки `Assets/Scripts/Editor` в папку шаблонов скриптов.  
Чтобы отобразить папку-источник и целевую папку, можно воспользоваться командой из контекстного меню `Show in explorer Tic80 Script Templates` либо выбрав её из `Assets > Show in explorer Tic80 Script Templates`.
### Стандартный путь к папке шаблонов скриптов: ### 
* **Windows** `C:\Program Files\Unity\Editor\Data\Resources\ScriptTemplates`
* **Mac** `/Applications/Unity.app/Contents/Resources/ScriptTemplates`
* **Linux** `/opt/Unity/Editor/Data/Resources/ScriptTemplates`  

![](/Gifs/tigra_copy_template.gif)

## Использование сниппетов функций API TIC-80 для Microsoft Visual Studio Code.
Для того чтобы использовать автодополнение кода(сниппеты) функций API TIC-80 проделайте следующее:
1. Откройте файл csharp.json с нужным языком локализации, доступны EN, RU, FR. Они находятся здесь: `Assets/Plugins/VSCodeSnippets`.
2. В Visual Studio Code откройте: File->Preferences->User Snippets, найдите язык C#, скопируйте и вставьте содержимое соответствующего json-а.

## Твит-карты и сокращения
Что такое твит-карты и твит-джемы?
Твит-картом называют пост в tweeter, который содержит сомодостаточный скрипт, который как правило рисует забавную графику на экране.
Размер скрипта ограничен количеством символов в одном твите, т.е. 280 символов.
Твит-джем - это некое соревнование или конкурс, на самый интересный твит-карт посвященный какой либо теме.
Для того, чтобы задействовать мощь языка C#, а также библиотек Unity и уместиться в такой маленький размер кода - мы добавили сокращения.
Можно легко создавать пустую заготовку твит-карта или создать твит-карт с авто-подстановкой кода из твит-карта.
Команды из контекстного меню: `Create > Tic80 New TweetCart` и `Create > Tic80 Paste TweetCart`.

![](/Gifs/tigra_paste_tweet_cart.gif)

## Список изменений Tigra v.0.1.0
Реализованы следующие графические функции API TIC-80:
* pix
* line
* circ
* circb
* rect
* rectb
* tri
* trib [нет в API]
* cls
* border [эквивалент в API: poke(#03FF8, color)]

Реализованы прочие функции API:
* TIC
* init [обязательная функция]
* btn
* btnp
* mouse
* trace
* time
* exit

Частично реализованы:
* print - выводит текст один раз за тик

## Благодарности.
Хочу поблагодарить за вклад и мотивацию:
 * [TIC-80](http://twitter.com/@tic_computer)
 * [GI](http://twitter.com/@Gi972)
 * [Sim](http://twitter.com/@_scambier)
 * [Trevor Martin](http://twitter.com/@trelemar)
 * [Filippo](http://twitter.com/@HomineLudens)
 * [MonstersGoBoom](http://twitter.com/@MonstersGo)
 * [Dark Hole](http://twitter.com/@darkh01e)
 * [Mike](https://twitter.com/@torabora08)