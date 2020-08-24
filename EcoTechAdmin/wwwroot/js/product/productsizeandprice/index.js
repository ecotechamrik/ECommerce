$('a[name*="ctlCreateNew"]').click(function () {
    CreateEditProductSizeAndPrice($('#ctlProductAttributeID').val(), 'Create');
});


// #region [ Create Product Size And Price -- OPEN NEW - Create Form ]
// Create Product Size And Price -- OPEN NEW [Create Form]
function CreateEditProductSizeAndPrice(id, mode) {
    ShowHideDivMessages(false);

    var _url = '/Product/ProductSizeAndPrice/' + mode + '/' + id;

    // Ajax Call
    $.ajax({
        type: 'GET',
        url: _url,
        success: function (result) {
            $('#divProductPriceAndSize').html(result);
        },
        complete: function () {
            ShowHideDivMessages(true);
        }
    });
}
// #endregion [ Create Product Size And Price -- OPEN NEW - Create Form ]

// #region [ Calculate Final Price ]
// Calculate Final Price
function CalculatePrice() {
    CalculateSellingPrice(countNo);
}
// #endregion [ Calculate Final Price ]

// #region [ Calculate Final Price ]
// Calculate Final Price
var countNo = 0;
function CalculateSellingPrice(count) {
    countNo = count;
    var LandedCost = parseInt($("#ctlLandedCost_" + count).val()) || 0;
    var Markup = parseInt($("#ctlMarkup").val()) || 0;
    var RetailPriceDisc = parseInt($("#ctlRetailPriceDisc").val()) || 0;
    var SellingPrice = LandedCost + (LandedCost * (Markup / 100));

    if (RetailPriceDisc != 0) {
        SellingPrice = SellingPrice - (SellingPrice * (RetailPriceDisc / 100));
    }


    $("#ctlSellingPrice").val(SellingPrice.toFixed(2));

    // SET New Values for Supplier's InboundCost, TransportationCost, LandedCost to Save in DB with Save/Update
    $("#ctlSupplierID").val($("#rdbSupplier_" + count).val());
    $("#ctlInboundCost").val($("#ctlInboundCost_" + count).val());
    $("#ctlTransportationCost").val($("#ctlTransportationCost_" + count).val());
    $("#ctlLandedCost").val($("#ctlLandedCost_" + count).val());
}
// #endregion [ Calculate Final Price ]


// Save + Update
function SaveSizeAndPriceDetails() {
    var _form = $("#frmSizeAndPriceForm");

    _form.removeData("validator").removeData("unobtrusiveValidation");//remove the form validation
    $.validator.unobtrusive.parse(_form);//add the form validation

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
                    if (_form.attr("action").toLowerCase().indexOf("create") != -1)
                        $('#successMessage').html("Product Size & Price record has been created successfully.");
                    else
                        $('#successMessage').html("Product Size & Price record has been updated successfully.");

                    $('#successMessage').addClass("text-success");                    
                    $('#successMessage').fadeIn();
                    LoadProductSizeAndPriceList();
                }
                else {
                    alert("Enter data in the form.");  //
                }
            },
            failure: function (response) {
                alert("Error: " + response);  //
            },
            complete: function () {
                $('#successMessage').fadeOut(5000);
            }
        });
    }
    else
        return false;
}


// #region [ Delete Product Size & Price Record ]
function DeleteProductSizeAndPrice(id) {
    if (confirm('Do you want to delete this record?')) {

        ShowHideDivMessages(false);

        $.ajax({
            type: 'GET',
            url: '/Product/ProductSizeAndPrice/Delete/' + id,
            success: function () {
                LoadProductSizeAndPriceList();
                $('#successMessage').html("Product Size & Price record has been deleted successfully.");
                $('#successMessage').addClass("text-danger");
                $('#successMessage').fadeIn();
            },
            complete: function () {
                $('#successMessage').fadeOut(5000);
            }
        });
    }
}
// #endregion