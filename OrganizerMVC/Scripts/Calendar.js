function AddUserEvent(btnClicked) {
    var $form = $(btnClicked).parents("form");

    $.ajax({
        type: "POST",
        url: "Calendar/AddEvent",
        dataType: "json",
        data: {
            Title: $("#title").val(),
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
                title: $("#title").val(),
                allDay: false,
                description: response.description,
                start: response.start,
                end: response.end,
                id: response.id
            };
            myCalendar.fullCalendar("renderEvent", myEvent);
        }
    });
    return false;// if it's a link to prevent post
}

function CalendarCallback() {
//    var title = $("#calendar .fc-toolbar .fc-center").firstChild.textContent;
}

function DeleteUserEvent(id) {
    $.ajax({
        type: "POST",
        url: "Calendar/DeleteEvent",
        dataType: "json",
        data: {
            id: id.data
        },
        error: function (xhr, status, error) {
            var f = error.message;
            //do something about the error
        },
        success: function (response) {
            if (response != null) {
                $("#calendar").fullCalendar("removeEvents", response);
                HideEditModal();
            }
        }
    });
    return false;// if it's a link to prevent post
}

function UpdateEvent(id) {
    $.ajax({
        type: "POST",
        url: "Calendar/UpdateEvent",
        dataType: "json",
        data: {
            Id: id.data,
            Title: $("#titleEdit").val(),
            Description: $("#descEdit").val(),
            Date: $("#dateEditor").val(),
            Start: $("#startEdit").val(),
            End: $("#endEdit").val()
        },
        error: function (xhr, status, error) {
            var f = error.message;
            //do something about the error
        },
        success: function (response) {
            if (response != null) {
                var id = response;
                $("#calendar").fullCalendar("removeEvents", id);

                var myEvent = {
                    title: $("#titleEdit").val(),
                    allDay: false,
                    description: $("#descEdit").val(),
                    start: $("#dateEditor").val() + "T" + $("#startEdit").val(),
                    end: $("#dateEditor").val() + "T" + $("#endEdit").val(),
                    id: id
                };

                $("#calendar").fullCalendar("renderEvent", myEvent);

                HideEditModal();
            }
        }
    });
    return false;// if it's a link to prevent post
}

function EventDoubleClicked(data) {
    var event = data.data;
    var id = event.id;

    $("#editEventModal").modal("show");
    $("#dateEditor").val(event.start.format("YYYY-MM-DD"));
    $("#endEdit").val(event.end.format("HH:mm"));
    $("#startEdit").val(event.start.format("HH:mm"));
    $("#titleEdit").val(event.title);
    $("#descEdit").val(event.description);


    $("#delButton").bind("click", id, DeleteUserEvent);
    //$("#delButton").bind("click", HideEditModal);
    $("#saveButton").bind("click", id, UpdateEvent);
    //$("#saveButton").bind("click", HideEditModal);
}

function HideEditModal() {
    $("#editEventModal").modal("hide");
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