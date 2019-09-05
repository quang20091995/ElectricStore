var current_page = 1;

$(document).ready(function () {
    //displayAllProducts();
    displayProducts();
    $('.memory-condition-area ul li').on('click', function () {
        if ($(this).hasClass('active')) {
            $(this).removeClass('active');
        } else {
            $(this).addClass('active');
        }
    });

    $('.monitor-size-condition-area ul li').on('click', function () {
        if ($(this).hasClass('active')) {
            $(this).removeClass('active');
        } else {
            $(this).addClass('active');
        }
    });

    $('.condition-search').on('click', function () {
        searchByConditions();
    });
});

//function displayAllProducts() {
//    $('.main-above .row').empty();
//    $.ajax({
//        url: '/Home/GetProduct',
//        type: 'GET',
//        dataType: 'json',
//        contentType: 'application/json; charset=utf-8',
//        success: function (data) {
//            var html = '';
//            $.each(data, function (key, item) {
//                html += createProductItem(item);              
//            });

//            $('.main-above .row').append(html);

//            createPagination(2);

//        },
//        error: function (error) {
//            alert(error);
//        }
//    });
//}

function displayProducts() {
    displayPagination(current_page);
}

function displayPagination(current_page) {
    $('.main-above .row').empty();
    $.ajax({
        url: '/Home/GetProductsByPagination',
        data: { current_page : current_page},
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var html = '';
            $.each(data.ProductList, function (key, item) {
                html += createProductItem(item); 
            });
            $('.main-above .row').append(html);

            // create pagination area when product is exist
            createPagination(data.TotalPage);

            // change color pagination item when clicked item
            $('#page_' + current_page).css('background-color', '#ddd');
        },
        error: function (error) {
            alert(error);
        }
    });
}

function createPagination(total_page) {
    $('.pagination-area').empty();
    var html = '';   
    html += '<ul class="pagination">';
    html += '<li><a href="javascript:void(0);" id="prev">Trước</a></li>';
    for (var i = 1; i <= total_page; i++) {
        html += '<li><a href="javascript:void(0);" onclick="page(' + i + ')" id="page_' + i + '">' + i + '</a></li>';
    }
    html += '<li><a href="javascript:void(0);" id="next">Tiếp</a></li>';
    html += '<span style="padding-left:10px;font-size:20px;color:red">Trang:' + current_page + '/' + total_page + '</span>';
    html += '</ul>';
    $('.pagination-area').append(html);

    $('#prev').on('click', function () {
        current_page--;
        if (current_page < 1) {
            current_page = 1;
        } else {
            displayPagination(current_page);
        }
    });

    $('#next').on('click', function () {
        current_page++;
        if (current_page > total_page) {
            current_page = total_page;
        } else {
            displayPagination(current_page);
        }
    });
}

function page(page_number) {
    current_page = page_number;
    displayPagination(current_page);
}

function createProductItem(product) {
    var html='';
    html += '<div class="col-md-4">';
    html += '<div class="product-item">';
    html += '<div class="pi-img-wrapper">';
    html += '<img src="/Content/Images/LaptopProduct/' + product.ProductImage + '" class="img-responsive" alt = "Berry Lace Dress"/>';
    html += '<div>';
    html += '<a href="#" class="btn">Zoom</a>';
    html += '<a href="#" class="btn">View</a>';
    html += '</div>';
    html += '</div>';
    html += '<h3><a href="/ProductDetail/Index?product_id=' + product.ProductId + '">' + product.ProductName + '</a></h3>';
    html += '<div class="pi-price">' + product.ProductPrice.toLocaleString('it-IT', { style: 'currency', currency: 'VND' }) + '</div>';
    html += '<a href="javascript:;" class="btn add2cart" onclick="clickCart(' + product.ProductId + ')">Thêm vào rỏ</a>';
    html += '</div>';
    html += '</div>';
    return html;
}

function CheckConditions() {
    var conditionByCompany = [];
    $('input[name="company"]:checked').each(function () {
        conditionByCompany.push(this.value);
    });

    var conditionByCPU = [];
    $('input[name="cpu"]:checked').each(function () {
        conditionByCPU.push(this.value);
    });

    var conditionByMemory = [];
    $('.memory-condition-area ul .active a span').each(function () {
        conditionByMemory.push($(this).text());
    });

    var conditionByDisc = [];
    $('input[name="disc"]:checked').each(function () {
        conditionByDisc.push(this.value);
    });

    var conditionByMonitorSize = [];
    $('.monitor-size-condition-area ul .active a span').each(function () {
        conditionByMonitorSize.push($(this).text());
    });

    var conditionByCapacity = [];
    $('input[name="capacity"]:checked').each(function () {
        conditionByCapacity.push(this.value);
    });

    var conditionByCore = [];
    $('input[name="core"]:checked').each(function () {
        conditionByCore.push(this.value);
    });

    var conditionByPrice = [];
    $('input[name="price"]:checked').each(function () {
        conditionByPrice.push(this.value);
    });

    var parameter = {
        'Company': conditionByCompany,
        'CPU': conditionByCPU,
        'Memory': conditionByMemory,
        'Disc': conditionByDisc,
        'Monitorsize': conditionByMonitorSize,
        'Capacity': conditionByCapacity,
        'Core': conditionByCore,
        'Price': conditionByPrice
    };
    return parameter;
}

function searchByConditions() {
    var search_request = CheckConditions();
    $('.main-above .row').empty();
    $.ajax({
        url: '/Search/SearchAdvanced',
        data: JSON.stringify(search_request),
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var html = '';
            $.each(data.ProductList, function (key, item) {
                html += createProductItem(item);
            });
            $('.main-above .row').append(html);

            // create pagination area when product is exist
            createPagination(data.TotalPage);

            // change color pagination item when clicked item
            $('#page_' + current_page).css('background-color', '#ddd');
        },
        error: function (error) {
            alert(error);
        }
    });
}




