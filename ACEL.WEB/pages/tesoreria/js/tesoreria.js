function toggleDetails(id) {
    var detailsRow = document.getElementById('details-' + id);
    if (detailsRow.style.display === 'none' || detailsRow.style.display === '') {
        detailsRow.style.display = 'table-row';
    } else {
        detailsRow.style.display = 'none';
    }
}