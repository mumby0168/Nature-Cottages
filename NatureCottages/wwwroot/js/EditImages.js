$(document).ready(() => {


    $("#edit-images-btn").click(editClicked);
});


var editing = false;


var editClicked = () => {

    var removeBtns = $(".remove-image");
    var i;

    if (editing) {        

        for (i = 0; i < removeBtns.length; i++) {
            removeBtns[i].classList.add("invisible");
        }

        editing = false;
    } else {           
        for (i = 0; i < removeBtns.length; i++) {
            removeBtns[i].classList.remove("invisible");
        }
        editing = true;
    }
};