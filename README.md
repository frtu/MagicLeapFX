# MagicLeapFX
Visual effects on Magic Leap

## Project setup

Open the new project in Unity.

### Setup for Magic Leap

* _File > Build Settings_
* (Check that Platform is Lumin, else select ***Lumin*** and click ***Switch Platform***)

Setup the right certificate :

* Edit > Project Settings... > Player > MagicLeap - Publishing Settings
* Set ***ML certificate*** to the path of the key

Import Magic Leap package :

* _Assets > Import package > custom package..._
* Choose the Magic Leap Unity package (Installed with Magic Leap Package manager)
* "API Update required: : click ***I made a backup. Go Ahead!***
* In _Hierachy_ tag, delete *Main camera*
* In /Assets/MagicLeap/Core/Prefabs, drag and drop the MagicLeap ***Main camera***

### Setup Zenject (Dependency Injection framework)

* Go to _Window > Asset Store_ or using Cmd+9
* Search for ***zenject***
* Download & Import
* QUICK CHECK : You should be able to see _Window > Zenject Pool Monitor_

Also check the amazing YouTube channel from *Dilmerv Dependency Injection*

## Build

* _File > Build Settings_
* Click ***Build*** to get an installable MPK OR ***Build and Run*** if Magic Leap is connected


