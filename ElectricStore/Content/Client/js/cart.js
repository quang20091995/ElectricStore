$(document).ready(function () {
    displayCart();
});

function displayCart() {
    $(".cart-items-area table tbody").empty();
    $.ajax({
        url: '/Cart/DisplayCart',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var html;
            if (data !== null) {
                $.each(data, function (key, item) {
                    html += '<tr>';
                    html += '<td>' + key + '</td>';
                    html += '<td><img src="/Content/Images/LaptopProduct/' + item.Image + '" class="img-responsive" /></td>';
                    html += '<td>' + item.ProductName + '</td>';
                    html += '<td>' + item.ProductPrice + '</td>';
                    html += '<td><input class="cart-item-quantity" type="number" value="' + item.Quantity + '"></input></td>';
                    //html += '<td><span class="remove-btn"><button class="btn btn-danger" onclick="removeCartItem(' + item.ProductName + ')">Xóa</button></span></td>';
                    html += '<td>';
                    html += '<span class="remove-btn">';
                    html += '<button class="btn btn-danger" onclick="removeCartItem(' + item.ProductName + ')">Xóa</button>';
                    html += '</span>';
                    html += '</td>';
                    html += '</tr>';                    
                });
                $(".cart-items-area table tbody").append(html);
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

function removeCartItem(product_name) {
    $.ajax({
        url: '/Cart/RemoveCartItem',
        data: { product_name: product_name },
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            displayCart();
        },
        error: function (error) {
            alert('Không thể xóa');
        }
    });
}
