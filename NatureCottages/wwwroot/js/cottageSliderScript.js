
$(document).ready(() => {
    addListeners();
    setupCards();
});


var addListeners = () => {
    var rightBtns = $(".right-slider-button");
    var leftBtns = $(".left-slider-button");

    var i;

    for (i = 0; i < rightBtns.length; i++) {
        $(rightBtns[i]).click(right);
    }
    for (i = 0; i < leftBtns.length; i++) {
        $(leftBtns[i]).click(left);
    }
};

var setupCards = () => {
    cards = $(".cottage-card");

    var i;

    for (i = 0; i < cards.length; i++) {
        var cardimgs = cards[i].getElementsByClassName("card-img");
        var sliderIndex = $(cards[i]).attr("sliderIndex");
        cardimgs[sliderIndex].style.display = "block";
    }
};

var right = (event) => {
    var parent = $(event.target).closest(".cottage-card")[0];    
    var sliderIndex = Number($(parent).attr("sliderIndex"));

    var images = parent.getElementsByClassName("card-img");

    if (sliderIndex === (images.length - 1)) return;

    images[sliderIndex].style.display = "none";

    sliderIndex++;

    images[sliderIndex].style.display = "block";

    $(parent).attr("sliderIndex", sliderIndex);
};

var left = (event) => {

    var parent = $(event.target).closest(".cottage-card")[0];    
    var sliderIndex = Number($(parent).attr("sliderIndex"));

    var images = parent.getElementsByClassName("card-img");

    if (sliderIndex === 0) return;

    images[sliderIndex].style.display = "none";

    sliderIndex--;

    images[sliderIndex].style.display = "block";

    $(parent).attr("sliderIndex",sliderIndex);
};