$(document).ready(function () {
    // Bind the Sub Cat Gallery List on Page Load
    LoadSubCatGalList();
});

// Load Sub Cat Gallery List Table
function LoadSubCatGalList() {
    // Listing Table Hide till loading...
    $('#divSubCatGalList').hide();

    // Loader Show till Listing Table loading...
    $('#loading-image').show();

    // Ajax Call
    $.ajax({
        type: 'GET',
        url: '/subcatgallery/bindlist?id=' + $("#hdnSubCategoryID").val() + '&catid=' + $("#hdnCategoryID").val(),
    success: function (result) {
        $('#divSubCatGalList').html(result);
        $('#divSubCatGalList').show();
        $('#loading-image').hide();
        $('#successMessage').delay(5000).fadeOut('slow');
    }
});
        }

// Set Image as the Default Image
function SetDefaultImage(subcatgalid, subcatid, catid) {
    // Listing Table Hide till loading...
    $('#divSubCatGalList').hide();

    // Loader Show till Listing Table loading...
    $('#loading-image').show();

    // Ajax Call
    $.ajax({
        type: 'GET',
        url: '/subcatgallery/SetDefaultImage?id=' + subcatgalid + '&subcatid=' + subcatid + '&catid=' + catid,
        success: function (result) {
            $('#divSubCatGalList').html(result);
            $('#divSubCatGalList').show();
            $('#loading-image').hide();
        }
    });
}

// Delete All Sub Category Gallery Images
function DeleteAllImages(subcatid) {
    if (confirm('Do you want to delete all images?')) {
        // Listing Table Hide till loading...
        $('#divSubCatGalList').hide();

        // Loader Show till Listing Table loading...
        $('#loading-image').show();

        // Ajax Call
        $.ajax({
            type: 'GET',
            url: '/subcategory/DeleteAllSubCatGallery?id=' + subcatid,
            success: function (result) {
                LoadSubCatGalList();
            }
        });
    }
}