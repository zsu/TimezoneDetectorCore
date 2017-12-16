# What is TimezoneDetector

TimezoneDetector is an asp.net core library for browser timezone detection. It provides a simple way to detect browser timezone and display browser time.

# Nuget
~~~xml
  Install-Package TimezoneDetectorCore
~~~

# Getting started with TimezoneDetector
  * Reference TimezoneDetector.dll
  * Inject IDateTimeService
  * Use it on your page;
```xml
Razor:
@inject TimezoneDetector.IDateTimeService dateTimeService
@using TimezoneDetector
...
//After JQuery reference
@Html.TimezoneDetector()
  * Call dateTimeService.ToClientTime(DateTime.Now, "yyyy-MM-dd") to get browser time;

```
  * Link to Jquery/cookies-js/jstz on your page: 
```xml
    <script src='https://code.jquery.com/jquery-3.2.1.min.js'></script>
    <script src="https://fastcdn.org/Cookies.js/1.2.2/cookies.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jstimezonedetect/1.0.6/jstz.min.js"></script>
```
# License
All source code is licensed under MIT license - http://www.opensource.org/licenses/mit-license.php
