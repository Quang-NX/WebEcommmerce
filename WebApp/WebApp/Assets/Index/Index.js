$(function () {
    $.ajax({
        type: 'GET',
        url: '/Home/GetProduct',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (res) {
            var html = '';
            console.log(res);
            $.each(res, function (key, item) {
                html += '<div class="product">';
                html += '< div class="product-img" >';
                hmtl += '<img src="@Url.Content(' + item.UrlImage + ')"></div>';
                html += '< div class="product-body" > <p class="product-category">Thể loại</p> <h3 class="product-name"><a href="#">' + item.Name + '</a ></h3 > ';
                html += '<h4 class="product-price">' + item.Price + '</h4 > ';
                html += '< div class="product-rating" ><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i></div>';
                html += '<div class="product-btns"><button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">Yêu thích</span></button> </div></div><div class="add-to-cart"><button class="add-to-cart-btn"><i class="fa fa-shopping-cart"></i> add to cart</button></div></div >"';
            });
            $(".product2").html(html);
        }
    });
});