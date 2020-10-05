// Variable to set the Product Image Value from the Query String for the first time
var isFirstLoad = true;

// Save + Update Details into the DB and Upload Image to the Folder
function SavProductImagesDetails(inputId) {
    console.log("Start");
    var input = document.getElementById(inputId);
    var files = input.files;
    var formData = new FormData();

    console.log("Files Length: " + files.length);

    if (files.length > 10) {
        alert("You can upload maximum 10 files at one time.");
        return false;
    }
    else {

        for (var i = 0; i != files.length; i++) {
            formData.append("files", files[i]);
        }

        if (files.length > 0) {
            formData.append('ProductID', $("#ProductID").val());

            $("#loading-image").css({ "display": "block" });
            $("#main-form").css({ "display": "none" });

            console.log("Before Ajax Method Call");

            $.ajax(
                {
                    url: "/Product/ProductImages/FileUpload",
                    data: formData,
                    processData: false,
                    contentType: false,
                    type: "POST",
                    success: function (data) {
                        console.log("Uploaded Successfully.");
                        location.href = "/Product/ProductImages/Index/" + $("#ProductID").val();
                    },
                    failure: function (response) {
                        console.log("Error: " + response);
                        alert("Error: " + response);
                        $("#loading-image").css({ "display": "none" });
                        $("#main-form").css({ "display": "block" });
                    },
                    complete: function () {
                        console.log("Completed");
                    }
                }
            );
        }
        else {
            alert("Please Upload Files.");
            return false;
        }
    }
}

// #region [ Show files name after getting files selected into File Control ]
var input = document.getElementById('files');
var infoArea = document.getElementById('file-upload-filename');

input.addEventListener('change', showFileName);

function showFileName(event) {

    // the change event gives us the input it occurred in 
    var input = event.srcElement;

    if (input.files.length > 10) {
        alert("You can upload maximum 10 files at one time.");
        infoArea.textContent = '';
        return false;
    }
    else {
        var fileName = "";
        // the input has an array of files in the `files` property, each one has a name that you can use. We're just using the name here.
        for (var fileCount = 1; fileCount <= input.files.length; fileCount++) {
            if (fileName == "")
                fileName = "File " + fileCount + ": " + input.files[fileCount - 1].name;
            else
                fileName = fileName + "; File " + fileCount + ": " + input.files[fileCount - 1].name;
        }

        // use fileName however fits your app best, i.e. add it into a div
        infoArea.textContent = fileName;
    }
}
// #endregion [ Show files name after getting files selected into File Control ]