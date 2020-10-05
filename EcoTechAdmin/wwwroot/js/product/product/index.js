$(document).ready(function () {
    // Bind the Products List on Page Load
    LoadProductsList(true);
});

// Enter Key Code check with Null Value
function getIntKey(key) {
    var keycode;
    if (key == null) { keycode = event.keyCode; } else { keycode = key.keyCode; }
    return keycode;
}

// Load Products List Table
function LoadProductsList(boolVal) {
    // boolVal = true means Show All Products link Clicked
    if (boolVal)
        $('#searchText').val('');

    // Listing Table Hide till loading...
    $('#productsResult').hide();

    // Loader Show till Listing Table loading...
    $('#loading-image').show();

    var searchText = $('#searchText').val();

    // Ajax Call
    $.ajax({
        type: 'GET',
        url: '/product/product/search?search=' + searchText,
        success: function (result) {
            $('#productsResult').html(result);
        },
        complete: function () {
            ShowHideDivMessages();
        }
    });
}

// Load Products List Table
function LoadProductsByCode() {
    // Listing Table Hide till loading...
    $('#productsResult').hide();

    // Loader Show till Listing Table loading...
    $('#loading-image').show();

    var searchText = $('#searchProductCodeText').val();

    // Ajax Call
    $.ajax({
        type: 'GET',
        url: '/product/product/SearchByCode?search=' + searchText,
        success: function (result) {
            $('#productsResult').html(result);
        },
        complete: function () {
            ShowHideDivMessages();
        }
    });
}

// Show Hide Message/Loader Divs on Main Product Listing Page
function ShowHideDivMessages() {
    $('#loading-image').hide();
    $('#productsResult').show();

    $("#searchText").keyup(function (event) {
        if (getIntKey(event) == 13) {
            LoadProductsList(false);
        }
    });

    // Set Search Text Box Focus on Page Load
    $('#searchText').focus();
}