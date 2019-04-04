$(document).ready(() => {

    var markButtons = $(".mark-btn");
    var readButtons = $(".read-btn");

    markButtons.forEach((i) => $(i).click(onMarkClicked));
    readButtons.forEach((i) => $(i).click(onReadClicked));
});


var onMarkClicked = (e) => {
	var id = getId(e);
}

var onReadClicked = (e) => {
    var id = getId(e);

    $.ajax({
        url: "",
        method: "get",
        success: function(partial) {

        },
        error: function(error) {
            console.log("error");
        }
    });

}

var getId = (e) => {
    var button = $(e.target);
    var id = Number($(button).attr("messageId"));

    return id;
}