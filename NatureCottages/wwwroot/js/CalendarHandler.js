var dateNow = new Date();
var year = dateNow.getFullYear();

$(document).ready(() => {       
    getMonthsForYear(year);
    getBookingsForYearFromCottage();
});


var getMonthsForYear = (year) => {
    $.ajax({
        url: "Calendar/GetMonthsForYear/" + year,
        success: function (months) {

            //RETURNS 1 LESS THAN ACTUAL SO ADD 1.
            var monthNumber = dateNow.getMonth() + 1;

            var month = getMonthFromCollection(months, monthNumber);            
        },
        error: function (error, type, errorMessage) {
            console.log(error);            
        }
    });
};

var getMonthFromCollection = (months, month) => {
    var i;

    for (i = 0; i < months.length; i++) {
        if (months[i].number === month)
            return months[i];
    }

    return null;
};

var getBookingsForYearFromCottage = () => {
    var table = $("#CalendarTable");

    var cottageId = $(table).attr("cottageId");

    

    //TODO: Remove and think of a way to only render script on single page.
    if (cottageId !== undefined) {
        $.ajax({
            url: "Calendar/GetBookingsForCottageUntilEndOfYear/" + year + "/" + cottageId,
            success: function (bookings) {
                console.log(bookings);
            },
            error: function(error, type, errorMessage) {
                console.log(error);
            }
        });
    }
};