What is it?
===========
Ojo is a build monitoring tool akin to CCTray, written in WPF.

Why do this?
============
Primary reason is a lot of the CI setups I see use authentication to control access to certain pipelines.  Unfortunately, CCTray doesn't support authentication out of the box.  As a project I thought it would be cool to do something like it for monitoring builds on a Windows desktop.

Surely this exists?
===================
CCTray is adequate for a lot of purposes, but it falls to lowest common denominator which is the Cruise Control XML output.  Also, when not using CruiseControl.NET the start/stop buttons are useless.  

How do I use this?
==================
Pre-requisites
--------------
I've built Ojo as a .NET 4 project, so the runtime will need to be installed.
Installation
------------
Simplest way to get started is to grab the installer from [HERE](https://github.com/downloads/thenathanjones/ojo/Ojo-0.1.msi) which is the latest one I've bothered to produce.  
Or, if you're feeling up for it, grab the whole lot and build it yourself.  The dependencies are managed by NuGet, and for the moment it's a .NET 4 project, although it's quite possible it will work on earlier versions of the framework.
Configuration
-------------
On startup, Ojo will look for a configuration file in the installed directory called "ojo.yml".  If one isn't present, it will create one with a sample configuration and close.  You may supply the full path to an alternative config file as an argument to the service. 
Here is an example config:

     # Configuration file for Ojo build monitoring tool
     -
       servertype: CruiseControl
       url: http://10.1.1.2:8153/go/cctray.xml
       username: 
       password: 
       pipelines:
         -
           name: "Trunk :: spec"
           
At this point in time it's the standard [Burro](https://github.com/thenathanjones/burro) configuration.  Check that project for details.
It will only read the configuration file on startup, so if you make any changes to the file, you will need to restart the application to pick them up.
Running Ojo
-------------
Ojo is a standard WPF application and as such, after installation it can be executed just like any normal application.  

Limitations
===========
Currently, Ojo is only able to view build servers, but going forward it will be able to control some of them...eventually.
The types of build servers supported are again limited by whatever [Burro](https://github.com/thenathanjones/burro) is capable of, and as such check that project for what you can use.
