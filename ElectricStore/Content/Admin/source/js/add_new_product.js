var now_product_id = "#";

$(document).ready(function () {
    displayHtml();
    $("#add-product-btn").click(function () {
        addProduct();
    });

    $("#new-product-btn").click(function () {
        refreshInput();
    });

    $("#edit-product-btn").click(function () {
        refreshInput();
    });

    $('.save-image').click(function () {
        createImage();
    });
});

function getParameterInCurrentURL(target) {
    var current_url = new URL(window.location);

    var parameter_value = current_url.searchParams.get(target);
    return parameter_value;
}

function displayHtml() {
    displayComboxBoxCompany();
    displayComboxBoxCategory();
    displayToggle();
    //editProduct();
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
        "ProductName": $('#product-name').val(),
        "CategoryId": $('#category option:selected').val(),
        "ManufactureId": $('#company option:selected').val(),
        "ProductPrice": $('#price').val(),
        "StockStatus": $('#stock-status').is(':checked') ? 'true' : 'false'
    };
    return parameter;
}

function getParameterFormAddProductDetail() {
    var parameter = {
        "ProductId": now_product_id,
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
    if ($("#add-product-btn").attr("value") === "add") {
        url = '/Admin/AddProductApi';
        parameter = getParameterFormAddProduct();
    } else {
        url = '/Admin/AddProductDetailApi';
        parameter = getParameterFormAddProductDetail();
    }
    ajaxAddProduct(url, parameter);
}

function ajaxAddProduct(url, parameter) {
    $(".success-area").empty();
    $(".errors-area").empty();

    $.ajax({
        url: url,
        data: JSON.stringify(parameter),
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var html = '';
            if (data.success) {
                html = '<div class="alert alert-success">';
                html += data.message;
                html += '</div>';
                $(".success-area").append(html);

                // Bật lại phần nhập thông tin chi tiết
                $(".info-advanced-area").removeClass('hidden');
                if (data.type === "add-product") {
                    // Đổi lại trạng thái nút Lưu(Sửa) sản phẩm
                    $("#add-product-btn").attr('value', 'addedit');
                    now_product_id = data.id;
                    blockProductArea();
                } else {
                    // Đổi lại trạng thái nút Lưu mới sản phẩm
                    $("#add-product-btn").attr('value', 'add');
                    blockProductDetailArea();
                }

            } else {
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
            var html = '';
            html += '<div class="alert alert-danger error-area">';
            html += 'Xảy ra lỗi!';
            html += '</div>';
            $(".errors-area").append(html);
        }
    });
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
    html += '<td>' + index + '</td>';
    html += '<td><img src="' + item.ImagePath + '" /></td>';
    html += '<td>' + item.Title + '</td>';
    html += '</tr>';
}

function createImage() {
    var image_path = UploadImage();
    var request = {
        ProductId: now_product_id,
        Title: $('#image-title').val(),
        Alt: $('#image-alt').val(),
        ImagePath: image_path
    };

    $.ajax({
        url: '/Admin/SaveImage',
        data: JSON.stringify(request),
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('.table-image-list tbody').empty();
            var html = '';
            $.each(data, function (index, item) {
                html += createTableImages(index, item);
            });
            $('.table-image-list tbody').append(html);
        }
    });
}

function UploadImage() {
    var image_path = null;

    var fileUpload = $(".file-upload").get(0);
    var files = fileUpload.files;
    //Tạo một đối tượng form data             
    var filedata = new FormData();
    filedata.append("filename", files[0]);
    $.ajax({
        url: '/Admin/UploadFiles',
        data: filedata,
        type: 'POST',
        contentType: false,
        processData: false, 
        success: function (filename) {
            image_path = filename;
        }
    });

    return image_path;
}




