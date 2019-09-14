﻿$(document).ready(function () {
    displayHtml();
    $("#add-product-btn").click(function () {
        addProduct();
    });
});

function displayHtml() {
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

function getParameterFormAddProduct() {
    var parameter = {
        "ProductName": $('#product_name').val(),
        "CategoryId": $('#category option:selected').val(),
        "ManufactureId": $('#company option:selected').val(),
        "ProductPrice": $('#price').val(),
        "StockStatus": $('#stock-status').is(':checked') ? 'true' : 'false',        
    };
    return parameter;
}

function getParameterFormAddProductDetail() {
    var parameter = {
        "ProductId": $('#product-id').val(),
        "Microprocessor": $('#micro-processor').val(),
        "Speed": $('#speed').val(),
        "Core": $('#core').val(),
        "Graphics": $('#graphics').val(),
        "RAM": $('#ram').val(),
        "Capacity": $('#capacity').val(),
        "Hardware": $('#hardware').val(),
        "Monitor": $('#monitor').val(),
        "Monitorsize": $('#monitor-size').val(),
        "Operation": $('#operation').val(),
        "Color": $('#color').val(),
        "Connection": $('#connection').val(),
        "Gate": $('#gate').val(),
        "Webcam": $('#webcam').is(':checked') ? 'true' : 'false',
        "Recognition": $('#recognition').is(':checked') ? 'true' : 'false',
        "Battery": $('#battery').val(),
        "Size": $('#size').val(),
        "Weight": $('#weight').val(),
        "Description": $('#description').val(),
        "Disc": $('#disc').val()
    };
    return parameter;
}

function addProduct() {
    var parameter = '';
    var url = '';
    if ($("#add-product-btn").val() === "add") {
        url = '/Admin/AddProduct';
        parameter = getParameterFormAddProduct();
    } else {
        url = '/Admin/AddDetailProduct';
        parameter = getParameterFormAddProductDetail();
    }
    ajaxAddProduct(url, parameter);
}

function ajaxAddProduct(url, parameter) {
    $.ajax({
        url: url,
        data: JSON.stringify(parameter),
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var html = '';
            if (data.success && data.status) {
                html = '<div class="alert alert-success">';
                html += data.message;
                html += '</div>';
                $(".success-area").append(html);
            } else if (data.success && !data.status) {
                html = '<div class="alert alert-success">';
                html += data.message;
                html += '</div>';
                $(".success-area").append(html);
            } else {
                $(".errors-area").empty();
                html = '<div class="alert alert-danger error-area">';
                html += '<div class="error-content">';
                html = '<ul style="margin:auto;">';
                $.each(data.validation_errors, function (key, item) {
                    html += '<li>' + item + '</li>';
                });
                html += '</ul></div</div>';
                $(".errors-area").append(html);
            }
        },
        error: function (error) {
            alert("Lỗi");
        }
    });
}