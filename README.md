# NwLookup
A Navisworks object inspector similar to AppInfo published in the Navisworks SDK but with easy extensibility and included COM Interop inspection for ModelItems. This project is very similar and loosely based on the Revit Lookup project which provides similar functionality but for the Revit family of software.

As an added benefit NwLookup provides a baseline structure for how to create an addin for the Navisworks family of software. Multiple versions of the addin targeting different versions of Navisworks are included and wrapped together in a bundle package which serves as one source for all the addin requirements.

It is my preference to keep this project both as lightweight and flexible as possible. All the common components including base inspection streams, collectors, views, and data objects are under the NwLookup base project. All subsequent target versions (NwLookup.v*) will incorporate this project which should reduce the number of changes required when updating.

# Build
Build requires Visual Studio to be run as administrator given the build events attempt to create directories under the ProgramData folder ($(ProgramData)\Autodesk\ApplicationPlugins\NwLookup.bundle\).

Additionlly, the projects .csproj have been manually modified to include build events which apply to all projects. A custom macro `TargetYear` has been created for each project and is used in the build events to create the correct directories for each plugin version. 

All specific versions need to include the following at the end:
```
<Project>
  ...
  <Import Project="$(SolutionDir)\Tools\ExtraCleanup.targets" />
  <Import Project="$(SolutionDir)\Tools\Build.events" />
</Project>
```
This, in conjunction with updating the `TargetYear`, will ensure that everything works as expected when building.

Note: This (and all other addins under ApplicationPlugins) will not run unless the package ends in ".bundle". Case DOES matter; ".Bundle" or any variation will not work.

Note: Target version of Navisworks can be found by right clicking on the Autodesk.Navisworks.Api.dll selecting Properties -> Details and looking at the Product Version. All versions follow the form (YEAR - 3), so 2018 is Nw15, 2017 is Nw14 and so on. This is incredibly confusing.

# Usage
There are some important usage notes to keep in mind when using this project.
1. Certain methods are available for invocation as part of the baseline information gathering on objects. These methods are not invoked UNLESS you click on them to inspect. It is up to you to ensure that no unwanted document changes are made when selecting a method to invoke.
2. All instance property information is included in the base inspection and is invoked prior to viewing as to be easily available.
3. If no model items are selected then the ActiveDocument will be the inspection target. This allows you to also inspect the ActiveDocument.State which could very easily let you damage or change the active document. If you are unsure about what you are doing when inspecting objects (specifically invoking methods) then I would recommend researching the topic first or if nothing else creating a copy of the document and inspecting that instead.

# StreamPass
Stream passes are high level passes that you want the collector to perform for certain objects. `StreamPassBase` should always be extended when creating a new Stream Pass as it provides the base methods for reflecting and acquiring method and property information.

An example of an extension `IStreamPass` can be seen in the `ModelItemStreamPass` class located in each version of NwLookup.v*. This pass checks to see if the target object is a `ModelItem` and if it is then it converts it to `ComApi.InwOaPath` and performs inspection on that object as well.

