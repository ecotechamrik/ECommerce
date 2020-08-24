$(document).ready(function () {
    // Bind the Websites List on Page Load
    LoadWebsitesList();
});

// Enter Key Code check with Null Value
function getIntKey(key) {
    var keycode;
    if (key == null) { keycode = event.keyCode; } else { keycode = key.keyCode; }
    return keycode;
}

// Load Websites List Table
function LoadWebsitesList(boolVal) {
    // boolVal = true means Show All Clicked
    if (boolVal)
        $('#searchText').val('');

    // Listing Table Hide till loading...
    $('#websitesResult').hide();

    // Loader Show till Listing Table loading...
    $('#loading-image').show();

    var searchText = $('#searchText').val();

    // Ajax Call
    $.ajax({
        type: 'GET',
        url: '/websites/search?search=' + searchText,
        success: function (result) {
            $('#websitesResult').html(result);
        },
        complete: function () {
            ShowHideDivMessages();
        }
    });
}

// Back to List / Cancel Click Event
function BackToWebsiteList(actionMessage) {
    $('#loading-image').show();

    $.ajax({
        type: 'GET',
        url: '/websites/websitesmainpage',
        success: function (result) {
            $('#divWebsite').html(result);
            if (actionMessage == "delete") {
                $('#successMessage').html("Website record has been deleted successfully.");
                $('#successMessage').addClass("text-danger");
            }
            else if (actionMessage == "create") {
                $('#successMessage').html("Website record has been created successfully.");
                $('#successMessage').addClass("text-success");
            }
            else if (actionMessage == "edit") {
                $('#successMessage').html("Website record has been updated successfully.");
                $('#successMessage').addClass("text-success");
            }
            $('#successMessage').delay(5000).fadeOut('slow');

            LoadWebsitesList(false);
        }
    });
}

// Create New Website / Edit Website / Details Website
function CRUDActions(actionName, websiteID) {
    var performAction = true;

    if (actionName == 'delete') {
        performAction = confirm('Do you want to delete this record?');
    }
    if (performAction) {
        $('#websitesResult').hide();
        $('#loading-image').show();

        $.ajax({
            type: 'GET',
            url: '/websites/' + actionName + '/' + websiteID,
            success: function (result) {
                if (actionName == 'delete') {
                    BackToWebsiteList("delete");
                }
                else {
                    // Load Create/Edit/Details View
                    $('#divWebsite').html(result);
                }
            },
            complete: function () { }
        });
    }
}

// Save + Update
function SaveWebsiteDetails() {
    var _form = $("#frmWebsiteForm");

    _form.validate({
        rules: {
            WebsiteName: {
                required: true,
                minlength: 2,
                maxlength: 200,
            },
        },
        messages: {
            WebsiteName: {
                required: " Enter Website Name *",
                minlength: "Minimum length 2 digit",
                maxlength: "Minimum length 200 digit",
            },
        },
    });


    if (_form.valid()) {
        var _url = _form.attr("action");
        var _formData = _form.serialize();

        $.ajax({
            type: "POST",
            data: _formData,
            url: _url,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            success: function (response) {
                if (response != 500) {
                    if (_form.attr("action").toLowerCase().indexOf("create") != -1) {
                        BackToWebsiteList("create");
                    }
                    else {
                        BackToWebsiteList("edit");
                    }
                }
                else {
                    alert("Enter data in the form.");  //
                }
            },
            failure: function (response) {
                alert("Error: " + response);  //
            }
        });
    }
    else
        return false;
}

// Show Hide Message/Loader Divs on Main Website Listing Page
function ShowHideDivMessages() {
    $('#loading-image').hide();
    $('#websitesResult').show();

    $("#searchText").keyup(function (event) {
        if (getIntKey(event) == 13) {
            LoadWebsitesList(false);
        }
    });

    // Set Search Text Box Focus on Page Load
    $('#searchText').focus();
}