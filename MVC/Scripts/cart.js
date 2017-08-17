$(window).ready(function ()
{
    $('#quantityform').submit(function (event) {
        $('#quantityform').removeAttr('novalidate');
        var isValid = $('#quantityform').validate();
        if (isValid.errorList.length < 1) { Add() };
        return false;
    });
});


    //add item to the shopping cart
    function Add()
    {
        $('#quantityform').removeAttr('novalidate');
        var isValid = $('#quantityform').validate();

        
        var customerid = $('#customerid').html();

        var url = 'http://localhost:20632/api/cartapi/addtocart/' + customerid;

        var data = {
            productid: $('#ProductID').val(),
            quantity: $('#Quantity').val(),
            packaged: false

        };

        $.ajax({
            url: url,
            type: 'POST',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            error: function (jqXHR, textStatus, errorThrown) {
                alert(errorThrown.Message);
            },
            success: function (response) {
                $('#bar-notification').show()
                    .html($('#productname').html()+" has been added to your cart.")
                    .delay(3000).fadeOut(1000);
            }
        });
    }


    //delete item from the shopping cart
    function deleteitem(data) {
        var id = $(data).attr('id');
        $.ajax({
            url: 'http://localhost:20632/api/cartapi/deleteitem/' + id,
            type: 'GET',
            error: function (jqXHR, textStatus, errorThrown) {
                alert(errorThrown.Message);
            },
            success: function (response) {
                
                $('#bar-notification').show().html($('#productname_' + id)
                    .html() + " has been removed from your cart.").delay(3000).fadeOut(1000);
                $('#subtotal h3').text('Subtotal: ' + response);
                $(data).parent().parent().hide('slow')
            }
        });
    }



    //update item in the shopping cart
    function updateitem(itemid) {
        
        var itemquantity = $('#'+itemid).val();
        var postdata = {
            id: itemid,
            quantity: itemquantity
        };
        
        $.ajax({
            url: 'http://localhost:20632/api/cartapi/updateitem/',
            type: 'POST',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(postdata),
            error: function (jqXHR, textStatus, errorThrown) {
                alert(errorThrown);
            },
            success: function (response) {

                $('#bar-notification').show().html("Cart has been updated.").delay(3000).fadeOut(1000);
                $('#subtotal h3').text('Subtotal: ' + response);
                //  $(data).parent().parent().hide('slow')
            }
        });
    }


    //product catgory filter dropdown
    function filter(data) {

        var id = data.value;
        var url = 'http://localhost:20632/api/productsapi/getproducts/' + id;
        var detailurl = 'http://localhost:20632/Products/Details/';

        var content = '';
        $.getJSON(url, function (response) {
            if (response.length == 0) {
                content += '<tr"><td style="color:red">There are currently no products in this category</td></tr>';
            }


            else {
                $.each(response, function (i, value) {
                    content += '<tr><td> ';
                    content += '<img src=';
                    content += value.SImage;
                    content += '/>';
                    content += ' </td><td><a href= ';
                    content += detailurl;
                    content += value.ProductID;
                    content += '>';
                    content += value.ProductName;
                    content += '</a></td><td> ';
                    content += value.Price;
                    content += ' </td>';
                    content += ' </tr>';
                });
            }
            $('#tbproducts tbody').empty().html(content);
        });
    }