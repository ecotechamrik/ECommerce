var bodyClick = true;
var oldOrderVal;
// Get the modal

var modal = document.getElementById("myModal");

function ShowPopUp(img) {
    bodyClick = false;
    // Get the image and insert it inside the modal - use its "alt" text as a caption
    var modalImg = document.getElementById("imgModalContent");
    var captionText = document.getElementById("modalCaption");

    modalImg.src = img.src.replace("T_", "O_");
    captionText.innerHTML = img.alt;

    setInterval(function () {
        bodyClick = true;
    }, 1000);
    modal.style.display = "block";
}

// Body Click Event to Close the Modal Popup
document.body.addEventListener("click", clickBody)
function clickBody() {
    if (bodyClick)
        modal.style.display = "none";
}

// Get the <span> element that closes the modal
//var span = document.getElementsByClassName("close")[0];

// When the user clicks on <span> (x), close the modal
//span.onclick = function () {
//    modal.style.display = "none";
//}

/* Function to do not allow negative values into Number Textbox*/
function disallowNegativeNumber(e) {
    var charCode = (e.which) ? e.which : event.keyCode
    if (charCode == 45) {
        return false;
    }
    return true;
}

function UpdateOrder(txtNo, divContainer, id, divMessage) {
    if (oldOrderVal != $(txtNo).val()) {

        $('#' + divContainer).show();
        $('#' + divMessage).show();

        $.ajax({
            type: 'GET',
            url: '/subcatgallery/updateorder/' + id + '/' + $(txtNo).val(),
            success: function () {
                $('#' + divContainer).hide();
                $('#' + divMessage).html("Order changed.");
                $('#' + divMessage).delay(1000).fadeOut('slow');
            },
            failure: function (response) {
                $('#' + divMessage).html(response);
                $('#' + divMessage).delay(1000).fadeOut('slow');
            }
        });
    }
}

$.fn.focus = function (txtNo) {
    oldOrderVal = $(txtNo).val();
}