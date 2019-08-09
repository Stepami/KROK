$(document).ready(function () {

    $('#consultant').click(function () {
        $('#consultModal').modal('show');
        hub.send('onQueryMade', true, null);
    });
    $('#consultModalfooter').click(function () {
        $('#consultModal').modal('hide');
    });

    $('#size').click(function () {
        
        if ($('#sizes').is(":hidden")) {
            $('#sizes').show();
        } else {
            $('#sizes').hide();
        }
    });
    $('#sizes').hide();

    $('#cart a').click(function () {

        $.ajax({
            type: 'GET',
            url: $(this).data('url'),
            success: function (result) {
                $('#mainView').html(result);
            },
            failure: function () {
                console.log('failure');
            }
        });
    });

    $('#getproduct').on('keyup keypress', function (e) {

        var keyCode = e.keyCode || e.which;

        if (keyCode === 13) {

            $.ajax({
                type: 'GET',
                url: $('#getproduct').attr('action'),
                data: {
                    "vcode": $("input[name='vcode']").val()
                },
                success: function (result) {
                    $('#mainView').html(result);
                    initProductView();
                },
                failure: function () {
                    console.log('failure');
                }
            });
            e.preventDefault();
            return false;
        }
    });

    $('#getproduct button').click(function () {

        $.ajax({
            type: 'GET',
            url: $('#getproduct').attr('action'),
            data: {
                "vcode": $("input[name='vcode']").val()
            },
            success: function (result) {
                $('#mainView').html(result);
                initProductView();
            },
            failure: function () {
                console.log('failure');
            }
        });
    });
});

$(document).ajaxSend(function (e, xhr, options) {

    if (options.type.toUpperCase() == "POST") {
        var token = $('form').find("input[name='__RequestVerificationToken']").val();
        xhr.setRequestHeader("RequestVerificationToken", token);
    }
});

function initProductView() {
    $('#productCarousel').carousel();

    $('#sizeval').html($('#productCarousel').data('size'));
    $('#priceval').html($('#productCarousel').data('price'));

    $.ajax({
        type: 'GET',
        url: $('#sizes').data('url'),
        data: {
            "vcode": $('#productCarousel').data('vcode')
        },
        success: function (result) {
            $('#sizes').html(result);
            $("#sizes label").click(function () {

                var size = $(this).find("input[name='sizes']").val();
                $('#sizeval').html(size);
                $("input[name='selectedSize']").val(size);
            });
        },
        failure: function () {
            console.log('failure');
        }
    });

    $('#collectionCarousel').owlCarousel({
        center: true,
        items: 1,
        loop: true,
        margin: 5,
        nav: true,
        navText: [
            "<img src='/images/icons/left.png' alt='left nav' id='leftArrow'>",
            "<img src='/images/icons/right.png' alt='right nav' id='rightArrow'>"
        ],
        responsive: {
            600: {
                items: 3
            }
        }
    });

    $('.links').click(function () {

        var vcode = $(this).data('vcode');

        $.ajax({
            type: 'GET',
            url: $(this).data('url'),
            success: function (result) {
                $('#mainView').html(result);
                initProductView();
                $("input[name='vcode']").val(vcode);
            },
            failure: function () {
                console.log('failure');
            }
        });
    });

    $('#sendproduct').submit(function (event) {

        var form = $(this);
        var urlTo = '';

        if (parseInt($("input[name='quantity']").val()) > 0) {

            $.ajax({
                type: 'POST',
                url: form.attr('action'),
                data: {
                    "id": $("input[name='id']").val(),
                    "quantity": $("input[name='quantity']").val(),
                    "selectedSize": $("input[name='selectedSize']").val()
                },
                success: function (result) {
                    urlTo = result;
                },
                complete: function () {

                    $.ajax({
                        type: 'GET',
                        url: urlTo,
                        success: function (data) {
                            $('#mainView').html(data);
                            initCartView();
                        },
                        failure: function () {
                            console.log('failure');
                        }
                    });
                },
                failure: function () {
                    console.log('failure');
                }
            });
        }
        event.preventDefault();
    });

    $('#bring').off('click');

    $('#bring').click(function () {
        $('#consultModal').modal('show');
        var product = {
            "VendorCode": $('#productCarousel').data('vcode'),
            "SelectedSize": $('#productCarousel').data('size'),
            "ImgUrl": $('#productCarousel').data('imgurl'),
            "ImgCount": $('#productCarousel').data('imgcount'),
        }
        hub.send('onQueryMade', false, product);
    });
}

function initCartView() {
    $('#sizeval').html('');
    $('#priceval').html('');

    $('#countDiv').html($('#totalCount').html());
    $('#span1').html($('#totalValue').html());

    $('.removeProduct').submit(function (event) {

        var form = $(this);
        var url = form.find("input[type='submit']").data('url');
        var id = form.find("input[name='id']").val();
        var urlTo = '';

        $.ajax({
            type: 'POST',
            url: url,
            data: {
                "id": id
            },
            success: function (result) {
                urlTo = result;
            },
            complete: function () {

                $.ajax({
                    type: 'GET',
                    url: urlTo,
                    success: function (data) {
                        $('#mainView').html(data);
                        initCartView();
                    },
                    failure: function () {
                        console.log('failure');
                    }
                });
            },
            failure: function () {
                console.log('failure');
            }
        });
        event.preventDefault();
    });
}