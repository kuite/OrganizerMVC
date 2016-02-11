function AddActivity(btnClicked) {
    var $form = $(btnClicked).parents("form");

    $.ajax({
        type: "POST",
        url: "Calendar/Add",
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