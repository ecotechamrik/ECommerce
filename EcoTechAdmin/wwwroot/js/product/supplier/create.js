$('#ctlTransportationCost').on('input', function () {
    CalculateLandedCost();
});

$('#ctlInboundCost').on('input', function () {
    CalculateLandedCost();
});

function CalculateLandedCost() {
    var LandedCost = parseInt($('#ctlTransportationCost').val()) + parseInt($('#ctlInboundCost').val());
    $('#ctlLandedCost').val(LandedCost);
}