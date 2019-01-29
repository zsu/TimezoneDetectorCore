$().ready(function () {
    setTimezoneCookie();
});
function setTimezoneCookie() {

    var timezone_cookie = 'timezoneid';

    // if the timezone cookie not exists create one.
    if (!Cookies.get(timezone_cookie)) {

        //// check if the browser supports cookie
        //var test_cookie = 'test cookie';
        //$.cookie(test_cookie, true);

        //// browser supports cookie
        //if ($.cookie(test_cookie)) {

        // delete the test cookie
        //$.cookie(test_cookie, null);

        // create a new cookie
        Cookies.set(timezone_cookie, jstz.determine().name(), { path: '/' });

        // re-load the page
        location.reload();
        //}
    }
    // if the current timezone and the one stored in cookie are different
    // then store the new timezone in the cookie and refresh the page.
    else {

        var storedTimezone = Cookies.get(timezone_cookie);
        var currentTimezone = jstz.determine().name();

        // user may have changed the timezone
        if (storedTimezone !== currentTimezone) {
            Cookies.set(timezone_cookie, currentTimezone, { path: '/' });
            location.reload();
        }
    }
}