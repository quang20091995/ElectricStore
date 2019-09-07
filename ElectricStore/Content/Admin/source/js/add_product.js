$(document).ready(function () {

});

function getParameterFormAddProduct() {
    var parameter = {
        "ProductName": $('#product_name').val(),
        "CategoryId": $('#category option:selected').val(),
        "Manufacture": $('#company option:selected').val(),
        "ProductPrice": $('#price').val(),
        "StockStatus": $('input[name="stock-status"]:checked').val(), 
        "ProductImage": $('#upload-image').get(0).files,
        "ImageTitle": $('#image-title').val(),
        "Microprocessor": $('#micro-processor').val(),
        "Speed": $('#speed').val(),
        "Core": $('#core').val(),
        "Graphics": $('#graphics').val(),
        "RAM": $('#ram').val(),
        "Capacity": $('#capacity').val(),
        "Hardware": $('#hardware').val(),
        "Monitor": $('#monitor').val(),
        "Monitorsize": $('#monitorsize').val(),
        "Operation": $('#operation').val(),
        "Color": $('#color').val(),
        "Connection": $('#connection').val(),
        "Gate": $('#gate').val(),
        "Webcam": $('#webcam').val(),
        "Recognition": $('#recognition').val(),
        "Battery": $('#battery').val(),
        "Size": $('#size').val(),
        "Weight": $('#weight').val(),
        "Description": $('#description').val(),
        "Disc": $('#disc').val()
    };
    return parameter;
}