$(document).ready(() => {
    
    var readButtons = $(".read-btn");

    var i;

    for (i = 0; i < readButtons.length; i++) {
	    $(readButtons[i]).click(onReadClicked);
    }
});


var onReadClicked = (e) => {
    var id = getId(e);

    $.ajax({
        url: "/Admin/ReadMessage/" + id,
        method: "GET",
        success: function(partial) {
			$("#MessageArea").html(partial);
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