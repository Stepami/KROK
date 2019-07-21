$(document).ready(function () {

    $('#consultant').click(function () {
        $('#consultModal').modal('show');
    });
    $('#consultModalfooter').click(function () {
        $('#consultModal').modal('hide');
    });

    $('#sizeval').html($('#productCarousel').data('size'));
    $('#priceval').html($('#productCarousel').data('price'));
    $('.owl-carousel').owlCarousel({
        center: true,
        items: 1,
        loop: true,
        margin: 5,
        nav: true,
        navText: [
            "<img src='/images/icons/left.png' alt='left nav' style='height: 50px'>",
            "<img src='/images/icons/right.png' alt='right nav' style='height: 50px'>"
        ],
        responsive: {
            600: {
                items: 3
            }
        }
    });
});