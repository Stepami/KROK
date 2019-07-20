$(document).ready(function () {

    $('#consultant').click(function () {
        $('#consultModal').modal('show');
    });
    $('#consultModalfooter').click(function () {
        $('#consultModal').modal('hide');
    });

    $('#sizeval').html($('#productCarousel').data('size'));
    $('#priceval').html($('#productCarousel').data('price'));
});