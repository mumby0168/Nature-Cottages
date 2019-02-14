$(document).ready(() => {

    var removeButtons = $(".remove-image-button");
    var i = 0;

    if (removeButtons !== null) {
        for (i = 0; i < removeButtons.length; i++) {
            $(removeButtons[i]).click(removeImage);
        }
    }    
});


var removeImage = (e) => {
    var imageCell = $(e.target).closest(".image-cell");
    var imageId = $(imageCell).attr("imageId");

    $.ajax({
        url: "/Form/RemoveImage/" + imageId,
        success: function (result) {
            if (result === true) {
                imageCell.remove();
            } else {
                alert("Something went wrong removing the image please try again.");
            }
        }
    });
};