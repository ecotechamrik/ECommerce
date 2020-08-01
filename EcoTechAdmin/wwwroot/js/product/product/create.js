var isFirstLoad = true;
// Category ID List Selected Index Change Event
$("#CategoryID").change(function () {
    BindSubCategoryList(this.value);
});

// Bind Sub Category List
function BindSubCategoryList(id) {
    $('#divSubCategoryList').html("Loading...");

    // Ajax Call
    $.ajax({
        type: 'GET',
        url: '/subcatgallery/bindsubcategories/' + id,
        success: function (result) {
            $('#divSubCategoryList').html(result);

            // Set the Sub Category Value from the Query String for the first time
            if (isFirstLoad) {
                $('#SubCategoryID').val($('#hdnSubCategoryID').val());
                isFirstLoad = false;
            }
        },
    });
}

// A $( document ).ready() block.
$(document).ready(function () {
    BindSubCategoryList($('#CategoryID :selected').val());
});

function SetSubCatVal() {
    $("#hdnSubCategoryID").val($('#SubCategoryID').val());
}