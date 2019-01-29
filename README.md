# What is TimezoneDetector

TimezoneDetector is an asp.net core library for browser timezone detection. It provides a simple way to detect browser timezone and display browser time.

# Nuget
```xml
  Install-Package TimezoneDetectorCore
```

# Getting started with TimezoneDetector
  * Call the followings in Startup:  
  ```xml
  * services.AddTimezoneDetector();
  * app.UseTimezoneDetector();
  ```
Razor:
  * Add @addTagHelper *, TimezoneDetector to _ViewImports.cshtml
  * Link to Jquery/js-cookies/jstz on your page: 
  ```xml
    <script src='https://code.jquery.com/jquery-3.2.1.min.js'></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/js-cookie/2.2.0/js.cookie.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jstimezonedetect/1.0.6/jstz.min.js"></script>
  ```
  * Insert <timezonedetector /> after reference to jquery/cookies-js/jstz;
  *Use it on the page:
  ```xml
  @inject TimezoneDetector.IDateTimeService dateTimeService
  @using TimezoneDetector
  ```
  * Call dateTimeService.ToClientTime(DateTime.Now, "yyyy-MM-dd") to get browser time;

# License
All source code is licensed under MIT license - http://www.opensource.org/licenses/mit-license.php
