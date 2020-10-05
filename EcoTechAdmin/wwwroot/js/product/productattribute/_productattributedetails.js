var oldOrderVal;

/* Function to do not allow negative values into Number Textbox*/
function disallowNegativeNumber(e) {
    var charCode = (e.which) ? e.which : event.keyCode
    if (charCode == 45) {
        return false;
    }
    return true;
}

function UpdateOrder(txtNo, divContainer, id) {
    if (oldOrderVal != $(txtNo).val()) {

        $('#' + divContainer).show();

        $.ajax({
            type: 'GET',
            url: '/Product/ProductAttribute/UpdatePriceVoid/' + id + '/' + $(txtNo).val(),
            success: function () {
                $('#' + divContainer).hide();
            },
            failure: function (response) {                
            }
        });
    }
}

$.fn.focus = function (txtNo) {
    oldOrderVal = $(txtNo).val();
}