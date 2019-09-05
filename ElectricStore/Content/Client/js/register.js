$(document).ready(function () {
    $('#register').click(function () {
        Register();
    });
});

function Register() {
    $('.error-area').empty();

    var register_request = {
        Email: $('#register-email').val(),
        Password: $('#password').val(),
        Address: $('#address').val()
    };

    $.ajax({
        url: '/Home/Register',
        data: JSON.stringify(register_request),
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (typeof (data) === 'string') {
                $('.error-area').removeClass('hidden');
                $('.error-area').text(data);
            }
            else {
                alert('Tạo tài khoản thành công');
                $('#register-modal').modal('toggle');               
            }
        },
        error: function (error) {
            alert('Tạo tài khoản thất bại');
        }
    });
}