$(document).ready(function () {
        ShowCount();
        $("body").on('click', '.btnAddToCart', function (e) {
            e.preventDefault();
            let id = $(this).data("id");
            let quantity = 1;
            let tQuantity = $("#quantity_value").val();
            if (tQuantity !== "") {
                quantity = parseInt(tQuantity);

            }
            $.ajax({
                url: '/shoppingcart/addtocart',
                type: 'POST',
                data: { id: id, quantity: quantity },
                success: function (rs) {
                    location.reload();
                    console.log(rs.msg);
                    
                }

            });
        });


    $("body").on('click', '.btnDelete', function (e) {
        e.preventDefault();
        let id = $(this).data("id");
        let conf = confirm("Bạn có chắc muốn xóa sản phẩm này ra khỏi giỏ hàng ? ");
        if (conf == true) {
            $.ajax({
                url: '/shoppingcart/Delete',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    $("#checkout_items").html(rs.Count);
                    $("#trow_" + id).remove();
                    LoadCart();
                }

            });
        }
        
    });


    $("body").on('click', '.btnDeleteAll', function (e) {
        e.preventDefault();
        let conf = confirm("Bạn có chắc muốn xóa hết sản phẩm trong giỏ hàng không ? ");
        if (conf == true) {
            DeleteAll();
           

        }

    });

    $("body").on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        let id = $(this).data("id");
        let quantity = $("#Quantity_" + id).val();
        Update(id, quantity);

    });

});

function ShowCount() {
    $.ajax({
        url: '/shoppingcart/showcount',
        type: 'GET',
        success: function (rs) {
            $("#checkout_items").html(rs.Count);

        }

    });
}


function DeleteAll() {
    $.ajax({
        url: '/shoppingcart/DeleteAll',
        type: 'POST',
        success: function (rs) {
            LoadCart();

        }

    });
}


function LoadCart() {
    $.ajax({
        url: '/shoppingcart/Partial_Item_Cart',
        type: 'GET',
        success: function (rs) {
            $("#load_data").html(rs);

        }

    });
}


function Update(id,quantity) {
    $.ajax({
        url: '/shoppingcart/Update',
        type: 'POST',
        data: { id: id, quantity: quantity },
        success: function (rs) {
            LoadCart();

        }

    });
}