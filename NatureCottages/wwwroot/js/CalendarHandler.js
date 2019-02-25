
var calendarArea;
var cottageId;

var date;

var month;
var year;



$(document).ready(() => {

    date = new Date();

    month = date.getMonth() + 1;
    year = date.getFullYear();


    calendarArea = $("#CalendarArea");
    cottageId = $("#Booking_CottageId").attr("value");
    $(calendarArea).hide();
    loadCalendar();
    $("#ShowCalendar").click(showCalendar);
});



var loadCalendar = () => {
    $.ajax({
        url: "/Calendar/Load",
        data: {month: month, year: year, cottageId: cottageId},
        success: function(html) {
            $(calendarArea).html(html);   
            $("#CalendarForward").click(monthForward);
            $("#CalendarBack").click(monthBack);
        } 
    });

};

var monthForward = () => {

    month++;

    if (month > 12) {
        month = 1;
        year += 1;
    }
    console.log(month + "," + year);

    loadCalendar();

};

var monthBack = () => {

    month--;
    if (month < 1) {
        month = 12;
        year -= 1;
    }
    console.log(month + "," + year);
    loadCalendar();
};

var showCalendar = () => {
    $(calendarArea).show();    
};
