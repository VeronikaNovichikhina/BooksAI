function searchBooks() {
    let input = document.getElementById("searchInput").value.toLowerCase();
    let cards = document.querySelectorAll(".card");
    let emptyMes = document.getElementById("emptyMes");
    let found = false;

    cards.forEach(card => {
        let title = card.querySelector(".card-title").textContent.toLowerCase();
        let author = card.querySelector(".card-author").textContent.toLowerCase();
        if (title.includes(input) || author.includes(input)) {
            card.style.display = "block";
            found = true;
        } else {
            card.style.display = "none";
        }
    });

    if (found) {
        emptyMes.style.display = "none";
    } else {
        emptyMes.style.display = "block";
    }
}

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
