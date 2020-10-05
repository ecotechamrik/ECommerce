// #region [ Global Variables - thicknesses, heights, widths, suppliers, actieajaxconnections, refreshId ]
var _thicknesses = '';
var _heights = '';
var _widths = '';
var _suppliers = '';
var _activeAjaxConnections = 0;
var refreshId = '';
// #endregion

// #region [ Show Hide Message/Loader Divs on Main Website Listing Page ]
function ShowHideDivMessages(loaded) {
    if (loaded) {
        // Hide Loader after Listing Table loaded...
        $('#loading-image').hide();

        // Listing Table show after loading...
        $('#divThicknessSizes').show();

        $(':input[name="Submit"]').prop('disabled', false);
    }
    else {
        // Loader Show till Listing Table loading...
        $('#loading-image').show();

        // Listing Table Hide till loading...
        $('#divThicknessSizes').hide();

        $(':input[name="Submit"]').prop('disabled', true);
    }
}
// #endregion

// #region [ Document Ready Method to Set the CAD Default Currency and Load the Selected Thicknesses from DB ]
$(document).ready(function () {
    // Set Currency Value Default to CAD in Create New Mode.
    if ($('#hdnProductAttributeID').val() == 0) {
        $("#ctlCurrencyID option:contains('CAD')").attr('selected', 'selected');
    }

    $('input[name^="chkProductThickness"]').each(function () {
        if (this.checked) {
            let chkProductThicknessVal = this.value.split(",");
            if (chkProductThicknessVal.length == 4) // CHECK IF FOUR PARAMETERS PASSED [_productThicknessID, _productAttributeThicknessID, _isChecked, _productThickness]
                GetThicknessSizeDetails(chkProductThicknessVal[0], chkProductThicknessVal[1], chkProductThicknessVal[2], chkProductThicknessVal[3]);
        }
    });
});
// #endregion

// #region [ Select only 1 Thickness at a time ]
// the selector will match all input controls of type :checkbox
// and attach a click event handler 
$("input:checkbox").on('click', function () {
    // in the handler, 'this' refers to the box clicked on
    var $box = $(this);
    if ($box.is(":checked")) {
        // the name of the box is retrieved using the .attr() method
        // as it is assumed and expected to be immutable
        var group = "input:checkbox[name='" + $box.attr("name") + "']";
        // the checked state of the group/box on the other hand will change
        // and the current value is retrieved using .prop() method
        $(group).prop("checked", false);
        $box.prop("checked", true);
    } else {
        $box.prop("checked", false);
    }
});
// #endregion

// #region [ Load Product Size And Price Detailed List for the Selected Thickness Checkbox ]
function GetThicknessSizeDetails(_productThicknessID, _productAttributeThicknessID, _isChecked, _productThickness)
{
    $('#divThicknessSizes_' + _productThicknessID).html('');
    if (_isChecked) {
        // Add Thickness ID to SelectedList Variable
        _thicknesses = _thicknesses + _productThicknessID + ',';

        // Listing Table Hide till loading...
        $('#divThicknessSizes').hide();

        // Loader Show till Listing Table loading...
        $('#loading-image').show();

        $(':input[name="Submit"]').prop('disabled', true);

        $('input[name="chkProductThicknessActive_' + _productThicknessID + '_' + _productAttributeThicknessID + '"]').prop("checked", true);

        // Ajax Call
        $.ajax({
            type: 'GET',
            url: '/Product/ProductAttribute/ProductThickness/' + _productThicknessID + '/' + _productAttributeThicknessID + '/' + _productThickness,
            success: function (result) {
                $('#divThicknessSizes_' + _productThicknessID).html('<hr />' + result);
            },
            complete: function () {
                ShowHideDivMessages(true);
                InsertProductDesc($('#ctlDescription'))
            }
        });
    }
    else {
        // Remove Thickness ID to SelectedList Variable
        _thicknesses = _thicknesses.replace(_productThicknessID + ',', '');
    }
}
// #endregion

// #region [ Find All Product Description Input Controls by name parameter to AutoFill All Product Description ]
function InsertProductDesc(ctlKey) {
    $('input[name^="Description_"]').each(function () {
        let _productWidth = this.id.replace('Description_', 'ProductWidthID_');
        _productWidth = $('#' + _productWidth.substring(0, GetPosition(_productWidth, '_', 3))).val();
        $(this).val($(ctlKey).val() + ' ' + _productWidth + '"');
    });
}
// #endregion

// #region [ Find All Product Code Input Controls by name parameter based on ProductThickness to AutoFill All Product Codes ]
function InsertProductCode(ctlKey, _productThicknessID) {
    $('input[name^="ProductCode_' + _productThicknessID + '_"]').each(function () {
        let _productThickness = $('#ProductThicknessID_' + _productThicknessID).val();
        let _productWidth = this.id.replace('ProductCode_', 'ProductWidthID_');
        _productWidth = $('#' + _productWidth.substring(0, GetPosition(_productWidth, '_', 3))).val();
        let _productHeight = $('#' + this.id.replace('ProductCode_', 'ProductHeightID_')).val();
        $(this).val($(ctlKey).val() + _productWidth + _productHeight + _productThickness);
    });
}
// #endregion

// #region [ Get the nth Occurrence in a String of a Word ]
function GetPosition(string, subString, index) {
    return string.split(subString, index).join(subString).length;
}
// #endregion

// #region [ Calculate Selling Price from the Landing Cost, Markup & Retail Price Disc - OnKeyUp Events of Landing, Transportation, Supplier Checkbox/Radio Buttons ]
function CalculateSellingPrice(ctlKey) {
    let LandedCost = Markup = MarkupPer = RetailPriceDiscPer = RetailPriceDisc = SellingPrice = hdnLiveSupplier = SupplierOption = ctlKey;

    // Check the same Check Box Option for the Available Suppliers Options
    if ($('#' + ctlKey).is(':checked')) {
        SupplierOption = SupplierOption.replace('LiveSupplier', 'SupplierOption');
        $('#' + SupplierOption).prop("checked", true);

        // Get Landed Cost Value
        LandedCost = parseFloat($("#" + LandedCost.replace('LiveSupplier', 'LandedCost')).val()) || 0;

        // Get Markup Value
        Markup = parseFloat($("#" + Markup.substr(0, Markup.lastIndexOf('_')).replace('LiveSupplier', 'Markup')).val()) || 0;
        MarkupPer = $("#" + MarkupPer.substr(0, MarkupPer.lastIndexOf('_')).replace('LiveSupplier', 'MarkupPer'))

        // Get Retail Price Disc Value
        RetailPriceDisc = parseFloat($("#" + RetailPriceDisc.substr(0, RetailPriceDisc.lastIndexOf('_')).replace('LiveSupplier', 'RetailPriceDisc')).val()) || 0;
        RetailPriceDiscPer = $("#" + RetailPriceDiscPer.substr(0, RetailPriceDiscPer.lastIndexOf('_')).replace('LiveSupplier', 'RetailPriceDiscPer'))

        // --- Set Selected Supplier ID for the current Product
        let SupplierID = hdnLiveSupplier.substr(hdnLiveSupplier.lastIndexOf('_') + 1, hdnLiveSupplier.length - 1);
        hdnLiveSupplier = $("#" + hdnLiveSupplier.substr(0, hdnLiveSupplier.lastIndexOf('_')).replace('LiveSupplier', 'hdnLiveSupplier'));
        hdnLiveSupplier.val(SupplierID);

        // Calculate Selling Price
        let SellingPriceAmount = LandedCost * Markup;
        if (RetailPriceDisc != 0) {
            SellingPriceAmount = SellingPriceAmount - (SellingPriceAmount * RetailPriceDisc);
        }

        if (Markup != 0) {
            MarkupPer.html('Markup%: ' + (Markup * 100) + '%');
        }
        else
            MarkupPer.html('Markup%: 0%');

        if (RetailPriceDisc != 0) {
            RetailPriceDiscPer.html('RtlDis%: ' + (RetailPriceDisc * 100) + '%');
        }
        else
            RetailPriceDiscPer.html('RtlDis%: 0%');

        // Set Selling Price
        $("#" + SellingPrice.substr(0, SellingPrice.lastIndexOf('_')).replace('LiveSupplier', 'SellingPrice')).val(SellingPriceAmount.toFixed(2));
    }
}
// #endregion [ CalculateSellingPrice ]

// #region [ Calculate Price on the KeyUp Events of Markup, Retail Disc ]
function CalculatePrice(ctlHiddenKey) {
    ctlHiddenKey = ctlHiddenKey.id.replace('hdnLiveSupplier', 'LiveSupplier') + '_' + $(ctlHiddenKey).val();
    CalculateSellingPrice(ctlHiddenKey);
}
// #endregion

// #region [ Calculate Supplier Landed Cost from the Basic/Inbound Cost and Transportation Cost on KeyUp events of Both TextBoxes ]
function CalculateSupplierCost(ctlKey) {
    InboundCost = parseFloat($("#" + ctlKey.replace('TransportationCost', 'InboundCost')).val()) || 0;
    TransportationCost = parseFloat($("#" + ctlKey.replace('InboundCost', 'TransportationCost')).val()) || 0;
    LandedCost = ctlKey.replace('InboundCost', 'LandedCost').replace('TransportationCost', 'LandedCost');
    SupplierOption = ctlKey.replace('InboundCost', 'SupplierOption').replace('TransportationCost', 'SupplierOption');

    // Calculate the Updated Landed Cost Value
    $('#' + LandedCost).val(InboundCost + TransportationCost);

    if ($('#' + LandedCost).val() > 0) {
        $('#' + SupplierOption).prop("checked", true);
    }
    else {
        $('#' + SupplierOption).prop("checked", false);
    }

    // Calculate the Updated Selling Price
    CalculateSellingPrice(LandedCost.replace('LandedCost', 'LiveSupplier'));
}
// #endregion

// #region [ Save Main Product Attribute Data ]
function SaveUpdateDetails() {

    let _form = $("#frmPriceSizeForm");

    _form.validate({
        rules: {
            ProductAttributeName: {
                required: true,
                minlength: 2,
                maxlength: 200
            },
            CurrencyID: {
                required: true
            },
        },
        messages: {
            ProductAttributeName: {
                required: " Enter Product Attribute Name *",
                minlength: "Minimum length 2 digit.",
                maxlength: "Maximum length 200 digit."
            },
            CurrencyID: {
                required: " Select Currency *"
            }
        },
    });

    if (_form.valid()) {
        $('#saving-image').show();
        $('#divProductAttributes').hide();

        let chkProductThicknessActive = 0;

        $('input[id^="chkProductThicknessActive"]').each(function () {
            if (this.checked && this.value != 0) {
                if (chkProductThicknessActive != 0)
                    chkProductThicknessActive = chkProductThicknessActive + this.value + ',';
                else
                    chkProductThicknessActive = this.value + ',';
            }
        });

        if (chkProductThicknessActive != '')
            chkProductThicknessActive = chkProductThicknessActive.substring(0, chkProductThicknessActive.length - 1);

        let _url = _form.attr("action");

        let formProductAttributeData = new FormData();
        formProductAttributeData.append('ProductAttributeID', $("#hdnProductAttributeID").val());
        formProductAttributeData.append('ProductAttributeName', $("#ctlProductAttributeName").val());
        formProductAttributeData.append('Description', $("#ctlDescription").val());
        formProductAttributeData.append('CurrencyID', $("#ctlCurrencyID").val());
        formProductAttributeData.append('ProductID', $("#hdnProductID").val());
        formProductAttributeData.append('ProductActiveAttributes', chkProductThicknessActive);

        let arrayThickness = (_thicknesses.charAt(_thicknesses.length - 1) == ',' ? _thicknesses.substring(0, _thicknesses.length - 1) : _thicknesses).split(",");
        let arrayHeights = (_heights.charAt(_heights.length - 1) == ',' ? _heights.substring(0, _heights.length - 1) : _heights).split(",");
        let arrayWidths = (_widths.charAt(_widths.length - 1) == ',' ? _widths.substring(0, _widths.length - 1) : _widths).split(",");
        let arraySuppliers = (_suppliers.charAt(_suppliers.length - 1) == ',' ? _suppliers.substring(0, _suppliers.length - 1) : _suppliers).split(",");

        $.ajax({
            url: _url,
            data: formProductAttributeData,
            processData: false,
            contentType: false,
            type: "POST",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
                _activeAjaxConnections++;
            },
            success: function (response) {
                _activeAjaxConnections--;
                if (response != 500) {
                    $("#hdnProductAttributeID").val(response);

                    // Save Product Attribute Thickness Details
                    SaveProductAttributeThickness(arrayThickness, arrayHeights, arrayWidths, arraySuppliers);
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
    else {
        return false;
    }
}
// #endregion

// #region [ Save Product Attribute Thickness Details into Database ]
function SaveProductAttributeThickness(arrayThickness, arrayHeights, arrayWidths, arraySuppliers) {
    if (arrayThickness != '') {
        for (let _countThickness = 0; _countThickness < arrayThickness.length; _countThickness++) {
            let curProductThicknessID = arrayThickness[_countThickness];
            let curProductAttributeThicknessID = $('#ProductAttributeThicknessID_' + curProductThicknessID).val();
            let curAttributeID = $("#hdnProductAttributeID").val();
            let curProductCodeInitials = $('#ProductCodeInitials_' + curProductThicknessID).val();
            
            let formProductAttributeThicknessData = new FormData();
            formProductAttributeThicknessData.append('ProductAttributeThicknessID', curProductAttributeThicknessID);
            formProductAttributeThicknessData.append('ProductAttributeID', curAttributeID);
            formProductAttributeThicknessData.append('ProductThicknessID', curProductThicknessID);
            formProductAttributeThicknessData.append('ProductCodeInitials', curProductCodeInitials);

            let curProductActive = $('input[name="chkProductThicknessActive_' + curProductThicknessID + '_' + curProductAttributeThicknessID + '"]').is(':checked');
            
            if (curProductActive)
                formProductAttributeThicknessData.append('Active', true);
            else
                formProductAttributeThicknessData.append('Active', false);

            let _formAction = (curProductAttributeThicknessID == 0 || curProductAttributeThicknessID == '' || curProductAttributeThicknessID == undefined) ? 'create' : 'edit';
            formProductAttributeThicknessData.append('FormAction', _formAction);

            $.ajax({
                url: '/Product/ProductAttribute/SaveProductAttributeThickness',
                data: formProductAttributeThicknessData,
                processData: false,
                contentType: false,
                type: "POST",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                    _activeAjaxConnections++;
                },
                success: function (response) {
                    _activeAjaxConnections--;
                    if (response != 500) {
                        let responseProductAttributeThicknessID = response;

                        SaveSizeAndPriceDetails(curProductThicknessID, responseProductAttributeThicknessID, arrayHeights, arrayWidths, arraySuppliers);
                    }
                    else {
                        alert("Enter data in the form.");  //
                    }
                },
                failure: function (response) {
                    alert("Error: " + response);  //
                },
                complete: function () {
                    //
                }
            });
        }
        // Variable to Redirect to the Index Page after all data saving
        refreshId = setInterval(RedirectToIndex, 1000);
    }
    else {
        ShowHideSaveMessages();
    }
}
// #endregion

// #region [ Redirect to the Index Page After all Data Saving ]
function RedirectToIndex() {
    if (0 == _activeAjaxConnections)
    {
        clearInterval(refreshId);
        ShowHideSaveMessages();
    }
}
// #endregion

// #region [ Show/Hide Data Save Message and Redirect to Index Page ] 
function ShowHideSaveMessages() {
    $('#saving-image').hide();
    alert('Saved Successfully!!');

    location.href = '/Product/productattribute/edit/' + $("#hdnProductAttributeID").val();
}
// #endregion

// #region [ Save Product Size & Price Details into Database ]
function SaveSizeAndPriceDetails(curProductThicknessID, curProductAttributeThicknessID, arrayHeights, arrayWidths, arraySuppliers) {
    if (arrayWidths != '') {

        // Height Count Loop
        for (let _countWidths = 0; _countWidths < arrayWidths.length; _countWidths++) {

            // Product Width ID
            let curProductWidthID = arrayWidths[_countWidths];

            // Height Count Loop
            for (let _countHeights = 0; _countHeights < arrayHeights.length; _countHeights++) {

                // Product Height ID
                let curProductHeightID = arrayHeights[_countHeights];
                let curHeightIDPattern = 'ProductHeightID_' + curProductThicknessID + '_' + curProductWidthID + '_' + curProductHeightID;
                let curProductSizeAndPriceID = $('#' + curHeightIDPattern.replace('ProductHeightID', 'ProductSizeAndPriceID')).val();
                let curProductCode = $('#' + curHeightIDPattern.replace('ProductHeightID', 'ProductCode')).val();
                let curDescription = $('#' + curHeightIDPattern.replace('ProductHeightID', 'Description')).val();
                let curPriceDate = $('#' + curHeightIDPattern.replace('ProductHeightID', 'PriceDate')).val();
                let curInvDate = $('#' + curHeightIDPattern.replace('ProductHeightID', 'InvDate')).val();
                let curMarkup = $('#' + curHeightIDPattern.replace('ProductHeightID', 'Markup')).val();
                let curRetailPriceDisc = $('#' + curHeightIDPattern.replace('ProductHeightID', 'RetailPriceDisc')).val();
                let curSellingPrice = $('#' + curHeightIDPattern.replace('ProductHeightID', 'SellingPrice')).val();
                let curPriceVoid = $('#' + curHeightIDPattern.replace('ProductHeightID', 'PriceVoid')).val();

                if (curSellingPrice == 0 && curPriceVoid == 0) {

                }
                else {
                    let formPriceAndSizeData = new FormData();
                    formPriceAndSizeData.append('ProductWidthID', curProductWidthID);
                    formPriceAndSizeData.append('ProductAttributeThicknessID', curProductAttributeThicknessID);
                    formPriceAndSizeData.append('ProductHeightID', curProductHeightID);
                    formPriceAndSizeData.append('ProductCode', curProductCode);
                    formPriceAndSizeData.append('Description', curDescription);
                    formPriceAndSizeData.append('PriceDate', curPriceDate);
                    formPriceAndSizeData.append('InvDate', curInvDate);
                    formPriceAndSizeData.append('Markup', curMarkup);
                    formPriceAndSizeData.append('RetailPriceDisc', curRetailPriceDisc);
                    formPriceAndSizeData.append('SellingPrice', curSellingPrice);
                    formPriceAndSizeData.append('PriceVoid', curPriceVoid);
                    formPriceAndSizeData.append('ProductSizeAndPriceID', curProductSizeAndPriceID || 0);

                    let _formPriceAndSizeAction = (curProductSizeAndPriceID == 0 || curProductSizeAndPriceID == '' || curProductSizeAndPriceID == undefined) ? 'create' : 'edit';
                    formPriceAndSizeData.append('FormAction', _formPriceAndSizeAction);

                    $.ajax({
                        url: '/Product/ProductAttribute/SaveProductSizeAndPrice',
                        data: formPriceAndSizeData,
                        processData: false,
                        contentType: false,
                        type: "POST",
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("XSRF-TOKEN",
                                $('input:hidden[name="__RequestVerificationToken"]').val());
                            _activeAjaxConnections++;
                        },
                        success: function (response) {
                            if (response != 500) {
                                _activeAjaxConnections--;

                                let curProductSizeAndPriceID = response;
                                SaveSupplierDetails(curProductThicknessID, curProductHeightID, curProductWidthID, curProductSizeAndPriceID, arraySuppliers);
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
            }
        }
    }
    else {
        location.href = '/Product/productattribute/edit/' + $("#hdnProductAttributeID").val();
    }
}
// #endregion

// #region [ Save Product Supplier Details with their Landing Cost and Option/Live Supplier Details ]
function SaveSupplierDetails(curProductThicknessID, curProductHeightID, curProductWidthID, curProductSizeAndPriceID, arraySuppliers) {
    if (arraySuppliers != '') {

        // Suppliers Count Loop
        for (let _countSuppliers = 0; _countSuppliers < arraySuppliers.length; _countSuppliers++) {
            let curProductSupplierIDPattern = 'ProductSupplierID_' + curProductThicknessID + '_' + curProductWidthID + '_' + curProductHeightID + "_" + arraySuppliers[_countSuppliers];
            let curProductSupplierID = $('#' + curProductSupplierIDPattern).val() || 0;
            let curSupplierID = $('#' + curProductSupplierIDPattern.replace('ProductSupplierID', 'SupplierID')).val();
            let curInboundCost = $('#' + curProductSupplierIDPattern.replace('ProductSupplierID', 'InboundCost')).val();
            let curTransportationCost = $('#' + curProductSupplierIDPattern.replace('ProductSupplierID', 'TransportationCost')).val();
            let curLandedCost = $('#' + curProductSupplierIDPattern.replace('ProductSupplierID', 'LandedCost')).val();
            let curSupplierOption = $('#' + curProductSupplierIDPattern.replace('ProductSupplierID', 'SupplierOption')).prop('checked');
            let curLiveSupplier = $('#' + curProductSupplierIDPattern.replace('ProductSupplierID', 'LiveSupplier')).prop('checked');

            if (curLandedCost != 0) {
                let formProductSupplierData = new FormData();
                formProductSupplierData.append('ProductSupplierID', curProductSupplierID || 0);
                formProductSupplierData.append('SupplierID', curSupplierID);
                formProductSupplierData.append('ProductSizeAndPriceID', curProductSizeAndPriceID);
                formProductSupplierData.append('InboundCost', curInboundCost);
                formProductSupplierData.append('TransportationCost', curTransportationCost);
                formProductSupplierData.append('LandedCost', curLandedCost);
                formProductSupplierData.append('IsOption', curSupplierOption || false);
                formProductSupplierData.append('IsLive', curLiveSupplier || false);

                let _formProductSupplierAction = (curProductSupplierID == 0 || curProductSupplierID == '' || curProductSupplierID == undefined) ? 'create' : 'edit';
                formProductSupplierData.append('FormAction', _formProductSupplierAction);

                $.ajax({
                    url: '/Product/ProductAttribute/SaveProductSupplier',
                    data: formProductSupplierData,
                    processData: false,
                    contentType: false,
                    type: "POST",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("XSRF-TOKEN",
                            $('input:hidden[name="__RequestVerificationToken"]').val());
                        _activeAjaxConnections++;
                    },
                    success: function (response) {
                        if (response != 500) {
                            _activeAjaxConnections--;
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
        }
    }
}
// #endregion
