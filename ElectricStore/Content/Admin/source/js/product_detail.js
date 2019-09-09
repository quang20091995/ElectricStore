$(document).ready(function () {
    displayHtml();
});

function displayHtml(){
    displayComboxBoxCompany();
    displayComboxBoxCategory();
    displayToggle();
}

function displayToggle() {
    $(".open-close-1").click(function () {
        if ($(".open-close-1").hasClass("glyphicon glyphicon-plus")) {
            $(".open-close-1").removeClass("glyphicon glyphicon-plus");
            $(".open-close-1").addClass("glyphicon glyphicon-minus");
        } else if ($(".open-close-1").hasClass("glyphicon glyphicon-minus")) {
            $(".open-close-1").removeClass("glyphicon glyphicon-minus");
            $(".open-close-1").addClass("glyphicon glyphicon-plus");
        }
    });

    $(".open-close-2").click(function () {
        if ($(".open-close-2").hasClass("glyphicon glyphicon-plus")) {
            $(".open-close-2").removeClass("glyphicon glyphicon-plus");
            $(".open-close-2").addClass("glyphicon glyphicon-minus");
        } else if ($(".open-close-2").hasClass("glyphicon glyphicon-minus")) {
            $(".open-close-2").removeClass("glyphicon glyphicon-minus");
            $(".open-close-2").addClass("glyphicon glyphicon-plus");
        }
    });

    $(".open-close-3").click(function () {
        if ($(".open-close-3").hasClass("glyphicon glyphicon-plus")) {
            $(".open-close-3").removeClass("glyphicon glyphicon-plus");
            $(".open-close-3").addClass("glyphicon glyphicon-minus");
        } else if ($(".open-close-3").hasClass("glyphicon glyphicon-minus")) {
            $(".open-close-3").removeClass("glyphicon glyphicon-minus");
            $(".open-close-3").addClass("glyphicon glyphicon-plus");
        }
    });
}

function displayComboxBoxCompany() {
    $.ajax({
        url: '/Admin/GetCompany',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var html = '';
            $.each(data, function (key, item) {
                html += '<option value="' + item.ManufactureId + '">' + item.Manufacture + '</option>';
            });

            $('.company').append(html);
        },
        error: function (error) {
            alert('Không load được dữ liệu');
        }
    });
}

function displayComboxBoxCategory() {
    $.ajax({
        url: '/Admin/GetCategory',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var html = '';
            $.each(data, function (key, item) {
                html += '<option value="' + item.CategoryId + '">' + item.CategoryName + '</option>';
            });

            $('.category').append(html);
        },
        error: function (error) {
            alert('Không load được dữ liệu');
        }
    });
}