function openCity(evt, cityName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
}

// Get the element with id="defaultOpen" and click on it
document.getElementById("defaultOpen").click();

var clickEvent = false;
$('#myCarousel').on('click', '.nav a', function () {
    clickEvent = true;
    $('.nav li').removeClass('active');
    $(this).parent().addClass('active');
}).on('slid.bs.carousel', function (e) {
    if (!clickEvent) {
        var count = $('.nav').children().length - 1;
        var current = $('.nav li.active');
        current.removeClass('active').next().addClass('active');
        var id = parseInt(current.data('slide-to'));
        if (count === id) {
            $('.nav li').first().addClass('active');
        }
    }
    clickEvent = false;
    });