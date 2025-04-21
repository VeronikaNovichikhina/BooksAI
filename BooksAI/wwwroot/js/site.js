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


document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".add-to-cart").forEach(button => {
        button.addEventListener("click", function () {
            const bookId = parseInt(this.getAttribute("data-book-id"));

            let xhr = new XMLHttpRequest();
            xhr.open("POST", "/Basket/AddToCart", true);
            xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4) {
                    if (xhr.status === 200) {
                        let response = JSON.parse(xhr.responseText);
                        if (response.success) {
                            alert(response.message);
                        } else {
                            alert("Ошибка: " + response.message);
                        }
                    } else {
                        console.error("Ошибка: HTTP " + xhr.status);
                        alert("Произошла ошибка при добавлении в корзину.");
                    }
                }
            };
            xhr.send("bookId=" + encodeURIComponent(bookId));
        });
    });

    document.querySelectorAll(".update-cart-form").forEach(form => {
        form.addEventListener("submit", function (e) {
            e.preventDefault();
            const bookId = this.dataset.bookId;
            const quantity = this.querySelector("input[name='quantity']").value;

            fetch("/Basket/UpdateCart", {
                method: "POST",
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                body: `bookId=${bookId}&quantity=${quantity}`
            })
                .then(res => res.json())
                .then(data => {
                    if (data.success) {
                        // Обновляем цену
                        this.closest(".basket-card-detail")
                            .querySelector(".price-container p").textContent = `Цена: ${data.price}`;
                        // Обновляем сумму внизу
                        document.querySelector(".foot strong").textContent = `Итого: ${data.total}`;
                    } else {
                        alert(data.message);
                    }
                });
        });
    });


    // Удаление из корзины без перезагрузки
    document.querySelectorAll(".remove-from-cart").forEach(button => {
        button.addEventListener("click", function () {
            const bookId = parseInt(this.getAttribute("data-book-id"));

            let xhr = new XMLHttpRequest();
            xhr.open("POST", "/Basket/RemoveFromCart", true);
            xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4) {
                    if (xhr.status === 200) {
                        let response = JSON.parse(xhr.responseText);
                        if (response.success) {
                            // Удаляем HTML-элемент из корзины
                            const item = button.closest(".basket-card-detail");
                            item.remove();

                            // Обновляем сумму
                            const totalElement = document.querySelector(".foot strong");
                            if (totalElement) totalElement.textContent = "Итого: " + response.total;

                            // Если ничего не осталось, показать "корзина пуста"
                            if (document.querySelectorAll(".basket-card-detail").length === 0) {
                                document.querySelector(".books").innerHTML = '<div class="empty-cart"><p>Ваша корзина пуста</p></div>';
                            }
                        } else {
                            alert("Ошибка: " + response.message);
                        }
                    } else {
                        console.error("Ошибка: HTTP " + xhr.status);
                        alert("Произошла ошибка при удалении из корзины.");
                    }
                }
            };
            xhr.send("bookId=" + encodeURIComponent(bookId));
        });
    });
});
