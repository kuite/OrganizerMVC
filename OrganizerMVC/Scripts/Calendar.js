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
            //do something with response
        }
    });
    return false;// if it's a link to prevent post

}


function LoadEvents() {
    var events = [];
    $.ajax({
        async: false,
        type: "GET",
        url: "Calendar/GetEvents",
        dataType: "json",
        error: function (xhr, status, error) {
            var alertMsg = string.concat("Error: ", error, "Status: ", status);
            window.alert(alertMsg);
        },
        success: function (response) {

            var json = '{ "events" : [' +
          '{ "title":"Michel" , "start":"2016-02-12T07:30:00" ,"end":"2016-02-12T10:30:00" },' +
          '{ "title":"Richard" , "start":"2016-02-13T07:30:00","end":"2016-02-13T10:30:00" },' +
          '{ "title":"James" , "start":"2016-02-14T07:30:00","end":"2016-02-14T10:30:00" } ]}';


            var obj = JSON.parse(json);
            events = obj;
        }
    });

    return events;


}