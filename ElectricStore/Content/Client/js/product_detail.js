$(document).ready(function () {
    DisplayDetailProduct();
});

function GetParameterInCurrentURL(target) {
    var current_url = new URL(window.location);
    
    var parameter_value = current_url.searchParams.get(target);
    return parameter_value;
}

function DisplayDetailProduct() {
    var product_id = GetParameterInCurrentURL('product_id');
    $.ajax({
        url: '/ProductDetail/ProductDetail',
        data: { product_id: product_id },
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('.product-image-area img').prop('src', '/Content/Images/LaptopProduct/' + data.ProductImage);
            $('.product-name').text(data.ProductName);

            $('.product-microprocessor').text('- Bộ VXL: ' + data.Microprocessor);
            $('.product-graphics').text('- Cạc đồ họa: ' + data.Graphics);
            $('.product-memory').text('- Bộ nhớ: ' + data.RAM + 'GB');
            $('.product-disk-type').text('- Ổ cứng/ Ổ đĩa quang: ' + data.Hardware);
            $('.product-monitor').text('- Màn hình: ' + data.Monitor);
            $('.product-operation').text('- Hệ điều hành: ' + data.Operation);
            $('.product-color-material').text('- Màu sắc/ Chất liệu: ' + data.Color);

            var html = '';
            html += '<tbody>';
            html += '<tr><td class="col-md-3">Sản phẩm</td><td>' + data.CategoryName + '</td></tr>';
            html += '<tr><td class="col-md-3">Tên hãng</td><td>' + data.ManufactureName + '</td></tr>';
            html += '<tr><td class="col-md-3">Model</td><td>' + data.ProductName + '</td></tr>';
            html += '<tr><td class="col-md-3">Bộ VXL</td><td>' + data.Microprocessor + '</td></tr>';
            html += '<tr><td class="col-md-3">Cạc đồ họa</td><td>' + data.Graphics + '</td></tr>';
            html += '<tr><td class="col-md-3">Bộ nhớ</td><td>' + data.RAM + ' GB</td></tr>';
            html += '<tr><td class="col-md-3">Ổ cứng/ Ổ đĩa quang</td><td>' + data.Hardware + '</td></tr>';
            html += '<tr><td class="col-md-3">Màn hình</td><td>' + data.Monitor + ' Inch</td></tr>';
            html += '<tr><td class="col-md-3">Kết nối</td><td>' + data.Connection + '</td></tr>';
            html += '<tr><td class="col-md-3">Cổng giao tiếp</td><td>' + data.Gate + '</td></tr>';
            html += '<tr><td class="col-md-3">Webcam</td><td>' + (data.Webcam? 'Có' : 'Không')  + '</td></tr>';
            html += '<tr><td class="col-md-3">Nhận dạng vân tay</td><td>' + (data.Recognition ? 'Có' : 'Không') + '</td></tr>';
            html += '<tr><td class="col-md-3">Hệ điều hành</td><td>' + data.Operation + '</td></tr>';
            html += '<tr><td class="col-md-3">Pin</td><td>' + data.Battery + ' cell</td></tr>';
            html += '<tr><td class="col-md-3">Kích thước</td><td>' + data.Size + ' cm</td></tr>';
            html += '<tr><td class="col-md-3">Trọng lượng</td><td>' + data.Weight + ' kg</td></tr>';
            html += '<tr><td class="col-md-3">Màu sắc/ Chất liệu</td><td>' + data.Color + '</td></tr>';
            html += '</tbody>';

            $('.table-info').append(html);

        },
        error: function (error) {
            alert('Không load được dữ liệu');
        }
    });
}