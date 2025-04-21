
function toggleDescription(bookId) {
    const shortDesc = document.getElementById(`desc-short-${bookId}`);
    const fullDesc = document.getElementById(`desc-full-${bookId}`);
    const link = document.getElementById(`toggle-link-${bookId}`);

    if (fullDesc.classList.contains('d-none')) {
        shortDesc.classList.add('d-none');
        fullDesc.classList.remove('d-none');
        link.textContent = 'Свернуть';
    } else {
        shortDesc.classList.remove('d-none');
        fullDesc.classList.add('d-none');
        link.textContent = 'Читать далее';
    }
}

