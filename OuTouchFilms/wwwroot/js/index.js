$(document).ready(function () {
    $(".owl-carousel").owlCarousel({
        loop: true,
        margin: 20,
        autoplay: true,
        autoplayTimeout: 2000,
        responsive: {
            0: {
                items: 1
            },
            750: {
                items: 2
            },
            1040: {
                items: 3
            },
            1375: {
                items: 4
            },
            1700: {
                items: 5
            },
            2050: {
                items: 6
            }
        },
        autoplayHoverPause: true
    });
});