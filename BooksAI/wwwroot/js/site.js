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

function toggleDescription(button) {
    let fullDescription = button.parentElement.nextElementSibling;
    if (fullDescription.style.display === "none" || fullDescription.style.display === "") {
        fullDescription.style.display = "block";
    } else {
        fullDescription.style.display = "none";
    }
}
