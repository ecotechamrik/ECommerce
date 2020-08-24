//$(document).ready(function () {
//    // Bind the Websites List on Page Load
//    LoadProductSizeAndPriceList();
//});

//// Load Product Size And Price List
//function LoadProductSizeAndPriceList() {
//    // Listing Table Hide till loading...
//    $('#divProductPriceAndSize').hide();

//    // Loader Show till Listing Table loading...
//    $('#loading-image').show();

//    // Ajax Call
//    $.ajax({
//        type: 'GET',
//        url: '/Product/ProductSizeAndPrice/Index/' + $('#ctlProductAttributeID').val(),
//        success: function (result) {
//            $('#divProductPriceAndSize').html(result);
//        },
//        complete: function () {
//            ShowHideDivMessages(true);
//        }
//    });
//}

// Show Hide Message/Loader Divs on Main Website Listing Page
function ShowHideDivMessages(loaded) {
    if (loaded) {
        // Hide Loader after Listing Table loaded...
        $('#loading-image').hide();

        // Listing Table show after loading...
        $('#divThicknessSizes').show();
    }
    else {
        // Loader Show till Listing Table loading...
        $('#loading-image').show();

        // Listing Table Hide till loading...
        $('#divThicknessSizes').hide();
    }
}

$(document).ready(function () {
    // Set Currency Value Default to CAD in Create New Mode.
    if ($('#ctlProductAttributeID').val() == 0) {
        $("#dllCurrencies option:contains('CAD')").attr('selected', 'selected');
    }

    $("#dllDoorTypes").change(function () {
        if (this.value != '') {
            location.href = '/Product/ProductAttribute/Create/' + $('#hdnProductID').val() + '/' + this.value;
        }
        else
            location.href = '/Product/ProductAttribute/Create/' + $('#hdnProductID').val();
    });
});

// Load Product Size And Price List
function GetThicknessSizeDetails(rdbProductThickness, _productThicknessID, _productAttributeThicknessID, _isChecked, _productThickness) {
    if (_isChecked) {
        // Listing Table Hide till loading...
        $('#divThicknessSizes').hide();

        // Loader Show till Listing Table loading...
        $('#loading-image').show();

        // Ajax Call
        $.ajax({
            type: 'GET',
            url: '/Product/ProductAttribute/ProductThickness/' + _productThicknessID + '/' + _productAttributeThicknessID + '/' + _productThickness,
            success: function (result) {
                $('#divThicknessSizes_' + _productThicknessID).html('<hr />' + result);
            },
            complete: function () {
                ShowHideDivMessages(true);
            }
        });
    }
    else {
        //if (confirm('Do you want to save this data?')) {
        //    //alert('Data has been saved successfully!');
        //    $('#divThicknessSizes_' + _productThicknessID).html('');
        //}
        //else
        $('#divThicknessSizes_' + _productThicknessID).html('');
    }
}

// #region [ Find All Product Code Input Controls by name parameter based on ProductThickness ]
function InsertProductCode(ctlKey, _productThickness) {
    $('input[name^="ctlProductCode_' + _productThickness + '_"]').each(function () {
        var _productHeight = this.id.substr(this.id.lastIndexOf('&') + 1);
        var _productWidth = this.id.substr(this.id.lastIndexOf('_') + 1, (this.id.lastIndexOf('&') - 1) - this.id.lastIndexOf('_'));
        $(this).val($(ctlKey).val() + _productWidth + _productHeight + _productThickness);
    });
}
// #endregion

// #region [ CalculateSellingPrice ]
function CalculateSellingPrice(ctlKey) {
    var ctlLandedCost = ctlMarkup = ctlRetailDisc = ctlSellingPrice = hdnLiveSupplier = chkLiveSupplier = ctlKey;

    // Check the same Check Box Option for the Available Suppliers Options
    if ($('#' + ctlKey).is(':checked')) {
        chkLiveSupplier = chkLiveSupplier.replace('rdoLiveSupplier', 'chkLiveSupplier');
        $('#' + chkLiveSupplier).prop("checked", true);

        ctlLandedCost = parseInt($("#" + ctlLandedCost.replace('rdoLiveSupplier', 'ctlLandedCost')).val()) || 0;

        ctlMarkup = parseInt($("#" + ctlMarkup.substr(0, ctlMarkup.lastIndexOf('_')).replace('rdoLiveSupplier', 'ctlMarkup')).val()) || 0;

        ctlRetailDisc = parseInt($("#" + ctlRetailDisc.substr(0, ctlRetailDisc.lastIndexOf('_')).replace('rdoLiveSupplier', 'ctlRetailDisc')).val()) || 0;

        var supplierID = hdnLiveSupplier.substr(hdnLiveSupplier.lastIndexOf('_') + 1, hdnLiveSupplier.length - 1);
        hdnLiveSupplier = $("#" + hdnLiveSupplier.substr(0, hdnLiveSupplier.lastIndexOf('_')).replace('rdoLiveSupplier', 'hdnLiveSupplier'));
        hdnLiveSupplier.val(supplierID);

        var SellingPrice = ctlLandedCost + (ctlLandedCost * (ctlMarkup / 100));
        var SellingPrice = SellingPrice - (SellingPrice * (ctlRetailDisc / 100));

        $("#" + ctlSellingPrice.substr(0, ctlSellingPrice.lastIndexOf('_')).replace('rdoLiveSupplier', 'ctlSellingPrice')).val(SellingPrice.toFixed(2));
    }
}
// #endregion [ CalculateSellingPrice ]

// #region [ CalculateSellingPrice ]
function CalculatePrice(ctlHiddenKey) {
    var ctlHiddenKey = ctlHiddenKey.id.replace('hdnLiveSupplier', 'rdoLiveSupplier') + '_' + $(ctlHiddenKey).val();
    CalculateSellingPrice(ctlHiddenKey);
}
// #endregion [ CalculateSellingPrice ]