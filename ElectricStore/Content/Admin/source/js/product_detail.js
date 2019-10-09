﻿$(document).ready(function () {
    displayHtml();
    $("#save-product-btn").click(function () {
        updateProduct();
    });
});

function GetParameterInCurrentURL(target) {
    var current_url = new URL(window.location);

    var parameter_value = current_url.searchParams.get(target);
    return parameter_value;
}

function displayHtml() {
    displayComboxBoxCompany();
    displayComboxBoxCategory();
    displayToggle();
    displayProductDetail();
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

function getParameterFormUpdateProduct() {
    var parameter = {
        "ProductId": $('#product-id').val(),
        "ProductName": $('#product-name').val(),
        "CategoryId": $('#category option:selected').val(),
        "ManufactureId": $('#company option:selected').val(),
        "ProductPrice": $('#price').val(),
        "StockStatus": $('#stock-status').is(':checked') ? 'true' : 'false',

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

function editCreatedProduct() {
    // enable các input trong form
    $(".product-info .form-control").attr("disabled", false);
    $(".product-info input[type=checkbox]").attr("disabled", false);

    $(".info-advanced-area .form-control").attr("disabled", false);
    $(".info-advanced-area input[type=checkbox]").attr("disabled", false);

    // Chuyển trạng thái của nút thêm mới sang mới nút sửa
    $("#add-product-btn").attr('value', 'edit'); 
}

function blockProductArea() {
    $(".product-info .form-control").attr("disabled", true);
    $(".product-info input[type=checkbox]").attr("disabled", true);
}

function blockProductDetailArea() {
    $(".info-advanced-area .form-control").attr("disabled", true);
    $(".info-advanced-area input[type=checkbox]").attr("disabled", true);
    $(".add-product-btn").attr("disabled", true);
}

function refreshInput() {
    $(".product-info .form-control").val('');
    $(".info-advanced-area .form-control").val('');
    $("input[type='checkbox']").prop('checked', false);
}

function displayImages() {
    var product_id = now_product_id;
    $.ajax({
        url: '/Admin/GetAlbum',
        data: { product_id: now_product_id },
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $.each(data, function (index, item) {
                createTableImages(index, item);
            });
            $('.table-image-list tbody').append(html);
        }
            
    });
} 

function createTableImages(index, item) {
    var html = '';
    html += '<tr>';
    html += '<td><img src="' + item.ImageLink + '" /></td>';
    html += '<td>' + item.ImageName + '</td>';
    html += '</tr>';    
}

function displayProductDetail() {
    var product_id = GetParameterInCurrentURL('product_id');
    if (product_id === null) {
        return;
    }
    $.ajax({
        url: '/Admin/GetDetailProduct',
        data: { product_id: product_id },
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            displayInfoProductToFormControl(data);
        },
        error: function (error) {
            var html = '';
            html += '<div class="alert alert-danger error-area">';
            html += 'Xảy ra lỗi!';
            html += '</div>';
            $(".errors-area").append(html);
        }
    });
}

function displayInfoProductToFormControl(data) {
    // Thông tin cơ bản
    $('#product-id').val(data.ProductId);
    $('#product-name').val(data.ProductName);
    $("#category option:contains(" + data.CategoryName + ")").attr('selected', 'selected');
    $("#company option:contains(" + data.ManufactureName + ")").attr('selected', 'selected');
    $('#price').val(data.ProductPrice);

    // Thông tin nâng cao
    $('#micro-processor').val(data.Microprocessor);
    $('#speed').val(data.Speed);
    $("#core option:contains(" + data.Core + ")").attr('selected', 'selected');
    $('#graphics').val(data.Graphics);
    $('#ram option:selected').val(data.RAM);
    $('#capacity').val(data.Capacity);
    $('#hardware').val(data.Hardware);
    $('#monitor').val(data.Monitor);
    $("#monitor-size option:contains(" + data.Monitorsize + ")").attr('selected', 'selected');
    $("#color option:contains(" + data.Color + ")").attr('selected', 'selected');
    $('#operation').val(data.Operation); 
    $('#connection').val(data.Connection);
    $('#gate').val(data.Gate);
    $('#battery').val(data.Battery);
    $('#size').val(data.Size);
    $('#weight').val(data.Weight);
    $('#description').val(data.Description);
    $('#disc').val(data.Disc);
    data.Recognition ? $('#recognition').attr("checked", true) : $('#recognition').attr("checked", false);
    data.Webcam ? $('#webcam').attr("checked", true) : $('#webcam').attr("checked", false);
    data.StockStatus ? $('#stock-status').attr("checked", true) : $('#webcam').attr("checked", false);
}

function updateProduct() {
    $(".success-area").empty();
    $(".errors-area").empty();

    var parameter = getParameterFormUpdateProduct();
    $.ajax({
        url: '/Admin/UpdateProduct',
        data: JSON.stringify(parameter),
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var html = '';
            if (data.success) {
                displayProductDetail();
                html = '<div class="alert alert-success">';
                html += data.message;
                html += '</div>';
            } else {
                html = '<div class="alert alert-danger">';
                html += data.message;
                html += '</div>';
            }

            $(".success-area").append(html);
        },
        error: function (error) {
            var html = '';
            html += '<div class="alert alert-danger error-area">';
            html += 'Xảy ra lỗi!';
            html += '</div>';
            $(".errors-area").append(html);
        }
    });
}


