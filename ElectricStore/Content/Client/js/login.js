$(document).ready(function () {
    $('#login').click(function () {
        Login();
    }); 

    $('#log-out').click(function () {
        Logout();
    });
});

function Login() {
    $('.error-area').empty();

    var user_request = {
        Username: $('#email').val(), Password: $('#password').val()
    };

    $.ajax({
        url: '/Home/Login',
        data: JSON.stringify(user_request),
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (typeof (data) === 'string') {
                $('.error-area').removeClass('hidden');
                $('.error-area').text(data);
            }
            else {
                alert('Đăng nhập thành công');
                $('#modal-login').modal('toggle');
                $('.register').hide();
                $('.log-in').hide();

                $('.login-info').text(data.Email);
                $('.login-info').removeClass('hidden');
                $('.log-out').removeClass('hidden');
            }
        },
        error: function (error) {
            alert(error);
        }
    });
}

function Logout() {
    $.ajax({
        url: '/Home/CloseSession',
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('.login-info').text("");
            $('.login-info').addClass('hidden');
            $('.log-out').addClass('hidden');

            $('.register').show();
            $('.log-in').show();
        }
    });
}
