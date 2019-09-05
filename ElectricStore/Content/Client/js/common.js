$(document).ready(function () {
    init();
});

function init() {
    displayUserInfo();
    displayQuantityCart();
    displayAllCategories();
}

function displayUserInfo() {
    $.ajax({
        url: '/Home/GetSession',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data !== 'LOG_OUT') {
                $('.register').hide();
                $('.log-in').hide();

                $('.login-info').text(data);
                $('.login-info').removeClass('hidden');
                $('.log-out').removeClass('hidden');
            }
            else {
                return;
            }
        },
        error: function (error) {
            return;
        }
    });
}

function displayAllCategories() {
    $.ajax({
        url: '/Home/GetCategory',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            
            var html = '<li class="list-group-item main"><i class="glyphicon glyphicon-th-list"></i> Danh mục sản phẩm</li>';
            var html1 = '';
            $.each(data, function (key, item) {
                html += '<li class="list-group-item" id="link-category"><a href="/Category/Index?category_name=' + item.CategoryName + '">' + item.CategoryName + '</a></li>';
                html1 += '<li class="list-group-item" id="link-category"><a href="/Category/Index?category_name=' + item.CategoryName + '">' + item.CategoryName + '</a></li>';
            });
            $('.new-info .list-group').append(html);
            $('.list-item-category .list-group').append(html1);
        },
        error: function (error) {
            alert(error);
        }
    });
}

function clickCart(product_id) {
    $.ajax({
        url: '/Home/Cart',
        data: { 'product_id': product_id },
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var total_quantity = 0;
            $.each(data, function (key, value) {
                total_quantity += value.Quantity;
            });
            $('.cart-area b').text('Giỏ hàng ' + total_quantity);
        },
        error: function () {
            alert("Xảy ra lỗi khi thêm hàng vào rỏ");
        }
    });
}

function displayQuantityCart() {
    $.ajax({
        url: '/Home/DisplayQuantityCart',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('.cart-area b').text('Giỏ hàng: ' + data);
        },
        error: function () {
            $('.cart-area b').text('Giỏ hàng: ' + 0);
        }
    });
}

function loadCategory() {

}
