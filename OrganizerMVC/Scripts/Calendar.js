function AddUserEvent(btnClicked) {
    var $form = $(btnClicked).parents("form");

    $.ajax({
        type: "POST",
        url: "AddEvent",
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
            var myCalendar = $("#calendar");

            myCalendar.fullCalendar();
            var myEvent = {
                title: $("#name").val(),
                allDay: false,
                description: response.description,
                start: response.start,
                end: response.end
            };
            myCalendar.fullCalendar("renderEvent", myEvent);
        }
    });
    return false;// if it's a link to prevent post
}

function CalendarCallback() {
//    var title = $("#calendar .fc-toolbar .fc-center").firstChild.textContent;
}

function DeleteUserEvent(btnClicked) {
    var $form = $(btnClicked).parents("form");

    $.ajax({
        type: "POST",
        url: "DeleteEvent",
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
            //delete event from calendar without rereshing page
        }
    });
    return false;// if it's a link to prevent post
}

function EventDoubleClicked(data) {
    var event = data.data;
    var id = event.id;

    $("#editEventModal").modal("show");
    $("#dateEditor").val(event.start.format("YYYY-MM-DD"));
}


function LoadEvents() {
    var events = [];
    $.ajax({
        async: false,
        type: "GET",
        url: "GetUserEvents",
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