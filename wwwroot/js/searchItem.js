$(document).ready(function () {

    // Adding selected item
    for (var i = 0; i < $('.item').length; i++) {
        $($('.item')[i]).find('.btn').on('click', function () {
            var btn = $(this);

            // creating row
            var row = $('<tr class="row prod-item"></tr>');

            //creating columns
            var td1 = $('<td id="item-id" class="hidden"></td>').html($($('.item')[parseInt(btn.attr('name'))]).children('input').val());
            var td2 = $('<td id="item-name" class="col-6"></td>').html($($('.item')[parseInt(btn.attr('name'))]).children('td').html());
            var td3 = $('<td class="col-4"><input id="item-quantity" type="number" value="1"></td>');
            var td4 = $('<td class="col-2"><button name="'+ parseInt(btn.attr('name')) +'" class="btn btn-danger addedProductDelete" type="button">DELETE</button></td>');

            //saving columns into row
            row.append(td1, td2, td3, td4);

            // saving row into the table
            $('#productTable').append(row);

            $($('.item')[parseInt(btn.attr('name'))]).addClass('hidden');

            $('.addedProductDelete').on('click', function () {
                var index = parseInt($(this).attr('name'));
                $($('.item')[index]).removeClass('hidden');
                var parentRow = $(this).parents('.row');

                parentRow.remove();
            });

        });
    }

    // searching items in table
    $('#searchItemInput').on('input', function () {
        var textValue = $(this).val();

        for (var i = 0; i < $('.item').length; i++) {
            var item = $($('.item')[i]);

            if (textValue != '') {
                var itemName = item.children('td').html();
                item.hide();
                if (itemName.toLowerCase().search(textValue.toLowerCase()) != -1) {
                    item.show();
                }
            }
            else {
                item.show();
            }
        }
    });


    // saving data inputted 
    $('#addProductData').on('click', function () {
        var productName = $('#productName').val();
        var productPrice = $('#productPrice').val();
        var listOfProductItem = new Array();
        for (var i = 0; i < $('.prod-item').length; i++) {
            var productItem = new Object();
            var item = new Object();
            item.Id = $($('.prod-item')[i]).children('#item-id').html();
            item.Name = $($('.prod-item')[i]).children('#item-name').html();
            item.Quantity = 0;
            productItem.Item = item;
            productItem.Quantity = $($('.prod-item')[i]).find('#item-quantity').val();
            listOfProductItem.push(productItem);
        }

        var productViewModel = new Object();
        productViewModel.Name = productName;
        productViewModel.Price = productPrice;
        productViewModel.Items = listOfProductItem;
        console.log(productViewModel);
        console.log(JSON.stringify(productViewModel));

        // ajax request
        $.ajax({
            type: "POST",
            url: "/Admin/Product/Add",
            data: { 'model': productViewModel },
            dataType: "HTML",
            success: function () {
                location.reload();
            },
        });

    });
});
