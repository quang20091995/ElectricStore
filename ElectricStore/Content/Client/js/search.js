$(document).ready(function () {
    $('.button-search').click(function () {
        displayProductsBySearch();

        // Scroll to the product area when clicks search button
        $('html,body').animate({
            scrollTop: $('.main-above').offset().top - 70
        },'fast');
    });
});


function displayProductsBySearch() {
    var keyword = $('#search-input').val();
    $('.main-above .row').empty();
    $.ajax({
        url: '/Search/Search',
        data: { keyword: keyword },
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var html = '';
            $.each(data, function (key, item) {
                html += '<div class="col-md-4">';
                html += '<div class="product-item">';
                html += '<div class="pi-img-wrapper">';
                html += '<img src="/Content/Images/LaptopProduct/' + item.ProductImage + '" class="img-responsive" alt = "Berry Lace Dress"/>';
                html += '<div>';
                html += '<a href="#" class="btn">Zoom</a>';
                html += '<a href="#" class="btn">View</a>';
                html += '</div>';
                html += '</div>';
                html += '<h3><a href="/ProductDetail/Index?product_id=' + item.ProductId + '">' + item.ProductName + '</a></h3>';
                html += '<div class="pi-price">' + item.ProductPrice.toLocaleString('it-IT', { style: 'currency', currency: 'VND' }) + '</div>';
                html += '<a href="javascript:;" class="btn add2cart" onclick="clickCart(' + item.ProductId + ')">Thêm vào rỏ</a>';
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