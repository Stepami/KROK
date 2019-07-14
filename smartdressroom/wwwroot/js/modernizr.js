$(document).ready(function () {

    $('#consultant').after($('#size'));
    $('#user').after($('#price'));
    $('#consultant').click(function () {
        $('#consultModal').modal('show');
    });
    $('#consultModalfooter').click(function () {
        $('#consultModal').modal('hide');
    });
});