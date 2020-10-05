$(document).ready(function () {
    // Bind the Product Images List on Page Load
    LoadProductImagesList();
});

// Load Product Images List Table
function LoadProductImagesList() {
    // Listing Table Hide till loading...
    $('#divProductImagesList').hide();

    // Loader Show till Listing Table loading...
    $('#loading-image').show();

    // Ajax Call
    $.ajax({
        type: 'GET',
        url: '/product/productimages/bindlist?id=' + $("#hdnProductID").val(),
        success: function (result) {
            $('#divProductImagesList').html(result);
            $('#divProductImagesList').show();
            $('#loading-image').hide();
            $('#successMessage').delay(5000).fadeOut('slow');
        }
    });
}

// Set Image as the Default Image
function SetDefaultImage(productimageid, productid) {
    // Listing Table Hide till loading...
    $('#divProductImagesList').hide();

    // Loader Show till Listing Table loading...
    $('#loading-image').show();

    // Ajax Call
    $.ajax({
        type: 'GET',
        url: '/product/productimages/SetDefaultImage?id=' + productimageid + '&productid=' + productid,
        success: function (result) {
            $('#divProductImagesList').html(result);
            $('#divProductImagesList').show();
            $('#loading-image').hide();
        }
    });
}

// Delete All Product Images
function DeleteAllImages(productid) {
    if (confirm('Do you want to delete all images?')) {
        // Listing Table Hide till loading...
        $('#divProductImagesList').hide();

        // Loader Show till Listing Table loading...
        $('#loading-image').show();

        // Ajax Call
        $.ajax({
            type: 'GET',
            url: '/product/productimages/DeleteAllProductImages?id=' + productid,
            success: function (result) {
                LoadProductImagesList();
            }
        });
    }
}