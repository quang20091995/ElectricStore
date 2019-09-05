$(document).ready(function () {
    displayCategoryByProducts();
});

function GetParameterInCurrentURL(target) {
    var current_url = new URL(window.location);

    var parameter_value = current_url.searchParams.get(target);
    return parameter_value;
}


function displayCategoryByProducts() {
    var category_name = GetParameterInCurrentURL('category_name');
    $('.main-above .row').empty();
    $.ajax({
        url: '/Category/SearchByCategory',
        data: { category_name: category_name },
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var html = '';
            $.each(data, function (key, value) {
                html += '<div class="col-md-4">';
                html += '<div class="product-item">';
                html += '<div class="pi-img-wrapper">';
                html += '<img src="/Content/Images/LaptopProduct/' + value.ProductImage + '" class="img-responsive" alt = "Berry Lace Dress"/>';
                html += '<div>';
                html += '<a href="#" class="btn">Zoom</a>';
                html += '<a href="#" class="btn">View</a>';
                html += '</div>';
                html += '</div>';
                html += '<h3><a href="/ProductDetail/Index?product_id=' + value.ProductId + '">' + value.ProductName + '</a></h3>';
                html += '<div class="pi-price">' + value.ProductPrice.toLocaleString('it-IT', { style: 'currency', currency: 'VND' }) + '</div>';
                html += '<a href="javascript:;" class="btn add2cart" onclick="clickCart(' + value.ProductId + ')">Thêm vào rỏ</a>';
                html += '</div>';
                html += '</div>';
            });
            $('.main-above .row').append(html);

        },
        error: function (error) {
            alert('Không tìm thấy kết quả');
        }
    });
}