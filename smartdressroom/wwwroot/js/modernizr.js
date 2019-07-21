$(document).ready(function () {

    $('#consultant').click(function () {
        $('#consultModal').modal('show');
    });
    $('#consultModalfooter').click(function () {
        $('#consultModal').modal('hide');
    });

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
    $('#sizeval').html($('#productCarousel').data('size'));
    $('#priceval').html($('#productCarousel').data('price'));

    $('#collectionCarousel').owlCarousel({
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

    $('#sendproduct').submit(function () {
        var form = $(this);
        var urlTo = '';
        if (parseInt($("input[name='quantity']").val()) > 0) {
            $.ajax({
                type: 'POST',
                url: form.attr('action'),
                data: {
                    "id": $("input[name='id']").val(),
                    "quantity": $("input[name='quantity']").val(),
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
        return false;
    });
}

function initCartView() {
    $('#sizeval').html('');
    $('#priceval').html('');

    $('#countDiv').html($('#totalCount').html());
    $('#span1').html($('#totalValue').html());

    $('.removeProduct').submit(function () {
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
            error: function () {
                console.log('err');
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
        return false;
    });
}