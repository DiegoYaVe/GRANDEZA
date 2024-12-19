
function formatPrecioBundle(fieldId) {
    var txtField = document.getElementById(fieldId);
    var value = parseFloat(txtField.value.replace(/,/g, ''));

    if (isNaN(value) || value === 0) {
        txtField.value = '0.00';
    } else {
        txtField.value = value.toLocaleString('es-MX', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    }
}
