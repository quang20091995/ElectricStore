var current_page = 1;

$(document).ready(function () {
    displayProducts();
    displayCategory();
    displayCompany();
    init();
});

function displayProducts() {
    displayPagination(current_page);
}


function displayPagination(page) {
    $('.product-admin-table tbody').empty();
    $.ajax({
        url: '/Admin/GetProductsByPagination',
        data: { current_page: page },
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var html = '';
            $.each(data.ProductList, function (key, item) {
               html += createProductItem(item);
            });

            $('.product-admin-table tbody').append(html);

            // create pagination area when product is exist
            createPagination(data.TotalPage);

            // change color pagination item when clicked item
            $('#page_' + current_page).css('background-color', '#ddd');
        },
        error: function (error) {
            alert('Không load được dữ liệu');
        }
    });
}

function createProductItem(product) {
    var html = '';
    html += '<tr>';
    html += '<th style="text-align:center;"><input type="checkbox" value="' + product.ProductId + '"/></th>';
    html += '<td style="text-align:center;">' + product.ProductId + '</td>';
    html += '<td>' + product.ProductName + '</td>';
    html += '<td>' + product.CategoryName + '</td>';
    html += '<td>' + product.Manufacture + '</td>';
    html += '<td>' + product.ProductPrice.toLocaleString('it-IT', { style: 'currency', currency: 'VND' }); + '</td>';
    if (product.StockStatus) {
        html += '<td class="text-center"><i class="fa fa-check-circle text-success"></i></td>';
    } else {
        html += '<td class="text-center"><i class="fa fa-minus-circle text-danger"></i></td>';
    }
    html += '<td style="text-align:center;"><img src="/Content/Images/LaptopProduct/' + product.ProductImage + '"/></td>';
    html += '<td><a href="/Admin/ProductDetail?product_id=' + product.ProductId + '" class="btn btn-default"><i class="fa fa-edit"> Sửa</i></a>';
    html += '<a href="void:javascript(0)" class="btn btn-default" onclick="deleteProduct(' + product.ProductId + ')"><i class="fa fa-remove"> Xóa</i></a></td>';
    html += '</tr>';
    return html;
}

function page(page_number) {
    current_page = page_number;
    displayPagination(current_page);
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

    $('.page_number').text('Trang ' + current_page + '-' + total_page);
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

function displayCategory() {
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

            $('.categorylist').append(html);
        },
        error: function (error) {
            alert('Không load được dữ liệu');
        }
    });
}

function displayCompany() {
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

            $('.companylist').append(html);
        },
        error: function (error) {
            alert('Không load được dữ liệu');
        }
    });
}

function init() {
    var product_id = $('#product-id').val();
    var product_name = $('#product-name').val();
    var category_id = $('#category-id').val();
    var company_id = $('#company-id').val();
    var stock_exist = $("input[name='chk-stock-status']:checked").val();

    if (product_id !== '') {
        //$('#product-name').prop('disabled', true);
    }

    if (product_name !== '') {
        $('#product-id').prop('disabled', true);
    }

    if (category_id !== '#' || company_id !== '#' || stock_exist !== null) {
        //$('#product-name').prop('disabled', true);
        $('#product-id').prop('disabled', true);
    }  
} 

function checkSearch() {
    var search_parameters = {
        'ProductId': $('#product-id').val(),
        'ProductName': $('#product-name').val(),
        'CategoryId': $('#category-id').val(),
        'CompanyId': $('#company-id').val(),
        'StockStatus': $("input[name='chk-stock-status']:checked").val()
    };

    console.log(search_parameters);
}

function deleteProduct(product_id) {
    // Refresh lại thông báo
    refreshMessage();
    var display_confirm = confirm("Press a button!");
    if (display_confirm === true) {
        $.ajax({
            url: '/Admin/DeleteProduct',
            data: { product_id: product_id },
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // Hiển thị sản phẩm lại
                displayProducts();

                // Hiển thị thông điệp xóa thành công
                displayInfoMessage(data.message);            
            },
            error: function (error) {              
                displayErrorMessage(error.message); 
            }
        });
    } else {
        return;
    }
}

function displayInfoMessage(text) {
    html = '<div class="alert alert-success">';
    html += text;
    html += '</div>';
    $(".success-area").append(html);
}

function displayErrorMessage(text) {
    html = '<div class="alert alert-danger">';
    html += text;
    html += '</div>';
    $(".errors-area").append(html);
}

function refreshMessage() {
    $('.success-area').empty();
    $('.errors-area').empty();
}

