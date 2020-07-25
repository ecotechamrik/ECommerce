// Variable to set the Sub Category Value from the Query String for the first time
var isFirstLoad = true;

// Save + Update Details into the DB and Upload Image to the Folder
function SaveWebsiteDetails(inputId) {

    if ($("#CategoryID").val() == null || $("#CategoryID").val() == "") {
        alert("Please Select Category.");
        return false;
    }
    if ($("#SubCategoryID").val() == null || $("#SubCategoryID").val() == "") {
        alert("Please Select Sub Category.");
        return false;
    }
    var input = document.getElementById(inputId);
    var files = input.files;
    var formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }

    if (files.length > 0) {
        formData.append('CategoryID', $("#CategoryID").val());
        formData.append('SubCategoryID', $("#SubCategoryID").val());

        $("#loading-image").css({ "display":"block"});
        $("#main-form").css({ "display": "none" });

        $.ajax(
            {
                url: "/SubCatGallery/FileUpload",
                data: formData,
                processData: false,
                contentType: false,
                type: "POST",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                success: function (data) {
                    location.href = "/SubCatGallery/Index/" + $("#SubCategoryID").val() + "/" + $("#CategoryID").val();
                },
                failure: function (response) {
                    alert("Error: " + response);
                    $("#loading-image").css({ "display": "none" });
                    $("#main-form").css({ "display": "block" });
                },
                complete: function () {
                }
            }
        );
    }
    else {
        alert("Please Upload Files.");
        return false;
    }
}

// Category ID List Selected Index Change Event
$("#CategoryID").change(function ()
{
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


// #region [ Show files name after getting files selected into File Control ]
var input = document.getElementById('files');
var infoArea = document.getElementById('file-upload-filename');

input.addEventListener('change', showFileName);

function showFileName(event) {

    // the change event gives us the input it occurred in 
    var input = event.srcElement;

    var fileName = "";
    // the input has an array of files in the `files` property, each one has a name that you can use. We're just using the name here.
    for (var fileCount = 1; fileCount <= input.files.length; fileCount++)
    {
        if (fileName == "")
            fileName = "File " + fileCount + ": " + input.files[fileCount-1].name;
        else
            fileName = fileName + "; File " + fileCount + ": " + input.files[fileCount-1].name;
    }

    // use fileName however fits your app best, i.e. add it into a div
    infoArea.textContent = fileName;
}
// #endregion [ Show files name after getting files selected into File Control ]