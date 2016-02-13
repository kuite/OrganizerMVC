function AddUserEvent(btnClicked) {
    var $form = $(btnClicked).parents("form");

    $.ajax({
        type: "POST",
        url: "Calendar/AddEvent",
        dataType: "json",
        data: {
            Name: $("#name").val(),
            Description: $("#desc").val(),
            Date: $("#datePicker").val(),
            Start: $("#start").val(),
            End: $("#end").val()
        },
        error: function (xhr, status, error) {
            //do something about the error
        },
        success: function (response) {
            //$("#calendar").fullCalendar( 'renderEvent', event);
        }
    });
    return false;// if it's a link to prevent post

}


function LoadEvents() {
    var events = [];
    $.ajax({
        async: false,
        type: "GET",
        url: "Calendar/GetUserEvents",
        dataType: "text",
        error: function (xhr, status, error) {
            var alertMsg = string.concat("Error: ", error, "Status: ", status);
            window.alert(alertMsg);
        },
        success: function (response) {
            var formatted = '{ "events" : ' + response;

            var obj = JSON.parse(response);
            events = obj;
        }
    });

    return events;
}